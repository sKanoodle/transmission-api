using Newtonsoft.Json;
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
        public Task<Torrent[]> TorrentGetAsync()
        {
            return TorrentGetAsync(TorrentFields.All);
        }

        /// <summary>
        /// Gets all torrents with the specified fields.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        public Task<Torrent[]> TorrentGetAsync(TorrentFields fields)
        {
            return TorrentGetAsync(fields, (string)null);
        }

        /// <summary>
        /// Gets all fields for the single torrent matching the ID.
        /// </summary>
        /// <param name="id">single torrent ID</param>
        public Task<Torrent[]> TorrentGetAsync(int id)
        {
            return TorrentGetAsync(TorrentFields.All, id);
        }

        /// <summary>
        /// Gets the specified fields for the single torrent matching the ID.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="id">single torrent ID</param>
        public Task<Torrent[]> TorrentGetAsync(TorrentFields fields, int id)
        {
            return TorrentGetAsync<int>(fields, id);
        }

        /// <summary>
        /// Gets all fields for those torrents matching the torrent IDs.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        public Task<Torrent[]> TorrentGetAsync(IEnumerable<int> ids)
        {
            return TorrentGetAsync(TorrentFields.All, ids);
        }

        /// <summary>
        /// Gets the specified fields for those torrents matching the torrent IDs.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="ids">collection of torrent IDs</param>
        public Task<Torrent[]> TorrentGetAsync(TorrentFields fields, IEnumerable<int> ids)
        {
            return TorrentGetAsync(fields, ids.ToArray());
        }

        /// <summary>
        /// Gets all fields for those torrents matching the hashes.
        /// </summary>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task<Torrent[]> TorrentGetAsync(IEnumerable<string> hashes)
        {
            return TorrentGetAsync(TorrentFields.All, hashes);
        }

        /// <summary>
        /// Gets the specified fields for those torrents matching the hashes.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task<Torrent[]> TorrentGetAsync(TorrentFields fields, IEnumerable<string> hashes)
        {
            return TorrentGetAsync(fields, hashes.ToArray());
        }

        /// <summary>
        /// Gets all fields for those torrents matching either the IDs or hashes.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task<Torrent[]> TorrentGetAsync(IEnumerable<int> ids, IEnumerable<string> hashes)
        {
            return TorrentGetAsync(TorrentFields.All, ids, hashes);
        }

        /// <summary>
        /// Gets the specified fields for those torrents matching either the IDs or hashes.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        public Task<Torrent[]> TorrentGetAsync(TorrentFields fields, IEnumerable<int> ids, IEnumerable<string> hashes)
        {
            return TorrentGetAsync(fields, ((IEnumerable<object>)ids).Concat(hashes).ToArray());
        }

        /// <summary>
        /// Gets the specified fields for torrents matching any type of torrent-identifier (see supported values in transmission-rpc spec or <paramref name="ids"/>).
        /// </summary>
        /// <typeparam name="T">type of IDs</typeparam>
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
        public Task<TorrentGetResponse> TorrentGetRecentAsync()
        {
            return TorrentGetRecentAsync(TorrentFields.All);
        }

        /// <summary>
        /// Gets the specified fields of all recently-active torrents.
        /// </summary>
        /// <param name="fields">fields to get, multiple fields can be combined with "|"</param>
        public Task<TorrentGetResponse> TorrentGetRecentAsync(TorrentFields fields)
        {
            var request = new TorrentGetRequest<string> { Fields = fields.ToStringRepresentation(), IDs = "recently-active" };
            return GetResponseAsync<TorrentGetResponse, TorrentGetRequest<string>>(request);
        }
    }

    public class TorrentGetResponse
    {
        [JsonProperty("torrents")]
        public Torrent[] Torrents { get; set; }
        [JsonProperty("removed")]
        public int[] Removed { get; set; }
    }

    internal class TorrentGetRequest<T> : ArgumentsBase
    {
        public override string MethodName => "torrent-get";

        [JsonProperty("fields")]
        public string[] Fields { get; set; }

        [JsonProperty("ids")]
        public T IDs { get; set; }
    }
}
