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

        public VRCCamera(Camera camera)
        {
            _camera = camera ?? throw new System.ArgumentNullException(nameof(camera));
            Exposure = new Exposure();
            Hue = new Hue();
        }
    }
}