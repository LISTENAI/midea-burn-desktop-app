namespace ListenAI.Factory.FirmwareDeploy {
    public class ListenAiException : Exception {
        public int Code { get; set; }

        public int SubCode { get; set; }

        public string Details { get; set; }

        public ListenAiException(int code, string message, string details, int subCode = 0) : base(message) {
            Code = code;
            SubCode = subCode;
            Details = details;
        }

        public ListenAiException(int code, string message, string details, Exception innerException, int subCode = 0) : base(message, innerException) {
            Code = code;
            SubCode = subCode;
            Details = details;
        }
    }
}
