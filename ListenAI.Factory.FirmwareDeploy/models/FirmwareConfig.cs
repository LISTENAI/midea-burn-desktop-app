using Newtonsoft.Json;
using static ListenAI.Factory.FirmwareDeploy.Constants;

namespace ListenAI.Factory.FirmwareDeploy.Models {
    public class FirmwareConfig {
        public string FullPath { get; set; }

        [JsonProperty("pkg_ver", Required = Required.Always)]
        public string PackageVersion { get; set; }

        [JsonProperty("files", Required = Required.Always)]
        public List<FirmwareFile> Files { get; set; }

        public static FirmwareConfig? FromJson(string json) {
            try {
                return JsonConvert.DeserializeObject<FirmwareConfig>(json);
            }
            catch {
                return null;
            }
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }

        public FirmwareFile? GetFirmware(GroupType type) {
            if (type == GroupType.Common) {
                return null;
            }

            try {
                return Files.First(f => f.Id == (int)type);
            }
            catch {
                return null;
            }
        }
    }

    public class FirmwareFile {
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("ver", Required = Required.Always)]
        public string Version { get; set; }

        [JsonProperty("offset", Required = Required.Always)]
        public int Offset { get; set; }

        [JsonProperty("size", Required = Required.Always)]
        public long Size { get; set; }

        [JsonProperty("md5", Required = Required.Always)]
        public string Checksum { get; set; }
    }
}
