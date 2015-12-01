using System;
using System.Diagnostics;
using FubuCore;

namespace BusTracker.Domain.Model.Enumerations
{
    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class StringEnumeration<TEnmeration>: Enumeration<TEnmeration, string>, IEnumeration<TEnmeration, string>, IStringEnumeration
        where TEnmeration : StringEnumeration<TEnmeration>
    {
        protected StringEnumeration(string value, string displayName) : base(value, displayName)
        {
        }

        public static T TryGetFromValueAndParse<T>(string value) where T : StringEnumeration<T>
        {
          if(value.IsEmpty())return null;

            T res;

            if (!StringEnumeration<T>.TryFromValue(value, out res))
                StringEnumeration<T>.TryParse(value, out res);

            return res;
        }
    }
}
