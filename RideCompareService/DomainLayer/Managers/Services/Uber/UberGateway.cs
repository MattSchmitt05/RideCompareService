using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RideCompareService.DomainLayer.Managers.Services.ServiceAgent;
using RideCompareService.DomainLayer.Models;
using RideCompareService.DomainLayer.ServiceLocator;
using RideCompareService.DomainLayer.Managers.Services.Uber.Exceptions;
using RideCompareService.DomainLayer.Managers.Services.Uber.Mappers;
using RideCompareService.DomainLayer.Managers.Services.Uber.ResourceModels;
using RideCompareService.DomainLayer.Managers.Services.Uber.Validators;

namespace RideCompareService.DomainLayer.Managers.Services.Uber
{
    internal sealed class UberGateway : UberGatewayBase, IDisposable
    {
        private bool _disposed;
        private readonly string _baseUrl;
        private readonly IServiceAgentFactory _serviceAgentFactory;
        private HttpClient _httpClient;

        private ServiceAgentBase _serviceAgent;
        private ServiceAgentBase ServiceAgent => _serviceAgent ?? (_serviceAgent = _serviceAgentFactory.CreateServiceAgent(_baseUrl));

        public UberGateway(string uberBaseUrl, string uberToken, IServiceAgentFactory serviceAgentFactory)
        {
            _baseUrl = uberBaseUrl;
            _serviceAgentFactory = serviceAgentFactory;
            _httpClient = ServiceAgent.CreateHttpClient();
            _httpClient = ServiceAgent.SetAuthenticationHeader("Token", uberToken);
        }

        protected override async Task<List<RideCompareResponse>> GetRidesCore(RideCompareRequest rideCompareRequest)
        {
            var startLat = rideCompareRequest.StartingLatitude;
            var startLng = rideCompareRequest.StartingLongitude;
            var endLat = rideCompareRequest.DestinationLatitude;
            var endLng = rideCompareRequest.DestinationLongitude;

            var rideTypes = await GetRideTypes(startLat, startLng);
            UberResourceValidator.ValidateRideTypes(rideTypes);

            var rideCostEstimates = await GetRideCostEstimates(startLat, startLng, endLat, endLng);
            UberResourceValidator.ValidateRideCostEstimates(rideCostEstimates);

            var rideEtaEstimates = await GetRideEtaEstimates(startLat, startLng);
            UberResourceValidator.ValidateRideEtaEstimates(rideEtaEstimates);

            return RideCompareUberMapper.MapFromUberResources(rideTypes, rideCostEstimates, rideEtaEstimates);
        }

        private async Task<UberRideTypeResource> GetRideTypes(double startLat, double startLng)
        {
            var httpResponse = await _httpClient.GetAsync(_baseUrl + $"products?latitude={startLat}&longitude={startLng}");
            await EnsureSuccessStatusCode(httpResponse);
            var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UberRideTypeResource>(httpResponseString);
        }

        private async Task<UberRideCostEstimateResource> GetRideCostEstimates(double startLat, double startLng, double endLat, double endLng)
        {
            var httpResponse = await _httpClient.GetAsync(_baseUrl + $"estimates/price?start_latitude={startLat}&start_longitude={startLng}&end_latitude={endLat}&end_longitude={endLng}");
            await EnsureSuccessStatusCode(httpResponse);
            var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UberRideCostEstimateResource>(httpResponseString);
        }

        private async Task<UberRideEtaEstimateResource> GetRideEtaEstimates(double startLat, double startLng)
        {
            var httpResponse = await _httpClient.GetAsync(_baseUrl + $"estimates/time?start_latitude={startLat}&start_longitude={startLng}");
            await EnsureSuccessStatusCode(httpResponse);
            var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UberRideEtaEstimateResource>(httpResponseString);
        }

        private static async Task EnsureSuccessStatusCode(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
                return;

            var httpContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var reasonPhrase = httpResponseMessage.ReasonPhrase;

            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new UberServiceBadRequestException($"Calling the Uber Service, resulted in a Bad Request Error (400) reponse with a Message: {httpContent} and Reason Phrase: {reasonPhrase}.");
                case HttpStatusCode.NotFound:
                    throw new UberServiceNotFoundException($"Calling the Uber Service, resulted in a Not Found Error (404) reponse with a Message: {httpContent} and Reason Phrase: {reasonPhrase}.");
                case HttpStatusCode.InternalServerError:
                    throw new UberServiceInternalServerErrorException($"Calling the Uber Service, resulted in an Internal Server Error (500) reponse with a Message: {httpContent} and Reason Phrase: {reasonPhrase}.");
                default:
                    throw new UberServiceUnhandledErrorCodeException($"Calling the Uber Service, resulted in an Unhandled Error Code of ({(int)httpResponseMessage.StatusCode}) response with a Message: {httpContent} and Reason Phrase: {reasonPhrase}.");
            }
        }

        #region Disposing Methods

        public void Dispose()
        {
            DisposeInternal(true);
            GC.SuppressFinalize(this);
        }

        [ExcludeFromCodeCoverage]
        private void DisposeInternal(bool disposing)
        {
            if (disposing && !_disposed)
            {
                var client = _httpClient;
                _httpClient = null;
                client.Dispose();
            }

            _disposed = true;
        }

        #endregion

    }
}
