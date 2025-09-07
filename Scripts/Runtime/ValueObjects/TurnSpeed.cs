using System;
using UnityEngine;

namespace Parameters
{
    /// <summary>
    /// Represents camera turn speed value for VRChat OSC control
    /// Range: 0.1-5, Default: 1
    /// </summary>
    public readonly struct TurnSpeed : IEquatable<TurnSpeed>
    {
        public const float MinValue = 0.1f;
        public const float MaxValue = 5f;
        public const float DefaultValue = 1f;

        public readonly float Value;

        public TurnSpeed(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(TurnSpeed other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is TurnSpeed other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(TurnSpeed left, TurnSpeed right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TurnSpeed left, TurnSpeed right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(TurnSpeed turnSpeed)
        {
            return turnSpeed.Value;
        }
    }
}