using System;
using System.IO;
using Cobalt.Api;
using Cobalt.Api.Exception;
using Cobalt.Api.Message;
using Cobalt.Api.Service;
using CobaltTShock.Service;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace CobaltTShock
{
    public abstract class CobaltTShockPlugin: TerrariaPlugin, ICobaltPlugin
    {
        public string PluginPrefix => $"[{Name}]";

        public string DataFolder => Path.Combine(TShock.SavePath, Name);
        public ServiceManager ServiceManager { get; }
        
        protected CobaltTShockPlugin(Main game) : base(game)
        {
            ServiceManager = new ServiceManager(this);
        }

        public override void Initialize()
        {
            try {
                PreEnable();
            } catch (Exception e) {
                Disable(e);
                return;
            }

            try
            {
                ServiceManager.RegisterService<ConfigService>();
                ServiceManager.RegisterService<SettingsService>();
                ServiceManager.RegisterService<CommandService>();
                ServiceManager.RegisterCustomServices();
            }
            catch (Exception e)
            {
                if (!(e is ServiceInitException) && !(e is ServiceAlreadyExistsException)) throw;
                Log(LogLevel.VERBOSE, "Loading services failed");
                Disable(e);
                return;
            }
            
            try {
                PostEnable();
            } catch (Exception e) {
                Disable(e);
            }
        }

        public virtual void PreEnable()
        {
        }
        
        public virtual void PostEnable()
        {
        }
        
        public void Disable(Exception exception)
        {
            Log(LogLevel.VERBOSE, "Disabling plugin with the following exception:");
            Log(LogLevel.VERBOSE, exception.ToString());
            //ToDo disable
            Enabled = false;
        }
        
        public void Disable()
        {
            Log(LogLevel.VERBOSE, "Disabling plugin...");
            Enabled = false;
        }

        public void Log(String message)
        {
            Log(LogLevel.INFO, message);
        }
        
        public void Log(LogLevel level, String message)
        {
            ConsoleColor color;
            switch (level)
            {
                case LogLevel.WARNING:
                    color = ConsoleColor.Yellow;
                    break;
                case LogLevel.VERBOSE:
                    color = ConsoleColor.Red;
                    break;
                case LogLevel.INFO:
                    color = ConsoleColor.White;
                    break;
                default:
                    color = ConsoleColor.White;
                    break;
            }
            Console.ForegroundColor = color;
            
            Console.WriteLine($"{PluginPrefix} {message}");
            Console.ResetColor();
        }

        /**
         * Services
         */

        public ConfigService GetConfigService()
        {
            return (ConfigService) ServiceManager.GetService<ConfigService>();
        }
        
        public SettingsService GetSettingsService()
        {
            return (SettingsService) ServiceManager.GetService<SettingsService>();
        }
        
        public AbstractCommandService GetCommandService()
        {
            return (AbstractCommandService) ServiceManager.GetService<CommandService>();
        }
    }
}