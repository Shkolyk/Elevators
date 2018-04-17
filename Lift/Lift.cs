using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lift
{
    public enum state
    {
        GoUp = 1,
        GoDown = -1,
        Stop = 0
    }
    public class Lift
    {
        public int maxWeight { get; set; }
        public List<Passenger> pass { get; set; }
        public List<int> floorButtons { get; set; }
        public delegate List<Passenger> GetPassegers(int floor, state state, state nextState, int activeWeight);
        public event GetPassegers getPassengers;
        public int activeFloor { get; set; }
        public state curState;
        public string name { get; set; }
        public Lift(int active, string name)
        {
            Random rnd = new Random();
            this.name = name;
            pass = new List<Passenger>();
            maxWeight = rnd.Next(400, 600);
            activeFloor = 1;
            floorButtons = new List<int>();
            curState = state.Stop;
        }
        public void GoUp(state direction)
        {
            curState = direction;
            Thread.Sleep(2000);
            if (curState == state.GoUp) { activeFloor++; } else { activeFloor--; }
            Console.WriteLine();
            if (pass.Exists(x=>x.goTo==activeFloor) || floorButtons.Contains(activeFloor))
            {
                OpenDoor();
            }
            else {
                GoUp(chooseDirection(activeFloor));
            }
        }

        public void OpenDoor() {
            pass.RemoveAll(x => x.goTo == activeFloor);
            floorButtons.Remove(activeFloor);
            Console.WriteLine("{0},{1}, Lift={2}",activeFloor, curState, name);
            state nextState = chooseDirection(activeFloor);
            var c = getPassengers(activeFloor, curState, nextState, pass.Sum(x=> x.weight));
            pass.AddRange(c);
            state newState = chooseDirection(activeFloor);
            GoUp(newState);
        }
        public void AddFloor(int num, Passenger pass)
        {
            if(!floorButtons.Contains(num))  floorButtons.Add(num);
            if (curState == state.Stop) {
                
                if (num == activeFloor)
                {
                    OpenDoor();
                }
                else if (num < activeFloor)
                {
                    GoUp(state.GoDown);
                }
                else
                {
                    GoUp(state.GoUp);
                }
            }
        }

        public state chooseDirection(int floor)
        {
           
            if (pass.Count == 0 && floorButtons.Count == 0)
            {
                curState = state.Stop;
            }
            else {
                switch (curState) {
                    case state.GoUp:
                        if (pass.Exists(x => x.goTo > floor) || floorButtons.Exists(x => x > floor))
                        {
                            Console.WriteLine("up");
                            return state.GoUp;
                        }
                        else
                        {
                            Console.WriteLine("down");
                            return state.GoDown;
                        }

                    case state.GoDown:
                        if (pass.Exists(x => x.goTo < floor) || floorButtons.Exists(x => x < floor))
                        {
                            Console.WriteLine("down");
                            return state.GoDown;
                        }
                        else
                        {
                            Console.WriteLine("up");
                            return state.GoUp;
                        }
                    }               
                }
            return state.Stop;
        }
    }
}

