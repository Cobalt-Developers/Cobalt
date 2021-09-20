using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using TShockAPI;

namespace CobaltTShock.Helper
{
    public class EmoteBubbleHelper
    {
        public static int AttachBubble(int x, int y, int projectile = 444, int emoticon = 0, int time = 360, TSPlayer sendToPlayer = null)
        {
            int id = Projectile.NewProjectile(new ProjectileSource_NPC(Main.npc[0]), new Vector2(x*16+8f, y*16+8f), new Vector2(0, 0), projectile, 0, 0f);
            
            return AttachBubble(Main.projectile[id], emoticon, time, sendToPlayer);
        }
        
        public static int AttachBubble(TSPlayer player, int emoticon = 0, int time = 360, TSPlayer sendToPlayer = null)
        {
            return AttachBubble(player.TPlayer, emoticon, time, sendToPlayer);
        }
        
        public static int AttachBubble(Entity entity, int emoticon = 0, int time = 360, TSPlayer sendToPlayer = null)
        {
            WorldUIAnchor bubbleAnchor = new WorldUIAnchor(entity);
            
            return AttachBubble(bubbleAnchor, emoticon, time, sendToPlayer);
        }
        
        public static int AttachBubble(WorldUIAnchor bubbleAnchor, int emoticon = 0, int time = 360, TSPlayer sendToPlayer = null)
        {
            EmoteBubble emoteBubble = new EmoteBubble(emoticon, bubbleAnchor, time)
            {
                ID = EmoteBubble.AssignNewID()
            };
            EmoteBubble.byID[emoteBubble.ID] = emoteBubble;
            
            if (Main.netMode != 2) return emoteBubble.ID;
            
            Tuple<int, int> tuple = EmoteBubble.SerializeNetAnchor(bubbleAnchor);
            if (sendToPlayer == null)
            {
                NetMessage.SendData((int) PacketTypes.EmoteBubble, number: emoteBubble.ID, number2: tuple.Item1, number3: tuple.Item2, number4: time, number5: emoticon);
            }
            else
            {
                sendToPlayer.SendData(PacketTypes.EmoteBubble, number: emoteBubble.ID, number2: tuple.Item1, number3: tuple.Item2, number4: time, number5: emoticon);
            }
            //EmoteBubble.OnBubbleChange(emoteBubble.ID);
            return emoteBubble.ID;
        }
    }
}