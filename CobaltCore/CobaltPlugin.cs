using System;
using System.IO;
using CobaltCore.Commands;
using CobaltCore.Exceptions;
using CobaltCore.Messages;
using CobaltCore.Services;
using CobaltCore.Storages.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace CobaltCore
{
    public abstract class CobaltPlugin: TerrariaPlugin
    {
        public ServiceManager ServiceManager { get; private set; }

        public abstract ColorScheme ColorScheme { get; }
        
        protected CobaltPlugin(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            try {
                PreEnable();
            } catch (Exception e) {
                Disable(e);
                return;
            }

            if (ColorScheme == null)
            {
                Log(LogLevel.VERBOSE, "ColorScheme not set! Please define a ColorScheme.");
                Disable();
                return;
            }

            ServiceManager = new ServiceManager(this);
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
            
            Console.WriteLine($"{GetPluginPrefix()} {message}");
            Console.ResetColor();
        }

        public string GetPluginPrefix()
        {
            return $"[{Name}]";
        }

        public string GetDataFolderPath()
        {
            return Path.Combine(TShock.SavePath, Name);
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
        
        public CommandService GetCommandService()
        {
            return (CommandService) ServiceManager.GetService<CommandService>();
        }
    }
}