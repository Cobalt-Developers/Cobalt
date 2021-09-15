using System.Collections.Generic;
using CobaltCore.Wrappers;

namespace CobaltCore.Commands
{
    public abstract class AbstractSimpleCommandManager : AbstractCommandManager
    {
        private string[] baseCommands;
        private AbstractCommand command;

        public AbstractSimpleCommandManager(ICobaltPlugin plugin, string[] baseCommands) : base(plugin)
        {
            this.baseCommands = baseCommands;
        }
        
        public void SetCommand(AbstractCommand command)
        {
            this.command = command;
        }
        
        public override void OnCommand(ICobaltPlayer player, List<string> args, string message, bool silent)
        {
            if (command.TryCommand(player, args, message, silent)) return;
            
            // don't show help to someone who isn't supposed to see it
            if (!command.HasPermission(player))
            {
                player.SendErrorMessage("You do not have the necessary permission to execute this command.");
                return;
            }
            
            player.SendErrorMessage("Invalid command. Try that:");
            player.SendErrorMessage(command.GetHelpMessage());
        }

        public override string[] GetBaseCommands()
        {
            return baseCommands;
        }
        
        public override AbstractCommand[] GetCommands()
        {
            return new []{command};
        }
    }
}