using System;
using System.Diagnostics;

namespace BusTracker.Domain.Model.Enumerations
{
    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class Int32Enumeration<TEnmeration>: Enumeration<TEnmeration, int>, IEnumeration<TEnmeration, int>
        where TEnmeration : Int32Enumeration<TEnmeration>
    {
        protected Int32Enumeration(int value, string displayName) : base(value, displayName)
        {
        }

        public static TEnmeration FromValue32(int value)
        {
            return FromValue(value);
        }

        public static bool TryFromInt32(int listItemValue, out TEnmeration result)
        {
            return TryFromValue(listItemValue, out result);
        }
    }
}
