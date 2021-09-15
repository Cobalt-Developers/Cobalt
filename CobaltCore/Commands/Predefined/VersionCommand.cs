﻿using System.Collections.Generic;
using CobaltCore.Attributes;
using CobaltCore.Wrappers;

namespace CobaltCore.Commands.Predefined
{
    [Description("Displays info about the plugin")]
    [SubCommand("version", "v")]
    public class VersionCommand: AbstractCommand
    {
        public VersionCommand(ICobaltPlugin plugin, AbstractCommandManager manager) : base(plugin, manager)
        {
        }

        public override void Execute(ICobaltPlayer player, List<string> args, string message, bool silent)
        {
            player.SendMessage($"You are running {Plugin.Name} version {Plugin.Version} by {Plugin.Author}.");
            player.SendMessage($"{Plugin.Description}");
        }
    }
}