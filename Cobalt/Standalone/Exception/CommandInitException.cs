namespace Cobalt.Standalone.Exception
{
    public class CommandInitException : System.Exception
    {
        public CommandInitException(string message) : base(message)
        {
        }

        public CommandInitException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}