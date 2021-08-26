using System.Linq;
using CobaltCore.Attributes;
using Microsoft.Xna.Framework;
using TerrariaApi.Server;
using TShockAPI;

namespace CobaltCore.Commands.Predefined
{
    [Description("Lists all associated commands")]
    [SubCommand("commands", "command", "cmd")]
    public class CommandListCommand : AbstractCommand
    {
        public CommandListCommand(CobaltPlugin plugin, CommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(CommandArgs args)
        {
            var color = Color.BlueViolet.packedValue;
            color = ((color & 0x000000FF) << 16) | (color & 0x0000FF00) | ((color & 0x00FF0000) >> 16);
            var pluginNames = Plugin.GetCommandService().CommandManagers.Select(m => $"[c/{color:X}:{m.GetBaseCommands()[0]}]");
            
            args.Player.SendInfoMessage($"Commands associated with {Plugin.Name}:");
            args.Player.SendInfoMessage(string.Join(", ", pluginNames));
        }
    }
}