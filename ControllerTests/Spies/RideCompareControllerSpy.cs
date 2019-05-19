using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ControllerTests.TestMediators;
using RideCompareService.Controllers;
using RideCompareService.DomainLayer.Models;

namespace ControllerTests.Spies
{
    [ExcludeFromCodeCoverage]
    internal sealed class RideCompareControllerSpy : RideCompareController
    {
        protected override async Task<RideCompareResponse> GetBestRide(RideCompareRequest rideCompareRequest)
        {
            return await GetDataOrThrow();
        }

        private async Task<RideCompareResponse> GetDataOrThrow()
        {
            if (TestMediatorForControllerSpy.ExceptionToThrow != null)
            {
                throw TestMediatorForControllerSpy.ExceptionToThrow;
            }

            return await Task.FromResult(TestMediatorForControllerSpy.BestRideToReturn);
        }
    }
}
