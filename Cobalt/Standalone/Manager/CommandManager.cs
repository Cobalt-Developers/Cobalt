﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cobalt.Api.Wrapper;
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

        private Dictionary<string, Action<CobaltPlayer, List<string>>> _commandActions =
            new Dictionary<string, Action<CobaltPlayer, List<string>>>();

        public static void RegisterCommand(string[] commands, Action<CobaltPlayer, List<string>> action)
        {
            if (commands == null) throw new ArgumentNullException(nameof(commands));
            foreach (var command in commands)
            {
                Instance.RegisterCommand(command, action);
            }
        }

        private void RegisterCommand(string command, Action<CobaltPlayer, List<string>> action)
        {
            if (_commandActions.ContainsKey(command))
            {
                throw new CommandInitException($"Command with the name '{command}' already registered.");
            }
            _commandActions.Add(command, action);
        }

        public bool TryExecuteCommand(CobaltPlayer player, string command, List<string> args)
        {
            if (!_commandActions.ContainsKey(command))
            {
                return false;
            }
            _commandActions[command].Invoke(player, args);
            return true;
        }
        
        public static bool HandleCommandMessage(CobaltPlayer player, string chatMessage)
        {
            var parts = chatMessage.Substring(1).Split(' ');
            var command = parts[0];
            List<string> args = parts.Skip(1).ToList();

            return Instance.TryExecuteCommand(player, command, args);
        }
        
        public static bool HandleCommand(CobaltPlayer player, string command, List<string> args)
        {
            return Instance.TryExecuteCommand(player, command, args);
        }
    }
}