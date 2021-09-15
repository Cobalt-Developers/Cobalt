using System;
using System.Linq;
using CobaltCore;
using CobaltCore.Attributes;
using CobaltCore.Commands;
using CobaltCore.Storages.Configs;
using CobaltServerPlugin.Configs;
using Microsoft.Xna.Framework;
using TerrariaApi.Server;
using TShockAPI;

namespace CobaltServerPlugin.Commands
{
    public class TestCommand : AbstractCommand
    {
        public TestCommand(CobaltPlugin plugin, CommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(CommandArgs args)
        {
            ConfigurationFile<TestConfig> config = Plugin.GetConfigService().GetConfig<TestConfig>();
            
            args.Player.SendInfoMessage(config.Content.TestField);
            config.Content.TestField = new Random().Next(100).ToString();
            config.Save();
        }
    }
}