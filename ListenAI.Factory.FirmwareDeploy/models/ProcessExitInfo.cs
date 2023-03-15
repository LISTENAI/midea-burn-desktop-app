namespace ListenAI.Factory.FirmwareDeploy.Models {
    public class ProcessExitInfo {
        public int ExitCode { get; set; }

        public string? StdOut { get; set; }

        public string? StdErr { get; set; }
    }
}
