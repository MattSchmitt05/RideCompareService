using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RideCompareService.DomainLayer.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class RideCompareInvalidStartingLocationException : RideCompareBusinessBaseException
    {
        public RideCompareInvalidStartingLocationException()
        {
        }

        public RideCompareInvalidStartingLocationException(string message) : base(message)
        {
        }

        public RideCompareInvalidStartingLocationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RideCompareInvalidStartingLocationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
