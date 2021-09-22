using System.Collections.Generic;
using Cobalt.Api.Attribute;
using Cobalt.Api.Wrapper;

namespace Cobalt.Api.Command.Predefined
{
    [Description("Displays info about the plugin")]
    [SubCommand("version", "v")]
    public class VersionCommand: AbstractCommand
    {
        public VersionCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(CobaltPlayer player, List<string> args)
        {
            player.SendMessage($"You are running {Plugin.Name} version {Plugin.Version} by {Plugin.Author}.");
            player.SendMessage($"{Plugin.Description}");
        }
    }
}