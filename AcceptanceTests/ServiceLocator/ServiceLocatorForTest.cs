using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using AcceptanceTests.TestDoubles.Managers.Services;
using RideCompareService.DomainLayer.ConfigurationProvider;
using RideCompareService.DomainLayer.Managers;
using RideCompareService.DomainLayer.Managers.Services.Lyft;
using RideCompareService.DomainLayer.Managers.Services.ServiceAgent;
using RideCompareService.DomainLayer.Managers.Services.Uber;
using RideCompareService.DomainLayer.ServiceLocator;

namespace AcceptanceTests.ServiceLocator
{
    [ExcludeFromCodeCoverage]
    internal sealed class ServiceLocatorForTest : ServiceLocatorBase
    {
        public Func<LyftGatewayBase> LyftGatewayFactory;
        public Func<UberGatewayBase> UberGatewayFactory;

        protected override ConfigurationProviderBase CreateConfigurationProviderCore() => new ConfigurationProvider();

        protected override RideCompareManagerBase CreateRideCompareManagerCore() => new RideCompareManager(this);

        protected override LyftGatewayBase CreateLyftGatewayCore(string lyftBaseUrl, string lyftClientCredentials)
        {
            if (LyftGatewayFactory == null)
                throw new ArgumentException("In order to use the CreateLyftGateway method in the ServiceLocatorForTest testing class, you MUST assign the LyftGatewayFactory delegate with a valid delegate instance.");
            return LyftGatewayFactory();
        }

        protected override UberGatewayBase CreateUberGatewayCore(string uberBaseUrl, string uberToken)
        {
            if (UberGatewayFactory == null)
                throw new ArgumentException("In order to use the CreateUberGateway method in the ServiceLocatorForTest testing class, you MUST assign the UberGatewayFactory delegate with a valid delegate instance.");
            return UberGatewayFactory();
        }

        protected override HttpMessageHandler CreateRideCompareHttpMessageHandlerFactoryCore() => new HttpMessageHandlerSpy();

        protected override ServiceAgentBase CreateServiceAgentCore(string baseUrl) => new ServiceAgent(baseUrl, this);
    }
}
