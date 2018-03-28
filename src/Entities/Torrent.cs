using Newtonsoft.Json;

namespace Transmission.Api.Entities
{
    public class Torrent
    {
        /// <summary>
        /// Unix timestamp. The last time we uploaded or downloaded piece data on this torrent.
        /// </summary>
        [JsonProperty("activityDate")]
        public int ActivityDate { get; set; }
        /// <summary>
        /// Unix Timestamp. When the torrent was first added.
        /// </summary>
        [JsonProperty("addedDate")]
        public int AddedDate { get; set; }
        [JsonProperty("bandwidthPriority")]
        public sbyte BandwidthPriority { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        /// <summary>
        /// Byte count of all the corrupt data you've ever downloaded for
        /// this torrent.If you're on a poisoned torrent, this number can
        /// grow very large.
        /// </summary>
        [JsonProperty("corruptEver")]
        public ulong CorruptEver { get; set; }
        [JsonProperty("creator")]
        public string Creator { get; set; }
        [JsonProperty("dateCreated")]
        public int DateCreated { get; set; }
        /// <summary>
        /// Byte count of all the piece data we want and don't have yet,
        /// but that a connected peer does have. [0...<see cref="LeftUntilDone"/>]
        /// </summary>
        [JsonProperty("desiredAvailable")]
        public ulong DesiredAvailable { get; set; }
        /// <summary>
        /// Unix Timestamp. When the torrent finished downloading.
        /// </summary>
        [JsonProperty("doneDate")]
        public int DoneDate { get; set; }
        [JsonProperty("downloadDir")]
        public string DownloadDir { get; set; }
        /// <summary>
        /// Byte count of all the non-corrupt data you've ever downloaded
        /// for this torrent.If you deleted the files and downloaded a second
        /// time, this will be 2*<see cref="TotalSize"/>..
        /// </summary>
        [JsonProperty("downloadedEver")]
        public ulong DownloadedEver { get; set; }
        [JsonProperty("downloadLimit")]
        public int DownloadLimit { get; set; }
        [JsonProperty("downloadLimited")]
        public bool DownloadLimited { get; set; }
        /// <summary>
        /// Defines what kind of text is in errorString.
        /// </summary>
        [JsonProperty("error")]
        public Error Error { get; set; }
        /// <summary>
        /// A warning or error message regarding the torrent.
        /// </summary>
        [JsonProperty("errorString")]
        public string ErrorString { get; set; }
        /// <summary>
        /// If downloading, estimated number of seconds left until the torrent is done.
        /// If seeding, estimated number of seconds left until seed ratio is reached.
        /// </summary>
        [JsonProperty("eta")]
        public int Eta { get; set; }
        /// <summary>
        /// If seeding, number of seconds left until the idle time limit is reached.
        /// </summary>
        [JsonProperty("etaIdle")]
        public int EtaIdle { get; set; }
        [JsonProperty("files")]
        public File[] Files { get; set; }
        [JsonProperty("fileStats")]
        public FileStats[] FileStats { get; set; }
        [JsonProperty("hashString")]
        public string HashString { get; set; }
        /// <summary>
        /// Byte count of all the partial piece data we have for this torrent.
        /// As pieces become complete, this value may decrease as portions of it
        /// are moved to <see cref="CorruptEver"/> or <see cref="HaveValid"/>.
        /// </summary>
        [JsonProperty("haveUnchecked")]
        public ulong HaveUnchecked { get; set; }
        /// <summary>
        /// Byte count of all the checksum-verified data we have for this torrent.
        /// </summary>
        [JsonProperty("haveValid")]
        public ulong HaveValid { get; set; }
        [JsonProperty("honorsSessionLimits")]
        public bool HonorsSessionLimits { get; set; }
        /// <summary>
        /// The torrent's unique Id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// A torrent is considered finished if it has met its seed ratio.
        /// As a result, only paused torrents can be finished.
        /// </summary>
        [JsonProperty("isFinished")]
        public bool IsFinished { get; set; }
        [JsonProperty("isPrivate")]
        public bool IsPrivate { get; set; }
        /// <summary>
        /// True if the torrent is running, but has been idle for long enough
        /// to be considered stalled.
        /// </summary>
        [JsonProperty("isStalled")]
        public bool IsStalled { get; set; }
        /// <summary>
        /// Byte count of how much data is left to be downloaded until we've got
        /// all the pieces that we want. [0...<see cref="SizeWhenDone"/>]
        /// </summary>
        [JsonProperty("leftUntilDone")]
        public ulong LeftUntilDone { get; set; }
        [JsonProperty("magnetLink")]
        public string MagnetLink { get; set; }
        /// <summary>
        /// Unix Timestamp. Time when one or more of the torrent's trackers will
        /// allow you to manually ask for more peers,
        /// or 0 if you can't.
        /// </summary>
        [JsonProperty("manualAnnounceTime")]
        public int ManualAnnounceTime { get; set; }
        [JsonProperty("maxConnectedPeers")]
        public int MaxConnectedPeers { get; set; }
        /// <summary>
        /// How much of the metadata the torrent has.
        /// For torrents added from a.torrent this will always be 1.
        /// For magnet links, this number will from from 0 to 1 as the metadata is downloaded.
        /// Range is [0..1]
        /// </summary>
        [JsonProperty("metadataPercentComplete")]
        public double MetadataPercentComplete { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("peer-limit")]
        public int PeerLimit { get; set; }
        [JsonProperty("peers")]
        public Peer[] Peers { get; set; }
        /// <summary>
        /// Number of peers that we're connected to
        /// </summary>
        [JsonProperty("peersConnected")]
        public int PeersConnected { get; set; }
        /// <summary>
        /// How many peers we found out about from the tracker, or from pex,
        /// or from incoming connections, or from our resume file.
        /// </summary>
        [JsonProperty("peersFrom")]
        public PeersFrom PeersFrom { get; set; }
        /// <summary>
        /// Number of peers that we're sending data to.
        /// </summary>
        [JsonProperty("peersGettingFromUs")]
        public int PeersGettingFromUs { get; set; }
        /// <summary>
        /// Number of peers that are sending data to us.
        /// </summary>
        [JsonProperty("peersSendingToUs")]
        public int PeersSendingToUs { get; set; }
        /// <summary>
        /// How much has been downloaded of the entire torrent.
        /// Range is [0..1]
        /// </summary>
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
        public uint PieceSize { get; set; }
        [JsonProperty("priorities")]
        public sbyte[] Priorities { get; set; }
        /// <summary>
        /// This torrent's queue position.
        /// All torrents have a queue position, even if it's not queued.
        /// </summary>
        [JsonProperty("queuePosition")]
        public int QueuePosition { get; set; }
        [JsonProperty("rateDownload")]
        public int RateDownload { get; set; }
        [JsonProperty("rateUpload")]
        public int RateUpload { get; set; }
        /// <summary>
        /// When <see cref="Status"/> is <see cref="Status.Check"/> or <see cref="Status.CheckWait"/>,
        /// this is the percentage of how much of the files has been
        /// verified.When it gets to 1, the verify process is done.
        /// Range is [0..1]
        /// </summary>
        [JsonProperty("recheckProgress")]
        public double RecheckProgress { get; set; }
        /// <summary>
        /// Cumulative seconds the torrent's ever spent downloading
        /// </summary>
        [JsonProperty("secondsDownloading")]
        public int SecondsDownloading { get; set; }
        /// <summary>
        /// Cumulative seconds the torrent's ever spent seeding
        /// </summary>
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
        /// <summary>
        /// Byte count of all the piece data we'll have downloaded when we're done,
        /// whether or not we have it yet.This may be less than <see cref="TotalSize"/>
        /// if only some of the torrent's files are wanted.
        /// [0...<see cref="TotalSize"/>]
        /// </summary>
        [JsonProperty("sizeWhenDone")]
        public ulong SizeWhenDone { get; set; }
        /// <summary>
        /// Unix timestamp. When the torrent was last started.
        /// </summary>
        [JsonProperty("startDate")]
        public int StartDate { get; set; }
        /// <summary>
        /// What is this torrent doing right now?
        /// </summary>
        [JsonProperty("status")]
        public Status Status { get; set; }
        [JsonProperty("trackers")]
        public Tracker[] Trackers { get; set; }
        [JsonProperty("trackerStats")]
        public TrackerStats[] TrackerStats { get; set; }
        [JsonProperty("totalSize")]
        public ulong TotalSize { get; set; }
        [JsonProperty("torrentFile")]
        public string TorrentFile { get; set; }
        /// <summary>
        /// Byte count of all data you've ever uploaded for this torrent.
        /// </summary>
        [JsonProperty("uploadedEver")]
        public ulong UploadedEver { get; set; }
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

    /// <summary>
    /// What the torrent is doing right now.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Torrent is stopped
        /// </summary>
        Stopped,
        /// <summary>
        /// Queued to check files
        /// </summary>
        CheckWait,
        /// <summary>
        /// Checking files
        /// </summary>
        Check,
        /// <summary>
        /// Queued to download
        /// </summary>
        DownloadWait,
        /// <summary>
        /// Downloading
        /// </summary>
        Download,
        /// <summary>
        /// Queued to seed
        /// </summary>
        SeedWait,
        /// <summary>
        /// Seeding
        /// </summary>
        Seed,
    }

    /// <summary>
    /// Defines what kind of text is in errorString.
    /// </summary>
    public enum Error
    {
        /// <summary>
        /// everything's fine
        /// </summary>
        OK,
        /// <summary>
        /// when we anounced to the tracker, we got a warning in the response
        /// </summary>
        TrackerWarning,
        /// <summary>
        /// when we anounced to the tracker, we got an error in the response
        /// </summary>
        TrackerError,
        /// <summary>
        /// local trouble, such as disk full or permissions error
        /// </summary>
        LocalError,
    }
}
