using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera duration value for VRChat OSC control
    /// Range: 0-10, Default: 2
    /// </summary>
    public readonly struct Duration : IEquatable<Duration>
    {
        public const float MinValue = 0f;
        public const float MaxValue = 10f;
        public const float DefaultValue = 2f;

        private readonly float _value;

        public Duration(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"Duration value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(Duration other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is Duration other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Duration left, Duration right) => left.Equals(right);
        public static bool operator !=(Duration left, Duration right) => !left.Equals(right);

        public static implicit operator float(Duration duration) => duration._value;
    }
}
