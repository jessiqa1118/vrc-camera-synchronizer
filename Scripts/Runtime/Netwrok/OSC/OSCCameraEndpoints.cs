using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public static class OSCCameraEndpoints
    {
        private const string BasePath = "/usercamera";

        // Mode
        public static readonly Address Mode = new($"{BasePath}/Mode");

        // Pose
        public static readonly Address Pose = new($"{BasePath}/Pose");

        // Actions
        public static readonly Address Close = new($"{BasePath}/Close");
        public static readonly Address Capture = new($"{BasePath}/Capture");
        public static readonly Address CaptureDelayed = new($"{BasePath}/CaptureDelayed");

        // Toggles
        public static readonly Address ShowUIInCamera = new($"{BasePath}/ShowUIInCamera");
        public static readonly Address Lock = new($"{BasePath}/Lock");
        public static readonly Address LocalPlayer = new($"{BasePath}/LocalPlayer");
        public static readonly Address RemotePlayer = new($"{BasePath}/RemotePlayer");
        public static readonly Address Environment = new($"{BasePath}/Environment");
        public static readonly Address GreenScreen = new($"{BasePath}/GreenScreen");
        public static readonly Address SmoothMovement = new($"{BasePath}/SmoothMovement");
        public static readonly Address LookAtMe = new($"{BasePath}/LookAtMe");
        public static readonly Address AutoLevelRoll = new($"{BasePath}/AutoLevelRoll");
        public static readonly Address AutoLevelPitch = new($"{BasePath}/AutoLevelPitch");
        public static readonly Address Flying = new($"{BasePath}/Flying");
        public static readonly Address TriggerTakesPhotos = new($"{BasePath}/TriggerTakesPhotos");
        public static readonly Address DollyPathsStayVisible = new($"{BasePath}/DollyPathsStayVisible");
        public static readonly Address CameraEars = new($"{BasePath}/CameraEars");
        public static readonly Address ShowFocus = new($"{BasePath}/ShowFocus");
        public static readonly Address Streaming = new($"{BasePath}/Streaming");
        public static readonly Address RollWhileFlying = new($"{BasePath}/RollWhileFlying");
        public static readonly Address OrientationIsLandscape = new($"{BasePath}/OrientationIsLandscape");

        // Sliders
        public static readonly Address Zoom = new($"{BasePath}/Zoom");
        public static readonly Address Exposure = new($"{BasePath}/Exposure");
        public static readonly Address FocalDistance = new($"{BasePath}/FocalDistance");
        public static readonly Address Aperture = new($"{BasePath}/Aperture");
        public static readonly Address Hue = new($"{BasePath}/Hue");
        public static readonly Address Saturation = new($"{BasePath}/Saturation");
        public static readonly Address Lightness = new($"{BasePath}/Lightness");
        public static readonly Address LookAtMeXOffset = new($"{BasePath}/LookAtMeXOffset");
        public static readonly Address LookAtMeYOffset = new($"{BasePath}/LookAtMeYOffset");
        public static readonly Address FlySpeed = new($"{BasePath}/FlySpeed");
        public static readonly Address TurnSpeed = new($"{BasePath}/TurnSpeed");
        public static readonly Address SmoothingStrength = new($"{BasePath}/SmoothingStrength");
        public static readonly Address PhotoRate = new($"{BasePath}/PhotoRate");
        public static readonly Address Duration = new($"{BasePath}/Duration");
    }
}
