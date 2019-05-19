using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using RideCompareService.DomainLayer.Enums;
using RideCompareService.DomainLayer.Models;
using RideCompareService.DomainLayer.ServiceLocator;
using RideCompareService.DomainLayer.ConfigurationProvider;
using RideCompareService.DomainLayer.Managers.Validators;
using RideCompareService.DomainLayer.Managers.Services.Lyft;
using RideCompareService.DomainLayer.Managers.Services.Uber;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService.DomainLayer.Managers
{
    internal sealed class RideCompareManager : RideCompareManagerBase, IDisposable
    {
        private bool _disposed;
        private readonly ServiceLocatorBase _serviceLocator;

        private ConfigurationProviderBase _configurationProvider;
        private ConfigurationProviderBase ConfigurationProvider => _configurationProvider ?? (_configurationProvider = _serviceLocator.CreateConfigurationProvider());

        private LyftGatewayBase _lyftGateway;
        private LyftGatewayBase LyftGateway => _lyftGateway ?? (_lyftGateway = _serviceLocator.CreateLyftGateway(ConfigurationProvider.LyftBaseUrl, ConfigurationProvider.LyftClientCredentials));

        private UberGatewayBase _uberGateway;
        private UberGatewayBase UberGateway => _uberGateway ?? (_uberGateway = _serviceLocator.CreateUberGateway(ConfigurationProvider.UberBaseUrl, ConfigurationProvider.UberServerToken));

        public RideCompareManager(ServiceLocatorBase serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        protected override async Task<RideCompareResponse> GetBestRideCore(RideCompareRequest rideCompareRequest)
        {
            var lyftRides = await LyftGateway.GetRides(rideCompareRequest);
            var uberRides = await UberGateway.GetRides(rideCompareRequest);

            RideCompareResponseValidator.Validate(lyftRides, uberRides);

            return GetBestRide(lyftRides, uberRides);
        }

        private RideCompareResponse GetBestRide(List<RideCompareResponse> lyftRides, List<RideCompareResponse> uberRides)
        {
            var (lowestLyftRideCost, shortestLyftRideEta) = GetLowestLyftEstimates(lyftRides);
            var (lowestUberRideCost, shortestUberRideEta) = GetLowestUberEstimates(uberRides);

            EnsureEstimatesAreNotEqual(lowestLyftRideCost, lowestUberRideCost, shortestLyftRideEta, shortestUberRideEta);

            if (lowestLyftRideCost <= lowestUberRideCost &&
                shortestLyftRideEta <= shortestUberRideEta)
            {
                return GetRideCompareResponse(RideShareProvider.Lyft, lowestLyftRideCost, shortestLyftRideEta);
            }
            
            if (lowestUberRideCost <= lowestLyftRideCost &&
                shortestUberRideEta <= shortestLyftRideEta)
            {
                return GetRideCompareResponse(RideShareProvider.Uber, lowestUberRideCost, shortestUberRideEta);
            }

            var (lowestRideCostProvider, lowestRideCost) = GetProviderWithLowestRideCost(lowestLyftRideCost, lowestUberRideCost);
            var (shortestRideEtaProvider, shortestRideEta) = GetProviderWithShortestRideEta(shortestLyftRideEta, shortestUberRideEta);

            return GetRideCompareResponse(RideShareProvider.None, lowestRideCost, shortestRideEta, lowestRideCostProvider, shortestRideEtaProvider);
        }

        private void EnsureEstimatesAreNotEqual(decimal lowestLyftRideCost, decimal lowestUberRideCost, int shortestLyftRideEta, int shortestUberRideEta)
        {
            if (lowestLyftRideCost == lowestUberRideCost &&
                shortestLyftRideEta == shortestUberRideEta)
            {
                throw new RideCompareNoBestRideFoundException("No Best Ride Found - Lowest estimates are all equal.");
            }
        }

        private (decimal, int) GetLowestLyftEstimates(List<RideCompareResponse> lyftRides)
        {
            var lowestCost = lyftRides.TrueForAll(r => r.RideCost == 0M)
                ? decimal.MaxValue
                : lyftRides.Where(r => r.RideCost > 0).OrderBy(r => r.RideCost).First().RideCost;

            var shortestEta = lyftRides.TrueForAll(r => r.RideEta == 0)
                ? int.MaxValue
                : lyftRides.Where(r => r.RideEta > 0).OrderBy(r => r.RideEta).First().RideEta;

            return (lowestCost, shortestEta);
        }

        private (decimal, int) GetLowestUberEstimates(List<RideCompareResponse> uberRides)
        {
            var lowestCost = uberRides.TrueForAll(r => r.RideCost == 0M)
                ? decimal.MaxValue
                : uberRides.Where(r => r.RideCost > 0).OrderBy(r => r.RideCost).First().RideCost;

            var shortestEta = uberRides.TrueForAll(r => r.RideEta == 0)
                ? int.MaxValue
                : uberRides.Where(r => r.RideEta > 0).OrderBy(r => r.RideEta).First().RideEta;

            return (lowestCost, shortestEta);
        }

        private (string, decimal) GetProviderWithLowestRideCost(decimal lowestLyftRideCost, decimal lowestUberRideCost)
        {
            return lowestLyftRideCost < lowestUberRideCost
                ? (ToString(RideShareProvider.Lyft), lowestLyftRideCost)
                : (ToString(RideShareProvider.Uber), lowestUberRideCost);
        }

        private (string, int) GetProviderWithShortestRideEta(int shortestLyftRideEta, int shortestUberRideEta)
        {
            return shortestLyftRideEta < shortestUberRideEta
                ? (ToString(RideShareProvider.Lyft), shortestLyftRideEta)
                : (ToString(RideShareProvider.Uber), shortestUberRideEta);
        }

        private RideCompareResponse GetRideCompareResponse(RideShareProvider rideShareProvider, decimal lowestRideCost, int shortestRideEta,
                                                            string lowCostProvider = null, string shortestEtaProvider = null)
        {
            if (lowCostProvider == null && shortestEtaProvider == null)
            {
                return new RideCompareResponse(rideShareProvider.ToString(), lowestRideCost, shortestRideEta);
            }
            return new RideCompareResponse(rideShareProvider.ToString(), lowestRideCost, shortestRideEta, lowCostProvider, shortestEtaProvider);
        }

        private static string ToString(RideShareProvider rideShareProvider)
        {
            return rideShareProvider.ToString();
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
                var lyftGateway = _lyftGateway as LyftGateway;
                var uberGateway = _uberGateway as UberGateway;
                _lyftGateway = null;
                _uberGateway = null;
                lyftGateway?.Dispose();
                uberGateway?.Dispose();
            }

            _disposed = true;
        }

        #endregion

    }
}
