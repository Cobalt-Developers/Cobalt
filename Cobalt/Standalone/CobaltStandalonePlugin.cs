using System;
using System.IO;
using Cobalt.Api;
using Cobalt.Api.Exceptions;
using Cobalt.Api.Messages;
using Cobalt.Api.Services;
using Cobalt.Loader;
using Cobalt.Standalone.Service;

namespace Cobalt.Standalone
{
    public abstract class CobaltStandalonePlugin : ICobaltPlugin
    {
        public abstract string Author { get; }
        public abstract string Description { get; }
        public abstract string Name { get; }
        public abstract Version Version { get; }
        
        public string PluginPrefix => $"[{Name}]";
        public string DataFolder => Path.Combine(CobaltMain.DataFolder, Name);
        
        public ServiceManager ServiceManager { get; }
        public bool Enabled { get; private set;  }
        

        protected CobaltStandalonePlugin()
        {
            ServiceManager = new ServiceManager(this);
        }

        public void Initialize()
        {
            try {
                PreEnable();
            } catch (System.Exception e) {
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
            catch (System.Exception e)
            {
                if (!(e is ServiceInitException) && !(e is ServiceAlreadyExistsException)) throw;
                Log(LogLevel.VERBOSE, "Loading services failed");
                Disable(e);
                return;
            }
            
            try {
                PostEnable();
            } catch (System.Exception e) {
                Disable(e);
            }
        }

        public virtual void PreEnable()
        {
        }
        
        public virtual void PostEnable()
        {
        }
        
        public void Disable(System.Exception exception)
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
            return (CommandService) ServiceManager.GetService<CommandService>();
        }
    }
}