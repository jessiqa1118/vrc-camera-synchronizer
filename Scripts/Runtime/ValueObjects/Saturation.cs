using System;
using UnityEngine;

namespace JessiQa
{
    /// <summary>
    /// Represents camera saturation value for VRChat OSC control (GreenScreen)
    /// Range: 0-100, Default: 100
    /// </summary>
    public readonly struct Saturation : IEquatable<Saturation>
    {
        public const float MinValue = 0f;
        public const float MaxValue = 100f;
        public const float DefaultValue = 100f;

        public readonly float Value;

        public Saturation(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(Saturation other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Saturation other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Saturation left, Saturation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Saturation left, Saturation right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(Saturation saturation)
        {
            return saturation.Value;
        }
    }
}