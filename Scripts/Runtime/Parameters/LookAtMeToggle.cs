using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling Look At Me mode for the user camera
    /// </summary>
    public readonly struct LookAtMeToggle : IEquatable<LookAtMeToggle>
    {
        public readonly bool Value;

        public LookAtMeToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(LookAtMeToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is LookAtMeToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(LookAtMeToggle left, LookAtMeToggle right) => left.Equals(right);
        public static bool operator !=(LookAtMeToggle left, LookAtMeToggle right) => !left.Equals(right);

        public static implicit operator bool(LookAtMeToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

