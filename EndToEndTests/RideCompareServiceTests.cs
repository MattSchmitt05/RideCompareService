using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EndToEndTests.TestMediators;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RideCompareService.Controllers.ResourceModels;

namespace EndToEndTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RideCompareServiceTests
    {
        private readonly WebApplicationFactory<RideCompareService.Startup> _webApplicationFactory;
        private readonly HttpClient _httpClient;
        private IConfigurationRoot _configurationRoot;
        private const string RideCompareEndpoint = "/api/ridecompare/bestride";

        public RideCompareServiceTests()
        {
            _webApplicationFactory = new WebApplicationFactory<RideCompareService.Startup>();
            _httpClient = _webApplicationFactory.CreateClient();

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configurationRoot = configurationBuilder.Build();
        }

        [TestMethod]
        [TestCategory("End to End Test")]
        public async Task RideCompareService_GetBestRide_WhenValidClientLocationsSent_ShouldGetBestRideResponse()
        {
            // Arrange
            var expectedResultCode = "OK";
            var expectedResultMessage = "Successful service call";
            var validClientRequest = TestMediatorForRideCompareService.ValidClientRequestResourceMock;
            var content = new StringContent(JsonConvert.SerializeObject(validClientRequest), Encoding.UTF8, "application/json");

            // Act
            var httpResponseMessage = await _httpClient.PostAsync(RideCompareEndpoint, content);
            var actualResponse = await httpResponseMessage.Content.ReadAsAsync<ClientResponseResource>();

            // Assert
            await AssertEnsureSuccessStatusCode(httpResponseMessage);
            Assert.IsNotNull(actualResponse.BestRide, "Expected to get back data in \"Best Ride\" of the response, but it was found to be Null.");
            Assert.AreEqual(actualResponse.ResultCode, expectedResultCode, $"Expected to get a Result Code of \"{expectedResultCode}\" in our response, but got {actualResponse.ResultCode} instead.");
            Assert.AreEqual(actualResponse.ResultMessage, expectedResultMessage, $"Expected to get the message of \"{expectedResultMessage}\" in our response, but got {actualResponse.ResultMessage} instead.");
        }

        [TestMethod]
        [TestCategory("End to End Test")]
        public async Task RideCompareService_GetBestRide_WhenCallingAPIWithBusinessException_ShouldReturnBusinessExceptionCodeAndMessage()
        {
            // Arrange
            var expectedStatusCode = 400;
            var expectedResultCode = "ERR-Business";
            var expectedResultMessage = "Invalid Request";
            var invalidClientRequest = TestMediatorForRideCompareService.InvalidClientRequestResourceMock;
            var content = new StringContent(JsonConvert.SerializeObject(invalidClientRequest), Encoding.UTF8, "application/json");

            // Act
            var httpResponseMessage = await _httpClient.PostAsync(RideCompareEndpoint, content);
            var actualStatusCode = (int)httpResponseMessage.StatusCode;
            var actualResponse = await httpResponseMessage.Content.ReadAsAsync<ClientResponseResource>();

            // Assert
            Assert.IsNull(actualResponse.BestRide, $"Expected Best Ride to be Null, but it was found to be Not Null.");
            Assert.AreEqual(expectedStatusCode, actualStatusCode, $"Expected to get a Status Code of {expectedStatusCode}, but got {actualStatusCode} instead.");
            Assert.IsTrue(httpResponseMessage.Headers.Contains("ExceptionType"), $"Expected to find \"ExceptionType\" in the header of the http response.");
            Assert.AreEqual(actualResponse.ResultCode, expectedResultCode, $"Expected to get a Result Code of \"{expectedResultCode}\" in our response, but got {actualResponse.ResultCode} instead.");
            StringAssert.Contains(actualResponse.ResultMessage, expectedResultMessage, $"Expected to find the message of \"{expectedResultMessage}\" in our response, but got {actualResponse.ResultMessage} instead.");
        }

        [TestMethod]
        [TestCategory("End to End Test")]
        public async Task RideCompareService_GetBestRide_WhenCallingAPIWithoutContentHeader_ShouldReturnUnsupportedMediaType()
        {
            // Arrange
            var expectedStatusCode = 415;
            var validClientRequest = TestMediatorForRideCompareService.ValidClientRequestResourceMock;
            var invalidContent = new StringContent(JsonConvert.SerializeObject(validClientRequest), Encoding.UTF8);
            
            // Act
            var httpResponseMessage = await _httpClient.PostAsync(RideCompareEndpoint, invalidContent);
            var actualStatusCode = (int)httpResponseMessage.StatusCode;

            // Assert
            Assert.AreEqual(expectedStatusCode, actualStatusCode, $"Expected the HTTP status code to be {expectedStatusCode}, but got {actualStatusCode} instead.");
        }

        [TestMethod]
        [TestCategory("End to End Test")]
        public async Task RideCompareService_GetBestRide_WhenCallingAPIWithoutRequestBody_ShouldReturnUnsupportedMediaType()
        {
            // Arrange
            var expectedStatusCode = 415;

            // Act
            var httpResponseMessage = await _httpClient.PostAsync(RideCompareEndpoint, null);
            var actualStatusCode = (int)httpResponseMessage.StatusCode;

            // Assert
            Assert.AreEqual(expectedStatusCode, actualStatusCode, $"Expected the HTTP status code to be {expectedStatusCode}, but got {actualStatusCode} instead.");
        }
        
        private async Task AssertEnsureSuccessStatusCode(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
                return;

            var reasonPhrase = httpResponseMessage.ReasonPhrase;
            var message = await httpResponseMessage.Content.ReadAsStringAsync();

            Assert.Fail($"Reason Phrase: {reasonPhrase} | Message: {message}");
        }
    }
}
