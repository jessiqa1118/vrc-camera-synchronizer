using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera aperture value for VRChat OSC control
    /// Range: 1.4-32, Default: 15
    /// </summary>
    public readonly struct Aperture : IEquatable<Aperture>
    {
        public const float MinValue = 1.4f;
        public const float MaxValue = 32f;
        public const float DefaultValue = 15f;

        private readonly float _value;

        public Aperture(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"Aperture value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(Aperture other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is Aperture other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Aperture left, Aperture right) => left.Equals(right);
        public static bool operator !=(Aperture left, Aperture right) => !left.Equals(right);

        public static implicit operator float(Aperture aperture) => aperture._value;
    }
}
