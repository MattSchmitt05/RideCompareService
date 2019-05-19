using Microsoft.VisualStudio.TestTools.UnitTesting;
using RideCompareService.DomainLayer.Enums;
using RideCompareService.DomainLayer.Managers.Services.Lyft.Mappers;
using RideCompareService.DomainLayer.Managers.Services.Lyft.ResourceModels;
using RideCompareService.DomainLayer.Managers.Services.Uber.Mappers;
using RideCompareService.DomainLayer.Managers.Services.Uber.ResourceModels;
using RideCompareService.DomainLayer.Models;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ClassTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MapperTests
    {
        private const string Lyft = nameof(RideShareProvider.Lyft);
        private const string Uber = nameof(RideShareProvider.Uber);

        #region Lyft Resource Mappers

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareLyftMapper_MapFromLyftResources_WhenNoLyftRideTypesFound_ShouldReturnNoCostAndEtaEstimates()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Lyft, rideCost: 0M, rideEta: 0);
            var noRideTypes = new LyftRideTypeResource
            {
                LyftRides = new LyftRideTypeResource.LyftRide[] { }
            };
            var validRideCost = SetLyftRideCostEstimateResource("1");
            var validRideEta = SetLyftRideEtaEstimateResource("1");

            // Act
            var actualBestRide = RideCompareLyftMapper.MapFromLyftResources(noRideTypes, validRideCost, validRideEta);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareLyftMapper_MapFromLyftResources_WhenNoLyftCostEstimatesFound_ShouldMapCostEstimateValuesTo0()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Lyft, rideCost: 0M, rideEta: 1);
            var validRideType = SetLyftRideTypeResource();
            var noRideCosts = new LyftRideCostEstimateResource
            {
                CostEstimates = new LyftRideCostEstimateResource.CostEstimate[] { }
            };      
            var validRideEta = SetLyftRideEtaEstimateResource("1");

            // Act
            var actualBestRide = RideCompareLyftMapper.MapFromLyftResources(validRideType, noRideCosts, validRideEta);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareLyftMapper_MapFromLyftResources_WhenNoLyftEtaEstimatesFound_ShouldMapEtaEstimateValuesTo0()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Lyft, rideCost: 1M, rideEta: 0);
            var validRideType = SetLyftRideTypeResource();
            var validRideCost = SetLyftRideCostEstimateResource("1");
            var noRideEtas = new LyftRideEtaEstimateResource
            {
                EtaEstimates = new LyftRideEtaEstimateResource.EtaEstimate[] { }
            };

            // Act
            var actualBestRide = RideCompareLyftMapper.MapFromLyftResources(validRideType, validRideCost, noRideEtas);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareLyftMapper_MapFromLyftResources_WhenNoLyftEstimatesFound_ShouldMapEstimateValuesTo0()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Lyft, rideCost: 0M, rideEta: 0);
            var validRideType = SetLyftRideTypeResource();
            var noRideCosts = new LyftRideCostEstimateResource
            {
                CostEstimates = new LyftRideCostEstimateResource.CostEstimate[] { }
            };
            var noRideEtas = new LyftRideEtaEstimateResource
            {
                EtaEstimates = new LyftRideEtaEstimateResource.EtaEstimate[] { }
            };

            // Act
            var actualBestRide = RideCompareLyftMapper.MapFromLyftResources(validRideType, noRideCosts, noRideEtas);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        [TestMethod]
        public void RideCompareLyftMapper_MapFromLyftResources_WhenAllEstimatesAre0_ShouldMapEstimateValuesTo0()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Lyft, rideCost: 0M, rideEta: 0);
            var validRideType = SetLyftRideTypeResource();
            var zeroRideCostValue = SetLyftRideCostEstimateResource("0");
            var zeroRideEtaValue = SetLyftRideEtaEstimateResource("0");

            // Act
            var actualBestRide = RideCompareLyftMapper.MapFromLyftResources(validRideType, zeroRideCostValue, zeroRideEtaValue);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        [TestMethod]
        public void RideCompareLyftMapper_MapFromLyftResources_WhenMappingToNonParsableValues_ShouldMapValuesTo0()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Lyft, rideCost: 0M, rideEta: 0);
            var validRideType = SetLyftRideTypeResource();
            var zeroRideCostValue = SetLyftRideCostEstimateResource("test");
            var zeroRideEtaValue = SetLyftRideEtaEstimateResource("test");

            // Act
            var actualBestRide = RideCompareLyftMapper.MapFromLyftResources(validRideType, zeroRideCostValue, zeroRideEtaValue);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        #endregion Lyft Resource Mappers


        #region Uber Resource Mappers

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareUberMapper_MapFromUberResources_WhenNoUberRideTypesFound_ShouldReturnNoCostAndEtaEstimates()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Uber, rideCost: 0M, rideEta: 0);
            var noRideTypes = new UberRideTypeResource
            {
                UberRides = new UberRideTypeResource.UberRide[] { }
            };
            var validRideCost = SetUberRideCostEstimateResource("1");
            var validRideEta = SetUberRideEtaEstimateResource("1");

            // Act
            var actualBestRide = RideCompareUberMapper.MapFromUberResources(noRideTypes, validRideCost, validRideEta);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareUberMapper_MapFromUberResources_WhenNoUberCostEstimatesFound_ShouldMapCostEstimateValuesTo0()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Uber, rideCost: 0M, rideEta: 1);
            var validRideType = SetUberRideTypeResource();
            var noRideCosts = new UberRideCostEstimateResource
            {
                Prices = new UberRideCostEstimateResource.Price[] { }
            };
            var validRideEta = SetUberRideEtaEstimateResource("1");

            // Act
            var actualBestRide = RideCompareUberMapper.MapFromUberResources(validRideType, noRideCosts, validRideEta);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareUberMapper_MapFromUberResources_WhenNoUberEtaEstimatesFound_ShouldMapEtaEstimateValuesTo0()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Uber, rideCost: 100M, rideEta: 0);
            var validRideType = SetUberRideTypeResource();
            var validRideCost = SetUberRideCostEstimateResource("1");
            var noRideEtas = new UberRideEtaEstimateResource
            {
                Times = new UberRideEtaEstimateResource.Time[] { }
            };

            // Act
            var actualBestRide = RideCompareUberMapper.MapFromUberResources(validRideType, validRideCost, noRideEtas);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareUberMapper_MapFromUberResources_WhenNoUberEstimatesFound_ShouldMapEstimateValuesTo0()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Uber, rideCost: 0M, rideEta: 0);
            var validRideType = SetUberRideTypeResource();
            var noRideCosts = new UberRideCostEstimateResource
            {
                Prices = new UberRideCostEstimateResource.Price[] { }
            };
            var noRideEtas = new UberRideEtaEstimateResource
            {
                Times = new UberRideEtaEstimateResource.Time[] { }
            };

            // Act
            var actualBestRide = RideCompareUberMapper.MapFromUberResources(validRideType, noRideCosts, noRideEtas);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        [TestMethod]
        public void RideCompareUberMapper_MapFromUberResources_WhenAllEstimatesAre0_ShouldMapEstimateValuesTo0()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Uber, rideCost: 0M, rideEta: 0);
            var validRideType = SetUberRideTypeResource();
            var zeroRideCostValue = SetUberRideCostEstimateResource("0");
            var zeroRideEtaValue = SetUberRideEtaEstimateResource("0");

            // Act
            var actualBestRide = RideCompareUberMapper.MapFromUberResources(validRideType, zeroRideCostValue, zeroRideEtaValue);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        [TestMethod]
        public void RideCompareUberMapper_MapFromUberResources_WhenMappingToNonParsableValues_ShouldMapValuesTo0()
        {
            // Arrange
            var expectedBestRide = new RideCompareResponse(Uber, rideCost: 0M, rideEta: 0);
            var validRideType = SetUberRideTypeResource();
            var zeroRideCostValue = SetUberRideCostEstimateResource("test");
            var zeroRideEtaValue = SetUberRideEtaEstimateResource("test");

            // Act
            var actualBestRide = RideCompareUberMapper.MapFromUberResources(validRideType, zeroRideCostValue, zeroRideEtaValue);

            // Assert
            AssertBestRideResponse(expectedBestRide, actualBestRide.First());
        }

        #endregion Uber Resource Mappers
        

        #region Set Lyft Resources

        private LyftRideTypeResource SetLyftRideTypeResource()
        {
            return new LyftRideTypeResource
            {
                LyftRides = new[]
                {
                    new LyftRideTypeResource.LyftRide
                    {
                        RideType = "test"
                    }
                }
            };
        }

        private LyftRideCostEstimateResource SetLyftRideCostEstimateResource(string value)
        {
            return new LyftRideCostEstimateResource
            {
                CostEstimates = new[]
                {
                    new LyftRideCostEstimateResource.CostEstimate
                    {
                        RideType = "test",
                        EstimatedCostCentsMax = value
                    }
                }
            };
        }

        private LyftRideEtaEstimateResource SetLyftRideEtaEstimateResource(string value)
        {
            return new LyftRideEtaEstimateResource
            {
                EtaEstimates = new[]
                {
                    new LyftRideEtaEstimateResource.EtaEstimate
                    {
                        RideType = "test",
                        EtaSeconds = value
                    }
                }
            };
        }

        #endregion Set Lyft Resources


        #region Set Uber Resources

        private UberRideTypeResource SetUberRideTypeResource()
        {
            return new UberRideTypeResource
            {
                UberRides = new[]
                {
                    new UberRideTypeResource.UberRide
                    {
                        DisplayName = "test"
                    }
                }
            };
        }

        private UberRideCostEstimateResource SetUberRideCostEstimateResource(string value)
        {
            return new UberRideCostEstimateResource
            {
                Prices = new[]
                {
                    new UberRideCostEstimateResource.Price
                    {
                        DisplayName = "test",
                        HighEstimate = value
                    }
                }
            };
        }

        private UberRideEtaEstimateResource SetUberRideEtaEstimateResource(string value)
        {
            return new UberRideEtaEstimateResource
            {
                Times = new[]
                {
                    new UberRideEtaEstimateResource.Time
                    {
                        DisplayName = "test",
                        Estimate = value
                    }
                }
            };
        }

        #endregion Set Uber Resources

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
