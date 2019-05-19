namespace RideCompareService.DomainLayer.Models
{
    public sealed class RideCompareRequest
    {
        public double StartingLatitude { get; set; }
        public double StartingLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }

        public RideCompareRequest(RideCompareLocation startingLocation, RideCompareLocation destinationLocation)
        {
            StartingLatitude = startingLocation.Latitude;
            StartingLongitude = startingLocation.Longitude;
            DestinationLatitude = destinationLocation.Latitude;
            DestinationLongitude = destinationLocation.Longitude;
        }
    }

    public sealed class RideCompareLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public RideCompareLocation(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }
    }
}
