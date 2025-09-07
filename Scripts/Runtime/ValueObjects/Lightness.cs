using System;
using UnityEngine;

namespace JessiQa
{
    /// <summary>
    /// Represents camera lightness value for VRChat OSC control (GreenScreen)
    /// Range: 0-50, Default: 50
    /// </summary>
    public readonly struct Lightness : IEquatable<Lightness>
    {
        public const float MinValue = 0f;
        public const float MaxValue = 50f;
        public const float DefaultValue = 50f;

        public readonly float Value;

        public Lightness(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(Lightness other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Lightness other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Lightness left, Lightness right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Lightness left, Lightness right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(Lightness lightness)
        {
            return lightness.Value;
        }
    }
}