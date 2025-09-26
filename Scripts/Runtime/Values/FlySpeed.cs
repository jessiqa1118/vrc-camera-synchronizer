using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera fly speed value for VRChat OSC control
    /// Range: 0.1-15, Default: 3
    /// </summary>
    public readonly struct FlySpeed : IEquatable<FlySpeed>
    {
        public const float MinValue = 0.1f;
        public const float MaxValue = 15f;
        public const float DefaultValue = 3f;

        private readonly float _value;

        public FlySpeed(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"FlySpeed value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(FlySpeed other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is FlySpeed other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(FlySpeed left, FlySpeed right) => left.Equals(right);
        public static bool operator !=(FlySpeed left, FlySpeed right) => !left.Equals(right);

        public static implicit operator float(FlySpeed flySpeed) => flySpeed._value;
    }
}
