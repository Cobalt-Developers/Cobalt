using System;

namespace Cobalt.Api.Exceptions
{
    public class ServiceAlreadyExistsException : Exception
    {
        public ServiceAlreadyExistsException()
        {
        }

        public ServiceAlreadyExistsException(string message) : base(message)
        {
        }

        public ServiceAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}