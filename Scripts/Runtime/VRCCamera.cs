using UnityEngine;
using Parameters;

namespace VRCCamera
{
    public class VRCCamera
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
        public void SetExposure(float value)
        {
            Exposure.SetValue(new Exposure(value));
        }
        
        /// <summary>
        /// Sets Hue value reactively
        /// </summary>
        public void SetHue(float value)
        {
            Hue.SetValue(new Hue(value));
        }
        
        /// <summary>
        /// Sets Saturation value reactively
        /// </summary>
        public void SetSaturation(float value)
        {
            Saturation.SetValue(new Saturation(value));
        }
        
        /// <summary>
        /// Sets Lightness value reactively
        /// </summary>
        public void SetLightness(float value)
        {
            Lightness.SetValue(new Lightness(value));
        }
        
        /// <summary>
        /// Sets LookAtMeOffset values reactively
        /// </summary>
        public void SetLookAtMeOffset(float x, float y)
        {
            LookAtMeOffset.SetValue(new LookAtMeOffset(
                new LookAtMeXOffset(x),
                new LookAtMeYOffset(y)));
        }
        
        /// <summary>
        /// Sets FlySpeed value reactively
        /// </summary>
        public void SetFlySpeed(float value)
        {
            FlySpeed.SetValue(new FlySpeed(value));
        }
        
        /// <summary>
        /// Sets TurnSpeed value reactively
        /// </summary>
        public void SetTurnSpeed(float value)
        {
            TurnSpeed.SetValue(new TurnSpeed(value));
        }
        
        /// <summary>
        /// Sets SmoothingStrength value reactively
        /// </summary>
        public void SetSmoothingStrength(float value)
        {
            SmoothingStrength.SetValue(new SmoothingStrength(value));
        }
        
        /// <summary>
        /// Sets PhotoRate value reactively
        /// </summary>
        public void SetPhotoRate(float value)
        {
            PhotoRate.SetValue(new PhotoRate(value));
        }
        
        /// <summary>
        /// Sets Duration value reactively
        /// </summary>
        public void SetDuration(float value)
        {
            Duration.SetValue(new Duration(value));
        }
    }
}