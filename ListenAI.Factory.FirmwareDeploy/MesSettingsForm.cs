using System.Text.RegularExpressions;
using ListenAI.Factory.FirmwareDeploy.Models;
using Microsoft.Data.SqlClient;

namespace ListenAI.Factory.FirmwareDeploy {
    public partial class MesSettingsForm : Form {

        public MesSettingsForm() {
            InitializeComponent();
        }

        private void MesSettingsForm_Load(object sender, EventArgs e) {
            this.Size = new Size(531, 806);
        }

        /// <summary>
        /// Whether to show db config section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowDbConfig_Click(object sender, EventArgs e) {
            if (cbIsImportFromDb.Checked) {
                this.Size = new Size(531, 806);
                cbIsImportFromDb.Checked = false;
            }
            else {
                this.Size = new Size(1061, 806);
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

            this.Size = new Size(531, 806);
            cbIsImportFromDb.Checked = false;

            tbDbIp.Text = "";
            tbDbName.Text = "";
            tbDbUsername.Text = "";
            tbDbPassword.Text = "";
            tbDbTableName.Text = "";
        }

        private void EnableAllControls(bool isEnabled) {
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
                MessageBox.Show("请输入MES指令单号", "错误");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbDbIp.Text) ||
                string.IsNullOrWhiteSpace(tbDbName.Text) ||
                string.IsNullOrWhiteSpace(tbDbUsername.Text) ||
                string.IsNullOrWhiteSpace(tbDbPassword.Text) ||
                string.IsNullOrWhiteSpace(tbDbTableName.Text)) {
                MessageBox.Show("数据库连接信息不完整，请补充。", "错误");
                return;
            }

            var tableNameTest = Regex.IsMatch(tbDbTableName.Text, @"^[A-Za-z0-9_-]*$", RegexOptions.Singleline);
            if (!tableNameTest) {
                MessageBox.Show("数据库表名不正确", "错误");
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
                using (var conn = Utils.CreateMsSqlConnection(server, username, password, db)) {
                    var sql = "select top 1 DAA001, DAA014, DAA015, DAA016 from " + tableName + " where DAA001 = @mesCmdId";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@mesCmdId", mesCmdId);

                    using (var reader = cmd.ExecuteReader()) {
                        if (!reader.HasRows) {
                            EnableAllControls(true);
                            MessageBox.Show("根据MES指令单号，没有找到任何记录。01", "错误");
                            return;
                        }

                        if (reader.Read()) {
                            tbProdId.Text = reader["DAA014"].ToString();
                            tbProdName.Text = reader["DAA015"].ToString();
                            tbProdModel.Text = reader["DAA016"].ToString();
                        }
                        else {
                            EnableAllControls(true);
                            MessageBox.Show("根据MES指令单号，没有找到任何记录。02", "错误");
                            return;
                        }
                    }
                }
            }
            catch (SqlException sqlEx) {
                MessageBox.Show($"无法连接数据库，请联系运维处理。\n{sqlEx.Message}", "错误");
            }
            catch (Exception ex) {
                MessageBox.Show($"连接数据库出现异常，请联系运维处理。\n{ex.Message}", "错误");
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
                MessageBox.Show("请完全填写MES记录需要的数据后再点击确认", "错误");
                return;
            }

            Global.MesRecord = new MesRecord() {
                MesCmdId = tbMesCmdId.Text,
                ProductId = tbProdId.Text,
                ProductName = tbProdName.Text,
                ProductModel = tbProdModel.Text,
                FlashOperator = tbFlashOpter.Text,
                FlashToolName = tbFlashToolName.Text,
                FlashMachineId = tbFlashMachineId.Text
            };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
