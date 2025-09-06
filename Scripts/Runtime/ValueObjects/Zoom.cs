using System;

namespace JessiQa
{
    public readonly struct Zoom
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
    }
}