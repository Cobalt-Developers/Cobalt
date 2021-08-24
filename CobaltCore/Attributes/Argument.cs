using System;

namespace CobaltCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class Argument : Attribute
    {
        public string Placeholder { get; }
        public bool Optional { get; }

        public Argument(string placeholder, bool optional)
        {
            Placeholder = placeholder;
            Optional = optional;
        }

        public Argument(string placeholder)
        {
            Placeholder = placeholder;
            Optional = false;
        }
    }
}