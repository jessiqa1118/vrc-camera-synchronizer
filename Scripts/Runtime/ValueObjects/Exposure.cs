using System;
using UnityEngine;

namespace Parameters
{
    /// <summary>
    /// Represents camera exposure value for VRChat OSC control
    /// Range: -10 to 4, Default: 0
    /// </summary>
    public readonly struct Exposure : IEquatable<Exposure>
    {
        public const float MinValue = -10f;
        public const float MaxValue = 4f;
        public const float DefaultValue = 0f;

        public readonly float Value;

        public Exposure(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(Exposure other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Exposure other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Exposure left, Exposure right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Exposure left, Exposure right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(Exposure exposure)
        {
            return exposure.Value;
        }
    }
}