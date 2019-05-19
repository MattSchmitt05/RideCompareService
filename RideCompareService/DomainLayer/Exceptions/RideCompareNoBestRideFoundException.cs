using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RideCompareService.DomainLayer.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class RideCompareNoBestRideFoundException : RideCompareBusinessBaseException
    {

        public RideCompareNoBestRideFoundException()
        {
        }

        public RideCompareNoBestRideFoundException(string message) : base(message)
        {
        }

        public RideCompareNoBestRideFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RideCompareNoBestRideFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
