using System.Diagnostics;
using ListenAI.Factory.FirmwareDeploy.Models;

namespace ListenAI.Factory.FirmwareDeploy {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            FormControlsInit();
            Global.AllWorkersCompleted += AllWorkersCompleted;
            CenterToScreen();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (MessageBox.Show("确定要退出？", "退出前确定", MessageBoxButtons.YesNo) == DialogResult.No) {
                e.Cancel = true;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            Utils.KillProcessByName("ASR_downloader_V1.0.6.exe");
            Utils.KillProcessByName("Uart_Burn_Tool.exe");
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

            //add event handlers
            for (var i = 1; i <= Global.GroupCount; i++) {
                var groupId = i;
                for (var t = 0; t <= 1; t++) {
                    //assign event handler for all default checkbox
                    var groupType = (Constants.GroupType)t;
                    var defaultCheckbox = (CheckBox)Constants.GetControl(groupId, groupType, Constants.GroupConfigType.IsDefault);
                    defaultCheckbox.CheckStateChanged += (_, _) => {
                        ((TextBox)Constants.GetControl(groupId, groupType, Constants.GroupConfigType.Port))
                            .ReadOnly = defaultCheckbox.Checked;
                        ((TextBox)Constants.GetControl(groupId, groupType, Constants.GroupConfigType.BaudRate))
                            .ReadOnly = defaultCheckbox.Checked;
                        Utils.SaveUiConfig();
                    };
                }

                //assign event handler for custom sn trigger
                var serialTextbox = (TextBox)Constants.GetControl(groupId, Constants.GroupType.Common, Constants.GroupConfigType.Serial);
                serialTextbox.KeyPress += (_, _) => {
                    Global.IsCustomSnEnabled[groupId] = true;
                };

                //build custom sn dict
                Global.IsCustomSnEnabled.Add(i, false);
            }

            //load ui config from file if exists
            var savedUiConfig = Utils.LoadUiConfig();
            if (savedUiConfig != null) {
                foreach (var portConfig in savedUiConfig.PortConfig) {
                    try {
                        if (!portConfig.Port.StartsWith("COM") || portConfig.GroupId < 1 ||
                            portConfig.GroupId > Global.GroupCount) {
                            continue;
                        }

                        var ctrlPort = (TextBox)Constants.GetControl(portConfig.GroupId,
                            (Constants.GroupType)portConfig.Type, Constants.GroupConfigType.Port);
                        if (ctrlPort == null) {
                            continue;
                        }
                        ctrlPort.Text = portConfig.Port;

                        var ctrlBaudRate = (TextBox)Constants.GetControl(portConfig.GroupId,
                            (Constants.GroupType)portConfig.Type, Constants.GroupConfigType.BaudRate);
                        if (ctrlBaudRate == null) {
                            continue;
                        }
                        ctrlBaudRate.Text = portConfig.BaudRate.ToString();

                        var ctrlIsDefault = (CheckBox)Constants.GetControl(portConfig.GroupId,
                            (Constants.GroupType)portConfig.Type, Constants.GroupConfigType.IsDefault);
                        if (ctrlIsDefault == null) {
                            continue;
                        }
                        ctrlIsDefault.Checked = true;
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Firmware selection button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                //try to unzip
                var unzipResult = Utils.Unzip(ofd.FileName, Path.Combine(Environment.CurrentDirectory, "firmware"));
                if (!unzipResult) {
                    EnableFirmwareButton(true);
                    throw new ListenAiException(101, "", "固件包无法解压");
                }

                var fwPackConfigPath = Path.Combine(Environment.CurrentDirectory, "firmware", "config.json");
                var fwPackDirPath = Path.GetDirectoryName(fwPackConfigPath);

                //parse config file
                var fwCfgRaw = File.ReadAllText(fwPackConfigPath);
                var fwCfg = FirmwareConfig.FromJson(fwCfgRaw);
                if (fwCfg == null) {
                    throw new ListenAiException(102, "", "配置文件无法解析");
                }

                if (fwCfg.Files.Count != 2) {
                    throw new ListenAiException(102, "", "配置文件无法解析", 1);
                }

                //check if all files mentioned in config exist
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

        /// <summary>
        /// Flash button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFlash_Click(object sender, EventArgs e) {
            btnFlash.BackColor = SystemColors.Control;

            if (Global.MesRecord == null) {
                MessageBox.Show("[301-1] 请完全填写MES记录需要的数据后再点击烧录。", "错误");
                return;
            }
            
            btnFlash.Enabled = false;
            if (Global.WorkersPool.Count == 0) {
                try {
                    //check if there are any duplicated ports
                    var existPorts = new HashSet<string>();
                    for (var g = 1; g <= Global.GroupCount; g++) {
                        for (var t = 0; t <= 1; t++) {
                            var ctrl = Constants.GetControl(g, (Constants.GroupType)t, Constants.GroupConfigType.Port);
                            if (ctrl == null) {
                                continue;
                            }

                            var intendedPort = ctrl.Text;
                            if (intendedPort.Length < 4) {
                                continue;
                            }

                            if (existPorts.Contains(intendedPort)) {
                                throw new ListenAiException(301, "重复的端口，", "", 4);
                            }
                            existPorts.Add(intendedPort);
                        }
                    }

                    var checkCskPortResult = Utils.CheckComPorts(Constants.GroupType.Csk);
                    var checkWifiPortResult = Utils.CheckComPorts(Constants.GroupType.Wifi);
                    var availableGroups = new List<int>();

                    foreach (var groupId in checkCskPortResult) {
                        if (checkWifiPortResult.Contains(groupId)) {
                            var serialControl = Constants.GetControl(groupId, Constants.GroupType.Common, Constants.GroupConfigType.Serial);
                            if (!Global.IsCustomSnEnabled[groupId]) {
                                serialControl.Text = Utils.GetSerialNumberWithDate();
                            }
                            else {
                                if (string.IsNullOrWhiteSpace(serialControl.Text)) {
                                    throw new ListenAiException(301, $"模组{Utils.ConvertToChineseChars(groupId)}产品序列号为空。",
                                        "", 3);
                                }
                            }
                            availableGroups.Add(groupId);
                            Constants.GetControl(groupId, Constants.GroupType.Common, Constants.GroupConfigType.Result).BackColor = Constants.ColorProcessing;
                        }
                    }
                    if (availableGroups.Count == 0) {
                        btnFlash.BackColor = Constants.ColorBlock;
                        throw new ListenAiException(301, "请正确配置烧录串口后再点击烧录", "", 2);
                    }

                    EnableMainFormUi(false);
                    var r = new Random();
                    foreach (var groupId in availableGroups) {
                        var newWorker = new LineWorker(groupId);
                        Thread.Sleep(r.Next(200, 1000));
                        Global.WorkersPool.Add(groupId, newWorker);
                        newWorker.Start();
                    }
                }
                catch (ListenAiException lex) {
                    var exCode = lex.SubCode != 0 ? $"{lex.Code:000}-{lex.SubCode}" : lex.Code.ToString();
                    MessageBox.Show($"[{exCode}] {lex.Message}\n{lex.Details}", "错误");
                    Global.WorkersPool.Clear();
                    EnableMainFormUi(true);
                    btnFlash.Enabled = true;
                    return;
                }

                btnFlash.Text = "停止烧录";
            }
            else {
                try {
                    foreach (var worker in Global.WorkersPool.Values) {
                        worker.Stop();
                    }
                    Thread.Sleep(2000);
                    Global.WorkersPool.Clear();
                    EnableMainFormUi(true);
                } catch { }
                btnFlash.Text = "烧录";
            }

            btnFlash.Enabled = true;
        }

        private void EnableMainFormUi(bool isEnabled) {
            btnMES.Enabled = isEnabled;
            btnFwSelect.Enabled = isEnabled;

            for (var i = 1; i <= Global.GroupCount; i++) {
                Constants.GetControl(i, Constants.GroupType.Csk, Constants.GroupConfigType.Port).Enabled = isEnabled;
                Constants.GetControl(i, Constants.GroupType.Csk, Constants.GroupConfigType.BaudRate).Enabled = isEnabled;
                Constants.GetControl(i, Constants.GroupType.Csk, Constants.GroupConfigType.IsDefault).Enabled = isEnabled;
                Constants.GetControl(i, Constants.GroupType.Wifi, Constants.GroupConfigType.Port).Enabled = isEnabled;
                Constants.GetControl(i, Constants.GroupType.Wifi, Constants.GroupConfigType.BaudRate).Enabled = isEnabled;
                Constants.GetControl(i, Constants.GroupType.Wifi, Constants.GroupConfigType.IsDefault).Enabled = isEnabled;
                Constants.GetControl(i, Constants.GroupType.Common, Constants.GroupConfigType.Serial).Enabled = isEnabled;
                
            }
        }

        private void btnMES_Click(object sender, EventArgs e) {
            using (var mesForm = new MesSettingsForm()) {
                var result = mesForm.ShowDialog();
                if (result == DialogResult.OK && Global.MesRecord != null) {
                    btnMES.BackColor = SystemColors.Control;
                }
            }
        }

        /// <summary>
        /// Event handler to be called on all workers completed their jobs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllWorkersCompleted(object? sender, EventArgs e) {
            btnFlash.Enabled = false;
            EnableMainFormUi(true);
            btnFlash.Text = "烧录";
            btnFlash.Enabled = true;
        }
    }
}
