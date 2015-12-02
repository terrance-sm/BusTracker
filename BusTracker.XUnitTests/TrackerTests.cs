using Xunit;
using BusTracker.Domain.Model;
using Should;
using System.Collections.Generic;

namespace BusTracker.XUnitTests
{
    public class TrackerTests
    {
        private readonly BusTracker _busTracker;
        private static readonly Bus BusLbbToDfw = new Bus { Number = "D001" };
        private static readonly Bus BusDfwToLbb = new Bus { Number = "D002" };
        private static readonly Bus BusAusToHou = new Bus { Number = "A001" };
        private static readonly Bus BusHouToDfw = new Bus { Number = "H001" };
        private static readonly Bus BusHouToAus = new Bus { Number = "H002" };
        private static readonly Bus BusDfwToHou = new Bus { Number = "D003" };
        private static readonly Bus BusLbbToAus = new Bus { Number = "L001" };
        private static readonly Bus BusAusToLbb = new Bus { Number = "A002" };

        private static readonly Route RouteHouToDfw = new Route { ArrivalCity = City.Houston, DepartureCity = City.DallasFortWorth };
        private static readonly Route RouteLbbToAus = new Route { ArrivalCity = City.Lubbock, DepartureCity = City.Austin };
        private static readonly Route RouteDfwToLbb = new Route { ArrivalCity = City.DallasFortWorth, DepartureCity = City.Lubbock };
        private static readonly Route RouteAusToHou = new Route { ArrivalCity = City.Austin, DepartureCity = City.Houston };

        private readonly Schedule _scheduleLubbockDFw = new Schedule
        {
            Buses = new List<Bus> { BusLbbToDfw, BusDfwToLbb },
            Route = RouteDfwToLbb
        };

        private readonly Schedule _scheduleHoustonAustin = new Schedule
        {
            Buses = new List<Bus> { BusAusToHou, BusHouToAus },
            Route = RouteAusToHou
        };

        private readonly Schedule _scheduleHoustonDfw = new Schedule
        {
            Buses = new List<Bus> { BusHouToDfw, BusDfwToHou },
            Route = RouteHouToDfw
        };

        private readonly Schedule _scheduleLubbockAustin = new Schedule
        {
            Buses = new List<Bus> { BusLbbToAus, BusAusToLbb },
            Route = RouteLbbToAus
        };

        public TrackerTests()
        {
            _busTracker = new BusTracker
            {
                Schedules = new List<Schedule> { _scheduleLubbockDFw, _scheduleHoustonAustin, _scheduleHoustonDfw, _scheduleLubbockAustin }
            };
        }

        [Fact]
        public void ShouldReturnBusLocation()
        {
            var location = _busTracker.TrackBus(BusHouToDfw);
            location.ShouldEqual(BusHouToDfw.Location);
        }

        [Fact]
        public void ShouldFindBusViaRoute()
        {
            var results = _busTracker.Search(City.Lubbock, City.DallasFortWorth);
            results.ShouldContain(_scheduleLubbockDFw);

            results = _busTracker.Search(City.DallasFortWorth, City.Austin);
            results.ShouldBeNull();
        }

        [Fact]
        public void ShouldFindBusViaBusNumber()
        {
            var result = _busTracker.Search("H001");
            result.ShouldEqual(BusHouToDfw);

            result = _busTracker.Search("F001");
            result.ShouldBeNull();
        }

        [Fact]
        public void ShouldFindBusesViaCity()
        {
            var result = _busTracker.Search(City.Houston);
            result.Count.ShouldEqual(1);
        }
    }
}