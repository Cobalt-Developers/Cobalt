using System;
using Cobalt.Api;
using Cobalt.Api.Attribute;
using Cobalt.Loader.Attribute;
using Cobalt.Standalone;
using Terraria;

namespace Cobalt.Plugin
{
    [CobaltPlugin]
    public class CobaltServerPlugin : CobaltStandalonePlugin
    {
        public override string Author => "Kronox";
        public override string Description => "Cobalt Base Plugin";
        public override string Name => "Cobalt";
        public override Version Version => new Version(1, 0, 0, 0);
        
    }
}