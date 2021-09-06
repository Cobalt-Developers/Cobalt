using System;
using System.Collections.Generic;
using System.IO;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using CobaltCore.Storages.Configs;
using TerrariaApi.Server;
using TShockAPI;

namespace CobaltCore.Storages.Settings
{
    public class SettingsManager
    {
        private CobaltPlugin Plugin { get; }
        
        private Type _implType;
        private string _name;
        private string _settingsDir;
        private FileStorageType _storageType;
        
        private Settings _default;
        
        private Dictionary<string, Settings> _settings;

        public SettingsManager(CobaltPlugin plugin, Type implType)
        {
            Plugin = plugin;
            _implType = implType;
            SetFileProperties();
            
            CreateDataFolder();
            InitDefault();
            LoadSettings();
        }
        
        /*
         * Initialization
         */
        
        private void SetFileProperties()
        {
            var attribute = (FileStorageAttribute) Attribute.GetCustomAttribute(_implType, typeof(FileStorageAttribute));
            if (attribute == null)
            {
                throw new StorageInitException($"Settings {_implType.Name} has no FileStorageAttribute");
            }

            _settingsDir = Path.Combine(Plugin.GetDataFolderPath(), "settings");
            _name = attribute.Name;
            _storageType = attribute.Type;
        }

        private void InitDefault()
        {
            _default = Settings.Create(_implType, _settingsDir, "_default", _storageType);
        }

        private void LoadSettings()
        {
            _settings = new Dictionary<string, Settings>();
            
            DirectoryInfo d = new DirectoryInfo(_settingsDir);
            FileInfo[] files = d.GetFiles($"*{_storageType.GetFileEnding()}");
            
            foreach(FileInfo file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file.Name);
                if (name.Equals("_default", StringComparison.OrdinalIgnoreCase)) continue;
                
                Settings settings = Settings.Create(_implType, _settingsDir, name, _storageType);
                _settings.Add(name, settings);
            }
        }
        
        private void CreateDataFolder()
        {
            // TODO: potential exception checking?
            
            if (!Directory.Exists(_settingsDir))
            {
                Directory.CreateDirectory(_settingsDir);
            }
            
            var customSettingsDir = Path.Combine(_settingsDir, _name);
            if (!Directory.Exists(customSettingsDir))
            {
                Directory.CreateDirectory(customSettingsDir);
            }

            _settingsDir = customSettingsDir;
        }
        
        /*
         * Interface Functions
         */

        public Settings GetSettings(string id)
        {
            if (id == null) throw new NullReferenceException("Settings identifier cannot be null");
            return !_settings.ContainsKey(id) ? null : _settings[id];
        }

        public Settings GetOrCreateSettings(string id)
        {
            Settings settings = GetSettings(id);
            if (settings != null) return settings;

            settings = Settings.Create(_implType, _settingsDir, id, _storageType);
            _settings.Add(id, settings);
            return settings;
        }
    }
}