namespace Cobalt.Loader.Exception
{
    public class PluginInitException : System.Exception
    {
        public PluginInitException(string message) : base(message)
        {
        }

        public PluginInitException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}