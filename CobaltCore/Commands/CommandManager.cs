using TShockAPI;

namespace CobaltCore.Commands
{
    public abstract class CommandManager
    {
        protected CobaltPlugin Plugin { get; }

        protected CommandManager(CobaltPlugin plugin)
        {
            Plugin = plugin;
        }

        public abstract void OnCommand(CommandArgs args);

        public abstract string[] GetBaseCommands();

        public abstract AbstractCommand[] GetCommands();
    }
}