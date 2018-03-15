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
        /// Starts all torrents.
        /// </summary>
        public Task TorrentStartAsync()
        {
            return TorrentStartAsync((string)null);
        }

        /// <summary>
        /// Starts the single torrent matching the ID.
        /// </summary>
        /// <param name="id">single torrent ID</param>
        public Task TorrentStartAsync(int id)
        {
            return TorrentStartAsync<int>(id);
        }

        /// <summary>
        /// Starts those torrents matching the torrent IDs.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        public Task TorrentStartAsync(IEnumerable<int> ids)
        {
            return TorrentStartAsync(ids.ToArray());
        }

        /// <summary>
        /// Starts those torrents matching the hashes.
        /// </summary>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task TorrentStartAsync(IEnumerable<string> hashes)
        {
            return TorrentStartAsync(hashes.ToArray());
        }

        /// <summary>
        /// Starts those torrents matching either the IDs or hashes.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task TorrentStartAsync(IEnumerable<int> ids, IEnumerable<string> hashes)
        {
            return TorrentStartAsync(((IEnumerable<object>)ids).Concat(hashes).ToArray());
        }

        /// <summary>
        /// Starts all recently-active torrents.
        /// </summary>
        public Task TorrentStartRecentAsync()
        {
            return TorrentStartAsync("recently-active");
        }

        /// <summary>
        /// Starts torrents matching any type of torrent-identifier (see supported values in transmission-rpc spec or <paramref name="ids"/>).
        /// </summary>
        /// <typeparam name="T">type of IDs</typeparam>
        /// <param name="ids">any type of supported value as ID (list of ints, hashstrings, or both in one list, int, (the string "recently-active" is a valid argument, but is handled in <see cref="TorrentStartRecentAsync"/>, because it causes the result to have a new array with recently-deleted IDs))</param>
        private async Task TorrentStartAsync<T>(T ids)
        {
            await GetResponseAsync<ResponseBase, TorrentActionRequest<T>>(new TorrentActionRequest<T>("torrent-start") { Ids = ids });
        }
    }

    internal class TorrentActionRequest<T> : ArgumentsBase
    {
        public override string MethodName { get; }

        public TorrentActionRequest(string methodName)
        {
            MethodName = methodName;
        }

        [JsonProperty("ids")]
        public T Ids { get; set; }
    }
}
