using RideCompareService.DomainLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RideCompareService.DomainLayer.Managers.Services.Uber
{
    internal abstract class UberGatewayBase
    {
        public async Task<List<RideCompareResponse>> GetRides(RideCompareRequest rideCompareRequest) => await GetRidesCore(rideCompareRequest);

        protected abstract Task<List<RideCompareResponse>> GetRidesCore(RideCompareRequest rideCompareRequest);
    }
}
