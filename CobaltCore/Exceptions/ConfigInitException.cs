using System;

namespace CobaltCore.Exceptions
{
    public class ConfigInitException : Exception
    {
        public ConfigInitException()
        {
        }

        public ConfigInitException(string message) : base(message)
        {
        }

        public ConfigInitException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}