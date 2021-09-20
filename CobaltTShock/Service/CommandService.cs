using Cobalt.Api;
using Cobalt.Api.Command;
using Cobalt.Api.Service;
using CobaltTShock.Command;
using ComplexCommandManager = Cobalt.Api.Command.ComplexCommandManager;
using SimpleCommandManager = Cobalt.Api.Command.SimpleCommandManager;

namespace CobaltTShock.Service
{
    public class CommandService : AbstractCommandService
    {
        public CommandService(ICobaltPlugin plugin) : base(plugin)
        {
        }

        protected override void RegisterCommand(AbstractCommandManager commandManager, string[] commands)
        {
            var cmdManager = (ICommandManager) commandManager;
            TShockAPI.Commands.ChatCommands.Add(new TShockAPI.Command(cmdManager.OnCommand, commands));
        }

        protected override SimpleCommandManager CreateSimpleCommandManager(ICobaltPlugin plugin, string[] commands)
        {
            return new Command.SimpleCommandManager(Plugin, commands);
        }

        protected override ComplexCommandManager CreateComplexCommandManager(ICobaltPlugin plugin, string[] commands)
        {
            return new Command.ComplexCommandManager(Plugin, commands);
        }
    }
}