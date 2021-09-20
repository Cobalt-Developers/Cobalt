using Cobalt.Api.Storage;

namespace Cobalt.Api.Attribute
{
    public class FileStorageAttribute : System.Attribute
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