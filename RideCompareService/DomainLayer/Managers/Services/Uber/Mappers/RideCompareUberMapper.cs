using System.Collections.Generic;
using System.Linq;
using RideCompareService.DomainLayer.Enums;
using RideCompareService.DomainLayer.Models;
using RideCompareService.DomainLayer.Managers.Services.Uber.ResourceModels;

namespace RideCompareService.DomainLayer.Managers.Services.Uber.Mappers
{
    internal static class RideCompareUberMapper
    {
        public static List<RideCompareResponse> MapFromUberResources(UberRideTypeResource rideTypes, UberRideCostEstimateResource rideCost, UberRideEtaEstimateResource rideEta)
        {
            var rideShareProvider = nameof(RideShareProvider.Uber);
            var rides = new List<RideCompareResponse>();

            var allEstimatesAreZero = rideCost.Prices.All(r => r.HighEstimate.ToDecimal() == 0M) &&
                                      rideEta.Times.All(r => r.Estimate.ToInt() == 0);

            if (rideTypes.UberRides.Length == 0 ||
                rideCost.Prices.Length == 0 && rideEta.Times.Length == 0 ||
                allEstimatesAreZero)
            {
                rides.Add(new RideCompareResponse(rideShareProvider, 0M, 0));
                return rides;
            }

            foreach (var ride in rideTypes.UberRides)
            {
                decimal costEstimate = 0M;
                int etaEstimate = 0;

                foreach (var cost in rideCost.Prices)
                {
                    if (cost.DisplayName == ride.DisplayName)
                    {
                        costEstimate = cost.HighEstimate.ToDecimal() * 100;
                        break;
                    }
                }

                foreach (var eta in rideEta.Times)
                {
                    if (eta.DisplayName == ride.DisplayName)
                    {
                        etaEstimate = eta.Estimate.ToInt();
                        break;
                    }
                }

                rides.Add(new RideCompareResponse(rideShareProvider, costEstimate, etaEstimate));
            }

            return rides;
        }

        private static decimal ToDecimal(this string value) => decimal.TryParse(value, out var decimalResult) ? decimalResult : 0M;

        private static int ToInt(this string value) => int.TryParse(value, out var intResult) ? intResult : 0;
    }
}
