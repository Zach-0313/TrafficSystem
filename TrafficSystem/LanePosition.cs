namespace TrafficSystem
{
    public class LanePosition
    {
        public enum LaneState { empty, closed, occupied };
        public LaneState thisState = LaneState.empty;
        public LanePosition()
        {
            thisState = LaneState.empty;
        }
    }
}
