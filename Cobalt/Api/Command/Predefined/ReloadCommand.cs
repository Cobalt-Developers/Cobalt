using System;
using System.Collections.Generic;
using Cobalt.Api.Attribute;
using Cobalt.Api.Model;

namespace Cobalt.Api.Command.Predefined
{
    [Description("Reloads parts of the plugin")]
    [SubCommand("reload", "rl")]
    public class ReloadCommand : AbstractCommand
    {
        public ReloadCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(IChatSender sender, List<string> args)
        {
            try
            {
                Plugin.ServiceManager.Reload();
                sender.SendMessage("Plugin was successfully reloaded!");
            }
            catch (System.Exception e)
            {
                sender.SendErrorMessage("Plugin reload failed! View console for more details.");
                Console.WriteLine(e.ToString());
            }
        }
    }
}