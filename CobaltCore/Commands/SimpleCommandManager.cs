using TShockAPI;

namespace CobaltCore.Commands
{
    public class SimpleCommandManager : CommandManager
    {
        private string[] baseCommands;
        private AbstractCommand command;

        public SimpleCommandManager(CobaltPlugin plugin, string[] baseCommands) : base(plugin)
        {
            this.baseCommands = baseCommands;
        }
        
        public void SetCommand(AbstractCommand command)
        {
            this.command = command;
        }
        
        public override void OnCommand(CommandArgs args)
        {
            if (command.TryCommand(args)) return;
            
            // don't show help to someone who isn't supposed to see it
            if (!command.HasPermission(args.Player))
            {
                args.Player.SendErrorMessage("You do not have the necessary permission to execute this command.");
                return;
            }
            
            args.Player.SendErrorMessage("Invalid command. Try that:");
            args.Player.SendErrorMessage(command.GetHelpMessage());
        }

        public override string[] GetCommands()
        {
            return baseCommands;
        }
    }
}