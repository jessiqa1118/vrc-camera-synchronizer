using System;
using UnityEngine;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera smoothing strength value for VRChat OSC control
    /// Range: 0.1-10, Default: 5
    /// </summary>
    public readonly struct SmoothingStrength : IEquatable<SmoothingStrength>
    {
        public const float MinValue = 0.1f;
        public const float MaxValue = 10f;
        public const float DefaultValue = 5f;

        public readonly float Value;

        public SmoothingStrength(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(SmoothingStrength other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is SmoothingStrength other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(SmoothingStrength left, SmoothingStrength right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SmoothingStrength left, SmoothingStrength right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(SmoothingStrength smoothingStrength)
        {
            return smoothingStrength.Value;
        }
    }
}
