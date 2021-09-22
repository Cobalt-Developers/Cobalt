using System.Collections.Generic;
using System.Linq;
using Cobalt.Api.Attribute;
using Cobalt.Api.Model;
using Microsoft.Xna.Framework;

namespace Cobalt.Api.Command.Predefined
{
    [Description("Lists all associated commands")]
    [SubCommand("commands", "command", "cmd")]
    public class CommandListCommand : AbstractCommand
    {
        public CommandListCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(IChatSender sender, List<string> args)
        {
            var color = Color.BlueViolet.packedValue;
            color = ((color & 0x000000FF) << 16) | (color & 0x0000FF00) | ((color & 0x00FF0000) >> 16);
            var pluginNames = Plugin.GetCommandService().CommandManagers.Select(m => $"[c/{color:X}:{m.GetBaseCommands()[0]}]");
            
            sender.SendMessage($"Commands associated with {Plugin.Name}:");
            sender.SendMessage(string.Join(", ", pluginNames));
        }
    }
}