using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RideCompareService.Controllers.Validators;
using RideCompareService.DomainLayer.Enums;
using RideCompareService.DomainLayer.Exceptions;
using RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions;
using RideCompareService.DomainLayer.Managers.Services.Lyft.ResourceModels;
using RideCompareService.DomainLayer.Managers.Services.Lyft.Validators;
using RideCompareService.DomainLayer.Managers.Services.Uber.Exceptions;
using RideCompareService.DomainLayer.Managers.Services.Uber.ResourceModels;
using RideCompareService.DomainLayer.Managers.Services.Uber.Validators;
using RideCompareService.DomainLayer.Managers.Validators;
using RideCompareService.DomainLayer.Models;

namespace ClassTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidatorTests
    {

        #region RideCompare Request Validator

        private const double InvalidLocationValue = double.NaN;

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareRequestValidator_Validate_WhenStartingLatitudeIsInvalid_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Request";
            var invalidStartingLocation = new RideCompareLocation(InvalidLocationValue, -1);
            var validDestinationLocation = new RideCompareLocation(2, -2);
            var invalidRideCompareRequest = new RideCompareRequest(invalidStartingLocation, validDestinationLocation);

            try
            {
                // Act
                RideCompareRequestValidator.Validate(invalidRideCompareRequest);
                Assert.Fail($"Expected a {nameof(RideCompareInvalidStartingLocationException)} to be thrown, but none was thrown.");
            }
            catch (RideCompareInvalidStartingLocationException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareRequestValidator_Validate_WhenStartingLongitudeIsInvalid_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Request";
            var invalidStartingLocation = new RideCompareLocation(1, InvalidLocationValue);
            var validDestinationLocation = new RideCompareLocation(2, -2);
            var invalidRideCompareRequest = new RideCompareRequest(invalidStartingLocation, validDestinationLocation);

            try
            {
                // Act
                RideCompareRequestValidator.Validate(invalidRideCompareRequest);
                Assert.Fail($"Expected a {nameof(RideCompareInvalidStartingLocationException)} to be thrown, but none was thrown.");
            }
            catch (RideCompareInvalidStartingLocationException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareRequestValidator_Validate_WhenDestinationLatitudeIsInvalid_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Request";
            var validStartingLocation = new RideCompareLocation(1, -1);
            var invalidDestinationLocation = new RideCompareLocation(InvalidLocationValue, -2);
            var invalidRideCompareRequest = new RideCompareRequest(validStartingLocation, invalidDestinationLocation);

            try
            {
                // Act
                RideCompareRequestValidator.Validate(invalidRideCompareRequest);
                Assert.Fail($"Expected a {nameof(RideCompareInvalidDestinationLocationException)} to be thrown, but none was thrown.");
            }
            catch (RideCompareInvalidDestinationLocationException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareRequestValidator_Validate_WhenDestinationLongitudeIsInvalid_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Request";
            var validStartingLocation = new RideCompareLocation(1, -1);
            var invalidDestinationLocation = new RideCompareLocation(2, InvalidLocationValue);
            var invalidRideCompareRequest = new RideCompareRequest(validStartingLocation, invalidDestinationLocation);

            try
            {
                // Act
                RideCompareRequestValidator.Validate(invalidRideCompareRequest);
                Assert.Fail($"Expected a {nameof(RideCompareInvalidDestinationLocationException)} to be thrown, but none was thrown.");
            }
            catch (RideCompareInvalidDestinationLocationException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareRequestValidator_Validate_WhenStartingLatitudeIsZero_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Request";
            var invalidStartingLocation = new RideCompareLocation(0, -1);
            var validDestinationLocation = new RideCompareLocation(2, -2);
            var invalidRideCompareRequest = new RideCompareRequest(invalidStartingLocation, validDestinationLocation);

            try
            {
                // Act
                RideCompareRequestValidator.Validate(invalidRideCompareRequest);
                Assert.Fail($"Expected a {nameof(RideCompareInvalidStartingLocationException)} to be thrown, but none was thrown.");
            }
            catch (RideCompareInvalidStartingLocationException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareRequestValidator_Validate_WhenStartingLongitudeIsZero_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Request";
            var invalidStartingLocation = new RideCompareLocation(1, 0);
            var validDestinationLocation = new RideCompareLocation(2, -2);
            var invalidRideCompareRequest = new RideCompareRequest(invalidStartingLocation, validDestinationLocation);

            try
            {
                // Act
                RideCompareRequestValidator.Validate(invalidRideCompareRequest);
                Assert.Fail($"Expected a {nameof(RideCompareInvalidStartingLocationException)} to be thrown, but none was thrown.");
            }
            catch (RideCompareInvalidStartingLocationException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareRequestValidator_Validate_WhenDestinationLatitudeIsZero_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Request";
            var validStartingLocation = new RideCompareLocation(1, -1);
            var invalidDestinationLocation = new RideCompareLocation(0, -2);
            var invalidRideCompareRequest = new RideCompareRequest(validStartingLocation, invalidDestinationLocation);

            try
            {
                // Act
                RideCompareRequestValidator.Validate(invalidRideCompareRequest);
                Assert.Fail($"Expected a {nameof(RideCompareInvalidDestinationLocationException)} to be thrown, but none was thrown.");
            }
            catch (RideCompareInvalidDestinationLocationException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareRequestValidator_Validate_WhenDestinationLongitudeIsZero_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Request";
            var validStartingLocation = new RideCompareLocation(1, -1);
            var invalidDestinationLocation = new RideCompareLocation(2, 0);
            var invalidRideCompareRequest = new RideCompareRequest(validStartingLocation, invalidDestinationLocation);

            try
            {
                // Act
                RideCompareRequestValidator.Validate(invalidRideCompareRequest);
                Assert.Fail($"Expected a {nameof(RideCompareInvalidDestinationLocationException)} to be thrown, but none was thrown.");
            }
            catch (RideCompareInvalidDestinationLocationException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareRequestValidator_Validate_WhenStartingLocationValuesAreEqual_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Request";
            var invalidStartingLocation = new RideCompareLocation(1, 1);
            var validDestinationLocation = new RideCompareLocation(2, -2);
            var invalidRideCompareRequest = new RideCompareRequest(invalidStartingLocation, validDestinationLocation);

            try
            {
                // Act
                RideCompareRequestValidator.Validate(invalidRideCompareRequest);
                Assert.Fail($"Expected a {nameof(RideCompareInvalidStartingLocationException)} to be thrown, but none was thrown.");
            }
            catch (RideCompareInvalidStartingLocationException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareRequestValidator_Validate_WhenDestinationLocationValuesAreEqual_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Request";
            var validStartingLocation = new RideCompareLocation(1, -1);
            var invalidDestinationLocation = new RideCompareLocation(2, 2);
            var invalidRideCompareRequest = new RideCompareRequest(validStartingLocation, invalidDestinationLocation);

            try
            {
                // Act
                RideCompareRequestValidator.Validate(invalidRideCompareRequest);
                Assert.Fail($"Expected a {nameof(RideCompareInvalidDestinationLocationException)} to be thrown, but none was thrown.");
            }
            catch (RideCompareInvalidDestinationLocationException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        #endregion


        #region RideCompare Response Validator

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareResponseValidator_Validate_WhenAllRideCostsAndRideEtasAreZero_ShouldThrow()
        {
            // Arrange
            var lyftString = nameof(RideShareProvider.Lyft);
            var uberString = nameof(RideShareProvider.Uber);
            var expectedExceptionMessage = "all ride costs AND etas were 0";
            var invalidRideCompareLyftResponse = new List<RideCompareResponse>
            {
                new RideCompareResponse(rideShareProvider: lyftString, rideCost: 0M, rideEta: 0)
            };
            var invalidRideCompareUberResponse = new List<RideCompareResponse>
            {
                new RideCompareResponse(rideShareProvider: uberString, rideCost: 0M, rideEta: 0)
            };

            try
            {
                // Act
                RideCompareResponseValidator.Validate(invalidRideCompareLyftResponse, invalidRideCompareUberResponse);
                Assert.Fail($"Expected a {nameof(RideCompareNoBestRideFoundException)} to be thrown, but none wsa thrown.");
            }
            catch (RideCompareNoBestRideFoundException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareResponseValidator_Validate_WhenAllRideCostsAreZero_ShouldThrow()
        {
            // Arrange
            var lyftString = nameof(RideShareProvider.Lyft);
            var uberString = nameof(RideShareProvider.Uber);
            var expectedExceptionMessage = "all ride costs were 0";
            var invalidRideCompareLyftResponse = new List<RideCompareResponse>
            {
                new RideCompareResponse(rideShareProvider: lyftString, rideCost: 0M, rideEta: 60)
            };
            var invalidRideCompareUberResponse = new List<RideCompareResponse>
            {
                new RideCompareResponse(rideShareProvider: uberString, rideCost: 0M, rideEta: 60)
            };

            try
            {
                // Act
                RideCompareResponseValidator.Validate(invalidRideCompareLyftResponse, invalidRideCompareUberResponse);
                Assert.Fail($"Expected a {nameof(RideCompareNoBestRideFoundException)} to be thrown, but none wsa thrown.");
            }
            catch (RideCompareNoBestRideFoundException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void RideCompareResponseValidator_Validate_WhenAllRideEtasAreZero_ShouldThrow()
        {
            // Arrange
            var lyftString = nameof(RideShareProvider.Lyft);
            var uberString = nameof(RideShareProvider.Uber);
            var expectedExceptionMessage = "all ride etas were 0";
            var invalidRideCompareLyftResponse = new List<RideCompareResponse>
            {
                new RideCompareResponse(rideShareProvider: lyftString, rideCost: 1000M, rideEta: 0)
            };
            var invalidRideCompareUberResponse = new List<RideCompareResponse>
            {
                new RideCompareResponse(rideShareProvider: uberString, rideCost: 10M, rideEta: 0)
            };

            try
            {
                // Act
                RideCompareResponseValidator.Validate(invalidRideCompareLyftResponse, invalidRideCompareUberResponse);
                Assert.Fail($"Expected a {nameof(RideCompareNoBestRideFoundException)} to be thrown, but none wsa thrown.");
            }
            catch (RideCompareNoBestRideFoundException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        #endregion


        #region Lyft Resource Validator

        [TestMethod]
        [TestCategory("Class Test")]
        public void LyftResourceValidator_ValidateAccessToken_WhenNoAccessTokenResourceIsReturnedFromLyft_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Access Token Resource";

            try
            {
                // Act
                LyftResourceValidator.ValidateAccessToken(null);
                Assert.Fail($"Expected a {nameof(LyftGatewayInvalidAccessTokenException)} to be thrown, but none was thrown.");
            }
            catch (LyftGatewayInvalidAccessTokenException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void LyftResourceValidator_ValidateAccessToken_WhenNoAccessTokenDataIsReturnedFromLyft_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Access Token Resource";
            var invalidAccessTokenResource = new LyftAccessTokenResource();

            try
            {
                // Act
                LyftResourceValidator.ValidateAccessToken(invalidAccessTokenResource);
                Assert.Fail($"Expected a {nameof(LyftGatewayInvalidAccessTokenException)} to be thrown, but none was thrown.");
            }
            catch (LyftGatewayInvalidAccessTokenException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void LyftResourceValidator_ValidateAccessToken_WhenInvalidAccessTokenIsReturnedFromLyft_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Access Token";
            var invalidAccessTokenResource = new LyftAccessTokenResource { AccessToken = "", ExpiresIn = "86400" };

            try
            {
                // Act
                LyftResourceValidator.ValidateAccessToken(invalidAccessTokenResource);
                Assert.Fail($"Expected a {nameof(LyftGatewayInvalidAccessTokenException)} to be thrown, but none was thrown.");
            }
            catch (LyftGatewayInvalidAccessTokenException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void LyftResourceValidator_ValidateAccessToken_WhenInvalidAccessTokenExpirationIsReturnedFromLyft_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Access Token";
            var invalidAccessTokenResource = new LyftAccessTokenResource { AccessToken = "test", ExpiresIn = "" };

            try
            {
                // Act
                LyftResourceValidator.ValidateAccessToken(invalidAccessTokenResource);
                Assert.Fail($"Expected a {nameof(LyftGatewayInvalidAccessTokenException)} to be thrown, but none was thrown.");
            }
            catch (LyftGatewayInvalidAccessTokenException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void LyftResourceValidator_ValidateRideTypes_WhenNoRideTypesResourceIsReturnedFromLyft_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Ride Types Resource";

            try
            {
                // Act
                LyftResourceValidator.ValidateRideTypes(null);
                Assert.Fail($"Expected a {nameof(LyftGatewayInvalidRideTypesException)} to be thrown, but none was thrown.");
            }
            catch (LyftGatewayInvalidRideTypesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void LyftResourceValidator_ValidateRideTypes_WhenNoRideTypesAreReturnedFromLyft_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Ride Types";
            var invalidRideTypeResource = new LyftRideTypeResource();

            try
            {
                // Act
                LyftResourceValidator.ValidateRideTypes(invalidRideTypeResource);
                Assert.Fail($"Expected a {nameof(LyftGatewayInvalidRideTypesException)} to be thrown, but none was thrown.");
            }
            catch (LyftGatewayInvalidRideTypesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void LyftResourceValidator_ValidateRideCostEstimates_WhenNoCostEstimatesResourceIsReturnedFromLyft_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Cost Estimates Resource";

            try
            {
                // Act
                LyftResourceValidator.ValidateRideCostEstimates(null);
                Assert.Fail($"Expected a {nameof(LyftGatewayInvalidRideCostEstimatesException)} to be thrown, but none was thrown.");
            }
            catch (LyftGatewayInvalidRideCostEstimatesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void LyftResourceValidator_ValidateRideCostEstimates_WhenNoCostEstimatesAreReturnedFromLyft_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Cost Estimates";
            var invalidCostEstimateResource = new LyftRideCostEstimateResource();

            try
            {
                // Act
                LyftResourceValidator.ValidateRideCostEstimates(invalidCostEstimateResource);
                Assert.Fail($"Expected a {nameof(LyftGatewayInvalidRideCostEstimatesException)} to be thrown, but none was thrown.");
            }
            catch (LyftGatewayInvalidRideCostEstimatesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void LyftResourceValidator_ValidateRideEtaEstimates_WhenNoEtaEstimatesResourceIsReturnedFromLyft_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Eta Estimates Resource";

            try
            {
                // Act
                LyftResourceValidator.ValidateRideEtaEstimates(null);
                Assert.Fail($"Expected a {nameof(LyftGatewayInvalidRideEtaEstimatesException)} to be thrown, but none was thrown.");
            }
            catch (LyftGatewayInvalidRideEtaEstimatesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void LyftResourceValidator_ValidateRideEtaEstimates_WhenNoEtaEstimatesAreReturnedFromLyft_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Eta Estimates";
            var invalidEtaEstimateResource = new LyftRideEtaEstimateResource();

            try
            {
                // Act
                LyftResourceValidator.ValidateRideEtaEstimates(invalidEtaEstimateResource);
                Assert.Fail($"Expected a {nameof(LyftGatewayInvalidRideEtaEstimatesException)} to be thrown, but none was thrown.");
            }
            catch (LyftGatewayInvalidRideEtaEstimatesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        #endregion


        #region Uber Resource Validator

        [TestMethod]
        [TestCategory("Class Test")]
        public void UberResourceValidator_ValidateRideTypes_WhenNoRideTypesResourceIsReturnedFromUber_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Ride Types Resource";

            try
            {
                // Act
                UberResourceValidator.ValidateRideTypes(null);
                Assert.Fail($"Expected a {nameof(UberGatewayInvalidRideTypesException)} to be thrown, but none was thrown.");
            }
            catch (UberGatewayInvalidRideTypesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void UberResourceValidator_ValidateRideTypes_WhenNoRideTypesAreReturnedFromUber_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Ride Types";
            var invalidRideTypeResource = new UberRideTypeResource();

            try
            {
                // Act
                UberResourceValidator.ValidateRideTypes(invalidRideTypeResource);
                Assert.Fail($"Expected a {nameof(UberGatewayInvalidRideTypesException)} to be thrown, but none was thrown.");
            }
            catch (UberGatewayInvalidRideTypesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void UberResourceValidator_ValidateRideCostEstimates_WhenNoCostEstimatesResourceIsReturnedFromUber_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Cost Estimates Resource";

            try
            {
                // Act
                UberResourceValidator.ValidateRideCostEstimates(null);
                Assert.Fail($"Expected a {nameof(UberGatewayInvalidRideCostEstimatesException)} to be thrown, but none was thrown.");
            }
            catch (UberGatewayInvalidRideCostEstimatesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void UberResourceValidator_ValidateRideCostEstimates_WhenNoCostEstimatesAreReturnedFromUber_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Cost Estimates";
            var invalidCostEstimateResource = new UberRideCostEstimateResource();

            try
            {
                // Act
                UberResourceValidator.ValidateRideCostEstimates(invalidCostEstimateResource);
                Assert.Fail($"Expected a {nameof(UberGatewayInvalidRideCostEstimatesException)} to be thrown, but none was thrown.");
            }
            catch (UberGatewayInvalidRideCostEstimatesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void UberResourceValidator_ValidateRideEtaEstimates_WhenNoEtaEstimatesResourceIsReturnedFromUber_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Eta Estimates Resource";

            try
            {
                // Act
                UberResourceValidator.ValidateRideEtaEstimates(null);
                Assert.Fail($"Expected a {nameof(UberGatewayInvalidRideEtaEstimatesException)} to be thrown, but none was thrown.");
            }
            catch (UberGatewayInvalidRideEtaEstimatesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        [TestMethod]
        [TestCategory("Class Test")]
        public void UberResourceValidator_ValidateRideEtaEstimates_WhenNoEtaEstimatesAreReturnedFromUber_ShouldThrow()
        {
            // Arrange
            var expectedExceptionMessage = "Invalid Eta Estimates";
            var invalidEtaEstimateResource = new UberRideEtaEstimateResource();

            try
            {
                // Act
                UberResourceValidator.ValidateRideEtaEstimates(invalidEtaEstimateResource);
                Assert.Fail($"Expected a {nameof(UberGatewayInvalidRideEtaEstimatesException)} to be thrown, but none was thrown.");
            }
            catch (UberGatewayInvalidRideEtaEstimatesException e)
            {
                // Assert
                var actualExceptionMessage = e.Message;
                StringAssert.Contains(actualExceptionMessage, expectedExceptionMessage, $"Expected to find \"{expectedExceptionMessage}\" in our exception message, but found \"{actualExceptionMessage}\" instead.");
            }
        }

        #endregion
    }
}
