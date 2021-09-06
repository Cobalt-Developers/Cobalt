﻿using System;

namespace CobaltCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ConfigurationAttribute : Attribute
    {
        public Type ImplType { get; }

        public ConfigurationAttribute(Type implType)
        {
            ImplType = implType;
        }
    }
}