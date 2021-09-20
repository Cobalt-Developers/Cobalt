namespace Cobalt.Api.Exception
{
    public class ServiceInitException : System.Exception
    {
        public ServiceInitException()
        {
        }

        public ServiceInitException(string message) : base(message)
        {
        }

        public ServiceInitException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}