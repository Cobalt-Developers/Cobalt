namespace Cobalt.Api.Exception
{
    public class UnknownServiceException : System.Exception
    {
        public UnknownServiceException()
        {
        }

        public UnknownServiceException(string message) : base(message)
        {
        }

        public UnknownServiceException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}