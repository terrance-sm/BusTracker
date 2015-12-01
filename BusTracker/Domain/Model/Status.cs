using BusTracker.Domain.Model.Enumerations;

namespace BusTracker.Domain.Model
{
    public class Status: Int32Enumeration<Status>
    {
        public static readonly Status Origin = new Status(1, "Origin");
        public static readonly Status Transit = new Status(2, "Transit");
        public static readonly Status Destination = new Status(3, "Destination");

        private  Status(int value, string displayName) : base(value, displayName)
        {
           
        }
    }
}
