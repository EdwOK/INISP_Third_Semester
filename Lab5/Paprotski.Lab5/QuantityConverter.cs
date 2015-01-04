using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Paprotski.Quantity;

namespace Paprotski.Lab5
{
    public static class QuantityConverter
    {
        private static readonly Dictionary<string, IQuantity> PluginsList = new Dictionary<string, IQuantity>();

        public static void LoadPlugins(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                return;
            }

            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            PluginsList.Clear();

            var directoryFiles = Directory.GetFiles(directoryPath, "*.dll");
            var correctPluginType = typeof(IQuantity);

            foreach (var dllFile in directoryFiles)
            {
                var assembly = Assembly.LoadFrom(dllFile);
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (!type.IsClass && !type.IsPublic)
                    {
                        continue;
                    }

                    if (type.GetInterface(correctPluginType.FullName) != null)
                    {
                        var plugin = (IQuantity)Activator.CreateInstance(type);
                        var pluginAttribute = Attribute.GetCustomAttribute(type, typeof(QuantityAttribute));
                        var pluginAttributeName = ((QuantityAttribute)pluginAttribute).QuanityType;

                        PluginsList[pluginAttributeName] = plugin; 
                    }
                }
            }
        }

        public static double Convert(double initiaValue, string toType, string fromType, string pluginName)
        {
            if (string.IsNullOrWhiteSpace(toType) || string.IsNullOrWhiteSpace(fromType)
                || string.IsNullOrWhiteSpace(pluginName))
            {
                return default(double);
            }

            return
                PluginsList.Where(plugin => plugin.Key == pluginName)
                    .Select(plugin => plugin.Value.ConvertTo(initiaValue, toType, fromType))
                    .FirstOrDefault();
        }

        public static bool PluginExist(string pluginName)
        {
            return PluginsList.Any(plugin => plugin.Key == pluginName);
        }

        public static string ShowQuantitiesTableOfPlugin(string pluginName)
        {
            var builder = new StringBuilder();

            foreach (var plugin in PluginsList.Where(plugin => plugin.Key == pluginName))
            {
                builder.AppendLine("Quanities: " + plugin.Value);
            }

            return builder.ToString();
        }

        public static string ShowPlugins()
        {
            var builder = new StringBuilder();

            foreach (var plugin in PluginsList)
            {
                builder.AppendLine("PluginName: " + plugin.Key + "\nQuanities:" + plugin.Value);
            }

            return builder.ToString();
        }
    }
}
