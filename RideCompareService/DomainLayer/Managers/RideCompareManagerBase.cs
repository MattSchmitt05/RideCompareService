using RideCompareService.DomainLayer.Models;
using System.Threading.Tasks;

namespace RideCompareService.DomainLayer.Managers
{
    internal abstract class RideCompareManagerBase
    {
        public async Task<RideCompareResponse> GetBestRide(RideCompareRequest rideCompareRequest) => await GetBestRideCore(rideCompareRequest);

        protected abstract Task<RideCompareResponse> GetBestRideCore(RideCompareRequest rideCompareRequest);
    }
}
