using AcceptanceTests.ServiceLocator;
using AcceptanceTests.TestMediators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RideCompareService.DomainLayer.Managers.Services.Lyft;
using RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions;
using RideCompareService.DomainLayer.Managers.Services.Lyft.ResourceModels;
using RideCompareService.DomainLayer.Managers.Services.Uber;
using RideCompareService.DomainLayer.Managers.Services.Uber.Exceptions;
using RideCompareService.DomainLayer.Managers.Services.Uber.ResourceModels;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;

namespace AcceptanceTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GatewayTests
    {
        private static LyftGateway _lyftGatewayUnderTest;
        private static UberGateway _uberGatewayUnderTest;

        [ClassInitialize]
        public static void InitializeGatewaysUnderTest(TestContext testContext)
        {
            var serviceLocatorForTest = new ServiceLocatorForTest();
            _lyftGatewayUnderTest = new LyftGateway("https://api.lyft.com/", "test", serviceLocatorForTest);
            _uberGatewayUnderTest = new UberGateway("https://api.uber.com/", "test", serviceLocatorForTest);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestMediatorForLyftGateway.Reset();
            TestMediatorForUberGateway.Reset();
        }

        #region Lyft Gateway Exceptions

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetRides_WhenLyftGatewayServiceCallResultsInABadRequest_ShouldThrow()
        {
            // Arrange
            var expectedReasonPhrase = "Testing Http Exception";
            TestMediatorForLyftGateway.ReasonPhrase = expectedReasonPhrase;

            SetStatusCodeForLyftServiceCalls(LyftServiceEndpoint.Token, HttpStatusCode.BadRequest);
            SetResourceMocksForLyftGateway();

            try
            {
                // Act
                await _lyftGatewayUnderTest.GetRides(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);
                Assert.Fail($"Expected a {nameof(LyftServiceBadRequestException)} to be thrown, but none was thrown.");
            }
            catch (LyftServiceBadRequestException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, "400", "Expected to find an error code of 400 in our exception message, but it was not found.");
                StringAssert.Contains(actualExceptionMessage, expectedReasonPhrase, $"Expected to find \"{expectedReasonPhrase}\" as the Content, but it was not found.");
            }
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetRides_LyftGatewayServiceCallResultsInANotFoundError_ShouldThrow()
        {
            // Arrange
            var expectedReasonPhrase = "Testing Http Exception";
            TestMediatorForLyftGateway.ReasonPhrase = expectedReasonPhrase;

            SetStatusCodeForLyftServiceCalls(LyftServiceEndpoint.Ridetypes, HttpStatusCode.NotFound);
            SetResourceMocksForLyftGateway();

            try
            {
                // Act
                await _lyftGatewayUnderTest.GetRides(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);
                Assert.Fail($"Expected a {nameof(LyftServiceNotFoundException)} to be thrown, but none was thrown.");
            }
            catch (LyftServiceNotFoundException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, "404", "Expected to find an error code of 404 in our exception message, but it was not found.");
                StringAssert.Contains(actualExceptionMessage, expectedReasonPhrase, $"Expected to find \"{expectedReasonPhrase}\" as the Content, but it was not found.");
            }
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetRides_LyftGatewayServiceCallResultsInAnInternalServerError_ShouldThrow()
        {
            // Arrange
            var expectedReasonPhrase = "Testing Http Exception";
            TestMediatorForLyftGateway.ReasonPhrase = expectedReasonPhrase;

            SetStatusCodeForLyftServiceCalls(LyftServiceEndpoint.Cost, HttpStatusCode.InternalServerError);
            SetResourceMocksForLyftGateway();

            try
            {
                // Act
                await _lyftGatewayUnderTest.GetRides(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);
                Assert.Fail($"Expected a {nameof(LyftServiceInternalServerErrorException)} to be thrown, but none was thrown.");
            }
            catch (LyftServiceInternalServerErrorException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, "500", "Expected to find an error code of 500 in our exception message, but it was not found.");
                StringAssert.Contains(actualExceptionMessage, expectedReasonPhrase, $"Expected to find \"{expectedReasonPhrase}\" as the Content, but it was not found.");
            }
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_LyftGatewayServiceCallResultsInAnUnhandledErrorCode_ShouldThrow()
        {
            // Arrange
            var expectedReasonPhrase = "Testing Http Exception";
            TestMediatorForLyftGateway.ReasonPhrase = expectedReasonPhrase;

            SetStatusCodeForLyftServiceCalls(LyftServiceEndpoint.Eta, HttpStatusCode.Forbidden);
            SetResourceMocksForLyftGateway();

            try
            {
                // Act
                await _lyftGatewayUnderTest.GetRides(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);
                Assert.Fail($"Expected a {nameof(LyftServiceUnhandledErrorCodeException)} to be thrown, but none was thrown.");
            }
            catch (LyftServiceUnhandledErrorCodeException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, "403", "Expected to find an error code of 403 in our exception message, but it was not found.");
                StringAssert.Contains(actualExceptionMessage, expectedReasonPhrase, $"Expected to find \"{expectedReasonPhrase}\" as the Content, but it was not found.");
            }
        }

        #endregion Lyft Gateway Exceptions


        #region Uber Gateway Exceptions

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetRides_WhenUberGatewayServiceCallResultsInABadRequest_ShouldThrow()
        {
            // Arrange
            var expectedReasonPhrase = "Testing Http Exception";
            TestMediatorForUberGateway.ReasonPhrase = expectedReasonPhrase;

            SetStatusCodeForUberServiceCalls(UberServiceEndpoint.Products, HttpStatusCode.BadRequest);
            SetResourceMocksForUberGateway();

            try
            {
                // Act
                await _uberGatewayUnderTest.GetRides(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);
                Assert.Fail($"Expected a {nameof(UberServiceBadRequestException)} to be thrown, but none was thrown.");
            }
            catch (UberServiceBadRequestException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, "400", "Expected to find an error code of 400 in our exception message, but it was not found.");
                StringAssert.Contains(actualExceptionMessage, expectedReasonPhrase, $"Expected to find \"{expectedReasonPhrase}\" as the Reason Phrase, but it was not found.");
            }
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetRides_WhenUberGatewayServiceCallResultsInANotFoundError_ShouldThrow()
        {
            // Arrange
            var expectedReasonPhrase = "Testing Http Exception";
            TestMediatorForUberGateway.ReasonPhrase = expectedReasonPhrase;

            SetStatusCodeForUberServiceCalls(UberServiceEndpoint.Price, HttpStatusCode.NotFound);
            SetResourceMocksForUberGateway();

            try
            {
                // Act
                await _uberGatewayUnderTest.GetRides(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);
                Assert.Fail($"Expected a {nameof(UberServiceNotFoundException)} to be thrown, but none was thrown.");
            }
            catch (UberServiceNotFoundException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, "404", "Expected to find an error code of 404 in our exception message, but it was not found.");
                StringAssert.Contains(actualExceptionMessage, expectedReasonPhrase, $"Expected to find \"{expectedReasonPhrase}\" as the Reason Phrase, but it was not found.");
            }
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenUberGatewayServiceCallResultsInAnInternalServerError_ShouldThrow()
        {
            // Arrange
            var expectedReasonPhrase = "Testing Http Exception";
            TestMediatorForUberGateway.ReasonPhrase = expectedReasonPhrase;

            SetStatusCodeForUberServiceCalls(UberServiceEndpoint.Time, HttpStatusCode.InternalServerError);
            SetResourceMocksForUberGateway();

            try
            {
                // Act
                await _uberGatewayUnderTest.GetRides(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);
                Assert.Fail($"Expected a {nameof(UberServiceInternalServerErrorException)} to be thrown, but none was thrown.");
            }
            catch (UberServiceInternalServerErrorException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, "500", "Expected to find an error code of 500 in our exception message, but it was not found.");
                StringAssert.Contains(actualExceptionMessage, expectedReasonPhrase, $"Expected to find \"{expectedReasonPhrase}\" as the Reason Phrase, but it was not found.");
            }
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenUberGatewayServiceCallResultsInAnUnhandledErrorCode_ShouldThrow()
        {
            // Arrange
            var expectedReasonPhrase = "Testing Http Exception";
            TestMediatorForUberGateway.ReasonPhrase = expectedReasonPhrase;

            SetStatusCodeForUberServiceCalls(UberServiceEndpoint.Products, HttpStatusCode.Forbidden);
            SetResourceMocksForUberGateway();

            try
            {
                // Act
                await _uberGatewayUnderTest.GetRides(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);
                Assert.Fail($"Expected a {nameof(UberServiceUnhandledErrorCodeException)} to be thrown, but none was thrown.");
            }
            catch (UberServiceUnhandledErrorCodeException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, "403", "Expected to find an error code of 403 in our exception message, but it was not found.");
                StringAssert.Contains(actualExceptionMessage, expectedReasonPhrase, $"Expected to find \"{expectedReasonPhrase}\" as the Reason Phrase, but it was not found.");
            }
        }

        #endregion Uber Gateway Exceptions

        private static void SetResourceMocksForLyftGateway()
        {
            var estimateValueMock = "9999";

            TestMediatorForLyftGateway.LyftAccessTokenResourceMock = new LyftAccessTokenResource { AccessToken = "test", ExpiresIn = "86400" };
            TestMediatorForLyftGateway.LyftRideTypeResourceMock = new LyftRideTypeResource
            {
                LyftRides = new[]
                {
                    new LyftRideTypeResource.LyftRide
                    {
                        RideType = "test",
                        DisplayName = "test"
                    }
                }
            };
            TestMediatorForLyftGateway.LyftRideCostEstimateResourceMock = new LyftRideCostEstimateResource
            {
                CostEstimates = new[]
                {
                    new LyftRideCostEstimateResource.CostEstimate
                    {
                        RideType = "test",
                        EstimatedCostCentsMax = estimateValueMock
                    }
                }
            };
            TestMediatorForLyftGateway.LyftRideEtaEstimateResourceMock = new LyftRideEtaEstimateResource
            {
                EtaEstimates = new[]
                {
                    new LyftRideEtaEstimateResource.EtaEstimate
                    {
                        RideType = "test",
                        EtaSeconds = estimateValueMock
                    }
                }
            };
        }

        private static void SetResourceMocksForUberGateway()
        {
            var estimateValueMock = "9999";

            TestMediatorForUberGateway.UberRideTypeResourceMock = new UberRideTypeResource
            {
                UberRides = new[]
                {
                    new UberRideTypeResource.UberRide
                    {
                        DisplayName = "test"
                    }
                }
            };
            TestMediatorForUberGateway.UberRideCostEstimateResourceMock = new UberRideCostEstimateResource
            {
                Prices = new[]
                {
                    new UberRideCostEstimateResource.Price
                    {
                        DisplayName = "test",
                        HighEstimate = estimateValueMock
                    }
                }
            };
            TestMediatorForUberGateway.UberRideEtaEstimateResourceMock = new UberRideEtaEstimateResource
            {
                Times = new[]
                {
                    new UberRideEtaEstimateResource.Time
                    {
                        DisplayName = "test",
                        Estimate = estimateValueMock
                    }
                }
            };
        }

        private static void SetStatusCodeForLyftServiceCalls(LyftServiceEndpoint endpoint, HttpStatusCode statusCode)
        {
            switch (endpoint)
            {
                case LyftServiceEndpoint.Token:
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftAccessToken = statusCode;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideTypes = HttpStatusCode.OK;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideCostEstimate = HttpStatusCode.OK;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideEtaEstimate = HttpStatusCode.OK;
                    break;
                case LyftServiceEndpoint.Ridetypes:
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftAccessToken = HttpStatusCode.OK;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideTypes = statusCode;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideCostEstimate = HttpStatusCode.OK;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideEtaEstimate = HttpStatusCode.OK;
                    break;
                case LyftServiceEndpoint.Cost:
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftAccessToken = HttpStatusCode.OK;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideTypes = HttpStatusCode.OK;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideCostEstimate = statusCode;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideEtaEstimate = HttpStatusCode.OK;
                    break;
                case LyftServiceEndpoint.Eta:
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftAccessToken = HttpStatusCode.OK;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideTypes = HttpStatusCode.OK;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideCostEstimate = HttpStatusCode.OK;
                    TestMediatorForLyftGateway.HttpStatusCodeForLyftRideEtaEstimate = statusCode;
                    break;
                default:
                    throw new Exception("Valid Lyft endpoints are: \"token\", \"ridetypes\", \"cost\" and \"eta\".");
            }
        }

        private enum LyftServiceEndpoint
        {
            Token,
            Ridetypes,
            Cost,
            Eta
        }

        private static void SetStatusCodeForUberServiceCalls(UberServiceEndpoint endpoint, HttpStatusCode statusCode)
        {
            switch (endpoint)
            {
                case UberServiceEndpoint.Products:
                    TestMediatorForUberGateway.HttpStatusCodeForUberRideTypes = statusCode;
                    TestMediatorForUberGateway.HttpStatusCodeForUberRideCostEstimate = HttpStatusCode.OK;
                    TestMediatorForUberGateway.HttpStatusCodeForUberRideEtaEstimate = HttpStatusCode.OK;
                    break;
                case UberServiceEndpoint.Price:
                    TestMediatorForUberGateway.HttpStatusCodeForUberRideTypes = HttpStatusCode.OK;
                    TestMediatorForUberGateway.HttpStatusCodeForUberRideCostEstimate = statusCode;
                    TestMediatorForUberGateway.HttpStatusCodeForUberRideEtaEstimate = HttpStatusCode.OK;
                    break;
                case UberServiceEndpoint.Time:
                    TestMediatorForUberGateway.HttpStatusCodeForUberRideTypes = HttpStatusCode.OK;
                    TestMediatorForUberGateway.HttpStatusCodeForUberRideCostEstimate = HttpStatusCode.OK;
                    TestMediatorForUberGateway.HttpStatusCodeForUberRideEtaEstimate = statusCode;
                    break;
                default:
                    throw new Exception("Valid Uber endpoints are: \"products\", \"price\" and \"time\".");
            }
        }

        private enum UberServiceEndpoint
        {
            Products,
            Price,
            Time
        }
    }
}
