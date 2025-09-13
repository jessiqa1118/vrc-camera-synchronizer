using System;
using Parameters;

namespace VRCCamera
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
        private readonly PhotoRateConverter _photoRateConverter = new();
        private readonly DurationConverter _durationConverter = new();
        private readonly ShowUIInCameraToggleConverter _showUIInCameraToggleConverter = new();
        private readonly LockToggleConverter _lockToggleConverter = new();
        private bool _disposed = false;

        public VRCCameraSynchronizer(IOSCTransmitter transmitter, VRCCamera vrcCamera)
        {
            _transmitter = transmitter ?? throw new ArgumentNullException(nameof(transmitter));
            _vrcCamera = vrcCamera ?? throw new ArgumentNullException(nameof(vrcCamera));
            
            // Subscribe to value changes
            _vrcCamera.Zoom.OnValueChanged += OnZoomChanged;
            _vrcCamera.Exposure.OnValueChanged += OnExposureChanged;
            _vrcCamera.FocalDistance.OnValueChanged += OnFocalDistanceChanged;
            _vrcCamera.Aperture.OnValueChanged += OnApertureChanged;
            _vrcCamera.Hue.OnValueChanged += OnHueChanged;
            _vrcCamera.Saturation.OnValueChanged += OnSaturationChanged;
            _vrcCamera.Lightness.OnValueChanged += OnLightnessChanged;
            _vrcCamera.LookAtMeOffset.OnValueChanged += OnLookAtMeOffsetChanged;
            _vrcCamera.FlySpeed.OnValueChanged += OnFlySpeedChanged;
            _vrcCamera.TurnSpeed.OnValueChanged += OnTurnSpeedChanged;
            _vrcCamera.SmoothingStrength.OnValueChanged += OnSmoothingStrengthChanged;
            _vrcCamera.PhotoRate.OnValueChanged += OnPhotoRateChanged;
            _vrcCamera.Duration.OnValueChanged += OnDurationChanged;
            _vrcCamera.ShowUIInCamera.OnValueChanged += OnShowUIInCameraChanged;
            _vrcCamera.Lock.OnValueChanged += OnLockChanged;
            
            // Send initial values
            Sync();
        }
        
        private void OnZoomChanged(Zoom zoom)
        {
            if (_disposed) return;
            
            var message = _zoomConverter.ToOSCMessage(zoom);
            _transmitter.Send(message);
        }
        
        private void OnExposureChanged(Exposure exposure)
        {
            if (_disposed) return;
            
            var message = _exposureConverter.ToOSCMessage(exposure);
            _transmitter.Send(message);
        }
        
        private void OnFocalDistanceChanged(FocalDistance focalDistance)
        {
            if (_disposed) return;
            
            var message = _focalDistanceConverter.ToOSCMessage(focalDistance);
            _transmitter.Send(message);
        }
        
        private void OnApertureChanged(Aperture aperture)
        {
            if (_disposed) return;
            
            var message = _apertureConverter.ToOSCMessage(aperture);
            _transmitter.Send(message);
        }
        
        private void OnHueChanged(Hue hue)
        {
            if (_disposed) return;
            
            var message = _hueConverter.ToOSCMessage(hue);
            _transmitter.Send(message);
        }
        
        private void OnSaturationChanged(Saturation saturation)
        {
            if (_disposed) return;
            
            var message = _saturationConverter.ToOSCMessage(saturation);
            _transmitter.Send(message);
        }
        
        private void OnLightnessChanged(Lightness lightness)
        {
            if (_disposed) return;
            
            var message = _lightnessConverter.ToOSCMessage(lightness);
            _transmitter.Send(message);
        }
        
        private void OnLookAtMeOffsetChanged(LookAtMeOffset lookAtMeOffset)
        {
            if (_disposed) return;
            
            // Send X offset
            var xMessage = _lookAtMeXOffsetConverter.ToOSCMessage(lookAtMeOffset.X);
            _transmitter.Send(xMessage);
            
            // Send Y offset
            var yMessage = _lookAtMeYOffsetConverter.ToOSCMessage(lookAtMeOffset.Y);
            _transmitter.Send(yMessage);
        }
        
        private void OnFlySpeedChanged(FlySpeed flySpeed)
        {
            if (_disposed) return;
            
            var message = _flySpeedConverter.ToOSCMessage(flySpeed);
            _transmitter.Send(message);
        }
        
        private void OnTurnSpeedChanged(TurnSpeed turnSpeed)
        {
            if (_disposed) return;
            
            var message = _turnSpeedConverter.ToOSCMessage(turnSpeed);
            _transmitter.Send(message);
        }
        
        private void OnSmoothingStrengthChanged(SmoothingStrength smoothingStrength)
        {
            if (_disposed) return;
            
            var message = _smoothingStrengthConverter.ToOSCMessage(smoothingStrength);
            _transmitter.Send(message);
        }
        
        private void OnPhotoRateChanged(PhotoRate photoRate)
        {
            if (_disposed) return;
            
            var message = _photoRateConverter.ToOSCMessage(photoRate);
            _transmitter.Send(message);
        }
        
        private void OnDurationChanged(Duration duration)
        {
            if (_disposed) return;
            
            var message = _durationConverter.ToOSCMessage(duration);
            _transmitter.Send(message);
        }
        
        private void OnShowUIInCameraChanged(ShowUIInCameraToggle showUIInCamera)
        {
            if (_disposed) return;

            var message = _showUIInCameraToggleConverter.ToOSCMessage(showUIInCamera);
            _transmitter.Send(message);
        }

        private void OnLockChanged(LockToggle lockToggle)
        {
            if (_disposed) return;

            var message = _lockToggleConverter.ToOSCMessage(lockToggle);
            _transmitter.Send(message);
        }

        public void Sync()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(VRCCameraSynchronizer));
            
            // Send all current values directly (used for initial sync)
            _transmitter.Send(_zoomConverter.ToOSCMessage(_vrcCamera.Zoom.Value));
            _transmitter.Send(_exposureConverter.ToOSCMessage(_vrcCamera.Exposure.Value));
            _transmitter.Send(_focalDistanceConverter.ToOSCMessage(_vrcCamera.FocalDistance.Value));
            _transmitter.Send(_apertureConverter.ToOSCMessage(_vrcCamera.Aperture.Value));
            _transmitter.Send(_hueConverter.ToOSCMessage(_vrcCamera.Hue.Value));
            _transmitter.Send(_saturationConverter.ToOSCMessage(_vrcCamera.Saturation.Value));
            _transmitter.Send(_lightnessConverter.ToOSCMessage(_vrcCamera.Lightness.Value));
            
            // LookAtMeOffset needs special handling
            var lookAtMeOffset = _vrcCamera.LookAtMeOffset.Value;
            _transmitter.Send(_lookAtMeXOffsetConverter.ToOSCMessage(lookAtMeOffset.X));
            _transmitter.Send(_lookAtMeYOffsetConverter.ToOSCMessage(lookAtMeOffset.Y));
            
            _transmitter.Send(_flySpeedConverter.ToOSCMessage(_vrcCamera.FlySpeed.Value));
            _transmitter.Send(_turnSpeedConverter.ToOSCMessage(_vrcCamera.TurnSpeed.Value));
            _transmitter.Send(_smoothingStrengthConverter.ToOSCMessage(_vrcCamera.SmoothingStrength.Value));
            _transmitter.Send(_photoRateConverter.ToOSCMessage(_vrcCamera.PhotoRate.Value));
            _transmitter.Send(_durationConverter.ToOSCMessage(_vrcCamera.Duration.Value));
            _transmitter.Send(_showUIInCameraToggleConverter.ToOSCMessage(_vrcCamera.ShowUIInCamera.Value));
            _transmitter.Send(_lockToggleConverter.ToOSCMessage(_vrcCamera.Lock.Value));
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            // Unsubscribe from events
            if (_vrcCamera != null)
            {
                _vrcCamera.Zoom.OnValueChanged -= OnZoomChanged;
                _vrcCamera.Exposure.OnValueChanged -= OnExposureChanged;
                _vrcCamera.FocalDistance.OnValueChanged -= OnFocalDistanceChanged;
                _vrcCamera.Aperture.OnValueChanged -= OnApertureChanged;
                _vrcCamera.Hue.OnValueChanged -= OnHueChanged;
                _vrcCamera.Saturation.OnValueChanged -= OnSaturationChanged;
                _vrcCamera.Lightness.OnValueChanged -= OnLightnessChanged;
                _vrcCamera.LookAtMeOffset.OnValueChanged -= OnLookAtMeOffsetChanged;
                _vrcCamera.FlySpeed.OnValueChanged -= OnFlySpeedChanged;
                _vrcCamera.TurnSpeed.OnValueChanged -= OnTurnSpeedChanged;
                _vrcCamera.SmoothingStrength.OnValueChanged -= OnSmoothingStrengthChanged;
                _vrcCamera.PhotoRate.OnValueChanged -= OnPhotoRateChanged;
                _vrcCamera.Duration.OnValueChanged -= OnDurationChanged;
                _vrcCamera.ShowUIInCamera.OnValueChanged -= OnShowUIInCameraChanged;
                _vrcCamera.Lock.OnValueChanged -= OnLockChanged;
            }
            
            _transmitter?.Dispose();
            _disposed = true;
        }
    }
}
