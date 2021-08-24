using System;
using System.Collections;
using CobaltCore.Exceptions;
using CobaltCore.Helpers;

namespace CobaltCore.Services
{
    public class ServiceManager
    {
        private CobaltPlugin Plugin { get; }

        private OrderedDictionary<Type, AbstractService> services = new OrderedDictionary<Type, AbstractService>(); // TODO: restrict to only AbstractService

        public ServiceManager(CobaltPlugin plugin)
        {
            Plugin = plugin;
        }

        public bool Exists<T>() where T : AbstractService
        {
            return services.Contains(typeof(T));
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
                service = (T) Activator.CreateInstance(typeof(T), Plugin);
            }
            catch (Exception e)
            {
                throw new ServiceInitException("Service creation failed.", e);
            }
            service.Init();
            services.Add(typeof(T), service);
        }

        public AbstractService GetService<T>() where T : AbstractService
        {
            if (!Exists<T>())
            {
                throw new UnknownServiceException($"Could not retrieve service: {typeof(T).Name}");
            }
            return services[typeof(T)];
        }

        public void Reload<T>() where T : AbstractService
        {
            GetService<T>().Reload();
        }

        public void Reload()
        {
            foreach (DictionaryEntry service in services)
            {
                ((AbstractService) service.Value).Reload();
            }
        }
    }
}