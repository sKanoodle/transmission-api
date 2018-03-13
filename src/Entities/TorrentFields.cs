using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.Api.Entities
{
    /// <summary>
    /// works like a flags enum, use "|" to set mutliple fields
    /// </summary>
    public class TorrentFields
    {
        public static readonly TorrentFields All = new TorrentFields(new BitArray(Enum.GetValues(typeof(TorrentField)).Length, true));

        public static readonly TorrentFields ActivityDate = new TorrentFields(TorrentField.ActivityDate);
        public static readonly TorrentFields AddedDate = new TorrentFields(TorrentField.AddedDate);
        public static readonly TorrentFields BandwidthPriority = new TorrentFields(TorrentField.BandwidthPriority);
        public static readonly TorrentFields Comment = new TorrentFields(TorrentField.Comment);
        public static readonly TorrentFields CorruptEver = new TorrentFields(TorrentField.CorruptEver);
        public static readonly TorrentFields Creator = new TorrentFields(TorrentField.Creator);
        public static readonly TorrentFields DateCreated = new TorrentFields(TorrentField.DateCreated);
        public static readonly TorrentFields DesiredAvailable = new TorrentFields(TorrentField.DesiredAvailable);
        public static readonly TorrentFields DoneDate = new TorrentFields(TorrentField.DoneDate);
        public static readonly TorrentFields DownloadDir = new TorrentFields(TorrentField.DownloadDir);
        public static readonly TorrentFields DownloadedEver = new TorrentFields(TorrentField.DownloadedEver);
        public static readonly TorrentFields DownloadLimit = new TorrentFields(TorrentField.DownloadLimit);
        public static readonly TorrentFields DownloadLimited = new TorrentFields(TorrentField.DownloadLimited);
        public static readonly TorrentFields Error = new TorrentFields(TorrentField.Error);
        public static readonly TorrentFields ErrorString = new TorrentFields(TorrentField.ErrorString);
        public static readonly TorrentFields Eta = new TorrentFields(TorrentField.Eta);
        public static readonly TorrentFields EtaIdle = new TorrentFields(TorrentField.EtaIdle);
        public static readonly TorrentFields Files = new TorrentFields(TorrentField.Files);
        public static readonly TorrentFields FileStats = new TorrentFields(TorrentField.FileStats);
        public static readonly TorrentFields HashString = new TorrentFields(TorrentField.HashString);
        public static readonly TorrentFields HaveUnchecked = new TorrentFields(TorrentField.HaveUnchecked);
        public static readonly TorrentFields HaveValid = new TorrentFields(TorrentField.HaveValid);
        public static readonly TorrentFields HonorsSessionLimits = new TorrentFields(TorrentField.HonorsSessionLimits);
        public static readonly TorrentFields Id = new TorrentFields(TorrentField.Id);
        public static readonly TorrentFields IsFinished = new TorrentFields(TorrentField.IsFinished);
        public static readonly TorrentFields IsPrivate = new TorrentFields(TorrentField.IsPrivate);
        public static readonly TorrentFields IsStalled = new TorrentFields(TorrentField.IsStalled);
        public static readonly TorrentFields LeftUntilDone = new TorrentFields(TorrentField.LeftUntilDone);
        public static readonly TorrentFields MagnetLink = new TorrentFields(TorrentField.MagnetLink);
        public static readonly TorrentFields ManualAnnounceTime = new TorrentFields(TorrentField.ManualAnnounceTime);
        public static readonly TorrentFields MaxConnectedPeers = new TorrentFields(TorrentField.MaxConnectedPeers);
        public static readonly TorrentFields MetadataPercentComplete = new TorrentFields(TorrentField.MetadataPercentComplete);
        public static readonly TorrentFields Name = new TorrentFields(TorrentField.Name);
        public static readonly TorrentFields PeerLimit = new TorrentFields(TorrentField.PeerLimit);
        public static readonly TorrentFields Peers = new TorrentFields(TorrentField.Peers);
        public static readonly TorrentFields PeersConnected = new TorrentFields(TorrentField.PeersConnected);
        public static readonly TorrentFields PeersFrom = new TorrentFields(TorrentField.PeersFrom);
        public static readonly TorrentFields PeersGettingFromUs = new TorrentFields(TorrentField.PeersGettingFromUs);
        public static readonly TorrentFields PeersSendingToUs = new TorrentFields(TorrentField.PeersSendingToUs);
        public static readonly TorrentFields PercentDone = new TorrentFields(TorrentField.PercentDone);
        public static readonly TorrentFields Pieces = new TorrentFields(TorrentField.Pieces);
        public static readonly TorrentFields PieceCount = new TorrentFields(TorrentField.PieceCount);
        public static readonly TorrentFields PieceSize = new TorrentFields(TorrentField.PieceSize);
        public static readonly TorrentFields Priorities = new TorrentFields(TorrentField.Priorities);
        public static readonly TorrentFields QueuePosition = new TorrentFields(TorrentField.QueuePosition);
        public static readonly TorrentFields RateDownload = new TorrentFields(TorrentField.RateDownload);
        public static readonly TorrentFields RateUpload = new TorrentFields(TorrentField.RateUpload);
        public static readonly TorrentFields RecheckProgress = new TorrentFields(TorrentField.RecheckProgress);
        public static readonly TorrentFields SecondsDownloading = new TorrentFields(TorrentField.SecondsDownloading);
        public static readonly TorrentFields SecondsSeeding = new TorrentFields(TorrentField.SecondsSeeding);
        public static readonly TorrentFields SeedIdleLimit = new TorrentFields(TorrentField.SeedIdleLimit);
        public static readonly TorrentFields SeedIdleMode = new TorrentFields(TorrentField.SeedIdleMode);
        public static readonly TorrentFields SeedRatioLimit = new TorrentFields(TorrentField.SeedRatioLimit);
        public static readonly TorrentFields SeedRatioMode = new TorrentFields(TorrentField.SeedRatioMode);
        public static readonly TorrentFields SizeWhenDone = new TorrentFields(TorrentField.SizeWhenDone);
        public static readonly TorrentFields StartDate = new TorrentFields(TorrentField.StartDate);
        public static readonly TorrentFields Status = new TorrentFields(TorrentField.Status);
        public static readonly TorrentFields Trackers = new TorrentFields(TorrentField.Trackers);
        public static readonly TorrentFields TrackerStats = new TorrentFields(TorrentField.TrackerStats);
        public static readonly TorrentFields TotalSize = new TorrentFields(TorrentField.TotalSize);
        public static readonly TorrentFields TorrentFile = new TorrentFields(TorrentField.TorrentFile);
        public static readonly TorrentFields UploadedEver = new TorrentFields(TorrentField.UploadedEver);
        public static readonly TorrentFields UploadLimit = new TorrentFields(TorrentField.UploadLimit);
        public static readonly TorrentFields UploadLimited = new TorrentFields(TorrentField.UploadLimited);
        public static readonly TorrentFields UploadRatio = new TorrentFields(TorrentField.UploadRatio);
        public static readonly TorrentFields Wanted = new TorrentFields(TorrentField.Wanted);
        public static readonly TorrentFields Webseeds = new TorrentFields(TorrentField.Webseeds);
        public static readonly TorrentFields WebseedsSendingToUs = new TorrentFields(TorrentField.WebseedsSendingToUs);

        private BitArray Array { get; }

        private TorrentFields(TorrentField field)
        {
            Array = new BitArray(Enum.GetValues(typeof(TorrentField)).Length, false);
            Array.Set((int)field, true);
        }

        private TorrentFields(BitArray array)
        {
            Array = array;
        }

        public static TorrentFields operator |(TorrentFields f1, TorrentFields f2)
        {
            return new TorrentFields(f1.Array.Or(f2.Array));
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
        private static readonly string[] LookupArray = new string[]
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
}
