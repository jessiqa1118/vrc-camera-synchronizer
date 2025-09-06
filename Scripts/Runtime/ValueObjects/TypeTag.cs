using System;
using System.Linq;

namespace JessiQa
{
    public readonly struct TypeTag
    {
        public readonly string Value;

        public TypeTag(Argument[] arguments)
        {
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));

            Value = string.Concat(arguments.Select(arg => arg.Type switch
            {
                Argument.ValueType.Int32 => 'i',
                Argument.ValueType.Float32 => 'f',
                Argument.ValueType.String => 's',
                Argument.ValueType.Blob => 'b',
                Argument.ValueType.Bool => (bool)arg.Value ? 'T' : 'F',
                _ => throw new InvalidOperationException($"Unsupported argument type: {arg.Type}")
            }));
        }
    }
}