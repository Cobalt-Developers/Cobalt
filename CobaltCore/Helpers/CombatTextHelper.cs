using System;
using Microsoft.Xna.Framework;
using TShockAPI;

namespace CobaltCore.Helpers
{
    public class CombatTextHelper
    {
        public static void CreateCombatText(TSPlayer player, string message, uint color = 4294967295)
        {
            CreateCombatText(player, message, new Vector2(player.X, player.Y), color);
        }

        public static void CreateCombatText(TSPlayer player, string message, Vector2 pos, uint color = 4294967295)
        {
            player.SendData(PacketTypes.CreateCombatTextExtended, message, (int) color, pos.X, pos.Y);
        }
    }
}