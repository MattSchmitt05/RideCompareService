namespace RideCompareService.DomainLayer.Models
{
    public sealed class RideCompareResponse
    {
        public string RideShareProvider { get; }
        public decimal RideCost { get; }
        public int RideEta { get; }
        public string LowestRideCostProvider { get; set; }
        public string ShortestRideEtaProvider { get; set; }

        public RideCompareResponse(string rideShareProvider, decimal rideCost, int rideEta, 
                                   string lowestRideCostProvider = null, string shortestRideEtaProvider = null)
        {
            RideShareProvider = rideShareProvider;
            RideCost = rideCost;
            RideEta = rideEta;
            LowestRideCostProvider = lowestRideCostProvider;
            ShortestRideEtaProvider = shortestRideEtaProvider;
        }
    }
}
