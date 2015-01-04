using System;
namespace Paprotski.Lab5
{
    public static class MainClass
    {
        public static void Main()
        {
            Menu();
            Console.ReadKey();
        }

        private static void ChoisePlugin()
        {
            Console.Write("Please enter the plugin name: ");
            var pluginName = Console.ReadLine();

            if (QuantityConverter.PluginExist(pluginName))
            {
                Console.WriteLine(QuantityConverter.ShowQuantitiesTableOfPlugin(pluginName));

                var initialValue = Parse<double>("initial value");
                var toType = Parse<string>("to type");
                var fromType = Parse<string>("from type");

                var result = QuantityConverter.Convert(initialValue, toType, fromType, pluginName); 
                Console.WriteLine("Result: {0}, initialValue: {1}, toType: {2}, fromType: {3}", result, initialValue, toType, fromType);
            }
            else
            {
                Console.WriteLine("Incorrect plugin name or is not found!");
            }
        }

        private static void Menu()
        {
            var repeat = true;

            while (repeat)
            {
                Console.Clear();
                Console.WriteLine("1. LoadPlugins\n2. ShowPlugins\n3. Selected plugin\n4. Quit");
                var number = Parse<int>("number");

                switch (number)
                {
                    case 1:
                        QuantityConverter.LoadPlugins("D:/plugins");
                        Console.WriteLine("Plugins loaded successfully");
                        Console.ReadKey();
                        break;
                    case 2:
                        var stringListPlugins = QuantityConverter.ShowPlugins();
                        if (string.IsNullOrWhiteSpace(stringListPlugins))
                        {
                            Console.WriteLine("List of empty!");
                        }
                        else
                        {
                            Console.WriteLine("List of plugins!");
                            Console.WriteLine(stringListPlugins);
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        ChoisePlugin();
                        Console.ReadKey();
                        break;
                    case 4:
                        repeat = false; 
                        break;
                }
            }
        }

        private static T Parse<T>(this string valueName) where T : IConvertible
        {
            while (true)
            {
                try
                {
                    Console.Write("Please enter the " + valueName + ": ");
                    var line = Console.ReadLine();

                    if (line != null)
                    {
                        return (T)Convert.ChangeType(line, typeof(T));
                    }
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (OverflowException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}
