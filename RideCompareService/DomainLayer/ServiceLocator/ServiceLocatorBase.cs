using System.Net.Http;
using RideCompareService.DomainLayer.ConfigurationProvider;
using RideCompareService.DomainLayer.Managers;
using RideCompareService.DomainLayer.Managers.Services.Lyft;
using RideCompareService.DomainLayer.Managers.Services.ServiceAgent;
using RideCompareService.DomainLayer.Managers.Services.Uber;

namespace RideCompareService.DomainLayer.ServiceLocator
{
    internal abstract class ServiceLocatorBase : IServiceAgentFactory, IRideCompareHttpMessageHandlerFactory
    {
        public ConfigurationProviderBase CreateConfigurationProvider() => CreateConfigurationProviderCore();
        public RideCompareManagerBase CreateRideCompareManager() => CreateRideCompareManagerCore();
        public LyftGatewayBase CreateLyftGateway(string lyftBaseUrl, string lyftClientCredentials) => CreateLyftGatewayCore(lyftBaseUrl, lyftClientCredentials);
        public UberGatewayBase CreateUberGateway(string uberBaseUrl, string uberToken) => CreateUberGatewayCore(uberBaseUrl, uberToken);
        public HttpMessageHandler CreateRideCompareHttpMessageHandlerFactory() => CreateRideCompareHttpMessageHandlerFactoryCore();
        public ServiceAgentBase CreateServiceAgent(string baseUrl) => CreateServiceAgentCore(baseUrl);

        protected abstract ConfigurationProviderBase CreateConfigurationProviderCore();
        protected abstract RideCompareManagerBase CreateRideCompareManagerCore();
        protected abstract LyftGatewayBase CreateLyftGatewayCore(string lyftBaseUrl, string lyftClientCredentials);
        protected abstract UberGatewayBase CreateUberGatewayCore(string uberBaseUrl, string uberToken);
        protected abstract HttpMessageHandler CreateRideCompareHttpMessageHandlerFactoryCore();
        protected abstract ServiceAgentBase CreateServiceAgentCore(string baseUrl);
    }

    internal interface IServiceAgentFactory
    {
        ServiceAgentBase CreateServiceAgent(string baseUrl);
    }

    internal interface IRideCompareHttpMessageHandlerFactory
    {
        HttpMessageHandler CreateRideCompareHttpMessageHandlerFactory();
    }
}
