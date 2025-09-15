using System;

namespace Parameters
{
    /// <summary>
    /// Represents a 3D pose composed of position and euler rotation (degrees).
    /// </summary>
    public readonly struct Pose : IEquatable<Pose>
    {
        public readonly Position Pos;
        public readonly Rotation Rot;

        public Pose(Position position, Rotation rotation)
        {
            Pos = position;
            Rot = rotation;
        }

        public bool Equals(Pose other) => Pos.Equals(other.Pos) && Rot.Equals(other.Rot);
        public override bool Equals(object obj) => obj is Pose other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Pos, Rot);
        public static bool operator ==(Pose left, Pose right) => left.Equals(right);
        public static bool operator !=(Pose left, Pose right) => !left.Equals(right);

        /// <summary>
        /// Position component (meters), immutable value type.
        /// </summary>
        public readonly struct Position : IEquatable<Position>
        {
            public readonly float X;
            public readonly float Y;
            public readonly float Z;

            public Position(float x, float y, float z)
            {
                X = x; Y = y; Z = z;
            }

            public bool Equals(Position other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
            public override bool Equals(object obj) => obj is Position other && Equals(other);
            public override int GetHashCode() => HashCode.Combine(X, Y, Z);
            public static bool operator ==(Position left, Position right) => left.Equals(right);
            public static bool operator !=(Position left, Position right) => !left.Equals(right);
        }

        /// <summary>
        /// Rotation component in euler angles (degrees), immutable value type.
        /// </summary>
        public readonly struct Rotation : IEquatable<Rotation>
        {
            public readonly float X;
            public readonly float Y;
            public readonly float Z;

            public Rotation(float x, float y, float z)
            {
                X = x; Y = y; Z = z;
            }

            public bool Equals(Rotation other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
            public override bool Equals(object obj) => obj is Rotation other && Equals(other);
            public override int GetHashCode() => HashCode.Combine(X, Y, Z);
            public static bool operator ==(Rotation left, Rotation right) => left.Equals(right);
            public static bool operator !=(Rotation left, Rotation right) => !left.Equals(right);
        }
    }
}

