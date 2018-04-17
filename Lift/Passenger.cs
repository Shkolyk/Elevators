using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift
{
    public class Passenger
    {
        public int weight { get; set; }
        public int goTo { get; set; }
        public Passenger(int floor, int count) {
            Random rnd = new Random();
            weight = rnd.Next(40,120);
            do
            {
                goTo = rnd.Next(1, count);
            } while (goTo == floor);
        }
    }
}
