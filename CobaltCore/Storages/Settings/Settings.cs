using System;
using System.IO;
using System.Runtime.CompilerServices;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using CobaltCore.Storages.Configs;
using TShockAPI;

namespace CobaltCore.Storages.Settings
{
    public abstract class Settings : AbstractFileStorage
    {
        protected Settings(string dataFolder, string name, FileStorageType storageType) : base(dataFolder, name, storageType)
        {
        }
        
        public new static Settings Create(Type implType, string dataFolder, string name, FileStorageType storageType)
        {
            if (!implType.IsSubclassOf(typeof(Settings)))
            {
                throw new InvalidClassTypeException(implType, typeof(Settings));
            }
            
            return (Settings) AbstractFileStorage.Create(implType, dataFolder, name, storageType);
        }
    }
}