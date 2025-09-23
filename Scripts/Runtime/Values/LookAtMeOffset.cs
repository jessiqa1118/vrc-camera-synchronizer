using System;
using UnityEngine;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Represents LookAtMe offset values for VRChat OSC control
    /// </summary>
    public readonly struct LookAtMeOffset : IEquatable<LookAtMeOffset>
    {
        public readonly LookAtMeXOffset X;
        public readonly LookAtMeYOffset Y;

        public LookAtMeOffset(LookAtMeXOffset x, LookAtMeYOffset y)
        {
            X = x;
            Y = y;
        }
        
        public LookAtMeOffset(float x, float y) : this(new LookAtMeXOffset(x), new LookAtMeYOffset(y))
        {
        }
        
        public LookAtMeOffset(Vector2 offset) : this(new LookAtMeXOffset(offset.x), new LookAtMeYOffset(offset.y))
        {
        }

        public Vector2 ToVector2() => new Vector2(X, Y);

        public bool Equals(LookAtMeOffset other)
        {
            return Mathf.Approximately(X, other.X) && Mathf.Approximately(Y, other.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is LookAtMeOffset other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public static bool operator ==(LookAtMeOffset left, LookAtMeOffset right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LookAtMeOffset left, LookAtMeOffset right)
        {
            return !left.Equals(right);
        }

        public static implicit operator Vector2(LookAtMeOffset offset)
        {
            return offset.ToVector2();
        }
    }
}
