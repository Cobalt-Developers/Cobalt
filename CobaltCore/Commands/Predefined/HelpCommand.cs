using System.Collections.Generic;
using System.Linq;
using CobaltCore.Attributes;
using TShockAPI;

namespace CobaltCore.Commands.Predefined
{
    [Description("Displays command help")]
    [SubCommand("help")]
    public class HelpCommand : AbstractCommand
    { 
        public HelpCommand(CobaltPlugin plugin, CommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(CommandArgs args)
        {
            PrintFullHelp(args.Player);
        }
        
        private void PrintFullHelp(TSPlayer argsPlayer)
        {
            var header = GetHeader(Manager.GetBaseCommands()[0]);
            var content = GetHelpMessages(argsPlayer);

            argsPlayer.SendInfoMessage(header);
            foreach (var line in content) argsPlayer.SendInfoMessage(line);
        }

        private string GetHeader(string label)
        {
            return $"=====[ {label.First().ToString().ToUpper()+label.Substring(1).ToLower()} ]=====";
        }

        private List<string> GetHelpMessages(TSPlayer argsPlayer)
        {
            return GetCommands(argsPlayer).Select(c => $"{c.GetHelpMessage()} : {c.Description}").ToList();
        }
        
        private List<AbstractCommand> GetCommands(TSPlayer argsPlayer)
        {
            var helpCommands = new List<AbstractCommand>(
                Manager.GetCommands().Where(c => c.HasPermission(argsPlayer))
                );
            // TODO: Sort with priority
            return helpCommands;
        }
    }
}