using ListenAI.Factory.FirmwareDeploy.Models;

namespace ListenAI.Factory.FirmwareDeploy {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            FormControlsInit();
            CenterToScreen();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (MessageBox.Show("确定要退出？", "退出前确定", MessageBoxButtons.YesNo) == DialogResult.No) {
                e.Cancel = true;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        /// <summary>
        /// Init all window controls
        /// </summary>
        private void FormControlsInit() {
            panelCsk1.BackColor = Constants.ColorCsk6PanelBackground;
            panelWifi1.BackColor = Constants.ColorWifiPanelBackground;
            CreateUiGroup(4);
        }

        /// <summary>
        /// Create module group
        /// </summary>
        /// <param name="count">groups count to create (1-4 only)</param>
        private void CreateUiGroup(ushort count) {
            if (count <= 1 || count > 4) {
                count = 1;
            }
            Utils.CtrlPropModify(gbMod1, 1);
            while (Global.GroupCount < count) {
                Global.GroupCount++;
                var clonedGrpbox = Utils.CloneControl(gbMod1);
                clonedGrpbox = Utils.CtrlPropModify(clonedGrpbox, Global.GroupCount);
                var locX = gbMod1.Location.X + 454 * (Global.GroupCount - 1);
                clonedGrpbox.Location = gbMod1.Location with { X = locX };
                this.Controls.Add(clonedGrpbox);
            }
        }

        private void btnFwSelect_Click(object sender, EventArgs e) {
            var ofd = new OpenFileDialog();
            ofd.Filter = "固件包 (*.zip)|*.zip";
            ofd.Multiselect = false;
            var result = ofd.ShowDialog();

            if (result != DialogResult.OK) {
                return;
            }

            EnableFirmwareButton(false);

            try {
                var unzipResult = Utils.Unzip(ofd.FileName, Path.Combine(Environment.CurrentDirectory, "firmware"));
                if (!unzipResult) {
                    EnableFirmwareButton(true);
                    throw new ListenAiException(101, "", "固件包无法解压");
                }

                var fwPackConfigPath = Path.Combine(Environment.CurrentDirectory, "firmware", "config.json");
                var fwPackDirPath = Path.GetDirectoryName(fwPackConfigPath);

                var fwCfgRaw = File.ReadAllText(fwPackConfigPath);
                var fwCfg = FirmwareConfig.FromJson(fwCfgRaw);
                if (fwCfg == null) {
                    throw new ListenAiException(102, "", "配置文件无法解析");
                }

                if (fwCfg.Files.Count != 2) {
                    throw new ListenAiException(102, "", "配置文件无法解析", 1);
                }

                foreach (var file in fwCfg.Files) {
                    if (file.Id != 0 && file.Id != 1) {
                        throw new ListenAiException(102, "", "配置文件无法解析", 2);
                    }

                    var fwPackFilePath = Path.Combine(fwPackDirPath, file.Name);
                    if (!File.Exists(fwPackFilePath)) {
                        throw new ListenAiException(103, "", $"固件 {file.Name} 不存在！");
                    }

                    var fi = new FileInfo(fwPackFilePath);
                    if (fi.Length != file.Size) {
                        throw new ListenAiException(104, "", $"固件 {file.Name} 大小不正确！\nActual = {fi.Length}, Expected = {file.Size}");
                    }

                    var hash = Utils.GetMd5Hash(fwPackFilePath);
                    if (hash != file.Checksum) {
                        throw new ListenAiException(105, "", $"固件 {file.Name} 校验失败！\nActual = {hash}\nExpected = {file.Checksum}");
                    }
                }

                btnFwSelect.BackColor = SystemColors.Control;
                fwCfg.FullPath = fwPackDirPath;
                var fwCskInfo = fwCfg.GetFirmware(Constants.GroupType.Csk);
                var fwWifiInfo = fwCfg.GetFirmware(Constants.GroupType.Wifi);
                if (fwCskInfo == null || fwWifiInfo == null) {
                    throw new ListenAiException(102, "", "配置文件无法解析", 3);
                }
                tsslCurrentFirmware.Text = $"CSK6固件: {fwCskInfo.Name} ({fwCskInfo.Version}) " +
                                           $"WIFI固件: {fwWifiInfo.Name} ({fwWifiInfo.Version}) " +
                                           $"固件包路径: {fwCfg.FullPath}";
                Global.SelectedFirmware = fwCfg;
                EnableFirmwareButton(true, false);
            }
            catch (ListenAiException lex) {
                EnableFirmwareButton(true);
                var exCode = lex.SubCode != 0 ? $"{lex.Code:000}-{lex.SubCode}" : lex.Code.ToString();
                MessageBox.Show($"[{exCode}] 请浏览并导入正确的固件包后再点击烧录。\n{lex.Details}", "错误");
            }
        }

        private void EnableFirmwareButton(bool enable, bool isUsingDefaultColor = true) {
            if (enable) {
                btnFwSelect.BackColor = isUsingDefaultColor ? Constants.ColorAbleToSelect : SystemColors.Control;
                btnFwSelect.Text = "浏览";
                btnFwSelect.Enabled = true;
            }
            else {
                btnFwSelect.Enabled = false;
                btnFwSelect.BackColor = Constants.ColorProcessing;
                btnFwSelect.Text = "正在解压...";
            }
        }

        private void btnFlash_Click(object sender, EventArgs e) {
            btnFlash.BackColor = SystemColors.Control;

            if (Global.MesRecord == null) {
                MessageBox.Show("请完全填写MES记录需要的数据后再点击烧录。", "错误");
                btnMES.BackColor = Constants.ColorBlock;
                btnFlash.BackColor = Constants.ColorBlock;
                return;
            }

            var checkCskPortResult = Utils.CheckComPorts(Constants.GroupType.Csk);
            var checkWifiPortResult = Utils.CheckComPorts(Constants.GroupType.Wifi);
            var availableGroups = new List<int>();

            foreach (var groupId in checkCskPortResult) {
                if (checkWifiPortResult.Contains(groupId)) {
                    availableGroups.Add(groupId);
                    var serialControl = Constants.GetControl(groupId, Constants.GroupType.Common, Constants.GroupConfigType.Serial);
                    if (serialControl.Text.Length == 0) {
                        serialControl.Text = Utils.GetSerialNumberWithDate();
                    }
                    //Constants.GetControl(groupId, Constants.GroupType.Common, Constants.GroupConfigType.Result).BackColor = Constants.ColorPreprocessing;
                }
            }
            if (availableGroups.Count == 0) {
                btnFlash.BackColor = Constants.ColorBlock;
                MessageBox.Show("请正确配置烧录串口后再点击烧录。", "错误");
                return;
            }

            var test1 = new LineWorker(1);
            test1.Start();

            btnFlash.BackColor = Constants.ColorProcessing;
        }

        private void btnMES_Click(object sender, EventArgs e) {
            using (var mesForm = new MesSettingsForm()) {
                var result = mesForm.ShowDialog();
                if (result == DialogResult.OK && Global.MesRecord != null) {
                    btnMES.BackColor = SystemColors.Control;
                }
            }
        }
    }
}
