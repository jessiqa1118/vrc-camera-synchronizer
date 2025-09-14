using System;

namespace Parameters
{
    /// <summary>
    /// Represents the toggle state for showing the local player in the VRChat camera
    /// </summary>
    public readonly struct LocalPlayerToggle : IEquatable<LocalPlayerToggle>
    {
        public readonly bool Value;

        public LocalPlayerToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(LocalPlayerToggle other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is LocalPlayerToggle other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(LocalPlayerToggle left, LocalPlayerToggle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LocalPlayerToggle left, LocalPlayerToggle right)
        {
            return !left.Equals(right);
        }

        public static implicit operator bool(LocalPlayerToggle toggle)
        {
            return toggle.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

