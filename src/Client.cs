using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Transmission.Api.Entities;

namespace Transmission.Api
{
    public class Client
    {
        private const string ID_HEADER = "X-Transmission-Session-Id";
        private readonly HttpClient HttpClient;
        private readonly string Address;

        public Client(string address, string user, string pass)
        {
            Address = address;
            HttpClient = new HttpClient(new HttpClientHandler { Credentials = new NetworkCredential(user, pass) });
        }

        public Client(string address)
        {
            Address = address;
            HttpClient = new HttpClient();
        }

        public async Task<Torrent[]> TorrentGetAsync()
        {
            return await TorrentGetAsync(TorrentFields.All);
        }

        public async Task<Torrent[]> TorrentGetAsync(TorrentFields fields)
        {
            return await TorrentGetAsync(fields, (string)null);
        }

        public async Task<Torrent[]> TorrentGetAsync(int id)
        {
            return await TorrentGetAsync(TorrentFields.All, id);
        }

        public async Task<Torrent[]> TorrentGetAsync(TorrentFields fields, int id)
        {
            return await TorrentGetAsync<int>(fields, id);
        }

        public async Task<Torrent[]> TorrentGetAsync(IEnumerable<int> ids)
        {
            return await TorrentGetAsync(TorrentFields.All, ids);
        }

        public async Task<Torrent[]> TorrentGetAsync(TorrentFields fields, IEnumerable<int> ids)
        {
            return await TorrentGetAsync(fields, ids.ToArray());
        }

        public async Task<Torrent[]> TorrentGetAsync(IEnumerable<string> hashes)
        {
            return await TorrentGetAsync(TorrentFields.All, hashes);
        }

        public async Task<Torrent[]> TorrentGetAsync(TorrentFields fields, IEnumerable<string> hashes)
        {
            return await TorrentGetAsync(fields, hashes.ToArray());
        }

        public async Task<Torrent[]> TorrentGetAsync(IEnumerable<int> ids, IEnumerable<string> hashes)
        {
            return await TorrentGetAsync(TorrentFields.All, ids, hashes);
        }

        public async Task<Torrent[]> TorrentGetAsync(TorrentFields fields, IEnumerable<int> ids, IEnumerable<string> hashes)
        {
            return await TorrentGetAsync(fields, ((IEnumerable<object>)ids).Concat(hashes).ToArray());
        }

        private async Task<Torrent[]> TorrentGetAsync<T>(TorrentFields fields, T ids)
        {
            var request = new TorrentGetRequest<T> { Fields = fields.ToStringRepresentation(), IDs = ids };
            var response = await GetResponseAsync<TorrentGetResponse, TorrentGetRequest<T>>(request);
            return response.Torrents;
        }

        public async Task<TorrentGetResponse> TorrentGetRecentAsync()
        {
            return await TorrentGetRecentAsync(TorrentFields.All);
        }

        public async Task<TorrentGetResponse> TorrentGetRecentAsync(TorrentFields fields)
        {
            var request = new TorrentGetRequest<string> { Fields = fields.ToStringRepresentation(), IDs = "recently-active" };
            return await GetResponseAsync<TorrentGetResponse, TorrentGetRequest<string>>(request);
        }

        private async Task<T> GetResponseAsync<T, U>(U request) where U : ArgumentsBase
        {
            var wrappedRequest = new Request<U> { Arguments = request };
            var response = await HttpClient.PostAsync(Address, new StringContent(JsonConvert.SerializeObject(wrappedRequest, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore })));
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return JsonConvert.DeserializeObject<Response<T>>(await response.Content.ReadAsStringAsync()).Data;
                case HttpStatusCode.Conflict: // 409 means header with session ID is not set, so just set header and try again
                    HttpClient.DefaultRequestHeaders.Remove(ID_HEADER);
                    HttpClient.DefaultRequestHeaders.Add(ID_HEADER, response.Headers.GetValues(ID_HEADER));
                    return await GetResponseAsync<T, U>(request);
                default:
                    throw new NotImplementedException();
            }
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

    internal class Response<T>
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("arguments")]
        public T Data { get; set; }

        [JsonProperty("tag")]
        public int Tag { get; set; }
    }

    internal abstract class ArgumentsBase
    {
        [JsonIgnore]
        public abstract string MethodName { get; }
    }

    internal class Request<T> where T : ArgumentsBase
    {
        [JsonProperty("method")]
        public string Method => Arguments.MethodName;

        [JsonProperty("arguments")]
        public T Arguments { get; set; }

        [JsonProperty("tag")]
        public int Tag { get; set; }
    }
}
