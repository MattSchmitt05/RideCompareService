using Newtonsoft.Json;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.ResourceModels
{
    public sealed class UberRideEtaEstimateResource
    {
        [JsonProperty("times")]
        public Time[] Times { get; set; }

        public sealed class Time
        {
            [JsonProperty("localized_display_name")]
            public string LocalizedDisplayName { get; set; }

            [JsonProperty("estimate")]
            public string Estimate { get; set; }

            [JsonProperty("display_name")]
            public string DisplayName { get; set; }

            [JsonProperty("product_id")]
            public string ProductId { get; set; }
        }
    }
}
