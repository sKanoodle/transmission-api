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
        /// Reannounces ("ask tracker for more peers") all torrents.
        /// </summary>
        public Task TorrentReannounceAsync()
        {
            return TorrentReannounceAsync((string)null);
        }

        /// <summary>
        /// Reannounces ("ask tracker for more peers") the single torrent matching the ID.
        /// </summary>
        /// <param name="id">single torrent ID</param>
        public Task TorrentReannounceAsync(int id)
        {
            return TorrentReannounceAsync<int>(id);
        }

        /// <summary>
        /// Reannounces ("ask tracker for more peers") those torrents matching the torrent IDs.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        public Task TorrentReannounceAsync(IEnumerable<int> ids)
        {
            return TorrentReannounceAsync(ids.ToArray());
        }

        /// <summary>
        /// Reannounces ("ask tracker for more peers") those torrents matching the hashes.
        /// </summary>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task TorrentReannounceAsync(IEnumerable<string> hashes)
        {
            return TorrentReannounceAsync(hashes.ToArray());
        }

        /// <summary>
        /// Reannounces ("ask tracker for more peers") those torrents matching either the IDs or hashes.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task TorrentReannounceAsync(IEnumerable<int> ids, IEnumerable<string> hashes)
        {
            return TorrentReannounceAsync(((IEnumerable<object>)ids).Concat(hashes).ToArray());
        }

        /// <summary>
        /// Reannounces ("ask tracker for more peers") all recently-active torrents.
        /// </summary>
        public Task TorrentReannounceRecentAsync()
        {
            return TorrentReannounceAsync("recently-active");
        }

        /// <summary>
        /// Reannounces ("ask tracker for more peers") torrents matching any type of torrent-identifier (see supported values in transmission-rpc spec or <paramref name="ids"/>).
        /// </summary>
        /// <typeparam name="T">type of IDs</typeparam>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="ids">any type of supported value as ID (list of ints, hashstrings, or both in one list, int, (the string "recently-active" is a valid argument, but is handled in <see cref="TorrentReannounceRecentAsync"/>, because it causes the result to have a new array with recently-deleted IDs))</param>
        private async Task TorrentReannounceAsync<T>(T ids)
        {
            await GetResponseAsync<ResponseBase, TorrentActionRequest<T>>(new TorrentActionRequest<T>("torrent-start") { Ids = ids });
        }
    }
}
