namespace RideCompareService.DomainLayer.ConfigurationProvider
{
    internal abstract class ConfigurationProviderBase
    {
        public string LyftBaseUrl => GetLyftBaseUrlCore();
        protected abstract string GetLyftBaseUrlCore();

        public string LyftClientCredentials => GetLyftClientCredentialsCore();
        protected abstract string GetLyftClientCredentialsCore();

        public string UberBaseUrl => GetUberBaseUrlCore();
        protected abstract string GetUberBaseUrlCore();

        public string UberServerToken => GetUberServerTokenCore();
        protected abstract string GetUberServerTokenCore();
    }
}
