using RideCompareService.DomainLayer.Models;
using System.Threading.Tasks;

namespace RideCompareService.DomainLayer
{
    public sealed partial class DomainFacade
    {
        public async Task<RideCompareResponse> GetBestRide(RideCompareRequest rideCompareRequest)
        {
            return await RideCompareManager.GetBestRide(rideCompareRequest);
        }
    }
}
