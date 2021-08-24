using System;

namespace CobaltCore.Exceptions
{
    public class ServiceInitException : Exception
    {
        public ServiceInitException()
        {
        }

        public ServiceInitException(string message) : base(message)
        {
        }

        public ServiceInitException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}