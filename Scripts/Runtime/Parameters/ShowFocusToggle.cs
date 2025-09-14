using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling focus visualization in the user camera
    /// </summary>
    public readonly struct ShowFocusToggle : IEquatable<ShowFocusToggle>
    {
        public readonly bool Value;

        public ShowFocusToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(ShowFocusToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is ShowFocusToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(ShowFocusToggle left, ShowFocusToggle right) => left.Equals(right);
        public static bool operator !=(ShowFocusToggle left, ShowFocusToggle right) => !left.Equals(right);

        public static implicit operator bool(ShowFocusToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

