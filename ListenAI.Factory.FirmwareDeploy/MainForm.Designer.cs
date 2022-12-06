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
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.btnFlash = new System.Windows.Forms.Button();
            this.btnFwSelect = new System.Windows.Forms.Button();
            this.btnMES = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslCurrentFirmware = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbMod1 = new System.Windows.Forms.GroupBox();
            this.panelResult1 = new System.Windows.Forms.Panel();
            this.pbCsk1Progress = new System.Windows.Forms.ProgressBar();
            this.btnCommon1Result = new System.Windows.Forms.Button();
            this.lbCommon1Title = new System.Windows.Forms.Label();
            this.panelSn1 = new System.Windows.Forms.Panel();
            this.tbCommon1Serial = new System.Windows.Forms.TextBox();
            this.lbCommon1TitleSn = new System.Windows.Forms.Label();
            this.panelWifi1 = new System.Windows.Forms.Panel();
            this.cbWifi1Default = new System.Windows.Forms.CheckBox();
            this.lbWifi1TitleDefault = new System.Windows.Forms.Label();
            this.tbWifi1Stopbits = new System.Windows.Forms.TextBox();
            this.lbWifi1TitleStopbit = new System.Windows.Forms.Label();
            this.tbWifi1Parity = new System.Windows.Forms.TextBox();
            this.lbWifi1TitleChecksum = new System.Windows.Forms.Label();
            this.tbWifi1Databits = new System.Windows.Forms.TextBox();
            this.lbWifi1TitleDatabit = new System.Windows.Forms.Label();
            this.tbWifi1BaudRate = new System.Windows.Forms.TextBox();
            this.lbWifi1TitleBaudRate = new System.Windows.Forms.Label();
            this.tbWifi1Port = new System.Windows.Forms.TextBox();
            this.lbWifi1TitlePort = new System.Windows.Forms.Label();
            this.lbWifi1Title = new System.Windows.Forms.Label();
            this.panelCsk1 = new System.Windows.Forms.Panel();
            this.cbCsk1Default = new System.Windows.Forms.CheckBox();
            this.lbCsk1TitleDefault = new System.Windows.Forms.Label();
            this.tbCsk1Stopbits = new System.Windows.Forms.TextBox();
            this.lbCsk1TitleStopbit = new System.Windows.Forms.Label();
            this.tbCsk1Parity = new System.Windows.Forms.TextBox();
            this.lbCsk1TitleChecksum = new System.Windows.Forms.Label();
            this.tbCsk1Databits = new System.Windows.Forms.TextBox();
            this.lbCsk1TitleDatabit = new System.Windows.Forms.Label();
            this.tbCsk1BaudRate = new System.Windows.Forms.TextBox();
            this.lbCsk1TitleBaudRate = new System.Windows.Forms.Label();
            this.tbCsk1Port = new System.Windows.Forms.TextBox();
            this.lbCsk1TitlePort = new System.Windows.Forms.Label();
            this.lbCsk1Title = new System.Windows.Forms.Label();
            this.gbSettings.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.gbMod1.SuspendLayout();
            this.panelResult1.SuspendLayout();
            this.panelSn1.SuspendLayout();
            this.panelWifi1.SuspendLayout();
            this.panelCsk1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.btnFlash);
            this.gbSettings.Controls.Add(this.btnFwSelect);
            this.gbSettings.Controls.Add(this.btnMES);
            this.gbSettings.Location = new System.Drawing.Point(8, 7);
            this.gbSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbSettings.Size = new System.Drawing.Size(1267, 83);
            this.gbSettings.TabIndex = 2;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "烧录设置";
            // 
            // btnFlash
            // 
            this.btnFlash.BackColor = System.Drawing.SystemColors.Control;
            this.btnFlash.Location = new System.Drawing.Point(898, 27);
            this.btnFlash.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFlash.Name = "btnFlash";
            this.btnFlash.Size = new System.Drawing.Size(277, 41);
            this.btnFlash.TabIndex = 2;
            this.btnFlash.Text = "烧录";
            this.btnFlash.UseVisualStyleBackColor = false;
            this.btnFlash.Click += new System.EventHandler(this.btnFlash_Click);
            // 
            // btnFwSelect
            // 
            this.btnFwSelect.BackColor = System.Drawing.Color.Blue;
            this.btnFwSelect.Location = new System.Drawing.Point(477, 27);
            this.btnFwSelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFwSelect.Name = "btnFwSelect";
            this.btnFwSelect.Size = new System.Drawing.Size(277, 41);
            this.btnFwSelect.TabIndex = 1;
            this.btnFwSelect.Text = "浏览";
            this.btnFwSelect.UseVisualStyleBackColor = false;
            this.btnFwSelect.Click += new System.EventHandler(this.btnFwSelect_Click);
            // 
            // btnMES
            // 
            this.btnMES.BackColor = System.Drawing.Color.Blue;
            this.btnMES.Location = new System.Drawing.Point(57, 27);
            this.btnMES.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMES.Name = "btnMES";
            this.btnMES.Size = new System.Drawing.Size(277, 41);
            this.btnMES.TabIndex = 0;
            this.btnMES.Text = "MES记录";
            this.btnMES.UseVisualStyleBackColor = false;
            this.btnMES.Click += new System.EventHandler(this.btnMES_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCurrentFirmware});
            this.statusStrip1.Location = new System.Drawing.Point(0, 475);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1285, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslCurrentFirmware
            // 
            this.tsslCurrentFirmware.Name = "tsslCurrentFirmware";
            this.tsslCurrentFirmware.Size = new System.Drawing.Size(112, 17);
            this.tsslCurrentFirmware.Text = "当前固件: (未选定)";
            // 
            // gbMod1
            // 
            this.gbMod1.Controls.Add(this.panelResult1);
            this.gbMod1.Controls.Add(this.panelSn1);
            this.gbMod1.Controls.Add(this.panelWifi1);
            this.gbMod1.Controls.Add(this.panelCsk1);
            this.gbMod1.Location = new System.Drawing.Point(8, 94);
            this.gbMod1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbMod1.Name = "gbMod1";
            this.gbMod1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbMod1.Size = new System.Drawing.Size(314, 374);
            this.gbMod1.TabIndex = 4;
            this.gbMod1.TabStop = false;
            // 
            // panelResult1
            // 
            this.panelResult1.Controls.Add(this.pbCsk1Progress);
            this.panelResult1.Controls.Add(this.btnCommon1Result);
            this.panelResult1.Controls.Add(this.lbCommon1Title);
            this.panelResult1.Location = new System.Drawing.Point(12, 304);
            this.panelResult1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelResult1.Name = "panelResult1";
            this.panelResult1.Size = new System.Drawing.Size(290, 61);
            this.panelResult1.TabIndex = 3;
            // 
            // pbCsk1Progress
            // 
            this.pbCsk1Progress.Location = new System.Drawing.Point(25, 7);
            this.pbCsk1Progress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbCsk1Progress.Name = "pbCsk1Progress";
            this.pbCsk1Progress.Size = new System.Drawing.Size(226, 19);
            this.pbCsk1Progress.Step = 1;
            this.pbCsk1Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbCsk1Progress.TabIndex = 5;
            // 
            // btnCommon1Result
            // 
            this.btnCommon1Result.BackColor = System.Drawing.SystemColors.Control;
            this.btnCommon1Result.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCommon1Result.Location = new System.Drawing.Point(119, 32);
            this.btnCommon1Result.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCommon1Result.Name = "btnCommon1Result";
            this.btnCommon1Result.Size = new System.Drawing.Size(132, 20);
            this.btnCommon1Result.TabIndex = 1;
            this.btnCommon1Result.Text = "Pass/Fail";
            this.btnCommon1Result.UseVisualStyleBackColor = false;
            // 
            // lbCommon1Title
            // 
            this.lbCommon1Title.AutoSize = true;
            this.lbCommon1Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbCommon1Title.Location = new System.Drawing.Point(24, 35);
            this.lbCommon1Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCommon1Title.Name = "lbCommon1Title";
            this.lbCommon1Title.Size = new System.Drawing.Size(64, 21);
            this.lbCommon1Title.TabIndex = 0;
            this.lbCommon1Title.Text = "模组一";
            this.lbCommon1Title.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panelSn1
            // 
            this.panelSn1.Controls.Add(this.tbCommon1Serial);
            this.panelSn1.Controls.Add(this.lbCommon1TitleSn);
            this.panelSn1.Location = new System.Drawing.Point(12, 266);
            this.panelSn1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelSn1.Name = "panelSn1";
            this.panelSn1.Size = new System.Drawing.Size(290, 35);
            this.panelSn1.TabIndex = 2;
            // 
            // tbCommon1Serial
            // 
            this.tbCommon1Serial.Location = new System.Drawing.Point(98, 7);
            this.tbCommon1Serial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbCommon1Serial.MaxLength = 13;
            this.tbCommon1Serial.Name = "tbCommon1Serial";
            this.tbCommon1Serial.Size = new System.Drawing.Size(154, 23);
            this.tbCommon1Serial.TabIndex = 6;
            // 
            // lbCommon1TitleSn
            // 
            this.lbCommon1TitleSn.AutoSize = true;
            this.lbCommon1TitleSn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbCommon1TitleSn.Location = new System.Drawing.Point(12, 10);
            this.lbCommon1TitleSn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCommon1TitleSn.Name = "lbCommon1TitleSn";
            this.lbCommon1TitleSn.Size = new System.Drawing.Size(77, 15);
            this.lbCommon1TitleSn.TabIndex = 5;
            this.lbCommon1TitleSn.Text = "产品序列号";
            // 
            // panelWifi1
            // 
            this.panelWifi1.Controls.Add(this.cbWifi1Default);
            this.panelWifi1.Controls.Add(this.lbWifi1TitleDefault);
            this.panelWifi1.Controls.Add(this.tbWifi1Stopbits);
            this.panelWifi1.Controls.Add(this.lbWifi1TitleStopbit);
            this.panelWifi1.Controls.Add(this.tbWifi1Parity);
            this.panelWifi1.Controls.Add(this.lbWifi1TitleChecksum);
            this.panelWifi1.Controls.Add(this.tbWifi1Databits);
            this.panelWifi1.Controls.Add(this.lbWifi1TitleDatabit);
            this.panelWifi1.Controls.Add(this.tbWifi1BaudRate);
            this.panelWifi1.Controls.Add(this.lbWifi1TitleBaudRate);
            this.panelWifi1.Controls.Add(this.tbWifi1Port);
            this.panelWifi1.Controls.Add(this.lbWifi1TitlePort);
            this.panelWifi1.Controls.Add(this.lbWifi1Title);
            this.panelWifi1.Location = new System.Drawing.Point(159, 18);
            this.panelWifi1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelWifi1.Name = "panelWifi1";
            this.panelWifi1.Size = new System.Drawing.Size(143, 242);
            this.panelWifi1.TabIndex = 1;
            // 
            // cbWifi1Default
            // 
            this.cbWifi1Default.AutoSize = true;
            this.cbWifi1Default.Location = new System.Drawing.Point(90, 217);
            this.cbWifi1Default.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbWifi1Default.Name = "cbWifi1Default";
            this.cbWifi1Default.Size = new System.Drawing.Size(15, 14);
            this.cbWifi1Default.TabIndex = 24;
            this.cbWifi1Default.UseVisualStyleBackColor = true;
            // 
            // lbWifi1TitleDefault
            // 
            this.lbWifi1TitleDefault.AutoSize = true;
            this.lbWifi1TitleDefault.Location = new System.Drawing.Point(15, 215);
            this.lbWifi1TitleDefault.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbWifi1TitleDefault.Name = "lbWifi1TitleDefault";
            this.lbWifi1TitleDefault.Size = new System.Drawing.Size(59, 15);
            this.lbWifi1TitleDefault.TabIndex = 23;
            this.lbWifi1TitleDefault.Text = "是否默认";
            // 
            // tbWifi1Stopbits
            // 
            this.tbWifi1Stopbits.Location = new System.Drawing.Point(74, 182);
            this.tbWifi1Stopbits.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbWifi1Stopbits.MaxLength = 1;
            this.tbWifi1Stopbits.Name = "tbWifi1Stopbits";
            this.tbWifi1Stopbits.ReadOnly = true;
            this.tbWifi1Stopbits.Size = new System.Drawing.Size(57, 23);
            this.tbWifi1Stopbits.TabIndex = 22;
            this.tbWifi1Stopbits.Text = "1";
            // 
            // lbWifi1TitleStopbit
            // 
            this.lbWifi1TitleStopbit.AutoSize = true;
            this.lbWifi1TitleStopbit.Location = new System.Drawing.Point(15, 182);
            this.lbWifi1TitleStopbit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbWifi1TitleStopbit.Name = "lbWifi1TitleStopbit";
            this.lbWifi1TitleStopbit.Size = new System.Drawing.Size(46, 15);
            this.lbWifi1TitleStopbit.TabIndex = 21;
            this.lbWifi1TitleStopbit.Text = "停止位";
            // 
            // tbWifi1Parity
            // 
            this.tbWifi1Parity.Location = new System.Drawing.Point(74, 148);
            this.tbWifi1Parity.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbWifi1Parity.MaxLength = 1;
            this.tbWifi1Parity.Name = "tbWifi1Parity";
            this.tbWifi1Parity.ReadOnly = true;
            this.tbWifi1Parity.Size = new System.Drawing.Size(57, 23);
            this.tbWifi1Parity.TabIndex = 20;
            this.tbWifi1Parity.Text = "0";
            // 
            // lbWifi1TitleChecksum
            // 
            this.lbWifi1TitleChecksum.AutoSize = true;
            this.lbWifi1TitleChecksum.Location = new System.Drawing.Point(15, 148);
            this.lbWifi1TitleChecksum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbWifi1TitleChecksum.Name = "lbWifi1TitleChecksum";
            this.lbWifi1TitleChecksum.Size = new System.Drawing.Size(46, 15);
            this.lbWifi1TitleChecksum.TabIndex = 19;
            this.lbWifi1TitleChecksum.Text = "校验位";
            // 
            // tbWifi1Databits
            // 
            this.tbWifi1Databits.Location = new System.Drawing.Point(74, 115);
            this.tbWifi1Databits.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbWifi1Databits.MaxLength = 1;
            this.tbWifi1Databits.Name = "tbWifi1Databits";
            this.tbWifi1Databits.ReadOnly = true;
            this.tbWifi1Databits.Size = new System.Drawing.Size(57, 23);
            this.tbWifi1Databits.TabIndex = 18;
            this.tbWifi1Databits.Text = "8";
            // 
            // lbWifi1TitleDatabit
            // 
            this.lbWifi1TitleDatabit.AutoSize = true;
            this.lbWifi1TitleDatabit.Location = new System.Drawing.Point(15, 117);
            this.lbWifi1TitleDatabit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbWifi1TitleDatabit.Name = "lbWifi1TitleDatabit";
            this.lbWifi1TitleDatabit.Size = new System.Drawing.Size(46, 15);
            this.lbWifi1TitleDatabit.TabIndex = 17;
            this.lbWifi1TitleDatabit.Text = "数据位";
            // 
            // tbWifi1BaudRate
            // 
            this.tbWifi1BaudRate.Location = new System.Drawing.Point(74, 83);
            this.tbWifi1BaudRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbWifi1BaudRate.MaxLength = 7;
            this.tbWifi1BaudRate.Name = "tbWifi1BaudRate";
            this.tbWifi1BaudRate.Size = new System.Drawing.Size(57, 23);
            this.tbWifi1BaudRate.TabIndex = 16;
            this.tbWifi1BaudRate.Text = "1000000";
            // 
            // lbWifi1TitleBaudRate
            // 
            this.lbWifi1TitleBaudRate.AutoSize = true;
            this.lbWifi1TitleBaudRate.Location = new System.Drawing.Point(15, 87);
            this.lbWifi1TitleBaudRate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbWifi1TitleBaudRate.Name = "lbWifi1TitleBaudRate";
            this.lbWifi1TitleBaudRate.Size = new System.Drawing.Size(46, 15);
            this.lbWifi1TitleBaudRate.TabIndex = 15;
            this.lbWifi1TitleBaudRate.Text = "波特率";
            // 
            // tbWifi1Port
            // 
            this.tbWifi1Port.Location = new System.Drawing.Point(74, 53);
            this.tbWifi1Port.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbWifi1Port.MaxLength = 6;
            this.tbWifi1Port.Name = "tbWifi1Port";
            this.tbWifi1Port.Size = new System.Drawing.Size(57, 23);
            this.tbWifi1Port.TabIndex = 14;
            this.tbWifi1Port.Text = "COM";
            // 
            // lbWifi1TitlePort
            // 
            this.lbWifi1TitlePort.AutoSize = true;
            this.lbWifi1TitlePort.Location = new System.Drawing.Point(28, 55);
            this.lbWifi1TitlePort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbWifi1TitlePort.Name = "lbWifi1TitlePort";
            this.lbWifi1TitlePort.Size = new System.Drawing.Size(33, 15);
            this.lbWifi1TitlePort.TabIndex = 13;
            this.lbWifi1TitlePort.Text = "串口";
            // 
            // lbWifi1Title
            // 
            this.lbWifi1Title.AutoSize = true;
            this.lbWifi1Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbWifi1Title.Location = new System.Drawing.Point(15, 16);
            this.lbWifi1Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbWifi1Title.Name = "lbWifi1Title";
            this.lbWifi1Title.Size = new System.Drawing.Size(116, 21);
            this.lbWifi1Title.TabIndex = 0;
            this.lbWifi1Title.Text = "WIFI串口烧录";
            // 
            // panelCsk1
            // 
            this.panelCsk1.Controls.Add(this.cbCsk1Default);
            this.panelCsk1.Controls.Add(this.lbCsk1TitleDefault);
            this.panelCsk1.Controls.Add(this.tbCsk1Stopbits);
            this.panelCsk1.Controls.Add(this.lbCsk1TitleStopbit);
            this.panelCsk1.Controls.Add(this.tbCsk1Parity);
            this.panelCsk1.Controls.Add(this.lbCsk1TitleChecksum);
            this.panelCsk1.Controls.Add(this.tbCsk1Databits);
            this.panelCsk1.Controls.Add(this.lbCsk1TitleDatabit);
            this.panelCsk1.Controls.Add(this.tbCsk1BaudRate);
            this.panelCsk1.Controls.Add(this.lbCsk1TitleBaudRate);
            this.panelCsk1.Controls.Add(this.tbCsk1Port);
            this.panelCsk1.Controls.Add(this.lbCsk1TitlePort);
            this.panelCsk1.Controls.Add(this.lbCsk1Title);
            this.panelCsk1.Location = new System.Drawing.Point(12, 18);
            this.panelCsk1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelCsk1.Name = "panelCsk1";
            this.panelCsk1.Size = new System.Drawing.Size(143, 242);
            this.panelCsk1.TabIndex = 0;
            // 
            // cbCsk1Default
            // 
            this.cbCsk1Default.AutoSize = true;
            this.cbCsk1Default.Location = new System.Drawing.Point(85, 217);
            this.cbCsk1Default.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbCsk1Default.Name = "cbCsk1Default";
            this.cbCsk1Default.Size = new System.Drawing.Size(15, 14);
            this.cbCsk1Default.TabIndex = 12;
            this.cbCsk1Default.UseVisualStyleBackColor = true;
            // 
            // lbCsk1TitleDefault
            // 
            this.lbCsk1TitleDefault.AutoSize = true;
            this.lbCsk1TitleDefault.Location = new System.Drawing.Point(12, 215);
            this.lbCsk1TitleDefault.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCsk1TitleDefault.Name = "lbCsk1TitleDefault";
            this.lbCsk1TitleDefault.Size = new System.Drawing.Size(59, 15);
            this.lbCsk1TitleDefault.TabIndex = 11;
            this.lbCsk1TitleDefault.Text = "是否默认";
            // 
            // tbCsk1Stopbits
            // 
            this.tbCsk1Stopbits.Location = new System.Drawing.Point(71, 182);
            this.tbCsk1Stopbits.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbCsk1Stopbits.MaxLength = 1;
            this.tbCsk1Stopbits.Name = "tbCsk1Stopbits";
            this.tbCsk1Stopbits.ReadOnly = true;
            this.tbCsk1Stopbits.Size = new System.Drawing.Size(57, 23);
            this.tbCsk1Stopbits.TabIndex = 10;
            this.tbCsk1Stopbits.Text = "1";
            // 
            // lbCsk1TitleStopbit
            // 
            this.lbCsk1TitleStopbit.AutoSize = true;
            this.lbCsk1TitleStopbit.Location = new System.Drawing.Point(12, 182);
            this.lbCsk1TitleStopbit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCsk1TitleStopbit.Name = "lbCsk1TitleStopbit";
            this.lbCsk1TitleStopbit.Size = new System.Drawing.Size(46, 15);
            this.lbCsk1TitleStopbit.TabIndex = 9;
            this.lbCsk1TitleStopbit.Text = "停止位";
            // 
            // tbCsk1Parity
            // 
            this.tbCsk1Parity.Location = new System.Drawing.Point(71, 148);
            this.tbCsk1Parity.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbCsk1Parity.MaxLength = 1;
            this.tbCsk1Parity.Name = "tbCsk1Parity";
            this.tbCsk1Parity.ReadOnly = true;
            this.tbCsk1Parity.Size = new System.Drawing.Size(57, 23);
            this.tbCsk1Parity.TabIndex = 8;
            this.tbCsk1Parity.Text = "0";
            // 
            // lbCsk1TitleChecksum
            // 
            this.lbCsk1TitleChecksum.AutoSize = true;
            this.lbCsk1TitleChecksum.Location = new System.Drawing.Point(12, 148);
            this.lbCsk1TitleChecksum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCsk1TitleChecksum.Name = "lbCsk1TitleChecksum";
            this.lbCsk1TitleChecksum.Size = new System.Drawing.Size(46, 15);
            this.lbCsk1TitleChecksum.TabIndex = 7;
            this.lbCsk1TitleChecksum.Text = "校验位";
            // 
            // tbCsk1Databits
            // 
            this.tbCsk1Databits.Location = new System.Drawing.Point(71, 115);
            this.tbCsk1Databits.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbCsk1Databits.MaxLength = 1;
            this.tbCsk1Databits.Name = "tbCsk1Databits";
            this.tbCsk1Databits.ReadOnly = true;
            this.tbCsk1Databits.Size = new System.Drawing.Size(57, 23);
            this.tbCsk1Databits.TabIndex = 6;
            this.tbCsk1Databits.Text = "8";
            // 
            // lbCsk1TitleDatabit
            // 
            this.lbCsk1TitleDatabit.AutoSize = true;
            this.lbCsk1TitleDatabit.Location = new System.Drawing.Point(12, 117);
            this.lbCsk1TitleDatabit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCsk1TitleDatabit.Name = "lbCsk1TitleDatabit";
            this.lbCsk1TitleDatabit.Size = new System.Drawing.Size(46, 15);
            this.lbCsk1TitleDatabit.TabIndex = 5;
            this.lbCsk1TitleDatabit.Text = "数据位";
            // 
            // tbCsk1BaudRate
            // 
            this.tbCsk1BaudRate.Location = new System.Drawing.Point(71, 83);
            this.tbCsk1BaudRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbCsk1BaudRate.MaxLength = 7;
            this.tbCsk1BaudRate.Name = "tbCsk1BaudRate";
            this.tbCsk1BaudRate.Size = new System.Drawing.Size(57, 23);
            this.tbCsk1BaudRate.TabIndex = 4;
            this.tbCsk1BaudRate.Text = "3000000";
            // 
            // lbCsk1TitleBaudRate
            // 
            this.lbCsk1TitleBaudRate.AutoSize = true;
            this.lbCsk1TitleBaudRate.Location = new System.Drawing.Point(12, 85);
            this.lbCsk1TitleBaudRate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCsk1TitleBaudRate.Name = "lbCsk1TitleBaudRate";
            this.lbCsk1TitleBaudRate.Size = new System.Drawing.Size(46, 15);
            this.lbCsk1TitleBaudRate.TabIndex = 3;
            this.lbCsk1TitleBaudRate.Text = "波特率";
            // 
            // tbCsk1Port
            // 
            this.tbCsk1Port.Location = new System.Drawing.Point(71, 53);
            this.tbCsk1Port.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbCsk1Port.MaxLength = 6;
            this.tbCsk1Port.Name = "tbCsk1Port";
            this.tbCsk1Port.Size = new System.Drawing.Size(57, 23);
            this.tbCsk1Port.TabIndex = 2;
            this.tbCsk1Port.Text = "COM";
            // 
            // lbCsk1TitlePort
            // 
            this.lbCsk1TitlePort.AutoSize = true;
            this.lbCsk1TitlePort.Location = new System.Drawing.Point(25, 55);
            this.lbCsk1TitlePort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCsk1TitlePort.Name = "lbCsk1TitlePort";
            this.lbCsk1TitlePort.Size = new System.Drawing.Size(33, 15);
            this.lbCsk1TitlePort.TabIndex = 1;
            this.lbCsk1TitlePort.Text = "串口";
            // 
            // lbCsk1Title
            // 
            this.lbCsk1Title.AutoSize = true;
            this.lbCsk1Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbCsk1Title.Location = new System.Drawing.Point(12, 16);
            this.lbCsk1Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCsk1Title.Name = "lbCsk1Title";
            this.lbCsk1Title.Size = new System.Drawing.Size(111, 21);
            this.lbCsk1Title.TabIndex = 0;
            this.lbCsk1Title.Text = "CSK串口烧录";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 497);
            this.Controls.Add(this.gbMod1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "聆思模组烧录工具 v1.0.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbSettings.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbMod1.ResumeLayout(false);
            this.panelResult1.ResumeLayout(false);
            this.panelResult1.PerformLayout();
            this.panelSn1.ResumeLayout(false);
            this.panelSn1.PerformLayout();
            this.panelWifi1.ResumeLayout(false);
            this.panelWifi1.PerformLayout();
            this.panelCsk1.ResumeLayout(false);
            this.panelCsk1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private TextBox tbCsk1Port;
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
        private CheckBox cbWifi1Default;
        private Label lbWifi1TitleDefault;
        private TextBox tbWifi1Stopbits;
        private Label lbWifi1TitleStopbit;
        private TextBox tbWifi1Parity;
        private Label lbWifi1TitleChecksum;
        private TextBox tbWifi1Databits;
        private Label lbWifi1TitleDatabit;
        private TextBox tbWifi1BaudRate;
        private Label lbWifi1TitleBaudRate;
        private TextBox tbWifi1Port;
        private Label lbWifi1TitlePort;
        private CheckBox cbCsk1Default;
        private TextBox tbCommon1Serial;
        private Label lbCommon1TitleSn;
        private Panel panelResult1;
        private Label lbCommon1Title;
        private Button btnCommon1Result;
        private ProgressBar pbCsk1Progress;
    }
}