using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("floors = ");
            int floors = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("lifts = ");
            int lifts = Convert.ToInt32(Console.ReadLine());

            Dispetcher dispetcher = new Dispetcher(floors, lifts);
            dispetcher.start();
            Console.ReadLine();
        }
    }
}
