using System;
using System.Collections.Generic;
using System.IO;
using Cobalt.Api.Attributes;
using Cobalt.Api.Exceptions;

namespace Cobalt.Api.Storages.Settings
{
    public class SettingsManager<T> : ISettingsManager
    {
        private ICobaltPlugin Plugin { get; }
        
        private string _name;
        private string _settingsDir;
        private FileStorageType _storageType;
        
        private SettingsFile<T> _default;
        
        private Dictionary<string, IStorageFile> _settings;

        public SettingsManager(ICobaltPlugin plugin)
        {
            Plugin = plugin;
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
            var attribute = (FileStorageAttribute) Attribute.GetCustomAttribute(typeof(T), typeof(FileStorageAttribute));
            if (attribute == null)
            {
                throw new StorageInitException($"SettingsFile {typeof(T).Name} has no FileStorageAttribute");
            }

            _settingsDir = Path.Combine(Plugin.DataFolder, "settings");
            _name = attribute.Name;
            _storageType = attribute.Type;
        }

        private void InitDefault()
        {
            _default = SettingsFile<T>.Create<T>(_settingsDir, "_default", _storageType);
        }

        private void LoadSettings()
        {
            _settings = new Dictionary<string, IStorageFile>();
            
            DirectoryInfo d = new DirectoryInfo(_settingsDir);
            FileInfo[] files = d.GetFiles($"*{_storageType.GetFileEnding()}");
            
            foreach(FileInfo file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file.Name);
                if (name.Equals("_default", StringComparison.OrdinalIgnoreCase)) continue;
                
                SettingsFile<T> settingsFile = SettingsFile<T>.Create<T>(_settingsDir, name, _storageType);
                _settings.Add(name, settingsFile);
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

        public SettingsFile<T> GetSettings(string id)
        {
            if (id == null) throw new NullReferenceException("SettingsFile identifier cannot be null");
            return (SettingsFile<T>) (!_settings.ContainsKey(id) ? null : _settings[id]);
        }

        public SettingsFile<T> GetOrCreateSettings(string id)
        {
            SettingsFile<T> settingsFile = GetSettings(id);
            if (settingsFile != null) return settingsFile;

            settingsFile = SettingsFile<T>.Create<T>(_settingsDir, id, _storageType);
            _settings.Add(id, settingsFile);
            return settingsFile;
        }
    }
}