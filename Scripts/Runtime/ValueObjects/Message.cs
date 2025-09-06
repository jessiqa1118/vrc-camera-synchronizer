using System;

namespace JessiQa
{
    public readonly struct Message
    {
        public readonly Address Address;
        public readonly Argument[] Arguments;
        public readonly TypeTag TypeTag;

        public Message(Address address, Argument[] arguments)
        {
            Address = address;
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
            TypeTag = new TypeTag(arguments);
        }
    }
}