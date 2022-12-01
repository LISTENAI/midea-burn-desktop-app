namespace ListenAI.Factory.FirmwareDeploy {
    public static class Global {
        public static string SelectedFirmwarePath = "";
        public static short GroupCount = 1;
        public static Dictionary<int, Dictionary<string, Control>> ControlGroups = new();
        public static List<LineWorker> WorkersPool = new();

        public static int NextSerialNumber = 0;
    }

    public static class Constants {
        public static Color ColorProcceed = Color.FromArgb(0, 192, 0);
        public static Color ColorBlock = Color.FromArgb(255, 0, 0);
        public static Color ColorProcessing = Color.Yellow;
        public static Color ColorCsk6PanelBackground = Color.FromArgb(247, 252, 254);
        public static Color ColorWifiPanelBackground = Color.FromArgb(253, 245, 234);
        public static string BurnToolPath = Path.Combine(Environment.CurrentDirectory, "tools", "Uart_Burn_Tool.exe");

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
                default:
                    result = "tb";
                    break;
            }
            result += Enum.GetName(typeof(GroupType), groupType);
            result += groupId.ToString();
            result += Enum.GetName(typeof(GroupConfigType), configType);

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
            Progress = 8
        }

        public enum WorkerState {
            Idle = 0,
            Processing = 1,
            Success = 2,
            Error = 3
        }
    }
}
