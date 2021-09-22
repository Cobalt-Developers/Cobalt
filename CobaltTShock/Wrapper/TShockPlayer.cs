using System.Collections.Generic;
using Cobalt.Api.Wrapper;
using TShockAPI;

namespace CobaltTShock.Wrapper
{
    public class TShockPlayer : CobaltPlayer
    {
        private TSPlayer SrcInstance { get; }
        public override string DisplayName => SrcInstance.Name;
        public override CobaltPosition Position => new TShockPosition(SrcInstance.X, SrcInstance.X);

        public TShockPlayer(TSPlayer srcInstance)
        {
            SrcInstance = srcInstance;
        }

        public static TShockPlayer Wrap(TSPlayer src)
        {
            return new TShockPlayer(src);
        }
        
        public override void SendMessage(string msg)
        {
            SrcInstance.SendInfoMessage(msg);
        }

        public override void SendErrorMessage(string msg)
        {
            SrcInstance.SendErrorMessage(msg);
        }

        public override bool HasPermission(string permission)
        {
            return SrcInstance.HasPermission(permission);
        }

        public override void Teleport(CobaltPosition pos)
        {
            var realPos = (TShockPosition) pos;
            SrcInstance.Teleport(realPos.X, realPos.Y);
        }

        public override void Teleport(CobaltPlayer player)
        {
            var realPos = (TShockPosition) player.Position;
            SrcInstance.Teleport(realPos.X, realPos.Y);
        }

        protected override Dictionary<object, object> GetPrintableVariables()
        {
            return new Dictionary<object, object>
            {
                {"name", DisplayName}
            };
        }
    }
}