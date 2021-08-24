using System.Collections.Generic;
using TShockAPI;

namespace CobaltCore.Commands
{
    public class ComplexCommandManager : CommandManager
    {
        private string[] baseCommands;
        private List<AbstractCommand> subCommands = new List<AbstractCommand>();
        
        public ComplexCommandManager(CobaltPlugin plugin, string[] baseCommands) : base(plugin)
        {
            this.baseCommands = baseCommands;
        }

        public override void OnCommand(CommandArgs args)
        {
            if (args.Parameters.Count == 0)
            {
                // Only Base Command
                // TODO: Display help
                args.Player.SendInfoMessage("display help");
                return;
            }

            // Try Actual Commands
            foreach (AbstractCommand command in subCommands)
            {
                if (command.TryCommand(args)) return;
            }

            // TODO: Display Help
            args.Player.SendInfoMessage("possible help?");
        }

        public void AddCommand(AbstractCommand command)
        {
            subCommands.Add(command);
        }
        
        public override string[] GetCommands()
        {
            return baseCommands;
        }
    }
}