using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling roll input while flying for the user camera
    /// </summary>
    public readonly struct RollWhileFlyingToggle : IEquatable<RollWhileFlyingToggle>
    {
        public readonly bool Value;

        public RollWhileFlyingToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(RollWhileFlyingToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is RollWhileFlyingToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(RollWhileFlyingToggle left, RollWhileFlyingToggle right) => left.Equals(right);
        public static bool operator !=(RollWhileFlyingToggle left, RollWhileFlyingToggle right) => !left.Equals(right);

        public static implicit operator bool(RollWhileFlyingToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

