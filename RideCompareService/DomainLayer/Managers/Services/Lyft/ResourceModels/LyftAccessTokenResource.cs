using Newtonsoft.Json;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.ResourceModels
{
    public sealed class LyftAccessTokenResource
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
    }
}
