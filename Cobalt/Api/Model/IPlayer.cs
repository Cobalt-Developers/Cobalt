namespace Cobalt.Api.Model
{
    public interface IPlayer : IChatSender, IPrettyPrintable
    {
        IPosition Position { get; }

        void Teleport(IPosition pos);

        void Teleport(IPlayer player);
    }
}