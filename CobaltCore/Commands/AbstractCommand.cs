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
            SubCommand[] subcommands = (SubCommand[]) Attribute.GetCustomAttributes(GetType(), typeof(SubCommand));
            foreach (SubCommand subcommand in subcommands)
            {
                this.subcommands.Add(subcommand.Names);
            }
            
            Permission[] permissions = (Permission[]) Attribute.GetCustomAttributes(GetType(), typeof(Permission));
            foreach (Permission permission in permissions)
            {
                this.permissions.Add(permission.Name);
            }
            
            if (Attribute.GetCustomAttribute(GetType(), typeof(IngameCommand)) != null)
            {
                isIngameCommandOnly = true;
            }
        }
        
        public abstract void Execute(CommandArgs args);

        public bool TryCommand(CommandArgs args)
        {
            if (!IsMatchingSubcommands(args.Parameters))
                return false;
            
            PreExecute(args);
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

        private bool IsMatchingSubcommands(List<string> args)
        {
            if (args.Count < subcommands.Count) return false;
            if (subcommands.Count == 0) return true;

            return !subcommands.Where((t, i) => !t
                .Any(n => n.Equals(args[i], StringComparison.OrdinalIgnoreCase))).Any();
        }

        public string GetHelpMessage()
        {
            var command = Manager.GetCommands()[0];
            var primarySubcommands = subcommands.Select(s => s[0]).ToArray();
            return $"/{command} {string.Join(" ", primarySubcommands)}";
        }
    }
}