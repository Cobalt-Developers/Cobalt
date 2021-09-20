using System;
using Terraria;
using TerrariaApi.Server;

namespace TShockCobaltBridge
{
    [ApiVersion(2, 1)]
    public class CobaltBridgePlugin : TerrariaPlugin
    {
        public override string Author => "Kronox";
        public override string Description => "Cobalt Base Plugin";
        public override string Name => "Cobalt";
        public override Version Version => new Version(1, 0, 0, 0);
        
        public CobaltBridgePlugin(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            
        }
    }
}