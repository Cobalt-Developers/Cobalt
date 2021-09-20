using System;
using System.Collections.Generic;
using System.IO;
using Cobalt.Api.Attribute;
using Cobalt.Api.Exception;
using Cobalt.Api.Message;
using Cobalt.Api.Storage;
using Cobalt.Api.Storage.Config;

namespace Cobalt.Api.Service
{
    public class ConfigService : AbstractService
    {
        private Dictionary<Type, IStorageFile> _configFiles;

        public ConfigService(ICobaltPlugin plugin) : base(plugin)
        {
        }

        public override void Init()
        {
            CreateDataFolder();
            
            _configFiles = new Dictionary<Type, IStorageFile>();
            
            ConfigurationAttribute[] attributes = (ConfigurationAttribute[]) System.Attribute.GetCustomAttributes(Plugin.GetType(), typeof(ConfigurationAttribute));
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
            string path = Plugin.DataFolder;
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