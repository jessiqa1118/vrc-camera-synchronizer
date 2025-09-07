using System;
using UnityEngine;

namespace JessiQa
{
    /// <summary>
    /// Represents camera focal distance value for VRChat OSC control
    /// Range: 0-10, Default: 1.5
    /// </summary>
    public readonly struct FocalDistance : IEquatable<FocalDistance>
    {
        public const float MinValue = 0f;
        public const float MaxValue = 10f;
        public const float DefaultValue = 1.5f;

        public readonly float Value;

        public FocalDistance(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(FocalDistance other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is FocalDistance other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(FocalDistance left, FocalDistance right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FocalDistance left, FocalDistance right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(FocalDistance focalDistance)
        {
            return focalDistance.Value;
        }
    }
}