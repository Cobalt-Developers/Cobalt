using System.IO;

namespace Cobalt.Api.Storages.Settings
{
    public class SettingsFile<T> : StorageFile<T>
    {
        private SettingsFile(string dataFolder, string name, FileStorageType storageType) : base(dataFolder, name, storageType)
        {
        }

        public static SettingsFile<TS> Create<TS>(string dataFolder, string name, FileStorageType storageType)
        {
            return new SettingsFile<TS>(dataFolder, name, storageType);
        }

        protected override void InitContentFromNewInstance()
        {
            // Overide to use _default.yml values
            Content = ReadContentFromDisk(Path.Combine(DataFolder, $"_default{StorageType.GetFileEnding()}"));
        }
    }
}