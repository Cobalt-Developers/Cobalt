using Cobalt.Api;
using Cobalt.Api.Model;
using CobaltTShock.Wrapper;
using Terraria;
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
            IChatSender sender = args.TPlayer.whoAmI == -1
                ? new TShockChatSender(args.Player)
                : new TShockPlayer(args.Player);
            OnCommand(sender, args.Parameters);
        }
    }
}