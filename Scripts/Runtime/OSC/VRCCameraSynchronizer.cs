using System;
using UnityEngine;

namespace JessiQa
{
    public class VRCCameraSynchronizer : IDisposable
    {
        private readonly IOSCTransmitter _transmitter;
        private readonly Camera _camera;
        private readonly ZoomConverter _zoomConverter = new();
        private bool _disposed = false;

        public VRCCameraSynchronizer(IOSCTransmitter transmitter, Camera camera)
        {
            _transmitter = transmitter ?? throw new ArgumentNullException(nameof(transmitter));
            _camera = camera ?? throw new ArgumentNullException(nameof(camera));
        }

        public void Sync()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(VRCCameraSynchronizer));
            
            var zoom = _zoomConverter.ToOSCMessage(new Zoom(_camera.focalLength));
            _transmitter.Send(zoom);
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            _transmitter?.Dispose();
            _disposed = true;
        }
    }
}