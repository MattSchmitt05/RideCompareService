using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RideCompareService.DomainLayer.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class RideCompareInvalidDestinationLocationException : RideCompareBusinessBaseException
    {
        public RideCompareInvalidDestinationLocationException()
        {
        }

        public RideCompareInvalidDestinationLocationException(string message) : base(message)
        {
        }

        public RideCompareInvalidDestinationLocationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RideCompareInvalidDestinationLocationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
