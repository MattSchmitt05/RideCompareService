using Newtonsoft.Json;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.ResourceModels
{
    public sealed class LyftRideCostEstimateResource
    {
        [JsonProperty("cost_estimates")]
        public CostEstimate[] CostEstimates { get; set; }

        public sealed class CostEstimate
        {
            [JsonProperty("ride_type")]
            public string RideType { get; set; }

            [JsonProperty("estimated_duration_seconds")]
            public string EstimatedDurationSeconds { get; set; }

            [JsonProperty("estimated_distance_miles")]
            public string EstimatedDistanceMiles { get; set; }

            [JsonProperty("estimated_cost_cents_max")]
            public string EstimatedCostCentsMax { get; set; }

            [JsonProperty("primetime_percentage")]
            public string PrimetimePercentage { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("estimated_cost_cents_min")]
            public string EstimatedCostCentsMin { get; set; }

            [JsonProperty("display_name")]
            public string DisplayName { get; set; }

            [JsonProperty("primetime_confirmation_token")]
            public string PrimetimeConfirmationToken { get; set; }

            [JsonProperty("cost_token")]
            public string CostToken { get; set; }

            [JsonProperty("is_valid_estimate")]
            public string IsValidEstimate { get; set; }
        }
    }
}
