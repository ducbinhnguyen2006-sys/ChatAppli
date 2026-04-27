namespace ChatClient;

public partial class RegisterForm : Form
{
    public RegisterForm()
    {
        InitializeComponent();
    }

    private async void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please complete all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ProtocolHelper.ContainsReservedDelimiter(username) || ProtocolHelper.ContainsReservedDelimiter(password))
            {
                MessageBox.Show("The '|' character is not allowed.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ToggleBusy(true);

            string response = await AppSession.Client.SendCommandAsync($"REGISTER|{username}|{password}");
            if (!ProtocolHelper.IsSuccess(response))
            {
                MessageBox.Show(ProtocolHelper.GetPayload(response), "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(ProtocolHelper.GetPayload(response), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
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

    private void ToggleBusy(bool isBusy)
    {
        btnRegister.Enabled = !isBusy;
        UseWaitCursor = isBusy;
    }
}
