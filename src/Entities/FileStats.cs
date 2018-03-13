using Newtonsoft.Json;

namespace Transmission.Api.Entities
{
    public class FileStats
    {
        [JsonProperty("bytesCompleted")]
        public long BytesCompleted { get; set; }
        [JsonProperty("wanted")]
        public bool Wanted { get; set; }
        [JsonProperty("priority")]
        public int Priority { get; set; }
    }
}
