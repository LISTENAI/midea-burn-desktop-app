using ListenAI.Factory.FirmwareDeploy.Models;
using Newtonsoft.Json;

namespace ListenAI.Factory.FirmwareDeploy {
    public partial class FirmwarePackingForm : Form {
        public FirmwarePackingForm() {
            InitializeComponent();
        }

        private void FirmwarePackingForm_Load(object sender, EventArgs e) {

        }

        private void btnCskFwPathSelect_Click(object sender, EventArgs e) {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "CSK6固件(*.img,*.hex)|*.img;*.hex";
            ofd.Multiselect = false;
            ofd.Title = "选择CSK6固件";
            ofd.CheckPathExists = true;
            var result = ofd.ShowDialog();

            if (result != DialogResult.OK) {
                return;
            }

            if (string.IsNullOrWhiteSpace(ofd.FileName) ||
                !Utils.IsValidFirmware(ofd.FileName, Constants.FirmwareType.Csk)) {
                MessageBox.Show("[403-1] 请导入正确的 CSK6 固件。");
                return;
            }

            tbCskFwPath.Text = ofd.FileName;
        }

        private void btnAsrFwPathSelect_Click(object sender, EventArgs e) {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "ASR固件(*.bin)|*.bin";
            ofd.Multiselect = false;
            ofd.Title = "选择ASR固件";
            ofd.CheckPathExists = true;
            var result = ofd.ShowDialog();

            if (result != DialogResult.OK) {
                return;
            }

            if (string.IsNullOrWhiteSpace(ofd.FileName) ||
                !Utils.IsValidFirmware(ofd.FileName, Constants.FirmwareType.Asr)) {
                MessageBox.Show("[403-2] 请导入正确的 ASR 固件。");
                return;
            }

            tbAsrFwPath.Text = ofd.FileName;
        }

        private void btnPackFwPathSelect_Click(object sender, EventArgs e) {
            using var sfd = new SaveFileDialog();
            sfd.Filter = "固件包(*.zip)|*.zip";
            sfd.OverwritePrompt = true;
            sfd.Title = "选择固件包保存位置";
            sfd.CheckPathExists = true;
            var result = sfd.ShowDialog();

            if (result != DialogResult.OK) {
                return;
            }

            tbPackFwPath.Text = sfd.FileName;
        }

        private void btnPack_Click(object sender, EventArgs e) {
            try {
                tsslStatus.Text = "当前状态：正在打包";
                EnableMainFormUi(false);

                //check version numbers
                if (string.IsNullOrWhiteSpace(tbCskFwVer.Text) || string.IsNullOrWhiteSpace(tbAsrFwVer.Text) ||
                    string.IsNullOrWhiteSpace(tbPackFwVer.Text)) {
                    throw new ListenAiException(401, "请输入版本号。", "");
                }

                //check firmware pack
                if (string.IsNullOrWhiteSpace(tbPackFwPath.Text)) {
                    throw new ListenAiException(402, "请选择固件包保存位置。", "");
                }

                //check csk6/asr firmware file
                if (string.IsNullOrWhiteSpace(tbCskFwPath.Text)) {
                    throw new ListenAiException(403, "请导入正确的 CSK6 固件", "", 1);
                }
                if (string.IsNullOrWhiteSpace(tbAsrFwPath.Text)) {
                    throw new ListenAiException(403, "请导入正确的 ASR 固件", "", 2);
                }

                //generate work dir
                var workDirPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(workDirPath);

                //Generate package metadata
                var csk6Hash = Utils.GetFileMd5Hash(tbCskFwPath.Text);
                var csk6Size = Utils.GetFileSize(tbCskFwPath.Text);
                var asrHash = Utils.GetFileMd5Hash(tbAsrFwPath.Text);
                var asrSize = Utils.GetFileSize(tbAsrFwPath.Text);

                var metadata = new FwPkgMetadata() {
                    Version = tbPackFwVer.Text,
                    Files = new() {
                        //csk6
                        new FwPkgFileMetadata() {
                            Id = 0,
                            Name = Path.GetFileName(tbCskFwPath.Text),
                            Version = tbCskFwVer.Text,
                            Size = csk6Size,
                            Hash = csk6Hash
                        },
                        //asr
                        new FwPkgFileMetadata() {
                            Id = 1,
                            Name = Path.GetFileName(tbAsrFwPath.Text),
                            Version = tbAsrFwVer.Text,
                            Size = asrSize,
                            Hash = asrHash
                        },
                    }
                };
                File.WriteAllText(Path.Combine(workDirPath, "config.json"), JsonConvert.SerializeObject(metadata));
                File.Copy(tbCskFwPath.Text, Path.Combine(workDirPath, Path.GetFileName(tbCskFwPath.Text)));
                File.Copy(tbAsrFwPath.Text, Path.Combine(workDirPath, Path.GetFileName(tbAsrFwPath.Text)));

                var zipResult = Utils.Zip(workDirPath, tbPackFwPath.Text);
                Directory.Delete(workDirPath, true);

                if (!zipResult) {
                    throw new ListenAiException(404, "固件打包失败！", "");
                }

                tsslStatus.Text = "当前状态：空闲";
                MessageBox.Show("打包完成！");
            }
            catch (ListenAiException lex) {
                MessageBox.Show($"[{lex.Code}] {lex.Message}\n{lex.Details}");
            }
            catch (Exception ex) {
                MessageBox.Show($"[499] 打包固件时出现了意外状况！\n{ex.Message}");
            }
            finally {
                tsslStatus.Text = "当前状态：空闲";
                EnableMainFormUi(true);
            }
        }

        private void EnableMainFormUi(bool isEnabled) {
            tbCskFwVer.Enabled = isEnabled;
            btnCskFwPathSelect.Enabled = isEnabled;
            tbAsrFwVer.Enabled = isEnabled;
            btnAsrFwPathSelect.Enabled = isEnabled;
            tbPackFwVer.Enabled = isEnabled;
            btnPackFwPathSelect.Enabled = isEnabled;
            btnPack.Enabled = isEnabled;
        }
    }
}
