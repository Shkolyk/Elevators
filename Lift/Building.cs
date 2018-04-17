using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift
{
    public class Building
    {
        public List<Floor> floors { get; set; }
        public List<Lift> lifts { get; set; }

        public Building(int floorAmount, int liftAmount = 1)
        {
            floors = new List<Floor>();
            for (int i = 0; i < floorAmount; i++) {
                floors.Add(new Floor(i+1));
            }
            lifts = new List<Lift>();
            for (int i = 0; i < liftAmount; i++)
            {
                if (i % 2 == 0)
                {
                    lifts.Add(new Lift(floorAmount - 1 - i, i.ToString()));
                }
                else {
                    lifts.Add(new Lift(i, i.ToString()));
                }
            }
        }
    }
}
