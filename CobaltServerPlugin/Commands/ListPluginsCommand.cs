using System.Linq;
using CobaltCore;
using CobaltCore.Commands;
using Microsoft.Xna.Framework;
using TerrariaApi.Server;
using TShockAPI;

namespace CobaltServerPlugin.Commands
{
    public class ListPluginsCommand : AbstractCommand
    {
        public ListPluginsCommand(CobaltPlugin plugin, CommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(CommandArgs args)
        {
            // It's in ABGR format
            var color = Color.BlueViolet.packedValue;

            // Flip color, because Terraria uses RGB
            color = ((color & 0x000000FF) << 16) | (color & 0x0000FF00) | ((color & 0x00FF0000) >> 16);

            // Colorize name to avoid confusion with comma-d names
            var colorTag = $"[c/{color:X}:";
            var pluginNames = ServerApi.Plugins.Select(p => $"{colorTag}{p.Plugin.Name.Replace("]", $"]{colorTag}]")}]");

            var pluginsList = string.Join(", ", pluginNames);
            args.Player.SendInfoMessage(pluginsList);
        }
    }
}