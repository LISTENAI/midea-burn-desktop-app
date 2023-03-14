namespace ListenAI.Factory.FirmwareDeploy {
    internal static class Program {
        private static Mutex mutex = null;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            const string appName = "ListenAI.Factory.FirmwareDeploy";

            mutex = new Mutex(true, appName, out var createdNew);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (!createdNew) {
                MessageBox.Show("工具已打开，请勿重复打开", "重复运行");
                return;
            }

            if (!CheckTools()) {
                MessageBox.Show("烧录工具缺失，请重新安装！", "错误");
                return;
            }

            Application.Run(new MainForm());
        }

        static bool CheckTools() {
            var asrPath = Path.Combine(Environment.CurrentDirectory, "tools", "ASR_downloader_V1.0.6.exe");
            var cskPath = Path.Combine(Environment.CurrentDirectory, "tools", "Uart_Burn_Tool_v2.exe");
            if (!File.Exists(asrPath) || !File.Exists(cskPath)) {
                return false;
            }

            return true;
        }
    }
}