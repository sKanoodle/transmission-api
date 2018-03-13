using Newtonsoft.Json;

namespace Transmission.Api.Entities
{
    public class PeersFrom
    {
        [JsonProperty("fromCache")]
        public int FromCache { get; set; }
        [JsonProperty("fromDht")]
        public int FromDht { get; set; }
        [JsonProperty("fromIncoming")]
        public int FromIncoming { get; set; }
        [JsonProperty("fromLpd")]
        public int FromLpd { get; set; }
        [JsonProperty("fromLtep")]
        public int fromLtep { get; set; }
        [JsonProperty("fromPex")]
        public int FromPex { get; set; }
        [JsonProperty("fromTracker")]
        public int FromTracker { get; set; }
    }
}
