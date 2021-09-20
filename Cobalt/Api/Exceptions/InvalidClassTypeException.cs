using System;

namespace Cobalt.Api.Exceptions
{
    public class InvalidClassTypeException : Exception
    {
        public InvalidClassTypeException(Type type, Type target) : base($"Invalid Type: Found {type.Name}, but instance of {target.Name} is needed")
        {
        }
    }
}