<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Timer1 = New Timer(components)
        txID = New DataGridViewTextBoxColumn()
        Wallet = New DataGridViewTextBoxColumn()
        Value = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        Timer2 = New Timer(components)
        miniToolStrip = New StatusStrip()
        StatusStrip1 = New StatusStrip()
        ToolStripStatusLabel4 = New ToolStripStatusLabel()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        ToolStripStatusLabel2 = New ToolStripStatusLabel()
        ToolTip1 = New ToolTip(components)
        SaveFileDialog1 = New SaveFileDialog()
        Timer4 = New Timer(components)
        Timer5 = New Timer(components)
        TabPage6 = New TabPage()
        RichTextBox3 = New RichTextBox()
        TabPage10 = New TabPage()
        Button19 = New Button()
        GroupBox2 = New GroupBox()
        Button18 = New Button()
        Button20 = New Button()
        DataGridView3 = New DataGridView()
        DataGridViewTextBoxColumn2 = New DataGridViewTextBoxColumn()
        dgv3_Description = New DataGridViewTextBoxColumn()
        Label40 = New Label()
        Label35 = New Label()
        CheckBox4 = New CheckBox()
        ComboBox9 = New ComboBox()
        TabPage9 = New TabPage()
        GroupBox3 = New GroupBox()
        Label46 = New Label()
        Label45 = New Label()
        Label44 = New Label()
        Label41 = New Label()
        ProgressBar1 = New ProgressBar()
        Button17 = New Button()
        Label39 = New Label()
        GroupBox1 = New GroupBox()
        Button16 = New Button()
        Label37 = New Label()
        Button12 = New Button()
        Button15 = New Button()
        Label38 = New Label()
        Label36 = New Label()
        TabPage3 = New TabPage()
        Panel1 = New Panel()
        CheckBox5 = New CheckBox()
        ComboBox10 = New ComboBox()
        Label32 = New Label()
        Button6 = New Button()
        Label20 = New Label()
        TextBox3 = New TextBox()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        Label18 = New Label()
        ComboBox6 = New ComboBox()
        Label16 = New Label()
        Label15 = New Label()
        ComboBox5 = New ComboBox()
        Label13 = New Label()
        ComboBox1 = New ComboBox()
        ComboBox4 = New ComboBox()
        Label7 = New Label()
        Label12 = New Label()
        ComboBox2 = New ComboBox()
        CheckBox3 = New CheckBox()
        Label6 = New Label()
        Button5 = New Button()
        Label8 = New Label()
        Button4 = New Button()
        CheckBox2 = New CheckBox()
        Label9 = New Label()
        CheckBox1 = New CheckBox()
        ComboBox3 = New ComboBox()
        Label10 = New Label()
        Label11 = New Label()
        Label5 = New Label()
        Button3 = New Button()
        TabPage2 = New TabPage()
        Button21 = New Button()
        Label42 = New Label()
        ComboBox11 = New ComboBox()
        Button14 = New Button()
        Button13 = New Button()
        Button2 = New Button()
        Button1 = New Button()
        Label19 = New Label()
        DataGridView1 = New DataGridView()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column6 = New DataGridViewTextBoxColumn()
        Column5 = New DataGridViewTextBoxColumn()
        Column7 = New DataGridViewTextBoxColumn()
        TabPage1 = New TabPage()
        PictureBox1 = New PictureBox()
        Label34 = New Label()
        Label29 = New Label()
        LinkLabel1 = New LinkLabel()
        Label17 = New Label()
        Label4 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        TabControl1 = New TabControl()
        StatusStrip1.SuspendLayout()
        TabPage6.SuspendLayout()
        TabPage10.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(DataGridView3, ComponentModel.ISupportInitialize).BeginInit()
        TabPage9.SuspendLayout()
        GroupBox3.SuspendLayout()
        GroupBox1.SuspendLayout()
        TabPage3.SuspendLayout()
        Panel1.SuspendLayout()
        TabPage2.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        TabPage1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        TabControl1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 60000
        ' 
        ' txID
        ' 
        txID.HeaderText = "txID"
        txID.MinimumWidth = 6
        txID.Name = "txID"
        txID.ReadOnly = True
        txID.Width = 125
        ' 
        ' Wallet
        ' 
        Wallet.HeaderText = "Wallet"
        Wallet.MinimumWidth = 6
        Wallet.Name = "Wallet"
        Wallet.ReadOnly = True
        Wallet.Width = 125
        ' 
        ' Value
        ' 
        Value.HeaderText = "Value"
        Value.MinimumWidth = 6
        Value.Name = "Value"
        Value.ReadOnly = True
        Value.Width = 125
        ' 
        ' DataGridViewTextBoxColumn1
        ' 
        DataGridViewTextBoxColumn1.HeaderText = "txID"
        DataGridViewTextBoxColumn1.MinimumWidth = 6
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        DataGridViewTextBoxColumn1.Width = 125
        ' 
        ' Timer2
        ' 
        Timer2.Interval = 1000
        ' 
        ' miniToolStrip
        ' 
        miniToolStrip.AccessibleName = "Neue Elementauswahl"
        miniToolStrip.AccessibleRole = AccessibleRole.ButtonDropDown
        miniToolStrip.AutoSize = False
        miniToolStrip.Dock = DockStyle.None
        miniToolStrip.ImageScalingSize = New Size(20, 20)
        miniToolStrip.Location = New Point(437, 1)
        miniToolStrip.Name = "miniToolStrip"
        miniToolStrip.Size = New Size(858, 22)
        miniToolStrip.TabIndex = 13
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripStatusLabel4, ToolStripStatusLabel1, ToolStripStatusLabel2})
        StatusStrip1.Location = New Point(0, 703)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 16, 0)
        StatusStrip1.Size = New Size(997, 26)
        StatusStrip1.SizingGrip = False
        StatusStrip1.TabIndex = 12
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripStatusLabel4
        ' 
        ToolStripStatusLabel4.BackColor = SystemColors.Control
        ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        ToolStripStatusLabel4.Size = New Size(153, 20)
        ToolStripStatusLabel4.Text = "ToolStripStatusLabel4"
        ' 
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.BackColor = SystemColors.Control
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(210, 20)
        ToolStripStatusLabel1.Text = "RaptorWING Donation Adress:"
        ' 
        ' ToolStripStatusLabel2
        ' 
        ToolStripStatusLabel2.BackColor = SystemColors.Control
        ToolStripStatusLabel2.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripStatusLabel2.ForeColor = Color.Red
        ToolStripStatusLabel2.IsLink = True
        ToolStripStatusLabel2.LinkColor = Color.Red
        ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        ToolStripStatusLabel2.Size = New Size(153, 20)
        ToolStripStatusLabel2.Text = "ToolStripStatusLabel2"
        ' 
        ' Timer4
        ' 
        Timer4.Interval = 12000000
        ' 
        ' Timer5
        ' 
        Timer5.Interval = 120000
        ' 
        ' TabPage6
        ' 
        TabPage6.Controls.Add(RichTextBox3)
        TabPage6.Location = New Point(4, 30)
        TabPage6.Margin = New Padding(3, 4, 3, 4)
        TabPage6.Name = "TabPage6"
        TabPage6.Padding = New Padding(3, 4, 3, 4)
        TabPage6.Size = New Size(989, 695)
        TabPage6.TabIndex = 3
        TabPage6.Text = "TabPage6"
        TabPage6.UseVisualStyleBackColor = True
        ' 
        ' RichTextBox3
        ' 
        RichTextBox3.BackColor = SystemColors.Window
        RichTextBox3.BorderStyle = BorderStyle.None
        RichTextBox3.Dock = DockStyle.Fill
        RichTextBox3.Location = New Point(3, 4)
        RichTextBox3.Margin = New Padding(3, 4, 3, 4)
        RichTextBox3.Name = "RichTextBox3"
        RichTextBox3.ReadOnly = True
        RichTextBox3.ScrollBars = RichTextBoxScrollBars.ForcedVertical
        RichTextBox3.Size = New Size(983, 687)
        RichTextBox3.TabIndex = 0
        RichTextBox3.Text = ""
        ' 
        ' TabPage10
        ' 
        TabPage10.Controls.Add(Button19)
        TabPage10.Controls.Add(GroupBox2)
        TabPage10.Controls.Add(Label40)
        TabPage10.Controls.Add(Label35)
        TabPage10.Controls.Add(CheckBox4)
        TabPage10.Controls.Add(ComboBox9)
        TabPage10.Location = New Point(4, 30)
        TabPage10.Margin = New Padding(3, 4, 3, 4)
        TabPage10.Name = "TabPage10"
        TabPage10.Size = New Size(989, 695)
        TabPage10.TabIndex = 5
        TabPage10.Text = "Settings"
        TabPage10.UseVisualStyleBackColor = True
        ' 
        ' Button19
        ' 
        Button19.BackgroundImage = My.Resources.Resources.Save_icon
        Button19.BackgroundImageLayout = ImageLayout.Stretch
        Button19.Cursor = Cursors.Hand
        Button19.FlatStyle = FlatStyle.Flat
        Button19.ForeColor = SystemColors.Window
        Button19.Location = New Point(29, 19)
        Button19.Margin = New Padding(3, 4, 3, 4)
        Button19.Name = "Button19"
        Button19.Size = New Size(51, 56)
        Button19.TabIndex = 48
        Button19.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(Button18)
        GroupBox2.Controls.Add(Button20)
        GroupBox2.Controls.Add(DataGridView3)
        GroupBox2.Location = New Point(29, 183)
        GroupBox2.Margin = New Padding(3, 4, 3, 4)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(3, 4, 3, 4)
        GroupBox2.Size = New Size(369, 469)
        GroupBox2.TabIndex = 20
        GroupBox2.TabStop = False
        GroupBox2.Text = "Walletprofile"
        ' 
        ' Button18
        ' 
        Button18.BackgroundImage = My.Resources.Resources.Delete_file_icon
        Button18.BackgroundImageLayout = ImageLayout.Stretch
        Button18.Cursor = Cursors.Hand
        Button18.FlatStyle = FlatStyle.Flat
        Button18.ForeColor = SystemColors.Window
        Button18.Location = New Point(70, 32)
        Button18.Margin = New Padding(3, 4, 3, 4)
        Button18.Name = "Button18"
        Button18.Size = New Size(48, 56)
        Button18.TabIndex = 51
        Button18.UseVisualStyleBackColor = True
        ' 
        ' Button20
        ' 
        Button20.BackgroundImage = My.Resources.Resources.new_icon
        Button20.BackgroundImageLayout = ImageLayout.Stretch
        Button20.Cursor = Cursors.Hand
        Button20.FlatStyle = FlatStyle.Flat
        Button20.ForeColor = SystemColors.Window
        Button20.Location = New Point(7, 32)
        Button20.Margin = New Padding(3, 4, 3, 4)
        Button20.Name = "Button20"
        Button20.Size = New Size(53, 56)
        Button20.TabIndex = 49
        Button20.UseVisualStyleBackColor = True
        ' 
        ' DataGridView3
        ' 
        DataGridView3.AllowUserToAddRows = False
        DataGridView3.AllowUserToDeleteRows = False
        DataGridView3.AllowUserToResizeColumns = False
        DataGridView3.AllowUserToResizeRows = False
        DataGridView3.BackgroundColor = SystemColors.Window
        DataGridView3.BorderStyle = BorderStyle.Fixed3D
        DataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView3.Columns.AddRange(New DataGridViewColumn() {DataGridViewTextBoxColumn2, dgv3_Description})
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Window
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle1.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = Color.RosyBrown
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        DataGridView3.DefaultCellStyle = DataGridViewCellStyle1
        DataGridView3.Location = New Point(5, 103)
        DataGridView3.Margin = New Padding(3, 4, 3, 4)
        DataGridView3.MultiSelect = False
        DataGridView3.Name = "DataGridView3"
        DataGridView3.RowHeadersVisible = False
        DataGridView3.RowHeadersWidth = 51
        DataGridView3.RowTemplate.Height = 25
        DataGridView3.Size = New Size(358, 359)
        DataGridView3.TabIndex = 21
        ' 
        ' DataGridViewTextBoxColumn2
        ' 
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewTextBoxColumn2.HeaderText = "Nr."
        DataGridViewTextBoxColumn2.MinimumWidth = 6
        DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable
        DataGridViewTextBoxColumn2.Width = 39
        ' 
        ' dgv3_Description
        ' 
        dgv3_Description.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgv3_Description.HeaderText = "Description"
        dgv3_Description.MinimumWidth = 6
        dgv3_Description.Name = "dgv3_Description"
        ' 
        ' Label40
        ' 
        Label40.AutoSize = True
        Label40.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label40.Location = New Point(213, 103)
        Label40.Name = "Label40"
        Label40.Size = New Size(154, 23)
        Label40.TabIndex = 19
        Label40.Text = "Change language:"
        ' 
        ' Label35
        ' 
        Label35.AutoSize = True
        Label35.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label35.Location = New Point(29, 103)
        Label35.Name = "Label35"
        Label35.Size = New Size(133, 23)
        Label35.TabIndex = 18
        Label35.Text = "Change design:"
        ' 
        ' CheckBox4
        ' 
        CheckBox4.AutoSize = True
        CheckBox4.Location = New Point(33, 129)
        CheckBox4.Margin = New Padding(3, 4, 3, 4)
        CheckBox4.Name = "CheckBox4"
        CheckBox4.Size = New Size(116, 27)
        CheckBox4.TabIndex = 16
        CheckBox4.Text = "Dark Mode"
        CheckBox4.UseVisualStyleBackColor = True
        ' 
        ' ComboBox9
        ' 
        ComboBox9.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox9.FormattingEnabled = True
        ComboBox9.Location = New Point(213, 129)
        ComboBox9.Margin = New Padding(3, 4, 3, 4)
        ComboBox9.Name = "ComboBox9"
        ComboBox9.Size = New Size(161, 29)
        ComboBox9.TabIndex = 15
        ' 
        ' TabPage9
        ' 
        TabPage9.Controls.Add(GroupBox3)
        TabPage9.Controls.Add(GroupBox1)
        TabPage9.Controls.Add(Label36)
        TabPage9.Location = New Point(4, 30)
        TabPage9.Margin = New Padding(3, 4, 3, 4)
        TabPage9.Name = "TabPage9"
        TabPage9.Padding = New Padding(3, 4, 3, 4)
        TabPage9.Size = New Size(989, 695)
        TabPage9.TabIndex = 4
        TabPage9.Text = "RTM Support"
        TabPage9.UseVisualStyleBackColor = True
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(Label46)
        GroupBox3.Controls.Add(Label45)
        GroupBox3.Controls.Add(Label44)
        GroupBox3.Controls.Add(Label41)
        GroupBox3.Controls.Add(ProgressBar1)
        GroupBox3.Controls.Add(Button17)
        GroupBox3.Controls.Add(Label39)
        GroupBox3.Location = New Point(539, 83)
        GroupBox3.Margin = New Padding(3, 4, 3, 4)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Padding = New Padding(3, 4, 3, 4)
        GroupBox3.Size = New Size(430, 176)
        GroupBox3.TabIndex = 34
        GroupBox3.TabStop = False
        GroupBox3.Text = "RTM Core Bootstraps"
        ' 
        ' Label46
        ' 
        Label46.AutoSize = True
        Label46.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point)
        Label46.Location = New Point(142, 144)
        Label46.Name = "Label46"
        Label46.Size = New Size(83, 19)
        Label46.TabIndex = 39
        Label46.Text = "No activities"
        ' 
        ' Label45
        ' 
        Label45.AutoSize = True
        Label45.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point)
        Label45.Location = New Point(142, 109)
        Label45.Name = "Label45"
        Label45.Size = New Size(47, 19)
        Label45.TabIndex = 38
        Label45.Text = "Status"
        ' 
        ' Label44
        ' 
        Label44.AutoSize = True
        Label44.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label44.Location = New Point(7, 140)
        Label44.Name = "Label44"
        Label44.RightToLeft = RightToLeft.No
        Label44.Size = New Size(74, 23)
        Label44.TabIndex = 37
        Label44.Text = "Update:"
        ' 
        ' Label41
        ' 
        Label41.AutoSize = True
        Label41.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label41.Location = New Point(7, 85)
        Label41.Name = "Label41"
        Label41.RightToLeft = RightToLeft.No
        Label41.Size = New Size(97, 23)
        Label41.TabIndex = 33
        Label41.Text = "Download:"
        Label41.TextAlign = ContentAlignment.TopRight
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(142, 77)
        ProgressBar1.Margin = New Padding(3, 4, 3, 4)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(273, 31)
        ProgressBar1.TabIndex = 32
        ' 
        ' Button17
        ' 
        Button17.FlatStyle = FlatStyle.Flat
        Button17.Location = New Point(289, 32)
        Button17.Margin = New Padding(3, 4, 3, 4)
        Button17.Name = "Button17"
        Button17.Size = New Size(126, 37)
        Button17.TabIndex = 31
        Button17.Text = "Update"
        Button17.UseVisualStyleBackColor = True
        ' 
        ' Label39
        ' 
        Label39.AutoSize = True
        Label39.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label39.Location = New Point(8, 40)
        Label39.Name = "Label39"
        Label39.RightToLeft = RightToLeft.No
        Label39.Size = New Size(249, 23)
        Label39.TabIndex = 29
        Label39.Text = "Update RTM Core Bootstraps:"
        Label39.TextAlign = ContentAlignment.TopRight
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Button16)
        GroupBox1.Controls.Add(Label37)
        GroupBox1.Controls.Add(Button12)
        GroupBox1.Controls.Add(Button15)
        GroupBox1.Controls.Add(Label38)
        GroupBox1.Location = New Point(21, 83)
        GroupBox1.Margin = New Padding(3, 4, 3, 4)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(3, 4, 3, 4)
        GroupBox1.Size = New Size(512, 176)
        GroupBox1.TabIndex = 31
        GroupBox1.TabStop = False
        GroupBox1.Text = "RTM Core Wallet"
        ' 
        ' Button16
        ' 
        Button16.FlatStyle = FlatStyle.Flat
        Button16.Location = New Point(321, 84)
        Button16.Margin = New Padding(3, 4, 3, 4)
        Button16.Name = "Button16"
        Button16.Size = New Size(86, 37)
        Button16.TabIndex = 30
        Button16.Text = "Save it"
        Button16.UseVisualStyleBackColor = True
        ' 
        ' Label37
        ' 
        Label37.AutoSize = True
        Label37.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label37.Location = New Point(16, 40)
        Label37.Name = "Label37"
        Label37.RightToLeft = RightToLeft.No
        Label37.Size = New Size(249, 23)
        Label37.TabIndex = 25
        Label37.Text = "Download an start RTM-Core:"
        Label37.TextAlign = ContentAlignment.TopRight
        ' 
        ' Button12
        ' 
        Button12.FlatStyle = FlatStyle.Flat
        Button12.Location = New Point(321, 32)
        Button12.Margin = New Padding(3, 4, 3, 4)
        Button12.Name = "Button12"
        Button12.Size = New Size(86, 37)
        Button12.TabIndex = 26
        Button12.Text = "portable"
        Button12.UseVisualStyleBackColor = True
        ' 
        ' Button15
        ' 
        Button15.FlatStyle = FlatStyle.Flat
        Button15.Location = New Point(419, 32)
        Button15.Margin = New Padding(3, 4, 3, 4)
        Button15.Name = "Button15"
        Button15.Size = New Size(86, 37)
        Button15.TabIndex = 28
        Button15.Text = "install"
        Button15.UseVisualStyleBackColor = True
        ' 
        ' Label38
        ' 
        Label38.AutoSize = True
        Label38.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label38.Location = New Point(16, 92)
        Label38.Name = "Label38"
        Label38.RightToLeft = RightToLeft.No
        Label38.Size = New Size(259, 23)
        Label38.TabIndex = 27
        Label38.Text = "Save RTM Wallet to your Place:"
        Label38.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label36
        ' 
        Label36.AutoSize = True
        Label36.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label36.Location = New Point(21, 27)
        Label36.Name = "Label36"
        Label36.RightToLeft = RightToLeft.No
        Label36.Size = New Size(234, 23)
        Label36.TabIndex = 24
        Label36.Text = "Your Personal RTM Support:"
        Label36.TextAlign = ContentAlignment.TopRight
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(Panel1)
        TabPage3.Controls.Add(Label11)
        TabPage3.Controls.Add(Label5)
        TabPage3.Controls.Add(Button3)
        TabPage3.Location = New Point(4, 30)
        TabPage3.Margin = New Padding(3, 4, 3, 4)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3, 4, 3, 4)
        TabPage3.Size = New Size(989, 695)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Mining"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(CheckBox5)
        Panel1.Controls.Add(ComboBox10)
        Panel1.Controls.Add(Label32)
        Panel1.Controls.Add(Button6)
        Panel1.Controls.Add(Label20)
        Panel1.Controls.Add(TextBox3)
        Panel1.Controls.Add(TextBox1)
        Panel1.Controls.Add(TextBox2)
        Panel1.Controls.Add(Label18)
        Panel1.Controls.Add(ComboBox6)
        Panel1.Controls.Add(Label16)
        Panel1.Controls.Add(Label15)
        Panel1.Controls.Add(ComboBox5)
        Panel1.Controls.Add(Label13)
        Panel1.Controls.Add(ComboBox1)
        Panel1.Controls.Add(ComboBox4)
        Panel1.Controls.Add(Label7)
        Panel1.Controls.Add(Label12)
        Panel1.Controls.Add(ComboBox2)
        Panel1.Controls.Add(CheckBox3)
        Panel1.Controls.Add(Label6)
        Panel1.Controls.Add(Button5)
        Panel1.Controls.Add(Label8)
        Panel1.Controls.Add(Button4)
        Panel1.Controls.Add(CheckBox2)
        Panel1.Controls.Add(Label9)
        Panel1.Controls.Add(CheckBox1)
        Panel1.Controls.Add(ComboBox3)
        Panel1.Controls.Add(Label10)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(3, 4)
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(983, 687)
        Panel1.TabIndex = 48
        Panel1.Visible = False
        ' 
        ' CheckBox5
        ' 
        CheckBox5.Checked = True
        CheckBox5.CheckState = CheckState.Checked
        CheckBox5.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        CheckBox5.Location = New Point(742, 383)
        CheckBox5.Margin = New Padding(3, 4, 3, 4)
        CheckBox5.Name = "CheckBox5"
        CheckBox5.Size = New Size(219, 45)
        CheckBox5.TabIndex = 76
        CheckBox5.Text = "Raptorwings Donation"
        CheckBox5.UseVisualStyleBackColor = True
        ' 
        ' ComboBox10
        ' 
        ComboBox10.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox10.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        ComboBox10.FormattingEnabled = True
        ComboBox10.Items.AddRange(New Object() {"Default", "1", "2", "3", "4", "5"})
        ComboBox10.Location = New Point(17, 469)
        ComboBox10.Margin = New Padding(3, 4, 3, 4)
        ComboBox10.Name = "ComboBox10"
        ComboBox10.Size = New Size(204, 29)
        ComboBox10.TabIndex = 75
        ' 
        ' Label32
        ' 
        Label32.AutoSize = True
        Label32.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label32.Location = New Point(17, 445)
        Label32.Name = "Label32"
        Label32.Size = New Size(202, 23)
        Label32.TabIndex = 74
        Label32.Text = "Number of cores to use:"
        Label32.TextAlign = ContentAlignment.TopRight
        ' 
        ' Button6
        ' 
        Button6.BackgroundImage = My.Resources.Resources.Delete_file_icon
        Button6.BackgroundImageLayout = ImageLayout.Stretch
        Button6.Cursor = Cursors.Hand
        Button6.FlatStyle = FlatStyle.Flat
        Button6.ForeColor = SystemColors.Window
        Button6.Location = New Point(350, 355)
        Button6.Margin = New Padding(3, 4, 3, 4)
        Button6.Name = "Button6"
        Button6.Size = New Size(37, 41)
        Button6.TabIndex = 73
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label20.Location = New Point(297, 292)
        Label20.Name = "Label20"
        Label20.Size = New Size(155, 23)
        Label20.TabIndex = 72
        Label20.Text = "WingSheet Name:"
        Label20.TextAlign = ContentAlignment.TopRight
        ' 
        ' TextBox3
        ' 
        TextBox3.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        TextBox3.Location = New Point(297, 316)
        TextBox3.Margin = New Padding(3, 4, 3, 4)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(348, 29)
        TextBox3.TabIndex = 71
        ' 
        ' TextBox1
        ' 
        TextBox1.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        TextBox1.Location = New Point(17, 235)
        TextBox1.Margin = New Padding(3, 4, 3, 4)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(204, 29)
        TextBox1.TabIndex = 53
        ' 
        ' TextBox2
        ' 
        TextBox2.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        TextBox2.Location = New Point(297, 235)
        TextBox2.Margin = New Padding(3, 4, 3, 4)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(348, 29)
        TextBox2.TabIndex = 55
        TextBox2.Text = "c=RTM"
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label18.Location = New Point(17, 37)
        Label18.Name = "Label18"
        Label18.Size = New Size(159, 23)
        Label18.TabIndex = 70
        Label18.Text = "Load your Setting:"
        ' 
        ' ComboBox6
        ' 
        ComboBox6.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox6.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        ComboBox6.FormattingEnabled = True
        ComboBox6.Items.AddRange(New Object() {"Default"})
        ComboBox6.Location = New Point(185, 33)
        ComboBox6.Margin = New Padding(3, 4, 3, 4)
        ComboBox6.Name = "ComboBox6"
        ComboBox6.Size = New Size(772, 29)
        ComboBox6.TabIndex = 69
        ' 
        ' Label16
        ' 
        Label16.Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point)
        Label16.ForeColor = Color.Red
        Label16.Location = New Point(8, 599)
        Label16.Name = "Label16"
        Label16.Size = New Size(965, 49)
        Label16.TabIndex = 68
        Label16.Text = "Please do not select a wallet address from an exchange server. You could lose your mined coins as a result."
        Label16.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' Label15
        ' 
        Label15.Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point)
        Label15.Location = New Point(8, 540)
        Label15.Name = "Label15"
        Label15.Size = New Size(965, 59)
        Label15.TabIndex = 67
        Label15.Text = "Please check the mining fee and the mining conditions independently on the respective websites of all providers."
        Label15.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' ComboBox5
        ' 
        ComboBox5.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox5.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        ComboBox5.FormattingEnabled = True
        ComboBox5.Location = New Point(17, 395)
        ComboBox5.Margin = New Padding(3, 4, 3, 4)
        ComboBox5.Name = "ComboBox5"
        ComboBox5.Size = New Size(204, 29)
        ComboBox5.TabIndex = 66
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label13.Location = New Point(17, 371)
        Label13.Name = "Label13"
        Label13.Size = New Size(202, 23)
        Label13.TabIndex = 65
        Label13.Text = "Number of cores to use:"
        Label13.TextAlign = ContentAlignment.TopRight
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(185, 77)
        ComboBox1.Margin = New Padding(3, 4, 3, 4)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(772, 29)
        ComboBox1.TabIndex = 49
        ' 
        ' ComboBox4
        ' 
        ComboBox4.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox4.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        ComboBox4.FormattingEnabled = True
        ComboBox4.Items.AddRange(New Object() {"Raptorhash.com", "Raptoreum.Zone", "FlockPool"})
        ComboBox4.Location = New Point(297, 163)
        ComboBox4.Margin = New Padding(3, 4, 3, 4)
        ComboBox4.Name = "ComboBox4"
        ComboBox4.Size = New Size(349, 29)
        ComboBox4.TabIndex = 64
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label7.Location = New Point(14, 81)
        Label7.Name = "Label7"
        Label7.Size = New Size(133, 23)
        Label7.TabIndex = 50
        Label7.Text = "Choos a Wallet:"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label12.Location = New Point(297, 139)
        Label12.Name = "Label12"
        Label12.RightToLeft = RightToLeft.No
        Label12.Size = New Size(115, 23)
        Label12.TabIndex = 63
        Label12.Text = "Choos a Port:"
        Label12.TextAlign = ContentAlignment.TopRight
        ' 
        ' ComboBox2
        ' 
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"Raptorhash.com", "Raptoreum.Zone", "FlockPool"})
        ComboBox2.Location = New Point(17, 163)
        ComboBox2.Margin = New Padding(3, 4, 3, 4)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(204, 29)
        ComboBox2.TabIndex = 51
        ' 
        ' CheckBox3
        ' 
        CheckBox3.AutoSize = True
        CheckBox3.Enabled = False
        CheckBox3.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        CheckBox3.Location = New Point(229, 165)
        CheckBox3.Margin = New Padding(3, 4, 3, 4)
        CheckBox3.Name = "CheckBox3"
        CheckBox3.Size = New Size(73, 27)
        CheckBox3.TabIndex = 62
        CheckBox3.Text = "Solo?"
        CheckBox3.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label6.Location = New Point(17, 139)
        Label6.Name = "Label6"
        Label6.Size = New Size(111, 23)
        Label6.TabIndex = 48
        Label6.Text = "Choos a Pool"
        ' 
        ' Button5
        ' 
        Button5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Button5.BackgroundImage = My.Resources.Resources.Save_icon
        Button5.BackgroundImageLayout = ImageLayout.Stretch
        Button5.Cursor = Cursors.Hand
        Button5.FlatStyle = FlatStyle.Flat
        Button5.ForeColor = SystemColors.Window
        Button5.Location = New Point(302, 355)
        Button5.Margin = New Padding(3, 4, 3, 4)
        Button5.Name = "Button5"
        Button5.Size = New Size(41, 41)
        Button5.TabIndex = 61
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label8.Location = New Point(16, 211)
        Label8.Name = "Label8"
        Label8.Size = New Size(0, 23)
        Label8.TabIndex = 52
        ' 
        ' Button4
        ' 
        Button4.BackColor = Color.YellowGreen
        Button4.Cursor = Cursors.Hand
        Button4.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point)
        Button4.Location = New Point(738, 283)
        Button4.Margin = New Padding(3, 4, 3, 4)
        Button4.Name = "Button4"
        Button4.Size = New Size(152, 92)
        Button4.TabIndex = 60
        Button4.Text = "Starte Mining"
        Button4.UseVisualStyleBackColor = False
        ' 
        ' CheckBox2
        ' 
        CheckBox2.Enabled = False
        CheckBox2.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        CheckBox2.Location = New Point(681, 188)
        CheckBox2.Margin = New Padding(3, 4, 3, 4)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(280, 71)
        CheckBox2.TabIndex = 59
        CheckBox2.Text = "Close RaptorWings after start Mining"
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label9.Location = New Point(297, 211)
        Label9.Name = "Label9"
        Label9.RightToLeft = RightToLeft.No
        Label9.Size = New Size(157, 23)
        Label9.TabIndex = 54
        Label9.Text = "Choos a Password:"
        Label9.TextAlign = ContentAlignment.TopRight
        ' 
        ' CheckBox1
        ' 
        CheckBox1.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        CheckBox1.Location = New Point(681, 139)
        CheckBox1.Margin = New Padding(3, 4, 3, 4)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(280, 51)
        CheckBox1.TabIndex = 58
        CheckBox1.Text = "Start Miner in background"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' ComboBox3
        ' 
        ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox3.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        ComboBox3.FormattingEnabled = True
        ComboBox3.Items.AddRange(New Object() {"XMRIG"})
        ComboBox3.Location = New Point(17, 316)
        ComboBox3.Margin = New Padding(3, 4, 3, 4)
        ComboBox3.Name = "ComboBox3"
        ComboBox3.Size = New Size(204, 29)
        ComboBox3.TabIndex = 57
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label10.Location = New Point(17, 292)
        Label10.Name = "Label10"
        Label10.Size = New Size(130, 23)
        Label10.TabIndex = 56
        Label10.Text = "Choos a Miner:"
        ' 
        ' Label11
        ' 
        Label11.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point)
        Label11.ForeColor = Color.Red
        Label11.ImageAlign = ContentAlignment.MiddleRight
        Label11.Location = New Point(0, 372)
        Label11.Name = "Label11"
        Label11.Size = New Size(992, 132)
        Label11.TabIndex = 47
        Label11.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label5
        ' 
        Label5.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point)
        Label5.ForeColor = Color.Red
        Label5.Location = New Point(3, 88)
        Label5.Name = "Label5"
        Label5.Size = New Size(984, 139)
        Label5.TabIndex = 45
        Label5.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(394, 235)
        Button3.Margin = New Padding(3, 4, 3, 4)
        Button3.Name = "Button3"
        Button3.Size = New Size(197, 108)
        Button3.TabIndex = 46
        Button3.Text = "I have read and understood the warning and would like to proceed"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(Button21)
        TabPage2.Controls.Add(Label42)
        TabPage2.Controls.Add(ComboBox11)
        TabPage2.Controls.Add(Button14)
        TabPage2.Controls.Add(Button13)
        TabPage2.Controls.Add(Button2)
        TabPage2.Controls.Add(Button1)
        TabPage2.Controls.Add(Label19)
        TabPage2.Controls.Add(DataGridView1)
        TabPage2.Location = New Point(4, 30)
        TabPage2.Margin = New Padding(3, 4, 3, 4)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3, 4, 3, 4)
        TabPage2.Size = New Size(989, 695)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Wallets"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' Button21
        ' 
        Button21.BackgroundImage = CType(resources.GetObject("Button21.BackgroundImage"), Image)
        Button21.BackgroundImageLayout = ImageLayout.Stretch
        Button21.Cursor = Cursors.Hand
        Button21.FlatAppearance.BorderSize = 0
        Button21.FlatAppearance.MouseDownBackColor = Color.White
        Button21.FlatAppearance.MouseOverBackColor = Color.White
        Button21.FlatStyle = FlatStyle.Flat
        Button21.ForeColor = SystemColors.Window
        Button21.Location = New Point(286, 8)
        Button21.Margin = New Padding(3, 4, 3, 4)
        Button21.Name = "Button21"
        Button21.Size = New Size(48, 56)
        Button21.TabIndex = 52
        Button21.UseVisualStyleBackColor = True
        ' 
        ' Label42
        ' 
        Label42.AutoSize = True
        Label42.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label42.Location = New Point(685, 8)
        Label42.Name = "Label42"
        Label42.Size = New Size(134, 23)
        Label42.TabIndex = 51
        Label42.Text = "Change profile:"
        ' 
        ' ComboBox11
        ' 
        ComboBox11.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox11.FormattingEnabled = True
        ComboBox11.Items.AddRange(New Object() {"1 - Default"})
        ComboBox11.Location = New Point(685, 35)
        ComboBox11.Margin = New Padding(3, 4, 3, 4)
        ComboBox11.Name = "ComboBox11"
        ComboBox11.Size = New Size(290, 29)
        ComboBox11.TabIndex = 50
        ' 
        ' Button14
        ' 
        Button14.BackgroundImage = My.Resources.Resources._001_RTM_Logo_mini2
        Button14.BackgroundImageLayout = ImageLayout.Stretch
        Button14.Cursor = Cursors.Hand
        Button14.FlatAppearance.BorderSize = 0
        Button14.FlatAppearance.MouseDownBackColor = Color.White
        Button14.FlatAppearance.MouseOverBackColor = Color.White
        Button14.FlatStyle = FlatStyle.Flat
        Button14.ForeColor = SystemColors.Window
        Button14.Location = New Point(223, 8)
        Button14.Margin = New Padding(3, 4, 3, 4)
        Button14.Name = "Button14"
        Button14.Size = New Size(48, 56)
        Button14.TabIndex = 49
        Button14.UseVisualStyleBackColor = True
        ' 
        ' Button13
        ' 
        Button13.BackgroundImage = My.Resources.Resources.Delete_file_icon
        Button13.BackgroundImageLayout = ImageLayout.Stretch
        Button13.Cursor = Cursors.Hand
        Button13.FlatStyle = FlatStyle.Flat
        Button13.ForeColor = SystemColors.Window
        Button13.Location = New Point(150, 8)
        Button13.Margin = New Padding(3, 4, 3, 4)
        Button13.Name = "Button13"
        Button13.Size = New Size(48, 56)
        Button13.TabIndex = 48
        Button13.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.BackgroundImage = My.Resources.Resources.Save_icon
        Button2.BackgroundImageLayout = ImageLayout.Stretch
        Button2.Cursor = Cursors.Hand
        Button2.Enabled = False
        Button2.FlatStyle = FlatStyle.Flat
        Button2.ForeColor = SystemColors.Window
        Button2.Location = New Point(81, 8)
        Button2.Margin = New Padding(3, 4, 3, 4)
        Button2.Name = "Button2"
        Button2.Size = New Size(51, 56)
        Button2.TabIndex = 47
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.BackgroundImage = My.Resources.Resources.new_icon
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.Cursor = Cursors.Hand
        Button1.FlatStyle = FlatStyle.Flat
        Button1.ForeColor = SystemColors.Window
        Button1.Location = New Point(9, 8)
        Button1.Margin = New Padding(3, 4, 3, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(53, 56)
        Button1.TabIndex = 46
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        Label19.Location = New Point(7, 627)
        Label19.Name = "Label19"
        Label19.Size = New Size(866, 23)
        Label19.TabIndex = 8
        Label19.Text = "All wallet information is read out via the RTM Explorer API. All prizes are transmitted through the Coingecko API."
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToResizeColumns = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridView1.BackgroundColor = SystemColors.Window
        DataGridView1.BorderStyle = BorderStyle.Fixed3D
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column1, Column2, Column3, Column4, Column6, Column5, Column7})
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = SystemColors.Window
        DataGridViewCellStyle6.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle6.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = Color.RosyBrown
        DataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = DataGridViewTriState.False
        DataGridView1.DefaultCellStyle = DataGridViewCellStyle6
        DataGridView1.Location = New Point(7, 72)
        DataGridView1.Margin = New Padding(3, 4, 3, 4)
        DataGridView1.MultiSelect = False
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersVisible = False
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.RowTemplate.Height = 25
        DataGridView1.Size = New Size(971, 551)
        DataGridView1.TabIndex = 1
        ' 
        ' Column1
        ' 
        Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Column1.HeaderText = "Nr."
        Column1.MinimumWidth = 6
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        Column1.SortMode = DataGridViewColumnSortMode.NotSortable
        Column1.Width = 39
        ' 
        ' Column2
        ' 
        Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Column2.HeaderText = "Adress"
        Column2.MinimumWidth = 6
        Column2.Name = "Column2"
        Column2.SortMode = DataGridViewColumnSortMode.NotSortable
        Column2.Width = 66
        ' 
        ' Column3
        ' 
        Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Column3.HeaderText = "Description"
        Column3.MinimumWidth = 6
        Column3.Name = "Column3"
        Column3.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Column4
        ' 
        Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        Column4.DefaultCellStyle = DataGridViewCellStyle2
        Column4.HeaderText = "Balance"
        Column4.MinimumWidth = 6
        Column4.Name = "Column4"
        Column4.ReadOnly = True
        Column4.SortMode = DataGridViewColumnSortMode.NotSortable
        Column4.Width = 75
        ' 
        ' Column6
        ' 
        Column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        Column6.DefaultCellStyle = DataGridViewCellStyle3
        Column6.HeaderText = "BTC"
        Column6.MinimumWidth = 6
        Column6.Name = "Column6"
        Column6.ReadOnly = True
        Column6.SortMode = DataGridViewColumnSortMode.NotSortable
        Column6.Width = 44
        ' 
        ' Column5
        ' 
        Column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        Column5.DefaultCellStyle = DataGridViewCellStyle4
        Column5.HeaderText = "USD"
        Column5.MinimumWidth = 6
        Column5.Name = "Column5"
        Column5.ReadOnly = True
        Column5.SortMode = DataGridViewColumnSortMode.NotSortable
        Column5.Width = 49
        ' 
        ' Column7
        ' 
        Column7.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        Column7.DefaultCellStyle = DataGridViewCellStyle5
        Column7.HeaderText = "EUR"
        Column7.MinimumWidth = 6
        Column7.Name = "Column7"
        Column7.ReadOnly = True
        Column7.SortMode = DataGridViewColumnSortMode.NotSortable
        Column7.Width = 47
        ' 
        ' TabPage1
        ' 
        TabPage1.BackColor = Color.Transparent
        TabPage1.Controls.Add(PictureBox1)
        TabPage1.Controls.Add(Label34)
        TabPage1.Controls.Add(Label29)
        TabPage1.Controls.Add(LinkLabel1)
        TabPage1.Controls.Add(Label17)
        TabPage1.Controls.Add(Label4)
        TabPage1.Controls.Add(Label2)
        TabPage1.Controls.Add(Label1)
        TabPage1.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        TabPage1.Location = New Point(4, 30)
        TabPage1.Margin = New Padding(3, 4, 3, 4)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3, 4, 3, 4)
        TabPage1.Size = New Size(989, 695)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Overview"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackgroundImage = My.Resources.Resources.Rptorwings_logo_small
        PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox1.InitialImage = Nothing
        PictureBox1.Location = New Point(23, 44)
        PictureBox1.Margin = New Padding(3, 4, 3, 4)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(475, 321)
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' Label34
        ' 
        Label34.Font = New Font("Segoe UI", 39.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label34.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Label34.Location = New Point(495, 145)
        Label34.Name = "Label34"
        Label34.Size = New Size(483, 80)
        Label34.TabIndex = 13
        Label34.Text = "RAPTORWINGS"
        ' 
        ' Label29
        ' 
        Label29.AutoSize = True
        Label29.Location = New Point(315, 369)
        Label29.Name = "Label29"
        Label29.Size = New Size(66, 20)
        Label29.TabIndex = 12
        Label29.Text = "Logo by:"
        ' 
        ' LinkLabel1
        ' 
        LinkLabel1.AutoSize = True
        LinkLabel1.LinkColor = Color.Black
        LinkLabel1.Location = New Point(374, 369)
        LinkLabel1.Name = "LinkLabel1"
        LinkLabel1.Size = New Size(136, 20)
        LinkLabel1.TabIndex = 11
        LinkLabel1.TabStop = True
        LinkLabel1.Text = "zlataamaranth.com"
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Label17.Location = New Point(306, 537)
        Label17.Name = "Label17"
        Label17.Size = New Size(93, 28)
        Label17.TabIndex = 8
        Label17.Text = "0,00 RTM"
        ' 
        ' Label4
        ' 
        Label4.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point)
        Label4.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Label4.Location = New Point(519, 225)
        Label4.Name = "Label4"
        Label4.Size = New Size(459, 44)
        Label4.TabIndex = 6
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 18F, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point)
        Label2.Location = New Point(304, 452)
        Label2.Name = "Label2"
        Label2.Size = New Size(431, 41)
        Label2.TabIndex = 4
        Label2.Text = "Balance of all your addresses:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        Label1.Location = New Point(304, 495)
        Label1.Name = "Label1"
        Label1.Size = New Size(141, 41)
        Label1.TabIndex = 3
        Label1.Text = "0,00 RTM"
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Controls.Add(TabPage9)
        TabControl1.Controls.Add(TabPage10)
        TabControl1.Controls.Add(TabPage6)
        TabControl1.Dock = DockStyle.Fill
        TabControl1.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        TabControl1.Location = New Point(0, 0)
        TabControl1.Margin = New Padding(3, 4, 3, 4)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(997, 729)
        TabControl1.TabIndex = 1
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        BackColor = SystemColors.WindowFrame
        ClientSize = New Size(997, 729)
        Controls.Add(StatusStrip1)
        Controls.Add(TabControl1)
        ForeColor = SystemColors.ControlText
        FormBorderStyle = FormBorderStyle.Fixed3D
        HelpButton = True
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(3, 4, 3, 4)
        MaximizeBox = False
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        TabPage6.ResumeLayout(False)
        TabPage10.ResumeLayout(False)
        TabPage10.PerformLayout()
        GroupBox2.ResumeLayout(False)
        CType(DataGridView3, ComponentModel.ISupportInitialize).EndInit()
        TabPage9.ResumeLayout(False)
        TabPage9.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        TabPage3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        TabControl1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents txID As DataGridViewTextBoxColumn
    Friend WithEvents Wallet As DataGridViewTextBoxColumn
    Friend WithEvents Value As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents Timer2 As Timer
    Friend WithEvents miniToolStrip As StatusStrip
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As ToolStripStatusLabel
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents Timer4 As Timer
    Friend WithEvents Timer5 As Timer
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents RichTextBox3 As RichTextBox
    Friend WithEvents TabPage10 As TabPage
    Friend WithEvents Button19 As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Button18 As Button
    Friend WithEvents Button20 As Button
    Friend WithEvents DataGridView3 As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents dgv3_Description As DataGridViewTextBoxColumn
    Friend WithEvents Label40 As Label
    Friend WithEvents Label35 As Label
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents ComboBox9 As ComboBox
    Friend WithEvents TabPage9 As TabPage
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label46 As Label
    Friend WithEvents Label45 As Label
    Friend WithEvents Label44 As Label
    Friend WithEvents Label41 As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Button17 As Button
    Friend WithEvents Label39 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button16 As Button
    Friend WithEvents Label37 As Label
    Friend WithEvents Button12 As Button
    Friend WithEvents Button15 As Button
    Friend WithEvents Label38 As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents ComboBox10 As ComboBox
    Friend WithEvents Label32 As Label
    Friend WithEvents Button6 As Button
    Friend WithEvents Label20 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents ComboBox6 As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents ComboBox5 As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ComboBox4 As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Label9 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Button21 As Button
    Friend WithEvents Label42 As Label
    Friend WithEvents ComboBox11 As ComboBox
    Friend WithEvents Button14 As Button
    Friend WithEvents Button13 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label19 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label34 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Label17 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TabControl1 As TabControl
End Class
