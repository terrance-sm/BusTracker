using BusTracker.Domain.Model.Enumerations;

namespace BusTracker.Domain.Model
{
    public class City: StringEnumeration<City>
    {
        public static readonly City DallasFortWorth = new City("DFW", "Dallas/Fort Worth");
        public static readonly City Austin = new City("AUS", "Austin");
        public static readonly City Houston = new City("HOU", "Houston");
        public static readonly City Lubbock = new City("LBB", "Lubbock");

        private  City(string  value, string displayName) : base(value, displayName)
        {
           
        }
    }
}
