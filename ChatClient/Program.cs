using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatClient;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new LoginForm());
    }
}

internal static class AppSession
{
    public static ChatClientService Client { get; } = new();
    public static string Username { get; set; } = string.Empty;
}

internal static class ProtocolHelper
{
    public static bool IsSuccess(string response)
    {
        return response.StartsWith("SUCCESS", StringComparison.Ordinal);
    }

    public static string GetPayload(string response)
    {
        int index = response.IndexOf('|');
        return index >= 0 ? response[(index + 1)..] : response;
    }

    public static bool ContainsReservedDelimiter(string value)
    {
        return value.Contains('|');
    }
}

internal sealed class ChatClientService : IDisposable
{
    private const int Port = 8888;
    private readonly IPAddress _ip = IPAddress.Parse("127.0.0.1");
    private readonly Encoding _encoding = new UTF8Encoding(false);
    private readonly SemaphoreSlim _connectionLock = new(1, 1);
    private readonly SemaphoreSlim _sendLock = new(1, 1);
    private readonly object _syncRoot = new();
    private Socket? _clientSocket;
    private NetworkStream? _stream;
    private CancellationTokenSource? _listenCts;
    private Task? _listenTask;
    private TaskCompletionSource<string>? _pendingResponse;

    public event Action<string>? MessageReceived;
    public event Action<string>? StatusReceived;

    public async Task<string> SendCommandAsync(string command, bool expectResponse = true)
    {
        try
        {
            await EnsureConnectedAsync();

            TaskCompletionSource<string>? pendingResponse = null;
            if (expectResponse)
            {
                pendingResponse = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
                lock (_syncRoot)
                {
                    if (_pendingResponse is not null)
                    {
                        throw new InvalidOperationException("Another request is still waiting for a response.");
                    }

                    _pendingResponse = pendingResponse;
                }
            }

            await _sendLock.WaitAsync();
            try
            {
                await WritePacketAsync(command);
            }
            finally
            {
                _sendLock.Release();
            }

            if (!expectResponse)
            {
                return "SUCCESS|Command sent.";
            }

            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            using var registration = timeoutCts.Token.Register(() =>
            {
                pendingResponse?.TrySetResult("FAIL|Server timeout.");
            });

            return await pendingResponse!.Task;
        }
        catch (Exception ex)
        {
            ClearPending($"FAIL|{ex.Message}");
            await ResetConnectionAsync();
            return $"FAIL|{ex.Message}";
        }
    }

    private async Task EnsureConnectedAsync()
    {
        if (_clientSocket?.Connected == true && _stream is not null)
        {
            return;
        }

        await _connectionLock.WaitAsync();
        try
        {
            if (_clientSocket?.Connected == true && _stream is not null)
            {
                return;
            }

            CloseCurrentConnection();

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ep = new IPEndPoint(ip, 8888);

            Socket client = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            await client.ConnectAsync(ep);

            _clientSocket = client;
            _stream = new NetworkStream(client, ownsSocket: true);
            _listenCts = new CancellationTokenSource();
            _listenTask = ListenAsync(_listenCts.Token);
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    private async Task ListenAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                string? packet = await ReadPacketAsync(cancellationToken);
                if (string.IsNullOrWhiteSpace(packet))
                {
                    break;
                }

                DispatchPacket(packet);
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            StatusReceived?.Invoke($"Connection lost: {ex.Message}");
            ClearPending($"FAIL|Connection lost: {ex.Message}");
        }
        finally
        {
            CloseCurrentConnection();
        }
    }

    private void DispatchPacket(string packet)
    {
        try
        {
            if (packet.StartsWith("MESSAGE|", StringComparison.Ordinal))
            {
                MessageReceived?.Invoke(packet["MESSAGE|".Length..]);
                return;
            }

            if (packet.StartsWith("SUCCESS", StringComparison.Ordinal) || packet.StartsWith("FAIL", StringComparison.Ordinal))
            {
                TaskCompletionSource<string>? pending = null;
                lock (_syncRoot)
                {
                    pending = _pendingResponse;
                    if (pending is not null)
                    {
                        _pendingResponse = null;
                    }
                }

                if (pending is not null)
                {
                    pending.TrySetResult(packet);
                    return;
                }
            }

            StatusReceived?.Invoke(ProtocolHelper.GetPayload(packet));
        }
        catch (Exception ex)
        {
            StatusReceived?.Invoke(ex.Message);
        }
    }

    private async Task WritePacketAsync(string message)
    {
        if (_stream is null)
        {
            throw new InvalidOperationException("No active connection.");
        }

        byte[] payload = _encoding.GetBytes(message);
        byte[] lengthBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(payload.Length));

        await _stream.WriteAsync(lengthBytes);
        await _stream.WriteAsync(payload);
        await _stream.FlushAsync();
    }

    private async Task<string?> ReadPacketAsync(CancellationToken cancellationToken)
    {
        if (_stream is null)
        {
            return null;
        }

        byte[] lengthBuffer = new byte[4];
        bool hasLength = await ReadExactAsync(_stream, lengthBuffer, 4, cancellationToken);
        if (!hasLength)
        {
            return null;
        }

        int length = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(lengthBuffer, 0));
        if (length <= 0)
        {
            return null;
        }

        byte[] dataBuffer = new byte[length];
        bool hasData = await ReadExactAsync(_stream, dataBuffer, length, cancellationToken);
        if (!hasData)
        {
            return null;
        }

        return _encoding.GetString(dataBuffer);
    }

    private static async Task<bool> ReadExactAsync(NetworkStream stream, byte[] buffer, int length, CancellationToken cancellationToken)
    {
        int offset = 0;
        while (offset < length)
        {
            int read = await stream.ReadAsync(buffer.AsMemory(offset, length - offset), cancellationToken);
            if (read == 0)
            {
                return false;
            }

            offset += read;
        }

        return true;
    }

    private void ClearPending(string message)
    {
        TaskCompletionSource<string>? pending = null;
        lock (_syncRoot)
        {
            pending = _pendingResponse;
            _pendingResponse = null;
        }

        pending?.TrySetResult(message);
    }

    private async Task ResetConnectionAsync()
    {
        await _connectionLock.WaitAsync();
        try
        {
            CloseCurrentConnection();
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    private void CloseCurrentConnection()
    {
        try
        {
            _listenCts?.Cancel();
        }
        catch
        {
        }

        try
        {
            _stream?.Dispose();
        }
        catch
        {
        }

        try
        {
            _clientSocket?.Dispose();
        }
        catch
        {
        }

        _listenTask = null;
        _listenCts = null;
        _stream = null;
        _clientSocket = null;
    }

    public void Dispose()
    {
        ClearPending("FAIL|Connection closed.");
        CloseCurrentConnection();
        _connectionLock.Dispose();
        _sendLock.Dispose();
    }
}
