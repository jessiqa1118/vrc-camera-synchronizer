using UnityEngine;

namespace JessiQa
{
    public class VRCCamera
    {
        private readonly Camera _camera;
        private float _lastFocalLength;

        public ReactiveProperty<Zoom> Zoom { get; }

        public Exposure Exposure { get; set; }

        // Both Unity and VRChat use meters, range 0-10m
        public FocalDistance FocalDistance => new(_camera.focusDistance);

        // Sync with Unity's Camera.aperture
        public Aperture Aperture => new(_camera.aperture);

        public Hue Hue { get; set; }

        public Saturation Saturation { get; set; }

        public Lightness Lightness { get; set; }

        public LookAtMeOffset LookAtMeOffset { get; set; }

        public FlySpeed FlySpeed { get; set; }

        public TurnSpeed TurnSpeed { get; set; }

        public SmoothingStrength SmoothingStrength { get; set; }

        public PhotoRate PhotoRate { get; set; }

        public Duration Duration { get; set; }

        public VRCCamera(Camera camera)
        {
            _camera = camera ?? throw new System.ArgumentNullException(nameof(camera));

            // Initialize reactive property for Zoom
            _lastFocalLength = _camera.focalLength;
            Zoom = new ReactiveProperty<Zoom>(new Zoom(_camera.focalLength, true));

            // Initialize other properties
            Exposure = new Exposure(Exposure.DefaultValue);
            Hue = new Hue(Hue.DefaultValue);
            Saturation = new Saturation(Saturation.DefaultValue);
            Lightness = new Lightness(Lightness.DefaultValue);
            LookAtMeOffset = new LookAtMeOffset(new LookAtMeXOffset(LookAtMeXOffset.DefaultValue),
                new LookAtMeYOffset(LookAtMeYOffset.DefaultValue));
            FlySpeed = new FlySpeed(FlySpeed.DefaultValue);
            TurnSpeed = new TurnSpeed(TurnSpeed.DefaultValue);
            SmoothingStrength = new SmoothingStrength(SmoothingStrength.DefaultValue);
            PhotoRate = new PhotoRate(PhotoRate.DefaultValue);
            Duration = new Duration(Duration.DefaultValue);
        }

        /// <summary>
        /// Updates Zoom value from Camera component if changed
        /// </summary>
        public void UpdateFromCamera()
        {
            // Check if focal length has changed
            if (Mathf.Approximately(_lastFocalLength, _camera.focalLength)) return;

            _lastFocalLength = _camera.focalLength;
            Zoom.SetValue(new Zoom(_camera.focalLength, true));
        }
    }
}