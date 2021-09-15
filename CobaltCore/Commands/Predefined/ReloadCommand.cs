using System;
using System.Collections.Generic;
using CobaltCore.Attributes;
using CobaltCore.Wrappers;

namespace CobaltCore.Commands.Predefined
{
    [Description("Reloads parts of the plugin")]
    [SubCommand("reload", "rl")]
    public class ReloadCommand : AbstractCommand
    {
        public ReloadCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(ICobaltPlayer player, List<string> args, string message, bool silent)
        {
            try
            {
                Plugin.ServiceManager.Reload();
                player.SendMessage("Plugin was successfully reloaded!");
            }
            catch (Exception e)
            {
                player.SendErrorMessage("Plugin reload failed! View console for more details.");
                Console.WriteLine(e.ToString());
            }
        }
    }
}