namespace ListenAI.Factory.FirmwareDeploy {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (!CheckTools()) {
                MessageBox.Show("烧录工具缺失，请重新安装！", "错误");
                return;
            }

            Application.Run(new MainForm());
        }

        static bool CheckTools() {
            var cskPath = Path.Combine(Environment.CurrentDirectory, "tools", "Uart_Burn_Tool_v2.exe");
            if (!File.Exists(cskPath)) {
                return false;
            }

            return true;
        }
    }
}