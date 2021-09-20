using System;

namespace Cobalt.Api.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SubCommandAttribute : System.Attribute
    {
        public string[] Names { get; }

        public SubCommandAttribute(params string[] names)
        {
            Names = names;
        }
    }
}