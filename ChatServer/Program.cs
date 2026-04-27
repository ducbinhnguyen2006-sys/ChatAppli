using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;



try
{
    var server = new ChatServer();
    await server.StartAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"[FATAL] {ex.Message}");
}



internal sealed class ChatServer
{
    private const int Port = 8888;

    private readonly IPAddress _ip = IPAddress.Parse("127.0.0.1");
    private readonly Socket _server;
    private readonly IPEndPoint _endPoint;
    private readonly Encoding _encoding = new UTF8Encoding(false);

    private readonly string _usersFilePath = Path.Combine(AppContext.BaseDirectory, "users.json");
    private readonly string _groupsFilePath = Path.Combine(AppContext.BaseDirectory, "groups.json");
    private readonly string _receivedFilesPath = Path.Combine(AppContext.BaseDirectory, "ReceivedFiles");

    private readonly List<ClientSession> _clients = [];
    private readonly object _clientsLock = new();

    private readonly SemaphoreSlim _userLock = new(1, 1);
    private readonly SemaphoreSlim _groupLock = new(1, 1);



    public ChatServer()
    {
        _endPoint = new IPEndPoint(_ip, Port);
        _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    

    public async Task StartAsync()
    {
        try
        {
            await EnsureStorageAsync();

            _server.Bind(_endPoint);
            _server.Listen(10);

            Console.WriteLine($"Server listening on {_endPoint}");

            while (true)
            {
                Socket clientSocket = await _server.AcceptAsync();

                var session = new ClientSession(clientSocket);
                AddClient(session);

                Console.WriteLine($"Client connected: {clientSocket.RemoteEndPoint}");

                _ = Task.Run(() => HandleClientAsync(session));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[START] {ex.Message}");
        }
    }

    // ================= CORE FLOW =================

    private async Task HandleClientAsync(ClientSession session)
    {
        try
        {
            while (true)
            {
                string? command = await ReadPacketAsync(session.Stream);
                if (string.IsNullOrWhiteSpace(command)) break;

                Console.WriteLine($"[RECEIVED] {command}");

                await ProcessCommandAsync(session, command);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CLIENT] {ex.Message}");
        }
        finally
        {
            await RemoveClientAsync(session);
        }
    }

    private async Task ProcessCommandAsync(ClientSession session, string command)
    {
        if (command.StartsWith("REGISTER|"))
            await ProcessRegisterAsync(session, command);
        else if (command.StartsWith("LOGIN|"))
            await ProcessLoginAsync(session, command);
        else if (command.StartsWith("MESSAGE|"))
            await ProcessMessageAsync(session, command);
        else if (command.StartsWith("FILE|"))
            await ProcessFileAsync(session, command);
        else if (command.StartsWith("CREATE_GROUP|"))
            await ProcessCreateGroupAsync(session, command);
        else
            await SendAsync(session, "FAIL|Unknown command.");
    }

    

    private async Task ProcessRegisterAsync(ClientSession session, string command)
    {
        var parts = command.Split('|', 3);
        if (parts.Length < 3)
        {
            await SendAsync(session, "FAIL|Invalid format.");
            return;
        }

        string username = parts[1];
        string password = parts[2];

        await _userLock.WaitAsync();
        try
        {
            var users = await LoadUsersAsync();

            if (users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                await SendAsync(session, "FAIL|Username exists.");
                return;
            }

            users.Add(new UserRecord { Username = username, Password = password });
            await SaveUsersAsync(users);
        }
        finally
        {
            _userLock.Release();
        }

        await SendAsync(session, "SUCCESS|Registered.");
    }

    private async Task ProcessLoginAsync(ClientSession session, string command)
    {
        var parts = command.Split('|', 3);

        var users = await LoadUsersAsync();

        var user = users.FirstOrDefault(u =>
            u.Username.Equals(parts[1], StringComparison.OrdinalIgnoreCase) &&
            u.Password == parts[2]);

        if (user == null)
        {
            await SendAsync(session, "FAIL|Invalid login.");
            return;
        }

        session.Username = user.Username;
        session.IsAuthenticated = true;

        await SendAsync(session, $"SUCCESS|Welcome {user.Username}");
        await BroadcastAsync($"MESSAGE|[System] {user.Username} joined.");
    }

    private async Task ProcessMessageAsync(ClientSession session, string command)
    {
        if (!session.IsAuthenticated)
        {
            await SendAsync(session, "FAIL|Login first.");
            return;
        }

        string msg = command["MESSAGE|".Length..];

        await BroadcastAsync($"MESSAGE|{session.Username}: {msg}");
    }

    private async Task ProcessFileAsync(ClientSession session, string command)
    {
        if (!session.IsAuthenticated)
        {
            await SendAsync(session, "FAIL|Login first.");
            return;
        }

        var parts = command.Split('|', 3);

        string fileName = SanitizeFileName(parts[1]);
        byte[] data = Convert.FromBase64String(parts[2]);

        string path = Path.Combine(_receivedFilesPath,
            $"{DateTime.Now:yyyyMMddHHmmssfff}_{fileName}");

        await File.WriteAllBytesAsync(path, data);

        await BroadcastAsync($"MESSAGE|[File] {session.Username} sent {fileName}");
    }

    private async Task ProcessCreateGroupAsync(ClientSession session, string command)
    {
        if (!session.IsAuthenticated)
        {
            await SendAsync(session, "FAIL|Login first.");
            return;
        }

        string groupName = command["CREATE_GROUP|".Length..];

        await _groupLock.WaitAsync();
        try
        {
            var groups = await LoadGroupsAsync();

            if (groups.Any(g => g.GroupName.Equals(groupName, StringComparison.OrdinalIgnoreCase)))
            {
                await SendAsync(session, "FAIL|Group exists.");
                return;
            }

            groups.Add(new GroupRecord
            {
                GroupName = groupName,
                CreatedBy = session.Username!,
                CreatedAt = DateTime.Now
            });

            await SaveGroupsAsync(groups);
        }
        finally
        {
            _groupLock.Release();
        }

        await SendAsync(session, $"SUCCESS|Group {groupName} created.");
        await BroadcastAsync($"MESSAGE|[System] {session.Username} created group '{groupName}'.");
    }



    private async Task BroadcastAsync(string message)
    {
        List<ClientSession> clients;

        lock (_clientsLock)
        {
            clients = _clients.Where(c => c.IsAuthenticated).ToList();
        }

        foreach (var c in clients)
            await SendAsync(c, message);
    }

    private async Task SendAsync(ClientSession session, string message)
    {
        byte[] payload = _encoding.GetBytes(message);
        byte[] length = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(payload.Length));

        await session.WriteLock.WaitAsync();
        try
        {
            await session.Stream.WriteAsync(length);
            await session.Stream.WriteAsync(payload);
        }
        finally
        {
            session.WriteLock.Release();
        }
    }

    private async Task<string?> ReadPacketAsync(NetworkStream stream)
    {
        byte[] lenBuf = new byte[4];

        if (!await ReadExactAsync(stream, lenBuf, 4))
            return null;

        int length = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(lenBuf));

        byte[] data = new byte[length];

        if (!await ReadExactAsync(stream, data, length))
            return null;

        return _encoding.GetString(data);
    }

    private static async Task<bool> ReadExactAsync(NetworkStream stream, byte[] buffer, int length)
    {
        int readTotal = 0;

        while (readTotal < length)
        {
            int read = await stream.ReadAsync(buffer.AsMemory(readTotal, length - readTotal));
            if (read == 0) return false;

            readTotal += read;
        }

        return true;
    }

    

    private async Task EnsureStorageAsync()
    {
        Directory.CreateDirectory(_receivedFilesPath);

        if (!File.Exists(_usersFilePath))
            await File.WriteAllTextAsync(_usersFilePath, "[]", _encoding);

        if (!File.Exists(_groupsFilePath))
            await File.WriteAllTextAsync(_groupsFilePath, "[]", _encoding);
    }

    private async Task<List<UserRecord>> LoadUsersAsync()
    {
        string json = await File.ReadAllTextAsync(_usersFilePath);
        return JsonSerializer.Deserialize<List<UserRecord>>(json) ?? [];
    }

    private async Task SaveUsersAsync(List<UserRecord> users)
    {
        string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_usersFilePath, json);
    }

    private async Task<List<GroupRecord>> LoadGroupsAsync()
    {
        string json = await File.ReadAllTextAsync(_groupsFilePath);
        return JsonSerializer.Deserialize<List<GroupRecord>>(json) ?? [];
    }

    private async Task SaveGroupsAsync(List<GroupRecord> groups)
    {
        string json = JsonSerializer.Serialize(groups, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_groupsFilePath, json);
    }

   

    private void AddClient(ClientSession session)
    {
        lock (_clientsLock)
        {
            _clients.Add(session);
        }
    }

    private async Task RemoveClientAsync(ClientSession session)
    {
        lock (_clientsLock)
        {
            _clients.Remove(session);
        }

        session.Stream.Dispose();
        session.Socket.Dispose();

        if (session.IsAuthenticated)
        {
            await BroadcastAsync($"MESSAGE|[System] {session.Username} left.");
        }
    }

    

    private static string SanitizeFileName(string fileName)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            fileName = fileName.Replace(c, '_');

        return fileName;
    }
}



internal sealed class ClientSession
{
    public ClientSession(Socket socket)
    {
        Socket = socket;
        Stream = new NetworkStream(socket, false);
    }

    public Socket Socket { get; }
    public NetworkStream Stream { get; }
    public SemaphoreSlim WriteLock { get; } = new(1, 1);

    public bool IsAuthenticated { get; set; }
    public string? Username { get; set; }
}

internal sealed class UserRecord
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}

internal sealed class GroupRecord
{
    public string GroupName { get; set; } = "";
    public string CreatedBy { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}