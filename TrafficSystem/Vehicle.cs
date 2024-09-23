using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSystem
{
    public class Vehicle
    {
        Highway Highway;
        LanePosition LanePosition;
        int x_current;
        int y_current;
        int exit = -1;
        int steps_waiting;
        public Vehicle(Highway h, int startX, int startY, int exitIndex)
        {
            x_current = startX;
            y_current = startY;
            Highway = h;
            exit = exitIndex;
            LanePosition = Highway.lanePositions[startX, startY];
            LanePosition.thisState = LanePosition.State.occupied;
        }
        public void SetExit(int exitIndex)
        {
            exit = exitIndex;
        }
        public void Step()
        {
            Console.WriteLine("Vehicle Step");
            if((exit - y_current <= Highway.x_size + 1) && exit != -1)
            {
                if ((x_current == 0) || Highway.lanePositions[x_current-1, y_current - 1].thisState == LanePosition.State.empty)
                {
                    if (Highway.lanePositions[x_current - 1, y_current+1].thisState == LanePosition.State.empty && Highway.lanePositions[x_current, y_current].thisState == LanePosition.State.empty)
                    {
                        LanePosition.thisState = LanePosition.State.empty;
                        LanePosition = Highway.lanePositions[x_current - 1, y_current + 1];
                        Highway.lanePositions[x_current -1, y_current+1].thisState = LanePosition.State.occupied;
                        x_current--;
                        y_current++;
                    }
                    else
                    {
                        steps_waiting++;
                    }
                }
            }
            if (Highway.lanePositions[x_current, y_current+1].thisState == LanePosition.State.empty)
            {
                LanePosition.thisState = LanePosition.State.empty;
                LanePosition = Highway.lanePositions[x_current, y_current + 1];
                Highway.lanePositions[x_current, y_current + 1].thisState = LanePosition.State.occupied;
                y_current++;
            }
            if(Highway.lanePositions[x_current, y_current + 1].thisState == LanePosition.State.occupied)
            {
                steps_waiting++;
            }
            if (Highway.lanePositions[x_current, y_current + 1].thisState == LanePosition.State.closed)
            {
                if (Highway.lanePositions[x_current-1, y_current].thisState == LanePosition.State.empty && Highway.lanePositions[x_current - 1, y_current-1].thisState == LanePosition.State.empty)
                {
                    LanePosition.thisState = LanePosition.State.empty;
                    LanePosition = Highway.lanePositions[x_current-1, y_current];
                    Highway.lanePositions[x_current-1, y_current].thisState = LanePosition.State.occupied;
                    x_current--;
                }
                else
                {
                    steps_waiting++;
                }
            }
        }
    }
}
