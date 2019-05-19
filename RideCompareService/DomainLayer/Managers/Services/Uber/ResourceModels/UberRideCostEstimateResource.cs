using Newtonsoft.Json;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.ResourceModels
{
    public sealed class UberRideCostEstimateResource
    {
        [JsonProperty("prices")]
        public Price[] Prices { get; set; }

        public sealed class Price
        {
            [JsonProperty("localized_display_name")]
            public string LocalizedDisplayName { get; set; }

            [JsonProperty("distance")]
            public string Distance { get; set; }

            [JsonProperty("display_name")]
            public string DisplayName { get; set; }

            [JsonProperty("product_id")]
            public string ProductId { get; set; }

            [JsonProperty("high_estimate")]
            public string HighEstimate { get; set; }

            [JsonProperty("low_estimate")]
            public string LowEstimate { get; set; }

            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("estimate")]
            public string Estimate { get; set; }

            [JsonProperty("currency_code")]
            public string CurrencyCode { get; set; }
        }
    }
}
