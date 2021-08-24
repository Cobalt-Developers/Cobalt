namespace CobaltCore.Commands
{
    public class ArgumentI
    {
        private CobaltPlugin Plugin { get; }

        public string Placeholder { get; }
        public bool Optional { get; } 

        public ArgumentI(CobaltPlugin plugin, string placeholder, bool optional)
        {
            Plugin = plugin;
            Placeholder = placeholder;
            Optional = optional;
        }

        public string ToPrettyString()
        {
            return Optional ? $"[{Placeholder}]" : $"<{Placeholder}>";
        }
    }
}