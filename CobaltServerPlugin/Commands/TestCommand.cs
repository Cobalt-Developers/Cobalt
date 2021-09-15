using System;
using System.Collections.Generic;
using System.Linq;
using CobaltCore;
using CobaltCore.Attributes;
using CobaltCore.Commands;
using CobaltCore.Storages.Configs;
using CobaltCore.Wrappers;
using CobaltServerPlugin.Configs;
using Microsoft.Xna.Framework;
using TerrariaApi.Server;
using TShockAPI;

namespace CobaltServerPlugin.Commands
{
    public class TestCommand : AbstractCommand
    {
        public TestCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(ICobaltPlayer player, List<string> args, string message, bool silent)
        {
            ConfigurationFile<TestConfig> config = Plugin.GetConfigService().GetConfig<TestConfig>();
            
            player.SendMessage(config.Content.TestField);
            config.Content.TestField = new Random().Next(100).ToString();
            config.Save();
        }
    }
}