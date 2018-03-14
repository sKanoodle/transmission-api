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
        /// Immediately starts (bypassing queues and other conditions) all torrents.
        /// </summary>
        public Task TorrentStartNowAsync()
        {
            return TorrentStartNowAsync((string)null);
        }

        /// <summary>
        /// Immediately starts (bypassing queues and other conditions) the single torrent matching the ID.
        /// </summary>
        /// <param name="id">single torrent ID</param>
        public Task TorrentStartNowAsync(int id)
        {
            return TorrentStartNowAsync<int>(id);
        }

        /// <summary>
        /// Immediately starts (bypassing queues and other conditions) those torrents matching the torrent IDs.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        public Task TorrentStartNowAsync(IEnumerable<int> ids)
        {
            return TorrentStartNowAsync(ids.ToArray());
        }

        /// <summary>
        /// Immediately starts (bypassing queues and other conditions) those torrents matching the hashes.
        /// </summary>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task TorrentStartNowAsync(IEnumerable<string> hashes)
        {
            return TorrentStartNowAsync(hashes.ToArray());
        }

        /// <summary>
        /// Immediately starts (bypassing queues and other conditions) those torrents matching either the IDs or hashes.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task TorrentStartNowAsync(IEnumerable<int> ids, IEnumerable<string> hashes)
        {
            return TorrentStartNowAsync(((IEnumerable<object>)ids).Concat(hashes).ToArray());
        }

        /// <summary>
        /// Immediately starts (bypassing queues and other conditions) all recently-active torrents.
        /// </summary>
        public Task TorrentStartNowRecentAsync()
        {
            return TorrentStartNowAsync("recently-active");
        }

        /// <summary>
        /// Immediately starts (bypassing queues and other conditions) torrents matching any type of torrent-identifier (see supported values in transmission-rpc spec or <paramref name="ids"/>).
        /// </summary>
        /// <typeparam name="T">type of IDs</typeparam>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="ids">any type of supported value as ID (list of ints, hashstrings, or both in one list, int, (the string "recently-active" is a valid argument, but is handled in <see cref="TorrentStartNowRecentAsync"/>, because it causes the result to have a new array with recently-deleted IDs))</param>
        private async Task TorrentStartNowAsync<T>(T ids)
        {
            await GetResponseAsync<ResponseBase, TorrentActionRequest<T>>(new TorrentActionRequest<T>("torrent-start-now") { Ids = ids });
        }
    }
}
