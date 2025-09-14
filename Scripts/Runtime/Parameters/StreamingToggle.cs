using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling Streaming mode for the user camera
    /// </summary>
    public readonly struct StreamingToggle : IEquatable<StreamingToggle>
    {
        public readonly bool Value;

        public StreamingToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(StreamingToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is StreamingToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(StreamingToggle left, StreamingToggle right) => left.Equals(right);
        public static bool operator !=(StreamingToggle left, StreamingToggle right) => !left.Equals(right);

        public static implicit operator bool(StreamingToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

