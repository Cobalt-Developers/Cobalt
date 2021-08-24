using CobaltCore.Commands;

namespace CobaltCore.Services
{
    public abstract class AbstractService
    {
        protected CobaltPlugin Plugin { get; }

        protected AbstractService(CobaltPlugin plugin)
        {
            Plugin = plugin;
        }

        public abstract void Init();

        public abstract void Reload();
    }
}