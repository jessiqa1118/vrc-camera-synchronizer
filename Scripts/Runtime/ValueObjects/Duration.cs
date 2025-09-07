using System;
using UnityEngine;

namespace Parameters
{
    /// <summary>
    /// Represents duration value for VRChat OSC control
    /// Range: 0.1-60, Default: 2
    /// </summary>
    public readonly struct Duration : IEquatable<Duration>
    {
        public const float MinValue = 0.1f;
        public const float MaxValue = 60f;
        public const float DefaultValue = 2f;

        public readonly float Value;

        public Duration(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(Duration other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Duration other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Duration left, Duration right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Duration left, Duration right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(Duration duration)
        {
            return duration.Value;
        }
    }
}