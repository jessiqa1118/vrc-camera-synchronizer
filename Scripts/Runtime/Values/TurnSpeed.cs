using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera turn speed value for VRChat OSC control
    /// Range: 0.1-15, Default: 3
    /// </summary>
    public readonly struct TurnSpeed : IEquatable<TurnSpeed>
    {
        public const float MinValue = 0.1f;
        public const float MaxValue = 15f;
        public const float DefaultValue = 3f;

        private readonly float _value;

        public TurnSpeed(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"TurnSpeed value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(TurnSpeed other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is TurnSpeed other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(TurnSpeed left, TurnSpeed right) => left.Equals(right);
        public static bool operator !=(TurnSpeed left, TurnSpeed right) => !left.Equals(right);

        public static implicit operator float(TurnSpeed turnSpeed) => turnSpeed._value;
    }
}
