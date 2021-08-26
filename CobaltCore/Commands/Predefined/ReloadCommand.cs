using System;
using CobaltCore.Attributes;
using TShockAPI;

namespace CobaltCore.Commands.Predefined
{
    [SubCommand("reload", "rl")]
    public class ReloadCommand : AbstractCommand
    {
        public ReloadCommand(CobaltPlugin plugin, CommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(CommandArgs args)
        {
            try
            {
                Plugin.ServiceManager.Reload();
                args.Player.SendInfoMessage("Plugin was successfully reloaded!");
            }
            catch (Exception e)
            {
                args.Player.SendErrorMessage("Plugin reload failed! View console for more details.");
                Console.WriteLine(e.ToString());
            }
        }
    }
}