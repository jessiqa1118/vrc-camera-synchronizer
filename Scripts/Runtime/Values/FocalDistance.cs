using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera focal distance value for VRChat OSC control
    /// Range: 0-10, Default: 1.5
    /// </summary>
    public readonly struct FocalDistance : IEquatable<FocalDistance>
    {
        public const float MinValue = 0f;
        public const float MaxValue = 10f;
        public const float DefaultValue = 1.5f;

        private readonly float _value;

        public FocalDistance(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"FocalDistance value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(FocalDistance other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is FocalDistance other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(FocalDistance left, FocalDistance right) => left.Equals(right);
        public static bool operator !=(FocalDistance left, FocalDistance right) => !left.Equals(right);

        public static implicit operator float(FocalDistance focalDistance) => focalDistance._value;
    }
}
