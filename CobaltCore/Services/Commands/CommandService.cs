using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CobaltCore.Attributes;
using CobaltCore.Commands;
using CobaltCore.Commands.Predefined;
using CobaltCore.Messages;
using TShockAPI;
using Attribute = System.Attribute;

namespace CobaltCore.Services.Commands
{
    public class CommandService : AbstractService
    {
        public List<CommandManager> CommandManagers { get; } = new List<CommandManager>();

        public CommandService(CobaltPlugin plugin) : base(plugin)
        {
        }

        public override void Init()
        {
            // Add predefined plugin commands
            string pluginCommand = Plugin.Name.ToLower();
            ComplexCommandManager pluginCommandManager = new ComplexCommandManager(Plugin, new[] {pluginCommand});
            pluginCommandManager.AddCommand(new VersionCommand(Plugin, pluginCommandManager));
            pluginCommandManager.AddCommand(new CommandListCommand(Plugin, pluginCommandManager));
            pluginCommandManager.AddCommand(new ReloadCommand(Plugin, pluginCommandManager));
            TShockAPI.Commands.ChatCommands.Add(new Command(pluginCommandManager.OnCommand, pluginCommand));
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

        private void AddCommandHandler(string[] commands, Type[] handlers) // TODO: restrict type
        {
            if (commands == null || commands.Length == 0)
            {
                Plugin.Log(LogLevel.VERBOSE,"CommandService: Not registering (no command assigned)");
                return;
            }

            commands = commands.Select(c => c.ToLower()).ToArray();
            
            if (handlers == null || handlers.Length == 0)
            {
                Plugin.Log(LogLevel.VERBOSE,$"CommandService: Not registering {commands[0]} (no handlers assigned)");
                return;
            }

            CommandManager commandManager;

            if (handlers.Length == 1)
            {
                commandManager = new SimpleCommandManager(Plugin, commands);
                AbstractCommand command = (AbstractCommand) Activator.CreateInstance(handlers[0], Plugin, commandManager);

                ((SimpleCommandManager) commandManager).SetCommand(command);
            }
            else
            {
                commandManager = new ComplexCommandManager(Plugin, commands);
                foreach (Type handler in handlers)
                {
                    AbstractCommand command = (AbstractCommand) Activator.CreateInstance(handler, Plugin, commandManager);
                    ((ComplexCommandManager) commandManager).AddCommand(command);
                }
            }

            TShockAPI.Commands.ChatCommands.Add(new Command(commandManager.OnCommand, commands));
            
            CommandManagers.Add(commandManager);
        }
    }
}