using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera smoothing strength value for VRChat OSC control
    /// Range: 0.1-10, Default: 5
    /// </summary>
    public readonly struct SmoothingStrength : IEquatable<SmoothingStrength>
    {
        public const float MinValue = 0.1f;
        public const float MaxValue = 10f;
        public const float DefaultValue = 5f;

        private readonly float _value;

        public SmoothingStrength(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"SmoothingStrength value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(SmoothingStrength other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is SmoothingStrength other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(SmoothingStrength left, SmoothingStrength right) => left.Equals(right);
        public static bool operator !=(SmoothingStrength left, SmoothingStrength right) => !left.Equals(right);

        public static implicit operator float(SmoothingStrength smoothingStrength) => smoothingStrength._value;
    }
}
