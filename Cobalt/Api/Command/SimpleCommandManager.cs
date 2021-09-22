using System.Collections.Generic;
using Cobalt.Api.Model;

namespace Cobalt.Api.Command
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
        
        public override void OnCommand(IChatSender sender, List<string> args)
        {
            if (command.TryCommand(sender, args)) return;
            
            // don't show help to someone who isn't supposed to see it
            if (!command.HasPermission(sender))
            {
                sender.SendErrorMessage("You do not have the necessary permission to execute this command.");
                return;
            }
            
            sender.SendErrorMessage("Invalid command. Try that:");
            sender.SendErrorMessage(command.GetHelpMessage());
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