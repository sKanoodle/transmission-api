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
        /// Sets the location for all torrents.
        /// </summary>
        /// <param name="location">new location</param>
        /// <param name="move">If true, move from previous location. Otherwise, search "location" for files. (default: false)</param>
        public Task TorrentSetLocationAsync(string location, bool move = false)
        {
            return TorrentSetLocationAsync((string)null, location, move);
        }

        /// <summary>
        /// Sets the location for the single torrent matching the ID.
        /// </summary>
        /// <param name="id">single torrent ID</param>
        /// <param name="location">new location</param>
        /// <param name="move">If true, move from previous location. Otherwise, search "location" for files. (default: false)</param>
        public Task TorrentSetLocationAsync(int id, string location, bool move = false)
        {
            return TorrentSetLocationAsync<int>(id, location, move);
        }

        /// <summary>
        /// Sets the location for those torrents matching the torrent IDs.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="location">new location</param>
        /// <param name="move">If true, move from previous location. Otherwise, search "location" for files. (default: false)</param>
        public Task TorrentSetLocationAsync(IEnumerable<int> ids, string location, bool move = false)
        {
            return TorrentSetLocationAsync(ids.ToArray(), location, move);
        }

        /// <summary>
        /// Sets the location for those torrents matching the hashes.
        /// </summary>
        /// <param name="hashes">collection of torrent-hashes</param>
        /// <param name="location">new location</param>
        /// <param name="move">If true, move from previous location. Otherwise, search "location" for files. (default: false)</param>
        public Task TorrentSetLocationAsync(IEnumerable<string> hashes, string location, bool move = false)
        {
            return TorrentSetLocationAsync(hashes.ToArray(), location, move);
        }

        /// <summary>
        /// Sets the location for those torrents matching either the IDs or hashes.
        /// </summary>
        /// <param name="ids">collection of torrent IDs</param>
        /// <param name="hashes">collection of torrent-hashes</param>
        /// <param name="location">new location</param>
        /// <param name="move">If true, move from previous location. Otherwise, search "location" for files. (default: false)</param>
        public Task TorrentSetLocationAsync(IEnumerable<int> ids, IEnumerable<string> hashes, string location, bool move = false)
        {
            return TorrentSetLocationAsync(((IEnumerable<object>)ids).Concat(hashes).ToArray(), location, move);
        }

        /// <summary>
        /// Sets the location for all recently-active torrents.
        /// </summary>
        /// <param name="location">new location</param>
        /// <param name="move">If true, move from previous location. Otherwise, search "location" for files. (default: false)</param>
        public Task TorrentSetLocationRecentAsync(string location, bool move = false)
        {
            return TorrentSetLocationAsync("recently-active", location, move);
        }

        /// <summary>
        /// Sets the location for torrents matching any type of torrent-identifier (see supported values in transmission-rpc spec or <paramref name="ids"/>).
        /// </summary>
        /// <typeparam name="T">type of IDs</typeparam>
        /// <param name="ids">any type of supported value as ID (list of ints, hashstrings, or both in one list, int, (the string "recently-active" is a valid argument, but is handled in <see cref="TorrentSetLocationRecentAsync"/>, because it causes the result to have a new array with recently-deleted IDs))</param>
        /// <param name="location">new location</param>
        /// <param name="move">If true, move from previous location. Otherwise, search "location" for files. (default: false)</param>
        private async Task TorrentSetLocationAsync<T>(T ids, string location, bool move)
        {
            await GetResponseAsync<ResponseBase, SetLocationRequest<T>>(new SetLocationRequest<T> { Ids = ids, Location = location, Move = move });
        }
    }

    internal class SetLocationRequest<T> : ArgumentsBase
    {
        public override string MethodName => "torrent-set-location";
        [JsonProperty("ids")]
        public T Ids { get; set; }
        /// <summary>
        /// the new torrent location
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }
        /// <summary>
        /// If true, move from previous location. Otherwise, search "location" for files. (default: false)
        /// </summary>
        [JsonProperty("move")]
        public bool Move { get; set; }
    }
}
