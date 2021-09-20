namespace Cobalt.Api.Wrappers
{
    public interface ICobaltPlayer
    {
        string DisplayName { get; }
        
        void SendMessage(string msg);
        void SendErrorMessage(string msg);

        bool HasPermission(string permission);
    }
}