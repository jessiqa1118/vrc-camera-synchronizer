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
        private readonly ApertureConverter _apertureConverter = new();
        private readonly HueConverter _hueConverter = new();
        private readonly SaturationConverter _saturationConverter = new();
        private readonly LightnessConverter _lightnessConverter = new();
        private readonly LookAtMeXOffsetConverter _lookAtMeXOffsetConverter = new();
        private readonly LookAtMeYOffsetConverter _lookAtMeYOffsetConverter = new();
        private readonly FlySpeedConverter _flySpeedConverter = new();
        private readonly TurnSpeedConverter _turnSpeedConverter = new();
        private readonly SmoothingStrengthConverter _smoothingStrengthConverter = new();
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
            
            // Send aperture
            var apertureMessage = _apertureConverter.ToOSCMessage(_vrcCamera.Aperture);
            _transmitter.Send(apertureMessage);
            
            // Send hue
            var hueMessage = _hueConverter.ToOSCMessage(_vrcCamera.Hue);
            _transmitter.Send(hueMessage);
            
            // Send saturation
            var saturationMessage = _saturationConverter.ToOSCMessage(_vrcCamera.Saturation);
            _transmitter.Send(saturationMessage);
            
            // Send lightness
            var lightnessMessage = _lightnessConverter.ToOSCMessage(_vrcCamera.Lightness);
            _transmitter.Send(lightnessMessage);
            
            // Send LookAtMe X offset
            var lookAtMeXMessage = _lookAtMeXOffsetConverter.ToOSCMessage(_vrcCamera.LookAtMeXOffset);
            _transmitter.Send(lookAtMeXMessage);
            
            // Send LookAtMe Y offset
            var lookAtMeYMessage = _lookAtMeYOffsetConverter.ToOSCMessage(_vrcCamera.LookAtMeYOffset);
            _transmitter.Send(lookAtMeYMessage);
            
            // Send FlySpeed
            var flySpeedMessage = _flySpeedConverter.ToOSCMessage(_vrcCamera.FlySpeed);
            _transmitter.Send(flySpeedMessage);
            
            // Send TurnSpeed
            var turnSpeedMessage = _turnSpeedConverter.ToOSCMessage(_vrcCamera.TurnSpeed);
            _transmitter.Send(turnSpeedMessage);
            
            // Send SmoothingStrength
            var smoothingStrengthMessage = _smoothingStrengthConverter.ToOSCMessage(_vrcCamera.SmoothingStrength);
            _transmitter.Send(smoothingStrengthMessage);
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            _transmitter?.Dispose();
            _disposed = true;
        }
    }
}