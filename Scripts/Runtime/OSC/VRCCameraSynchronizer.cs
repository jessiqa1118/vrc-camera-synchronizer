using System;
using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public class VRCCameraSynchronizer : IDisposable
    {
        private readonly IOSCTransmitter _transmitter;
        private readonly VRCCamera _vrcCamera;
        private readonly PoseConverter _poseConverter = new();
        private readonly ModeConverter _modeConverter = new();
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
            _vrcCamera.LocalPlayer.OnValueChanged += OnLocalPlayerChanged;
            _vrcCamera.RemotePlayer.OnValueChanged += OnRemotePlayerChanged;
            _vrcCamera.Environment.OnValueChanged += OnEnvironmentChanged;
            _vrcCamera.GreenScreen.OnValueChanged += OnGreenScreenChanged;
            _vrcCamera.SmoothMovement.OnValueChanged += OnSmoothMovementChanged;
            _vrcCamera.LookAtMe.OnValueChanged += OnLookAtMeChanged;
            _vrcCamera.AutoLevelRoll.OnValueChanged += OnAutoLevelRollChanged;
            _vrcCamera.AutoLevelPitch.OnValueChanged += OnAutoLevelPitchChanged;
            _vrcCamera.Flying.OnValueChanged += OnFlyingChanged;
            _vrcCamera.TriggerTakesPhotos.OnValueChanged += OnTriggerTakesPhotosChanged;
            _vrcCamera.DollyPathsStayVisible.OnValueChanged += OnDollyPathsStayVisibleChanged;
            _vrcCamera.Pose.OnValueChanged += OnPoseChanged;
            _vrcCamera.Mode.OnValueChanged += OnModeChanged;
            _vrcCamera.CameraEars.OnValueChanged += OnCameraEarsChanged;
            _vrcCamera.ShowFocus.OnValueChanged += OnShowFocusChanged;
            _vrcCamera.Streaming.OnValueChanged += OnStreamingChanged;
            _vrcCamera.RollWhileFlying.OnValueChanged += OnRollWhileFlyingChanged;
            _vrcCamera.Orientation.OnValueChanged += OnOrientationChanged;

            // Send initial values
            Sync();
        }

        private void OnZoomChanged(Zoom zoom)
        {
            if (_disposed) return;

            _transmitter.Send(new ZoomOscMessage(zoom));
        }

        private void OnExposureChanged(Exposure exposure)
        {
            if (_disposed) return;

            _transmitter.Send(new ExposureOscMessage(exposure));
        }

        private void OnFocalDistanceChanged(FocalDistance focalDistance)
        {
            if (_disposed) return;

            _transmitter.Send(new FocalDistanceOscMessage(focalDistance));
        }

        private void OnApertureChanged(Aperture aperture)
        {
            if (_disposed) return;

            _transmitter.Send(new ApertureOscMessage(aperture));
        }

        private void OnHueChanged(Hue hue)
        {
            if (_disposed) return;

            _transmitter.Send(new HueOscMessage(hue));
        }

        private void OnSaturationChanged(Saturation saturation)
        {
            if (_disposed) return;

            _transmitter.Send(new SaturationOscMessage(saturation));
        }

        private void OnLightnessChanged(Lightness lightness)
        {
            if (_disposed) return;

            _transmitter.Send(new LightnessOscMessage(lightness));
        }

        private void OnLookAtMeOffsetChanged(LookAtMeOffset lookAtMeOffset)
        {
            if (_disposed) return;

            _transmitter.Send(new LookAtMeXOffsetOscMessage(lookAtMeOffset.X));
            _transmitter.Send(new LookAtMeYOffsetOscMessage(lookAtMeOffset.Y));
        }

        private void OnFlySpeedChanged(FlySpeed flySpeed)
        {
            if (_disposed) return;

            _transmitter.Send(new FlySpeedOscMessage(flySpeed));
        }

        private void OnTurnSpeedChanged(TurnSpeed turnSpeed)
        {
            if (_disposed) return;

            _transmitter.Send(new TurnSpeedOscMessage(turnSpeed));
        }

        private void OnSmoothingStrengthChanged(SmoothingStrength smoothingStrength)
        {
            if (_disposed) return;

            _transmitter.Send(new SmoothingStrengthOscMessage(smoothingStrength));
        }

        private void OnPhotoRateChanged(PhotoRate photoRate)
        {
            if (_disposed) return;

            _transmitter.Send(new PhotoRateOscMessage(photoRate));
        }

        private void OnDurationChanged(Duration duration)
        {
            if (_disposed) return;

            _transmitter.Send(new DurationOscMessage(duration));
        }

        private void OnShowUIInCameraChanged(bool showUIInCamera)
        {
            if (_disposed) return;

            var message = new ShowUIInCameraToggleOscMessage(showUIInCamera);
            _transmitter.Send(message);
        }

        private void OnLockChanged(bool lockToggle)
        {
            if (_disposed) return;

            var message = new LockToggleOscMessage(lockToggle);
            _transmitter.Send(message);
        }

        private void OnLocalPlayerChanged(bool localPlayer)
        {
            if (_disposed) return;

            var message = new LocalPlayerToggleOscMessage(localPlayer);
            _transmitter.Send(message);
        }

        private void OnRemotePlayerChanged(bool remotePlayer)
        {
            if (_disposed) return;

            var message = new RemotePlayerToggleOscMessage(remotePlayer);
            _transmitter.Send(message);
        }

        private void OnEnvironmentChanged(bool environment)
        {
            if (_disposed) return;

            var message = new EnvironmentToggleOscMessage(environment);
            _transmitter.Send(message);
        }

        private void OnGreenScreenChanged(bool greenScreen)
        {
            if (_disposed) return;

            var message = new GreenScreenToggleOscMessage(greenScreen);
            _transmitter.Send(message);
        }

        private void OnSmoothMovementChanged(bool smoothMovement)
        {
            if (_disposed) return;

            var message = new SmoothMovementToggleOscMessage(smoothMovement);
            _transmitter.Send(message);
        }

        private void OnLookAtMeChanged(bool lookAtMe)
        {
            if (_disposed) return;

            var message = new LookAtMeToggleOscMessage(lookAtMe);
            _transmitter.Send(message);
        }

        private void OnAutoLevelRollChanged(bool autoLevelRoll)
        {
            if (_disposed) return;

            var message = new AutoLevelRollToggleOscMessage(autoLevelRoll);
            _transmitter.Send(message);
        }

        private void OnAutoLevelPitchChanged(bool autoLevelPitch)
        {
            if (_disposed) return;

            _transmitter.Send(new AutoLevelPitchToggleOscMessage(autoLevelPitch));
        }

        private void OnFlyingChanged(bool flying)
        {
            if (_disposed) return;

            var message = new FlyingToggleOscMessage(flying);
            _transmitter.Send(message);
        }

        private void OnTriggerTakesPhotosChanged(bool trigger)
        {
            if (_disposed) return;

            var message = new TriggerTakesPhotosToggleOscMessage(trigger);
            _transmitter.Send(message);
        }

        private void OnDollyPathsStayVisibleChanged(bool dolly)
        {
            if (_disposed) return;

            var message = new DollyPathsStayVisibleToggleOscMessage(dolly);
            _transmitter.Send(message);
        }

        private void OnCameraEarsChanged(bool cameraEars)
        {
            if (_disposed) return;

            var message = new CameraEarsToggleOscMessage(cameraEars);
            _transmitter.Send(message);
        }

        private void OnShowFocusChanged(bool showFocus)
        {
            if (_disposed) return;

            var message = new ShowFocusToggleOscMessage(showFocus);
            _transmitter.Send(message);
        }

        private void OnStreamingChanged(bool streaming)
        {
            if (_disposed) return;

            var message = new StreamingToggleOscMessage(streaming);
            _transmitter.Send(message);
        }

        private void OnRollWhileFlyingChanged(bool rollWhileFlying)
        {
            if (_disposed) return;

            var message = new RollWhileFlyingToggleOscMessage(rollWhileFlying);
            _transmitter.Send(message);
        }

        private void OnOrientationChanged(Orientation orientation)
        {
            if (_disposed) return;

            bool isLandscape = orientation == Orientation.Landscape;
            var message = new Message(OSCCameraEndpoints.OrientationIsLandscape,
                new[] { new Argument(isLandscape) });
            _transmitter.Send(message);
        }

        public void Sync()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(VRCCameraSynchronizer));

            // Send all current values directly (used for initial sync)
            _transmitter.Send(new ZoomOscMessage(_vrcCamera.Zoom.Value));
            _transmitter.Send(new ExposureOscMessage(_vrcCamera.Exposure.Value));
            _transmitter.Send(new FocalDistanceOscMessage(_vrcCamera.FocalDistance.Value));
            _transmitter.Send(new ApertureOscMessage(_vrcCamera.Aperture.Value));
            _transmitter.Send(new HueOscMessage(_vrcCamera.Hue.Value));
            _transmitter.Send(new SaturationOscMessage(_vrcCamera.Saturation.Value));
            _transmitter.Send(new LightnessOscMessage(_vrcCamera.Lightness.Value));

            var lookAtMeOffset = _vrcCamera.LookAtMeOffset.Value;
            _transmitter.Send(new LookAtMeXOffsetOscMessage(lookAtMeOffset.X));
            _transmitter.Send(new LookAtMeYOffsetOscMessage(lookAtMeOffset.Y));

            _transmitter.Send(new FlySpeedOscMessage(_vrcCamera.FlySpeed.Value));
            _transmitter.Send(new TurnSpeedOscMessage(_vrcCamera.TurnSpeed.Value));
            _transmitter.Send(new SmoothingStrengthOscMessage(_vrcCamera.SmoothingStrength.Value));
            _transmitter.Send(new PhotoRateOscMessage(_vrcCamera.PhotoRate.Value));
            _transmitter.Send(new DurationOscMessage(_vrcCamera.Duration.Value));
            _transmitter.Send(_poseConverter.ToOSCMessage(_vrcCamera.Pose.Value));
            _transmitter.Send(new ShowUIInCameraToggleOscMessage(_vrcCamera.ShowUIInCamera.Value));
            _transmitter.Send(new LockToggleOscMessage(_vrcCamera.Lock.Value));
            _transmitter.Send(new LocalPlayerToggleOscMessage(_vrcCamera.LocalPlayer.Value));
            _transmitter.Send(new RemotePlayerToggleOscMessage(_vrcCamera.RemotePlayer.Value));
            _transmitter.Send(new EnvironmentToggleOscMessage(_vrcCamera.Environment.Value));
            _transmitter.Send(new GreenScreenToggleOscMessage(_vrcCamera.GreenScreen.Value));
            _transmitter.Send(new SmoothMovementToggleOscMessage(_vrcCamera.SmoothMovement.Value));
            _transmitter.Send(new LookAtMeToggleOscMessage(_vrcCamera.LookAtMe.Value));
            _transmitter.Send(new AutoLevelRollToggleOscMessage(_vrcCamera.AutoLevelRoll.Value));
            _transmitter.Send(new AutoLevelPitchToggleOscMessage(_vrcCamera.AutoLevelPitch.Value));
            _transmitter.Send(new FlyingToggleOscMessage(_vrcCamera.Flying.Value));
            _transmitter.Send(new TriggerTakesPhotosToggleOscMessage(_vrcCamera.TriggerTakesPhotos.Value));
            _transmitter.Send(
                new DollyPathsStayVisibleToggleOscMessage(_vrcCamera.DollyPathsStayVisible.Value));
            _transmitter.Send(new CameraEarsToggleOscMessage(_vrcCamera.CameraEars.Value));
            _transmitter.Send(new ShowFocusToggleOscMessage(_vrcCamera.ShowFocus.Value));
            _transmitter.Send(new StreamingToggleOscMessage(_vrcCamera.Streaming.Value));
            _transmitter.Send(new RollWhileFlyingToggleOscMessage(_vrcCamera.RollWhileFlying.Value));
            _transmitter.Send(_modeConverter.ToOSCMessage(_vrcCamera.Mode.Value));
            var isLandscape = _vrcCamera.Orientation.Value == Orientation.Landscape;
            _transmitter.Send(new Message(OSCCameraEndpoints.OrientationIsLandscape,
                new[] { new Argument(isLandscape) }));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true; // Gate all handlers before tearing down

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
                _vrcCamera.LocalPlayer.OnValueChanged -= OnLocalPlayerChanged;
                _vrcCamera.RemotePlayer.OnValueChanged -= OnRemotePlayerChanged;
                _vrcCamera.Environment.OnValueChanged -= OnEnvironmentChanged;
                _vrcCamera.GreenScreen.OnValueChanged -= OnGreenScreenChanged;
                _vrcCamera.SmoothMovement.OnValueChanged -= OnSmoothMovementChanged;
                _vrcCamera.LookAtMe.OnValueChanged -= OnLookAtMeChanged;
                _vrcCamera.AutoLevelRoll.OnValueChanged -= OnAutoLevelRollChanged;
                _vrcCamera.AutoLevelPitch.OnValueChanged -= OnAutoLevelPitchChanged;
                _vrcCamera.Flying.OnValueChanged -= OnFlyingChanged;
                _vrcCamera.TriggerTakesPhotos.OnValueChanged -= OnTriggerTakesPhotosChanged;
                _vrcCamera.DollyPathsStayVisible.OnValueChanged -= OnDollyPathsStayVisibleChanged;
                _vrcCamera.Pose.OnValueChanged -= OnPoseChanged;
                _vrcCamera.CameraEars.OnValueChanged -= OnCameraEarsChanged;
                _vrcCamera.ShowFocus.OnValueChanged -= OnShowFocusChanged;
                _vrcCamera.Streaming.OnValueChanged -= OnStreamingChanged;
                _vrcCamera.RollWhileFlying.OnValueChanged -= OnRollWhileFlyingChanged;
                _vrcCamera.Orientation.OnValueChanged -= OnOrientationChanged;
                _vrcCamera.Mode.OnValueChanged -= OnModeChanged;
            }

            _transmitter?.Dispose();
        }

        public void Close() => SendAction(OSCCameraEndpoints.Close);

        public void Capture() => SendAction(OSCCameraEndpoints.Capture);

        public void CaptureDelayed() => SendAction(OSCCameraEndpoints.CaptureDelayed);

        private void SendAction(Address address)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(VRCCameraSynchronizer));
            _transmitter.Send(new Message(address, Array.Empty<Argument>()));
        }

        private void OnPoseChanged(UnityEngine.Pose pose)
        {
            if (_disposed) return;
            var message = _poseConverter.ToOSCMessage(pose);
            _transmitter.Send(message);
        }

        private void OnModeChanged(Mode mode)
        {
            if (_disposed) return;

            var message = _modeConverter.ToOSCMessage(mode);
            _transmitter.Send(message);
        }
    }
}
