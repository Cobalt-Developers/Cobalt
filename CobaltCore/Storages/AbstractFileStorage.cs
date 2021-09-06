using System;
using System.IO;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using CobaltCore.Storages.Configs;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CobaltCore.Storages
{
    public abstract class AbstractFileStorage
    {
        [YamlIgnore]
        private string DataFolder { get; set; }
        
        [YamlIgnore]
        private string Name { get; set; }
        
        [YamlIgnore]
        private FileStorageType StorageType { get; set; }

        protected AbstractFileStorage(string dataFolder, string name, FileStorageType storageType)
        {
            DataFolder = dataFolder;
            Name = name;
            StorageType = storageType;
        }

        /*
         * Initialization
         */
        
        protected static AbstractFileStorage Create(Type implType, string dataFolder, string name, FileStorageType storageType)
        {
            if (!implType.IsSubclassOf(typeof(AbstractFileStorage)))
            {
                throw new InvalidClassTypeException(implType, typeof(AbstractFileStorage));
            }

            AbstractFileStorage fileStorage;
            
            var filePath = Path.Combine(dataFolder, name + storageType.GetFileEnding());
            if (!File.Exists(filePath))
            {
                // Create new File and save it
                try
                {
                    fileStorage = (AbstractFileStorage) Activator.CreateInstance(implType, dataFolder, name, storageType);
                    fileStorage.Save();
                }
                catch (Exception e)
                {
                    throw new StorageInitException($"Could not write Settings {implType.Name}", e);
                }
            }
            else
            {
                // Read existing File
                try
                {
                    var yaml = File.ReadAllText(filePath);
                    var deserializer = new DeserializerBuilder().WithNamingConvention(HyphenatedNamingConvention.Instance).Build();
                    fileStorage = (AbstractFileStorage) deserializer.Deserialize(yaml, implType);
                    if (fileStorage == null)
                    {
                        throw new StorageInitException($"Creation of Settings {implType.Name} failed");
                    }
                    fileStorage.DataFolder = dataFolder;
                    fileStorage.Name = name;
                    fileStorage.StorageType = storageType;
                }
                catch (Exception e)
                {
                    throw new StorageInitException($"Could not read Settings {implType.Name}", e);
                }
            }

            return fileStorage;
        }

        /*
         * Interface Functions
         */
        
        public void Save()
        {
            using (StreamWriter sw = File.CreateText(GetFilePath()))
            {
                var serializer = new SerializerBuilder().WithNamingConvention(HyphenatedNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(this);
                sw.Write(yaml);
            }
        }

        public string GetFilePath()
        {
            return Path.Combine(DataFolder, Name + StorageType.GetFileEnding());
        }
    }
}