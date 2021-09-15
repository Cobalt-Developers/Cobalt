using System;
using System.Collections.Generic;
using System.IO;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using CobaltCore.Messages;
using CobaltCore.Storages;
using CobaltCore.Storages.Configs;

namespace CobaltCore.Services
{
    public class ConfigService : AbstractService
    {
        private Dictionary<Type, IStorageFile> _configFiles;

        public ConfigService(CobaltPlugin plugin) : base(plugin)
        {
        }

        public override void Init()
        {
            CreateDataFolder();
            
            _configFiles = new Dictionary<Type, IStorageFile>();
            
            ConfigurationAttribute[] attributes = (ConfigurationAttribute[]) Attribute.GetCustomAttributes(Plugin.GetType(), typeof(ConfigurationAttribute));
            foreach (var attribute in attributes)
            {
                GetType().GetMethod("AddConfig")?.MakeGenericMethod(attribute.ImplType).Invoke(this, null);
            }
        }

        public override void Reload()
        {
            Init();
        }

        /*
         * Initialization
         */
        
        public void AddConfig<T>()
        {
            try
            {
                RegisterConfig(ConfigurationFile<T>.Create(Plugin));
            }
            catch (StorageInitException e)
            {
                Plugin.Log(LogLevel.VERBOSE, "ConfigurationFile initialization failed:");
                Plugin.Log(LogLevel.VERBOSE, e.Message);
            }
        }

        private void RegisterConfig<T>(ConfigurationFile<T> config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            _configFiles.Add(typeof(T), config);
        }

        private void CreateDataFolder()
        {
            string path = Plugin.GetDataFolderPath();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        
        /*
         * Interface Functions
         */
        
        public ConfigurationFile<T> GetConfig<T>()
        {
            if (!IsConfigExisting<T>()) return default;
            return (ConfigurationFile<T>) _configFiles[typeof(T)];
        }

        public bool IsConfigExisting<T>()
        {
            return _configFiles.ContainsKey(typeof(T));
        }
    }
}