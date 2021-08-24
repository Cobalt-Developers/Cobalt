using System;

namespace CobaltCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class Permission : System.Attribute
    {
        public string Name { get; }

        public Permission(string name)
        {
            Name = name;
        }
    }
}