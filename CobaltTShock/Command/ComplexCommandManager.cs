using Cobalt.Api;
using CobaltTShock.Wrapper;
using TShockAPI;

namespace CobaltTShock.Command
{
    public class ComplexCommandManager : Cobalt.Api.Command.ComplexCommandManager, ICommandManager
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