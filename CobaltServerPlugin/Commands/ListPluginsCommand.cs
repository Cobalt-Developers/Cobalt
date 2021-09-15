using System.Collections.Generic;
using System.Linq;
using CobaltCore;
using CobaltCore.Commands;
using CobaltCore.Wrappers;
using Microsoft.Xna.Framework;
using TerrariaApi.Server;
using TShockAPI;

namespace CobaltServerPlugin.Commands
{
    public class ListPluginsCommand : AbstractCommand
    {
        public ListPluginsCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(ICobaltPlayer player, List<string> args, string message, bool silent)
        {
            var color = Color.BlueViolet.packedValue;
            color = ((color & 0x000000FF) << 16) | (color & 0x0000FF00) | ((color & 0x00FF0000) >> 16);
            var colorTag = $"[c/{color:X}:";
            var pluginNames = ServerApi.Plugins.Select(p => $"{colorTag}{p.Plugin.Name.Replace("]", $"]{colorTag}]")}]");

            var pluginsList = string.Join(", ", pluginNames);
            player.SendMessage(pluginsList);
        }
    }
}