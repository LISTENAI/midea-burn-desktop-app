using ListenAI.Factory.FirmwareDeploy.Models;

namespace ListenAI.Factory.FirmwareDeploy {
    public static class Global {
        public static FirmwareConfig? SelectedFirmware;
        public static short GroupCount = 1;
        public static Dictionary<int, Dictionary<string, Control>> ControlGroups = new();
        public static MesRecord? MesRecord = null;
        public static Dictionary<int, LineWorker> WorkersPool = new();
        public static Dictionary<int, bool> IsCustomSnEnabled = new();
        public static object LogOperationLock = new();
        public static EventHandler AllWorkersCompleted;
        public static EventHandler FailsafeMode;
        public static Constants.WorkingMode WorkingMode = Constants.WorkingMode.OnlineAndOffline;

        public static int NextSerialNumber = 0;
    }

    public static class Constants {
        public static Color ColorProcceed = Color.FromArgb(0, 192, 0);
        public static Color ColorBlock = Color.FromArgb(255, 0, 0);
        public static Color ColorProcessing = Color.Yellow;
        public static Color ColorAbleToSelect = SystemColors.Highlight;
        public static Color ColorCsk6PanelBackground = Color.FromArgb(247, 252, 254);
        public static Color ColorWifiPanelBackground = Color.FromArgb(253, 245, 234);
        public static string BurnToolExeName = "Uart_Burn_Tool_v3.exe";
        public static string ASRToolExeName = "ASR_downloader_V1.0.6.exe";
        public static string BurnToolPath = Path.Combine(Environment.CurrentDirectory, "tools", BurnToolExeName);
        public static string ASRToolPath = Path.Combine(Environment.CurrentDirectory, "tools", ASRToolExeName);
        public static string UiConfigPath = Path.Combine(Environment.CurrentDirectory, "config.json");
        public static string LogDirPath = Path.Combine(Environment.CurrentDirectory, "logs");

        public static byte[] ValidAsrFirmwareHeader = {
            0xff, 0x41, 0x53, 0x52, 0x2d, 0x49, 0x6f, 0x54
        };

        public static string GetControlName(int groupId, GroupType groupType, GroupConfigType configType) {
            string result;
            switch (configType) {
                case GroupConfigType.IsDefault:
                    result = "cb";
                    break;
                case GroupConfigType.Result:
                    result = "btn";
                    break;
                case GroupConfigType.Progress:
                    result = "pb";
                    break;
                case GroupConfigType.Port:
                    result = "cmb";
                    break;
                case GroupConfigType.Panel:
                    result = "panel";
                    break;
                default:
                    result = "tb";
                    break;
            }
            result += Enum.GetName(typeof(GroupType), groupType);
            result += groupId.ToString();
            result += (int)configType != 99 ? Enum.GetName(typeof(GroupConfigType), configType) : "";

            return result;
        }

        /// <summary>
        /// Get dynamically generated control by name
        /// </summary>
        /// <param name="groupId">group id</param>
        /// <param name="groupType">group type</param>
        /// <param name="configType">config type</param>
        /// <returns></returns>
        public static Control? GetControl(int groupId, GroupType groupType, GroupConfigType configType) {
            var controlName = GetControlName(groupId, groupType, configType);
            return Global.ControlGroups[groupId]?[controlName];
        }

        public enum GroupType {
            Csk = 0,
            Wifi = 1,
            Common = 2
        }

        public enum GroupConfigType {
            Port = 0,
            BaudRate = 1,
            Databits = 2,
            Parity = 3,
            Stopbits = 4,
            IsDefault = 5,
            Result = 6,
            Serial = 7,
            Progress = 8,
            Panel = 99
        }

        public enum WorkerState {
            Idle = 0,
            Processing = 1,
            Success = 2,
            Error = 3
        }

        public enum FirmwareType {
            Csk = 0,
            Asr = 1
        }

        public enum WorkingMode {
            OnlineAndOffline = 0,
            OfflineOnly = 1
        }
    }
}
