namespace Cobalt.Api.Wrapper
{
    public abstract class CobaltPlayer : WrappedEntity
    {
        public abstract string DisplayName { get; }
        
        public abstract CobaltPosition Position { get; }
        
        public abstract void SendMessage(string msg);
        public abstract void SendErrorMessage(string msg);

        public abstract bool HasPermission(string permission);

        public abstract void Teleport(CobaltPosition pos);

        public abstract void Teleport(CobaltPlayer player);
    }
}