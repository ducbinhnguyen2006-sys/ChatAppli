namespace ChatClient;

public partial class GroupForm : Form
{
    public GroupForm()
    {
        InitializeComponent();
    }

    private async void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            string groupName = txtGroupName.Text.Trim();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                MessageBox.Show("Please enter a group name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ProtocolHelper.ContainsReservedDelimiter(groupName))
            {
                MessageBox.Show("The '|' character is not allowed.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnCreate.Enabled = false;
            string response = await AppSession.Client.SendCommandAsync($"CREATE_GROUP|{groupName}");

            if (!ProtocolHelper.IsSuccess(response))
            {
                MessageBox.Show(ProtocolHelper.GetPayload(response), "Create Group Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            btnCreate.Enabled = true;
        }
    }
}
