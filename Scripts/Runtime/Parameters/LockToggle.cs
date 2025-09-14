using System;

namespace Parameters
{
    /// <summary>
    /// Represents the toggle state for locking the VRChat camera position
    /// </summary>
    public readonly struct LockToggle : IEquatable<LockToggle>
    {
        public readonly bool Value;

        public LockToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(LockToggle other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is LockToggle other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(LockToggle left, LockToggle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LockToggle left, LockToggle right)
        {
            return !left.Equals(right);
        }

        public static implicit operator bool(LockToggle toggle)
        {
            return toggle.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

