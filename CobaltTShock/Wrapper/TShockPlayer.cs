using System.Collections.Generic;
using Cobalt.Api.Model;
using TShockAPI;

namespace CobaltTShock.Wrapper
{
    public class TShockPlayer : TShockChatSender, IPlayer
    {
        public IPosition Position => new TShockPosition(SrcInstance.X, SrcInstance.X);

        public TShockPlayer(TSPlayer srcInstance) : base(srcInstance)
        {
        }
        
        public void Teleport(IPosition pos)
        {
            var realPos = (TShockPosition) pos;
            SrcInstance.Teleport(realPos.X, realPos.Y);
        }

        public void Teleport(IPlayer player)
        {
            var realPos = (TShockPosition) player.Position;
            SrcInstance.Teleport(realPos.X, realPos.Y);
        }
    }
}