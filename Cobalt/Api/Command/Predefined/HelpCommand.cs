using System.Collections.Generic;
using System.Linq;
using Cobalt.Api.Attribute;
using Cobalt.Api.Wrapper;

namespace Cobalt.Api.Command.Predefined
{
    [Description("Displays command help")]
    [SubCommand("help")]
    public class HelpCommand : AbstractCommand
    { 
        public HelpCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(CobaltPlayer player, List<string> args)
        {
            PrintFullHelp(player);
        }
        
        private void PrintFullHelp(CobaltPlayer argsPlayer)
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

        private List<string> GetHelpMessages(CobaltPlayer argsPlayer)
        {
            return GetCommands(argsPlayer).Select(c => $"{c.GetHelpMessage()} : {c.Description}").ToList();
        }
        
        private List<AbstractCommand> GetCommands(CobaltPlayer argsPlayer)
        {
            var helpCommands = new List<AbstractCommand>(
                Manager.GetCommands().Where(c => c.HasPermission(argsPlayer))
                );
            // TODO: Sort with priority
            return helpCommands;
        }
    }
}