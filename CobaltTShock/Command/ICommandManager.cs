using TShockAPI;

namespace CobaltTShock.Command
{
    public interface ICommandManager
    {
        void OnCommand(CommandArgs args);
    }
}