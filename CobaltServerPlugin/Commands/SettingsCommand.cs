using CobaltCore;
using CobaltCore.Attributes;
using CobaltCore.Commands;
using CobaltCore.Storages.Settings;
using CobaltServerPlugin.Storage;
using TShockAPI;

namespace CobaltServerPlugin.Commands
{
    [Argument("input")]
    public class SettingsCommand : AbstractCommand
    {
        public SettingsCommand(CobaltPlugin plugin, CommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(CommandArgs args)
        {
            SettingsFile<TestSettings> settingsFile = Plugin.GetSettingsService().GetOrCreateSettings<TestSettings>(args.Player);
            args.Player.SendInfoMessage($"Old value: {settingsFile.Content.TestField}");
            settingsFile.Content.TestField = args.Parameters[0];
            settingsFile.Save();
        }
    }
}