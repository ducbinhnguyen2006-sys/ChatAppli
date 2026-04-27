namespace ChatClient;

partial class ChatForm
{
    private System.ComponentModel.IContainer components = null;
    private TableLayoutPanel layoutRoot;
    private Panel panelHeader;
    private Label lblTitle;
    private Label lblSubtitle;
    private Panel panelChat;
    private RichTextBox rtbChat;
    private Panel panelComposer;
    private TextBox txtMessage;
    private Button btnSend;
    private Button btnSendFile;
    private Button btnCreateGroup;

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
        panelHeader = new Panel();
        btnCreateGroup = new Button();
        lblSubtitle = new Label();
        lblTitle = new Label();
        panelChat = new Panel();
        rtbChat = new RichTextBox();
        panelComposer = new Panel();
        btnSendFile = new Button();
        btnSend = new Button();
        txtMessage = new TextBox();
        layoutRoot.SuspendLayout();
        panelHeader.SuspendLayout();
        panelChat.SuspendLayout();
        panelComposer.SuspendLayout();
        SuspendLayout();
        // 
        // layoutRoot
        // 
        layoutRoot.BackColor = Color.FromArgb(245, 247, 250);
        layoutRoot.ColumnCount = 1;
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutRoot.Controls.Add(panelHeader, 0, 0);
        layoutRoot.Controls.Add(panelChat, 0, 1);
        layoutRoot.Controls.Add(panelComposer, 0, 2);
        layoutRoot.Dock = DockStyle.Fill;
        layoutRoot.Location = new Point(0, 0);
        layoutRoot.Margin = new Padding(3, 4, 3, 4);
        layoutRoot.Name = "layoutRoot";
        layoutRoot.Padding = new Padding(23, 27, 23, 27);
        layoutRoot.RowCount = 3;
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 117F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 147F));
        layoutRoot.Size = new Size(1095, 764);
        layoutRoot.TabIndex = 0;
        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.White;
        panelHeader.Controls.Add(btnCreateGroup);
        panelHeader.Controls.Add(lblSubtitle);
        panelHeader.Controls.Add(lblTitle);
        panelHeader.Dock = DockStyle.Fill;
        panelHeader.Location = new Point(26, 31);
        panelHeader.Margin = new Padding(3, 4, 3, 4);
        panelHeader.Name = "panelHeader";
        panelHeader.Padding = new Padding(27, 24, 27, 24);
        panelHeader.Size = new Size(1043, 109);
        panelHeader.TabIndex = 0;
        // 
        // btnCreateGroup
        // 
        btnCreateGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCreateGroup.BackColor = Color.FromArgb(74, 144, 226);
        btnCreateGroup.FlatAppearance.BorderSize = 0;
        btnCreateGroup.FlatStyle = FlatStyle.Flat;
        btnCreateGroup.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        btnCreateGroup.ForeColor = Color.White;
        btnCreateGroup.Location = new Point(857, 27);
        btnCreateGroup.Margin = new Padding(3, 4, 3, 4);
        btnCreateGroup.Name = "btnCreateGroup";
        btnCreateGroup.Size = new Size(158, 51);
        btnCreateGroup.TabIndex = 2;
        btnCreateGroup.Text = "Create Group";
        btnCreateGroup.UseVisualStyleBackColor = false;
        btnCreateGroup.Click += btnCreateGroup_Click;
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 10F);
        lblSubtitle.ForeColor = Color.FromArgb(125, 133, 145);
        lblSubtitle.Location = new Point(32, 57);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(105, 23);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Connected...";
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(35, 47, 62);
        lblTitle.Location = new Point(27, 16);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(81, 41);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Chat";
        // 
        // panelChat
        // 
        panelChat.BackColor = Color.White;
        panelChat.Controls.Add(rtbChat);
        panelChat.Dock = DockStyle.Fill;
        panelChat.Location = new Point(26, 148);
        panelChat.Margin = new Padding(3, 4, 3, 4);
        panelChat.Name = "panelChat";
        panelChat.Padding = new Padding(21, 24, 21, 24);
        panelChat.Size = new Size(1043, 438);
        panelChat.TabIndex = 1;
        // 
        // rtbChat
        // 
        rtbChat.BackColor = Color.White;
        rtbChat.BorderStyle = BorderStyle.None;
        rtbChat.Dock = DockStyle.Fill;
        rtbChat.Font = new Font("Segoe UI", 10.5F);
        rtbChat.ForeColor = Color.FromArgb(54, 71, 87);
        rtbChat.Location = new Point(21, 24);
        rtbChat.Margin = new Padding(3, 4, 3, 4);
        rtbChat.Name = "rtbChat";
        rtbChat.ReadOnly = true;
        rtbChat.ScrollBars = RichTextBoxScrollBars.Vertical;
        rtbChat.Size = new Size(1001, 390);
        rtbChat.TabIndex = 0;
        rtbChat.Text = "";
        // 
        // panelComposer
        // 
        panelComposer.BackColor = Color.White;
        panelComposer.Controls.Add(btnSendFile);
        panelComposer.Controls.Add(btnSend);
        panelComposer.Controls.Add(txtMessage);
        panelComposer.Dock = DockStyle.Fill;
        panelComposer.Location = new Point(26, 594);
        panelComposer.Margin = new Padding(3, 4, 3, 4);
        panelComposer.Name = "panelComposer";
        panelComposer.Padding = new Padding(21, 24, 21, 24);
        panelComposer.Size = new Size(1043, 139);
        panelComposer.TabIndex = 2;
        // 
        // btnSendFile
        // 
        btnSendFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSendFile.BackColor = Color.White;
        btnSendFile.FlatAppearance.BorderColor = Color.FromArgb(74, 144, 226);
        btnSendFile.FlatStyle = FlatStyle.Flat;
        btnSendFile.Font = new Font("Segoe UI", 10F);
        btnSendFile.ForeColor = Color.FromArgb(74, 144, 226);
        btnSendFile.Location = new Point(744, 73);
        btnSendFile.Margin = new Padding(3, 4, 3, 4);
        btnSendFile.Name = "btnSendFile";
        btnSendFile.Size = new Size(127, 45);
        btnSendFile.TabIndex = 1;
        btnSendFile.Text = "Send File";
        btnSendFile.UseVisualStyleBackColor = false;
        btnSendFile.Click += btnSendFile_Click;
        // 
        // btnSend
        // 
        btnSend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSend.BackColor = Color.FromArgb(74, 144, 226);
        btnSend.FlatAppearance.BorderSize = 0;
        btnSend.FlatStyle = FlatStyle.Flat;
        btnSend.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        btnSend.ForeColor = Color.White;
        btnSend.Location = new Point(886, 73);
        btnSend.Margin = new Padding(3, 4, 3, 4);
        btnSend.Name = "btnSend";
        btnSend.Size = new Size(128, 45);
        btnSend.TabIndex = 2;
        btnSend.Text = "Send";
        btnSend.UseVisualStyleBackColor = false;
        btnSend.Click += btnSend_Click;
        // 
        // txtMessage
        // 
        txtMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtMessage.BackColor = Color.FromArgb(245, 247, 250);
        txtMessage.BorderStyle = BorderStyle.FixedSingle;
        txtMessage.Font = new Font("Segoe UI", 11F);
        txtMessage.Location = new Point(24, 24);
        txtMessage.Margin = new Padding(3, 4, 3, 4);
        txtMessage.Multiline = true;
        txtMessage.Name = "txtMessage";
        txtMessage.PlaceholderText = "Type a message and press Enter...";
        txtMessage.Size = new Size(990, 39);
        txtMessage.TabIndex = 0;
        txtMessage.KeyDown += txtMessage_KeyDown;
        // 
        // ChatForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(1095, 764);
        Controls.Add(layoutRoot);
        Font = new Font("Segoe UI", 9F);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(980, 811);
        Name = "ChatForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Chat Client";
        FormClosed += ChatForm_FormClosed;
        Load += ChatForm_Load;
        layoutRoot.ResumeLayout(false);
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        panelChat.ResumeLayout(false);
        panelComposer.ResumeLayout(false);
        panelComposer.PerformLayout();
        ResumeLayout(false);
    }
}
