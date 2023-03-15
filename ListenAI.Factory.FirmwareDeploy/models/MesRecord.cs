namespace ListenAI.Factory.FirmwareDeploy.Models {
    public class MesRecord {
        public string MesCmdId { get; set; } = "None";

        public string ProductId { get; set; } = "None";

        public string ProductName { get; set; } = "None";

        public string ProductModel { get; set; } = "None";

        public string FlashOperator { get; set; } = "None";

        public string FlashToolName { get; set; } = "None";

        public string FlashMachineId { get; set; } = "None";

        #region Database config (for state retaining purpose only)

        public string DbIp { get; set; } = string.Empty;

        public string DbName { get; set; } = string.Empty;

        public string DbUsername { get; set; } = string.Empty;

        public string DbPassword { get; set; } = string.Empty;

        public string DbTableName { get; set; } = string.Empty;

        #endregion
    }
}
