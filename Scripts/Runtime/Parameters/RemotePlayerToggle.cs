using System;

namespace Parameters
{
    /// <summary>
    /// Represents the toggle state for showing remote players in the VRChat camera
    /// </summary>
    public readonly struct RemotePlayerToggle : IEquatable<RemotePlayerToggle>
    {
        public readonly bool Value;

        public RemotePlayerToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(RemotePlayerToggle other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is RemotePlayerToggle other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(RemotePlayerToggle left, RemotePlayerToggle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RemotePlayerToggle left, RemotePlayerToggle right)
        {
            return !left.Equals(right);
        }

        public static implicit operator bool(RemotePlayerToggle toggle)
        {
            return toggle.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

