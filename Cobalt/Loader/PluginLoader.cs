using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Cobalt.Api;
using Cobalt.Api.Messages;
using Cobalt.Loader.Attribute;
using Cobalt.Loader.Exception;
using Cobalt.Plugin;

namespace Cobalt.Loader
{
    public class PluginLoader
    {
        private const string Prefix = "[Cobalt]";
        
        private readonly List<Assembly> _pluginAssemblies = new List<Assembly>();
        private readonly Dictionary<string, ICobaltPlugin> _pluginInstances = new Dictionary<string, ICobaltPlugin>();

        public PluginLoader()
        {
            LoadPluginDlls();
            InitializePlugins();
        }

        private void LoadPluginDlls()
        {
            Log("Loading plugins...");
            
            Directory.CreateDirectory(CobaltMain.PluginFolder);

            var files = Directory.GetFiles(CobaltMain.PluginFolder, "*.dll", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var assembly = AppDomain.CurrentDomain.Load(File.ReadAllBytes(file));
                _pluginAssemblies.Add(assembly);
            }
            
            Log($"Loaded {_pluginAssemblies.Count} plugins.");
        }

        private void InitializePlugins()
        {
            Log("Initializing plugins...");
            
            // Initialize core plugin
            InitializePlugin(typeof(CobaltServerPlugin));
            
            // Initialize custom plugin
            _pluginAssemblies.ForEach(assembly =>
            {
                IEnumerable<Type> pluginClasses = GetTypesWithPluginAttribute(assembly);
                foreach (var pluginClass in pluginClasses)
                {
                    try
                    {
                        InitializePlugin(pluginClass);
                    }
                    catch (PluginInitException e)
                    {
                        Log(LogLevel.VERBOSE, $"Plugin Initialization failed:");
                        Log(LogLevel.VERBOSE, e.ToString());
                    }
                }
            });
        }

        private void InitializePlugin(Type pluginClass)
        {
            var instance = (ICobaltPlugin) Activator.CreateInstance(pluginClass);

            if (_pluginInstances.ContainsKey(instance.Name))
            {
                throw new PluginInitException($"Plugin with the name '{instance.Name}' already exists.");
            }

            _pluginInstances.Add(instance.Name, instance);

            try
            {
                instance.Initialize();
            }
            catch (System.Exception e)
            {
                instance.Disable();
                throw new PluginInitException("Exception while calling Initialize()", e);
            }
            
            Log($"{instance.Name} {instance.Version} initialized.");
        }

        private static IEnumerable<Type> GetTypesWithPluginAttribute(Assembly assembly)
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types;
            }

            return types
                .Where(t => t != null) // From loaded assembly
                .Where(type => typeof(ICobaltPlugin).IsAssignableFrom(type)) // Instanceof ICobaltPlugin
                .Where(type => type.GetCustomAttributes<CobaltPluginAttribute>().Any()) // Has CobaltPlugin-Attribute
                .ToList();
        }

        public ICobaltPlugin GetPlugin(string pluginName)
        {
            return _pluginInstances.ContainsKey(pluginName) ? _pluginInstances[pluginName] : null;
        }
        
        private static void Log(String message)
        {
            Log(LogLevel.INFO, message);
        }

        private static void Log(LogLevel level, String message)
        {
            ConsoleColor color;
            switch (level)
            {
                case LogLevel.WARNING:
                    color = ConsoleColor.Yellow;
                    break;
                case LogLevel.VERBOSE:
                    color = ConsoleColor.Red;
                    break;
                case LogLevel.INFO:
                    color = ConsoleColor.White;
                    break;
                default:
                    color = ConsoleColor.White;
                    break;
            }
            Console.ForegroundColor = color;
            
            Console.WriteLine($"{Prefix} {message}");
            Console.ResetColor();
        }
    }
}