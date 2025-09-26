using System;
using System.Linq;

namespace Astearium.Network.Osc
{
    public readonly struct Address : IEquatable<Address>
    {
        public readonly string Value;

        public Address(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Address cannot be null or whitespace.", nameof(value));
            }

            if (!value.StartsWith("/"))
            {
                throw new ArgumentException("OSC address must start with '/'.", nameof(value));
            }

            if (!IsValidOSCAddress(value))
            {
                throw new ArgumentException($"Invalid OSC address format: {value}", nameof(value));
            }

            Value = value;
        }

        private static bool IsValidOSCAddress(string address)
        {
            var invalidChars = new[] { ' ', '#', '*', ',', '?', '[', ']', '{', '}' };
            return !address.Any(c => invalidChars.Contains(c));
        }

        public bool Equals(Address other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Address other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Address address)
        {
            return address.Value;
        }

        public static bool operator ==(Address left, Address right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !left.Equals(right);
        }
    }
}
