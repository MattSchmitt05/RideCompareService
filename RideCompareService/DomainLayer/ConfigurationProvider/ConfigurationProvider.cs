using Microsoft.Extensions.Configuration;

namespace RideCompareService.DomainLayer.ConfigurationProvider
{
    internal sealed class ConfigurationProvider : ConfigurationProviderBase
    {
        private readonly IConfigurationRoot _configurationRoot;

        public ConfigurationProvider()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            _configurationRoot = configurationBuilder.Build();
        }

        protected override string GetLyftBaseUrlCore() => _configurationRoot["AppSettings:LyftBaseUrl"];

        protected override string GetLyftClientCredentialsCore() => _configurationRoot["AppSettings:LyftClientCredentials"];

        protected override string GetUberBaseUrlCore() => _configurationRoot["AppSettings:UberBaseUrl"];

        protected override string GetUberServerTokenCore() => _configurationRoot["AppSettings:UberServerToken"];
    }
}
