using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CobaltCore;
using CobaltCore.Attributes;
using CobaltCore.Messages;
using CobaltServerPlugin.Commands;
using CobaltServerPlugin.Configs;
using CobaltServerPlugin.Storage;
using Microsoft.Xna.Framework;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace CobaltServerPlugin
{
    [Configuration(typeof(TestConfig))]
    [Settings(typeof(TestSettings))]
    [CommandHandler(new[]{"pl", "plugins"}, typeof(ListPluginsCommand))]
    [CommandHandler(new[]{"test"}, typeof(TestCommand))]
    [CommandHandler(new[]{"settings"}, typeof(SettingsCommand))]
    [ApiVersion(2, 1)]
    public class CobaltServerPlugin : CobaltPlugin
    {
        public override string Author => "Kronox";
        public override string Description => "Cobalt Base Plugin";
        public override string Name => "Cobalt";
        public override Version Version => new Version(1, 0, 0, 0);
        
        public CobaltServerPlugin(Main game) : base(game)
        {
            
        }

        public override ColorScheme ColorScheme { get; } =
            new ColorScheme(Color.White, Color.White, Color.White, Color.White);
    }
}