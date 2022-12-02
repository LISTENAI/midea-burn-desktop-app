using System.ComponentModel;
using System.IO.Ports;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace ListenAI.Factory.FirmwareDeploy {
    public class Utils {
        public static Control CloneControl(Control srcCtl) {
            //ref: https://stackoverflow.com/questions/3473597/it-is-possible-to-copy-all-the-properties-of-a-certain-control-c-window-forms
            var cloned = Activator.CreateInstance(srcCtl.GetType()) as Control;
            var binding = BindingFlags.Public | BindingFlags.Instance;
            foreach (PropertyInfo prop in srcCtl.GetType().GetProperties(binding)) {
                if (IsClonable(prop)) {
                    object val = prop.GetValue(srcCtl);
                    prop.SetValue(cloned, val, null);
                }
            }

            foreach (Control ctl in srcCtl.Controls) {
                cloned.Controls.Add(CloneControl(ctl));
            }

            return cloned;
        }

        private static bool IsClonable(PropertyInfo prop) {
            if (prop.Name == "Name") {
                return true;
            }
            var browsableAttr = prop.GetCustomAttribute(typeof(BrowsableAttribute), true) as BrowsableAttribute;
            var editorBrowsableAttr = prop.GetCustomAttribute(typeof(EditorBrowsableAttribute), true) as EditorBrowsableAttribute;

            return prop.CanWrite
                && (browsableAttr == null || browsableAttr.Browsable == true)
                && (editorBrowsableAttr == null || editorBrowsableAttr.State != EditorBrowsableState.Advanced);
        }

        /// <summary>
        /// Initialize properties for newly created controls
        /// </summary>
        /// <param name="ctrl">control</param>
        /// <param name="id">group id</param>
        /// <returns></returns>
        public static Control CtrlPropModify(Control ctrl, int id) {
            var binding = BindingFlags.Public | BindingFlags.Instance;

            foreach (PropertyInfo prop in ctrl.GetType().GetProperties(binding)) {
                if (IsClonable(prop)) {
                    object val = prop.GetValue(ctrl);
                    switch (prop.Name) {
                        case "Name":
                            var newName = val.ToString().Replace("1", id.ToString());
                            prop.SetValue(ctrl, newName);
                            if (ctrl.GetType() != typeof(Label)) {
                                AddControlGroupMember(id, newName, ctrl);
                            }

                            //modify name of group
                            if (newName == $"lbCommon{id}Title") {
                                ctrl.Text = $"模组{ConvertToChineseChars(id)}";
                            }

                            //init pass/fail button backColor to be red
                            if (newName == $"btnCsk{id}Result") {
                                ctrl.BackColor = SystemColors.Control;
                            }
                            break;
                        default:
                            prop.SetValue(ctrl, val, null);
                            break;
                    }
                }
            }

            foreach (Control subCtrl in ctrl.Controls) {
                CtrlPropModify(subCtrl, id);
            }

            return ctrl;
        }

        private static void AddControlGroupMember(int groupId, string name, Control control) {
            if (!Global.ControlGroups.ContainsKey(groupId)) {
                Global.ControlGroups.Add(groupId, new());
            }

            if (!Global.ControlGroups[groupId].ContainsKey(name)) {
                Global.ControlGroups[groupId].Add(name, control);
            } else {
                Global.ControlGroups[groupId][name] = control;
            }
        }

        public static string ConvertToChineseChars(int num) {
            var result = num.ToString();
            result = result.Replace("0", "零")
                .Replace("1", "一")
                .Replace("2", "二")
                .Replace("3", "三")
                .Replace("4", "四")
                .Replace("5", "五")
                .Replace("6", "六")
                .Replace("7", "七")
                .Replace("8", "八")
                .Replace("9", "九");

            return result;
        }

        public static string GetSerialNumberWithDate() {
            var today = DateTime.UtcNow.AddHours(8).ToString("yyyyMMdd");
            var serial = ++Global.NextSerialNumber;

            return $"{today}{serial:00000}";
        }
        /// <summary>
        /// Check if COM port(s) is/are available before everything starts.
        /// </summary>
        /// <returns></returns>
        public static HashSet<int> CheckComPorts(Constants.GroupType groupType) {
            var allPorts = SerialPort.GetPortNames().ToHashSet();
            var result = new HashSet<int>();

            for (int i = 1; i <= Global.GroupCount; i++) {
                var targetCskControl = Constants.GetControl(i, groupType, Constants.GroupConfigType.Port);
                if (string.IsNullOrWhiteSpace(targetCskControl.Text)) {
                    continue;
                }

                if (!targetCskControl.Text.StartsWith("COM") || !allPorts.Contains(targetCskControl.Text)) {
                    MessageBox.Show($"模组{ConvertToChineseChars(i)}串口设置错误", "错误");
                    continue;
                }

                try {
                    var comPort = targetCskControl.Text;
                    var baudRate = int.Parse(Constants.GetControl(i, groupType, Constants.GroupConfigType.BaudRate).Text);
                    var dataBits = int.Parse(Constants.GetControl(i, groupType, Constants.GroupConfigType.Databits).Text);
                    var parity = int.Parse(Constants.GetControl(i, groupType, Constants.GroupConfigType.Parity).Text);
                    var stopBits = int.Parse(Constants.GetControl(i, groupType, Constants.GroupConfigType.Stopbits).Text);

                    var portOpenResult = IsComPortWorking(comPort, baudRate, dataBits, parity, stopBits);
                    if (!portOpenResult) {
                        MessageBox.Show($"模组{ConvertToChineseChars(i)}串口无法打开", "错误");
                        continue;
                    }
                }
                catch {
                    MessageBox.Show($"模组{ConvertToChineseChars(i)}串口无法打开", "错误");
                    continue;
                }

                result.Add(i);
            }

            return result;
        }

        /// <summary>
        /// [INTERNAL USE ONLY] Check if com port could be opened
        /// </summary>
        /// <param name="comPort">com port</param>
        /// <param name="baudRate">baud rate</param>
        /// <param name="databits">data bits</param>
        /// <param name="parity">parity</param>
        /// <param name="stopBits">stop bits</param>
        /// <returns></returns>
        private static bool IsComPortWorking(string comPort, int baudRate, int databits, int parity, int stopBits) {
            try {
                var port = new SerialPort(comPort, baudRate, (Parity)parity, databits, (StopBits)stopBits);

                port.Open();
                port.Close();

                return true;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// Create a connection to mssql server
        /// </summary>
        /// <param name="server">server ip/domain</param>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <param name="initCatalog">assigned database</param>
        /// <returns></returns>
        public static SqlConnection? CreateMsSqlConnection(string server, string username, string password, string initCatalog) {
            var connStrBuilder = new SqlConnectionStringBuilder {
                DataSource = server,
                UserID = username,
                Password = password,
                InitialCatalog = initCatalog,
                TrustServerCertificate = true
            };

            var conn = new SqlConnection(connStrBuilder.ConnectionString);
            conn.Open();

            return conn;
        }
    }
}
