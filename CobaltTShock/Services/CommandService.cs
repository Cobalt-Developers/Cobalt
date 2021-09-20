using Cobalt.Api;
using Cobalt.Api.Commands;
using Cobalt.Api.Services;
using CobaltTShock.Commands;
using TShockAPI;

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

        protected override AbstractSimpleCommandManager CreateSimpleCommandManager(ICobaltPlugin plugin, string[] commands)
        {
            return new SimpleCommandManager(Plugin, commands);
        }

        protected override AbstractComplexCommandManager CreateComplexCommandManager(ICobaltPlugin plugin, string[] commands)
        {
            return new ComplexCommandManager(Plugin, commands);
        }
    }
}