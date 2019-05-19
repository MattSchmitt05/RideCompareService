using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RideCompareService.DomainLayer.Models;

namespace AcceptanceTests.TestMediators
{
    [ExcludeFromCodeCoverage]
    internal static class TestMediatorForDomainFacadeRideCompare
    {           
        public static RideCompareRequest ValidRideCompareRequestModelMock { get => GetValidRideCompareRequestModelMock(); }

        public static Exception ExceptionToThrowFromLyftGateway { get; internal set; }
        public static List<RideCompareResponse> RidesToReturnFromLyftGateway { get; set; }

        public static Exception ExceptionToThrowFromUberGateway { get; set; }
        public static List<RideCompareResponse> RidesToReturnFromUberGateway { get; set; }

        public static void Reset()
        {
            ResetRidesToReturn();
            ResetExceptionsToThrow();
        }

        private static void ResetRidesToReturn()
        {
            RidesToReturnFromLyftGateway = null;
            RidesToReturnFromUberGateway = null;
        }

        private static void ResetExceptionsToThrow()
        {
            ExceptionToThrowFromLyftGateway = null;
            ExceptionToThrowFromUberGateway = null;
        }

        private static RideCompareRequest GetValidRideCompareRequestModelMock()
        {
            return new RideCompareRequest(new RideCompareLocation(1, -1), new RideCompareLocation(2, -2));
        }
    }
}
