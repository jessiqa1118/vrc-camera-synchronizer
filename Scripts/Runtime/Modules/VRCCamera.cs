using System;
using UnityEngine;

namespace Astearium.VRChat.Camera
{
    public class VRCCamera : IDisposable
    {
        public ReactiveProperty<Zoom> Zoom { get; } = new(new Zoom(Astearium.VRChat.Camera.Zoom.DefaultValue, true));

        public ReactiveProperty<Exposure> Exposure { get; } =
            new(new Exposure(Astearium.VRChat.Camera.Exposure.DefaultValue));

        public ReactiveProperty<FocalDistance> FocalDistance { get; } = new(
            new FocalDistance(Astearium.VRChat.Camera.FocalDistance.DefaultValue));

        public ReactiveProperty<Aperture> Aperture { get; } =
            new(new Aperture(Astearium.VRChat.Camera.Aperture.DefaultValue));

        public ReactiveProperty<Hue> Hue { get; } = new(new Hue(Astearium.VRChat.Camera.Hue.DefaultValue));

        public ReactiveProperty<Saturation> Saturation { get; } =
            new(new Saturation(Astearium.VRChat.Camera.Saturation.DefaultValue));

        public ReactiveProperty<Lightness> Lightness { get; } =
            new(new Lightness(Astearium.VRChat.Camera.Lightness.DefaultValue));

        public ReactiveProperty<LookAtMeOffset> LookAtMeOffset { get; } = new(new LookAtMeOffset(
            new LookAtMeXOffset(LookAtMeXOffset.DefaultValue),
            new LookAtMeYOffset(LookAtMeYOffset.DefaultValue)));

        public ReactiveProperty<FlySpeed> FlySpeed { get; } =
            new(new FlySpeed(Astearium.VRChat.Camera.FlySpeed.DefaultValue));

        public ReactiveProperty<TurnSpeed> TurnSpeed { get; } =
            new(new TurnSpeed(Astearium.VRChat.Camera.TurnSpeed.DefaultValue));

        public ReactiveProperty<SmoothingStrength> SmoothingStrength { get; } = new(
            new SmoothingStrength(Astearium.VRChat.Camera.SmoothingStrength.DefaultValue));

        public ReactiveProperty<PhotoRate> PhotoRate { get; } =
            new(new PhotoRate(Astearium.VRChat.Camera.PhotoRate.DefaultValue));

        public ReactiveProperty<Duration> Duration { get; } =
            new(new Duration(Astearium.VRChat.Camera.Duration.DefaultValue));

        /// <summary>
        /// Current world-space pose (position + rotation) for OSC sync
        /// </summary>
        public ReactiveProperty<Pose> Pose { get; } = new(UnityEngine.Pose.identity);

        public ReactiveProperty<bool> ShowUIInCamera { get; } = new(false);
        public ReactiveProperty<bool> Lock { get; } = new(false);
        public ReactiveProperty<bool> LocalPlayer { get; } = new(true);
        public ReactiveProperty<bool> RemotePlayer { get; } = new(true);
        public ReactiveProperty<bool> Environment { get; } = new(true);
        public ReactiveProperty<bool> GreenScreen { get; } = new(false);
        public ReactiveProperty<bool> SmoothMovement { get; } = new(false);
        public ReactiveProperty<bool> LookAtMe { get; } = new(false);
        public ReactiveProperty<bool> AutoLevelRoll { get; } = new(false);
        public ReactiveProperty<bool> AutoLevelPitch { get; } = new(false);
        public ReactiveProperty<bool> Flying { get; } = new(false);
        public ReactiveProperty<bool> TriggerTakesPhotos { get; } = new(false);
        public ReactiveProperty<bool> DollyPathsStayVisible { get; } = new(false);
        public ReactiveProperty<bool> CameraEars { get; } = new(false);
        public ReactiveProperty<bool> ShowFocus { get; } = new(false);
        public ReactiveProperty<bool> Streaming { get; } = new(false);
        public ReactiveProperty<bool> RollWhileFlying { get; } = new(false);
        public ReactiveProperty<Orientation> Orientation { get; } = new(Astearium.VRChat.Camera.Orientation.Landscape);
        public ReactiveProperty<bool> Items { get; } = new(true);

        /// <summary>
        /// Current camera <see cref="Mode"/>; changes are published over OSC.
        /// </summary>
        public ReactiveProperty<Mode> Mode { get; } = new(Astearium.VRChat.Camera.Mode.Photo);

        // Initialize reactive properties
        // Items has no OSC endpoint; keep as display-only (default true)

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

        public void SetZoom(Zoom zoom)
        {
            Zoom.SetValue(zoom);
        }

        public void SetFocalDistance(FocalDistance focalDistance)
        {
            FocalDistance.SetValue(focalDistance);
        }

        public void SetAperture(Aperture aperture)
        {
            Aperture.SetValue(aperture);
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