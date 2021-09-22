using System.Collections.Generic;
using System.Linq;
using Cobalt.Api.Attribute;
using Cobalt.Api.Model;

namespace Cobalt.Api.Command.Predefined
{
    [Description("Displays command help")]
    [SubCommand("help")]
    public class HelpCommand : AbstractCommand
    { 
        public HelpCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(IChatSender sender, List<string> args)
        {
            PrintFullHelp(sender);
        }
        
        private void PrintFullHelp(IChatSender sender)
        {
            var header = GetHeader(Manager.GetBaseCommands()[0]);
            var content = GetHelpMessages(sender);

            sender.SendMessage(header);
            foreach (var line in content) sender.SendMessage(line);
        }

        private string GetHeader(string label)
        {
            return $"=====[ {label.First().ToString().ToUpper()+label.Substring(1).ToLower()} ]=====";
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