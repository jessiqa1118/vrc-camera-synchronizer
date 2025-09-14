using System;
using UnityEngine;
using Parameters;

namespace VRCCamera
{
    [AddComponentMenu("VRCCamera/VRC Camera Synchronizer")]
    [RequireComponent(typeof(Camera))]
    public class VRCCameraSynchronizerComponent : MonoBehaviour
    {
        [SerializeField] private string destination = "127.0.0.1";
        [SerializeField] private int port = 9000;
        
        [SerializeField] private float exposure = Exposure.DefaultValue;
        [SerializeField] private float hue = Hue.DefaultValue;
        [SerializeField] private float saturation = Saturation.DefaultValue;
        [SerializeField] private float lightness = Lightness.DefaultValue;
        [SerializeField] private float lookAtMeXOffset = LookAtMeXOffset.DefaultValue;
        [SerializeField] private float lookAtMeYOffset = LookAtMeYOffset.DefaultValue;
        [SerializeField] private float flySpeed = FlySpeed.DefaultValue;
        [SerializeField] private float turnSpeed = TurnSpeed.DefaultValue;
        [SerializeField] private float smoothingStrength = SmoothingStrength.DefaultValue;
        [SerializeField] private float photoRate = PhotoRate.DefaultValue;
        [SerializeField] private float duration = Duration.DefaultValue;
        [SerializeField] private bool showUIInCamera = false;
        [SerializeField] private bool lockCamera = false;
        [SerializeField] private bool localPlayer = false;
        [SerializeField] private bool remotePlayer = false;
        [SerializeField] private bool environment = false;
        [SerializeField] private bool greenScreen = false;
        [SerializeField] private bool smoothMovement = false;
        [SerializeField] private bool lookAtMe = false;
        [SerializeField] private bool autoLevelRoll = false;
        [SerializeField] private bool autoLevelPitch = false;
        [SerializeField] private bool flying = false;
        [SerializeField] private bool triggerTakesPhotos = false;
        [SerializeField] private bool dollyPathsStayVisible = false;

        private VRCCamera _vrcCamera;
        private VRCCameraSynchronizer _synchronizer;

        private void OnEnable()
        {
            var cameraComponent = GetComponent<Camera>();
            if (!cameraComponent)
            {
                Debug.LogError($"[{nameof(VRCCameraSynchronizer)}] Camera component is missing");
                enabled = false;
                return;
            }

            IOSCTransmitter transmitter = null;
            try
            {
                _vrcCamera = new VRCCamera(cameraComponent);
                
                // Set all initial values before creating synchronizer to avoid duplicate messages
                _vrcCamera.SetExposure(new Exposure(exposure));
                _vrcCamera.SetHue(new Hue(hue));
                _vrcCamera.SetSaturation(new Saturation(saturation));
                _vrcCamera.SetLightness(new Lightness(lightness));
                _vrcCamera.SetLookAtMeOffset(new LookAtMeOffset(
                    new LookAtMeXOffset(lookAtMeXOffset),
                    new LookAtMeYOffset(lookAtMeYOffset)));
                _vrcCamera.SetFlySpeed(new FlySpeed(flySpeed));
                _vrcCamera.SetTurnSpeed(new TurnSpeed(turnSpeed));
                _vrcCamera.SetSmoothingStrength(new SmoothingStrength(smoothingStrength));
                _vrcCamera.SetPhotoRate(new PhotoRate(photoRate));
                _vrcCamera.SetDuration(new Duration(duration));
                _vrcCamera.SetShowUIInCamera(new ShowUIInCameraToggle(showUIInCamera));
                _vrcCamera.SetLock(new LockToggle(lockCamera));
                _vrcCamera.SetLocalPlayer(new LocalPlayerToggle(localPlayer));
                _vrcCamera.SetRemotePlayer(new RemotePlayerToggle(remotePlayer));
                _vrcCamera.SetEnvironment(new EnvironmentToggle(environment));
                _vrcCamera.SetGreenScreen(new GreenScreenToggle(greenScreen));
                _vrcCamera.SetSmoothMovement(new SmoothMovementToggle(smoothMovement));
                _vrcCamera.SetLookAtMe(new LookAtMeToggle(lookAtMe));
                _vrcCamera.SetAutoLevelRoll(new AutoLevelRollToggle(autoLevelRoll));
                _vrcCamera.SetAutoLevelPitch(new AutoLevelPitchToggle(autoLevelPitch));
                _vrcCamera.SetFlying(new FlyingToggle(flying));
                _vrcCamera.SetTriggerTakesPhotos(new TriggerTakesPhotosToggle(triggerTakesPhotos));
                _vrcCamera.SetDollyPathsStayVisible(new DollyPathsStayVisibleToggle(dollyPathsStayVisible));
                
                transmitter = new OSCJackTransmitter(destination, port);
                _synchronizer = new VRCCameraSynchronizer(transmitter, _vrcCamera);
            }
            catch (Exception ex)
            {
                transmitter?.Dispose();
                Debug.LogError(
                    $"[{nameof(VRCCameraSynchronizer)}] Failed to initialize: {ex.Message}\n{ex.StackTrace}");
                enabled = false;
            }
        }

        private void OnDisable()
        {
            _synchronizer?.Dispose();
            _synchronizer = null;
            _vrcCamera?.Dispose();
            _vrcCamera = null;
        }

        private void FixedUpdate()
        {
            // NOTE: Use FixedUpdate to reduce the frequency of updates
            if (_vrcCamera == null) return;

            // Update camera-tracked values (Zoom, FocalDistance, Aperture)
            _vrcCamera.UpdateFromCamera();

            // Update other parameters via setter methods
            _vrcCamera.SetExposure(new Exposure(exposure));
            _vrcCamera.SetHue(new Hue(hue));
            _vrcCamera.SetSaturation(new Saturation(saturation));
            _vrcCamera.SetLightness(new Lightness(lightness));
            _vrcCamera.SetLookAtMeOffset(new LookAtMeOffset(
                new LookAtMeXOffset(lookAtMeXOffset),
                new LookAtMeYOffset(lookAtMeYOffset)));
            _vrcCamera.SetFlySpeed(new FlySpeed(flySpeed));
            _vrcCamera.SetTurnSpeed(new TurnSpeed(turnSpeed));
            _vrcCamera.SetSmoothingStrength(new SmoothingStrength(smoothingStrength));
            _vrcCamera.SetPhotoRate(new PhotoRate(photoRate));
            _vrcCamera.SetDuration(new Duration(duration));
            _vrcCamera.SetShowUIInCamera(new ShowUIInCameraToggle(showUIInCamera));
            _vrcCamera.SetLock(new LockToggle(lockCamera));
            _vrcCamera.SetLocalPlayer(new LocalPlayerToggle(localPlayer));
            _vrcCamera.SetRemotePlayer(new RemotePlayerToggle(remotePlayer));
            _vrcCamera.SetEnvironment(new EnvironmentToggle(environment));
            _vrcCamera.SetGreenScreen(new GreenScreenToggle(greenScreen));
            _vrcCamera.SetSmoothMovement(new SmoothMovementToggle(smoothMovement));
            _vrcCamera.SetLookAtMe(new LookAtMeToggle(lookAtMe));
            _vrcCamera.SetAutoLevelRoll(new AutoLevelRollToggle(autoLevelRoll));
            _vrcCamera.SetAutoLevelPitch(new AutoLevelPitchToggle(autoLevelPitch));
            _vrcCamera.SetFlying(new FlyingToggle(flying));
            _vrcCamera.SetTriggerTakesPhotos(new TriggerTakesPhotosToggle(triggerTakesPhotos));
            _vrcCamera.SetDollyPathsStayVisible(new DollyPathsStayVisibleToggle(dollyPathsStayVisible));
        }
    }
}
