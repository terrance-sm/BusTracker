using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace BusTracker.Domain.Model.Enumerations
{
    public interface IEnumeration
    {
        string DisplayName { get; } 
    }

    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class Enumeration <TEnumeration, TValue> : IComparable<TEnumeration>, IEquatable<TEnumeration>, IEnumeration 
        where TEnumeration: Enumeration<TEnumeration, TValue> 
        where TValue: IComparable
    {
        readonly string _displayName;
        readonly TValue _value;

        private  static readonly Lazy<TEnumeration[]> _enumerations = new Lazy<TEnumeration[]>(GetEnumerations);

        protected Enumeration(TValue value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        /*public TValue Value
        {
            get { return _value; }
        } 
        public string DisplayName
        {
            get { return _displayName; }
        }*/
            
        //Using "Body Expression
        public TValue Value => _value;

        public string DisplayName => _displayName;

        public virtual int CompareTo(TEnumeration other)
        {
            return Value.CompareTo(other.Value);
        }

        public override sealed string ToString()
        {
            return DisplayName;
        }

        public static TEnumeration[] GetAll()
        {
            return _enumerations.Value;
        }

        private static TEnumeration[] GetEnumerations()
        {
            Type enumerationType = typeof (TEnumeration);
            return enumerationType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(info => enumerationType.IsAssignableFrom(info.FieldType))
                .Select(info => info.GetValue(null))
                .Cast<TEnumeration>()
                .ToArray();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TEnumeration);
        }

        public bool Equals(TEnumeration other)
        {
            return other != null && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
        {
            return !(left == right);
        }

        public static TEnumeration FromValue(TValue value)
        {
            return Parse(value, "value", item => item.Value.ToString().Equals(value.ToString().Trim()));
        }

        public static bool TryFromValue(TValue value, out TEnumeration result)
        {
            return TryParse(e => e.Value.Equals(value), out result);
        }

        static bool TryParse(Func<TEnumeration, bool> predicate, out TEnumeration result)
        {
            result = GetAll().FirstOrDefault(predicate);
            return result != null;
        }

        private static TEnumeration Parse(object value, string description, Func<TEnumeration, bool> predicate)
        {
            TEnumeration result;
            if (Equals(value, default(TValue))) return null;

            if (!TryParse(predicate, out result))
            {
                //string message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof (TEnumeration));
                string message = $"'{value}' is not a valid {description} in {typeof(TEnumeration)}"; //using interpolation
                //throw new ArgumentException(message, "value");
                throw new ArgumentException(message, nameof(value));
            }

            return result;
        }

        public static bool TryParse(string displayName, out TEnumeration result)
        {
            return TryParse(e=>e.DisplayName ==displayName, out result);
        }
    }
}
