using System;
using CobaltCore.Commands;
using CobaltCore.Exceptions;
using CobaltCore.Messages;
using CobaltCore.Services;
using CobaltCore.Services.Commands;
using Terraria;
using TerrariaApi.Server;

namespace CobaltCore
{
    public abstract class CobaltPlugin: TerrariaPlugin
    {
        public ServiceManager ServiceManager { get; private set; }
        
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

            ServiceManager = new ServiceManager(this);
            try
            {
                ServiceManager.RegisterService<CommandService>();
            }
            catch (Exception e)
            {
                if (!(e is ServiceInitException) && !(e is ServiceAlreadyExistsException)) throw;
                Log(LogLevel.VERBOSE, "Loading services failed");
                Disable(e);
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
            // TODO: implement
            throw exception;
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

        /**
         * Services
         */

        public CommandService GetCommandService()
        {
            return (CommandService) ServiceManager.GetService<CommandService>();
        }
    }
}