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
        /// Queues all torrents for verification.
        /// </summary>
        public Task TorrentVerifyAsync()
        {
            return TorrentVerifyAsync((string)null);
        }

        /// <summary>
        /// Queues the single torrent matching the ID for verification.
        /// </summary>
        /// <param name="id">single torrent ID</param>
        public Task TorrentVerifyAsync(int id)
        {
            return TorrentVerifyAsync<int>(id);
        }

        /// <summary>
        /// Queues those torrents matching the torrent IDs for verification.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        public Task TorrentVerifyAsync(IEnumerable<int> ids)
        {
            return TorrentVerifyAsync(ids.ToArray());
        }

        /// <summary>
        /// Queues those torrents matching the hashes for verification.
        /// </summary>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task TorrentVerifyAsync(IEnumerable<string> hashes)
        {
            return TorrentVerifyAsync(hashes.ToArray());
        }

        /// <summary>
        /// Queues those torrents matching either the IDs or hashes for verification.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task TorrentVerifyAsync(IEnumerable<int> ids, IEnumerable<string> hashes)
        {
            return TorrentVerifyAsync(((IEnumerable<object>)ids).Concat(hashes).ToArray());
        }

        /// <summary>
        /// Queues all recently-active torrents for verification.
        /// </summary>
        public Task TorrentVerifyRecentAsync()
        {
            return TorrentVerifyAsync("recently-active");
        }

        /// <summary>
        /// Queues torrents matching any type of torrent-identifier (see supported values in transmission-rpc spec or <paramref name="ids"/>) for verification.
        /// </summary>
        /// <typeparam name="T">type of IDs</typeparam>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="ids">any type of supported value as ID (list of ints, hashstrings, or both in one list, int, (the string "recently-active" is a valid argument, but is handled in <see cref="TorrentVerifyRecentAsync"/>, because it causes the result to have a new array with recently-deleted IDs))</param>
        private async Task TorrentVerifyAsync<T>(T ids)
        {
            await GetResponseAsync<ResponseBase, TorrentActionRequest<T>>(new TorrentActionRequest<T>("torrent-start") { Ids = ids });
        }
    }
}
