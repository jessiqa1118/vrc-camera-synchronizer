using System;

namespace JessiQa
{
    public readonly struct Zoom : IEquatable<Zoom>
    {
        public const float MinValue = 20f;
        public const float MaxValue = 150f;

        public readonly float Value;

        public Zoom(float value, bool clamp = true)
        {
            Value = clamp switch
            {
                true => Math.Clamp(value, MinValue, MaxValue),
                false when value is < MinValue or > MaxValue =>
                    throw new ArgumentOutOfRangeException(
                        nameof(value), $"Zoom value must be between {MinValue} and {MaxValue}."),
                _ => value
            };
        }

        public bool Equals(Zoom other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Zoom other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}