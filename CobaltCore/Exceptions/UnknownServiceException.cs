using System;

namespace CobaltCore.Exceptions
{
    public class UnknownServiceException : Exception
    {
        public UnknownServiceException()
        {
        }

        public UnknownServiceException(string message) : base(message)
        {
        }

        public UnknownServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}