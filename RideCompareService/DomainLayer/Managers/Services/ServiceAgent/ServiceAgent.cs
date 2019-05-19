using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using RideCompareService.DomainLayer.ServiceLocator;

namespace RideCompareService.DomainLayer.Managers.Services.ServiceAgent
{
    internal sealed class ServiceAgent : ServiceAgentBase
    {
        private HttpClient _httpClient;
        private readonly Uri _baseAddress;
        private readonly IRideCompareHttpMessageHandlerFactory _httpMessageHandlerFactory;       
        
        public ServiceAgent(string baseUrl, IRideCompareHttpMessageHandlerFactory httpMessageHandlerFactory)
        {
            _baseAddress = new Uri(baseUrl);
            _httpMessageHandlerFactory = httpMessageHandlerFactory;
        }

        protected override HttpClient CreateHttpClientCore()
        {
            var httpClient = new HttpClient(_httpMessageHandlerFactory.CreateRideCompareHttpMessageHandlerFactory());
            httpClient.BaseAddress = _baseAddress;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("RideCompare_HttpClient", "1.0")));
            _httpClient = httpClient;
            return httpClient;
        }

        protected override HttpClient SetAuthenticationHeaderCore(string scheme, string value)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, value);
            return _httpClient;
        }

        protected override HttpContent SetHttpContentCore()
        {
            var accessTokenBody = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "scope", "public" }
            };

            var accessTokenBodyJson = JsonConvert.SerializeObject(accessTokenBody);
            HttpContent httpContent = new StringContent(accessTokenBodyJson, Encoding.UTF8, "application/json");
            return httpContent;
        }
    }
}
