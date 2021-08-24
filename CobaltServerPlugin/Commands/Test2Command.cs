using System.Linq;
using CobaltCore;
using CobaltCore.Attributes;
using CobaltCore.Commands;
using Microsoft.Xna.Framework;
using TerrariaApi.Server;
using TShockAPI;

namespace CobaltServerPlugin.Commands
{
    [SubCommand("bla")]
    public class Test2Command : AbstractCommand
    {
        public Test2Command(CobaltPlugin plugin, CommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(CommandArgs args)
        {
            args.Player.SendInfoMessage("blob");
        }
    }
}