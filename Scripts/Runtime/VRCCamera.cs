using UnityEngine;

namespace JessiQa
{
    public class VRCCamera
    {
        private readonly Camera _camera;

        public Zoom Zoom => new(_camera.focalLength);

        public Exposure Exposure { get; set; }
        
        // Both Unity and VRChat use meters, range 0-10m
        public FocalDistance FocalDistance => new(_camera.focusDistance);
        
        // Sync with Unity's Camera.aperture
        public Aperture Aperture => new(_camera.aperture);
        
        public Hue Hue { get; set; }
        
        public Saturation Saturation { get; set; }
        
        public Lightness Lightness { get; set; }
        
        public LookAtMeXOffset LookAtMeXOffset { get; set; }
        
        public LookAtMeYOffset LookAtMeYOffset { get; set; }
        
        public FlySpeed FlySpeed { get; set; }
        
        public TurnSpeed TurnSpeed { get; set; }
        
        public SmoothingStrength SmoothingStrength { get; set; }
        
        public PhotoRate PhotoRate { get; set; }
        
        public Duration Duration { get; set; }

        public VRCCamera(Camera camera)
        {
            _camera = camera ?? throw new System.ArgumentNullException(nameof(camera));
            Exposure = new Exposure();
            Hue = new Hue();
            Saturation = new Saturation();
            Lightness = new Lightness();
            LookAtMeXOffset = new LookAtMeXOffset();
            LookAtMeYOffset = new LookAtMeYOffset();
            FlySpeed = new FlySpeed();
            TurnSpeed = new TurnSpeed();
            SmoothingStrength = new SmoothingStrength();
            PhotoRate = new PhotoRate();
            Duration = new Duration();
        }
    }
}