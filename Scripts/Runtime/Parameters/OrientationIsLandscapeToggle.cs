using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling landscape orientation for the user camera
    /// </summary>
    public readonly struct OrientationIsLandscapeToggle : IEquatable<OrientationIsLandscapeToggle>
    {
        public readonly bool Value;

        public OrientationIsLandscapeToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(OrientationIsLandscapeToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is OrientationIsLandscapeToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(OrientationIsLandscapeToggle left, OrientationIsLandscapeToggle right) => left.Equals(right);
        public static bool operator !=(OrientationIsLandscapeToggle left, OrientationIsLandscapeToggle right) => !left.Equals(right);

        public static implicit operator bool(OrientationIsLandscapeToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

