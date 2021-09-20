using System;
using Cobalt.Api.Storages;

namespace Cobalt.Api.Attributes
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