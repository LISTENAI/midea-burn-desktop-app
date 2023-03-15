using Newtonsoft.Json;

namespace ListenAI.Factory.FirmwareDeploy.Models {
    public class UiConfig {
        [JsonProperty("portConfig")]
        public List<PortConfig> PortConfig { get; set; } = new();

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }

        public static UiConfig? FromJson(string json) {
            try {
                return JsonConvert.DeserializeObject<UiConfig>(json);
            }
            catch {
                return null;
            }
        }
    }

    public class PortConfig {
        [JsonProperty("groupId")]
        public int GroupId { get; set; }

        [JsonProperty("type")]
        public PortConfigType Type { get; set; }

        [JsonProperty("port", Required = Required.Always)]
        public string? Port { get; set; }

        [JsonProperty("baudRate")]
        public long BaudRate { get; set; }
    }

    public enum PortConfigType {
        Csk = 0,
        Wifi = 1
    }
}
