﻿using System;

namespace CobaltCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SubCommand : System.Attribute
    {
        public string[] Names { get; }

        public SubCommand(string[] names)
        {
            Names = names;
        }
    }
}