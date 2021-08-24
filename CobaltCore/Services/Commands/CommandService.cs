using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CobaltCore.Attributes;
using CobaltCore.Commands;
using CobaltCore.Messages;
using TShockAPI;
using Attribute = System.Attribute;

namespace CobaltCore.Services.Commands
{
    public class CommandService : AbstractService
    {
        private List<CommandManager> commandManagers = new List<CommandManager>();

        public CommandService(CobaltPlugin plugin) : base(plugin)
        {
        }

        public override void Init()
        {
            var handlers = (CommandHandlerAttribute[]) Attribute.GetCustomAttributes(Plugin.GetType(), typeof(CommandHandlerAttribute));

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
            
            commandManagers.Add(commandManager);
        }
    }
}