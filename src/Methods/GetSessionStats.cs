using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.Api
{
    public partial class Client
    {
        public Task<Stats> GetSessionStats()
        {
            return GetResponseAsync<Stats, SessionStatsRequest>(new SessionStatsRequest());
        }
    }

    internal class SessionStatsRequest : ArgumentsBase
    {
        public override string MethodName => "session-stats";
    }

    public class Stats
    {
        [JsonProperty("activeTorrentCount")]
        public int ActiveTorrentCount { get; set; }
        [JsonProperty("downloadSpeed")]
        public int DownloadSpeed { get; set; }
        [JsonProperty("pausedTorrentCount")]
        public int PausedTorrentCount { get; set; }
        [JsonProperty("torrentCount")]
        public int TorrentCount { get; set; }
        [JsonProperty("uploadSpeed")]
        public int UploadSpeed { get; set; }


        [JsonProperty("cumulative-stats")]
        public SessionStats AllTimeStats { get; set; }
        [JsonProperty("current-stats")]
        public SessionStats CurrentStats { get; set; }
    }

    public class SessionStats
    {
        [JsonProperty("uploadedBytes")]
        public ulong UploadedBytes { get; set; }
        [JsonProperty("downloadedBytes")]
        public ulong DownloadedBytes { get; set; }
        [JsonProperty("filesAdded")]
        public int FilesAdded { get; set; }
        [JsonProperty("sessionCount")]
        public int SessionCount { get; set; }
        [JsonProperty("secondsActive")]
        public int SecondsActive { get; set; }
    }
}
