using Cobalt.Api;
using Cobalt.Api.Commands;
using CobaltTShock.Wrappers;
using TShockAPI;

namespace CobaltTShock.Commands
{
    public class SimpleCommandManager : Cobalt.Api.Commands.SimpleCommandManager, ICommandManager
    {
        public SimpleCommandManager(ICobaltPlugin plugin, string[] baseCommands) : base(plugin, baseCommands)
        {
        }

        public void OnCommand(CommandArgs args)
        {
            OnCommand(TShockPlayer.Wrap(args.Player), args.Parameters);
        }
    }
}