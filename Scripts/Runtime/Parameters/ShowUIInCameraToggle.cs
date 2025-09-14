using System;

namespace Parameters
{
    /// <summary>
    /// Represents the toggle state for showing UI elements in VRChat camera view
    /// </summary>
    public readonly struct ShowUIInCameraToggle : IEquatable<ShowUIInCameraToggle>
    {
        public readonly bool Value;

        public ShowUIInCameraToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(ShowUIInCameraToggle other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is ShowUIInCameraToggle other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(ShowUIInCameraToggle left, ShowUIInCameraToggle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ShowUIInCameraToggle left, ShowUIInCameraToggle right)
        {
            return !left.Equals(right);
        }

        public static implicit operator bool(ShowUIInCameraToggle toggle)
        {
            return toggle.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}