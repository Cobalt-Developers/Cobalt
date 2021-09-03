using System;
using System.IO;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CobaltCore.Storages.Configs
{
    public class Configuration
    {
        public string FilePath { get; }
        public ConfigurationFile Content { get; private set; }

        public Configuration(CobaltPlugin plugin, string filePath)
        {
            FilePath = filePath;
        }

        public static Configuration Create(CobaltPlugin plugin, Type configType)
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

            // Create Instance
            Configuration config = new Configuration(plugin, filePath);
            
            // Initialize Content
            config.InitContent(configType);
            
            // Create Instance
            return config;
        }

        private void InitContent(Type configType)
        {
            if (!File.Exists(FilePath))
            {
                try
                {
                    Content = (ConfigurationFile) Activator.CreateInstance(configType);
                    Save();
                }
                catch (Exception e)
                {
                    throw new ConfigInitException($"Could not write ConfigurationFile {configType.Name}", e);
                }
            }
            else
            {
                try
                {
                    var yaml = File.ReadAllText(FilePath);
                    var deserializer = new DeserializerBuilder().WithNamingConvention(HyphenatedNamingConvention.Instance).Build();
                    Content = (ConfigurationFile) deserializer.Deserialize(yaml, configType);
                }
                catch (Exception e)
                {
                    throw new ConfigInitException($"Could not read ConfigurationFile {configType.Name}", e);
                }
            }
            if (Content == null)
            {
                throw new ConfigInitException($"Creation of ConfigurationFile {configType.Name} failed");
            }
        }

        public void Save()
        {
            using (StreamWriter sw = File.CreateText(FilePath))
            {
                var serializer = new SerializerBuilder().WithNamingConvention(HyphenatedNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(Content);
                sw.Write(yaml);
            }
        }
    }
}