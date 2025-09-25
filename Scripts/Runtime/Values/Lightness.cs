using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera lightness value for VRChat OSC control
    /// Range: 0 to 100, Default: 60
    /// </summary>
    public readonly struct Lightness : IEquatable<Lightness>
    {
        public const float MinValue = 0f;
        public const float MaxValue = 100f;
        public const float DefaultValue = 60f;

        private readonly float _value;

        public Lightness(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"Lightness value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(Lightness other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is Lightness other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Lightness left, Lightness right) => left.Equals(right);
        public static bool operator !=(Lightness left, Lightness right) => !left.Equals(right);

        public static implicit operator float(Lightness lightness) => lightness._value;
    }
}
