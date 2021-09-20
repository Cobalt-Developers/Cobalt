using Cobalt.Api;
using CobaltTShock.Wrapper;
using TShockAPI;

namespace CobaltTShock.Command
{
    public class SimpleCommandManager : Cobalt.Api.Command.SimpleCommandManager, ICommandManager
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