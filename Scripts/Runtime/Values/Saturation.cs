using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera saturation value for VRChat OSC control
    /// Range: 0 to 100, Default: 100
    /// </summary>
    public readonly struct Saturation : IEquatable<Saturation>
    {
        public const float MinValue = 0f;
        public const float MaxValue = 100f;
        public const float DefaultValue = 100f;

        private readonly float _value;

        public Saturation(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"Saturation value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(Saturation other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is Saturation other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Saturation left, Saturation right) => left.Equals(right);
        public static bool operator !=(Saturation left, Saturation right) => !left.Equals(right);

        public static implicit operator float(Saturation saturation) => saturation._value;
    }
}
