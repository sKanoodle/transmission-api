using Newtonsoft.Json;

namespace Transmission.Api.Entities
{
    public class Torrent
    {
        [JsonProperty("activityDate")]
        public int ActivityDate { get; set; }
        [JsonProperty("addedDate")]
        public int AddedDate { get; set; }
        [JsonProperty("bandwidthPriority")]
        public int BandwidthPriority { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [JsonProperty("corruptEver")]
        public int CorruptEver { get; set; }
        [JsonProperty("creator")]
        public string Creator { get; set; }
        [JsonProperty("dateCreated")]
        public int DateCreated { get; set; }
        [JsonProperty("desiredAvailable")]
        public long DesiredAvailable { get; set; }
        [JsonProperty("doneDate")]
        public int DoneDate { get; set; }
        [JsonProperty("downloadDir")]
        public string DownloadDir { get; set; }
        [JsonProperty("downloadedEver")]
        public long DownloadedEver { get; set; }
        [JsonProperty("downloadLimit")]
        public int DownloadLimit { get; set; }
        [JsonProperty("downloadLimited")]
        public bool DownloadLimited { get; set; }
        [JsonProperty("error")]
        public int Error { get; set; }
        [JsonProperty("errorString")]
        public string ErrorString { get; set; }
        [JsonProperty("eta")]
        public int Eta { get; set; }
        [JsonProperty("etaIdle")]
        public int EtaIdle { get; set; }
        [JsonProperty("files")]
        public File[] Files { get; set; }
        [JsonProperty("fileStats")]
        public FileStats[] FileStats { get; set; }
        [JsonProperty("hashString")]
        public string HashString { get; set; }
        [JsonProperty("haveUnchecked")]
        public int HaveUnchecked { get; set; }
        [JsonProperty("haveValid")]
        public long HaveValid { get; set; }
        [JsonProperty("honorsSessionLimits")]
        public bool HonorsSessionLimits { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("isFinished")]
        public bool IsFinished { get; set; }
        [JsonProperty("isPrivate")]
        public bool IsPrivate { get; set; }
        [JsonProperty("isStalled")]
        public bool IsStalled { get; set; }
        [JsonProperty("leftUntilDone")]
        public long LeftUntilDone { get; set; }
        [JsonProperty("magnetLink")]
        public string MagnetLink { get; set; }
        [JsonProperty("manualAnnounceTime")]
        public int ManualAnnounceTime { get; set; }
        [JsonProperty("maxConnectedPeers")]
        public int MaxConnectedPeers { get; set; }
        [JsonProperty("metadataPercentComplete")]
        public double MetadataPercentComplete { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("peer-limit")]
        public int PeerLimit { get; set; }
        [JsonProperty("peers")]
        public Peer[] Peers { get; set; }
        [JsonProperty("peersConnected")]
        public int PeersConnected { get; set; }
        [JsonProperty("peersFrom")]
        public PeersFrom PeersFrom { get; set; }
        [JsonProperty("peersGettingFromUs")]
        public int PeersGettingFromUs { get; set; }
        [JsonProperty("peersSendingToUs")]
        public int PeersSendingToUs { get; set; }
        [JsonProperty("percentDone")]
        public double PercentDone { get; set; }
        /// <summary>
        /// A bitfield holding pieceCount flags which are set to 'true' if we have the piece matching that position. JSON doesn't allow raw binary data, so this is a base64-encoded string.
        /// </summary>
        [JsonProperty("pieces")]
        public string Pieces { get; set; }
        [JsonProperty("pieceCount")]
        public int PieceCount { get; set; }
        [JsonProperty("pieceSize")]
        public int PieceSize { get; set; }
        [JsonProperty("priorities")]
        public int[] Priorities { get; set; }
        [JsonProperty("queuePosition")]
        public int QueuePosition { get; set; }
        [JsonProperty("rateDownload")]
        public int RateDownload { get; set; }
        [JsonProperty("rateUpload")]
        public int RateUpload { get; set; }
        [JsonProperty("recheckProgress")]
        public double RecheckProgress { get; set; }
        [JsonProperty("secondsDownloading")]
        public int SecondsDownloading { get; set; }
        [JsonProperty("secondsSeeding")]
        public int SecondsSeeding { get; set; }
        [JsonProperty("seedIdleLimit")]
        public int SeedIdleLimit { get; set; }
        [JsonProperty("seedIdleMode")]
        public int SeedIdleMode { get; set; }
        [JsonProperty("seedRatioLimit")]
        public double SeedRatioLimit { get; set; }
        [JsonProperty("seedRatioMode")]
        public int SeedRatioMode { get; set; }
        [JsonProperty("sizeWhenDone")]
        public long SizeWhenDone { get; set; }
        [JsonProperty("startDate")]
        public int StartDate { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("trackers")]
        public Tracker[] Trackers { get; set; }
        [JsonProperty("trackerStats")]
        public TrackerStats[] TrackerStats { get; set; }
        [JsonProperty("totalSize")]
        public long TotalSize { get; set; }
        [JsonProperty("torrentFile")]
        public string TorrentFile { get; set; }
        [JsonProperty("uploadedEver")]
        public long UploadedEver { get; set; }
        [JsonProperty("uploadLimit")]
        public int UploadLimit { get; set; }
        [JsonProperty("uploadLimited")]
        public bool IsUploadLimited { get; set; }
        [JsonProperty("uploadRatio")]
        public double UploadRatio { get; set; }
        [JsonProperty("wanted")]
        public bool[] Wanted { get; set; }
        [JsonProperty("webseeds")]
        public string[] Webseeds { get; set; }
        [JsonProperty("webseedsSendingToUs")]
        public int WebseedsSendingToUs { get; set; }
    }
}
