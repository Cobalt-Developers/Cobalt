﻿using System;

namespace Cobalt.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CommandHandlerAttribute : Attribute
    {
        public string[] Commands { get; }
        public Type[] Handlers { get; } // TODO: restrict to only CommandHandlers

        public CommandHandlerAttribute(string[] commands, params Type[] handlers)
        {
            Commands = commands;
            Handlers = handlers;
        }
    }
}