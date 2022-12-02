using System.Data;
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
            tbOptMachineId.Text = "";

            this.Size = new Size(531, 806);
            cbIsImportFromDb.Checked = false;

            tbDbIp.Text = "";
            tbDbName.Text = "";
            tbDbUsername.Text = "";
            tbDbPassword.Text = "";
            tbDbTableName.Text = "";
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
                            MessageBox.Show("根据MES指令单号，没有找到任何记录。01", "错误");
                            return;
                        }

                        if (reader.Read()) {
                            tbProdId.Text = reader["DAA014"].ToString();
                            tbProdName.Text = reader["DAA015"].ToString();
                            tbProdModel.Text = reader["DAA016"].ToString();
                        }
                        else {
                            MessageBox.Show("根据MES指令单号，没有找到任何记录。02", "错误");
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
        }
    }
}
