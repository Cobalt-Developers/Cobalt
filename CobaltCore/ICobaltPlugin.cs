using System;
using CobaltCore.Messages;
using CobaltCore.Services;

namespace CobaltCore
{
    public interface ICobaltPlugin
    {
        
        string Author { get; }
        string Description { get; }
        string Name { get; }
        Version Version { get; }
        string PluginPrefix { get; }
        string DataFolder { get; }
        
        ServiceManager ServiceManager { get; }

        void PreEnable();
        void Initialize();
        void PostEnable();
        void Disable(Exception exception);
        void Disable();

        void Log(String message);
        void Log(LogLevel level, String message);


        /**
         * Services
         */

        ConfigService GetConfigService();
        SettingsService GetSettingsService();
        AbstractCommandService GetCommandService();
    }
}