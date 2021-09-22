using System.Collections.Generic;
using Cobalt.Api.Model;

namespace Cobalt.Api.Command
{
    public abstract class AbstractCommandManager
    {
        protected ICobaltPlugin Plugin { get; }

        protected AbstractCommandManager(ICobaltPlugin plugin)
        {
            Plugin = plugin;
        }

        public abstract void OnCommand(IChatSender sender, List<string> args);

        public abstract string[] GetBaseCommands();

        public abstract AbstractCommand[] GetCommands();
    }
}