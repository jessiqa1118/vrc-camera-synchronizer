using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera zoom (focal length) value for VRChat OSC control
    /// Range: 20-150, Default: 45
    /// </summary>
    public readonly struct Zoom : IEquatable<Zoom>
    {
        public const float MinValue = 20f;
        public const float MaxValue = 150f;
        public const float DefaultValue = 45f;

        private readonly float _value;

        public Zoom(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"Zoom value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(Zoom other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is Zoom other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Zoom left, Zoom right) => left.Equals(right);
        public static bool operator !=(Zoom left, Zoom right) => !left.Equals(right);

        public static implicit operator float(Zoom zoom) => zoom._value;
    }
}