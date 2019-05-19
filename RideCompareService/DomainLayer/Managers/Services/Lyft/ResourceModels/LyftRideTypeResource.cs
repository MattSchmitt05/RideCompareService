using Newtonsoft.Json;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.ResourceModels
{
    public sealed class LyftRideTypeResource
    {
        [JsonProperty("ride_types")]
        public LyftRide[] LyftRides { get; set; }

        public sealed class LyftRide
        {
            [JsonProperty("ride_type")]
            public string RideType { get; set; }

            [JsonProperty("display_name")]
            public string DisplayName { get; set; }

            [JsonProperty("seats")]
            public string Seats { get; set; }
        }
    }
}
