using System;
using UnityEngine;

namespace Astearium.Network
{
    /// <summary>
    /// Strongly typed representation of a UDP port used by the synchronizer.
    /// </summary>
    [Serializable]
    public struct PortNumber : IEquatable<PortNumber>
    {
        public const int MinValue = 1;
        public const int MaxValue = 65535;

        [SerializeField] private int value;

        public PortNumber(int value)
        {
            if (value is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value,
                    $"Port number must be between {MinValue} and {MaxValue}.");
            }

            this.value = value;
        }

        public int Value => value;

        public bool Equals(PortNumber other)
        {
            return value == other.value;
        }

        public override bool Equals(object obj)
        {
            return obj is PortNumber other && Equals(other);
        }

        public override int GetHashCode()
        {
            return value;
        }

        public static bool operator ==(PortNumber left, PortNumber right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PortNumber left, PortNumber right)
        {
            return !left.Equals(right);
        }

        public static implicit operator int(PortNumber port)
        {
            return port.value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
