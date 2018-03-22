using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.Api
{
    public partial class Client
    {
        /// <summary>
        /// Add a new torrent by filename (magnet-link, filename? not entirely clear).
        /// </summary>
        /// <param name="filename">can be a magnet-link, and possibly a filename? idk</param>
        /// <param name="paused">sets the download to paused after uploading</param>
        /// <param name="downloadDir">sets the target downloaddir</param>
        /// <returns>an indicator of success and the new/existing torrent</returns>
        public Task<TorrentAdded> TorrentAddAsync(string filename, bool paused = false, string downloadDir = null)
        {
            return TorrentAddAsync(new TorrentAddRequest {FileName = filename, Paused = paused, DownloadDir = downloadDir });
        }

        /// <summary>
        /// Add a new torrent by supplying the base64 encoded content of a .torrent file.
        /// </summary>
        /// <param name="base64">base64 encoded content of a .torrent file</param>
        /// <param name="paused">sets the download to paused after uploading</param>
        /// <param name="downloadDir">sets the target downloaddir</param>
        /// <returns>an indicator of success and the new/existing torrent</returns>
        public Task<TorrentAdded> TorrentAddBase64Async(string base64, bool paused = false, string downloadDir = null)
        {
            return TorrentAddAsync(new TorrentAddRequest { MetaInfo = base64, Paused = paused, DownloadDir = downloadDir });
        }

        /// <summary>
        /// Add a new torrent by giving the path to a .torrent file.
        /// </summary>
        /// <param name="path">Path to a local .torrent file to upload to the server.</param>
        /// <param name="paused">sets the download to paused after uploading</param>
        /// <param name="downloadDir">sets the target downloaddir</param>
        /// <returns>an indicator of success and the new/existing torrent</returns>
        public Task<TorrentAdded> TorrentAddPathAsync(string path, bool paused = false, string downloadDir = null)
        {
            string base64 = Convert.ToBase64String(File.ReadAllBytes(path));
            return TorrentAddBase64Async(base64, paused, downloadDir);
        }

        /// <summary>
        /// Add a new torrent by directly giving a <see cref="TorrentAddRequest"/>. Only do this when you know what you are doing!
        /// </summary>
        /// <param name="arguments">settings for torrent that should be added</param>
        /// <returns>an indicator of success and the new/existing torrent</returns>
        public async Task<TorrentAdded> TorrentAddAsync(TorrentAddRequest arguments)
        {
            var result = await GetResponseAsync<TorrentAddResponse, TorrentAddRequest>(arguments);
            if (result.Added is LightweightTorrent added)
                return new TorrentAdded { Result = TorrentAddResult.Added, Torrent = added };
            if (result.Duplicate is LightweightTorrent dublicate)
                return new TorrentAdded { Result = TorrentAddResult.Duplicate, Torrent = dublicate };
            return new TorrentAdded { Result = TorrentAddResult.Error };
        }
    }

    public class TorrentAdded
    {
        public TorrentAddResult Result { get; set; }
        public LightweightTorrent Torrent { get; set; }
    }

    public class TorrentAddRequest : ArgumentsBase
    {
        public override string MethodName => "torrent-add";
        /// <summary>
        /// Pointer to a string of one or more cookies. The format of the "cookies" should be NAME=CONTENTS, where NAME is the cookie name and CONTENTS is what the cookie should contain. Set multiple cookies like this: "name1=content1; name2=content2;" etc. <see href="http://curl.haxx.se/libcurl/c/curl_easy_setopt.html#CURLOPTCOOKIE>
        /// </summary>
        [JsonProperty("cookies")]
        public string Cookies { get; set; }
        /// <summary>
        /// Path to download the torrent to.
        /// </summary>
        [JsonProperty("download-dir")]
        public string DownloadDir { get; set; }
        /// <summary>
        /// Filename or URL of the .torrent file or magnet link.
        /// </summary>
        [JsonProperty("filename")]
        public string FileName { get; set; }
        /// <summary>
        /// Base64-encoded .torrent content.
        /// </summary>
        [JsonProperty("metainfo")]
        public string MetaInfo { get; set; }
        /// <summary>
        /// If true, don't start the torrent.
        /// </summary>
        [JsonProperty("paused")]
        public bool Paused { get; set; }
        /// <summary>
        /// Maximum number of peers.
        /// </summary>
        [JsonProperty("peer-limit")]
        public int PeerLimit { get; set; }
        /// <summary>
        /// Torrent's bandwidth tr_priority_t.
        /// </summary>
        [JsonProperty("bandwidthPriority")]
        public sbyte BandwidthPriority { get; set; }
        /// <summary>
        /// Indices of file(s) to download.
        /// </summary>
        [JsonProperty("files-wanted")]
        public int[] FilesWanted { get; set; }
        /// <summary>
        /// Indices of file(s) to not download.
        /// </summary>
        [JsonProperty("files-unwanted")]
        public int[] FilesUnwanted { get; set; }
        /// <summary>
        /// Indices of high-priority file(s).
        /// </summary>
        [JsonProperty("priority-high")]
        public int[] PriorityHigh { get; set; }
        /// <summary>
        /// Indices of low-priority file(s).
        /// </summary>
        [JsonProperty("priority-low")]
        public int[] PriorityLow { get; set; }
        /// <summary>
        /// Indices of normal-priority file(s).
        /// </summary>
        [JsonProperty("priority-normal")]
        public int[] PriorityNormal { get; set; }
    }

    public enum TorrentAddResult
    {
        Error,
        Added,
        Duplicate,
    }

    internal class TorrentAddResponse
    {
        [JsonProperty("torrent-added")]
        public LightweightTorrent Added { get; set; }
        [JsonProperty("torrent-duplicate")]
        public LightweightTorrent Duplicate { get; set; }
    }

    public class LightweightTorrent
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("hashString")]
        public string HashString { get; set; }
    }
}
