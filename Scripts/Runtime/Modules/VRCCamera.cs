using System;
using UnityEngine;

namespace Astearium.VRChat.Camera
{
    public class VRCCamera : IDisposable
    {
        private readonly UnityEngine.Camera _camera;

        public ReactiveProperty<Zoom> Zoom { get; }
        public ReactiveProperty<Exposure> Exposure { get; }
        public ReactiveProperty<FocalDistance> FocalDistance { get; }
        public ReactiveProperty<Aperture> Aperture { get; }
        public ReactiveProperty<Hue> Hue { get; }
        public ReactiveProperty<Saturation> Saturation { get; }
        public ReactiveProperty<Lightness> Lightness { get; }
        public ReactiveProperty<LookAtMeOffset> LookAtMeOffset { get; }
        public ReactiveProperty<FlySpeed> FlySpeed { get; }
        public ReactiveProperty<TurnSpeed> TurnSpeed { get; }
        public ReactiveProperty<SmoothingStrength> SmoothingStrength { get; }
        public ReactiveProperty<PhotoRate> PhotoRate { get; }
        public ReactiveProperty<Duration> Duration { get; }

        /// <summary>
        /// Current world-space pose (position + rotation) for OSC sync
        /// </summary>
        public ReactiveProperty<Pose> Pose { get; }

        public ReactiveProperty<bool> ShowUIInCamera { get; }
        public ReactiveProperty<bool> Lock { get; }
        public ReactiveProperty<bool> LocalPlayer { get; }
        public ReactiveProperty<bool> RemotePlayer { get; }
        public ReactiveProperty<bool> Environment { get; }
        public ReactiveProperty<bool> GreenScreen { get; }
        public ReactiveProperty<bool> SmoothMovement { get; }
        public ReactiveProperty<bool> LookAtMe { get; }
        public ReactiveProperty<bool> AutoLevelRoll { get; }
        public ReactiveProperty<bool> AutoLevelPitch { get; }
        public ReactiveProperty<bool> Flying { get; }
        public ReactiveProperty<bool> TriggerTakesPhotos { get; }
        public ReactiveProperty<bool> DollyPathsStayVisible { get; }
        public ReactiveProperty<bool> CameraEars { get; }
        public ReactiveProperty<bool> ShowFocus { get; }
        public ReactiveProperty<bool> Streaming { get; }
        public ReactiveProperty<bool> RollWhileFlying { get; }
        public ReactiveProperty<Orientation> Orientation { get; }
        public ReactiveProperty<bool> Items { get; }

        /// <summary>
        /// Current camera <see cref="Mode"/>; changes are published over OSC.
        /// </summary>
        public ReactiveProperty<Mode> Mode { get; }

        public VRCCamera(UnityEngine.Camera camera)
        {
            _camera = camera ?? throw new ArgumentNullException(nameof(camera));

            // Initialize reactive properties
            Zoom = new ReactiveProperty<Zoom>(new Zoom(_camera.focalLength, true));
            Exposure = new ReactiveProperty<Exposure>(new Exposure(Astearium.VRChat.Camera.Exposure.DefaultValue));
            FocalDistance = new ReactiveProperty<FocalDistance>(new FocalDistance(_camera.focusDistance));
            Aperture = new ReactiveProperty<Aperture>(new Aperture(_camera.aperture));
            Hue = new ReactiveProperty<Hue>(new Hue(Astearium.VRChat.Camera.Hue.DefaultValue));
            Saturation =
                new ReactiveProperty<Saturation>(new Saturation(Astearium.VRChat.Camera.Saturation.DefaultValue));
            Lightness = new ReactiveProperty<Lightness>(new Lightness(Astearium.VRChat.Camera.Lightness.DefaultValue));
            LookAtMeOffset = new ReactiveProperty<LookAtMeOffset>(new LookAtMeOffset(
                new LookAtMeXOffset(LookAtMeXOffset.DefaultValue),
                new LookAtMeYOffset(LookAtMeYOffset.DefaultValue)));
            FlySpeed = new ReactiveProperty<FlySpeed>(new FlySpeed(Astearium.VRChat.Camera.FlySpeed.DefaultValue));
            TurnSpeed = new ReactiveProperty<TurnSpeed>(new TurnSpeed(Astearium.VRChat.Camera.TurnSpeed.DefaultValue));
            SmoothingStrength =
                new ReactiveProperty<SmoothingStrength>(
                    new SmoothingStrength(Astearium.VRChat.Camera.SmoothingStrength.DefaultValue));
            PhotoRate = new ReactiveProperty<PhotoRate>(new PhotoRate(Astearium.VRChat.Camera.PhotoRate.DefaultValue));
            Duration = new ReactiveProperty<Duration>(new Duration(Astearium.VRChat.Camera.Duration.DefaultValue));
            Pose = new ReactiveProperty<Pose>(new Pose(_camera.transform.position, _camera.transform.rotation));
            ShowUIInCamera = new ReactiveProperty<bool>(false);
            Lock = new ReactiveProperty<bool>(false);
            LocalPlayer = new ReactiveProperty<bool>(true);
            RemotePlayer = new ReactiveProperty<bool>(true);
            Environment = new ReactiveProperty<bool>(true);
            GreenScreen = new ReactiveProperty<bool>(false);
            SmoothMovement = new ReactiveProperty<bool>(false);
            LookAtMe = new ReactiveProperty<bool>(false);
            AutoLevelRoll = new ReactiveProperty<bool>(false);
            AutoLevelPitch = new ReactiveProperty<bool>(false);
            Flying = new ReactiveProperty<bool>(false);
            TriggerTakesPhotos = new ReactiveProperty<bool>(false);
            DollyPathsStayVisible = new ReactiveProperty<bool>(false);
            CameraEars = new ReactiveProperty<bool>(false);
            ShowFocus = new ReactiveProperty<bool>(false);
            Streaming = new ReactiveProperty<bool>(false);
            RollWhileFlying = new ReactiveProperty<bool>(false);
            Orientation = new ReactiveProperty<Orientation>(Astearium.VRChat.Camera.Orientation.Landscape);
            Mode = new ReactiveProperty<Mode>(Astearium.VRChat.Camera.Mode.Photo);

            // Items has no OSC endpoint; keep as display-only (default true)
            Items = new ReactiveProperty<bool>(true);
        }

        /// <summary>
        /// Updates camera-related values from Camera component
        /// </summary>
        public void UpdateFromCamera()
        {
            // ReactiveProperty will check for equality internally
            Zoom.SetValue(new Zoom(_camera.focalLength, true));
            FocalDistance.SetValue(new FocalDistance(_camera.focusDistance));
            Aperture.SetValue(new Aperture(_camera.aperture));
        }

        /// <summary>
        /// Sets Exposure value reactively
        /// </summary>
        public void SetExposure(Exposure exposure)
        {
            Exposure.SetValue(exposure);
        }

        /// <summary>
        /// Sets Hue value reactively
        /// </summary>
        public void SetHue(Hue hue)
        {
            Hue.SetValue(hue);
        }

        /// <summary>
        /// Sets Saturation value reactively
        /// </summary>
        public void SetSaturation(Saturation saturation)
        {
            Saturation.SetValue(saturation);
        }

        /// <summary>
        /// Sets Lightness value reactively
        /// </summary>
        public void SetLightness(Lightness lightness)
        {
            Lightness.SetValue(lightness);
        }

        /// <summary>
        /// Sets LookAtMeOffset values reactively
        /// </summary>
        public void SetLookAtMeOffset(LookAtMeOffset lookAtMeOffset)
        {
            LookAtMeOffset.SetValue(lookAtMeOffset);
        }

        /// <summary>
        /// Sets FlySpeed value reactively
        /// </summary>
        public void SetFlySpeed(FlySpeed flySpeed)
        {
            FlySpeed.SetValue(flySpeed);
        }

        /// <summary>
        /// Sets TurnSpeed value reactively
        /// </summary>
        public void SetTurnSpeed(TurnSpeed turnSpeed)
        {
            TurnSpeed.SetValue(turnSpeed);
        }

        /// <summary>
        /// Sets SmoothingStrength value reactively
        /// </summary>
        public void SetSmoothingStrength(SmoothingStrength smoothingStrength)
        {
            SmoothingStrength.SetValue(smoothingStrength);
        }

        /// <summary>
        /// Sets PhotoRate value reactively
        /// </summary>
        public void SetPhotoRate(PhotoRate photoRate)
        {
            PhotoRate.SetValue(photoRate);
        }

        /// <summary>
        /// Sets Duration value reactively
        /// </summary>
        public void SetDuration(Duration duration)
        {
            Duration.SetValue(duration);
        }

        /// <summary>
        /// Sets Pose value reactively. Always applies the provided pose.
        /// </summary>
        public void SetPose(Pose pose)
        {
            Pose.SetValue(pose);
        }

        /// <summary>
        /// Sets ShowUIInCamera toggle value reactively
        /// </summary>
        public void SetShowUIInCamera(bool showUIInCamera)
        {
            ShowUIInCamera.SetValue(showUIInCamera);
        }

        /// <summary>
        /// Sets Lock toggle value reactively
        /// </summary>
        public void SetLock(bool lockToggle)
        {
            Lock.SetValue(lockToggle);
        }

        /// <summary>
        /// Sets LocalPlayer toggle value reactively
        /// </summary>
        public void SetLocalPlayer(bool localPlayer)
        {
            LocalPlayer.SetValue(localPlayer);
        }

        /// <summary>
        /// Sets RemotePlayer toggle value reactively
        /// </summary>
        public void SetRemotePlayer(bool remotePlayer)
        {
            RemotePlayer.SetValue(remotePlayer);
        }

        /// <summary>
        /// Sets Environment toggle value reactively
        /// </summary>
        public void SetEnvironment(bool environment)
        {
            Environment.SetValue(environment);
        }

        /// <summary>
        /// Sets GreenScreen toggle value reactively
        /// </summary>
        public void SetGreenScreen(bool greenScreen)
        {
            GreenScreen.SetValue(greenScreen);
        }

        /// <summary>
        /// Sets SmoothMovement toggle value reactively
        /// </summary>
        public void SetSmoothMovement(bool smoothMovement)
        {
            SmoothMovement.SetValue(smoothMovement);
        }

        /// <summary>
        /// Sets LookAtMe toggle value reactively
        /// </summary>
        public void SetLookAtMe(bool lookAtMe)
        {
            LookAtMe.SetValue(lookAtMe);
        }

        /// <summary>
        /// Sets AutoLevelRoll toggle value reactively
        /// </summary>
        public void SetAutoLevelRoll(bool autoLevelRoll)
        {
            AutoLevelRoll.SetValue(autoLevelRoll);
        }

        /// <summary>
        /// Sets AutoLevelPitch toggle value reactively
        /// </summary>
        public void SetAutoLevelPitch(bool autoLevelPitch)
        {
            AutoLevelPitch.SetValue(autoLevelPitch);
        }

        /// <summary>
        /// Sets Flying toggle value reactively
        /// </summary>
        public void SetFlying(bool flying)
        {
            Flying.SetValue(flying);
        }

        /// <summary>
        /// Sets TriggerTakesPhotos toggle value reactively
        /// </summary>
        public void SetTriggerTakesPhotos(bool trigger)
        {
            TriggerTakesPhotos.SetValue(trigger);
        }

        /// <summary>
        /// Sets DollyPathsStayVisible toggle value reactively
        /// </summary>
        public void SetDollyPathsStayVisible(bool dollyPathsStayVisible)
        {
            DollyPathsStayVisible.SetValue(dollyPathsStayVisible);
        }

        /// <summary>
        /// Sets CameraEars toggle value reactively
        /// </summary>
        public void SetCameraEars(bool cameraEars)
        {
            CameraEars.SetValue(cameraEars);
        }

        /// <summary>
        /// Sets ShowFocus toggle value reactively
        /// </summary>
        public void SetShowFocus(bool showFocus)
        {
            ShowFocus.SetValue(showFocus);
        }

        /// <summary>
        /// Sets Streaming toggle value reactively
        /// </summary>
        public void SetStreaming(bool streaming)
        {
            Streaming.SetValue(streaming);
        }

        /// <summary>
        /// Sets RollWhileFlying toggle value reactively
        /// </summary>
        public void SetRollWhileFlying(bool rollWhileFlying)
        {
            RollWhileFlying.SetValue(rollWhileFlying);
        }

        /// <summary>
        /// Sets Orientation value reactively
        /// </summary>
        public void SetOrientation(Orientation orientation)
        {
            Orientation.SetValue(orientation);
        }

        /// <summary>
        /// Sets <see cref="Mode"/> value reactively
        /// </summary>
        public void SetMode(Mode mode)
        {
            Mode.SetValue(mode);
        }

        /// <summary>
        /// Disposes the VRCCamera and clears all event subscriptions
        /// </summary>
        public void Dispose()
        {
            // Clear all event subscriptions to prevent memory leaks
            Zoom?.ClearSubscriptions();
            Exposure?.ClearSubscriptions();
            FocalDistance?.ClearSubscriptions();
            Aperture?.ClearSubscriptions();
            Hue?.ClearSubscriptions();
            Saturation?.ClearSubscriptions();
            Lightness?.ClearSubscriptions();
            LookAtMeOffset?.ClearSubscriptions();
            FlySpeed?.ClearSubscriptions();
            TurnSpeed?.ClearSubscriptions();
            SmoothingStrength?.ClearSubscriptions();
            PhotoRate?.ClearSubscriptions();
            Duration?.ClearSubscriptions();
            ShowUIInCamera?.ClearSubscriptions();
            Lock?.ClearSubscriptions();
            LocalPlayer?.ClearSubscriptions();
            RemotePlayer?.ClearSubscriptions();
            Environment?.ClearSubscriptions();
            GreenScreen?.ClearSubscriptions();
            SmoothMovement?.ClearSubscriptions();
            LookAtMe?.ClearSubscriptions();
            AutoLevelRoll?.ClearSubscriptions();
            AutoLevelPitch?.ClearSubscriptions();
            Flying?.ClearSubscriptions();
            TriggerTakesPhotos?.ClearSubscriptions();
            DollyPathsStayVisible?.ClearSubscriptions();
            CameraEars?.ClearSubscriptions();
            ShowFocus?.ClearSubscriptions();
            Streaming?.ClearSubscriptions();
            RollWhileFlying?.ClearSubscriptions();
            Orientation?.ClearSubscriptions();
            Mode?.ClearSubscriptions();
            Pose?.ClearSubscriptions();
            Items?.ClearSubscriptions();
        }
    }
}
