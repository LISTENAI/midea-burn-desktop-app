namespace ListenAI.Factory.FirmwareDeploy {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            Constants.ColorProcceed = Color.FromArgb(0, 192, 0);
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

        }
    }
}
