using Newtonsoft.Json;

namespace Transmission.Api.Entities
{
    public class File
    {
        [JsonProperty("bytesCompleted")]
        public long BytesCompleted { get; set; }
        [JsonProperty("length")]
        public long Length { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
