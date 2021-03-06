namespace Cobalt.Api.Service
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