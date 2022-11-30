namespace ListenAI.Factory.FirmwareDeploy {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            FormControlsInit();
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
            ofd.Filter = "固件文件 (*.bin)|*.bin|所有文件 (*.*)|*.*";
            ofd.Multiselect = false;
            var result = ofd.ShowDialog();

            if (result == DialogResult.OK) {
                Global.SelectedFirmwarePath = ofd.FileName;
                tsslCurrentFirmware.Text = $"当前固件: {Global.SelectedFirmwarePath}";
                btnFwSelect.BackColor = Constants.ColorProcceed;
            } else {
                Global.SelectedFirmwarePath = "";
                tsslCurrentFirmware.Text = $"当前固件: (未选择)";
                btnFwSelect.BackColor = Constants.ColorBlock;
            }
        }

        private void btnFlash_Click(object sender, EventArgs e) {
            if (!File.Exists(Global.SelectedFirmwarePath)) {
                Global.SelectedFirmwarePath = "";
                tsslCurrentFirmware.Text = $"当前固件: (未选择)";
                btnFwSelect.BackColor = Constants.ColorBlock;
                MessageBox.Show("请浏览并导入正确的固件包后再点击烧录。", "错误");
                return;
            }

            var checkCskPortResult = LineWorker.CheckComPorts(Constants.GroupType.Csk);
            //var checkWifiPortResult = LineWorker.CheckComPorts(Constants.GroupType.Wifi);
            var checkWifiPortResult = true;
            if (!checkCskPortResult || !checkWifiPortResult) {
                btnFlash.BackColor = Constants.ColorBlock;
                MessageBox.Show("请正确配置烧录串口后再点击烧录。", "错误");
                return;
            }

            btnFlash.BackColor = Constants.ColorProcceed;
        }
    }
}
