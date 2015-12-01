using System.Collections.Generic;

namespace BusTracker.Domain.Model
{
    public class Schedule
    {
        public List<Bus> Buses { get; set; }
        public Route Route { get; set; }
        
    }
}