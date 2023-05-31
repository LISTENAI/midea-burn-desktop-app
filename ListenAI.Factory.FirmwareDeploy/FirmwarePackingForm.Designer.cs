namespace ListenAI.Factory.FirmwareDeploy {
    partial class FirmwarePackingForm {
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
            gbCsk6 = new GroupBox();
            btnCskFwPathSelect = new Button();
            tbCskFwPath = new TextBox();
            label2 = new Label();
            tbCskFwVer = new TextBox();
            label1 = new Label();
            gbAsr = new GroupBox();
            btnAsrFwPathSelect = new Button();
            tbAsrFwPath = new TextBox();
            label3 = new Label();
            tbAsrFwVer = new TextBox();
            label4 = new Label();
            groupBox1 = new GroupBox();
            btnPack = new Button();
            btnPackFwPathSelect = new Button();
            tbPackFwPath = new TextBox();
            label5 = new Label();
            tbPackFwVer = new TextBox();
            label6 = new Label();
            statusStrip1 = new StatusStrip();
            tsslStatus = new ToolStripStatusLabel();
            gbCsk6.SuspendLayout();
            gbAsr.SuspendLayout();
            groupBox1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // gbCsk6
            // 
            gbCsk6.Controls.Add(btnCskFwPathSelect);
            gbCsk6.Controls.Add(tbCskFwPath);
            gbCsk6.Controls.Add(label2);
            gbCsk6.Controls.Add(tbCskFwVer);
            gbCsk6.Controls.Add(label1);
            gbCsk6.Location = new Point(12, 12);
            gbCsk6.Name = "gbCsk6";
            gbCsk6.Size = new Size(523, 148);
            gbCsk6.TabIndex = 0;
            gbCsk6.TabStop = false;
            gbCsk6.Text = "CSK6固件设置";
            // 
            // btnCskFwPathSelect
            // 
            btnCskFwPathSelect.Location = new Point(406, 86);
            btnCskFwPathSelect.Name = "btnCskFwPathSelect";
            btnCskFwPathSelect.Size = new Size(94, 29);
            btnCskFwPathSelect.TabIndex = 4;
            btnCskFwPathSelect.Text = "选择";
            btnCskFwPathSelect.UseVisualStyleBackColor = true;
            btnCskFwPathSelect.Click += btnCskFwPathSelect_Click;
            // 
            // tbCskFwPath
            // 
            tbCskFwPath.Location = new Point(139, 87);
            tbCskFwPath.Name = "tbCskFwPath";
            tbCskFwPath.ReadOnly = true;
            tbCskFwPath.Size = new Size(246, 27);
            tbCskFwPath.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 90);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 2;
            label2.Text = "固件路径：";
            // 
            // tbCskFwVer
            // 
            tbCskFwVer.Location = new Point(139, 36);
            tbCskFwVer.Name = "tbCskFwVer";
            tbCskFwVer.PlaceholderText = "0.0.0";
            tbCskFwVer.Size = new Size(246, 27);
            tbCskFwVer.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 39);
            label1.Name = "label1";
            label1.Size = new Size(105, 20);
            label1.TabIndex = 0;
            label1.Text = "固件版本号：";
            // 
            // gbAsr
            // 
            gbAsr.Controls.Add(btnAsrFwPathSelect);
            gbAsr.Controls.Add(tbAsrFwPath);
            gbAsr.Controls.Add(label3);
            gbAsr.Controls.Add(tbAsrFwVer);
            gbAsr.Controls.Add(label4);
            gbAsr.Location = new Point(12, 166);
            gbAsr.Name = "gbAsr";
            gbAsr.Size = new Size(523, 148);
            gbAsr.TabIndex = 1;
            gbAsr.TabStop = false;
            gbAsr.Text = "ASR固件设置";
            gbAsr.Visible = false;
            // 
            // btnAsrFwPathSelect
            // 
            btnAsrFwPathSelect.Enabled = false;
            btnAsrFwPathSelect.Location = new Point(406, 86);
            btnAsrFwPathSelect.Name = "btnAsrFwPathSelect";
            btnAsrFwPathSelect.Size = new Size(94, 29);
            btnAsrFwPathSelect.TabIndex = 4;
            btnAsrFwPathSelect.Text = "选择";
            btnAsrFwPathSelect.UseVisualStyleBackColor = true;
            btnAsrFwPathSelect.Click += btnAsrFwPathSelect_Click;
            // 
            // tbAsrFwPath
            // 
            tbAsrFwPath.Location = new Point(139, 87);
            tbAsrFwPath.Name = "tbAsrFwPath";
            tbAsrFwPath.ReadOnly = true;
            tbAsrFwPath.Size = new Size(246, 27);
            tbAsrFwPath.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 90);
            label3.Name = "label3";
            label3.Size = new Size(89, 20);
            label3.TabIndex = 2;
            label3.Text = "固件路径：";
            // 
            // tbAsrFwVer
            // 
            tbAsrFwVer.Enabled = false;
            tbAsrFwVer.Location = new Point(139, 36);
            tbAsrFwVer.Name = "tbAsrFwVer";
            tbAsrFwVer.PlaceholderText = "0.0.0";
            tbAsrFwVer.Size = new Size(246, 27);
            tbAsrFwVer.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(28, 39);
            label4.Name = "label4";
            label4.Size = new Size(105, 20);
            label4.TabIndex = 0;
            label4.Text = "固件版本号：";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnPack);
            groupBox1.Controls.Add(btnPackFwPathSelect);
            groupBox1.Controls.Add(tbPackFwPath);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(tbPackFwVer);
            groupBox1.Controls.Add(label6);
            groupBox1.Location = new Point(12, 320);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(523, 184);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "CSK6离在线固件打包";
            // 
            // btnPack
            // 
            btnPack.Location = new Point(28, 134);
            btnPack.Name = "btnPack";
            btnPack.Size = new Size(94, 29);
            btnPack.TabIndex = 5;
            btnPack.Text = "打包";
            btnPack.UseVisualStyleBackColor = true;
            btnPack.Click += btnPack_Click;
            // 
            // btnPackFwPathSelect
            // 
            btnPackFwPathSelect.Location = new Point(406, 86);
            btnPackFwPathSelect.Name = "btnPackFwPathSelect";
            btnPackFwPathSelect.Size = new Size(94, 29);
            btnPackFwPathSelect.TabIndex = 4;
            btnPackFwPathSelect.Text = "选择";
            btnPackFwPathSelect.UseVisualStyleBackColor = true;
            btnPackFwPathSelect.Click += btnPackFwPathSelect_Click;
            // 
            // tbPackFwPath
            // 
            tbPackFwPath.Location = new Point(139, 87);
            tbPackFwPath.Name = "tbPackFwPath";
            tbPackFwPath.ReadOnly = true;
            tbPackFwPath.Size = new Size(246, 27);
            tbPackFwPath.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(44, 90);
            label5.Name = "label5";
            label5.Size = new Size(89, 20);
            label5.TabIndex = 2;
            label5.Text = "保存地址：";
            // 
            // tbPackFwVer
            // 
            tbPackFwVer.Location = new Point(139, 36);
            tbPackFwVer.Name = "tbPackFwVer";
            tbPackFwVer.PlaceholderText = "0.0.0";
            tbPackFwVer.Size = new Size(246, 27);
            tbPackFwVer.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(28, 39);
            label6.Name = "label6";
            label6.Size = new Size(105, 20);
            label6.TabIndex = 0;
            label6.Text = "固件版本号：";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsslStatus });
            statusStrip1.Location = new Point(0, 525);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(547, 26);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            tsslStatus.Name = "tsslStatus";
            tsslStatus.Size = new Size(121, 20);
            tsslStatus.Text = "当前状态：空闲";
            // 
            // FirmwarePackingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(547, 551);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox1);
            Controls.Add(gbAsr);
            Controls.Add(gbCsk6);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FirmwarePackingForm";
            Text = "固件打包";
            Load += FirmwarePackingForm_Load;
            gbCsk6.ResumeLayout(false);
            gbCsk6.PerformLayout();
            gbAsr.ResumeLayout(false);
            gbAsr.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gbCsk6;
        private Label label1;
        private TextBox tbCskFwPath;
        private Label label2;
        private TextBox tbCskFwVer;
        private Button btnCskFwPathSelect;
        private GroupBox gbAsr;
        private Button btnAsrFwPathSelect;
        private TextBox tbAsrFwPath;
        private Label label3;
        private TextBox tbAsrFwVer;
        private Label label4;
        private GroupBox groupBox1;
        private Button btnPack;
        private Button btnPackFwPathSelect;
        private TextBox tbPackFwPath;
        private Label label5;
        private TextBox tbPackFwVer;
        private Label label6;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsslStatus;
    }
}