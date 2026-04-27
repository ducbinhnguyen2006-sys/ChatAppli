namespace ChatClient;

public partial class ChatForm : Form
{
    private readonly string _username;

    public ChatForm(string username)
    {
        _username = username;
        InitializeComponent();
        lblSubtitle.Text = $"Connected as {_username}";

        AppSession.Client.MessageReceived += HandleIncomingMessage;
        AppSession.Client.StatusReceived += HandleStatusMessage;
    }

    private void ChatForm_Load(object sender, EventArgs e)
    {
        AppendChatLine($"[System] Welcome, {_username}", Color.FromArgb(74, 144, 226));
    }

    private async void btnSend_Click(object sender, EventArgs e)
    {
        await SendMessageAsync();
    }

    private async Task SendMessageAsync()
    {
        try
        {
            string content = txtMessage.Text.Trim();
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }

            ToggleComposer(false);
            txtMessage.Clear();
            await AppSession.Client.SendCommandAsync($"MESSAGE|{content}", false);
            txtMessage.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Send Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            ToggleComposer(true);
        }
    }

    private async void btnSendFile_Click(object sender, EventArgs e)
    {
        try
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "All files (*.*)|*.*",
                Title = "Select a file"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            string fileName = Path.GetFileName(dialog.FileName);
            if (ProtocolHelper.ContainsReservedDelimiter(fileName))
            {
                MessageBox.Show("The selected file name contains an unsupported character '|'.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ToggleComposer(false);
            byte[] fileBytes = await File.ReadAllBytesAsync(dialog.FileName);
            string base64 = Convert.ToBase64String(fileBytes);
            await AppSession.Client.SendCommandAsync($"FILE|{fileName}|{base64}", false);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "File Send Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            ToggleComposer(true);
        }
    }

    private void btnCreateGroup_Click(object sender, EventArgs e)
    {
        try
        {
            using var groupForm = new GroupForm();
            groupForm.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Group Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void txtMessage_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                _ = SendMessageAsync();
            }
        }
        catch
        {
        }
    }

    private void HandleIncomingMessage(string message)
    {
        SafeUi(() => AppendChatLine(message, Color.FromArgb(54, 71, 87)));
    }

    private void HandleStatusMessage(string message)
    {
        SafeUi(() =>
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                AppendChatLine($"[Status] {message}", Color.FromArgb(226, 123, 88));
            }
        });
    }

    private void AppendChatLine(string message, Color color)
    {
        rtbChat.SelectionStart = rtbChat.TextLength;
        rtbChat.SelectionLength = 0;
        rtbChat.SelectionColor = color;
        rtbChat.AppendText($"[{DateTime.Now:HH:mm}] {message}{Environment.NewLine}{Environment.NewLine}");
        rtbChat.SelectionColor = rtbChat.ForeColor;
        rtbChat.SelectionStart = rtbChat.TextLength;
        rtbChat.ScrollToCaret();
    }

    private void ToggleComposer(bool enabled)
    {
        btnSend.Enabled = enabled;
        btnSendFile.Enabled = enabled;
        btnCreateGroup.Enabled = enabled;
        txtMessage.Enabled = enabled;
        UseWaitCursor = !enabled;
    }

    private void SafeUi(Action action)
    {
        if (IsDisposed)
        {
            return;
        }

        if (InvokeRequired)
        {
            BeginInvoke(action);
            return;
        }

        action();
    }

    private void ChatForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        try
        {
            AppSession.Client.MessageReceived -= HandleIncomingMessage;
            AppSession.Client.StatusReceived -= HandleStatusMessage;
            AppSession.Client.Dispose();
        }
        catch
        {
        }
    }
}
