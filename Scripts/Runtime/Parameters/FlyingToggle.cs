using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling Flying mode for the VRChat user camera
    /// </summary>
    public readonly struct FlyingToggle : IEquatable<FlyingToggle>
    {
        public readonly bool Value;

        public FlyingToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(FlyingToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is FlyingToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(FlyingToggle left, FlyingToggle right) => left.Equals(right);
        public static bool operator !=(FlyingToggle left, FlyingToggle right) => !left.Equals(right);

        public static implicit operator bool(FlyingToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

