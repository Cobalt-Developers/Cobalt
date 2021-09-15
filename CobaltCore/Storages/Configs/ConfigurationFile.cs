using System;
using System.Data.SqlTypes;
using System.IO;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using YamlDotNet.Serialization;

namespace CobaltCore.Storages.Configs
{
    public class ConfigurationFile<T> : StorageFile<T>
    {
        protected ConfigurationFile(string dataFolder, string name, FileStorageType storageType) : base(dataFolder, name, storageType)
        {
        }

        public static ConfigurationFile<T> Create(CobaltPlugin plugin)
        {
            var attribute = (FileStorageAttribute) Attribute.GetCustomAttribute(typeof(T), typeof(FileStorageAttribute));
            if (attribute == null)
            {
                throw new StorageInitException($"ConfigurationFile {typeof(T).Name} has no FileStorageAttribute");
            }

            var dataFolder = plugin.GetDataFolderPath();
            var name = attribute.Name;
            var storageType = attribute.Type;

            return new ConfigurationFile<T>(dataFolder, name, storageType);
        }
    }
}