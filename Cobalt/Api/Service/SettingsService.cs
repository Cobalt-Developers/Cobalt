using System;
using System.Collections.Generic;
using Cobalt.Api.Attribute;
using Cobalt.Api.Storage.Setting;
using Cobalt.Api.Wrapper;

namespace Cobalt.Api.Service
{
    public class SettingsService : AbstractService
    {
        private Dictionary<Type, ISettingsManager> _managers;

        public SettingsService(ICobaltPlugin plugin) : base(plugin)
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
            _managers = new Dictionary<Type, ISettingsManager>();
            
            SettingsAttribute[] attributes = (SettingsAttribute[]) System.Attribute.GetCustomAttributes(Plugin.GetType(), typeof(SettingsAttribute));
            foreach (var attribute in attributes)
            {
                GetType().GetMethod("AddSettingsManager")?.MakeGenericMethod(attribute.ImplType).Invoke(this, null);
            }
        }

        public void AddSettingsManager<T>()
        {
            var manager = new SettingsManager<T>(Plugin);
            _managers.Add(typeof(T), manager);
        }

        /*
         * Interface Functions
         */

        public SettingsManager<T> GetSettingsManager<T>()
        {
            if (!_managers.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"SettingsFile {typeof(T).Name} not registered; please add it to your plugin");
            }
            return (SettingsManager<T>) _managers[typeof(T)];
        }

        public SettingsFile<T> GetSettings<T>(string id)
        {
            return GetSettingsManager<T>().GetSettings(id);
        }
        
        public SettingsFile<T> GetOrCreateSettings<T>(string id)
        {
            return GetSettingsManager<T>().GetOrCreateSettings(id);
        }
        
        public SettingsFile<T> GetSettings<T>(CobaltPlayer player)
        {
            return GetSettings<T>(player.DisplayName);
        }
        
        public SettingsFile<T> GetOrCreateSettings<T>(CobaltPlayer player)
        {
            return GetOrCreateSettings<T>(player.DisplayName);
        }
    }
}