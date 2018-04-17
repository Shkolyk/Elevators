using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
namespace Lift
{
    public class Floor
    {
        public int num { get; set; }
        public List<Passenger> passengers { get; set; }
        public delegate void LiftHandler(int num, Passenger pass);
        public event LiftHandler callLift;
        public bool isActive { get; set; }

        public Floor(int num)
        {
            this.num = num;
            isActive = false;
            passengers = new List<Passenger>();
        }

        public async void generatePassengers(int count)
        {
            Random rnd = new Random();
            await Task.Run(() => {
                while (true)
                {
                    int delay = rnd.Next(10000, 20000);
                    Thread.Sleep(delay);
                    Passenger pas = new Passenger(num, count);
                    passengers.Add(pas);
                    callLift(num, pas);
                    isActive = true;
                    Console.WriteLine("How many passegers in floor {0} ? Answer: {1} {2}", num, passengers.Count, pas.goTo);
                }
            });
        }
    }
}
