using System;
using UnityEngine;

namespace Parameters
{
    /// <summary>
    /// Represents camera fly speed value for VRChat OSC control
    /// Range: 0.1-15, Default: 3
    /// </summary>
    public readonly struct FlySpeed : IEquatable<FlySpeed>
    {
        public const float MinValue = 0.1f;
        public const float MaxValue = 15f;
        public const float DefaultValue = 3f;

        public readonly float Value;

        public FlySpeed(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(FlySpeed other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is FlySpeed other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(FlySpeed left, FlySpeed right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FlySpeed left, FlySpeed right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(FlySpeed flySpeed)
        {
            return flySpeed.Value;
        }
    }
}