using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paprotski.Lab2
{
    class Program 
    {
        static void Main(string[] args)
        {
            var coupes = new Coupe[6]
                             {
                                 new Coupe(true, false, 4, 3), new Coupe(false, false, 4, 4), new Coupe(true, false, 4, 2),
                                 new Coupe(false, false, 4, 1), new Coupe(true, true, 4, 2), new Coupe(true, false, 4, 3)
                             };

            var wagons = new Wagon(coupes, 2008, 25, 5, 100);

            Console.ReadLine();

        }
    }
}
