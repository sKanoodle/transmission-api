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
        /// Stops all torrents.
        /// </summary>
        public Task TorrentStopAsync()
        {
            return TorrentStopAsync((string)null);
        }

        /// <summary>
        /// Stops the single torrent matching the ID.
        /// </summary>
        /// <param name="id">single torrent ID</param>
        public Task TorrentStopAsync(int id)
        {
            return TorrentStopAsync<int>(id);
        }

        /// <summary>
        /// Stops those torrents matching the torrent IDs.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        public Task TorrentStopAsync(IEnumerable<int> ids)
        {
            return TorrentStopAsync(ids.ToArray());
        }

        /// <summary>
        /// Stops those torrents matching the hashes.
        /// </summary>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task TorrentStopAsync(IEnumerable<string> hashes)
        {
            return TorrentStopAsync(hashes.ToArray());
        }

        /// <summary>
        /// Stops those torrents matching either the IDs or hashes.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task TorrentStopAsync(IEnumerable<int> ids, IEnumerable<string> hashes)
        {
            return TorrentStopAsync(((IEnumerable<object>)ids).Concat(hashes).ToArray());
        }

        /// <summary>
        /// Stops all recently-active torrents.
        /// </summary>
        public Task TorrentStopRecentAsync()
        {
            return TorrentStopAsync("recently-active");
        }

        /// <summary>
        /// Stops torrents matching any type of torrent-identifier (see supported values in transmission-rpc spec or <paramref name="ids"/>).
        /// </summary>
        /// <typeparam name="T">type of IDs</typeparam>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="ids">any type of supported value as ID (list of ints, hashstrings, or both in one list, int, (the string "recently-active" is a valid argument, but is handled in <see cref="TorrentStopRecentAsync"/>, because it causes the result to have a new array with recently-deleted IDs))</param>
        private async Task TorrentStopAsync<T>(T ids)
        {
            await GetResponseAsync<ResponseBase, TorrentActionRequest<T>>(new TorrentActionRequest<T>("torrent-start") { Ids = ids });
        }
    }
}
