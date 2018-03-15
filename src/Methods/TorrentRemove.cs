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
        /// Deletes all torrents.
        /// </summary>
        /// <param name="deleteLocalData">delete local data</param>
        public Task TorrentRemoveAsync(bool deleteLocalData = false)
        {
            return TorrentRemoveAsync((string)null, deleteLocalData);
        }

        /// <summary>
        /// Deletes the single torrent matching the ID.
        /// </summary>
        /// <param name="id">single torrent ID</param>
        /// <param name="deleteLocalData">delete local data</param>
        public Task TorrentRemoveAsync(int id, bool deleteLocalData = false)
        {
            return TorrentRemoveAsync<int>(id, deleteLocalData);
        }

        /// <summary>
        /// Deletes those torrents matching the torrent IDs.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="deleteLocalData">delete local data</param>
        public Task TorrentRemoveAsync(IEnumerable<int> ids, bool deleteLocalData = false)
        {
            return TorrentRemoveAsync(ids.ToArray(), deleteLocalData);
        }

        /// <summary>
        /// Deletes those torrents matching the hashes.
        /// </summary>
        /// <param name="hashes">collection of torrent-hashes</param>
        /// <param name="deleteLocalData">delete local data</param>
        public Task TorrentRemoveAsync(IEnumerable<string> hashes, bool deleteLocalData = false)
        {
            return TorrentRemoveAsync(hashes.ToArray(), deleteLocalData);
        }

        /// <summary>
        /// Deletes those torrents matching either the IDs or hashes.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        /// <param name="deleteLocalData">delete local data</param>
        public Task TorrentRemoveAsync(IEnumerable<int> ids, IEnumerable<string> hashes, bool deleteLocalData = false)
        {
            return TorrentRemoveAsync(((IEnumerable<object>)ids).Concat(hashes).ToArray(), deleteLocalData);
        }

        /// <summary>
        /// Deletes all recently-active torrents.
        /// </summary>
        /// <param name="deleteLocalData">delete local data</param>
        public Task TorrentRemoveRecentAsync(bool deleteLocalData = false)
        {
            return TorrentRemoveAsync("recently-active", deleteLocalData);
        }

        /// <summary>
        /// Deletes torrents matching any type of torrent-identifier (see supported values in transmission-rpc spec or <paramref name="ids"/>).
        /// </summary>
        /// <typeparam name="T">type of IDs</typeparam>
        /// <param name="ids">any type of supported value as ID (list of ints, hashstrings, or both in one list, int, (the string "recently-active" is a valid argument, but is handled in <see cref="TorrentRemoveRecentAsync"/>, because it causes the result to have a new array with recently-deleted IDs))</param>
        /// <param name="deleteLocalData">delete local data</param>
        private async Task TorrentRemoveAsync<T>(T ids, bool deleteLocalData)
        {
            await GetResponseAsync<ResponseBase, TorrentRemoveRequest<T>>(new TorrentRemoveRequest<T>{ Ids = ids, DeleteLocalData = deleteLocalData });
        }
    }

    internal class TorrentRemoveRequest<T> : ArgumentsBase
    {
        public override string MethodName => "torrent-remove";
        [JsonProperty("ids")]
        public T Ids { get; set; }
        [JsonProperty("delete-local-data")]
        public bool DeleteLocalData { get; set; }
    }
}
