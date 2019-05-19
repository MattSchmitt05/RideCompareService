using RideCompareService.DomainLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft
{
    internal abstract class LyftGatewayBase
    {
        public async Task<List<RideCompareResponse>> GetRides(RideCompareRequest rideCompareRequest) => await GetRidesCore(rideCompareRequest);

        protected abstract Task<List<RideCompareResponse>> GetRidesCore(RideCompareRequest rideCompareRequest);
    }
}
