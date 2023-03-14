using Newtonsoft.Json;

namespace ListenAI.Factory.FirmwareDeploy.Models {
    internal class FwPkgMetadata {
        [JsonProperty("pkg_ver")]
        public string Version { get; set; }

        [JsonProperty("files")]
        public List<FwPkgFileMetadata> Files { get; set; } = new();
    }

    internal class FwPkgFileMetadata {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ver")]
        public string Version { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; } = 0;

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("md5")]
        public string Hash { get; set; }
    }
}
