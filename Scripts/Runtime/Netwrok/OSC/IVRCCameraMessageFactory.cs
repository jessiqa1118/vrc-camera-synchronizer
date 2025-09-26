using System.Collections.Generic;
using Astearium.Network.Osc;
using UnityEngine;

namespace Astearium.VRChat.Camera
{
    public interface IVRCCameraMessageFactory
    {
        IEnumerable<IOSCMessage> CreateZoom(Zoom value);
        IEnumerable<IOSCMessage> CreateExposure(Exposure value);
        IEnumerable<IOSCMessage> CreateFocalDistance(FocalDistance value);
        IEnumerable<IOSCMessage> CreateAperture(Aperture value);
        IEnumerable<IOSCMessage> CreateHue(Hue value);
        IEnumerable<IOSCMessage> CreateSaturation(Saturation value);
        IEnumerable<IOSCMessage> CreateLightness(Lightness value);
        IEnumerable<IOSCMessage> CreateLookAtMeOffset(LookAtMeOffset value);
        IEnumerable<IOSCMessage> CreateFlySpeed(FlySpeed value);
        IEnumerable<IOSCMessage> CreateTurnSpeed(TurnSpeed value);
        IEnumerable<IOSCMessage> CreateSmoothingStrength(SmoothingStrength value);
        IEnumerable<IOSCMessage> CreatePhotoRate(PhotoRate value);
        IEnumerable<IOSCMessage> CreateDuration(Duration value);
        IEnumerable<IOSCMessage> CreateShowUIInCamera(bool value);
        IEnumerable<IOSCMessage> CreateLock(bool value);
        IEnumerable<IOSCMessage> CreateLocalPlayer(bool value);
        IEnumerable<IOSCMessage> CreateRemotePlayer(bool value);
        IEnumerable<IOSCMessage> CreateEnvironment(bool value);
        IEnumerable<IOSCMessage> CreateGreenScreen(bool value);
        IEnumerable<IOSCMessage> CreateSmoothMovement(bool value);
        IEnumerable<IOSCMessage> CreateLookAtMe(bool value);
        IEnumerable<IOSCMessage> CreateAutoLevelRoll(bool value);
        IEnumerable<IOSCMessage> CreateAutoLevelPitch(bool value);
        IEnumerable<IOSCMessage> CreateFlying(bool value);
        IEnumerable<IOSCMessage> CreateTriggerTakesPhotos(bool value);
        IEnumerable<IOSCMessage> CreateDollyPathsStayVisible(bool value);
        IEnumerable<IOSCMessage> CreateCameraEars(bool value);
        IEnumerable<IOSCMessage> CreateShowFocus(bool value);
        IEnumerable<IOSCMessage> CreateStreaming(bool value);
        IEnumerable<IOSCMessage> CreateRollWhileFlying(bool value);
        IEnumerable<IOSCMessage> CreateOrientation(Orientation value);
        IEnumerable<IOSCMessage> CreatePose(Pose value);
        IEnumerable<IOSCMessage> CreateMode(Mode value);
    }
}
