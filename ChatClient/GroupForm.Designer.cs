namespace ChatClient;

partial class GroupForm
{
    private System.ComponentModel.IContainer components = null;
    private TableLayoutPanel layoutRoot;
    private Panel panelCard;
    private Label lblTitle;
    private Label lblSubtitle;
    private TextBox txtGroupName;
    private Button btnCreate;

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
        components = new System.ComponentModel.Container();
        layoutRoot = new TableLayoutPanel();
        panelCard = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        txtGroupName = new TextBox();
        btnCreate = new Button();
        layoutRoot.SuspendLayout();
        panelCard.SuspendLayout();
        SuspendLayout();
        layoutRoot.BackColor = Color.FromArgb(245, 247, 250);
        layoutRoot.ColumnCount = 3;
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        layoutRoot.Controls.Add(panelCard, 1, 1);
        layoutRoot.Dock = DockStyle.Fill;
        layoutRoot.Location = new Point(0, 0);
        layoutRoot.Name = "layoutRoot";
        layoutRoot.RowCount = 3;
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        layoutRoot.Size = new Size(640, 360);
        layoutRoot.TabIndex = 0;
        panelCard.Anchor = AnchorStyles.None;
        panelCard.BackColor = Color.White;
        panelCard.Controls.Add(btnCreate);
        panelCard.Controls.Add(txtGroupName);
        panelCard.Controls.Add(lblSubtitle);
        panelCard.Controls.Add(lblTitle);
        panelCard.Location = new Point(160, 90);
        panelCard.Name = "panelCard";
        panelCard.Padding = new Padding(28);
        panelCard.Size = new Size(320, 180);
        panelCard.TabIndex = 0;
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(35, 47, 62);
        lblTitle.Location = new Point(28, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(132, 30);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "New Group";
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 9.5F);
        lblSubtitle.ForeColor = Color.FromArgb(125, 133, 145);
        lblSubtitle.Location = new Point(30, 52);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(168, 17);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Create a basic group record";
        txtGroupName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtGroupName.BackColor = Color.FromArgb(245, 247, 250);
        txtGroupName.BorderStyle = BorderStyle.FixedSingle;
        txtGroupName.Font = new Font("Segoe UI", 10.5F);
        txtGroupName.Location = new Point(28, 84);
        txtGroupName.Name = "txtGroupName";
        txtGroupName.PlaceholderText = "Group name";
        txtGroupName.Size = new Size(264, 26);
        txtGroupName.TabIndex = 2;
        btnCreate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnCreate.BackColor = Color.FromArgb(74, 144, 226);
        btnCreate.FlatAppearance.BorderSize = 0;
        btnCreate.FlatStyle = FlatStyle.Flat;
        btnCreate.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        btnCreate.ForeColor = Color.White;
        btnCreate.Location = new Point(28, 126);
        btnCreate.Name = "btnCreate";
        btnCreate.Size = new Size(264, 34);
        btnCreate.TabIndex = 3;
        btnCreate.Text = "Create Group";
        btnCreate.UseVisualStyleBackColor = false;
        btnCreate.Click += btnCreate_Click;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(640, 360);
        Controls.Add(layoutRoot);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "GroupForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Create Group";
        layoutRoot.ResumeLayout(false);
        panelCard.ResumeLayout(false);
        panelCard.PerformLayout();
        ResumeLayout(false);
    }
}
