using System;
using System.IO;
using System.Xml;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using Terraria.IO;
using TShockAPI;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CobaltCore.Storages.Configs
{
    public abstract class ConfigurationFile
    {
        [YamlIgnore]
        private string FilePath { get; set; }

        public static ConfigurationFile Create(CobaltPlugin plugin, Type configType)
        {
            if (!configType.IsSubclassOf(typeof(ConfigurationFile)))
            {
                throw new InvalidClassTypeException(configType, typeof(ConfigurationFile));
            }
            
            // Set FilePath
            var attribute = (ConfigurationFileAttribute) Attribute.GetCustomAttribute(configType, typeof(ConfigurationFileAttribute));
            if (attribute == null)
            {
                throw new ConfigInitException($"ConfigurationFile {configType.Name} has no ConfigurationFileAttribute");
            }
            var filePath = Path.Combine(plugin.GetDataFolderPath(), attribute.GetFileName());
            
            ConfigurationFile config;
            if (!File.Exists(filePath))
            {
                // Create new File and save it
                try
                {
                    config = (ConfigurationFile) Activator.CreateInstance(configType);
                    config.FilePath = filePath;
                    config.Save();
                }
                catch (Exception e)
                {
                    throw new ConfigInitException($"Could not write ConfigurationFile {configType.Name}", e);
                }
            }
            else
            {
                // Read existing File
                try
                {
                    var yaml = File.ReadAllText(filePath);
                    var deserializer = new DeserializerBuilder().WithNamingConvention(HyphenatedNamingConvention.Instance).Build();
                    config = (ConfigurationFile) deserializer.Deserialize(yaml, configType);
                    if (config == null)
                    {
                        throw new ConfigInitException($"Creation of ConfigurationFile {configType.Name} failed");
                    }
                    config.FilePath = filePath;
                }
                catch (Exception e)
                {
                    throw new ConfigInitException($"Could not read ConfigurationFile {configType.Name}", e);
                }
            }

            return config;
        }

        public void Save()
        {
            using (StreamWriter sw = File.CreateText(FilePath))
            {
                var serializer = new SerializerBuilder().WithNamingConvention(HyphenatedNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(this);
                sw.Write(yaml);
            }
        }
    }
}