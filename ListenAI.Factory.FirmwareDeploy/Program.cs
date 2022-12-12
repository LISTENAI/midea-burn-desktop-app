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
                MessageBox.Show("另一个实例已经在运行，本实例退出。", "重复运行");
                return;
            }
            Application.Run(new MainForm());
        }
    }
}