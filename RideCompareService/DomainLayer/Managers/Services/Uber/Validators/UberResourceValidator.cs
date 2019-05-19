using RideCompareService.DomainLayer.Managers.Services.Uber.Exceptions;
using RideCompareService.DomainLayer.Managers.Services.Uber.ResourceModels;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.Validators
{
    internal static class UberResourceValidator
    {
        public static void ValidateRideTypes(UberRideTypeResource rideTypeResource)
        {
            if (rideTypeResource == null)
                throw new UberGatewayInvalidRideTypesException("Invalid Ride Types Resource - Did not receive a ride types resource from Uber.");

            if (rideTypeResource.UberRides == null)
                throw new UberGatewayInvalidRideTypesException("Invalid Ride Types - Did not receive ride types from Uber.");
        }

        public static void ValidateRideCostEstimates(UberRideCostEstimateResource costEstimateResource)
        {
            if (costEstimateResource == null)
                throw new UberGatewayInvalidRideCostEstimatesException("Invalid Cost Estimates Resource - Did not receive a cost estimates resource from Uber.");

            if (costEstimateResource.Prices == null)
                throw new UberGatewayInvalidRideCostEstimatesException("Invalid Cost Estimates - Did not receive cost estimates from Uber.");
        }

        public static void ValidateRideEtaEstimates(UberRideEtaEstimateResource etaEstimateResource)
        {
            if (etaEstimateResource == null)
                throw new UberGatewayInvalidRideEtaEstimatesException("Invalid Eta Estimates Resource - Did not receive an eta estimates resource from Uber.");

            if (etaEstimateResource.Times == null)
                throw new UberGatewayInvalidRideEtaEstimatesException("Invalid Eta Estimates - Did not receive eta estimates from Uber.");
        }
    }
}
