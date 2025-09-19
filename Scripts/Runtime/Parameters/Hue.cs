using System;
using UnityEngine;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera hue value for VRChat OSC control
    /// Range: 0 to 360, Default: 120
    /// </summary>
    public readonly struct Hue : IEquatable<Hue>
    {
        public const float MinValue = 0f;
        public const float MaxValue = 360f;
        public const float DefaultValue = 120f;

        public readonly float Value;

        public Hue(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(Hue other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Hue other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Hue left, Hue right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Hue left, Hue right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(Hue hue)
        {
            return hue.Value;
        }
    }
}