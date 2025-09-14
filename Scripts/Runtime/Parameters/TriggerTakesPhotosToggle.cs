using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling the behavior where trigger input takes photos
    /// </summary>
    public readonly struct TriggerTakesPhotosToggle : IEquatable<TriggerTakesPhotosToggle>
    {
        public readonly bool Value;

        public TriggerTakesPhotosToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(TriggerTakesPhotosToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is TriggerTakesPhotosToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(TriggerTakesPhotosToggle left, TriggerTakesPhotosToggle right) => left.Equals(right);
        public static bool operator !=(TriggerTakesPhotosToggle left, TriggerTakesPhotosToggle right) => !left.Equals(right);

        public static implicit operator bool(TriggerTakesPhotosToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

