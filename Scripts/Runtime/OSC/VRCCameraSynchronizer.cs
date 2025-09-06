using System;
using UnityEngine;

namespace JessiQa
{
    public class VRCCameraSynchronizer : IDisposable
    {
        private readonly IOSCTransmitter _transmitter;
        private readonly VRCCamera _vrcCamera;
        private readonly ZoomConverter _zoomConverter = new();
        private readonly ExposureConverter _exposureConverter = new();
        private readonly FocalDistanceConverter _focalDistanceConverter = new();
        private bool _disposed = false;

        public VRCCameraSynchronizer(IOSCTransmitter transmitter, VRCCamera vrcCamera)
        {
            _transmitter = transmitter ?? throw new ArgumentNullException(nameof(transmitter));
            _vrcCamera = vrcCamera ?? throw new ArgumentNullException(nameof(vrcCamera));
        }

        public void Sync()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(VRCCameraSynchronizer));
            
            // Send zoom
            var zoomMessage = _zoomConverter.ToOSCMessage(_vrcCamera.Zoom);
            _transmitter.Send(zoomMessage);
            
            // Send exposure
            var exposureMessage = _exposureConverter.ToOSCMessage(_vrcCamera.Exposure);
            _transmitter.Send(exposureMessage);
            
            // Send focal distance
            var focalDistanceMessage = _focalDistanceConverter.ToOSCMessage(_vrcCamera.FocalDistance);
            _transmitter.Send(focalDistanceMessage);
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            _transmitter?.Dispose();
            _disposed = true;
        }
    }
}