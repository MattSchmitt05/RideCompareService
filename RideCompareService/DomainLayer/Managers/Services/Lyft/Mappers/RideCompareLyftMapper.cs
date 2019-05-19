using System.Collections.Generic;
using System.Linq;
using RideCompareService.DomainLayer.Enums;
using RideCompareService.DomainLayer.Models;
using RideCompareService.DomainLayer.Managers.Services.Lyft.ResourceModels;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft.Mappers
{
    internal static class RideCompareLyftMapper
    {
        public static List<RideCompareResponse> MapFromLyftResources(LyftRideTypeResource rideTypes, LyftRideCostEstimateResource rideCost, LyftRideEtaEstimateResource rideEta)
        {
            var rideShareProvider = nameof(RideShareProvider.Lyft);
            var rides = new List<RideCompareResponse>();

            var allEstimatesAreZero = rideCost.CostEstimates.All(r => r.EstimatedCostCentsMax.ToDecimal() == 0M) &&
                                      rideEta.EtaEstimates.All(r => r.EtaSeconds.ToInt() == 0);

            if (rideTypes.LyftRides.Length == 0 ||
                rideCost.CostEstimates.Length == 0 && rideEta.EtaEstimates.Length == 0 ||
                allEstimatesAreZero)
            {
                rides.Add(new RideCompareResponse(nameof(RideShareProvider.Lyft), 0M, 0));
                return rides;
            }

            foreach (var ride in rideTypes.LyftRides)
            {
                decimal costEstimate = 0M;
                int etaEstimate = 0;

                foreach (var cost in rideCost.CostEstimates)
                {
                    if (cost.RideType == ride.RideType)
                    {
                        costEstimate = cost.EstimatedCostCentsMax.ToDecimal();
                        break;
                    }
                }

                foreach (var eta in rideEta.EtaEstimates)
                {
                    if (eta.RideType == ride.RideType)
                    {
                        etaEstimate = eta.EtaSeconds.ToInt();
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
