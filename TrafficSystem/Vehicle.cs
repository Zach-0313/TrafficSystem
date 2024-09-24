using System.Windows;
using System.Windows.Controls;

namespace TrafficSystem
{
    public class Vehicle
    {
        private Highway _highway;
        LanePosition LanePosition;
        int x_current;
        int y_current;
        int exit = -1;
        int steps_waiting;
        bool reachedExit;
        private VehicleData data;
        public struct VehicleData
        {
            public int vehicleNum;
            public int stepsWaiting;
            public int laneChanges;
            public int exit;
        }

        public event EventHandler<VehicleData> ExitReached;
        public Vehicle(Highway h, int startX, int startY, int exitIndex, int num)
        {
            data.vehicleNum = num;
            x_current = startX;
            y_current = startY;
            _highway = h;
            data.exit = exitIndex;
            LanePosition = _highway.lanePositions[startX, startY];
            LanePosition.thisState = LanePosition.State.occupied;

            _highway.VehicleTimeStep += Step;
        }
        private bool isEmpty(int offX, int offY)
        {
            int x = Math.Clamp(x_current + offX, 0, _highway.x_size - 1);
            int y = Math.Clamp(y_current + offY, 0, _highway.y_size - 1);
            return _highway.lanePositions[x, y].thisState == LanePosition.State.empty;
        }
        private LanePosition.State getNextPosition()
        {
            return _highway.lanePositions[x_current, y_current + 1].thisState;
        }
        public void Step(object sender, EventArgs e)
        {
            if (reachedExit)
            {
                return;
            }
            if (y_current >= exit && x_current == _highway.x_size - 1)
            {
                Console.WriteLine("Reached Exit");
                LanePosition.thisState = LanePosition.State.empty;
                reachedExit = true;

                ExitReached?.Invoke(this, data);
                return;
            }
            int rightMost = Math.Clamp(x_current + 1, 0, _highway.x_size - 1);
            if ((x_current != _highway.x_size) && (data.exit - y_current <= _highway.x_size + 1))
            {
                if (_highway.lanePositions[rightMost, y_current + 1].thisState == LanePosition.State.empty)
                {
                    LanePosition.thisState = LanePosition.State.empty;
                    LanePosition = _highway.lanePositions[rightMost, y_current+1];
                    _highway.lanePositions[rightMost, y_current+1].thisState = LanePosition.State.occupied;
                    x_current = rightMost;
                    y_current++;
                    data.laneChanges++;
                }
                else
                {
                    data.stepsWaiting++;
                }
                return;
            }
            if (isEmpty(0,1))
            {
                LanePosition.thisState = LanePosition.State.empty;
                LanePosition = _highway.lanePositions[x_current, y_current + 1];
                _highway.lanePositions[x_current, y_current + 1].thisState = LanePosition.State.occupied;
                y_current++;
                data.laneChanges++;
                return;
            }
            if (getNextPosition() == LanePosition.State.occupied)
            {
                data.stepsWaiting++;
                return;
            }
            if (getNextPosition() == LanePosition.State.closed)
            {
                if (isEmpty(-1,0) && isEmpty(-1,-1))
                {
                    LanePosition.thisState = LanePosition.State.empty;
                    LanePosition = _highway.lanePositions[x_current - 1, y_current];
                    _highway.lanePositions[x_current - 1, y_current].thisState = LanePosition.State.occupied;
                    x_current--;
                }
                else
                {
                    data.stepsWaiting++;
                }
                return;
            }
        }
    }
}
