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
            this.gbSettings.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.btnFlash.Enabled = false;
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 1042);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1722, 1074);
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
    }
}