using System.Collections.Generic;
using System.Linq;
using CobaltCore.Attributes;
using CobaltCore.Wrappers;

namespace CobaltCore.Commands.Predefined
{
    [Description("Displays command help")]
    [SubCommand("help")]
    public class HelpCommand : AbstractCommand
    { 
        public HelpCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(ICobaltPlayer player, List<string> args, string message, bool silent)
        {
            PrintFullHelp(player);
        }
        
        private void PrintFullHelp(ICobaltPlayer argsPlayer)
        {
            var header = GetHeader(Manager.GetBaseCommands()[0]);
            var content = GetHelpMessages(argsPlayer);

            argsPlayer.SendMessage(header);
            foreach (var line in content) argsPlayer.SendMessage(line);
        }

        private string GetHeader(string label)
        {
            return $"=====[ {label.First().ToString().ToUpper()+label.Substring(1).ToLower()} ]=====";
        }

        private List<string> GetHelpMessages(ICobaltPlayer argsPlayer)
        {
            return GetCommands(argsPlayer).Select(c => $"{c.GetHelpMessage()} : {c.Description}").ToList();
        }
        
        private List<AbstractCommand> GetCommands(ICobaltPlayer argsPlayer)
        {
            var helpCommands = new List<AbstractCommand>(
                Manager.GetCommands().Where(c => c.HasPermission(argsPlayer))
                );
            // TODO: Sort with priority
            return helpCommands;
        }
    }
}