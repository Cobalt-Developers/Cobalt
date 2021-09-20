using Cobalt.Api.Attribute;
using Cobalt.Api.Exception;

namespace Cobalt.Api.Storage.Config
{
    public class ConfigurationFile<T> : StorageFile<T>
    {
        protected ConfigurationFile(string dataFolder, string name, FileStorageType storageType) : base(dataFolder, name, storageType)
        {
        }

        public static ConfigurationFile<T> Create(ICobaltPlugin plugin)
        {
            var attribute = (FileStorageAttribute) System.Attribute.GetCustomAttribute(typeof(T), typeof(FileStorageAttribute));
            if (attribute == null)
            {
                throw new StorageInitException($"ConfigurationFile {typeof(T).Name} has no FileStorageAttribute");
            }

            var dataFolder = plugin.DataFolder;
            var name = attribute.Name;
            var storageType = attribute.Type;

            return new ConfigurationFile<T>(dataFolder, name, storageType);
        }
    }
}