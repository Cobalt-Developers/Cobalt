using System;

namespace Cobalt.Api.Exception
{
    public class InvalidClassTypeException : System.Exception
    {
        public InvalidClassTypeException(Type type, Type target) : base($"Invalid Type: Found {type.Name}, but instance of {target.Name} is needed")
        {
        }
    }
}