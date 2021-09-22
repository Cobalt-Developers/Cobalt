namespace Cobalt.Api.Wrapper
{
    public interface ICobaltPlayer
    {
        string DisplayName { get; }
        
        ICobaltPosition Position { get; }
        
        void SendMessage(string msg);
        void SendErrorMessage(string msg);

        bool HasPermission(string permission);

        void Teleport(ICobaltPosition pos);

        void Teleport(ICobaltPlayer player);
    }
}