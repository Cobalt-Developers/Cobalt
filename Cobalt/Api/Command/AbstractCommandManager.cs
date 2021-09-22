using System.Collections.Generic;
using Cobalt.Api.Wrapper;

namespace Cobalt.Api.Command
{
    public abstract class AbstractCommandManager
    {
        protected ICobaltPlugin Plugin { get; }

        protected AbstractCommandManager(ICobaltPlugin plugin)
        {
            Plugin = plugin;
        }

        public abstract void OnCommand(CobaltPlayer player, List<string> args);

        public abstract string[] GetBaseCommands();

        public abstract AbstractCommand[] GetCommands();
    }
}