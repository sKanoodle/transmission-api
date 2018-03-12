using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.Api
{
    public class Client
    {
        private const string ID_HEADER = "X-Transmission-Session-Id";
        private readonly HttpClient HttpClient;
        private readonly string Address;

        public Client(string address, string user, string pass)
        {
            Address = address;
            HttpClient = new HttpClient(new HttpClientHandler { Credentials = new NetworkCredential(user, pass) });
        }

        public Client(string address)
        {
            Address = address;
            HttpClient = new HttpClient();
        }

        public async Task<Torrent[]> TorrentGetAsync(Fields fields)
        {
            var result = await TorrentGetAsync(new TorrentGetRequest<string[]> { Fields = fields.ToStringRepresentation() });
            return result.Torrents;
        }

        private async Task<TorrentGetResponse> TorrentGetAsync<T>(TorrentGetRequest<T> request)
        {
            return await GetResponseAsync<TorrentGetResponse, TorrentGetRequest<T>>(request);
        }

        private async Task<T> GetResponseAsync<T, U>(U request) where U : ArgumentsBase
        {
            var wrappedRequest = new Request<U> { Arguments = request };
            var response = await HttpClient.PostAsync(Address, new StringContent(JsonConvert.SerializeObject(wrappedRequest, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore })));
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return JsonConvert.DeserializeObject<Response<T>>(await response.Content.ReadAsStringAsync()).Data;
                case HttpStatusCode.Conflict: // 409 means header with session ID is not set, so just set header and try again
                    HttpClient.DefaultRequestHeaders.Add(ID_HEADER, response.Headers.GetValues(ID_HEADER));
                    return await GetResponseAsync<T, U>(request);
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public class Fields
    {
        public static readonly Fields All = new Fields(new BitArray(Enum.GetValues(typeof(TorrentField)).Length, true));

        public static readonly Fields ActivityDate = new Fields(TorrentField.ActivityDate);
        public static readonly Fields AddedDate = new Fields(TorrentField.AddedDate);
        public static readonly Fields BandwidthPriority = new Fields(TorrentField.BandwidthPriority);
        public static readonly Fields Comment = new Fields(TorrentField.Comment);
        public static readonly Fields CorruptEver = new Fields(TorrentField.CorruptEver);
        public static readonly Fields Creator = new Fields(TorrentField.Creator);
        public static readonly Fields DateCreated = new Fields(TorrentField.DateCreated);
        public static readonly Fields DesiredAvailable = new Fields(TorrentField.DesiredAvailable);
        public static readonly Fields DoneDate = new Fields(TorrentField.DoneDate);
        public static readonly Fields DownloadDir = new Fields(TorrentField.DownloadDir);
        public static readonly Fields DownloadedEver = new Fields(TorrentField.DownloadedEver);
        public static readonly Fields DownloadLimit = new Fields(TorrentField.DownloadLimit);
        public static readonly Fields DownloadLimited = new Fields(TorrentField.DownloadLimited);
        public static readonly Fields Error = new Fields(TorrentField.Error);
        public static readonly Fields ErrorString = new Fields(TorrentField.ErrorString);
        public static readonly Fields Eta = new Fields(TorrentField.Eta);
        public static readonly Fields EtaIdle = new Fields(TorrentField.EtaIdle);
        public static readonly Fields Files = new Fields(TorrentField.Files);
        public static readonly Fields FileStats = new Fields(TorrentField.FileStats);
        public static readonly Fields HashString = new Fields(TorrentField.HashString);
        public static readonly Fields HaveUnchecked = new Fields(TorrentField.HaveUnchecked);
        public static readonly Fields HaveValid = new Fields(TorrentField.HaveValid);
        public static readonly Fields HonorsSessionLimits = new Fields(TorrentField.HonorsSessionLimits);
        public static readonly Fields Id = new Fields(TorrentField.Id);
        public static readonly Fields IsFinished = new Fields(TorrentField.IsFinished);
        public static readonly Fields IsPrivate = new Fields(TorrentField.IsPrivate);
        public static readonly Fields IsStalled = new Fields(TorrentField.IsStalled);
        public static readonly Fields LeftUntilDone = new Fields(TorrentField.LeftUntilDone);
        public static readonly Fields MagnetLink = new Fields(TorrentField.MagnetLink);
        public static readonly Fields ManualAnnounceTime = new Fields(TorrentField.ManualAnnounceTime);
        public static readonly Fields MaxConnectedPeers = new Fields(TorrentField.MaxConnectedPeers);
        public static readonly Fields MetadataPercentComplete = new Fields(TorrentField.MetadataPercentComplete);
        public static readonly Fields Name = new Fields(TorrentField.Name);
        public static readonly Fields PeerLimit = new Fields(TorrentField.PeerLimit);
        public static readonly Fields Peers = new Fields(TorrentField.Peers);
        public static readonly Fields PeersConnected = new Fields(TorrentField.PeersConnected);
        public static readonly Fields PeersFrom = new Fields(TorrentField.PeersFrom);
        public static readonly Fields PeersGettingFromUs = new Fields(TorrentField.PeersGettingFromUs);
        public static readonly Fields PeersSendingToUs = new Fields(TorrentField.PeersSendingToUs);
        public static readonly Fields PercentDone = new Fields(TorrentField.PercentDone);
        public static readonly Fields Pieces = new Fields(TorrentField.Pieces);
        public static readonly Fields PieceCount = new Fields(TorrentField.PieceCount);
        public static readonly Fields PieceSize = new Fields(TorrentField.PieceSize);
        public static readonly Fields Priorities = new Fields(TorrentField.Priorities);
        public static readonly Fields QueuePosition = new Fields(TorrentField.QueuePosition);
        public static readonly Fields RateDownload = new Fields(TorrentField.RateDownload);
        public static readonly Fields RateUpload = new Fields(TorrentField.RateUpload);
        public static readonly Fields RecheckProgress = new Fields(TorrentField.RecheckProgress);
        public static readonly Fields SecondsDownloading = new Fields(TorrentField.SecondsDownloading);
        public static readonly Fields SecondsSeeding = new Fields(TorrentField.SecondsSeeding);
        public static readonly Fields SeedIdleLimit = new Fields(TorrentField.SeedIdleLimit);
        public static readonly Fields SeedIdleMode = new Fields(TorrentField.SeedIdleMode);
        public static readonly Fields SeedRatioLimit = new Fields(TorrentField.SeedRatioLimit);
        public static readonly Fields SeedRatioMode = new Fields(TorrentField.SeedRatioMode);
        public static readonly Fields SizeWhenDone = new Fields(TorrentField.SizeWhenDone);
        public static readonly Fields StartDate = new Fields(TorrentField.StartDate);
        public static readonly Fields Status = new Fields(TorrentField.Status);
        public static readonly Fields Trackers = new Fields(TorrentField.Trackers);
        public static readonly Fields TrackerStats = new Fields(TorrentField.TrackerStats);
        public static readonly Fields TotalSize = new Fields(TorrentField.TotalSize);
        public static readonly Fields TorrentFile = new Fields(TorrentField.TorrentFile);
        public static readonly Fields UploadedEver = new Fields(TorrentField.UploadedEver);
        public static readonly Fields UploadLimit = new Fields(TorrentField.UploadLimit);
        public static readonly Fields UploadLimited = new Fields(TorrentField.UploadLimited);
        public static readonly Fields UploadRatio = new Fields(TorrentField.UploadRatio);
        public static readonly Fields Wanted = new Fields(TorrentField.Wanted);
        public static readonly Fields Webseeds = new Fields(TorrentField.Webseeds);
        public static readonly Fields WebseedsSendingToUs = new Fields(TorrentField.WebseedsSendingToUs);

        private BitArray Array { get; }

        private Fields(TorrentField field)
        {
            Array = new BitArray(Enum.GetValues(typeof(TorrentField)).Length, false);
            Array.Set((int)field, true);
        }

        private Fields(BitArray array)
        {
            Array = array;
        }

        public static Fields operator |(Fields f1, Fields f2)
        {
            return new Fields(f1.Array.Or(f2.Array));
        }

        public string[] ToStringRepresentation()
        {
            List<string> result = new List<string>();
            for (int i = 0; i < Array.Length; i++)
                if (Array[i])
                    result.Add(LookupArray[i]);
            return result.ToArray();
        }

        /// <summary>
        /// index in array matches enum int value
        /// </summary>
        private readonly string[] LookupArray = new string[]
        {
            "activityDate",
            "addedDate",
            "bandwidthPriority",
            "comment",
            "corruptEver",
            "creator",
            "dateCreated",
            "desiredAvailable",
            "doneDate",
            "downloadDir",
            "downloadedEver",
            "downloadLimit",
            "downloadLimited",
            "error",
            "errorString",
            "eta",
            "etaIdle",
            "files",
            "fileStats",
            "hashString",
            "haveUnchecked",
            "haveValid",
            "honorsSessionLimits",
            "id",
            "isFinished",
            "isPrivate",
            "isStalled",
            "leftUntilDone",
            "magnetLink",
            "manualAnnounceTime",
            "maxConnectedPeers",
            "metadataPercentComplete",
            "name",
            "peer-limit",
            "peers",
            "peersConnected",
            "peersFrom",
            "peersGettingFromUs",
            "peersSendingToUs",
            "percentDone",
            "pieces",
            "pieceCount",
            "pieceSize",
            "priorities",
            "queuePosition",
            "rateDownload",
            "rateUpload",
            "recheckProgress",
            "secondsDownloading",
            "secondsSeeding",
            "seedIdleLimit",
            "seedIdleMode",
            "seedRatioLimit",
            "seedRatioMode",
            "sizeWhenDone",
            "startDate",
            "status",
            "trackers",
            "trackerStats",
            "totalSize",
            "torrentFile",
            "uploadedEver",
            "uploadLimit",
            "uploadLimited",
            "uploadRatio",
            "wanted",
            "webseeds",
            "webseedsSendingToUs",
        };
    }

    enum TorrentField
    {
        ActivityDate,
        AddedDate,
        BandwidthPriority,
        Comment,
        CorruptEver,
        Creator,
        DateCreated,
        DesiredAvailable,
        DoneDate,
        DownloadDir,
        DownloadedEver,
        DownloadLimit,
        DownloadLimited,
        Error,
        ErrorString,
        Eta,
        EtaIdle,
        Files,
        FileStats,
        HashString,
        HaveUnchecked,
        HaveValid,
        HonorsSessionLimits,
        Id,
        IsFinished,
        IsPrivate,
        IsStalled,
        LeftUntilDone,
        MagnetLink,
        ManualAnnounceTime,
        MaxConnectedPeers,
        MetadataPercentComplete,
        Name,
        PeerLimit,
        Peers,
        PeersConnected,
        PeersFrom,
        PeersGettingFromUs,
        PeersSendingToUs,
        PercentDone,
        Pieces,
        PieceCount,
        PieceSize,
        Priorities,
        QueuePosition,
        RateDownload,
        RateUpload,
        RecheckProgress,
        SecondsDownloading,
        SecondsSeeding,
        SeedIdleLimit,
        SeedIdleMode,
        SeedRatioLimit,
        SeedRatioMode,
        SizeWhenDone,
        StartDate,
        Status,
        Trackers,
        TrackerStats,
        TotalSize,
        TorrentFile,
        UploadedEver,
        UploadLimit,
        UploadLimited,
        UploadRatio,
        Wanted,
        Webseeds,
        WebseedsSendingToUs,
    }

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

    public class File
    {
        [JsonProperty("bytesCompleted")]
        public long BytesCompleted { get; set; }
        [JsonProperty("length")]
        public long Length { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class FileStats
    {
        [JsonProperty("bytesCompleted")]
        public long BytesCompleted { get; set; }
        [JsonProperty("wanted")]
        public bool Wanted { get; set; }
        [JsonProperty("priority")]
        public int Priority { get; set; }
    }

    public class Peer
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("clientName")]
        public string ClientName { get; set; }
        [JsonProperty("clientIsChoked")]
        public bool ClientIsChoked { get; set; }
        [JsonProperty("clientIsInterested")]
        public bool ClientIsInterested { get; set; }
        [JsonProperty("flagStr")]
        public string FlagStr { get; set; }
        [JsonProperty("isDownloadingFrom")]
        public bool IsDownloadingFrom { get; set; }
        [JsonProperty("isEncrypted")]
        public bool IsEncrypted { get; set; }
        [JsonProperty("isIncoming")]
        public bool IsIncoming { get; set; }
        [JsonProperty("isUploadingTo")]
        public bool IsUploadingTo { get; set; }
        [JsonProperty("isUTP")]
        public bool IsUTP { get; set; }
        [JsonProperty("peerIsChoked")]
        public bool PeerIsChoked { get; set; }
        [JsonProperty("peerIsInterested")]
        public bool PeerIsInterested { get; set; }
        [JsonProperty("port")]
        public int Port { get; set; }
        [JsonProperty("progress")]
        public double Progress { get; set; }
        [JsonProperty("rateToClient")]
        public int RateToClient { get; set; }
        [JsonProperty("rateToPeer")]
        public int RateToPeer { get; set; }
    }

    public class PeersFrom
    {
        [JsonProperty("fromCache")]
        public int FromCache { get; set; }
        [JsonProperty("fromDht")]
        public int FromDht { get; set; }
        [JsonProperty("fromIncoming")]
        public int FromIncoming { get; set; }
        [JsonProperty("fromLpd")]
        public int FromLpd { get; set; }
        [JsonProperty("fromLtep")]
        public int fromLtep { get; set; }
        [JsonProperty("fromPex")]
        public int FromPex { get; set; }
        [JsonProperty("fromTracker")]
        public int FromTracker { get; set; }
    }

    public class Tracker
    {
        [JsonProperty("announce")]
        public string Announce { get; set; }
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("scrape")]
        public string Scrape { get; set; }
        [JsonProperty("tier")]
        public int Tier { get; set; }
    }

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

    public class TorrentGetResponse
    {
        [JsonProperty("torrents")]
        public Torrent[] Torrents { get; set; }
    }

    internal class TorrentGetRequest<T> : ArgumentsBase
    {
        public override string MethodName => "torrent-get";

        [JsonProperty("fields")]
        public string[] Fields { get; set; }

        [JsonProperty("ids")]
        public T IDs { get; set; }
    }

    internal class Response<T>
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("arguments")]
        public T Data { get; set; }

        [JsonProperty("tag")]
        public int Tag { get; set; }
    }

    internal abstract class ArgumentsBase
    {
        [JsonIgnore]
        public abstract string MethodName { get; }
    }

    internal class Request<T> where T : ArgumentsBase
    {
        [JsonProperty("method")]
        public string Method => Arguments.MethodName;

        [JsonProperty("arguments")]
        public T Arguments { get; set; }

        [JsonProperty("tag")]
        public int Tag { get; set; }
    }
}
