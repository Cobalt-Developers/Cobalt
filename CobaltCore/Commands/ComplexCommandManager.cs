using System.Collections.Generic;
using CobaltCore.Commands.Predefined;
using TShockAPI;

namespace CobaltCore.Commands
{
    public class ComplexCommandManager : CommandManager
    {
        private string[] baseCommands;
        private List<AbstractCommand> subCommands = new List<AbstractCommand>();
        
        private HelpCommand helpCommand;
        
        public ComplexCommandManager(CobaltPlugin plugin, string[] baseCommands) : base(plugin)
        {
            this.baseCommands = baseCommands;
            helpCommand = new HelpCommand(Plugin, this);
            subCommands.Add(helpCommand);
        }

        public override void OnCommand(CommandArgs args)
        {
            if (args.Parameters.Count == 0)
            {
                // Display help (page 0)
                helpCommand.PreExecute(args);
                return;
            }

            // Try Actual Commands
            foreach (AbstractCommand command in subCommands)
            {
                if (command.TryCommand(args)) return;
            }

            // Display help (all pages)
            helpCommand.PreExecute(args);
        }

        public void AddCommand(AbstractCommand command)
        {
            subCommands.Add(command);
        }
        
        public override string[] GetBaseCommands()
        {
            return baseCommands;
        }
        
        public override AbstractCommand[] GetCommands()
        {
            return subCommands.ToArray();
        }
    }
}