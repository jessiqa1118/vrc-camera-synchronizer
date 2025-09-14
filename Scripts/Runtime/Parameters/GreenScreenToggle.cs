using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling Green Screen mode in the VRChat user camera
    /// </summary>
    public readonly struct GreenScreenToggle : IEquatable<GreenScreenToggle>
    {
        public readonly bool Value;

        public GreenScreenToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(GreenScreenToggle other) => Value == other.Value;

        public override bool Equals(object obj) => obj is GreenScreenToggle other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(GreenScreenToggle left, GreenScreenToggle right) => left.Equals(right);
        public static bool operator !=(GreenScreenToggle left, GreenScreenToggle right) => !left.Equals(right);

        public static implicit operator bool(GreenScreenToggle toggle) => toggle.Value;

        public override string ToString() => Value.ToString();
    }
}

