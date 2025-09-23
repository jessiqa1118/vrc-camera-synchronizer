namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// User camera mode for VRChat OSC control.
    /// Values map to /usercamera/Mode indices 0–6.
    /// </summary>
    public enum Mode
    {
        /// <summary>0: Camera is Off</summary>
        Off = 0,
        /// <summary>1: Photo mode</summary>
        Photo = 1,
        /// <summary>2: Stream mode</summary>
        Stream = 2,
        /// <summary>3: Emoji mode</summary>
        Emoji = 3,
        /// <summary>4: Multilayer mode</summary>
        Multilayer = 4,
        /// <summary>5: Print mode</summary>
        Print = 5,
        /// <summary>6: Drone mode</summary>
        Drone = 6
    }
}
