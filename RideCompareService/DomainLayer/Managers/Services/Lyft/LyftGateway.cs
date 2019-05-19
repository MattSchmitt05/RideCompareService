using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RideCompareService.DomainLayer.Managers.Services.Lyft.Exceptions;
using RideCompareService.DomainLayer.Models;
using RideCompareService.DomainLayer.ServiceLocator;
using RideCompareService.DomainLayer.Managers.Services.Lyft.Mappers;
using RideCompareService.DomainLayer.Managers.Services.Lyft.ResourceModels;
using RideCompareService.DomainLayer.Managers.Services.Lyft.Validators;
using RideCompareService.DomainLayer.Managers.Services.ServiceAgent;

namespace RideCompareService.DomainLayer.Managers.Services.Lyft
{
    internal sealed class LyftGateway : LyftGatewayBase, IDisposable
    {
        private bool _disposed;
        private readonly string _baseUrl;
        private readonly string _clientCredentials;
        private readonly IServiceAgentFactory _serviceAgentFactory;
        private HttpClient _httpClient;

        private ServiceAgentBase _serviceAgent;
        private ServiceAgentBase ServiceAgent => _serviceAgent ?? (_serviceAgent = _serviceAgentFactory.CreateServiceAgent(_baseUrl));

        public LyftGateway(string lyftBaseUrl, string lyftClientCredentials, IServiceAgentFactory serviceAgentFactory)
        {
            _baseUrl = lyftBaseUrl;
            _clientCredentials = lyftClientCredentials;
            _serviceAgentFactory = serviceAgentFactory;
            _httpClient = ServiceAgent.CreateHttpClient();
        }

        protected override async Task<List<RideCompareResponse>> GetRidesCore(RideCompareRequest rideCompareRequest)
        {
            var accessTokenDetails = await GetAccesToken();
            LyftResourceValidator.ValidateAccessToken(accessTokenDetails);
            var accessToken = accessTokenDetails.AccessToken;

            _httpClient = ServiceAgent.SetAuthenticationHeader("Bearer", accessToken);

            var startLat = rideCompareRequest.StartingLatitude;
            var startLng = rideCompareRequest.StartingLongitude;
            var endLat = rideCompareRequest.DestinationLatitude;
            var endLng = rideCompareRequest.DestinationLongitude;

            var rideTypes = await GetRideTypes(startLat, startLng);
            LyftResourceValidator.ValidateRideTypes(rideTypes);

            var rideCostEstimates = await GetRideCostEstimates(startLat, startLng, endLat, endLng);
            LyftResourceValidator.ValidateRideCostEstimates(rideCostEstimates);

            var rideEtaEstimates = await GetRideEtaEstimates(startLat, startLng);
            LyftResourceValidator.ValidateRideEtaEstimates(rideEtaEstimates);

            return RideCompareLyftMapper.MapFromLyftResources(rideTypes, rideCostEstimates, rideEtaEstimates);
        }

        private async Task<LyftAccessTokenResource> GetAccesToken()
        {
            _httpClient = ServiceAgent.SetAuthenticationHeader("Basic", _clientCredentials);
            var httpContent = ServiceAgent.SetHttpContent();
            var httpResponse = await _httpClient.PostAsync(_baseUrl + "oauth/token", httpContent);
            await EnsureSuccessStatusCode(httpResponse);
            var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LyftAccessTokenResource>(httpResponseString);
        }

        private async Task<LyftRideTypeResource> GetRideTypes(double startLat, double startLng)
        {
            var httpResponse = await _httpClient.GetAsync(_baseUrl + $"v1/ridetypes?lat={startLat}&lng={startLng}");
            await EnsureSuccessStatusCode(httpResponse);
            var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LyftRideTypeResource>(httpResponseString);
        }

        private async Task<LyftRideCostEstimateResource> GetRideCostEstimates(double startLat, double startLng, double endLat, double endLng)
        {
            var httpResponse = await _httpClient.GetAsync(_baseUrl + $"v1/cost?start_lat={startLat}&start_lng={startLng}&end_lat={endLat}&end_lng={endLng}");
            await EnsureSuccessStatusCode(httpResponse);
            var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LyftRideCostEstimateResource>(httpResponseString);
        }

        private async Task<LyftRideEtaEstimateResource> GetRideEtaEstimates(double startLat, double startLng)
        {
            var httpResponse = await _httpClient.GetAsync(_baseUrl + $"v1/eta?lat={startLat}&lng={startLng}");
            await EnsureSuccessStatusCode(httpResponse);
            var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LyftRideEtaEstimateResource>(httpResponseString);
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
                    throw new LyftServiceBadRequestException($"Calling the Lyft Service, resulted in a Bad Request Error (400) reponse with a Message: {httpContent} and Reason Phrase: {reasonPhrase}.");
                case HttpStatusCode.NotFound:
                    throw new LyftServiceNotFoundException($"Calling the Lyft Service, resulted in a Not Found Error (404) reponse with a Message: {httpContent} and Reason Phrase: {reasonPhrase}.");
                case HttpStatusCode.InternalServerError:
                    throw new LyftServiceInternalServerErrorException($"Calling the Lyft Service, resulted in an Internal Server Error (500) reponse with a Message: {httpContent} and Reason Phrase: {reasonPhrase}.");
                default:
                    throw new LyftServiceUnhandledErrorCodeException($"Calling the Lyft Service, resulted in an Unhandled Error Code of ({(int)httpResponseMessage.StatusCode}) response with a Message: {httpContent} and Reason Phrase: {reasonPhrase}.");
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
