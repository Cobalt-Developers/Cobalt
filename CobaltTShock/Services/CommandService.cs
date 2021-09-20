using Cobalt.Api;
using Cobalt.Api.Commands;
using Cobalt.Api.Services;
using CobaltTShock.Commands;
using TShockAPI;
using ComplexCommandManager = Cobalt.Api.Commands.ComplexCommandManager;
using SimpleCommandManager = Cobalt.Api.Commands.SimpleCommandManager;

namespace CobaltTShock.Services
{
    public class CommandService : AbstractCommandService
    {
        public CommandService(ICobaltPlugin plugin) : base(plugin)
        {
        }

        protected override void RegisterCommand(AbstractCommandManager commandManager, string[] commands)
        {
            var cmdManager = (ICommandManager) commandManager;
            TShockAPI.Commands.ChatCommands.Add(new Command(cmdManager.OnCommand, commands));
        }

        protected override SimpleCommandManager CreateSimpleCommandManager(ICobaltPlugin plugin, string[] commands)
        {
            return new Commands.SimpleCommandManager(Plugin, commands);
        }

        protected override ComplexCommandManager CreateComplexCommandManager(ICobaltPlugin plugin, string[] commands)
        {
            return new Commands.ComplexCommandManager(Plugin, commands);
        }
    }
}