using RideCompareService.Controllers.ResourceModels;
using RideCompareService.DomainLayer.Models;

namespace RideCompareService.Controllers.Mappers
{
    internal static class RideCompareRequestMapper
    {
        public static RideCompareRequest MapFrom(ClientRequestResource clientRequest)
        {
            var startLat = double.NaN;
            var startLng = double.NaN;
            var destLat = double.NaN;
            var destLng = double.NaN;

            if (double.TryParse(clientRequest.StartingLocation.Latitude, out var startLatResult))
            {
                startLat = startLatResult;
            }

            if (double.TryParse(clientRequest.StartingLocation.Longitude, out var startLngResult))
            {
                startLng = startLngResult;
            }

            if (double.TryParse(clientRequest.DestinationLocation.Latitude, out var destLatResult))
            {
                destLat = destLatResult;
            }

            if (double.TryParse(clientRequest.DestinationLocation.Longitude, out var destLngResult))
            {
                destLng = destLngResult;
            }

            var clientStartingLocation = new RideCompareLocation(startLat, startLng);
            var clientDestinationLocation = new RideCompareLocation(destLat, destLng);

            return new RideCompareRequest(clientStartingLocation, clientDestinationLocation);
        }
    }
}
