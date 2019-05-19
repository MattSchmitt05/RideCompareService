using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RideCompareService.DomainLayer.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class RideCompareTechnicalBaseException : RideCompareBaseException
    {
        public RideCompareTechnicalBaseException()
        {
        }

        public RideCompareTechnicalBaseException(string message) : base(message)
        {
        }

        public RideCompareTechnicalBaseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RideCompareTechnicalBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
