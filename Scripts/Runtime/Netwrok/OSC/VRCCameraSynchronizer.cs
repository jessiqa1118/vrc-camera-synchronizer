using System;
using System.Collections.Generic;
using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public class VRCCameraSynchronizer : IDisposable
    {
        private readonly IOSCTransmitter _transmitter;
        private readonly VRCCamera _vrcCamera;
        private readonly IVRCCameraMessageFactory _messageFactory;
        private bool _disposed = false;

        public VRCCameraSynchronizer(
            IOSCTransmitter transmitter,
            VRCCamera vrcCamera,
            IVRCCameraMessageFactory messageFactory = null)
        {
            _transmitter = transmitter ?? throw new ArgumentNullException(nameof(transmitter));
            _vrcCamera = vrcCamera ?? throw new ArgumentNullException(nameof(vrcCamera));
            _messageFactory = messageFactory ?? new VRCCameraMessageFactory();

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

        public void Sync()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(VRCCameraSynchronizer));

            // Send all current values directly (used for initial sync)
            DispatchMessages(_messageFactory.CreateZoom(_vrcCamera.Zoom.Value));
            DispatchMessages(_messageFactory.CreateExposure(_vrcCamera.Exposure.Value));
            DispatchMessages(_messageFactory.CreateFocalDistance(_vrcCamera.FocalDistance.Value));
            DispatchMessages(_messageFactory.CreateAperture(_vrcCamera.Aperture.Value));
            DispatchMessages(_messageFactory.CreateHue(_vrcCamera.Hue.Value));
            DispatchMessages(_messageFactory.CreateSaturation(_vrcCamera.Saturation.Value));
            DispatchMessages(_messageFactory.CreateLightness(_vrcCamera.Lightness.Value));
            DispatchMessages(_messageFactory.CreateLookAtMeOffset(_vrcCamera.LookAtMeOffset.Value));
            DispatchMessages(_messageFactory.CreateFlySpeed(_vrcCamera.FlySpeed.Value));
            DispatchMessages(_messageFactory.CreateTurnSpeed(_vrcCamera.TurnSpeed.Value));
            DispatchMessages(_messageFactory.CreateSmoothingStrength(_vrcCamera.SmoothingStrength.Value));
            DispatchMessages(_messageFactory.CreatePhotoRate(_vrcCamera.PhotoRate.Value));
            DispatchMessages(_messageFactory.CreateDuration(_vrcCamera.Duration.Value));
            DispatchMessages(_messageFactory.CreatePose(_vrcCamera.Pose.Value));
            DispatchMessages(_messageFactory.CreateShowUIInCamera(_vrcCamera.ShowUIInCamera.Value));
            DispatchMessages(_messageFactory.CreateLock(_vrcCamera.Lock.Value));
            DispatchMessages(_messageFactory.CreateLocalPlayer(_vrcCamera.LocalPlayer.Value));
            DispatchMessages(_messageFactory.CreateRemotePlayer(_vrcCamera.RemotePlayer.Value));
            DispatchMessages(_messageFactory.CreateEnvironment(_vrcCamera.Environment.Value));
            DispatchMessages(_messageFactory.CreateGreenScreen(_vrcCamera.GreenScreen.Value));
            DispatchMessages(_messageFactory.CreateSmoothMovement(_vrcCamera.SmoothMovement.Value));
            DispatchMessages(_messageFactory.CreateLookAtMe(_vrcCamera.LookAtMe.Value));
            DispatchMessages(_messageFactory.CreateAutoLevelRoll(_vrcCamera.AutoLevelRoll.Value));
            DispatchMessages(_messageFactory.CreateAutoLevelPitch(_vrcCamera.AutoLevelPitch.Value));
            DispatchMessages(_messageFactory.CreateFlying(_vrcCamera.Flying.Value));
            DispatchMessages(_messageFactory.CreateTriggerTakesPhotos(_vrcCamera.TriggerTakesPhotos.Value));
            DispatchMessages(_messageFactory.CreateDollyPathsStayVisible(_vrcCamera.DollyPathsStayVisible.Value));
            DispatchMessages(_messageFactory.CreateCameraEars(_vrcCamera.CameraEars.Value));
            DispatchMessages(_messageFactory.CreateShowFocus(_vrcCamera.ShowFocus.Value));
            DispatchMessages(_messageFactory.CreateStreaming(_vrcCamera.Streaming.Value));
            DispatchMessages(_messageFactory.CreateRollWhileFlying(_vrcCamera.RollWhileFlying.Value));
            DispatchMessages(_messageFactory.CreateMode(_vrcCamera.Mode.Value));
            DispatchMessages(_messageFactory.CreateOrientation(_vrcCamera.Orientation.Value));
        }

        private void DispatchMessages(IEnumerable<IOSCMessage> messages)
        {
            if (_disposed || messages == null)
            {
                return;
            }

            foreach (var message in messages)
            {
                if (message == null)
                {
                    continue;
                }

                _transmitter.Send(message);
            }
        }

        private void OnZoomChanged(Zoom zoom) => DispatchMessages(_messageFactory.CreateZoom(zoom));

        private void OnExposureChanged(Exposure exposure) =>
            DispatchMessages(_messageFactory.CreateExposure(exposure));

        private void OnFocalDistanceChanged(FocalDistance focalDistance) =>
            DispatchMessages(_messageFactory.CreateFocalDistance(focalDistance));

        private void OnApertureChanged(Aperture aperture) =>
            DispatchMessages(_messageFactory.CreateAperture(aperture));

        private void OnHueChanged(Hue hue) => DispatchMessages(_messageFactory.CreateHue(hue));

        private void OnSaturationChanged(Saturation saturation) =>
            DispatchMessages(_messageFactory.CreateSaturation(saturation));

        private void OnLightnessChanged(Lightness lightness) =>
            DispatchMessages(_messageFactory.CreateLightness(lightness));

        private void OnLookAtMeOffsetChanged(LookAtMeOffset lookAtMeOffset) =>
            DispatchMessages(_messageFactory.CreateLookAtMeOffset(lookAtMeOffset));

        private void OnFlySpeedChanged(FlySpeed flySpeed) =>
            DispatchMessages(_messageFactory.CreateFlySpeed(flySpeed));

        private void OnTurnSpeedChanged(TurnSpeed turnSpeed) =>
            DispatchMessages(_messageFactory.CreateTurnSpeed(turnSpeed));

        private void OnSmoothingStrengthChanged(SmoothingStrength smoothingStrength) =>
            DispatchMessages(_messageFactory.CreateSmoothingStrength(smoothingStrength));

        private void OnPhotoRateChanged(PhotoRate photoRate) =>
            DispatchMessages(_messageFactory.CreatePhotoRate(photoRate));

        private void OnDurationChanged(Duration duration) =>
            DispatchMessages(_messageFactory.CreateDuration(duration));

        private void OnShowUIInCameraChanged(bool showUIInCamera) =>
            DispatchMessages(_messageFactory.CreateShowUIInCamera(showUIInCamera));

        private void OnLockChanged(bool lockToggle) =>
            DispatchMessages(_messageFactory.CreateLock(lockToggle));

        private void OnLocalPlayerChanged(bool localPlayer) =>
            DispatchMessages(_messageFactory.CreateLocalPlayer(localPlayer));

        private void OnRemotePlayerChanged(bool remotePlayer) =>
            DispatchMessages(_messageFactory.CreateRemotePlayer(remotePlayer));

        private void OnEnvironmentChanged(bool environment) =>
            DispatchMessages(_messageFactory.CreateEnvironment(environment));

        private void OnGreenScreenChanged(bool greenScreen) =>
            DispatchMessages(_messageFactory.CreateGreenScreen(greenScreen));

        private void OnSmoothMovementChanged(bool smoothMovement) =>
            DispatchMessages(_messageFactory.CreateSmoothMovement(smoothMovement));

        private void OnLookAtMeChanged(bool lookAtMe) =>
            DispatchMessages(_messageFactory.CreateLookAtMe(lookAtMe));

        private void OnAutoLevelRollChanged(bool autoLevelRoll) =>
            DispatchMessages(_messageFactory.CreateAutoLevelRoll(autoLevelRoll));

        private void OnAutoLevelPitchChanged(bool autoLevelPitch) =>
            DispatchMessages(_messageFactory.CreateAutoLevelPitch(autoLevelPitch));

        private void OnFlyingChanged(bool flying) =>
            DispatchMessages(_messageFactory.CreateFlying(flying));

        private void OnTriggerTakesPhotosChanged(bool trigger) =>
            DispatchMessages(_messageFactory.CreateTriggerTakesPhotos(trigger));

        private void OnDollyPathsStayVisibleChanged(bool dolly) =>
            DispatchMessages(_messageFactory.CreateDollyPathsStayVisible(dolly));

        private void OnCameraEarsChanged(bool cameraEars) =>
            DispatchMessages(_messageFactory.CreateCameraEars(cameraEars));

        private void OnShowFocusChanged(bool showFocus) =>
            DispatchMessages(_messageFactory.CreateShowFocus(showFocus));

        private void OnStreamingChanged(bool streaming) =>
            DispatchMessages(_messageFactory.CreateStreaming(streaming));

        private void OnRollWhileFlyingChanged(bool rollWhileFlying) =>
            DispatchMessages(_messageFactory.CreateRollWhileFlying(rollWhileFlying));

        private void OnOrientationChanged(Orientation orientation) =>
            DispatchMessages(_messageFactory.CreateOrientation(orientation));

        public void Close() => SendAction(OSCCameraEndpoints.Close);

        public void Capture() => SendAction(OSCCameraEndpoints.Capture);

        public void CaptureDelayed() => SendAction(OSCCameraEndpoints.CaptureDelayed);

        private void SendAction(Address address)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(VRCCameraSynchronizer));
            _transmitter.Send(new Message(address, Array.Empty<Argument>()));
        }

        private void OnPoseChanged(UnityEngine.Pose pose) =>
            DispatchMessages(_messageFactory.CreatePose(pose));

        private void OnModeChanged(Mode mode) =>
            DispatchMessages(_messageFactory.CreateMode(mode));
    }
}