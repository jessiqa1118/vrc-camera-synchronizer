using System;
using UnityEngine;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents photo rate value for VRChat OSC control
    /// Range: 0.1-2, Default: 1
    /// </summary>
    public readonly struct PhotoRate : IEquatable<PhotoRate>
    {
        public const float MinValue = 0.1f;
        public const float MaxValue = 2f;
        public const float DefaultValue = 1f;

        public readonly float Value;

        public PhotoRate(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(PhotoRate other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is PhotoRate other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(PhotoRate left, PhotoRate right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PhotoRate left, PhotoRate right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(PhotoRate photoRate)
        {
            return photoRate.Value;
        }
    }
}