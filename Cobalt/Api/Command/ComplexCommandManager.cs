using System.Collections.Generic;
using Cobalt.Api.Command.Predefined;
using Cobalt.Api.Model;

namespace Cobalt.Api.Command
{
    public class ComplexCommandManager : AbstractCommandManager
    {
        private string[] baseCommands;
        private List<AbstractCommand> subCommands = new List<AbstractCommand>();
        
        private HelpCommand helpCommand;
        
        public ComplexCommandManager(ICobaltPlugin plugin, string[] baseCommands) : base(plugin)
        {
            this.baseCommands = baseCommands;
            helpCommand = new HelpCommand(Plugin, this);
            subCommands.Add(helpCommand);
        }

        public override void OnCommand(IChatSender sender, List<string> args)
        {
            if (args.Count == 0)
            {
                // Display help (page 0)
                helpCommand.PreExecute(sender, args);
                return;
            }

            // Try Actual Commands
            foreach (AbstractCommand command in subCommands)
            {
                if (command.TryCommand(sender, args)) return;
            }

            // Display help (all pages)
            helpCommand.PreExecute(sender, args);
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