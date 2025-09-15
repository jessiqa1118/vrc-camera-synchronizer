using System;
using UnityEngine;
using Parameters;

namespace VRCCamera
{
    public class VRCCamera : IDisposable
    {
        private readonly Camera _camera;

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
        public ReactiveProperty<ShowUIInCameraToggle> ShowUIInCamera { get; }
        public ReactiveProperty<LockToggle> Lock { get; }
        public ReactiveProperty<LocalPlayerToggle> LocalPlayer { get; }
        public ReactiveProperty<RemotePlayerToggle> RemotePlayer { get; }
        public ReactiveProperty<EnvironmentToggle> Environment { get; }
        public ReactiveProperty<GreenScreenToggle> GreenScreen { get; }
        public ReactiveProperty<SmoothMovementToggle> SmoothMovement { get; }
        public ReactiveProperty<LookAtMeToggle> LookAtMe { get; }
        public ReactiveProperty<AutoLevelRollToggle> AutoLevelRoll { get; }
        public ReactiveProperty<AutoLevelPitchToggle> AutoLevelPitch { get; }
        public ReactiveProperty<FlyingToggle> Flying { get; }
        public ReactiveProperty<TriggerTakesPhotosToggle> TriggerTakesPhotos { get; }
        public ReactiveProperty<DollyPathsStayVisibleToggle> DollyPathsStayVisible { get; }
        public ReactiveProperty<CameraEarsToggle> CameraEars { get; }
        public ReactiveProperty<ShowFocusToggle> ShowFocus { get; }
        public ReactiveProperty<StreamingToggle> Streaming { get; }
        public ReactiveProperty<RollWhileFlyingToggle> RollWhileFlying { get; }
        public ReactiveProperty<Orientation> Orientation { get; }
        public ReactiveProperty<ItemsToggle> Items { get; }
        public ReactiveProperty<Mode> Mode { get; }

        public VRCCamera(Camera camera)
        {
            _camera = camera ?? throw new System.ArgumentNullException(nameof(camera));

            // Initialize reactive properties
            Zoom = new ReactiveProperty<Zoom>(new Zoom(_camera.focalLength, true));
            Exposure = new ReactiveProperty<Exposure>(new Exposure(Parameters.Exposure.DefaultValue));
            FocalDistance = new ReactiveProperty<FocalDistance>(new FocalDistance(_camera.focusDistance));
            Aperture = new ReactiveProperty<Aperture>(new Aperture(_camera.aperture));
            Hue = new ReactiveProperty<Hue>(new Hue(Parameters.Hue.DefaultValue));
            Saturation = new ReactiveProperty<Saturation>(new Saturation(Parameters.Saturation.DefaultValue));
            Lightness = new ReactiveProperty<Lightness>(new Lightness(Parameters.Lightness.DefaultValue));
            LookAtMeOffset = new ReactiveProperty<LookAtMeOffset>(new LookAtMeOffset(
                new LookAtMeXOffset(LookAtMeXOffset.DefaultValue),
                new LookAtMeYOffset(LookAtMeYOffset.DefaultValue)));
            FlySpeed = new ReactiveProperty<FlySpeed>(new FlySpeed(Parameters.FlySpeed.DefaultValue));
            TurnSpeed = new ReactiveProperty<TurnSpeed>(new TurnSpeed(Parameters.TurnSpeed.DefaultValue));
            SmoothingStrength = new ReactiveProperty<SmoothingStrength>(new SmoothingStrength(Parameters.SmoothingStrength.DefaultValue));
            PhotoRate = new ReactiveProperty<PhotoRate>(new PhotoRate(Parameters.PhotoRate.DefaultValue));
            Duration = new ReactiveProperty<Duration>(new Duration(Parameters.Duration.DefaultValue));
            ShowUIInCamera = new ReactiveProperty<ShowUIInCameraToggle>(new ShowUIInCameraToggle(false));
            Lock = new ReactiveProperty<LockToggle>(new LockToggle(false));
            LocalPlayer = new ReactiveProperty<LocalPlayerToggle>(new LocalPlayerToggle(true));
            RemotePlayer = new ReactiveProperty<RemotePlayerToggle>(new RemotePlayerToggle(true));
            Environment = new ReactiveProperty<EnvironmentToggle>(new EnvironmentToggle(true));
            GreenScreen = new ReactiveProperty<GreenScreenToggle>(new GreenScreenToggle(false));
            SmoothMovement = new ReactiveProperty<SmoothMovementToggle>(new SmoothMovementToggle(false));
            LookAtMe = new ReactiveProperty<LookAtMeToggle>(new LookAtMeToggle(false));
            AutoLevelRoll = new ReactiveProperty<AutoLevelRollToggle>(new AutoLevelRollToggle(false));
            AutoLevelPitch = new ReactiveProperty<AutoLevelPitchToggle>(new AutoLevelPitchToggle(false));
            Flying = new ReactiveProperty<FlyingToggle>(new FlyingToggle(false));
            TriggerTakesPhotos = new ReactiveProperty<TriggerTakesPhotosToggle>(new TriggerTakesPhotosToggle(false));
            DollyPathsStayVisible = new ReactiveProperty<DollyPathsStayVisibleToggle>(new DollyPathsStayVisibleToggle(false));
            CameraEars = new ReactiveProperty<CameraEarsToggle>(new CameraEarsToggle(false));
            ShowFocus = new ReactiveProperty<ShowFocusToggle>(new ShowFocusToggle(false));
            Streaming = new ReactiveProperty<StreamingToggle>(new StreamingToggle(false));
            RollWhileFlying = new ReactiveProperty<RollWhileFlyingToggle>(new RollWhileFlyingToggle(false));
            Orientation = new ReactiveProperty<Orientation>(Parameters.Orientation.Landscape);
            Mode = new ReactiveProperty<Mode>(Parameters.Mode.Photo);
            // Items has no OSC endpoint; keep as display-only (default true)
            Items = new ReactiveProperty<ItemsToggle>(new ItemsToggle(true));
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
        /// Sets ShowUIInCamera toggle value reactively
        /// </summary>
        public void SetShowUIInCamera(ShowUIInCameraToggle showUIInCamera)
        {
            ShowUIInCamera.SetValue(showUIInCamera);
        }

        /// <summary>
        /// Sets Lock toggle value reactively
        /// </summary>
        public void SetLock(LockToggle lockToggle)
        {
            Lock.SetValue(lockToggle);
        }

        /// <summary>
        /// Sets LocalPlayer toggle value reactively
        /// </summary>
        public void SetLocalPlayer(LocalPlayerToggle localPlayer)
        {
            LocalPlayer.SetValue(localPlayer);
        }

        /// <summary>
        /// Sets RemotePlayer toggle value reactively
        /// </summary>
        public void SetRemotePlayer(RemotePlayerToggle remotePlayer)
        {
            RemotePlayer.SetValue(remotePlayer);
        }

        /// <summary>
        /// Sets Environment toggle value reactively
        /// </summary>
        public void SetEnvironment(EnvironmentToggle environment)
        {
            Environment.SetValue(environment);
        }

        /// <summary>
        /// Sets GreenScreen toggle value reactively
        /// </summary>
        public void SetGreenScreen(GreenScreenToggle greenScreen)
        {
            GreenScreen.SetValue(greenScreen);
        }

        /// <summary>
        /// Sets SmoothMovement toggle value reactively
        /// </summary>
        public void SetSmoothMovement(SmoothMovementToggle smoothMovement)
        {
            SmoothMovement.SetValue(smoothMovement);
        }

        /// <summary>
        /// Sets LookAtMe toggle value reactively
        /// </summary>
        public void SetLookAtMe(LookAtMeToggle lookAtMe)
        {
            LookAtMe.SetValue(lookAtMe);
        }

        /// <summary>
        /// Sets AutoLevelRoll toggle value reactively
        /// </summary>
        public void SetAutoLevelRoll(AutoLevelRollToggle autoLevelRoll)
        {
            AutoLevelRoll.SetValue(autoLevelRoll);
        }

        /// <summary>
        /// Sets AutoLevelPitch toggle value reactively
        /// </summary>
        public void SetAutoLevelPitch(AutoLevelPitchToggle autoLevelPitch)
        {
            AutoLevelPitch.SetValue(autoLevelPitch);
        }

        /// <summary>
        /// Sets Flying toggle value reactively
        /// </summary>
        public void SetFlying(FlyingToggle flying)
        {
            Flying.SetValue(flying);
        }

        /// <summary>
        /// Sets TriggerTakesPhotos toggle value reactively
        /// </summary>
        public void SetTriggerTakesPhotos(TriggerTakesPhotosToggle trigger)
        {
            TriggerTakesPhotos.SetValue(trigger);
        }

        /// <summary>
        /// Sets DollyPathsStayVisible toggle value reactively
        /// </summary>
        public void SetDollyPathsStayVisible(DollyPathsStayVisibleToggle dollyPathsStayVisible)
        {
            DollyPathsStayVisible.SetValue(dollyPathsStayVisible);
        }

        /// <summary>
        /// Sets CameraEars toggle value reactively
        /// </summary>
        public void SetCameraEars(CameraEarsToggle cameraEars)
        {
            CameraEars.SetValue(cameraEars);
        }

        /// <summary>
        /// Sets ShowFocus toggle value reactively
        /// </summary>
        public void SetShowFocus(ShowFocusToggle showFocus)
        {
            ShowFocus.SetValue(showFocus);
        }

        /// <summary>
        /// Sets Streaming toggle value reactively
        /// </summary>
        public void SetStreaming(StreamingToggle streaming)
        {
            Streaming.SetValue(streaming);
        }

        /// <summary>
        /// Sets RollWhileFlying toggle value reactively
        /// </summary>
        public void SetRollWhileFlying(RollWhileFlyingToggle rollWhileFlying)
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
        /// Sets Mode value reactively
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
            Items?.ClearSubscriptions();
        }
    }
}
