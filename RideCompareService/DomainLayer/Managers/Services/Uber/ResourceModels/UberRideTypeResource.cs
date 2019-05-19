using Newtonsoft.Json;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.ResourceModels
{
    public sealed class UberRideTypeResource
    {
        [JsonProperty("products")]
        public UberRide[] UberRides { get; set; }

        public sealed class UberRide
        {
            [JsonProperty("capacity")]
            public string Capacity { get; set; }

            [JsonProperty("product_id")]
            public string ProductId { get; set; }

            [JsonProperty("cash_enabled")]
            public string CashEnabled { get; set; }

            [JsonProperty("shared")]
            public string Shared { get; set; }

            [JsonProperty("short_description")]
            public string ShortDescription { get; set; }

            [JsonProperty("display_name")]
            public string DisplayName { get; set; }

            [JsonProperty("product_group")]
            public string ProductGroup { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }
        }
    }
}
