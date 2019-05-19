using RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions;
using RideCompareService.DomainLayer.Managers.Services.Lyft.ResourceModels;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.Validators
{
    internal static class LyftResourceValidator
    {
        public static void ValidateAccessToken(LyftAccessTokenResource accessTokenResource)
        {
            if (accessTokenResource == null)
                throw new LyftGatewayInvalidAccessTokenException("Invalid Access Token Resource - Did not receive an access token resource from Lyft.");

            if (accessTokenResource.AccessToken == null || accessTokenResource.ExpiresIn == null)
                throw new LyftGatewayInvalidAccessTokenException("Invalid Access Token Resource - Did not receive an access token resource from Lyft.");

            if (accessTokenResource.AccessToken.Length == 0)
                throw new LyftGatewayInvalidAccessTokenException("Invalid Access Token - Received an empty access token from Lyft.");

            if (accessTokenResource.ExpiresIn.Length == 0)
                throw new LyftGatewayInvalidAccessTokenException("Invalid Access Token - Received an empty access token expiration value from Lyft.");
        }

        public static void ValidateRideTypes(LyftRideTypeResource rideTypeResource)
        {
            if (rideTypeResource == null)
                throw new LyftGatewayInvalidRideTypesException("Invalid Ride Types Resource - Did not receive a ride types resource from Lyft.");

            if (rideTypeResource.LyftRides == null)
                throw new LyftGatewayInvalidRideTypesException("Invalid Ride Types - Did not receive ride types from Lyft.");
        }

        public static void ValidateRideCostEstimates(LyftRideCostEstimateResource costEstimateResource)
        {
            if (costEstimateResource == null)
                throw new LyftGatewayInvalidRideCostEstimatesException("Invalid Cost Estimates Resource - Did not receive a cost estimates resource from Lyft.");

            if (costEstimateResource.CostEstimates == null)
                throw new LyftGatewayInvalidRideCostEstimatesException("Invalid Cost Estimates - Did not receive cost estimates from Lyft.");
        }

        public static void ValidateRideEtaEstimates(LyftRideEtaEstimateResource etaEstimateResource)
        {
            if (etaEstimateResource == null)
                throw new LyftGatewayInvalidRideEtaEstimatesException("Invalid Eta Estimates Resource - Did not receive a eta estimates resource from Lyft.");

            if (etaEstimateResource.EtaEstimates == null)
                throw new LyftGatewayInvalidRideEtaEstimatesException("Invalid Eta Estimates - Did not receive eta estimates from Lyft.");
        }
    }
}
