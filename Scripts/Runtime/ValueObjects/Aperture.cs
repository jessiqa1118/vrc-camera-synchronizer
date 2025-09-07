using System;
using UnityEngine;

namespace JessiQa
{
    /// <summary>
    /// Represents camera aperture value for VRChat OSC control
    /// Range: 1.4-32, Default: 15
    /// </summary>
    public readonly struct Aperture : IEquatable<Aperture>
    {
        public const float MinValue = 1.4f;
        public const float MaxValue = 32f;
        public const float DefaultValue = 15f;

        public readonly float Value;

        public Aperture(float value)
        {
            // Clamp value to valid range
            Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool Equals(Aperture other)
        {
            return Mathf.Approximately(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Aperture other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Aperture left, Aperture right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Aperture left, Aperture right)
        {
            return !left.Equals(right);
        }

        public static implicit operator float(Aperture aperture)
        {
            return aperture.Value;
        }
    }
}