using System.Collections.Generic;
using Astearium.Network.Osc;
using UnityEngine;

namespace Astearium.VRChat.Camera
{
    public class VRCCameraMessageFactory : IVRCCameraMessageFactory
    {
        public IEnumerable<IOSCMessage> CreateZoom(Zoom value)
        {
            yield return new ZoomOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateExposure(Exposure value)
        {
            yield return new ExposureOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateFocalDistance(FocalDistance value)
        {
            yield return new FocalDistanceOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateAperture(Aperture value)
        {
            yield return new ApertureOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateHue(Hue value)
        {
            yield return new HueOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateSaturation(Saturation value)
        {
            yield return new SaturationOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateLightness(Lightness value)
        {
            yield return new LightnessOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateLookAtMeOffset(LookAtMeOffset value)
        {
            yield return new LookAtMeXOffsetOscMessage(value.X);
            yield return new LookAtMeYOffsetOscMessage(value.Y);
        }

        public IEnumerable<IOSCMessage> CreateFlySpeed(FlySpeed value)
        {
            yield return new FlySpeedOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateTurnSpeed(TurnSpeed value)
        {
            yield return new TurnSpeedOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateSmoothingStrength(SmoothingStrength value)
        {
            yield return new SmoothingStrengthOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreatePhotoRate(PhotoRate value)
        {
            yield return new PhotoRateOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateDuration(Duration value)
        {
            yield return new DurationOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateShowUIInCamera(bool value)
        {
            yield return new ShowUIInCameraToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateLock(bool value)
        {
            yield return new LockToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateLocalPlayer(bool value)
        {
            yield return new LocalPlayerToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateRemotePlayer(bool value)
        {
            yield return new RemotePlayerToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateEnvironment(bool value)
        {
            yield return new EnvironmentToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateGreenScreen(bool value)
        {
            yield return new GreenScreenToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateSmoothMovement(bool value)
        {
            yield return new SmoothMovementToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateLookAtMe(bool value)
        {
            yield return new LookAtMeToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateAutoLevelRoll(bool value)
        {
            yield return new AutoLevelRollToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateAutoLevelPitch(bool value)
        {
            yield return new AutoLevelPitchToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateFlying(bool value)
        {
            yield return new FlyingToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateTriggerTakesPhotos(bool value)
        {
            yield return new TriggerTakesPhotosToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateDollyPathsStayVisible(bool value)
        {
            yield return new DollyPathsStayVisibleToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateCameraEars(bool value)
        {
            yield return new CameraEarsToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateShowFocus(bool value)
        {
            yield return new ShowFocusToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateStreaming(bool value)
        {
            yield return new StreamingToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateRollWhileFlying(bool value)
        {
            yield return new RollWhileFlyingToggleOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateOrientation(Orientation value)
        {
            var isLandscape = value == Orientation.Landscape;
            yield return new Message(OSCCameraEndpoints.OrientationIsLandscape, new[] { new Argument(isLandscape) });
        }

        public IEnumerable<IOSCMessage> CreatePose(Pose value)
        {
            yield return new PoseOscMessage(value);
        }

        public IEnumerable<IOSCMessage> CreateMode(Mode value)
        {
            yield return new ModeOscMessage(value);
        }
    }
}
