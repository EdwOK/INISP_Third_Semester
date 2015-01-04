using System;
using System.Globalization;

namespace Paprotski.Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var price = Price.Parse("   100 $   ");
            Console.WriteLine(price);
            Console.ReadKey();
        }
    }
}
