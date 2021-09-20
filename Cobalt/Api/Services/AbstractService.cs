namespace Cobalt.Api.Services
{
    public abstract class AbstractService
    {
        protected ICobaltPlugin Plugin { get; }

        protected AbstractService(ICobaltPlugin plugin)
        {
            Plugin = plugin;
        }

        public abstract void Init();

        public abstract void Reload();
    }
}