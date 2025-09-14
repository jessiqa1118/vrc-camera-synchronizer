using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling visibility of dolly paths in the user camera
    /// </summary>
    public readonly struct DollyPathsStayVisibleToggle : IEquatable<DollyPathsStayVisibleToggle>
    {
        public readonly bool Value;

        public DollyPathsStayVisibleToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(DollyPathsStayVisibleToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is DollyPathsStayVisibleToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(DollyPathsStayVisibleToggle left, DollyPathsStayVisibleToggle right) => left.Equals(right);
        public static bool operator !=(DollyPathsStayVisibleToggle left, DollyPathsStayVisibleToggle right) => !left.Equals(right);

        public static implicit operator bool(DollyPathsStayVisibleToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

