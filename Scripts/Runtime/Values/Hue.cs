using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera hue value for VRChat OSC control
    /// Range: 0 to 360, Default: 120
    /// </summary>
    public readonly struct Hue : IEquatable<Hue>
    {
        public const float MinValue = 0f;
        public const float MaxValue = 360f;
        public const float DefaultValue = 120f;

        private readonly float _value;

        public Hue(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"Hue value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(Hue other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is Hue other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Hue left, Hue right) => left.Equals(right);
        public static bool operator !=(Hue left, Hue right) => !left.Equals(right);

        public static implicit operator float(Hue hue) => hue._value;
    }
}
