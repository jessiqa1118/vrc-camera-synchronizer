using System;

namespace Parameters
{
    /// <summary>
    /// Represents enabling/disabling Camera Ears (use avatar ears for audio) in the user camera
    /// </summary>
    public readonly struct CameraEarsToggle : IEquatable<CameraEarsToggle>
    {
        public readonly bool Value;

        public CameraEarsToggle(bool value)
        {
            Value = value;
        }

        public bool Equals(CameraEarsToggle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is CameraEarsToggle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(CameraEarsToggle left, CameraEarsToggle right) => left.Equals(right);
        public static bool operator !=(CameraEarsToggle left, CameraEarsToggle right) => !left.Equals(right);

        public static implicit operator bool(CameraEarsToggle toggle) => toggle.Value;
        public override string ToString() => Value.ToString();
    }
}

