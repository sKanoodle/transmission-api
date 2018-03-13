using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transmission.Api.Entities;

namespace Transmission.Api
{
    public partial class Client
    {
        /// <summary>
        /// Gets all torrents with all fields.
        /// </summary>
        public async Task<Torrent[]> TorrentGetAsync()
        {
            return await TorrentGetAsync(TorrentFields.All);
        }

        /// <summary>
        /// Gets all torrents with the specified fields.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        public async Task<Torrent[]> TorrentGetAsync(TorrentFields fields)
        {
            return await TorrentGetAsync(fields, (string)null);
        }

        /// <summary>
        /// Gets all fields for the single torrent matching the ID.
        /// </summary>
        /// <param name="id">single torrent ID</param>
        public async Task<Torrent[]> TorrentGetAsync(int id)
        {
            return await TorrentGetAsync(TorrentFields.All, id);
        }

        /// <summary>
        /// Gets the specified fields for the single torrent matching the ID.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="id">single torrent ID</param>
        public async Task<Torrent[]> TorrentGetAsync(TorrentFields fields, int id)
        {
            return await TorrentGetAsync<int>(fields, id);
        }

        /// <summary>
        /// Gets all fields for those torrents matching the torrent IDs.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        public async Task<Torrent[]> TorrentGetAsync(IEnumerable<int> ids)
        {
            return await TorrentGetAsync(TorrentFields.All, ids);
        }

        /// <summary>
        /// Gets the specified fields for those torrents matching the torrent IDs.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="ids">collection of torrent IDs</param>
        public async Task<Torrent[]> TorrentGetAsync(TorrentFields fields, IEnumerable<int> ids)
        {
            return await TorrentGetAsync(fields, ids.ToArray());
        }

        /// <summary>
        /// Gets all fields for those torrents matching the hashes.
        /// </summary>
        /// <param name="hashes">collection of torrent-hashes</param>
        public async Task<Torrent[]> TorrentGetAsync(IEnumerable<string> hashes)
        {
            return await TorrentGetAsync(TorrentFields.All, hashes);
        }

        /// <summary>
        /// Gets the specified fields for those torrents matching the hashes.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        public async Task<Torrent[]> TorrentGetAsync(TorrentFields fields, IEnumerable<string> hashes)
        {
            return await TorrentGetAsync(fields, hashes.ToArray());
        }

        /// <summary>
        /// Gets all fields for the those torrents matching either the IDs or hashes.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        public async Task<Torrent[]> TorrentGetAsync(IEnumerable<int> ids, IEnumerable<string> hashes)
        {
            return await TorrentGetAsync(TorrentFields.All, ids, hashes);
        }

        /// <summary>
        /// Gets the specified fields for those torrents matching either the IDs or hashes.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        public async Task<Torrent[]> TorrentGetAsync(TorrentFields fields, IEnumerable<int> ids, IEnumerable<string> hashes)
        {
            return await TorrentGetAsync(fields, ((IEnumerable<object>)ids).Concat(hashes).ToArray());
        }

        /// <summary>
        /// Gets the specified fields for any type of torrent-identifier (see supported values in transmission-rpc spec or <paramref name="ids"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="ids">any type of supported value as ID (list of ints, hashstrings, or both in one list, int, (the string "recently-active" is a valid argument, but is handled in <see cref="TorrentGetRecentAsync"/>, because it causes the result to have a new array with recently-deleted IDs))</param>
        private async Task<Torrent[]> TorrentGetAsync<T>(TorrentFields fields, T ids)
        {
            var request = new TorrentGetRequest<T> { Fields = fields.ToStringRepresentation(), IDs = ids };
            var response = await GetResponseAsync<TorrentGetResponse, TorrentGetRequest<T>>(request);
            return response.Torrents;
        }

        /// <summary>
        /// Gets all fields of all recently-active torrents.
        /// </summary>
        public async Task<TorrentGetResponse> TorrentGetRecentAsync()
        {
            return await TorrentGetRecentAsync(TorrentFields.All);
        }

        /// <summary>
        /// Gets the specified fields of all recently-active torrents.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        public async Task<TorrentGetResponse> TorrentGetRecentAsync(TorrentFields fields)
        {
            var request = new TorrentGetRequest<string> { Fields = fields.ToStringRepresentation(), IDs = "recently-active" };
            return await GetResponseAsync<TorrentGetResponse, TorrentGetRequest<string>>(request);
        }
    }
}
