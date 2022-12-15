using System.Text.RegularExpressions;
using ListenAI.Factory.FirmwareDeploy.Models;
using Microsoft.Data.SqlClient;

namespace ListenAI.Factory.FirmwareDeploy {
    public partial class MesSettingsForm : Form {
        private int _noDbWidth;
        private int _noDbHeight;
        private int _withDbWidth;
        private int _withDbHeight;

        public MesSettingsForm() {
            InitializeComponent();
            var _widthBleeding = 1.07m;
            var _heightBleeding = 1.1m;
            _noDbWidth = Convert.ToInt32((groupBox1.Location.X + groupBox1.Size.Width) * _widthBleeding);
            _noDbHeight = Convert.ToInt32((groupBox1.Location.Y + groupBox1.Size.Height) * _heightBleeding);
            _withDbWidth = Convert.ToInt32((groupBox2.Location.X + groupBox2.Size.Width) * _widthBleeding);
            _withDbHeight = Convert.ToInt32((groupBox1.Location.Y + groupBox1.Size.Height) * _heightBleeding);
        }

        private void MesSettingsForm_Load(object sender, EventArgs e) {
            Size = new Size(_noDbWidth, _noDbHeight);

            if (Global.MesRecord != null) {
                tbMesCmdId.Text = Global.MesRecord.MesCmdId;
                tbProdId.Text = Global.MesRecord.ProductId;
                tbProdName.Text = Global.MesRecord.ProductName;
                tbProdModel.Text = Global.MesRecord.ProductModel;
                tbFlashOpter.Text = Global.MesRecord.FlashOperator;
                tbFlashToolName.Text = Global.MesRecord.FlashToolName;
                tbFlashMachineId.Text = Global.MesRecord.FlashMachineId;

                tbDbIp.Text = Global.MesRecord.DbIp;
                tbDbName.Text = Global.MesRecord.DbName;
                tbDbUsername.Text = Global.MesRecord.DbUsername;
                tbDbPassword.Text = Global.MesRecord.DbPassword;
                tbDbTableName.Text = Global.MesRecord.DbTableName;

                if (!string.IsNullOrWhiteSpace(Global.MesRecord.DbIp)) {
                    Size = new Size(_withDbWidth, _withDbHeight);
                    cbIsImportFromDb.Checked = true;
                }
            }
        }

        /// <summary>
        /// Whether to show db config section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowDbConfig_Click(object sender, EventArgs e) {
            if (cbIsImportFromDb.Checked) {
                Size = new Size(_noDbWidth, _noDbHeight);
                cbIsImportFromDb.Checked = false;
            }
            else {
                Size = new Size(_withDbWidth, _withDbHeight);
                cbIsImportFromDb.Checked = true;
            }
        }

        /// <summary>
        /// Reset the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e) {
            tbMesCmdId.Text = "";
            tbProdId.Text = "";
            tbProdName.Text = "";
            tbProdModel.Text = "";
            tbFlashOpter.Text = "";
            tbFlashToolName.Text = "";
            tbFlashMachineId.Text = "";

            Size = new Size(_noDbWidth, _noDbHeight);
            cbIsImportFromDb.Checked = false;

            tbDbIp.Text = "";
            tbDbName.Text = "";
            tbDbUsername.Text = "";
            tbDbPassword.Text = "";
            tbDbTableName.Text = "";
        }

        private void EnableAllControls(bool isEnabled) {
            btnDbImport.Text = isEnabled ? "导入" : "Loading...";

            tbMesCmdId.Enabled = isEnabled;
            tbProdId.Enabled = isEnabled;
            tbProdName.Enabled = isEnabled;
            tbProdModel.Enabled = isEnabled;
            tbFlashOpter.Enabled = isEnabled;
            tbFlashToolName.Enabled = isEnabled;
            tbFlashMachineId.Enabled = isEnabled;

            tbDbIp.Enabled = isEnabled;
            tbDbName.Enabled = isEnabled;
            tbDbUsername.Enabled = isEnabled;
            tbDbPassword.Enabled = isEnabled;
            tbDbTableName.Enabled = isEnabled;

            btnShowDbConfig.Enabled = isEnabled;
            btnReset.Enabled = isEnabled;
            btnConfirm.Enabled = isEnabled;
            btnDbImport.Enabled = isEnabled;
        }

        /// <summary>
        /// Connect to database and fetch records according to form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDbImport_Click(object sender, EventArgs e) {
            if (!cbIsImportFromDb.Checked) {
                return;
            }

            if (tbMesCmdId.Text == "") {
                MessageBox.Show("[201-1] 请输入MES指令单号", "错误");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbDbIp.Text) ||
                string.IsNullOrWhiteSpace(tbDbName.Text) ||
                string.IsNullOrWhiteSpace(tbDbUsername.Text) ||
                string.IsNullOrWhiteSpace(tbDbPassword.Text) ||
                string.IsNullOrWhiteSpace(tbDbTableName.Text)) {
                MessageBox.Show("[201-2] 数据库连接信息不完整，请补充。", "错误");
                return;
            }

            var tableNameTest = Regex.IsMatch(tbDbTableName.Text, @"^[A-Za-z0-9_-]*$", RegexOptions.Singleline);
            if (!tableNameTest) {
                MessageBox.Show("[201-3] 数据库表名不正确", "错误");
                return;
            }

            EnableAllControls(false);
            var server = tbDbIp.Text;
            var username = tbDbUsername.Text;
            var password = tbDbPassword.Text;
            var db = tbDbName.Text;
            var tableName = tbDbTableName.Text;
            var mesCmdId = tbMesCmdId.Text;

            try {
                using var conn = Utils.CreateMsSqlConnection(server, username, password, db);
                var sql = "select top 1 DAA001, DAA014, DAA015, DAA016 from " + tableName + " where DAA001 = @mesCmdId";
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mesCmdId", mesCmdId);

                using var reader = cmd.ExecuteReader();
                if (!reader.HasRows) {
                    throw new ListenAiException(202, "根据MES指令单号，没有找到任何记录。", "", 1);
                }

                if (reader.Read()) {
                    tbProdId.Text = reader["DAA014"].ToString();
                    tbProdName.Text = reader["DAA015"].ToString();
                    tbProdModel.Text = reader["DAA016"].ToString();
                }
                else {
                    throw new ListenAiException(202, "根据MES指令单号，没有找到任何记录。", "", 2);
                }
            }
            catch (ListenAiException lex) {
                var exCode = lex.SubCode != 0 ? $"{lex.Code:000}-{lex.SubCode}" : lex.Code.ToString();
                MessageBox.Show($"[{exCode}] {lex.Message}\n{lex.Details}", "错误");
            }
            catch (SqlException sqlEx) {
                MessageBox.Show($"[203] 无法连接数据库，请联系运维处理。\n{sqlEx.Message}", "错误");
            }
            catch (Exception ex) {
                MessageBox.Show($"[204] 连接数据库出现异常，请联系运维处理。\n{ex.Message}", "错误");
            }

            EnableAllControls(true);
        }

        private void btnConfirm_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(tbMesCmdId.Text) ||
                string.IsNullOrWhiteSpace(tbProdId.Text) ||
                string.IsNullOrWhiteSpace(tbProdName.Text) ||
                string.IsNullOrWhiteSpace(tbProdModel.Text) ||
                string.IsNullOrWhiteSpace(tbFlashOpter.Text) ||
                string.IsNullOrWhiteSpace(tbFlashToolName.Text) ||
                string.IsNullOrWhiteSpace(tbFlashMachineId.Text)) {
                MessageBox.Show("[201-4] 请完全填写MES记录需要的数据后再点击确认", "错误");
                return;
            }

            Global.MesRecord = new MesRecord {
                MesCmdId = tbMesCmdId.Text,
                ProductId = tbProdId.Text,
                ProductName = tbProdName.Text,
                ProductModel = tbProdModel.Text,
                FlashOperator = tbFlashOpter.Text,
                FlashToolName = tbFlashToolName.Text,
                FlashMachineId = tbFlashMachineId.Text,

                DbIp = tbDbIp.Text,
                DbName = tbDbName.Text,
                DbUsername = tbDbUsername.Text,
                DbPassword = tbDbPassword.Text,
                DbTableName = tbDbTableName.Text
            };
            DialogResult = DialogResult.OK;
            Close();
        }

        private void tbFlashMachineId_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == 13) {
                btnConfirm_Click(sender, null);
            }
        }
    }
}
