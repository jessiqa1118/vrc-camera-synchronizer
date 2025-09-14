using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling Auto Level (Roll) for the user camera
    /// </summary>
    public readonly struct AutoLevelRollToggle : IEquatable<AutoLevelRollToggle>
    {
        public readonly bool Value;

        public AutoLevelRollToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(AutoLevelRollToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is AutoLevelRollToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(AutoLevelRollToggle left, AutoLevelRollToggle right) => left.Equals(right);
        public static bool operator !=(AutoLevelRollToggle left, AutoLevelRollToggle right) => !left.Equals(right);

        public static implicit operator bool(AutoLevelRollToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

