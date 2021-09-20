using Cobalt.Api;
using Cobalt.Api.Commands;
using CobaltTShock.Wrappers;
using TShockAPI;

namespace CobaltTShock.Commands
{
    public class ComplexCommandManager : Cobalt.Api.Commands.ComplexCommandManager, ICommandManager
    {
        public ComplexCommandManager(ICobaltPlugin plugin, string[] baseCommands) : base(plugin, baseCommands)
        {
        }

        public void OnCommand(CommandArgs args)
        {
            OnCommand(TShockPlayer.Wrap(args.Player), args.Parameters);
        }
    }
}