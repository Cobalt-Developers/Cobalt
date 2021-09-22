using System;
using System.Collections.Generic;
using System.Linq;
using Cobalt.Api.Attribute;
using Cobalt.Api.Command.Argument;
using Cobalt.Api.Message;
using Cobalt.Api.Model;

namespace Cobalt.Api.Command.Predefined
{
    [Description("Displays command help")]
    [SubCommand("help")]
    [Argument("page", true, typeof(NumberConstraint))]
    public class HelpCommand : AbstractCommand
    { 
        public HelpCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(IChatSender sender, List<string> args)
        {
            var page = args.Count > 0 ? Convert.ToInt32(args[0]) : 1;
            PrintFullHelp(sender, page);
        }
        
        private void PrintFullHelp(IChatSender sender, int page)
        {
            var title = Manager.GetBaseCommands()[0];
            List<string> content = GetHelpMessages(sender);

            new PageableList(title, content).Print(sender, page);
        }

        private List<string> GetHelpMessages(IChatSender sender)
        {
            return GetCommands(sender).Select(c => $"{c.GetHelpMessage()} : {c.Description}").ToList();
        }
        
        private List<AbstractCommand> GetCommands(IChatSender sender)
        {
            var helpCommands = new List<AbstractCommand>(
                Manager.GetCommands().Where(c => c.HasPermission(sender))
                );
            // TODO: Sort with priority
            return helpCommands;
        }
    }
}