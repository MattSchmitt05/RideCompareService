using AcceptanceTests.TestMediators;
using RideCompareService.DomainLayer.Managers.Services.Uber;
using RideCompareService.DomainLayer.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace AcceptanceTests.TestDoubles.Managers.Services.Uber
{
    [ExcludeFromCodeCoverage]
    internal sealed class UberGatewaySpy : UberGatewayBase
    {
        protected override async Task<List<RideCompareResponse>> GetRidesCore(RideCompareRequest rideCompareRequest)
        {
            return await GetDataOrThrow();
        }

        private async Task<List<RideCompareResponse>> GetDataOrThrow()
        {
            if (TestMediatorForDomainFacadeRideCompare.ExceptionToThrowFromUberGateway != null)
            {
                throw TestMediatorForDomainFacadeRideCompare.ExceptionToThrowFromUberGateway;
            }

            return await Task.FromResult(TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway);
        }
    }
}
