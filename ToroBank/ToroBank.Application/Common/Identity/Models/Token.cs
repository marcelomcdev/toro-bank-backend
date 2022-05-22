using Newtonsoft.Json;

namespace ToroBank.Application.Common.Identity.Models
{
    public class Token
    {
        [JsonProperty("access-token")]
        public string? AccessToken { get; set; }

        [JsonProperty("client")]
        public string? Client { get; set; }

        [JsonProperty("uid")]
        public string? UID { get; set; }
    }
}
