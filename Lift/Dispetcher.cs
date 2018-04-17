using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift
{
    public class Dispetcher
    {
        const int maxWeight = 400;
        Building building { get; set; }

        public Dispetcher(int floorNum, int liftNum)
        {
            building = new Building(floorNum, liftNum);
        }

        public void start()
        {
            foreach (Lift lift in building.lifts)
            {
                lift.getPassengers += GetPassengersHandler;
            }

            foreach (Floor floor in building.floors)
            {
                floor.callLift += CallLiftHandler;
                floor.generatePassengers(building.floors.Count);
            }
            Console.ReadLine();
        }

        public void CallLiftHandler(int num, Passenger pass)
        {
            foreach (Lift lift in building.lifts)
            {
                lift.AddFloor(num, pass);
            }
        }

        public List<Passenger> GetPassengersHandler(int floor, state state, state newState, int activeWeight)
        {
            List<Passenger> newPass = new List<Passenger>();
            if (state == state.GoUp)
            {
                newPass.AddRange(building.floors[floor - 1].passengers.Where(x => x.goTo > floor).Select(x => x));
                if (newPass.Count == 0 && newState == state.GoDown)
                {
                    newPass.AddRange(building.floors[floor - 1].passengers.Where(x => x.goTo < floor).Select(x => x));
                }
            }
            else
            {
                newPass.AddRange(building.floors[floor - 1].passengers.Where(x => x.goTo < floor).Select(x => x));
                if (newPass.Count == 0 && newState == state.GoUp)
                {
                    newPass.AddRange(building.floors[floor - 1].passengers.Where(x => x.goTo > floor).Select(x => x));
                }
            }
            newPass = checkWeight(newPass, activeWeight);
            foreach (Passenger person in newPass)
                building.floors[floor - 1].passengers.Remove(person);
            return newPass;
        }

        public List<Passenger> checkWeight(List<Passenger> pass, int activeWeight)
        {
            List<Passenger> temp = new List<Passenger>();
            foreach (Passenger person in pass)
            {
                if (activeWeight + person.weight <= maxWeight)
                {
                    activeWeight += person.weight;
                    temp.Add(person);
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine(activeWeight);
            return temp;
        }

        public int[] amountPepoleOnFloor()
        {
            int[] peopleOnFloor = new int[building.floors.Count];
            foreach (Floor f in building.floors)
            {
                peopleOnFloor[f.num-1] = f.passengers.Count;
            }

            return peopleOnFloor;
        }

        public int[] activeFloors()
        {
            int[] activeFloorForLift = new int[building.floors.Count];
            foreach (Lift l in building.lifts)
            {
                activeFloorForLift[Convert.ToInt32(l.name)] = l.activeFloor;
            }

            return activeFloorForLift;
        }
    }
}
