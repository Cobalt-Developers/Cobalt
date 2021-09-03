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
        private Dictionary<Type, ConfigurationFile> configFiles;

        public ConfigService(CobaltPlugin plugin) : base(plugin)
        {
        }

        public override void Init()
        {
            CreateDataFolder();
            
            configFiles = new Dictionary<Type, ConfigurationFile>();
            
            ConfigurationAttribute[] attributes = (ConfigurationAttribute[]) Attribute.GetCustomAttributes(Plugin.GetType(), typeof(ConfigurationAttribute));
            foreach (var attribute in attributes)
            {
                AddConfig(attribute.ConfigType);
            }
        }

        public override void Reload()
        {
            Init();
        }

        public T GetConfig<T>() where T : ConfigurationFile
        {
            return (T) GetConfig(typeof(T));
        }
        
        public ConfigurationFile GetConfig(Type configType)
        {
            if (!configType.IsSubclassOf(typeof(ConfigurationFile)))
            {
                throw new InvalidClassTypeException(configType, typeof(ConfigurationFile));
            }
            return !IsConfigExisting(configType) ? null : configFiles[configType];
        }

        public bool IsConfigExisting<T>() where T : ConfigurationFile
        {
            return IsConfigExisting(typeof(T));
        }
        
        public bool IsConfigExisting(Type configType)
        {
            if (!configType.IsSubclassOf(typeof(ConfigurationFile)))
            {
                throw new InvalidClassTypeException(configType, typeof(ConfigurationFile));
            }
            
            return configFiles.ContainsKey(configType);
        }

        public void AddConfig(Type configType)
        {
            try
            {
                RegisterConfig(configType, ConfigurationFile.Create(Plugin, configType));
            }
            catch (ConfigInitException e)
            {
                Plugin.Log(LogLevel.VERBOSE, "Configuration initialization failed:");
                Plugin.Log(LogLevel.VERBOSE, e.Message);
            }
        }

        private void RegisterConfig(Type configType, ConfigurationFile configFile)
        {
            configFiles.Add(configType, configFile);
        }

        private void CreateDataFolder()
        {
            string path = Plugin.GetDataFolderPath();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}