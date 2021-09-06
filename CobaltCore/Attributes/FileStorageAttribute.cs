using System;
using CobaltCore.Storages.Configs;

namespace CobaltCore.Attributes
{
    public class FileStorageAttribute : Attribute
    {
        public string Name { get; }
        public FileStorageType Type { get; }

        public FileStorageAttribute(string name, FileStorageType type)
        {
            Name = name;
            Type = type;
        }
    }
}