using System.Net.Http;

namespace RideCompareService.DomainLayer.Managers.Services.ServiceAgent
{
    internal abstract class ServiceAgentBase
    {
        public HttpClient CreateHttpClient() => CreateHttpClientCore();
        public HttpClient SetAuthenticationHeader(string scheme, string value) => SetAuthenticationHeaderCore(scheme, value);
        public HttpContent SetHttpContent() => SetHttpContentCore();

        protected abstract HttpClient CreateHttpClientCore();
        protected abstract HttpClient SetAuthenticationHeaderCore(string scheme, string value);
        protected abstract HttpContent SetHttpContentCore();
    }
}
