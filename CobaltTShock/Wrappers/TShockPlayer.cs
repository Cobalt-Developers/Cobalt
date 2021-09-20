using Cobalt.Api.Wrappers;
using TShockAPI;

namespace CobaltTShock.Wrappers
{
    public class TShockPlayer : ICobaltPlayer
    {
        public TSPlayer SrcInstance { get; }
        public string DisplayName { get; }

        public TShockPlayer(TSPlayer srcInstance)
        {
            SrcInstance = srcInstance;
        }

        public static TShockPlayer Wrap(TSPlayer src)
        {
            return new TShockPlayer(src);
        }
        
        public void SendMessage(string msg)
        {
            SrcInstance.SendInfoMessage(msg);
        }

        public void SendErrorMessage(string msg)
        {
            SrcInstance.SendErrorMessage(msg);
        }

        public bool HasPermission(string permission)
        {
            return SrcInstance.HasPermission(permission);
        }
    }
}