using System;

namespace JessiQa
{
    public readonly struct Argument : IEquatable<Argument>
    {
        public enum ValueType
        {
            Int32,
            Float32,
            String,
            Blob,
            Bool,
        }

        public readonly object Value;
        public readonly ValueType Type;

        public Argument(int value)
        {
            Value = value;
            Type = ValueType.Int32;
        }

        public Argument(float value)
        {
            Value = value;
            Type = ValueType.Float32;
        }

        public Argument(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Type = ValueType.String;
        }

        public Argument(byte[] value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Type = ValueType.Blob;
        }

        public Argument(bool value)
        {
            Value = value;
            Type = ValueType.Bool;
        }

        public int AsInt32()
        {
            return Type switch
            {
                ValueType.Int32 => (int)Value,
                ValueType.Float32 => (int)(float)Value,
                _ => throw new InvalidCastException($"Cannot convert {Type} to Int32")
            };
        }

        public float AsFloat32()
        {
            return Type switch
            {
                ValueType.Float32 => (float)Value,
                ValueType.Int32 => (int)Value,
                _ => throw new InvalidCastException($"Cannot convert {Type} to Float32")
            };
        }

        public string AsString()
        {
            return Type switch
            {
                ValueType.String => (string)Value,
                _ => Value?.ToString() ?? string.Empty
            };
        }

        public byte[] AsBlob()
        {
            return Type switch
            {
                ValueType.Blob => (byte[])Value,
                _ => throw new InvalidCastException($"Cannot convert {Type} to Blob")
            };
        }

        public bool AsBool()
        {
            return Type switch
            {
                ValueType.Bool => (bool)Value,
                ValueType.Int32 => (int)Value != 0,
                _ => throw new InvalidCastException($"Cannot convert {Type} to Bool")
            };
        }


        public override string ToString()
        {
            return $"{Type}: {Value}";
        }

        public bool Equals(Argument other)
        {
            return Type == other.Type && Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Argument other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Value);
        }

        public static bool operator ==(Argument left, Argument right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Argument left, Argument right)
        {
            return !left.Equals(right);
        }
    }
}