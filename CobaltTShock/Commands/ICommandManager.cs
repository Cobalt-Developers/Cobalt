using TShockAPI;

namespace CobaltTShock.Commands
{
    public interface ICommandManager
    {
        void OnCommand(CommandArgs args);
    }
}