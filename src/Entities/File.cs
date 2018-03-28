using Newtonsoft.Json;

namespace Transmission.Api.Entities
{
    public class File
    {
        [JsonProperty("bytesCompleted")]
        public ulong BytesCompleted { get; set; }
        [JsonProperty("length")]
        public ulong Length { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
