using System;
using System.Diagnostics.CodeAnalysis;
using RideCompareService.DomainLayer.Managers;
using RideCompareService.DomainLayer.ServiceLocator;

namespace RideCompareService.DomainLayer
{
    public sealed partial class DomainFacade : IDisposable
    {
        private bool _disposed;
        private readonly ServiceLocatorBase _serviceLocator;

        private RideCompareManagerBase _rideCompareManager;
        private RideCompareManagerBase RideCompareManager => _rideCompareManager ?? (_rideCompareManager = _serviceLocator.CreateRideCompareManager());

        [ExcludeFromCodeCoverage]
        public DomainFacade()
            : this(new ServiceLocator.ServiceLocator())
        {

        }

        internal DomainFacade(ServiceLocatorBase serviceLocator)
        {
            _serviceLocator = serviceLocator;
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
                var rideCompareManager = _rideCompareManager as RideCompareManager;
                _rideCompareManager = null;
                rideCompareManager?.Dispose();
            }

            _disposed = true;
        }

        #endregion

    }
}
