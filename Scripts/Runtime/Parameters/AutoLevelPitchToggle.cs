using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling Auto Level (Pitch) for the user camera
    /// </summary>
    public readonly struct AutoLevelPitchToggle : IEquatable<AutoLevelPitchToggle>
    {
        public readonly bool Value;

        public AutoLevelPitchToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(AutoLevelPitchToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is AutoLevelPitchToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(AutoLevelPitchToggle left, AutoLevelPitchToggle right) => left.Equals(right);
        public static bool operator !=(AutoLevelPitchToggle left, AutoLevelPitchToggle right) => !left.Equals(right);

        public static implicit operator bool(AutoLevelPitchToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

