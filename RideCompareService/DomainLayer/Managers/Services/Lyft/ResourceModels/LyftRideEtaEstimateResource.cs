using Newtonsoft.Json;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.ResourceModels
{
    public class LyftRideEtaEstimateResource
    {
        [JsonProperty("eta_estimates")]
        public EtaEstimate[] EtaEstimates { get; set; }

        public class EtaEstimate
        {
            [JsonProperty("display_name")]
            public string DisplayName { get; set; }

            [JsonProperty("ride_type")]
            public string RideType { get; set; }

            [JsonProperty("eta_seconds")]
            public string EtaSeconds { get; set; }

            [JsonProperty("is_valid_estimate")]
            public string IsValidEstimate { get; set; }
        }
    }
}
