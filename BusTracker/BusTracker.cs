using System;
using System.Collections.Generic;
using System.Linq;
using BusTracker.Domain.Model;

namespace BusTracker
{
    public class BusTracker
    {
        public List<Schedule> Schedules{ get; set; }

        public BusTracker()
        {
            Schedules = new List<Schedule>();
        }

        public Location TrackBus(Bus busToTrack)
        {
            var buses = Schedules.SelectMany(x => x.Buses).ToList();
            var result = buses.FirstOrDefault(bus => bus == busToTrack);
            return result?.GetLocation();
        }

        public Bus Search(string busNumber)
        {
            return Schedules.SelectMany(x => x.Buses).FirstOrDefault(bus => bus.Number == busNumber);
        }

        public List<Schedule> Search(City city, bool isDepartureCity = false)
        {
            return isDepartureCity ? 
                Schedules.Where(x=>x.Route.DepartureCity == city).ToList():
                Schedules.Where(x=>x.Route.ArrivalCity == city).ToList();
        }

        public List<Schedule> Search(City departureCity, City arrivalCity)
        {
            var searchResult =
                Schedules.Where(
                    x =>
                        x.Route.DepartureCity == departureCity &&
                        x.Route.ArrivalCity == arrivalCity).ToList();

            return searchResult.Any() ? searchResult : null;
        }
    }
}
