using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.IO.Ports;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using ListenAI.Factory.FirmwareDeploy.Models;
using Microsoft.Data.SqlClient;

namespace ListenAI.Factory.FirmwareDeploy {
    public class Utils {
        /// <summary>
        /// Clone a control along with its child-controls
        /// </summary>
        /// <param name="srcCtl">the control to be cloned</param>
        /// <returns></returns>
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

        /// <summary>
        /// [INTERNAL] Check if property of control is clonable
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
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

        /// <summary>
        /// [INTERNAL] Add control to global directory for further usage in functions
        /// </summary>
        /// <param name="groupId">group id</param>
        /// <param name="name">control name</param>
        /// <param name="control">the control itself</param>
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

        /// <summary>
        /// Convert numbers into chinese chars
        /// </summary>
        /// <param name="num">the number to be converted</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get next product serial number (id stored in Global.NextSerialNumber)
        /// </summary>
        /// <returns></returns>
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
            var allPorts = SerialPort.GetPortNames().Select(p => p.Replace("COM", "")).ToHashSet();
            var result = new HashSet<int>();

            for (int i = 1; i <= Global.GroupCount; i++) {
                var targetCskControl = Constants.GetControl(i, groupType, Constants.GroupConfigType.Port);
                if (string.IsNullOrWhiteSpace(targetCskControl.Text) || !IsPositiveNumber(targetCskControl.Text)) {
                    continue;
                }

                if (!IsPositiveNumber(targetCskControl.Text) || !allPorts.Contains(targetCskControl.Text)) {
                    throw new ListenAiException(302, $"模组{ConvertToChineseChars(i)}串口设置错误", "");
                }

                try {
                    var comPort = "COM" + targetCskControl.Text;
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

        public static string[] ListComPorts() {
            return SerialPort.GetPortNames().Select(p => p.Replace("COM", "")).ToArray();
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

        /// <summary>
        /// Get MD5 hash of a file
        /// </summary>
        /// <param name="path">path to the file</param>
        /// <returns></returns>
        public static string GetMd5Hash(string path) {
            using (var md5 = MD5.Create()) {
                using (var stream = File.OpenRead(path)) {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        /// <summary>
        /// Create a zip archive
        /// </summary>
        /// <param name="sourceDir">source directory to files to be included in ZIP</param>
        /// <param name="destZipFile">where to put the created zip</param>
        /// <returns></returns>
        public static bool Zip(string sourceDir, string destZipFile) {
            try {
                if (!Directory.Exists(sourceDir)) {
                    return false;
                }

                if (File.Exists(destZipFile)) {
                    File.Delete(destZipFile);
                }

                ZipFile.CreateFromDirectory(sourceDir, destZipFile, CompressionLevel.Optimal, false, Encoding.UTF8);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Extract a zip archive
        /// </summary>
        /// <param name="zipFile">zip file path</param>
        /// <param name="destPath">Path to put extracted files</param>
        /// <returns></returns>
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

        /// <summary>
        /// Save port+baudrate of each group to file
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Load saved port+baudrate from file
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Kill process by name
        /// </summary>
        /// <param name="name">process name to kill</param>
        public static void KillProcessByName(string name) {
            try {
                var proc = Process.GetProcessesByName(name);
                foreach (var p in proc) {
                    p.Kill(true);
                }
            } catch {}
        }

        /// <summary>
        /// Check if firmware file is valid by checking header
        /// </summary>
        /// <param name="path">intended firmware header</param>
        /// <param name="type">firmware type</param>
        /// <returns>true or false</returns>
        public static bool IsValidFirmware(string path, Constants.FirmwareType type) {
            if (!File.Exists(path)) {
                return false;
            }

            try {
                var fw = File.ReadAllBytes(path);
                var ext = Path.GetExtension(path);
                switch (type) {
                    case Constants.FirmwareType.Csk:
                        switch (ext.ToLower()) {
                            case ".img":
                                return fw[0] == 0x0;
                            case ".hex":
                                return fw[0] == ':';
                            default:
                                return false;
                        }
                    case Constants.FirmwareType.Asr:
                        var header = fw.Take(8).ToArray();
                        return header.SequenceEqual(Constants.ValidAsrFirmwareHeader);
                    default:
                        return false;
                }
            }
            catch (Exception) {
                return false;
            }
        }

        /// <summary>
        /// Get MD5 hash of a file
        /// </summary>
        /// <param name="path">file path</param>
        /// <returns>MD5 hash</returns>
        public static string GetFileMd5Hash(string path) {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(path);
            return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
        }

        public static long GetFileSize(string path) {
            return new FileInfo(path).Length;
        }

        public static bool IsPositiveNumber(string str) {
            try {
                var result = int.Parse(str);
                return result > 0;
            }
            catch {
                return false;
            }
        }
    }

    public static class Extensions {
        public static void TryToReportProgress(this BackgroundWorker bgw, int percentProgress) {
            try {
                bgw.ReportProgress(percentProgress, null);
            }
            catch (Exception) {
                // ignored
            }
        }

        public static void TryToReportProgress(this BackgroundWorker bgw, int percentProgress, object? userState) {
            try {
                bgw.ReportProgress(percentProgress, userState);
            }
            catch (Exception) {
                // ignored
            }
        }
    }
}
