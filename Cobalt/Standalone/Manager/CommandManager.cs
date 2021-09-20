using System;
using System.Collections.Generic;
using System.Linq;
using Cobalt.Api.Wrappers;
using Cobalt.Standalone.Exception;

namespace Cobalt.Standalone.Manager
{
    public class CommandManager
    {
        private static CommandManager _instance;
        public static CommandManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new CommandManager();
                return _instance;
            }
        }

        private Dictionary<string, Action<ICobaltPlayer, List<string>>> _commandActions =
            new Dictionary<string, Action<ICobaltPlayer, List<string>>>();

        public static void RegisterCommand(string[] commands, Action<ICobaltPlayer, List<string>> action)
        {
            if (commands == null) throw new ArgumentNullException(nameof(commands));
            foreach (var command in commands)
            {
                Instance.RegisterCommand(command, action);
            }
        }

        private void RegisterCommand(string command, Action<ICobaltPlayer, List<string>> action)
        {
            if (_commandActions.ContainsKey(command))
            {
                throw new CommandInitException($"Command with the name '{command}' already registered.");
            }
            _commandActions.Add(command, action);
        }

        public void TryExecuteCommand(ICobaltPlayer player, string command, List<string> args)
        {
            Console.Out.WriteLine("HandleCommandMessage");
            if (!_commandActions.ContainsKey(command))
            {
                player.SendErrorMessage($"The command '{command}' is not valid.");
                return;
            }
            _commandActions[command].Invoke(player, args);
        }
        
        public static void HandleCommandMessage(ICobaltPlayer player, string chatMessage)
        {
            var parts = chatMessage.Substring(1).Split(' ');
            var command = parts[0];
            List<string> args = parts.Skip(1).ToList();

            Instance.TryExecuteCommand(player, command, args);
        }
        
        public static void HandleCommand(ICobaltPlayer player, string command, List<string> args)
        {
            Instance.TryExecuteCommand(player, command, args);
        }
    }
}