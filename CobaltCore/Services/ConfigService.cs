using System;
using System.Collections.Generic;
using System.IO;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using CobaltCore.Messages;
using CobaltCore.Services;
using CobaltCore.Storages.Configs;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CobaltCore.Services
{
    public class ConfigService : AbstractService
    {
        private Dictionary<Type, Configuration> _configFiles;

        public ConfigService(CobaltPlugin plugin) : base(plugin)
        {
        }

        public override void Init()
        {
            CreateDataFolder();
            
            _configFiles = new Dictionary<Type, Configuration>();
            
            ConfigurationAttribute[] attributes = (ConfigurationAttribute[]) Attribute.GetCustomAttributes(Plugin.GetType(), typeof(ConfigurationAttribute));
            foreach (var attribute in attributes)
            {
                AddConfig(attribute.ImplType);
            }
        }

        public override void Reload()
        {
            Init();
        }

        /*
         * Initialization
         */
        
        public void AddConfig(Type configType)
        {
            try
            {
                RegisterConfig(configType, Configuration.Create(Plugin, configType));
            }
            catch (StorageInitException e)
            {
                Plugin.Log(LogLevel.VERBOSE, "Configuration initialization failed:");
                Plugin.Log(LogLevel.VERBOSE, e.Message);
            }
        }

        private void RegisterConfig(Type configType, Configuration config)
        {
            _configFiles.Add(configType, config);
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
        
        public T GetConfig<T>() where T : Configuration
        {
            return (T) GetConfig(typeof(T));
        }
        
        public Configuration GetConfig(Type configType)
        {
            if (!configType.IsSubclassOf(typeof(Configuration)))
            {
                throw new InvalidClassTypeException(configType, typeof(Configuration));
            }
            return !IsConfigExisting(configType) ? null : _configFiles[configType];
        }

        public bool IsConfigExisting<T>() where T : Configuration
        {
            return IsConfigExisting(typeof(T));
        }
        
        public bool IsConfigExisting(Type configType)
        {
            if (!configType.IsSubclassOf(typeof(Configuration)))
            {
                throw new InvalidClassTypeException(configType, typeof(Configuration));
            }
            
            return _configFiles.ContainsKey(configType);
        }
    }
}