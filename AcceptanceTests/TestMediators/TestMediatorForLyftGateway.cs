using RideCompareService.DomainLayer.Managers.Services.Lyft.ResourceModels;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace AcceptanceTests.TestMediators
{
    [ExcludeFromCodeCoverage]
    internal static class TestMediatorForLyftGateway
    {
        public static string ResponseString { get; set; }
        public static string ReasonPhrase { get; set; }

        public static HttpStatusCode HttpStatusCodeForLyftAccessToken { get; set; }
        public static HttpStatusCode HttpStatusCodeForLyftRideTypes { get; set; }
        public static HttpStatusCode HttpStatusCodeForLyftRideCostEstimate { get; set; }
        public static HttpStatusCode HttpStatusCodeForLyftRideEtaEstimate { get; set; }
        public static LyftAccessTokenResource LyftAccessTokenResourceMock { get; set; }
        public static LyftRideTypeResource LyftRideTypeResourceMock { get; set; }
        public static LyftRideCostEstimateResource LyftRideCostEstimateResourceMock { get; set; }
        public static LyftRideEtaEstimateResource LyftRideEtaEstimateResourceMock { get; set; }
        public static string LyftRideCostEstimateValueMock { get; set; }
        public static string LyftRideEtaEstimateValueMock { get; set; }

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
            HttpStatusCodeForLyftAccessToken = HttpStatusCode.OK;
            HttpStatusCodeForLyftRideTypes = HttpStatusCode.OK;
            HttpStatusCodeForLyftRideCostEstimate = HttpStatusCode.OK;
            HttpStatusCodeForLyftRideEtaEstimate = HttpStatusCode.OK;
        }

        private static void ResetResourceMocks()
        {
            LyftAccessTokenResourceMock = null;
            LyftRideTypeResourceMock = null;
            LyftRideCostEstimateResourceMock = null;
            LyftRideEtaEstimateResourceMock = null;
        }

        private static void ResetValueMocks()
        {
            LyftRideCostEstimateValueMock = null;
            LyftRideEtaEstimateValueMock = null;
        }
    }
}
