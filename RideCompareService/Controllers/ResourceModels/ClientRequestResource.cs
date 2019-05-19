namespace RideCompareService.Controllers.ResourceModels
{
    public sealed class ClientRequestResource
    {
        public ClientLocation StartingLocation { get; set; }
        public ClientLocation DestinationLocation { get; set; }
    }

    public sealed class ClientLocation
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
