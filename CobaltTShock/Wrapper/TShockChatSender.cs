using System.Collections.Generic;
using Cobalt.Api.Model;
using TShockAPI;

namespace CobaltTShock.Wrapper
{
    public class TShockChatSender : WrappedEntity, IChatSender
    {
        protected TSPlayer SrcInstance { get; }
        public string DisplayName => SrcInstance.Name;
        
        public TShockChatSender(TSPlayer srcInstance)
        {
            SrcInstance = srcInstance;
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
        
        protected override Dictionary<object, object> GetPrintableVariables()
        {
            return new Dictionary<object, object>
            {
                {"name", DisplayName}
            };
        }
    }
}