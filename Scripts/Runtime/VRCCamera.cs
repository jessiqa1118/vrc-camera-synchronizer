using UnityEngine;

namespace JessiQa
{
    public class VRCCamera
    {
        private readonly Camera _camera;

        public Zoom Zoom => new(_camera.focalLength);

        public Exposure Exposure { get; set; }

        public VRCCamera(Camera camera)
        {
            _camera = camera ?? throw new System.ArgumentNullException(nameof(camera));
            Exposure = new Exposure();
        }
    }
}