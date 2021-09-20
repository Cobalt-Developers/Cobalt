using System;

namespace Cobalt.Api.Exceptions
{
    public class StorageInitException : Exception
    {
        public StorageInitException()
        {
        }

        public StorageInitException(string message) : base(message)
        {
        }

        public StorageInitException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}