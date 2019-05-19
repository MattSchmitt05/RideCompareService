using RideCompareService.DomainLayer.Managers.Services.Uber.ResourceModels;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace AcceptanceTests.TestMediators
{
    [ExcludeFromCodeCoverage]
    internal static class TestMediatorForUberGateway
    {
        public static string ResponseString { get; set; }
        public static string ReasonPhrase { get; set; }

        public static HttpStatusCode HttpStatusCodeForUberRideTypes { get; set; }
        public static HttpStatusCode HttpStatusCodeForUberRideCostEstimate { get; set; }
        public static HttpStatusCode HttpStatusCodeForUberRideEtaEstimate { get; set; }
        public static UberRideTypeResource UberRideTypeResourceMock { get; set; }
        public static UberRideCostEstimateResource UberRideCostEstimateResourceMock { get; set; }
        public static UberRideEtaEstimateResource UberRideEtaEstimateResourceMock { get; set; }
        public static string UberRideCostEstimateValueMock { get; set; }
        public static string UberRideEtaEstimateValueMock { get; set; }

        public static void Reset()
        {
            ResetHttpProperties();
            ResetStatusCodes();
            ResetResourceMocks();
            ResetValueMocks();
        }

        private static void ResetHttpProperties()
        {
            ResponseString = null;
            ReasonPhrase = null;
        }

        private static void ResetStatusCodes()
        {
            HttpStatusCodeForUberRideTypes = HttpStatusCode.OK;
            HttpStatusCodeForUberRideCostEstimate = HttpStatusCode.OK;
            HttpStatusCodeForUberRideEtaEstimate = HttpStatusCode.OK;
        }

        private static void ResetResourceMocks()
        {
            UberRideTypeResourceMock = null;
            UberRideCostEstimateResourceMock = null;
            UberRideEtaEstimateResourceMock = null;
        }

        private static void ResetValueMocks()
        {
            UberRideCostEstimateValueMock = null;
            UberRideEtaEstimateValueMock = null;
        }
    }
}
