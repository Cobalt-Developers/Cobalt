using CobaltCore.Attributes;
using TerrariaApi.Server;
using TShockAPI;

namespace CobaltCore.Commands.Predefined
{
    [SubCommand("version", "v")]
    public class VersionCommand: AbstractCommand
    {
        public VersionCommand(CobaltPlugin plugin, CommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(CommandArgs args)
        {
            args.Player.SendInfoMessage($"You are running {Plugin.Name} version {Plugin.Version} by {Plugin.Author}.");
            args.Player.SendInfoMessage($"{Plugin.Description}");
        }
    }
}