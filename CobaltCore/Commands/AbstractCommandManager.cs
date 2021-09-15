using System.Collections.Generic;
using CobaltCore.Wrappers;

namespace CobaltCore.Commands
{
    public abstract class AbstractCommandManager
    {
        protected ICobaltPlugin Plugin { get; }

        protected AbstractCommandManager(ICobaltPlugin plugin)
        {
            Plugin = plugin;
        }

        public abstract void OnCommand(ICobaltPlayer player, List<string> args, string message, bool silent);

        public abstract string[] GetBaseCommands();

        public abstract AbstractCommand[] GetCommands();
    }
}