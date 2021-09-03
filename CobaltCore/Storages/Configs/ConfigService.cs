using System;
using System.Collections.Generic;
using System.IO;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using CobaltCore.Messages;
using CobaltCore.Services;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CobaltCore.Storages.Configs
{
    public class ConfigService : AbstractService
    {
        private Dictionary<Type, Configuration> configFiles;

        public ConfigService(CobaltPlugin plugin) : base(plugin)
        {
        }

        public override void Init()
        {
            CreateDataFolder();
            
            configFiles = new Dictionary<Type, Configuration>();
            
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

        public Configuration GetConfig<T>() where T : ConfigurationFile
        {
            return GetConfig(typeof(T));
        }
        
        public Configuration GetConfig(Type configType)
        {
            if (!configType.IsSubclassOf(typeof(ConfigurationFile)))
            {
                throw new InvalidClassTypeException(configType, typeof(ConfigurationFile));
            }
            if (IsConfigExisting(configType))
            {
                return null;
            }
            return configFiles[configType];
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
            RegisterConfig(configType, Configuration.Create(Plugin, configType));
        }

        private void RegisterConfig(Type configType, Configuration configFile)
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