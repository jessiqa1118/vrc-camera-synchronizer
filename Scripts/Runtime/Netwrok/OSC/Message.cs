using System;

namespace Astearium.Network.Osc
{
    public readonly struct Message : IOSCMessage

    {
        public Address Address { get; }
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public Message(Address address, Argument[] arguments)
        {
            Address = address;
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
            TypeTag = new TypeTag(arguments);
        }
    }
}
