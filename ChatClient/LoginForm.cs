namespace ChatClient;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter username and password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ProtocolHelper.ContainsReservedDelimiter(username) || ProtocolHelper.ContainsReservedDelimiter(password))
            {
                MessageBox.Show("The '|' character is not allowed.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ToggleBusy(true);

            string response = await AppSession.Client.SendCommandAsync($"LOGIN|{username}|{password}");
            if (!ProtocolHelper.IsSuccess(response))
            {
                MessageBox.Show(ProtocolHelper.GetPayload(response), "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AppSession.Username = username;

            var chatForm = new ChatForm(username);
            chatForm.FormClosed += (_, _) => Close();
            chatForm.Show();
            Hide();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            ToggleBusy(false);
        }
    }

    private void btnOpenRegister_Click(object sender, EventArgs e)
    {
        try
        {
            using var registerForm = new RegisterForm();
            registerForm.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ToggleBusy(bool isBusy)
    {
        btnLogin.Enabled = !isBusy;
        btnOpenRegister.Enabled = !isBusy;
        UseWaitCursor = isBusy;
    }
}
