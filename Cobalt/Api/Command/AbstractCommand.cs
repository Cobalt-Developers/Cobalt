using System;
using System.Collections.Generic;
using System.Linq;
using Cobalt.Api.Attribute;
using Cobalt.Api.Command.Argument;
using Cobalt.Api.Model;

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
        
        public abstract void Execute(IChatSender chatSender, List<string> args);

        public bool TryCommand(IChatSender sender, List<string> args)
        {
            if (!HasMatchingSubcommands(args))
                return false;

            var realArguments = args;
            if(subcommands.Count > 0) realArguments.RemoveRange(0, subcommands.Count);
            PreExecute(sender, realArguments);
            return true;
        }
        public void PreExecute(IChatSender sender, List<string> args)
        {
            if (isIngameCommandOnly && !(sender is IPlayer))
            {
                sender.SendErrorMessage("You must be a real sender to execute this command.");
                return;
            }
            
            if (!HasPermission(sender))
            {
                sender.SendErrorMessage("You do not have the necessary permission to execute this command.");
                return;
            }

            if (!HasRequiredArgumentSize(args))
            {
                sender.SendErrorMessage("Invalid arguments. Try that:");
                sender.SendErrorMessage(GetHelpMessage());
                return;
            }
            
            if (!TestArgumentConditions(sender, args)) return;

            try
            {
                Execute(sender, args);
            }
            catch (NotImplementedException)
            {
                sender.SendErrorMessage("This command was not implemented yet. Please contact the plugin developer.");
            }
        }
        
        public bool HasPermission(IChatSender sender)
        {
            return GetPermissions().Count == 0 || permissions.All(sender.HasPermission);
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

        private bool TestArgumentConditions(IChatSender sender, List<string> args)
        {
            var relevantArgs = args.GetRange(0, Math.Min(args.Count, arguments.Count));
            return !relevantArgs.Where((arg, i) => !arguments[i].TestArgumentOrError(sender, arg)).Any();
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