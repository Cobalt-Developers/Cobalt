using TShockAPI;

namespace CobaltCore.Commands
{
    public abstract class CommandManager
    {
        private CobaltPlugin Plugin { get; }

        protected CommandManager(CobaltPlugin plugin)
        {
            Plugin = plugin;
        }

        public abstract void OnCommand(CommandArgs args);

        public abstract string[] GetCommands();
    }
}