using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera exposure value for VRChat OSC control
    /// Range: -10 to 4, Default: 0
    /// </summary>
    public readonly struct Exposure : IEquatable<Exposure>
    {
        public const float MinValue = -10f;
        public const float MaxValue = 4f;
        public const float DefaultValue = 0f;

        private readonly float _value;

        public Exposure(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"Exposure value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(Exposure other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is Exposure other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Exposure left, Exposure right) => left.Equals(right);
        public static bool operator !=(Exposure left, Exposure right) => !left.Equals(right);

        public static implicit operator float(Exposure exposure) => exposure._value;
    }
}
