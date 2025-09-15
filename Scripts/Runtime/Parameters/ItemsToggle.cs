using System;

namespace Parameters
{
    /// <summary>
    /// Represents the (non-operable) Items mask state for UI consistency.
    /// No OSC endpoint exists; used for display-only.
    /// </summary>
    public readonly struct ItemsToggle : IEquatable<ItemsToggle>
    {
        public readonly bool Value;

        public ItemsToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(ItemsToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is ItemsToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(ItemsToggle left, ItemsToggle right) => left.Equals(right);
        public static bool operator !=(ItemsToggle left, ItemsToggle right) => !left.Equals(right);

        public static implicit operator bool(ItemsToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

