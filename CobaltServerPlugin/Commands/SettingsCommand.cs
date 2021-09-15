using System.Collections.Generic;
using CobaltCore;
using CobaltCore.Attributes;
using CobaltCore.Commands;
using CobaltCore.Storages.Settings;
using CobaltCore.Wrappers;
using CobaltServerPlugin.Storage;
using TShockAPI;

namespace CobaltServerPlugin.Commands
{
    [Argument("input")]
    public class SettingsCommand : AbstractCommand
    {
        public SettingsCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(ICobaltPlayer player, List<string> args, string message, bool silent)
        {
            SettingsFile<TestSettings> settingsFile = Plugin.GetSettingsService().GetOrCreateSettings<TestSettings>(player);
            player.SendMessage($"Old value: {settingsFile.Content.TestField}");
            settingsFile.Content.TestField = args[0];
            settingsFile.Save();
        }
    }
}