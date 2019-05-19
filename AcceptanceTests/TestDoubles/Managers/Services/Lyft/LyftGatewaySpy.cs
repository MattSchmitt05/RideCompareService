using AcceptanceTests.TestMediators;
using RideCompareService.DomainLayer.Managers.Services.Lyft;
using RideCompareService.DomainLayer.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace AcceptanceTests.TestDoubles.Managers.Services.Lyft
{
    [ExcludeFromCodeCoverage]
    internal sealed class LyftGatewaySpy : LyftGatewayBase
    {
        protected override async Task<List<RideCompareResponse>> GetRidesCore(RideCompareRequest rideCompareRequest)
        {
            return await GetDataOrThrow();
        }

        private async Task<List<RideCompareResponse>> GetDataOrThrow()
        {
            if (TestMediatorForDomainFacadeRideCompare.ExceptionToThrowFromLyftGateway != null)
            {
                throw TestMediatorForDomainFacadeRideCompare.ExceptionToThrowFromLyftGateway;
            }

            return await Task.FromResult(TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway);
        }
    }
}
