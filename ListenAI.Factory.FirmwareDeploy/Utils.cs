using System.ComponentModel;
using System.IO.Compression;
using System.IO.Ports;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using ListenAI.Factory.FirmwareDeploy.Models;
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
            }
            else {
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
                    throw new ListenAiException(302, $"模组{ConvertToChineseChars(i)}串口设置错误", "");
                }

                try {
                    var comPort = targetCskControl.Text;
                    var baudRate = int.Parse(Constants.GetControl(i, groupType, Constants.GroupConfigType.BaudRate).Text);

                    var portOpenResult = IsComPortWorking(comPort, baudRate);
                    if (!portOpenResult) {
                        throw new ListenAiException(303, $"模组{ConvertToChineseChars(i)}串口无法打开", "", 1);
                    }
                }
                catch (Exception ex) when (!(ex is ListenAiException)) {
                    throw new ListenAiException(303, $"模组{ConvertToChineseChars(i)}串口无法打开", ex.Message, 2);
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
        private static bool IsComPortWorking(string comPort, int baudRate) {
            try {
                var port = new SerialPort(comPort, baudRate, Parity.None, 8, StopBits.One);

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

        public static string GetMd5Hash(string path) {
            using (var md5 = MD5.Create()) {
                using (var stream = File.OpenRead(path)) {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static bool Unzip(string zipFile, string destPath) {
            try {
                if (Directory.Exists(destPath)) {
                    Directory.Delete(destPath, true);
                }

                Directory.CreateDirectory(destPath);
                ZipFile.ExtractToDirectory(zipFile, destPath, true);
                return true;
            }
            catch {
                return false;
            }
        }

        public static UiConfig? SaveUiConfig() {
            var result = new UiConfig();

            try {
                for (var i = 1; i <= Global.GroupCount; i++) {
                    var groupId = i;
                    for (var t = 0; t <= 1; t++) {
                        var defaultCheckbox = (CheckBox)Constants.GetControl(groupId, (Constants.GroupType)t,
                            Constants.GroupConfigType.IsDefault);
                        if (defaultCheckbox.Checked) {
                            result.PortConfig.Add(new PortConfig() {
                                GroupId = groupId,
                                Type = (PortConfigType)t,
                                Port = Constants.GetControl(groupId, (Constants.GroupType)t,
                                    Constants.GroupConfigType.Port).Text,
                                BaudRate = long.Parse(Constants.GetControl(groupId, (Constants.GroupType)t,
                                    Constants.GroupConfigType.BaudRate).Text)
                            });
                        }
                    }
                }

                File.WriteAllText(Constants.UiConfigPath, result.ToString());
                return result;
            }
            catch {
                return null;
            }
        }

        public static UiConfig? LoadUiConfig() {
            if (!File.Exists(Constants.UiConfigPath)) {
                return null;
            }

            try {
                var result = UiConfig.FromJson(File.ReadAllText(Constants.UiConfigPath, Encoding.UTF8));
                return result;
            }
            catch {
                return null;
            }
        }
    }
}
