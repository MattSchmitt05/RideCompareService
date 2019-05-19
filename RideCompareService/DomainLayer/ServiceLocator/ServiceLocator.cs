using System.Net.Http;
using RideCompareService.DomainLayer.ConfigurationProvider;
using RideCompareService.DomainLayer.Managers;
using RideCompareService.DomainLayer.Managers.Services.Lyft;
using RideCompareService.DomainLayer.Managers.Services.ServiceAgent;
using RideCompareService.DomainLayer.Managers.Services.Uber;

namespace RideCompareService.DomainLayer.ServiceLocator
{
    internal sealed class ServiceLocator : ServiceLocatorBase
    {
        protected override ConfigurationProviderBase CreateConfigurationProviderCore() => new ConfigurationProvider.ConfigurationProvider();

        protected override RideCompareManagerBase CreateRideCompareManagerCore() => new RideCompareManager(this);

        protected override LyftGatewayBase CreateLyftGatewayCore(string lyftBaseUrl, string lyftClientCredentials) => new LyftGateway(lyftBaseUrl, lyftClientCredentials, this);

        protected override UberGatewayBase CreateUberGatewayCore(string uberBaseUrl, string uberToken) => new UberGateway(uberBaseUrl, uberToken, this);

        protected override HttpMessageHandler CreateRideCompareHttpMessageHandlerFactoryCore() => new HttpClientHandler();

        protected override ServiceAgentBase CreateServiceAgentCore(string baseUrl) => new ServiceAgent(baseUrl, this);
    }
}
