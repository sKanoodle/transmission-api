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
        /// <summary>
        /// Renames last part of the given path of a single torrent.<para />
        /// Consider a tr_torrent where its<para />
        /// info.files[0].name is "frobnitz-linux/checksum" and<para />
        /// info.files[1].name is "frobnitz-linux/frobnitz.iso".<para />
        /// 1. tr_torrentRenamePath(tor, "frobnitz-linux", "foo") will rename
        /// the "frotbnitz-linux" folder as "foo", and update both info.name
        /// and info.files[*].name.<para />
        /// 2. tr_torrentRenamePath(tor, "frobnitz-linux/checksum", "foo") will
        /// rename the "frobnitz-linux/checksum" file as "foo" and update
        /// files[0].name to "frobnitz-linux/foo".
        /// </summary>
        /// <param name="id">single ID for the affected torrent</param>
        /// <param name="path">the path of the file or folder that will be renamed</param>
        /// <param name="name">the file or folder's new name</param>
        public Task<TorrentRenameResponse> TorrentRenamePathAsync(int id, string path, string name)
        {
            return TorrentRenamePathAsync<int>(id, path, name);
        }

        /// <summary>
        /// Renames last part of the given path of a single torrent.<para />
        /// Consider a tr_torrent where its<para />
        /// info.files[0].name is "frobnitz-linux/checksum" and<para />
        /// info.files[1].name is "frobnitz-linux/frobnitz.iso".<para />
        /// 1. tr_torrentRenamePath(tor, "frobnitz-linux", "foo") will rename
        /// the "frotbnitz-linux" folder as "foo", and update both info.name
        /// and info.files[*].name.<para />
        /// 2. tr_torrentRenamePath(tor, "frobnitz-linux/checksum", "foo") will
        /// rename the "frobnitz-linux/checksum" file as "foo" and update
        /// files[0].name to "frobnitz-linux/foo".
        /// </summary>
        /// <param name="hash">single hash for the affected torrent</param>
        /// <param name="path">the path of the file or folder that will be renamed</param>
        /// <param name="name">the file or folder's new name</param>
        public Task<TorrentRenameResponse> TorrentRenamePathAsync(string hash, string path, string name)
        {
            return TorrentRenamePathAsync<string>(hash, path, name);
        }

        /// <summary>
        /// Stops torrents matching any type of torrent-identifier (see supported values in transmission-rpc spec or <paramref name="ids"/>).
        /// </summary>
        /// <typeparam name="T">type of IDs</typeparam>
        /// <param name="id">Here, only a single torrent is allowed. Either an ID or a hash.</param>
        private Task<TorrentRenameResponse> TorrentRenamePathAsync<T>(T id, string path, string name)
        {
            return GetResponseAsync<TorrentRenameResponse, TorrentRenamePathArguments<T>>(new TorrentRenamePathArguments<T> { Ids = new[] { id }, Path = path, Name = name });
        }
    }

    internal class TorrentRenamePathArguments<T> : ArgumentsBase
    {
        public override string MethodName => "torrent-rename-path";
        /// <summary>
        /// only one ID allowed
        /// </summary>
        [JsonProperty("ids")]
        public T[] Ids { get; set; }
        /// <summary>
        /// the path to the file or folder that will be renamed
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
        /// <summary>
        /// the file or folder's new name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class TorrentRenameResponse
    {
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
