using System;
using System.Collections;
using Cobalt.Api.Attribute;
using Cobalt.Api.Exception;
using Cobalt.Api.Helper;

namespace Cobalt.Api.Service
{
    public class ServiceManager
    {
        private ICobaltPlugin Plugin { get; }

        private readonly OrderedDictionary<Type, AbstractService> _services = new OrderedDictionary<Type, AbstractService>();

        public ServiceManager(ICobaltPlugin plugin)
        {
            Plugin = plugin;
        }

        public bool Exists<T>() where T : AbstractService
        {
            return _services.Contains(typeof(T));
        }

        public void RegisterCustomServices()
        {
            try
            {
                ServiceAttribute[] attributes =
                    (ServiceAttribute[]) System.Attribute.GetCustomAttributes(Plugin.GetType(), typeof(ServiceAttribute));
                foreach (var attribute in attributes)
                {
                    Plugin.Log("Registering custom service " + attribute.Value.Name);
                    GetType().GetMethod("RegisterService")?.MakeGenericMethod(attribute.Value).Invoke(this, null);
                }
            }
            catch (ServiceAlreadyExistsException e)
            {
                throw new ServiceInitException(e.Message);
            }
        }

        public void RegisterService<T>() where T : AbstractService
        {
            if (Exists<T>())
            {
                throw new ServiceAlreadyExistsException($"The Service '{typeof(T).Name}' is already existing.");
            }
            AbstractService service;
            try
            {
                service = (AbstractService) Activator.CreateInstance(typeof(T), Plugin);
            }
            catch (System.Exception e)
            {
                throw new ServiceInitException("Service creation failed.", e);
            }
            service.Init();
            _services.Add(typeof(T), service);
        }

        public AbstractService GetService<T>() where T : AbstractService
        {
            if (!Exists<T>())
            {
                throw new UnknownServiceException($"Could not retrieve service: {typeof(T).Name}");
            }
            return _services[typeof(T)];
        }

        public void Reload<T>() where T : AbstractService
        {
            GetService<T>().Reload();
        }

        public void Reload()
        {
            foreach (DictionaryEntry service in _services)
            {
                ((AbstractService) service.Value).Reload();
            }
        }
    }
}