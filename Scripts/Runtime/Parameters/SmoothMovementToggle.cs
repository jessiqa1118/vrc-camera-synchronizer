using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling smooth movement for the VRChat user camera
    /// </summary>
    public readonly struct SmoothMovementToggle : IEquatable<SmoothMovementToggle>
    {
        public readonly bool Value;

        public SmoothMovementToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(SmoothMovementToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is SmoothMovementToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(SmoothMovementToggle left, SmoothMovementToggle right) => left.Equals(right);
        public static bool operator !=(SmoothMovementToggle left, SmoothMovementToggle right) => !left.Equals(right);

        public static implicit operator bool(SmoothMovementToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

