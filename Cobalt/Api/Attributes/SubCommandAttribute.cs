using System;

namespace Cobalt.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SubCommandAttribute : Attribute
    {
        public string[] Names { get; }

        public SubCommandAttribute(params string[] names)
        {
            Names = names;
        }
    }
}