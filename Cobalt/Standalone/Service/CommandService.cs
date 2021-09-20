using Cobalt.Api;
using Cobalt.Api.Commands;
using Cobalt.Api.Services;
using Cobalt.Standalone.Manager;

namespace Cobalt.Standalone.Service
{
    public class CommandService : AbstractCommandService
    {
        public CommandService(ICobaltPlugin plugin) : base(plugin)
        {
        }

        protected override void RegisterCommand(AbstractCommandManager commandManager, params string[] commands)
        {
            CommandManager.RegisterCommand(commands, commandManager.OnCommand);
        }
    }
}