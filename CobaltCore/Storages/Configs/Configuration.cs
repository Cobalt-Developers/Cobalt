using System;
using System.IO;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using YamlDotNet.Serialization;

namespace CobaltCore.Storages.Configs
{
    public abstract class Configuration : AbstractFileStorage
    {
        protected Configuration(string dataFolder, string name, FileStorageType storageType) : base(dataFolder, name, storageType)
        {
        }
        
        public static Configuration Create(CobaltPlugin plugin, Type implType)
        {
            if (!implType.IsSubclassOf(typeof(Configuration)))
            {
                throw new InvalidClassTypeException(implType, typeof(Configuration));
            }
            
            var attribute = (FileStorageAttribute) Attribute.GetCustomAttribute(implType, typeof(FileStorageAttribute));
            if (attribute == null)
            {
                throw new StorageInitException($"Configuration {implType.Name} has no FileStorageAttribute");
            }

            var dataFolder = plugin.GetDataFolderPath();
            var name = attribute.Name;
            var storageType = attribute.Type;

            return (Configuration) Create(implType, dataFolder, name, storageType);
        }
    }
}