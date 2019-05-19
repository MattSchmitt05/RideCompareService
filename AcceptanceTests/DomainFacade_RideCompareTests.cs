using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AcceptanceTests.ServiceLocator;
using AcceptanceTests.TestDoubles.Managers.Services.Lyft;
using AcceptanceTests.TestDoubles.Managers.Services.Uber;
using AcceptanceTests.TestMediators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RideCompareService.DomainLayer;
using RideCompareService.DomainLayer.Enums;
using RideCompareService.DomainLayer.Exceptions;
using RideCompareService.DomainLayer.Models;

namespace AcceptanceTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DomainFacadeRideCompareTests
    {
        private static DomainFacade _domainFacadeUnderTest;

        [ClassInitialize]
        public static void InitializeDomainFacadeUnderTest(TestContext testContext)
        {
            var serviceLocatorForTest = new ServiceLocatorForTest();
            serviceLocatorForTest.LyftGatewayFactory = () => new LyftGatewaySpy();
            serviceLocatorForTest.UberGatewayFactory = () => new UberGatewaySpy();
            _domainFacadeUnderTest = new DomainFacade(serviceLocatorForTest);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestMediatorForDomainFacadeRideCompare.Reset();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _domainFacadeUnderTest.Dispose();
        }

        #region Best Ride

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenAllUberEstimatesAreZero_ShouldReturnBestRideResultForLyft()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(nameof(RideShareProvider.Lyft), rideCost: 1000M, rideEta: 60);

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway = new List<RideCompareResponse>()
            {
                expectedBestRide
            };

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse("Uber", rideCost: 0M, rideEta: 0)
            };

            // Act
            var actualBestRide = await _domainFacadeUnderTest.GetBestRide(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide);
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenAllLyftEstimatesAreZero_ShouldReturnBestRideResultForUber()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(nameof(RideShareProvider.Uber), rideCost: 1000M, rideEta: 60);

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Lyft", rideCost: 0M, rideEta: 0)
            };

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway = new List<RideCompareResponse>()
            {
                expectedBestRide
            };

            // Act
            var actualBestRide = await _domainFacadeUnderTest.GetBestRide(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide);
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenLyftHasLowestEstimates_ShouldReturnBestRideResultForLyft()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(nameof(RideShareProvider.Lyft), rideCost: 1000M, rideEta: 60);

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway = new List<RideCompareResponse>()
            {
                expectedBestRide
            };

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Uber", rideCost: 9999M, rideEta: 9999)
            };

            // Act
            var actualBestRide = await _domainFacadeUnderTest.GetBestRide(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide);
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenUberHasLowestEstimates_ShouldReturnBestRideResultForUber()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(nameof(RideShareProvider.Uber), rideCost: 1000M, rideEta: 60);

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Lyft", rideCost: 9999M, rideEta: 9999)
            };

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway = new List<RideCompareResponse>()
            {
                expectedBestRide
            };

            // Act
            var actualBestRide = await _domainFacadeUnderTest.GetBestRide(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide);
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenLyftHasLowestCostAndUberHasShortestEta_ShouldReturnBestRideResult()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(nameof(RideShareProvider.None), rideCost: 1000M, rideEta: 60, lowestRideCostProvider: "Lyft", shortestRideEtaProvider: "Uber");

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Lyft", rideCost: 1000M, rideEta: 9999)
            };

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Uber", rideCost: 9999M, rideEta: 60)
            };

            // Act
            var actualBestRide = await _domainFacadeUnderTest.GetBestRide(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide);
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenUberHasLowestCostAndLyftHasShortestEta_ShouldReturnBestRideResult()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(nameof(RideShareProvider.None), rideCost: 1000M, rideEta: 60, lowestRideCostProvider: "Uber", shortestRideEtaProvider: "Lyft");

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Lyft", rideCost: 9999M, rideEta: 60)
            };

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Uber", rideCost: 1000M, rideEta: 9999)
            };

            // Act
            var actualBestRide = await _domainFacadeUnderTest.GetBestRide(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide);
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenLyftAndUberHaveEqualLowestCostsAndLyftHasShortestEta_ShouldReturnBestRideResultForLyft()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(nameof(RideShareProvider.Lyft), rideCost: 1000M, rideEta: 10);

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway = new List<RideCompareResponse>()
            {
                expectedBestRide
            };

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Uber", rideCost: 1000M, rideEta: 9999)
            };

            // Act
            var actualBestRide = await _domainFacadeUnderTest.GetBestRide(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide);
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenLyftAndUberHaveEqualLowestCostsAndUberHasShortestEta_ShouldReturnBestRideResultForUber()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(nameof(RideShareProvider.Uber), rideCost: 1000M, rideEta: 10);

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Lyft", rideCost: 1000M, rideEta: 60)
            };

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway = new List<RideCompareResponse>()
            {
                expectedBestRide
            };

            // Act
            var actualBestRide = await _domainFacadeUnderTest.GetBestRide(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide);
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenLyftAndUberHaveEqualShortestEtasAndLyftHasLowestCost_ShouldReturnBestRideResultForLyft()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(nameof(RideShareProvider.Lyft), rideCost: 1000M, rideEta: 10);

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway = new List<RideCompareResponse>()
            {
                expectedBestRide
            };

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse("Uber", rideCost: 9999, rideEta: 10)
            };

            // Act
            var actualBestRide = await _domainFacadeUnderTest.GetBestRide(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide);
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenLyftAndUberHaveEqualShortestEtasAndUberHasLowestCost_ShouldReturnBestRideResultForUber()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(nameof(RideShareProvider.Uber), rideCost: 1000M, rideEta: 10);

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Lyft", rideCost: 9999M, rideEta: 10)
            };

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway = new List<RideCompareResponse>()
            {
                expectedBestRide
            };

            // Act
            var actualBestRide = await _domainFacadeUnderTest.GetBestRide(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide);
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public async Task GetBestRide_WhenLyftAndUberHaveEqualLowestCostsAndShortestEtas_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "No Best Ride Found";

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromLyftGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Lyft", rideCost: 10M, rideEta: 10)
            };

            TestMediatorForDomainFacadeRideCompare.RidesToReturnFromUberGateway = new List<RideCompareResponse>()
            {
                new RideCompareResponse(rideShareProvider: "Uber", rideCost: 10M, rideEta: 10)
            };

            try
            {
                // Act
                var actualBestRide = await _domainFacadeUnderTest.GetBestRide(TestMediatorForDomainFacadeRideCompare.ValidRideCompareRequestModelMock);
                Assert.Fail($"Expected a {nameof(RideCompareNoBestRideFoundException)} to be thrown, but none was thrown.");
            }
            catch (RideCompareNoBestRideFoundException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but it was not found.");
            }
        }

        #endregion Best Ride

        private void AssertBestRideResponse(RideCompareResponse expectedBestRide, RideCompareResponse actualBestRide)
        {
            Assert.IsNotNull(actualBestRide, "Expected to get back a Best Ride, but it was found to be Null.");
            Assert.AreEqual(expectedBestRide.RideShareProvider, actualBestRide.RideShareProvider, $"Expected the Ride Share Provider to be {expectedBestRide.RideShareProvider}, but got {actualBestRide.RideShareProvider} instead.");
            Assert.AreEqual(expectedBestRide.RideCost, actualBestRide.RideCost, $"Expected the Ride Cost to be {expectedBestRide.RideCost}, but got {actualBestRide.RideCost} instead.");
            Assert.AreEqual(expectedBestRide.RideEta, actualBestRide.RideEta, $"Expected the Ride Eta to be {expectedBestRide.RideEta}, but got {actualBestRide.RideEta} instead.");

            if (expectedBestRide.LowestRideCostProvider?.Length > 0 && expectedBestRide.ShortestRideEtaProvider?.Length > 0)
            {
                Assert.AreEqual(expectedBestRide.LowestRideCostProvider, actualBestRide.LowestRideCostProvider, $"Expected the Lowest Ride Cost Provider to be {expectedBestRide.LowestRideCostProvider}, but got {actualBestRide.LowestRideCostProvider} instead.");
                Assert.AreEqual(expectedBestRide.ShortestRideEtaProvider, actualBestRide.ShortestRideEtaProvider, $"Expected the Shortest Ride Eta Provider to be {expectedBestRide.LowestRideCostProvider}, but got {actualBestRide.LowestRideCostProvider} instead.");
            }
        }
    }
}
