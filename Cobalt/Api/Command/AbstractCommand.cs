using System;
using System.Collections.Generic;
using System.Linq;
using Cobalt.Api.Attribute;
using Cobalt.Api.Command.Argument;
using Cobalt.Api.Wrapper;

namespace Cobalt.Api.Command
{
    public abstract class AbstractCommand
    {
        protected ICobaltPlugin Plugin { get; }
        
        public AbstractCommandManager Manager { get; }
        public string Description { get; private set; }
        
        private List<string[]> subcommands = new List<string[]>();
        private List<ArgumentI> arguments = new List<ArgumentI>();
        private List<string> permissions = new List<string>();
        private bool isIngameCommandOnly;

        protected AbstractCommand(ICobaltPlugin plugin, AbstractCommandManager manager)
        {
            Plugin = plugin;
            Manager = manager;
            ParseAttributes();
        }

        private void ParseAttributes()
        {
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute) System.Attribute.GetCustomAttribute(GetType(), typeof(DescriptionAttribute));
            Description = descriptionAttribute != null ? descriptionAttribute.Description : "Does stuff.";
            
            SubCommandAttribute[] subcommandAttributes = (SubCommandAttribute[]) System.Attribute.GetCustomAttributes(GetType(), typeof(SubCommandAttribute));
            foreach (var attribute in subcommandAttributes)
            {
                subcommands.Add(attribute.Names);
            }
            
            ArgumentAttribute[] argumentAttributes = (ArgumentAttribute[]) System.Attribute.GetCustomAttributes(GetType(), typeof(ArgumentAttribute));
            foreach (var attribute in argumentAttributes)
            {
                arguments.Add(new ArgumentI(Plugin, attribute.Placeholder, attribute.Optional, attribute.Constraint));
            }
            
            PermissionAttribute[] permissionAttributes = (PermissionAttribute[]) System.Attribute.GetCustomAttributes(GetType(), typeof(PermissionAttribute));
            foreach (var attribute in permissionAttributes)
            {
                permissions.Add(attribute.Name);
            }
            
            if (System.Attribute.GetCustomAttribute(GetType(), typeof(IngameCommandAttribute)) != null)
            {
                isIngameCommandOnly = true;
            }
        }
        
        public abstract void Execute(ICobaltPlayer player, List<string> args);

        public bool TryCommand(ICobaltPlayer player, List<string> args)
        {
            if (!HasMatchingSubcommands(args))
                return false;

            var realArguments = args;
            if(subcommands.Count > 0) realArguments.RemoveRange(0, subcommands.Count);
            PreExecute(player, realArguments);
            return true;
        }
        public void PreExecute(ICobaltPlayer player, List<string> args)
        {
            if (isIngameCommandOnly)
            {
                player.SendErrorMessage("You must be a real player to execute this command.");
                return;
            }
            
            if (!HasPermission(player))
            {
                player.SendErrorMessage("You do not have the necessary permission to execute this command.");
                return;
            }

            if (!HasRequiredArgumentSize(args))
            {
                player.SendErrorMessage("Invalid arguments. Try that:");
                player.SendErrorMessage(GetHelpMessage());
                return;
            }
            
            if (!TestArgumentConditions(player, args)) return;

            try
            {
                Execute(player, args);
            }
            catch (NotImplementedException)
            {
                player.SendErrorMessage("This command was not implemented yet. Please contact the plugin developer.");
            }
        }
        
        public bool HasPermission(ICobaltPlayer player)
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

        private bool TestArgumentConditions(ICobaltPlayer argsPlayer, List<string> args)
        {
            var relevantArgs = args.GetRange(0, Math.Min(args.Count, arguments.Count));
            return !relevantArgs.Where((arg, i) => !arguments[i].TestArgumentOrError(argsPlayer, arg)).Any();
        }

        private bool HasRequiredArgumentSize(List<string> args)
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
            
            return $"/{command}{PrefixSpace(string.Join(" ", primarySubcommands))}{PrefixSpace(string.Join(" ", prettyArguments))}";
        }

        private string PrefixSpace(string str)
        {
            return str.Equals(string.Empty) ? "" : $" {str}";
        }
    }
}