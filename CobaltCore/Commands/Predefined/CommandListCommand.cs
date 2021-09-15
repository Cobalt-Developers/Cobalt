using System.Collections.Generic;
using System.Linq;
using CobaltCore.Attributes;
using CobaltCore.Wrappers;
using Microsoft.Xna.Framework;

namespace CobaltCore.Commands.Predefined
{
    [Description("Lists all associated commands")]
    [SubCommand("commands", "command", "cmd")]
    public class CommandListCommand : AbstractCommand
    {
        public CommandListCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(ICobaltPlayer player, List<string> args, string message, bool silent)
        {
            var color = Color.BlueViolet.packedValue;
            color = ((color & 0x000000FF) << 16) | (color & 0x0000FF00) | ((color & 0x00FF0000) >> 16);
            var pluginNames = Plugin.GetCommandService().CommandManagers.Select(m => $"[c/{color:X}:{m.GetBaseCommands()[0]}]");
            
            player.SendMessage($"Commands associated with {Plugin.Name}:");
            player.SendMessage(string.Join(", ", pluginNames));
        }
    }
}