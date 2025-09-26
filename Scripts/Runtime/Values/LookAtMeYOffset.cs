using System;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera LookAtMe Y offset value for VRChat OSC control
    /// Range: -25 to 25, Default: 0
    /// </summary>
    public readonly struct LookAtMeYOffset : IEquatable<LookAtMeYOffset>
    {
        public const float MinValue = -25f;
        public const float MaxValue = 25f;
        public const float DefaultValue = 0f;

        private readonly float _value;

        public LookAtMeYOffset(float value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"LookAtMeYOffset value must be between {MinValue} and {MaxValue}.");
            }

            _value = value;
        }

        public bool Equals(LookAtMeYOffset other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is LookAtMeYOffset other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(LookAtMeYOffset left, LookAtMeYOffset right) => left.Equals(right);
        public static bool operator !=(LookAtMeYOffset left, LookAtMeYOffset right) => !left.Equals(right);

        public static implicit operator float(LookAtMeYOffset offset) => offset._value;
    }
}
