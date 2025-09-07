using System;
using UnityEngine;

namespace JessiQa
{
    /// <summary>
    /// Represents camera zoom (focal length) value for VRChat OSC control
    /// Range: 20-150, Default: 45
    /// </summary>
    public readonly struct Zoom : IEquatable<Zoom>
    {
        public const float MinValue = 20f;
        public const float MaxValue = 150f;
        public const float DefaultValue = 45f;

        public readonly float Value;

        public Zoom(float value, bool clamp)
        {
            Value = clamp switch
            {
                true => Mathf.Clamp(value, MinValue, MaxValue),
                false when value is < MinValue or > MaxValue =>
                    throw new ArgumentOutOfRangeException(
                        nameof(value), $"Zoom value must be between {MinValue} and {MaxValue}."),
                _ => value
            };
        }

        public bool Equals(Zoom other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Zoom other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Zoom left, Zoom right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Zoom left, Zoom right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(Zoom zoom)
        {
            return zoom.Value;
        }
    }
}