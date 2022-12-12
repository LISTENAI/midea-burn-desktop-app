namespace ListenAI.Factory.FirmwareDeploy.Models {
    public class MesRecord {
        public string MesCmdId { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductModel { get; set; }

        public string FlashOperator { get; set; }

        public string FlashToolName { get; set; }

        public string FlashMachineId { get; set; }

        #region Database config (for state retaining purpose only)

        public string DbIp { get; set; }

        public string DbName { get; set; }

        public string DbUsername { get; set; }

        public string DbPassword { get; set; }

        public string DbTableName { get; set; }

        #endregion
    }
}
