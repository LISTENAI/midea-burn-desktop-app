namespace ListenAI.Factory.FirmwareDeploy {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            FormElementsInit();
        }

        private void FormElementsInit() {
            var cskPanels = new List<Panel>() {
                panelCsk1, panelCsk2, panelCsk3, panelCsk4
            };
            foreach (var panel in cskPanels) {
                panel.BackColor = Constants.ColorCsk6PanelBackground;
            }
            var wifiPanels = new List<Panel>() {
                panelWifi1, panelWifi2, panelWifi3, panelWifi4
            };
            foreach (var panel in wifiPanels) {
                panel.BackColor = Constants.ColorWifiPanelBackground;
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
                MessageBox.Show("选择的固件不存在，请重新选择");
                return;
            }

            btnFlash.BackColor = Constants.ColorProcceed;
        }
    }
}
