using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera photo rate value for VRChat OSC control
    /// Range: 0.1-2, Default: 1
    /// </summary>
    public readonly struct PhotoRate : IEquatable<PhotoRate>
    {
        public const float MinValue = 0.1f;
        public const float MaxValue = 2f;
        public const float DefaultValue = 1f;

        private readonly float _value;

        public PhotoRate(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"PhotoRate value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(PhotoRate other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is PhotoRate other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(PhotoRate left, PhotoRate right) => left.Equals(right);
        public static bool operator !=(PhotoRate left, PhotoRate right) => !left.Equals(right);

        public static implicit operator float(PhotoRate photoRate) => photoRate._value;
    }
}
