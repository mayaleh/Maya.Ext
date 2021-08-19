using System;

namespace Maya.Ext
{
    /// <summary>
    /// The unit type is a type that indicates the absence of a specific value; the unit type has only a single value, which acts as a placeholder when no other value exists or is needed.
    /// no void using, no return value needed in success
    /// by https://github.com/louthy/language-ext, https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/unit-type and https://github.com/siroky/FuncSharp
    /// </summary>
    [Serializable]
    public struct Unit : IEquatable<Unit>
    {
        public static readonly Unit Default = new();

        public override int GetHashCode()
        {
            return 97;
        }

        public bool Equals(Unit other)
        {
            return true;
        }

        public override string ToString()
        {
            return "()";
        }

        public override bool Equals(object obj)
        {
            return obj is Unit unit && this.Equals(unit);
        }

        public static bool operator ==(Unit left, Unit right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Unit left, Unit right)
        {
            return !(left == right);
        }
    }
}
