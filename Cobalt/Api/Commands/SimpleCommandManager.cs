using System.Collections.Generic;
using Cobalt.Api.Wrappers;

namespace Cobalt.Api.Commands
{
    public class SimpleCommandManager : AbstractCommandManager
    {
        private string[] baseCommands;
        private AbstractCommand command;

        public SimpleCommandManager(ICobaltPlugin plugin, string[] baseCommands) : base(plugin)
        {
            this.baseCommands = baseCommands;
        }
        
        public void SetCommand(AbstractCommand command)
        {
            this.command = command;
        }
        
        public override void OnCommand(ICobaltPlayer player, List<string> args)
        {
            if (command.TryCommand(player, args)) return;
            
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