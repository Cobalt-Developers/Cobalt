namespace Cobalt.Api.Model
{
    public interface IChatSender : IPrettyPrintable
    {
        string DisplayName { get; }
        
        void SendMessage(string msg);
        
        void SendErrorMessage(string msg);
        
        bool HasPermission(string permission);
    }
}