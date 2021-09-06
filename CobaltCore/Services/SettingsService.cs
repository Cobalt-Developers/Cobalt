using System;
using System.Collections.Generic;
using System.IO;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using CobaltCore.Storages.Settings;
using TShockAPI;

namespace CobaltCore.Services
{
    public class SettingsService : AbstractService
    {
        private Dictionary<Type, SettingsManager> _managers;

        public SettingsService(CobaltPlugin plugin) : base(plugin)
        {
        }

        public override void Init()
        {
            LoadSettings();
        }

        public override void Reload()
        {
            Init();
        }

        /*
         * Initialization
         */
        
        private void LoadSettings()
        {
            _managers = new Dictionary<Type, SettingsManager>();
            
            SettingsAttribute[] attributes = (SettingsAttribute[]) Attribute.GetCustomAttributes(Plugin.GetType(), typeof(SettingsAttribute));
            foreach (var attribute in attributes)
            {
                AddSettingsManager(attribute.ImplType);
            }
        }

        public void AddSettingsManager(Type implType)
        {
            var manager = new SettingsManager(Plugin, implType);
            _managers.Add(implType, manager);
        }

        /*
         * Interface Functions
         */

        public SettingsManager GetSettingsManager<T>() where T : Settings
        {
            if (!_managers.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"Settings {typeof(T).Name} not registered; please add it to your plugin");
            }
            return _managers[typeof(T)];
        }

        public T GetSettings<T>(string id) where T : Settings
        {
            return (T) GetSettingsManager<T>().GetSettings(id);
        }
        
        public T GetOrCreateSettings<T>(string id) where T : Settings
        {
            return (T) GetSettingsManager<T>().GetOrCreateSettings(id);
        }
        
        public T GetSettings<T>(TSPlayer player) where T : Settings
        {
            return GetSettings<T>(player.Account.Name);
        }
        
        public T GetOrCreateSettings<T>(TSPlayer player) where T : Settings
        {
            return GetOrCreateSettings<T>(player.Account.Name);
        }
    }
}