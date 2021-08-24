using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CobaltCore;
using CobaltCore.Attributes;
using CobaltServerPlugin.Commands;
using Microsoft.Xna.Framework;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace CobaltServerPlugin
{
    [CommandHandler(new[]{"pl", "plugins"}, new[]{typeof(ListPluginsCommand)})]
    [CommandHandler(new[]{"test"}, new[]{typeof(TestCommand)})]
    [CommandHandler(new[]{"test2"}, new[]{typeof(Test2Command), typeof(Test3Command)})]
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
    }
}