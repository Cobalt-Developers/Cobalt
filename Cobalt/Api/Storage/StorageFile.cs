using System;
using System.IO;
using Cobalt.Api.Exception;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Cobalt.Api.Storage
{
    public abstract class StorageFile<T> : IStorageFile
    {
        public string DataFolder { get; }
        public string Name { get; }
        public FileStorageType StorageType { get; }
        public string FilePath => Path.Combine(DataFolder, Name + StorageType.GetFileEnding());
        
        public T Content { get; protected set; }

        protected StorageFile(string dataFolder, string name, FileStorageType storageType)
        {
            DataFolder = dataFolder;
            Name = name;
            StorageType = storageType;
            
            InitContent();
        }

        /*
         * Initialization
         */

        private void InitContent()
        {
            if (!File.Exists(FilePath))
            {
                try
                {
                    InitContentFromNewInstance();
                    Save();
                }
                catch (System.Exception e)
                {
                    throw new StorageInitException($"Could not create StorageFile {GetType().Name} from new instance", e);
                }
            }
            else
            {
                try
                {
                    InitContentFromDisk();
                }
                catch (System.Exception e)
                {
                    throw new StorageInitException($"Could not create StorageFile {GetType().Name} from disk", e);
                }
            }
        }

        protected virtual void InitContentFromNewInstance()
        {
            Content = Activator.CreateInstance<T>();
        }

        protected virtual void InitContentFromDisk()
        {
            Content = ReadContentFromDisk(FilePath);
        }
        
        protected virtual T ReadContentFromDisk(string filePath)
        {
            var yaml = File.ReadAllText(filePath);
            var deserializer = new DeserializerBuilder().WithNamingConvention(HyphenatedNamingConvention.Instance).Build();
            var instance = deserializer.Deserialize<T>(yaml);
            if (instance == null)
            {
                throw new StorageInitException($"Could not read {filePath}");
            }

            return instance;
        }

        /*
         * Interface Functions
         */
        
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