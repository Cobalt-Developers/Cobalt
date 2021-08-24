using System;

namespace CobaltCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CommandHandler : System.Attribute
    {
        public string[] Commands { get; }
        public Type[] Handlers { get; } // TODO: restrict to only CommandHandlers

        public CommandHandler(string[] commands, params Type[] handlers)
        {
            Commands = commands;
            Handlers = handlers;
        }
    }
}