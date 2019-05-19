using System.Collections.Generic;
using RideCompareService.DomainLayer.Exceptions;
using RideCompareService.DomainLayer.Models;

namespace RideCompareService.DomainLayer.Managers.Validators
{
    internal static class RideCompareResponseValidator
    {
        public static void Validate(List<RideCompareResponse> lyftRides, List<RideCompareResponse> uberRides)
        {
            if (lyftRides.TrueForAll(r => r.RideCost == 0M && r.RideEta == 0) && uberRides.TrueForAll(r => r.RideCost == 0M && r.RideEta == 0))
                throw new RideCompareNoBestRideFoundException("No best ride was found because all ride costs AND etas were 0.");

            if (lyftRides.TrueForAll(r => r.RideCost == 0M) && uberRides.TrueForAll(r => r.RideCost == 0M))
                throw new RideCompareNoBestRideFoundException("No best ride was found because all ride costs were 0.");

            if (lyftRides.TrueForAll(r => r.RideEta == 0M) && uberRides.TrueForAll(r => r.RideEta == 0M))
                throw new RideCompareNoBestRideFoundException("No best ride was found because all ride etas were 0.");
        }
    }
}
