namespace ChatClient;

partial class RegisterForm
{
    private System.ComponentModel.IContainer components = null;
    private TableLayoutPanel layoutRoot;
    private Panel panelCard;
    private Label lblTitle;
    private Label lblSubtitle;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private TextBox txtConfirmPassword;
    private Button btnRegister;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        layoutRoot = new TableLayoutPanel();
        panelCard = new Panel();
        btnRegister = new Button();
        txtConfirmPassword = new TextBox();
        txtPassword = new TextBox();
        txtUsername = new TextBox();
        lblSubtitle = new Label();
        lblTitle = new Label();
        layoutRoot.SuspendLayout();
        panelCard.SuspendLayout();
        SuspendLayout();
        // 
        // layoutRoot
        // 
        layoutRoot.BackColor = Color.FromArgb(245, 247, 250);
        layoutRoot.ColumnCount = 3;
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        layoutRoot.Controls.Add(panelCard, 1, 1);
        layoutRoot.Dock = DockStyle.Fill;
        layoutRoot.Location = new Point(0, 0);
        layoutRoot.Margin = new Padding(3, 4, 3, 4);
        layoutRoot.Name = "layoutRoot";
        layoutRoot.RowCount = 3;
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 18F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 64F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 18F));
        layoutRoot.Size = new Size(1051, 827);
        layoutRoot.TabIndex = 0;
        // 
        // panelCard
        // 
        panelCard.Anchor = AnchorStyles.None;
        panelCard.BackColor = Color.White;
        panelCard.Controls.Add(btnRegister);
        panelCard.Controls.Add(txtConfirmPassword);
        panelCard.Controls.Add(txtPassword);
        panelCard.Controls.Add(txtUsername);
        panelCard.Controls.Add(lblSubtitle);
        panelCard.Controls.Add(lblTitle);
        panelCard.Location = new Point(321, 180);
        panelCard.Margin = new Padding(27, 32, 27, 32);
        panelCard.Name = "panelCard";
        panelCard.Padding = new Padding(41, 48, 41, 48);
        panelCard.Size = new Size(408, 465);
        panelCard.TabIndex = 0;
        // 
        // btnRegister
        // 
        btnRegister.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnRegister.BackColor = Color.FromArgb(74, 144, 226);
        btnRegister.FlatAppearance.BorderSize = 0;
        btnRegister.FlatStyle = FlatStyle.Flat;
        btnRegister.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        btnRegister.ForeColor = Color.White;
        btnRegister.Location = new Point(39, 373);
        btnRegister.Margin = new Padding(3, 4, 3, 4);
        btnRegister.Name = "btnRegister";
        btnRegister.Size = new Size(325, 56);
        btnRegister.TabIndex = 5;
        btnRegister.Text = "Register";
        btnRegister.UseVisualStyleBackColor = false;
        btnRegister.Click += btnRegister_Click;
        // 
        // txtConfirmPassword
        // 
        txtConfirmPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtConfirmPassword.BackColor = Color.FromArgb(245, 247, 250);
        txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
        txtConfirmPassword.Font = new Font("Segoe UI", 11F);
        txtConfirmPassword.Location = new Point(41, 301);
        txtConfirmPassword.Margin = new Padding(3, 4, 3, 4);
        txtConfirmPassword.Name = "txtConfirmPassword";
        txtConfirmPassword.PlaceholderText = "Confirm Password";
        txtConfirmPassword.Size = new Size(325, 32);
        txtConfirmPassword.TabIndex = 4;
        txtConfirmPassword.UseSystemPasswordChar = true;
        // 
        // txtPassword
        // 
        txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtPassword.BackColor = Color.FromArgb(245, 247, 250);
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Font = new Font("Segoe UI", 11F);
        txtPassword.Location = new Point(41, 237);
        txtPassword.Margin = new Padding(3, 4, 3, 4);
        txtPassword.Name = "txtPassword";
        txtPassword.PlaceholderText = "Password";
        txtPassword.Size = new Size(325, 32);
        txtPassword.TabIndex = 3;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // txtUsername
        // 
        txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtUsername.BackColor = Color.FromArgb(245, 247, 250);
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Font = new Font("Segoe UI", 11F);
        txtUsername.Location = new Point(41, 173);
        txtUsername.Margin = new Padding(3, 4, 3, 4);
        txtUsername.Name = "txtUsername";
        txtUsername.PlaceholderText = "Username";
        txtUsername.Size = new Size(325, 32);
        txtUsername.TabIndex = 2;
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 10F);
        lblSubtitle.ForeColor = Color.FromArgb(125, 133, 145);
        lblSubtitle.Location = new Point(41, 107);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(259, 23);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Create a new chat application ID";
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(35, 47, 62);
        lblTitle.Location = new Point(41, 45);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(145, 46);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Register";
        // 
        // RegisterForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(1051, 827);
        Controls.Add(layoutRoot);
        Font = new Font("Segoe UI", 9F);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(866, 704);
        Name = "RegisterForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Chat Client - Register";
        layoutRoot.ResumeLayout(false);
        panelCard.ResumeLayout(false);
        panelCard.PerformLayout();
        ResumeLayout(false);
    }
}
