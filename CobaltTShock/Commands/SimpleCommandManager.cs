using Cobalt.Api;
using Cobalt.Api.Commands;
using CobaltTShock.Wrappers;
using TShockAPI;

namespace CobaltTShock.Commands
{
    public class SimpleCommandManager : AbstractSimpleCommandManager, ICommandManager
    {
        public SimpleCommandManager(ICobaltPlugin plugin, string[] baseCommands) : base(plugin, baseCommands)
        {
        }

        public void OnCommand(CommandArgs args)
        {
            OnCommand(TShockPlayer.Wrap(args.Player), args.Parameters, args.Message, args.Silent);
        }
    }
}