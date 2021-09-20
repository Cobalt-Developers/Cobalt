﻿using System.Collections.Generic;
using Cobalt.Api.Wrappers;

namespace Cobalt.Api.Commands
{
    public abstract class AbstractCommandManager
    {
        protected ICobaltPlugin Plugin { get; }

        protected AbstractCommandManager(ICobaltPlugin plugin)
        {
            Plugin = plugin;
        }

        public abstract void OnCommand(ICobaltPlayer player, List<string> args);

        public abstract string[] GetBaseCommands();

        public abstract AbstractCommand[] GetCommands();
    }
}