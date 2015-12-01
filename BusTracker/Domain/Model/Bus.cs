using System;

namespace BusTracker.Domain.Model
{
    public class Bus
    {
        public Status Status { get; set; }
        public string Number { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime EstimatedTimeOfArrival { get; set; }
        public Location Location { get; set; }

        public Bus()
        {
            Status = Status.Origin;
        }

        public Location GetLocation()
        {
             return Location;
        }
    }
}
