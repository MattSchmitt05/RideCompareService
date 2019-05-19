using RideCompareService.DomainLayer.Exceptions;
using RideCompareService.DomainLayer.Models;

namespace RideCompareService.Controllers.Validators
{
    internal static class RideCompareRequestValidator
    {
        // TODO: Add more logic for latitude and longitude
        public static void Validate(RideCompareRequest rideCompareRequest)
        {
            if (double.IsNaN(rideCompareRequest.StartingLatitude))
            {
                throw new RideCompareInvalidStartingLocationException(
                    $"Invalid Request - Received an invalid Starting Location Latitude value of ({rideCompareRequest.StartingLatitude}).");
            }

            if (double.IsNaN(rideCompareRequest.StartingLongitude))
            {
                throw new RideCompareInvalidStartingLocationException(
                    $"Invalid Request - Received an invalid Starting Location Longitude value of ({rideCompareRequest.StartingLongitude}).");
            }

            if (double.IsNaN(rideCompareRequest.DestinationLatitude))
            {
                throw new RideCompareInvalidDestinationLocationException(
                    $"Invalid Request - Received an invalid Destination Location Latitude value of ({rideCompareRequest.DestinationLatitude}).");
            }

            if (double.IsNaN(rideCompareRequest.DestinationLongitude))
            {
                throw new RideCompareInvalidDestinationLocationException(
                    $"Invalid Request - Received an invalid Destination Location Longitude value of ({rideCompareRequest.DestinationLongitude}).");
            }

            if (rideCompareRequest.StartingLatitude == 0 || rideCompareRequest.StartingLongitude == 0)
            {
                throw new RideCompareInvalidStartingLocationException(
                    "Invalid Request - Starting Latitude and Starting Longitude cannot be zero.");
            }

            if (rideCompareRequest.DestinationLatitude == 0 || rideCompareRequest.DestinationLongitude == 0)
            {
                throw new RideCompareInvalidDestinationLocationException(
                    "Invalid Request - Destination Latitude and Destination Longitude cannot be zero.");
            }

            if (rideCompareRequest.StartingLatitude == rideCompareRequest.StartingLongitude)
            {
                throw new RideCompareInvalidStartingLocationException(
                    "Invalid Request - Starting Latitude and Starting Longitude cannot be the same value.");
            }

            if (rideCompareRequest.DestinationLatitude == rideCompareRequest.DestinationLongitude)
            {
                throw new RideCompareInvalidDestinationLocationException(
                    "Invalid Request - Destination Latitude and Destination Longitude cannot be the same value.");
            }
        }
    }
}
