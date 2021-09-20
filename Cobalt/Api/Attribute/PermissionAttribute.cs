using System;

namespace Cobalt.Api.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PermissionAttribute : System.Attribute
    {
        public string Name { get; }

        public PermissionAttribute(string name)
        {
            Name = name;
        }
    }
}