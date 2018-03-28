using Newtonsoft.Json;

namespace Transmission.Api.Entities
{
    public class FileStats
    {
        [JsonProperty("bytesCompleted")]
        public ulong BytesCompleted { get; set; }
        [JsonProperty("wanted")]
        public bool Wanted { get; set; }
        [JsonProperty("priority")]
        public int Priority { get; set; }
    }
}
