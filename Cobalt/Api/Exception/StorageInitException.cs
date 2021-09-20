namespace Cobalt.Api.Exception
{
    public class StorageInitException : System.Exception
    {
        public StorageInitException()
        {
        }

        public StorageInitException(string message) : base(message)
        {
        }

        public StorageInitException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}