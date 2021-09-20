using System;
using System.Collections.Generic;
using System.Linq;
using Cobalt.Api.Attributes;
using Cobalt.Api.Commands;
using Cobalt.Api.Commands.Predefined;
using Cobalt.Api.Messages;
using Attribute = System.Attribute;

namespace Cobalt.Api.Services
{
    public abstract class AbstractCommandService : AbstractService
    {
        public List<AbstractCommandManager> CommandManagers { get; } = new List<AbstractCommandManager>();

        public AbstractCommandService(ICobaltPlugin plugin) : base(plugin)
        {
        }

        public override void Init()
        {
            // Add predefined plugin commands
            string pluginCommand = Plugin.Name.ToLower();
            ComplexCommandManager pluginCommandManager = CreateComplexCommandManager(Plugin, new[] {pluginCommand});
            pluginCommandManager.AddCommand(new VersionCommand(Plugin, pluginCommandManager));
            pluginCommandManager.AddCommand(new CommandListCommand(Plugin, pluginCommandManager));
            pluginCommandManager.AddCommand(new ReloadCommand(Plugin, pluginCommandManager));
            RegisterCommand(pluginCommandManager, pluginCommand);
            CommandManagers.Add(pluginCommandManager);
            
            // Add custom commands
            CommandHandlerAttribute[] handlers = (CommandHandlerAttribute[]) Attribute.GetCustomAttributes(Plugin.GetType(), typeof(CommandHandlerAttribute));

            foreach (var handler in handlers)
            {
                AddCommandHandler(handler.Commands, handler.Handlers);
            }
        }

        public override void Reload()
        {
            // not supported
        }

        private void AddCommandHandler(string[] commands, Type[] handlers) // TODO: restrict Type
        {
            if (commands == null || commands.Length == 0)
            {
                Plugin.Log(LogLevel.VERBOSE,"AbstractCommandService: Not registering (no command assigned)");
                return;
            }

            commands = commands.Select(c => c.ToLower()).ToArray();
            
            if (handlers == null || handlers.Length == 0)
            {
                Plugin.Log(LogLevel.VERBOSE,$"AbstractCommandService: Not registering {commands[0]} (no handlers assigned)");
                return;
            }

            AbstractCommandManager commandManager;

            if (handlers.Length == 1)
            {
                commandManager = CreateSimpleCommandManager(Plugin, commands);
                AbstractCommand command = (AbstractCommand) Activator.CreateInstance(handlers[0], Plugin, commandManager);

                ((SimpleCommandManager) commandManager).SetCommand(command);
            }
            else
            {
                commandManager = CreateComplexCommandManager(Plugin, commands);
                foreach (Type handler in handlers)
                {
                    AbstractCommand command = (AbstractCommand) Activator.CreateInstance(handler, Plugin, commandManager);
                    ((ComplexCommandManager) commandManager).AddCommand(command);
                }
            }

            RegisterCommand(commandManager, commands);
            
            CommandManagers.Add(commandManager);
        }

        protected abstract void RegisterCommand(AbstractCommandManager commandManager, params string[] commands);

        protected virtual SimpleCommandManager CreateSimpleCommandManager(ICobaltPlugin plugin, string[] commands)
        {
            return new SimpleCommandManager(plugin, commands);
        }

        protected virtual ComplexCommandManager CreateComplexCommandManager(ICobaltPlugin plugin, string[] commands)
        {
            return new ComplexCommandManager(plugin, commands);
        }
    }
}