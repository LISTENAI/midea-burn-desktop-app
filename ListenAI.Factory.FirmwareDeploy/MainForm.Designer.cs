namespace ListenAI.Factory.FirmwareDeploy {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            gbSettings = new GroupBox();
            btnPack = new Button();
            btnFlash = new Button();
            btnFwSelect = new Button();
            btnMES = new Button();
            statusStrip1 = new StatusStrip();
            tsslSafeMode = new ToolStripStatusLabel();
            tsslWorkingMode = new ToolStripStatusLabel();
            tsslCurrentFirmware = new ToolStripStatusLabel();
            gbMod1 = new GroupBox();
            panelResult1 = new Panel();
            pbCommon1Progress = new ProgressBar();
            btnCommon1Result = new Button();
            lbCommon1Title = new Label();
            panelSn1 = new Panel();
            tbCommon1Serial = new TextBox();
            lbCommon1TitleSn = new Label();
            panelWifi1 = new Panel();
            cmbWifi1Port = new ComboBox();
            cbWifi1IsDefault = new CheckBox();
            lbWifi1TitleDefault = new Label();
            tbWifi1Stopbits = new TextBox();
            lbWifi1TitleStopbit = new Label();
            tbWifi1Parity = new TextBox();
            lbWifi1TitleChecksum = new Label();
            tbWifi1Databits = new TextBox();
            lbWifi1TitleDatabit = new Label();
            tbWifi1BaudRate = new TextBox();
            lbWifi1TitleBaudRate = new Label();
            lbWifi1TitlePort = new Label();
            lbWifi1Title = new Label();
            panelCsk1 = new Panel();
            cmbCsk1Port = new ComboBox();
            cbCsk1IsDefault = new CheckBox();
            lbCsk1TitleDefault = new Label();
            tbCsk1Stopbits = new TextBox();
            lbCsk1TitleStopbit = new Label();
            tbCsk1Parity = new TextBox();
            lbCsk1TitleChecksum = new Label();
            tbCsk1Databits = new TextBox();
            lbCsk1TitleDatabit = new Label();
            tbCsk1BaudRate = new TextBox();
            lbCsk1TitleBaudRate = new Label();
            lbCsk1TitlePort = new Label();
            lbCsk1Title = new Label();
            gbSettings.SuspendLayout();
            statusStrip1.SuspendLayout();
            gbMod1.SuspendLayout();
            panelResult1.SuspendLayout();
            panelSn1.SuspendLayout();
            panelWifi1.SuspendLayout();
            panelCsk1.SuspendLayout();
            SuspendLayout();
            // 
            // gbSettings
            // 
            gbSettings.Controls.Add(btnPack);
            gbSettings.Controls.Add(btnFlash);
            gbSettings.Controls.Add(btnFwSelect);
            gbSettings.Controls.Add(btnMES);
            gbSettings.Location = new Point(8, 8);
            gbSettings.Margin = new Padding(2);
            gbSettings.Name = "gbSettings";
            gbSettings.Padding = new Padding(2);
            gbSettings.Size = new Size(1161, 83);
            gbSettings.TabIndex = 2;
            gbSettings.TabStop = false;
            gbSettings.Text = "烧录设置";
            // 
            // btnPack
            // 
            btnPack.BackColor = SystemColors.Control;
            btnPack.Location = new Point(940, 27);
            btnPack.Margin = new Padding(2);
            btnPack.Name = "btnPack";
            btnPack.Size = new Size(200, 41);
            btnPack.TabIndex = 3;
            btnPack.Text = "打包";
            btnPack.UseVisualStyleBackColor = false;
            btnPack.Click += btnPack_Click;
            // 
            // btnFlash
            // 
            btnFlash.BackColor = SystemColors.Control;
            btnFlash.Location = new Point(634, 27);
            btnFlash.Margin = new Padding(2);
            btnFlash.Name = "btnFlash";
            btnFlash.Size = new Size(200, 41);
            btnFlash.TabIndex = 2;
            btnFlash.Text = "烧录";
            btnFlash.UseVisualStyleBackColor = false;
            btnFlash.Click += btnFlash_Click;
            // 
            // btnFwSelect
            // 
            btnFwSelect.BackColor = SystemColors.Highlight;
            btnFwSelect.Location = new Point(329, 27);
            btnFwSelect.Margin = new Padding(2);
            btnFwSelect.Name = "btnFwSelect";
            btnFwSelect.Size = new Size(200, 41);
            btnFwSelect.TabIndex = 1;
            btnFwSelect.Text = "浏览";
            btnFwSelect.UseVisualStyleBackColor = false;
            btnFwSelect.Click += btnFwSelect_Click;
            // 
            // btnMES
            // 
            btnMES.BackColor = SystemColors.Highlight;
            btnMES.Location = new Point(24, 27);
            btnMES.Margin = new Padding(2);
            btnMES.Name = "btnMES";
            btnMES.Size = new Size(200, 41);
            btnMES.TabIndex = 0;
            btnMES.Text = "MES记录";
            btnMES.UseVisualStyleBackColor = false;
            btnMES.Click += btnMES_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsslSafeMode, tsslWorkingMode, tsslCurrentFirmware });
            statusStrip1.Location = new Point(0, 477);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 10, 0);
            statusStrip1.Size = new Size(1179, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsslSafeMode
            // 
            tsslSafeMode.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            tsslSafeMode.ForeColor = Color.Red;
            tsslSafeMode.Name = "tsslSafeMode";
            tsslSafeMode.Size = new Size(161, 17);
            tsslSafeMode.Text = "安全模式，请尽快重启！";
            tsslSafeMode.Visible = false;
            // 
            // tsslWorkingMode
            // 
            tsslWorkingMode.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsslWorkingMode.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            tsslWorkingMode.ForeColor = Color.FromArgb(255, 128, 0);
            tsslWorkingMode.Name = "tsslWorkingMode";
            tsslWorkingMode.Size = new Size(119, 17);
            tsslWorkingMode.Text = "当前模式：离在线";
            tsslWorkingMode.Click += tsslWorkingMode_Click;
            // 
            // tsslCurrentFirmware
            // 
            tsslCurrentFirmware.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsslCurrentFirmware.Name = "tsslCurrentFirmware";
            tsslCurrentFirmware.Size = new Size(112, 17);
            tsslCurrentFirmware.Text = "当前固件: (未选定)";
            tsslCurrentFirmware.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gbMod1
            // 
            gbMod1.Controls.Add(panelResult1);
            gbMod1.Controls.Add(panelSn1);
            gbMod1.Controls.Add(panelWifi1);
            gbMod1.Controls.Add(panelCsk1);
            gbMod1.Location = new Point(8, 95);
            gbMod1.Margin = new Padding(2);
            gbMod1.Name = "gbMod1";
            gbMod1.Padding = new Padding(2);
            gbMod1.Size = new Size(286, 374);
            gbMod1.TabIndex = 4;
            gbMod1.TabStop = false;
            // 
            // panelResult1
            // 
            panelResult1.Controls.Add(pbCommon1Progress);
            panelResult1.Controls.Add(btnCommon1Result);
            panelResult1.Controls.Add(lbCommon1Title);
            panelResult1.Location = new Point(12, 304);
            panelResult1.Margin = new Padding(2);
            panelResult1.Name = "panelResult1";
            panelResult1.Size = new Size(262, 61);
            panelResult1.TabIndex = 3;
            // 
            // pbCommon1Progress
            // 
            pbCommon1Progress.Location = new Point(20, 7);
            pbCommon1Progress.Margin = new Padding(2);
            pbCommon1Progress.Maximum = 200;
            pbCommon1Progress.Name = "pbCommon1Progress";
            pbCommon1Progress.Size = new Size(226, 19);
            pbCommon1Progress.Step = 1;
            pbCommon1Progress.Style = ProgressBarStyle.Continuous;
            pbCommon1Progress.TabIndex = 5;
            // 
            // btnCommon1Result
            // 
            btnCommon1Result.BackColor = SystemColors.Control;
            btnCommon1Result.ForeColor = SystemColors.ControlText;
            btnCommon1Result.Location = new Point(114, 32);
            btnCommon1Result.Margin = new Padding(2);
            btnCommon1Result.Name = "btnCommon1Result";
            btnCommon1Result.Size = new Size(132, 20);
            btnCommon1Result.TabIndex = 1;
            btnCommon1Result.Text = "Pass/Fail";
            btnCommon1Result.UseVisualStyleBackColor = false;
            // 
            // lbCommon1Title
            // 
            lbCommon1Title.AutoSize = true;
            lbCommon1Title.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbCommon1Title.Location = new Point(19, 35);
            lbCommon1Title.Margin = new Padding(2, 0, 2, 0);
            lbCommon1Title.Name = "lbCommon1Title";
            lbCommon1Title.Size = new Size(64, 21);
            lbCommon1Title.TabIndex = 0;
            lbCommon1Title.Text = "模组一";
            lbCommon1Title.TextAlign = ContentAlignment.TopRight;
            // 
            // panelSn1
            // 
            panelSn1.Controls.Add(tbCommon1Serial);
            panelSn1.Controls.Add(lbCommon1TitleSn);
            panelSn1.Location = new Point(12, 266);
            panelSn1.Margin = new Padding(2);
            panelSn1.Name = "panelSn1";
            panelSn1.Size = new Size(262, 35);
            panelSn1.TabIndex = 2;
            // 
            // tbCommon1Serial
            // 
            tbCommon1Serial.Location = new Point(95, 7);
            tbCommon1Serial.Margin = new Padding(2);
            tbCommon1Serial.Name = "tbCommon1Serial";
            tbCommon1Serial.Size = new Size(154, 23);
            tbCommon1Serial.TabIndex = 6;
            // 
            // lbCommon1TitleSn
            // 
            lbCommon1TitleSn.AutoSize = true;
            lbCommon1TitleSn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbCommon1TitleSn.Location = new Point(9, 10);
            lbCommon1TitleSn.Margin = new Padding(2, 0, 2, 0);
            lbCommon1TitleSn.Name = "lbCommon1TitleSn";
            lbCommon1TitleSn.Size = new Size(77, 15);
            lbCommon1TitleSn.TabIndex = 5;
            lbCommon1TitleSn.Text = "产品序列号";
            // 
            // panelWifi1
            // 
            panelWifi1.Controls.Add(cmbWifi1Port);
            panelWifi1.Controls.Add(cbWifi1IsDefault);
            panelWifi1.Controls.Add(lbWifi1TitleDefault);
            panelWifi1.Controls.Add(tbWifi1Stopbits);
            panelWifi1.Controls.Add(lbWifi1TitleStopbit);
            panelWifi1.Controls.Add(tbWifi1Parity);
            panelWifi1.Controls.Add(lbWifi1TitleChecksum);
            panelWifi1.Controls.Add(tbWifi1Databits);
            panelWifi1.Controls.Add(lbWifi1TitleDatabit);
            panelWifi1.Controls.Add(tbWifi1BaudRate);
            panelWifi1.Controls.Add(lbWifi1TitleBaudRate);
            panelWifi1.Controls.Add(lbWifi1TitlePort);
            panelWifi1.Controls.Add(lbWifi1Title);
            panelWifi1.Location = new Point(145, 18);
            panelWifi1.Margin = new Padding(2);
            panelWifi1.Name = "panelWifi1";
            panelWifi1.Size = new Size(129, 242);
            panelWifi1.TabIndex = 1;
            // 
            // cmbWifi1Port
            // 
            cmbWifi1Port.FormattingEnabled = true;
            cmbWifi1Port.Location = new Point(63, 52);
            cmbWifi1Port.Margin = new Padding(3, 2, 3, 2);
            cmbWifi1Port.Name = "cmbWifi1Port";
            cmbWifi1Port.Size = new Size(58, 23);
            cmbWifi1Port.TabIndex = 25;
            // 
            // cbWifi1IsDefault
            // 
            cbWifi1IsDefault.AutoSize = true;
            cbWifi1IsDefault.Location = new Point(85, 217);
            cbWifi1IsDefault.Margin = new Padding(2);
            cbWifi1IsDefault.Name = "cbWifi1IsDefault";
            cbWifi1IsDefault.Size = new Size(15, 14);
            cbWifi1IsDefault.TabIndex = 24;
            cbWifi1IsDefault.UseVisualStyleBackColor = true;
            // 
            // lbWifi1TitleDefault
            // 
            lbWifi1TitleDefault.AutoSize = true;
            lbWifi1TitleDefault.Location = new Point(10, 215);
            lbWifi1TitleDefault.Margin = new Padding(2, 0, 2, 0);
            lbWifi1TitleDefault.Name = "lbWifi1TitleDefault";
            lbWifi1TitleDefault.Size = new Size(59, 15);
            lbWifi1TitleDefault.TabIndex = 23;
            lbWifi1TitleDefault.Text = "是否默认";
            // 
            // tbWifi1Stopbits
            // 
            tbWifi1Stopbits.Location = new Point(65, 182);
            tbWifi1Stopbits.Margin = new Padding(2);
            tbWifi1Stopbits.MaxLength = 1;
            tbWifi1Stopbits.Name = "tbWifi1Stopbits";
            tbWifi1Stopbits.ReadOnly = true;
            tbWifi1Stopbits.Size = new Size(57, 23);
            tbWifi1Stopbits.TabIndex = 22;
            tbWifi1Stopbits.Text = "1";
            // 
            // lbWifi1TitleStopbit
            // 
            lbWifi1TitleStopbit.AutoSize = true;
            lbWifi1TitleStopbit.Location = new Point(10, 182);
            lbWifi1TitleStopbit.Margin = new Padding(2, 0, 2, 0);
            lbWifi1TitleStopbit.Name = "lbWifi1TitleStopbit";
            lbWifi1TitleStopbit.Size = new Size(46, 15);
            lbWifi1TitleStopbit.TabIndex = 21;
            lbWifi1TitleStopbit.Text = "停止位";
            // 
            // tbWifi1Parity
            // 
            tbWifi1Parity.Location = new Point(65, 148);
            tbWifi1Parity.Margin = new Padding(2);
            tbWifi1Parity.MaxLength = 1;
            tbWifi1Parity.Name = "tbWifi1Parity";
            tbWifi1Parity.ReadOnly = true;
            tbWifi1Parity.Size = new Size(57, 23);
            tbWifi1Parity.TabIndex = 20;
            tbWifi1Parity.Text = "0";
            // 
            // lbWifi1TitleChecksum
            // 
            lbWifi1TitleChecksum.AutoSize = true;
            lbWifi1TitleChecksum.Location = new Point(10, 148);
            lbWifi1TitleChecksum.Margin = new Padding(2, 0, 2, 0);
            lbWifi1TitleChecksum.Name = "lbWifi1TitleChecksum";
            lbWifi1TitleChecksum.Size = new Size(46, 15);
            lbWifi1TitleChecksum.TabIndex = 19;
            lbWifi1TitleChecksum.Text = "校验位";
            // 
            // tbWifi1Databits
            // 
            tbWifi1Databits.Location = new Point(65, 115);
            tbWifi1Databits.Margin = new Padding(2);
            tbWifi1Databits.MaxLength = 1;
            tbWifi1Databits.Name = "tbWifi1Databits";
            tbWifi1Databits.ReadOnly = true;
            tbWifi1Databits.Size = new Size(57, 23);
            tbWifi1Databits.TabIndex = 18;
            tbWifi1Databits.Text = "8";
            // 
            // lbWifi1TitleDatabit
            // 
            lbWifi1TitleDatabit.AutoSize = true;
            lbWifi1TitleDatabit.Location = new Point(10, 117);
            lbWifi1TitleDatabit.Margin = new Padding(2, 0, 2, 0);
            lbWifi1TitleDatabit.Name = "lbWifi1TitleDatabit";
            lbWifi1TitleDatabit.Size = new Size(46, 15);
            lbWifi1TitleDatabit.TabIndex = 17;
            lbWifi1TitleDatabit.Text = "数据位";
            // 
            // tbWifi1BaudRate
            // 
            tbWifi1BaudRate.Location = new Point(65, 83);
            tbWifi1BaudRate.Margin = new Padding(2);
            tbWifi1BaudRate.MaxLength = 7;
            tbWifi1BaudRate.Name = "tbWifi1BaudRate";
            tbWifi1BaudRate.Size = new Size(57, 23);
            tbWifi1BaudRate.TabIndex = 16;
            tbWifi1BaudRate.Text = "1000000";
            // 
            // lbWifi1TitleBaudRate
            // 
            lbWifi1TitleBaudRate.AutoSize = true;
            lbWifi1TitleBaudRate.Location = new Point(10, 87);
            lbWifi1TitleBaudRate.Margin = new Padding(2, 0, 2, 0);
            lbWifi1TitleBaudRate.Name = "lbWifi1TitleBaudRate";
            lbWifi1TitleBaudRate.Size = new Size(46, 15);
            lbWifi1TitleBaudRate.TabIndex = 15;
            lbWifi1TitleBaudRate.Text = "波特率";
            // 
            // lbWifi1TitlePort
            // 
            lbWifi1TitlePort.AutoSize = true;
            lbWifi1TitlePort.Location = new Point(23, 55);
            lbWifi1TitlePort.Margin = new Padding(2, 0, 2, 0);
            lbWifi1TitlePort.Name = "lbWifi1TitlePort";
            lbWifi1TitlePort.Size = new Size(33, 15);
            lbWifi1TitlePort.TabIndex = 13;
            lbWifi1TitlePort.Text = "串口";
            // 
            // lbWifi1Title
            // 
            lbWifi1Title.AutoSize = true;
            lbWifi1Title.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbWifi1Title.Location = new Point(10, 16);
            lbWifi1Title.Margin = new Padding(2, 0, 2, 0);
            lbWifi1Title.Name = "lbWifi1Title";
            lbWifi1Title.Size = new Size(116, 21);
            lbWifi1Title.TabIndex = 0;
            lbWifi1Title.Text = "WIFI串口烧录";
            // 
            // panelCsk1
            // 
            panelCsk1.Controls.Add(cmbCsk1Port);
            panelCsk1.Controls.Add(cbCsk1IsDefault);
            panelCsk1.Controls.Add(lbCsk1TitleDefault);
            panelCsk1.Controls.Add(tbCsk1Stopbits);
            panelCsk1.Controls.Add(lbCsk1TitleStopbit);
            panelCsk1.Controls.Add(tbCsk1Parity);
            panelCsk1.Controls.Add(lbCsk1TitleChecksum);
            panelCsk1.Controls.Add(tbCsk1Databits);
            panelCsk1.Controls.Add(lbCsk1TitleDatabit);
            panelCsk1.Controls.Add(tbCsk1BaudRate);
            panelCsk1.Controls.Add(lbCsk1TitleBaudRate);
            panelCsk1.Controls.Add(lbCsk1TitlePort);
            panelCsk1.Controls.Add(lbCsk1Title);
            panelCsk1.Location = new Point(12, 18);
            panelCsk1.Margin = new Padding(2);
            panelCsk1.Name = "panelCsk1";
            panelCsk1.Size = new Size(129, 242);
            panelCsk1.TabIndex = 0;
            // 
            // cmbCsk1Port
            // 
            cmbCsk1Port.FormattingEnabled = true;
            cmbCsk1Port.Location = new Point(61, 52);
            cmbCsk1Port.Margin = new Padding(3, 2, 3, 2);
            cmbCsk1Port.Name = "cmbCsk1Port";
            cmbCsk1Port.Size = new Size(58, 23);
            cmbCsk1Port.TabIndex = 5;
            // 
            // cbCsk1IsDefault
            // 
            cbCsk1IsDefault.AutoSize = true;
            cbCsk1IsDefault.Location = new Point(81, 217);
            cbCsk1IsDefault.Margin = new Padding(2);
            cbCsk1IsDefault.Name = "cbCsk1IsDefault";
            cbCsk1IsDefault.Size = new Size(15, 14);
            cbCsk1IsDefault.TabIndex = 12;
            cbCsk1IsDefault.UseVisualStyleBackColor = true;
            // 
            // lbCsk1TitleDefault
            // 
            lbCsk1TitleDefault.AutoSize = true;
            lbCsk1TitleDefault.Location = new Point(8, 215);
            lbCsk1TitleDefault.Margin = new Padding(2, 0, 2, 0);
            lbCsk1TitleDefault.Name = "lbCsk1TitleDefault";
            lbCsk1TitleDefault.Size = new Size(59, 15);
            lbCsk1TitleDefault.TabIndex = 11;
            lbCsk1TitleDefault.Text = "是否默认";
            // 
            // tbCsk1Stopbits
            // 
            tbCsk1Stopbits.Location = new Point(62, 182);
            tbCsk1Stopbits.Margin = new Padding(2);
            tbCsk1Stopbits.MaxLength = 1;
            tbCsk1Stopbits.Name = "tbCsk1Stopbits";
            tbCsk1Stopbits.ReadOnly = true;
            tbCsk1Stopbits.Size = new Size(57, 23);
            tbCsk1Stopbits.TabIndex = 10;
            tbCsk1Stopbits.Text = "1";
            // 
            // lbCsk1TitleStopbit
            // 
            lbCsk1TitleStopbit.AutoSize = true;
            lbCsk1TitleStopbit.Location = new Point(8, 182);
            lbCsk1TitleStopbit.Margin = new Padding(2, 0, 2, 0);
            lbCsk1TitleStopbit.Name = "lbCsk1TitleStopbit";
            lbCsk1TitleStopbit.Size = new Size(46, 15);
            lbCsk1TitleStopbit.TabIndex = 9;
            lbCsk1TitleStopbit.Text = "停止位";
            // 
            // tbCsk1Parity
            // 
            tbCsk1Parity.Location = new Point(62, 148);
            tbCsk1Parity.Margin = new Padding(2);
            tbCsk1Parity.MaxLength = 1;
            tbCsk1Parity.Name = "tbCsk1Parity";
            tbCsk1Parity.ReadOnly = true;
            tbCsk1Parity.Size = new Size(57, 23);
            tbCsk1Parity.TabIndex = 8;
            tbCsk1Parity.Text = "0";
            // 
            // lbCsk1TitleChecksum
            // 
            lbCsk1TitleChecksum.AutoSize = true;
            lbCsk1TitleChecksum.Location = new Point(8, 148);
            lbCsk1TitleChecksum.Margin = new Padding(2, 0, 2, 0);
            lbCsk1TitleChecksum.Name = "lbCsk1TitleChecksum";
            lbCsk1TitleChecksum.Size = new Size(46, 15);
            lbCsk1TitleChecksum.TabIndex = 7;
            lbCsk1TitleChecksum.Text = "校验位";
            // 
            // tbCsk1Databits
            // 
            tbCsk1Databits.Location = new Point(62, 115);
            tbCsk1Databits.Margin = new Padding(2);
            tbCsk1Databits.MaxLength = 1;
            tbCsk1Databits.Name = "tbCsk1Databits";
            tbCsk1Databits.ReadOnly = true;
            tbCsk1Databits.Size = new Size(57, 23);
            tbCsk1Databits.TabIndex = 6;
            tbCsk1Databits.Text = "8";
            // 
            // lbCsk1TitleDatabit
            // 
            lbCsk1TitleDatabit.AutoSize = true;
            lbCsk1TitleDatabit.Location = new Point(8, 117);
            lbCsk1TitleDatabit.Margin = new Padding(2, 0, 2, 0);
            lbCsk1TitleDatabit.Name = "lbCsk1TitleDatabit";
            lbCsk1TitleDatabit.Size = new Size(46, 15);
            lbCsk1TitleDatabit.TabIndex = 5;
            lbCsk1TitleDatabit.Text = "数据位";
            // 
            // tbCsk1BaudRate
            // 
            tbCsk1BaudRate.Location = new Point(62, 83);
            tbCsk1BaudRate.Margin = new Padding(2);
            tbCsk1BaudRate.MaxLength = 7;
            tbCsk1BaudRate.Name = "tbCsk1BaudRate";
            tbCsk1BaudRate.Size = new Size(57, 23);
            tbCsk1BaudRate.TabIndex = 4;
            tbCsk1BaudRate.Text = "1500000";
            // 
            // lbCsk1TitleBaudRate
            // 
            lbCsk1TitleBaudRate.AutoSize = true;
            lbCsk1TitleBaudRate.Location = new Point(8, 85);
            lbCsk1TitleBaudRate.Margin = new Padding(2, 0, 2, 0);
            lbCsk1TitleBaudRate.Name = "lbCsk1TitleBaudRate";
            lbCsk1TitleBaudRate.Size = new Size(46, 15);
            lbCsk1TitleBaudRate.TabIndex = 3;
            lbCsk1TitleBaudRate.Text = "波特率";
            // 
            // lbCsk1TitlePort
            // 
            lbCsk1TitlePort.AutoSize = true;
            lbCsk1TitlePort.Location = new Point(21, 55);
            lbCsk1TitlePort.Margin = new Padding(2, 0, 2, 0);
            lbCsk1TitlePort.Name = "lbCsk1TitlePort";
            lbCsk1TitlePort.Size = new Size(33, 15);
            lbCsk1TitlePort.TabIndex = 1;
            lbCsk1TitlePort.Text = "串口";
            // 
            // lbCsk1Title
            // 
            lbCsk1Title.AutoSize = true;
            lbCsk1Title.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbCsk1Title.Location = new Point(8, 16);
            lbCsk1Title.Margin = new Padding(2, 0, 2, 0);
            lbCsk1Title.Name = "lbCsk1Title";
            lbCsk1Title.Size = new Size(111, 21);
            lbCsk1Title.TabIndex = 0;
            lbCsk1Title.Text = "CSK串口烧录";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1179, 499);
            Controls.Add(gbMod1);
            Controls.Add(statusStrip1);
            Controls.Add(gbSettings);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "聆思模组烧录工具 v3.1.0";
            FormClosing += MainForm_FormClosing;
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            gbSettings.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            gbMod1.ResumeLayout(false);
            panelResult1.ResumeLayout(false);
            panelResult1.PerformLayout();
            panelSn1.ResumeLayout(false);
            panelSn1.PerformLayout();
            panelWifi1.ResumeLayout(false);
            panelWifi1.PerformLayout();
            panelCsk1.ResumeLayout(false);
            panelCsk1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gbSettings;
        private Button btnFlash;
        private Button btnFwSelect;
        private Button btnMES;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsslCurrentFirmware;
        private GroupBox gbMod1;
        private Panel panelCsk1;
        private Label lbCsk1Title;
        private Panel panelWifi1;
        private Label lbWifi1Title;
        private Panel panelSn1;
        private Label lbCsk1TitlePort;
        private TextBox tbCsk1Stopbits;
        private Label lbCsk1TitleStopbit;
        private TextBox tbCsk1Parity;
        private Label lbCsk1TitleChecksum;
        private TextBox tbCsk1Databits;
        private Label lbCsk1TitleDatabit;
        private TextBox tbCsk1BaudRate;
        private Label lbCsk1TitleBaudRate;
        private Label lbCsk1TitleDefault;
        private CheckBox cbWifi1IsDefault;
        private Label lbWifi1TitleDefault;
        private TextBox tbWifi1Stopbits;
        private Label lbWifi1TitleStopbit;
        private TextBox tbWifi1Parity;
        private Label lbWifi1TitleChecksum;
        private TextBox tbWifi1Databits;
        private Label lbWifi1TitleDatabit;
        private TextBox tbWifi1BaudRate;
        private Label lbWifi1TitleBaudRate;
        private Label lbWifi1TitlePort;
        private CheckBox cbCsk1IsDefault;
        private TextBox tbCommon1Serial;
        private Label lbCommon1TitleSn;
        private Panel panelResult1;
        private Label lbCommon1Title;
        private Button btnCommon1Result;
        private ProgressBar pbCommon1Progress;
        private ToolStripStatusLabel tsslSafeMode;
        private Button btnPack;
        private ComboBox cmbWifi1Port;
        private ComboBox cmbCsk1Port;
        private ToolStripStatusLabel tsslWorkingMode;
    }
}