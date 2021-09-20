using System;

namespace Cobalt.Api.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ServiceAttribute : System.Attribute
    {
        public Type Value { get; }

        public ServiceAttribute(Type value)
        {
            this.Value = value;
        }
    }
}