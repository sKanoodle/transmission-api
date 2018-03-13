using Newtonsoft.Json;

namespace Transmission.Api.Entities
{
    public class TrackerStats
    {
        [JsonProperty("announce")]
        public string Announce { get; set; }
        [JsonProperty("announceState")]
        public int AnnounceState { get; set; }
        [JsonProperty("downloadCount")]
        public int DownloadCount { get; set; }
        [JsonProperty("hasAnnounced")]
        public bool HasAnnounced { get; set; }
        [JsonProperty("hasScraped")]
        public bool HasScraped { get; set; }
        [JsonProperty("host")]
        public string Host { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("isBackup")]
        public bool IsBackup { get; set; }
        [JsonProperty("lastAnnouncePeerCount")]
        public int LastAnnouncePeerCount { get; set; }
        [JsonProperty("lastAnnounceResult")]
        public string LastAnnounceResult { get; set; }
        [JsonProperty("lastAnnounceStartTime")]
        public int LastAnnounceStartTime { get; set; }
        [JsonProperty("lastAnnounceSucceeded")]
        public bool LastAnnounceSucceeded { get; set; }
        [JsonProperty("lastAnnounceTime")]
        public int LastAnnounceTime { get; set; }
        [JsonProperty("lastAnnounceTimedOut")]
        public bool LastAnnounceTimedOut { get; set; }
        [JsonProperty("lastScrapeResult")]
        public string LastScrapeResult { get; set; }
        [JsonProperty("lastScrapeStartTime")]
        public int LastScrapeStartTime { get; set; }
        [JsonProperty("lastScrapeSucceeded")]
        public string LastScrapeSucceeded { get; set; }
        [JsonProperty("lastScrapeTime")]
        public int LastScrapeTime { get; set; }
        [JsonProperty("lastScrapeTimedOut")]
        public bool LastScrapeTimedOut { get; set; }
        [JsonProperty("leecherCount")]
        public int LeecherCount { get; set; }
        [JsonProperty("nextAnnounceTime")]
        public int NextAnnounceTime { get; set; }
        [JsonProperty("nextScrapeTime")]
        public int NextScrapeTime { get; set; }
        [JsonProperty("scrape")]
        public string Scrape { get; set; }
        [JsonProperty("scrapeState")]
        public int ScrapeState { get; set; }
        [JsonProperty("seederCount")]
        public int SeederCount { get; set; }
        [JsonProperty("tier")]
        public int Tier { get; set; }
    }
}
