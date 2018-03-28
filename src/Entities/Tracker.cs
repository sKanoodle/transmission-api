using Newtonsoft.Json;

namespace Transmission.Api.Entities
{
    public class Tracker
    {
        [JsonProperty("announce")]
        public string Announce { get; set; }
        [JsonProperty("id")]
        public uint Id { get; set; }
        [JsonProperty("scrape")]
        public string Scrape { get; set; }
        [JsonProperty("tier")]
        public int Tier { get; set; }
    }
}
