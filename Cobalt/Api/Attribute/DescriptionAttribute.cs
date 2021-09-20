using System;

namespace Cobalt.Api.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DescriptionAttribute : System.Attribute
    {
        public string Description { get; }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}