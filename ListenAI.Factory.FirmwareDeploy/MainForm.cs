using ListenAI.Factory.FirmwareDeploy.models;

namespace ListenAI.Factory.FirmwareDeploy {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            FormControlsInit();
            CenterToScreen();
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
                clonedGrpbox.Location = new Point(locX, gbMod1.Location.Y);
                this.Controls.Add(clonedGrpbox);
            }
        }

        private void btnFwSelect_Click(object sender, EventArgs e) {
            var ofd = new OpenFileDialog();
            ofd.Filter = "固件包配置文件 (*.json)|*.json|所有文件 (*.*)|*.*";
            ofd.Multiselect = false;
            var result = ofd.ShowDialog();

            if (result != DialogResult.OK) {
                return;
            }

            var fwPackConfigPath = ofd.FileName;
            var fwPackDirPath = Path.GetDirectoryName(ofd.FileName);

            var fwCfgRaw = File.ReadAllText(fwPackConfigPath);
            var fwCfg = FirmwareConfig.FromJson(fwCfgRaw);
            if (fwCfg == null) {
                MessageBox.Show("请浏览并导入正确的固件包后再点击烧录。", "错误");
                return;
            }

            if (fwCfg.Files.Count != 2) {
                MessageBox.Show("固件包格式不正确！01", "错误");
                return;
            }

            foreach (var file in fwCfg.Files) {
                if (file.Id != 0 && file.Id != 1) {
                    MessageBox.Show("固件包格式不正确！02", "错误");
                    return;
                }

                var fwPackFilePath = Path.Combine(fwPackDirPath, file.Name);
                if (!File.Exists(fwPackFilePath)) {
                    MessageBox.Show($"固件 {file.Name} 不存在！", "错误");
                    return;
                }

                var fi = new FileInfo(fwPackFilePath);
                if (fi.Length != file.Size) {
                    MessageBox.Show($"固件 {file.Name} 大小不正确！\nActual = {fi.Length}, Expected = {file.Size}", "错误");
                    return;
                }

                var hash = Utils.GetMd5Hash(fwPackFilePath);
                if (hash != file.Checksum) {
                    MessageBox.Show($"固件 {file.Name} 校验失败！\nActual = {hash}\nExpected = {file.Checksum}", "错误");
                    return;
                }
            }

            try {
                btnFwSelect.BackColor = Constants.ColorProcceed;
                fwCfg.FullPath = fwPackDirPath;
                var fwCskInfo = fwCfg.GetFirmware(Constants.GroupType.Csk);
                var fwWifiInfo = fwCfg.GetFirmware(Constants.GroupType.Wifi);
                tsslCurrentFirmware.Text = $"CSK6固件: {fwCskInfo.Name} ({fwCskInfo.Version}) " +
                                           $"WIFI固件: {fwWifiInfo.Name} ({fwWifiInfo.Version}) " +
                                           $"固件包路径: {fwCfg.FullPath}";
                Global.SelectedFirmware = fwCfg;
            }
            catch (Exception ex) {
                MessageBox.Show($"固件包格式不正确！03\n{ex}", "错误");
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
                    Constants.GetControl(groupId, Constants.GroupType.Common, Constants.GroupConfigType.Result).BackColor = Constants.ColorPreprocessing;
                }
            }
            if (availableGroups.Count == 0) {
                btnFlash.BackColor = Constants.ColorBlock;
                MessageBox.Show("请正确配置烧录串口后再点击烧录。", "错误");
                return;
            }

            var test1 = new LineWorker(1, Constants.GroupType.Csk);
            test1.Flash();

            btnFlash.BackColor = Constants.ColorProcessing;
        }

        private void btnMES_Click(object sender, EventArgs e) {
            using (var mesForm = new MesSettingsForm()) {
                var result = mesForm.ShowDialog();
                if (result == DialogResult.OK && Global.MesRecord != null) {
                    btnMES.BackColor = Constants.ColorProcceed;
                }
            }
        }
    }
}
