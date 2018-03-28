using Newtonsoft.Json;

namespace Transmission.Api.Entities
{
    public class TrackerStats
    {
        /// <summary>
        /// the full announce URL
        /// </summary>
        [JsonProperty("announce")]
        public string Announce { get; set; }
        /// <summary>
        /// is the tracker announcing, waiting, queued, etc
        /// </summary>
        [JsonProperty("announceState")]
        public TrackerState AnnounceState { get; set; }
        /// <summary>
        /// how many downloads this tracker knows of (-1 means it does not know)
        /// </summary>
        [JsonProperty("downloadCount")]
        public int DownloadCount { get; set; }
        /// <summary>
        /// whether or not we've ever sent this tracker an announcement
        /// </summary>
        [JsonProperty("hasAnnounced")]
        public bool HasAnnounced { get; set; }
        /// <summary>
        /// whether or not we've ever scraped to this tracker
        /// </summary>
        [JsonProperty("hasScraped")]
        public bool HasScraped { get; set; }
        /// <summary>
        /// human-readable string identifying the tracker
        /// </summary>
        [JsonProperty("host")]
        public string Host { get; set; }
        /// <summary>
        /// used to match to a tr_tracker_info
        /// </summary>
        [JsonProperty("id")]
        public uint Id { get; set; }
        /// <summary>
        /// Transmission uses one tracker per tier,
        /// and the others are kept as backups
        /// </summary>
        [JsonProperty("isBackup")]
        public bool IsBackup { get; set; }
        /// <summary>
        /// number of peers the tracker told us about last time.
        /// if <see cref="LastAnnounceSucceeded"/> is false, this field is undefined
        /// </summary>
        [JsonProperty("lastAnnouncePeerCount")]
        public int LastAnnouncePeerCount { get; set; }
        /// <summary>
        /// human-readable string with the result of the last announce.
        /// if <see cref="HasAnnounced"/> is false, this field is undefined
        /// </summary>
        [JsonProperty("lastAnnounceResult")]
        public string LastAnnounceResult { get; set; }
        /// <summary>
        /// Unix timestamp. When the last announce was sent to the tracker.
        /// If <see cref="HasAnnounced"/> is false, this field is undefined.
        /// </summary>
        [JsonProperty("lastAnnounceStartTime")]
        public int LastAnnounceStartTime { get; set; }
        /// <summary>
        /// whether or not the last announce was a success.
        /// if <see cref="HasAnnounced"/> is false, this field is undefined
        /// </summary>
        [JsonProperty("lastAnnounceSucceeded")]
        public bool LastAnnounceSucceeded { get; set; }
        /// <summary>
        /// Unix timestamp. when the last announce was completed.
        /// if <see cref="HasAnnounced"/> is false, this field is undefined
        /// </summary>
        [JsonProperty("lastAnnounceTime")]
        public int LastAnnounceTime { get; set; }
        /// <summary>
        /// Whether or not the last announce timed out.
        /// </summary>
        [JsonProperty("lastAnnounceTimedOut")]
        public bool LastAnnounceTimedOut { get; set; }
        /// <summary>
        /// human-readable string with the result of the last scrape.
        /// if <see cref="HasScraped"/> is false, this field is undefined
        /// </summary>
        [JsonProperty("lastScrapeResult")]
        public string LastScrapeResult { get; set; }
        /// <summary>
        /// Unix timestamp. when the last scrape was sent to the tracker.
        /// if <see cref="HasScraped"/> is false, this field is undefined
        /// </summary>
        [JsonProperty("lastScrapeStartTime")]
        public int LastScrapeStartTime { get; set; }
        /// <summary>
        /// whether or not the last scrape was a success.
        /// if <see cref="HasAnnounced"/> is false, this field is undefined
        /// </summary>
        [JsonProperty("lastScrapeSucceeded")]
        public string LastScrapeSucceeded { get; set; }
        /// <summary>
        /// when the last scrape was completed.
        /// if <see cref="HasScraped"/> is false, this field is undefined
        /// </summary>
        [JsonProperty("lastScrapeTime")]
        public int LastScrapeTime { get; set; }
        /// <summary>
        /// whether or not the last scrape timed out.
        /// </summary>
        [JsonProperty("lastScrapeTimedOut")]
        public bool LastScrapeTimedOut { get; set; }
        /// <summary>
        /// number of leechers this tracker knows of (-1 means it does not know)
        /// </summary>
        [JsonProperty("leecherCount")]
        public int LeecherCount { get; set; }
        /// <summary>
        /// Unix timestamp. when the next periodic announce message will be sent out.
        /// if <see cref="AnnounceState"/> isn't <see cref="TrackerState.Waiting"/>, this field is undefined
        /// </summary>
        [JsonProperty("nextAnnounceTime")]
        public int NextAnnounceTime { get; set; }
        /// <summary>
        /// Unix timestamp. when the next periodic scrape message will be sent out.
        /// if <see cref="ScrapeState"/> isn't <see cref="TrackerState.Waiting"/>, this field is undefined
        /// </summary>
        [JsonProperty("nextScrapeTime")]
        public int NextScrapeTime { get; set; }
        /// <summary>
        /// the full scrape URL
        /// </summary>
        [JsonProperty("scrape")]
        public string Scrape { get; set; }
        /// <summary>
        /// is the tracker scraping, waiting, queued, etc
        /// </summary>
        [JsonProperty("scrapeState")]
        public TrackerState ScrapeState { get; set; }
        /// <summary>
        /// number of seeders this tracker knows of (-1 means it does not know)
        /// </summary>
        [JsonProperty("seederCount")]
        public int SeederCount { get; set; }
        /// <summary>
        /// which tier this tracker is in
        /// </summary>
        [JsonProperty("tier")]
        public int Tier { get; set; }
    }

    public enum TrackerState
    {
        /// <summary>
        /// we won't (announce,scrape) this torrent to this tracker because
        /// the torrent is stopped, or because of an error, or whatever
        /// </summary>
        Inactive,
        /// <summary>
        /// we will (announce,scrape) this torrent to this tracker, and are
        /// waiting for enough time to pass to satisfy the tracker's interval
        /// </summary>
        Waiting,
        /// <summary>
        /// it's time to (announce,scrape) this torrent, and we're waiting on a
        /// a free slot to open up in the announce manager
        /// </summary>
        Queued,
        /// <summary>
        /// we're (announcing,scraping) this torrent right now
        /// </summary>
        Active,
    }
}
