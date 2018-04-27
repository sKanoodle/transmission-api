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
        public Task<GetFreeSpaceResponse> GetFreeSpaceAsync()
        {
            return GetFreeSpaceAsync("/");
        }

        public Task<GetFreeSpaceResponse> GetFreeSpaceAsync(string path)
        {
            return GetResponseAsync<GetFreeSpaceResponse, GetFreeSpaceArguments>(new GetFreeSpaceArguments { Path = path });
        }
    }

    internal class GetFreeSpaceArguments : ArgumentsBase
    {
        public override string MethodName => "free-space";

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class GetFreeSpaceResponse
    {
        /// <summary>
        /// size in bytes
        /// </summary>
        [JsonProperty("size-bytes")]
        public long Size { get; set; }
        /// <summary>
        /// path of the directory
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
