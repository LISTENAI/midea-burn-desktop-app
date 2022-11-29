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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelWifiBurn1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelCskBurn1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCsk1Port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCsk1BaudRate = new System.Windows.Forms.TextBox();
            this.tbCsk1Checksum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbCsk1Databit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCsk1Stopbit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbCsk1Default = new System.Windows.Forms.CheckBox();
            this.cbWifi1Default = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbWifi1Stopbit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbWifi1Checksum = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbWifi1Databit = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbWifi1BaudRate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbWifi1Port = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbCsk1Serial = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.tbCsk1Result = new System.Windows.Forms.Button();
            this.gbSettings.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.gbMod1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelWifiBurn1.SuspendLayout();
            this.panelCskBurn1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.btnFlash);
            this.gbSettings.Controls.Add(this.btnFwSelect);
            this.gbSettings.Controls.Add(this.btnMES);
            this.gbSettings.Location = new System.Drawing.Point(12, 12);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(1693, 139);
            this.gbSettings.TabIndex = 2;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "烧录设置";
            // 
            // btnFlash
            // 
            this.btnFlash.BackColor = System.Drawing.Color.Red;
            this.btnFlash.Location = new System.Drawing.Point(1178, 45);
            this.btnFlash.Name = "btnFlash";
            this.btnFlash.Size = new System.Drawing.Size(396, 69);
            this.btnFlash.TabIndex = 2;
            this.btnFlash.Text = "烧录";
            this.btnFlash.UseVisualStyleBackColor = false;
            this.btnFlash.Click += new System.EventHandler(this.btnFlash_Click);
            // 
            // btnFwSelect
            // 
            this.btnFwSelect.BackColor = System.Drawing.Color.Red;
            this.btnFwSelect.Location = new System.Drawing.Point(643, 45);
            this.btnFwSelect.Name = "btnFwSelect";
            this.btnFwSelect.Size = new System.Drawing.Size(396, 69);
            this.btnFwSelect.TabIndex = 1;
            this.btnFwSelect.Text = "浏览";
            this.btnFwSelect.UseVisualStyleBackColor = false;
            this.btnFwSelect.Click += new System.EventHandler(this.btnFwSelect_Click);
            // 
            // btnMES
            // 
            this.btnMES.Enabled = false;
            this.btnMES.Location = new System.Drawing.Point(81, 45);
            this.btnMES.Name = "btnMES";
            this.btnMES.Size = new System.Drawing.Size(396, 69);
            this.btnMES.TabIndex = 0;
            this.btnMES.Text = "MES记录";
            this.btnMES.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCurrentFirmware});
            this.statusStrip1.Location = new System.Drawing.Point(0, 769);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1722, 32);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslCurrentFirmware
            // 
            this.tsslCurrentFirmware.Name = "tsslCurrentFirmware";
            this.tsslCurrentFirmware.Size = new System.Drawing.Size(164, 25);
            this.tsslCurrentFirmware.Text = "当前固件: (未选定)";
            // 
            // gbMod1
            // 
            this.gbMod1.Controls.Add(this.panel1);
            this.gbMod1.Controls.Add(this.panel2);
            this.gbMod1.Controls.Add(this.panelWifiBurn1);
            this.gbMod1.Controls.Add(this.panelCskBurn1);
            this.gbMod1.Location = new System.Drawing.Point(12, 157);
            this.gbMod1.Name = "gbMod1";
            this.gbMod1.Size = new System.Drawing.Size(506, 594);
            this.gbMod1.TabIndex = 4;
            this.gbMod1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbCsk1Serial);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Location = new System.Drawing.Point(17, 445);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(468, 58);
            this.panel2.TabIndex = 2;
            // 
            // panelWifiBurn1
            // 
            this.panelWifiBurn1.Controls.Add(this.cbWifi1Default);
            this.panelWifiBurn1.Controls.Add(this.label9);
            this.panelWifiBurn1.Controls.Add(this.tbWifi1Stopbit);
            this.panelWifiBurn1.Controls.Add(this.label10);
            this.panelWifiBurn1.Controls.Add(this.tbWifi1Checksum);
            this.panelWifiBurn1.Controls.Add(this.label11);
            this.panelWifiBurn1.Controls.Add(this.tbWifi1Databit);
            this.panelWifiBurn1.Controls.Add(this.label12);
            this.panelWifiBurn1.Controls.Add(this.tbWifi1BaudRate);
            this.panelWifiBurn1.Controls.Add(this.label13);
            this.panelWifiBurn1.Controls.Add(this.tbWifi1Port);
            this.panelWifiBurn1.Controls.Add(this.label14);
            this.panelWifiBurn1.Controls.Add(this.label2);
            this.panelWifiBurn1.Location = new System.Drawing.Point(254, 30);
            this.panelWifiBurn1.Name = "panelWifiBurn1";
            this.panelWifiBurn1.Size = new System.Drawing.Size(231, 409);
            this.panelWifiBurn1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(40, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 32);
            this.label2.TabIndex = 0;
            this.label2.Text = "WIFI串口烧录";
            // 
            // panelCskBurn1
            // 
            this.panelCskBurn1.Controls.Add(this.cbCsk1Default);
            this.panelCskBurn1.Controls.Add(this.label8);
            this.panelCskBurn1.Controls.Add(this.tbCsk1Stopbit);
            this.panelCskBurn1.Controls.Add(this.label7);
            this.panelCskBurn1.Controls.Add(this.tbCsk1Checksum);
            this.panelCskBurn1.Controls.Add(this.label5);
            this.panelCskBurn1.Controls.Add(this.tbCsk1Databit);
            this.panelCskBurn1.Controls.Add(this.label6);
            this.panelCskBurn1.Controls.Add(this.tbCsk1BaudRate);
            this.panelCskBurn1.Controls.Add(this.label4);
            this.panelCskBurn1.Controls.Add(this.tbCsk1Port);
            this.panelCskBurn1.Controls.Add(this.label3);
            this.panelCskBurn1.Controls.Add(this.label1);
            this.panelCskBurn1.Location = new System.Drawing.Point(17, 30);
            this.panelCskBurn1.Name = "panelCskBurn1";
            this.panelCskBurn1.Size = new System.Drawing.Size(231, 409);
            this.panelCskBurn1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(36, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "CSK串口烧录";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "串口";
            // 
            // tbCsk1Port
            // 
            this.tbCsk1Port.Location = new System.Drawing.Point(102, 88);
            this.tbCsk1Port.Name = "tbCsk1Port";
            this.tbCsk1Port.Size = new System.Drawing.Size(103, 31);
            this.tbCsk1Port.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "波特率";
            // 
            // tbCsk1BaudRate
            // 
            this.tbCsk1BaudRate.Location = new System.Drawing.Point(102, 139);
            this.tbCsk1BaudRate.Name = "tbCsk1BaudRate";
            this.tbCsk1BaudRate.Size = new System.Drawing.Size(103, 31);
            this.tbCsk1BaudRate.TabIndex = 4;
            // 
            // tbCsk1Checksum
            // 
            this.tbCsk1Checksum.Location = new System.Drawing.Point(102, 247);
            this.tbCsk1Checksum.Name = "tbCsk1Checksum";
            this.tbCsk1Checksum.Size = new System.Drawing.Size(103, 31);
            this.tbCsk1Checksum.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "校验位";
            // 
            // tbCsk1Databit
            // 
            this.tbCsk1Databit.Location = new System.Drawing.Point(102, 192);
            this.tbCsk1Databit.Name = "tbCsk1Databit";
            this.tbCsk1Databit.Size = new System.Drawing.Size(103, 31);
            this.tbCsk1Databit.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "数据位";
            // 
            // tbCsk1Stopbit
            // 
            this.tbCsk1Stopbit.Location = new System.Drawing.Point(102, 304);
            this.tbCsk1Stopbit.Name = "tbCsk1Stopbit";
            this.tbCsk1Stopbit.Size = new System.Drawing.Size(103, 31);
            this.tbCsk1Stopbit.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 304);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 25);
            this.label7.TabIndex = 9;
            this.label7.Text = "停止位";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 359);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 25);
            this.label8.TabIndex = 11;
            this.label8.Text = "是否默认";
            // 
            // cbCsk1Default
            // 
            this.cbCsk1Default.AutoSize = true;
            this.cbCsk1Default.Location = new System.Drawing.Point(148, 362);
            this.cbCsk1Default.Name = "cbCsk1Default";
            this.cbCsk1Default.Size = new System.Drawing.Size(22, 21);
            this.cbCsk1Default.TabIndex = 12;
            this.cbCsk1Default.UseVisualStyleBackColor = true;
            // 
            // cbWifi1Default
            // 
            this.cbWifi1Default.AutoSize = true;
            this.cbWifi1Default.Location = new System.Drawing.Point(152, 362);
            this.cbWifi1Default.Name = "cbWifi1Default";
            this.cbWifi1Default.Size = new System.Drawing.Size(22, 21);
            this.cbWifi1Default.TabIndex = 24;
            this.cbWifi1Default.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 359);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 25);
            this.label9.TabIndex = 23;
            this.label9.Text = "是否默认";
            // 
            // tbWifi1Stopbit
            // 
            this.tbWifi1Stopbit.Location = new System.Drawing.Point(106, 304);
            this.tbWifi1Stopbit.Name = "tbWifi1Stopbit";
            this.tbWifi1Stopbit.Size = new System.Drawing.Size(103, 31);
            this.tbWifi1Stopbit.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 304);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 25);
            this.label10.TabIndex = 21;
            this.label10.Text = "停止位";
            // 
            // tbWifi1Checksum
            // 
            this.tbWifi1Checksum.Location = new System.Drawing.Point(106, 247);
            this.tbWifi1Checksum.Name = "tbWifi1Checksum";
            this.tbWifi1Checksum.Size = new System.Drawing.Size(103, 31);
            this.tbWifi1Checksum.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 247);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 25);
            this.label11.TabIndex = 19;
            this.label11.Text = "校验位";
            // 
            // tbWifi1Databit
            // 
            this.tbWifi1Databit.Location = new System.Drawing.Point(106, 192);
            this.tbWifi1Databit.Name = "tbWifi1Databit";
            this.tbWifi1Databit.Size = new System.Drawing.Size(103, 31);
            this.tbWifi1Databit.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 195);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 25);
            this.label12.TabIndex = 17;
            this.label12.Text = "数据位";
            // 
            // tbWifi1BaudRate
            // 
            this.tbWifi1BaudRate.Location = new System.Drawing.Point(106, 139);
            this.tbWifi1BaudRate.Name = "tbWifi1BaudRate";
            this.tbWifi1BaudRate.Size = new System.Drawing.Size(103, 31);
            this.tbWifi1BaudRate.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 25);
            this.label13.TabIndex = 15;
            this.label13.Text = "波特率";
            // 
            // tbWifi1Port
            // 
            this.tbWifi1Port.Location = new System.Drawing.Point(106, 88);
            this.tbWifi1Port.Name = "tbWifi1Port";
            this.tbWifi1Port.Size = new System.Drawing.Size(103, 31);
            this.tbWifi1Port.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(40, 91);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 25);
            this.label14.TabIndex = 13;
            this.label14.Text = "串口";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(51, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 25);
            this.label15.TabIndex = 5;
            this.label15.Text = "产品序列号";
            // 
            // tbCsk1Serial
            // 
            this.tbCsk1Serial.Location = new System.Drawing.Point(196, 13);
            this.tbCsk1Serial.Name = "tbCsk1Serial";
            this.tbCsk1Serial.Size = new System.Drawing.Size(219, 31);
            this.tbCsk1Serial.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbCsk1Result);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Location = new System.Drawing.Point(17, 509);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 65);
            this.panel1.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label16.Location = new System.Drawing.Point(71, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(92, 32);
            this.label16.TabIndex = 0;
            this.label16.Text = "模组一";
            // 
            // tbCsk1Result
            // 
            this.tbCsk1Result.Location = new System.Drawing.Point(227, 14);
            this.tbCsk1Result.Name = "tbCsk1Result";
            this.tbCsk1Result.Size = new System.Drawing.Size(188, 34);
            this.tbCsk1Result.TabIndex = 1;
            this.tbCsk1Result.Text = "Pass/Fail";
            this.tbCsk1Result.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1722, 801);
            this.Controls.Add(this.gbMod1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "聆思模组烧录工具 v1.0.0";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbSettings.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbMod1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelWifiBurn1.ResumeLayout(false);
            this.panelWifiBurn1.PerformLayout();
            this.panelCskBurn1.ResumeLayout(false);
            this.panelCskBurn1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private Panel panelCskBurn1;
        private Label label1;
        private Panel panelWifiBurn1;
        private Label label2;
        private Panel panel2;
        private TextBox tbCsk1Port;
        private Label label3;
        private TextBox tbCsk1Stopbit;
        private Label label7;
        private TextBox tbCsk1Checksum;
        private Label label5;
        private TextBox tbCsk1Databit;
        private Label label6;
        private TextBox tbCsk1BaudRate;
        private Label label4;
        private Label label8;
        private CheckBox cbWifi1Default;
        private Label label9;
        private TextBox tbWifi1Stopbit;
        private Label label10;
        private TextBox tbWifi1Checksum;
        private Label label11;
        private TextBox tbWifi1Databit;
        private Label label12;
        private TextBox tbWifi1BaudRate;
        private Label label13;
        private TextBox tbWifi1Port;
        private Label label14;
        private CheckBox cbCsk1Default;
        private TextBox tbCsk1Serial;
        private Label label15;
        private Panel panel1;
        private Label label16;
        private Button tbCsk1Result;
    }
}