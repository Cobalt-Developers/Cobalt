using System.Collections.Generic;
using Cobalt.Api.Attribute;
using Cobalt.Api.Model;

namespace Cobalt.Api.Command.Predefined
{
    [Description("Displays info about the plugin")]
    [SubCommand("version", "v")]
    public class VersionCommand: AbstractCommand
    {
        public VersionCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(IChatSender sender, List<string> args)
        {
            sender.SendMessage($"You are running {Plugin.Name} version {Plugin.Version} by {Plugin.Author}.");
            sender.SendMessage($"{Plugin.Description}");
        }
    }
}