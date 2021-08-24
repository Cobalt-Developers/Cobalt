using System.Linq;
using CobaltCore;
using CobaltCore.Attributes;
using CobaltCore.Commands;
using Microsoft.Xna.Framework;
using TerrariaApi.Server;
using TShockAPI;

namespace CobaltServerPlugin.Commands
{
    [SubCommand("bli")]
    [Argument("arg1")]
    [Argument("arg2", true)]
    public class Test3Command : AbstractCommand
    {
        public Test3Command(CobaltPlugin plugin, CommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(CommandArgs args)
        {
            args.Player.SendInfoMessage("blob");
        }
    }
}