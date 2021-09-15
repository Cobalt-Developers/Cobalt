using System;
using System.Collections;
using CobaltCore.Attributes;
using CobaltCore.Exceptions;
using CobaltCore.Helpers;

namespace CobaltCore.Services
{
    public class ServiceManager
    {
        private ICobaltPlugin Plugin { get; }

        private OrderedDictionary<Type, AbstractService> services = new OrderedDictionary<Type, AbstractService>(); // TODO: restrict to only AbstractService

        public ServiceManager(ICobaltPlugin plugin)
        {
            Plugin = plugin;
        }

        public bool Exists<T>() where T : AbstractService
        {
            return Exists(typeof(T));
        }
        
        private bool Exists(Type serviceType)
        {
            return services.Contains(serviceType);
        }

        public void RegisterCustomServices()
        {
            try
            {
                ServiceAttribute[] attributes =
                    (ServiceAttribute[]) Attribute.GetCustomAttributes(Plugin.GetType(), typeof(ServiceAttribute));
                foreach (var attribute in attributes)
                {
                    Plugin.Log("Registering custom service " + attribute.Value.Name);
                    RegisterService(attribute.Value);
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
            RegisterService(typeof(T));
        }
        
        private void RegisterService(Type serviceType)
        {
            AbstractService service;
            try
            {
                service = (AbstractService) Activator.CreateInstance(serviceType, Plugin);
            }
            catch (Exception e)
            {
                throw new ServiceInitException("Service creation failed.", e);
            }
            service.Init();
            services.Add(serviceType, service);
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