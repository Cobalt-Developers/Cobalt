using System;
using CobaltCore.Commands.Arguments;

namespace CobaltCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ArgumentAttribute : Attribute
    {
        public string Placeholder { get; }
        public bool Optional { get; }
        public ArgumentConstraint Constraint { get; }

        public ArgumentAttribute(string placeholder, bool optional, Type constraintType, params object[] constraintParams) 
        {
            Placeholder = placeholder;
            Optional = optional;
            Constraint = (ArgumentConstraint) Activator.CreateInstance(constraintType, constraintParams);
        }

        public ArgumentAttribute(string placeholder, bool optional)
        {
            Placeholder = placeholder;
            Optional = optional;
        }

        public ArgumentAttribute(string placeholder, Type constraintType, params object[] constraintParams)
        {
            Placeholder = placeholder;
            Optional = false;
            Constraint = (ArgumentConstraint) Activator.CreateInstance(constraintType, constraintParams);
        }

        public ArgumentAttribute(string placeholder)
        {
            Placeholder = placeholder;
            Optional = false;
        }
    }
}