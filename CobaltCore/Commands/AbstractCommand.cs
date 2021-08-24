using System;
using System.Collections.Generic;
using System.Linq;
using CobaltCore.Attributes;
using Terraria;
using TShockAPI;
using Permission = CobaltCore.Attributes.Permission;

namespace CobaltCore.Commands
{
    public abstract class AbstractCommand
    {
        private CobaltPlugin Plugin { get; }
        
        public CommandManager Manager { get; }

        private List<string[]> subcommands = new List<string[]>();
        private List<ArgumentI> arguments = new List<ArgumentI>();
        private List<string> permissions = new List<string>();
        private bool isIngameCommandOnly;

        protected AbstractCommand(CobaltPlugin plugin, CommandManager manager)
        {
            Plugin = plugin;
            Manager = manager;
            ParseAttributes();
        }

        private void ParseAttributes()
        {
            SubCommand[] subcommandAttributes = (SubCommand[]) Attribute.GetCustomAttributes(GetType(), typeof(SubCommand));
            foreach (var attribute in subcommandAttributes)
            {
                subcommands.Add(attribute.Names);
            }
            
            Argument[] argumentAttributes = (Argument[]) Attribute.GetCustomAttributes(GetType(), typeof(Argument));
            foreach (var attribute in argumentAttributes)
            {
                arguments.Add(new ArgumentI(Plugin, attribute.Placeholder, attribute.Optional));
            }
            
            Permission[] permissionAttributes = (Permission[]) Attribute.GetCustomAttributes(GetType(), typeof(Permission));
            foreach (var attribute in permissionAttributes)
            {
                permissions.Add(attribute.Name);
            }
            
            if (Attribute.GetCustomAttribute(GetType(), typeof(IngameCommand)) != null)
            {
                isIngameCommandOnly = true;
            }
        }
        
        public abstract void Execute(CommandArgs args);

        public bool TryCommand(CommandArgs args)
        {
            if (!HasMatchingSubcommands(args.Parameters))
                return false;

            var realArguments = args.Parameters;
            if(subcommands.Count > 0) realArguments.RemoveRange(0, subcommands.Count);
            PreExecute(new CommandArgs(args.Message, args.Silent, args.Player, realArguments));
            return true;
        }
        public void PreExecute(CommandArgs args)
        {
            if (isIngameCommandOnly)
            {
                args.Player.SendErrorMessage("You must be a real player to execute this command.");
                return;
            }
            
            if (!HasPermission(args.Player))
            {
                args.Player.SendErrorMessage("You do not have the necessary permission to execute this command.");
                return;
            }

            if (!HasMatchingArguments(args.Parameters))
            {
                args.Player.SendErrorMessage("Invalid arguments. Try that:");
                args.Player.SendErrorMessage(GetHelpMessage());
                return;
            }

            Execute(args);
        }
        
        public bool HasPermission(TSPlayer player)
        {
            return GetPermissions().Count == 0 || permissions.All(player.HasPermission);
        }

        private List<string> GetPermissions()
        {
            return permissions;
        }

        private bool HasMatchingSubcommands(List<string> args)
        {
            if (args.Count < subcommands.Count) return false;
            if (subcommands.Count == 0) return true;

            return !subcommands.Where((t, i) => !t
                .Any(n => n.Equals(args[i], StringComparison.OrdinalIgnoreCase))).Any();
        }
        
        private bool HasMatchingArguments(List<string> args)
        {
            var requiredArgumentSize = GetRequiredArgumentSize();
            return args.Count >= requiredArgumentSize;
        }

        private int GetRequiredArgumentSize()
        {
            return arguments.Count(argument => !argument.Optional);
        }
        
        public string GetHelpMessage()
        {
            var command = Manager.GetBaseCommands()[0];
            var primarySubcommands = subcommands.Select(s => s[0]).ToArray();
            var prettyArguments = arguments.Select(a => a.ToPrettyString());
            
            return $"/{command} {string.Join(" ", primarySubcommands)} {string.Join(" ", prettyArguments)}";
        }
    }
}