using System;
using UnityEngine;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents camera LookAtMe Y offset value for VRChat OSC control
    /// Range: -25 to 25, Default: 0
    /// </summary>
    public readonly struct LookAtMeYOffset : IEquatable<LookAtMeYOffset>
    {
        public const float MinValue = -25f;
        public const float MaxValue = 25f;
        public const float DefaultValue = 0f;

        public readonly float Value;

        public LookAtMeYOffset(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(LookAtMeYOffset other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is LookAtMeYOffset other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(LookAtMeYOffset left, LookAtMeYOffset right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LookAtMeYOffset left, LookAtMeYOffset right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(LookAtMeYOffset offset)
        {
            return offset.Value;
        }
    }
}
