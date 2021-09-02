﻿using System;

namespace CobaltCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ServiceAttribute : Attribute
    {
        public Type Value { get; }

        public ServiceAttribute(Type value)
        {
            this.Value = value;
        }
    }
}