namespace Cobalt.Api.Exception
{
    public class ServiceAlreadyExistsException : System.Exception
    {
        public ServiceAlreadyExistsException()
        {
        }

        public ServiceAlreadyExistsException(string message) : base(message)
        {
        }

        public ServiceAlreadyExistsException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}