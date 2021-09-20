using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Cobalt.Loader.Exception;
using CobaltTShock.Wrappers;
using Terraria;
using Terraria.UI.Chat;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;
using TShockCobaltBridge.Helper;

namespace TShockCobaltBridge
{
    [ApiVersion(2, 1)]
    public class CobaltBridgePlugin : TerrariaPlugin
    {
        public override string Author => "Kronox";
        public override string Description => "Cobalt Base Plugin";
        public override string Name => "CobaltBridge";
        public override Version Version => new Version(1, 0, 0, 0);
        
        public CobaltBridgePlugin(Main game) : base(game)
        {
            DllHelper.ApplyDllMagic();
        }

        public override void Initialize()
        {
            var cobaltDll = Path.GetFullPath("Cobalt.dll");
            var cobaltAssembly = Assembly.LoadFile(cobaltDll);
            var cobaltInitialize = cobaltAssembly.GetType("Cobalt.Loader.CobaltMain")?.GetMethod("Initialize", BindingFlags.Public | BindingFlags.Static);
            if (cobaltInitialize == null)
            {
                throw new PluginInitException(
                    "Cobalt Initialize() method could not be found. Is the Cobalt.dll there and are you using the right version?");
            }
            try
            {
                cobaltInitialize.Invoke(null, null);
            }
            catch (Exception e)
            {
                throw new PluginInitException("Cobalt initialization failed!", e);
            }
            PlayerHooks.PlayerCommand += OnChat;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                PlayerHooks.PlayerCommand -= OnChat;
            }
            base.Dispose(disposing);
        }

        private void OnChat(PlayerCommandEventArgs args)
        {
            args.Handled = Cobalt.Standalone.Manager.CommandManager.HandleCommand(TShockPlayer.Wrap(args.Player), args.CommandName, args.Parameters);
        }
    }
}