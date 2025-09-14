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
            LocalPlayer = new ReactiveProperty<LocalPlayerToggle>(new LocalPlayerToggle(false));
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
        }
    }
}
