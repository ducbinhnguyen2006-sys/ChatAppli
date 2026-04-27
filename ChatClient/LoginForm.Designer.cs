namespace ChatClient;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;
    private TableLayoutPanel layoutRoot;
    private Panel panelCard;
    private Label lblTitle;
    private Label lblSubtitle;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Button btnOpenRegister;

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
        btnOpenRegister = new Button();
        btnLogin = new Button();
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
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        layoutRoot.Controls.Add(panelCard, 1, 1);
        layoutRoot.Dock = DockStyle.Fill;
        layoutRoot.Location = new Point(0, 0);
        layoutRoot.Margin = new Padding(3, 4, 3, 4);
        layoutRoot.Name = "layoutRoot";
        layoutRoot.RowCount = 3;
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        layoutRoot.Size = new Size(1029, 800);
        layoutRoot.TabIndex = 0;
        // 
        // panelCard
        // 
        panelCard.Anchor = AnchorStyles.None;
        panelCard.BackColor = Color.White;
        panelCard.Controls.Add(btnOpenRegister);
        panelCard.Controls.Add(btnLogin);
        panelCard.Controls.Add(txtPassword);
        panelCard.Controls.Add(txtUsername);
        panelCard.Controls.Add(lblSubtitle);
        panelCard.Controls.Add(lblTitle);
        panelCard.Location = new Point(335, 192);
        panelCard.Margin = new Padding(27, 32, 27, 32);
        panelCard.Name = "panelCard";
        panelCard.Padding = new Padding(41, 48, 41, 48);
        panelCard.Size = new Size(357, 416);
        panelCard.TabIndex = 0;
        // 
        // btnOpenRegister
        // 
        btnOpenRegister.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnOpenRegister.BackColor = Color.White;
        btnOpenRegister.FlatAppearance.BorderColor = Color.FromArgb(74, 144, 226);
        btnOpenRegister.FlatStyle = FlatStyle.Flat;
        btnOpenRegister.Font = new Font("Segoe UI", 10F);
        btnOpenRegister.ForeColor = Color.FromArgb(74, 144, 226);
        btnOpenRegister.Location = new Point(39, 342);
        btnOpenRegister.Margin = new Padding(3, 4, 3, 4);
        btnOpenRegister.Name = "btnOpenRegister";
        btnOpenRegister.Size = new Size(275, 51);
        btnOpenRegister.TabIndex = 5;
        btnOpenRegister.Text = "Create Account";
        btnOpenRegister.UseVisualStyleBackColor = false;
        btnOpenRegister.Click += btnOpenRegister_Click;
        // 
        // btnLogin
        // 
        btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnLogin.BackColor = Color.FromArgb(74, 144, 226);
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(38, 268);
        btnLogin.Margin = new Padding(3, 4, 3, 4);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(275, 53);
        btnLogin.TabIndex = 4;
        btnLogin.Text = "Login";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click;
        // 
        // txtPassword
        // 
        txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtPassword.BackColor = Color.FromArgb(245, 247, 250);
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Font = new Font("Segoe UI", 11F);
        txtPassword.Location = new Point(39, 208);
        txtPassword.Margin = new Padding(3, 4, 3, 4);
        txtPassword.Name = "txtPassword";
        txtPassword.PlaceholderText = "Password";
        txtPassword.Size = new Size(274, 32);
        txtPassword.TabIndex = 3;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // txtUsername
        // 
        txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtUsername.BackColor = Color.FromArgb(245, 247, 250);
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Font = new Font("Segoe UI", 11F);
        txtUsername.Location = new Point(41, 151);
        txtUsername.Margin = new Padding(3, 4, 3, 4);
        txtUsername.Name = "txtUsername";
        txtUsername.PlaceholderText = "Username";
        txtUsername.Size = new Size(274, 32);
        txtUsername.TabIndex = 2;
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 10F);
        lblSubtitle.ForeColor = Color.FromArgb(125, 133, 145);
        lblSubtitle.Location = new Point(41, 107);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(222, 23);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Connect to your chat server";
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(35, 47, 62);
        lblTitle.Location = new Point(39, 45);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(106, 46);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Login";
        // 
        // LoginForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(1029, 800);
        Controls.Add(layoutRoot);
        Font = new Font("Segoe UI", 9F);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(797, 651);
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Chat Client - Login";
        layoutRoot.ResumeLayout(false);
        panelCard.ResumeLayout(false);
        panelCard.PerformLayout();
        ResumeLayout(false);
    }
}
