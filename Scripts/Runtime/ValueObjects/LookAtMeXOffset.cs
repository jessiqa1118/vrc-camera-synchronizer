using System;
using UnityEngine;

namespace JessiQa
{
    /// <summary>
    /// Represents camera LookAtMe X offset value for VRChat OSC control
    /// Range: -25 to 25, Default: 0
    /// </summary>
    public readonly struct LookAtMeXOffset : IEquatable<LookAtMeXOffset>
    {
        public const float MinValue = -25f;
        public const float MaxValue = 25f;
        public const float DefaultValue = 0f;

        public readonly float Value;

        public LookAtMeXOffset(float value = DefaultValue)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(LookAtMeXOffset other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is LookAtMeXOffset other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(LookAtMeXOffset left, LookAtMeXOffset right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LookAtMeXOffset left, LookAtMeXOffset right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(LookAtMeXOffset offset)
        {
            return offset.Value;
        }
    }
}