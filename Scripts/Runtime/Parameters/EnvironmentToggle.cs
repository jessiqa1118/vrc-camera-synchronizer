using System;

namespace Parameters
{
    /// <summary>
    /// Represents the toggle state for showing the environment in the VRChat camera
    /// </summary>
    public readonly struct EnvironmentToggle : IEquatable<EnvironmentToggle>
    {
        public readonly bool Value;

        public EnvironmentToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(EnvironmentToggle other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is EnvironmentToggle other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(EnvironmentToggle left, EnvironmentToggle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EnvironmentToggle left, EnvironmentToggle right)
        {
            return !left.Equals(right);
        }

        public static implicit operator bool(EnvironmentToggle toggle)
        {
            return toggle.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

