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
    public partial class Client
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

    internal class Response<T> : ResponseBase
    {
        [JsonProperty("arguments")]
        public T Data { get; set; }
    }

    internal class ResponseBase
    {
        [JsonProperty("result")]
        public string Result { get; set; }

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
