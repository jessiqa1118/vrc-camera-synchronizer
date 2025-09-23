using System;
using UnityEngine;
using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    [AddComponentMenu("VRCCamera/VRC Camera Synchronizer")]
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class VRCCameraComponent : MonoBehaviour
    {
        [SerializeField] private string destination = "127.0.0.1";
        [SerializeField] private PortNumber port = new(9000);

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
        [SerializeField] private bool localPlayer = true;
        [SerializeField] private bool remotePlayer = true;
        [SerializeField] private bool environment = true;

        [SerializeField] private bool greenScreen = false;

        // Display-only (no OSC endpoint)
        [SerializeField] private bool items = true;
        [SerializeField] private bool smoothMovement = false;
        [SerializeField] private bool lookAtMe = false;
        [SerializeField] private bool autoLevelRoll = false;
        [SerializeField] private bool autoLevelPitch = false;
        [SerializeField] private bool flying = false;
        [SerializeField] private bool triggerTakesPhotos = false;
        [SerializeField] private bool dollyPathsStayVisible = false;
        [SerializeField] private bool cameraEars = false;
        [SerializeField] private bool showFocus = false;
        [SerializeField] private bool streaming = false;
        [SerializeField] private bool rollWhileFlying = false;
        [SerializeField] private Orientation orientation = Orientation.Landscape;
        [SerializeField] private Mode mode = Mode.Photo;
        [SerializeField] private Transform poseTransform = null;
        [SerializeField] private bool syncPoseFromTransform = false;
        [SerializeField] private Vector3 posePosition = Vector3.zero;
        [SerializeField] private Vector3 poseEuler = Vector3.zero;

        private VRCCamera _vrcCamera;
        private VRCCameraSynchronizer _synchronizer;
        private bool _missingPoseTransformWarned;

        private void OnEnable()
        {
            var cameraComponent = GetComponent<UnityEngine.Camera>();
            if (!cameraComponent)
            {
                Debug.LogError($"[{nameof(VRCCameraComponent)}] Camera component is missing");
                enabled = false;
                return;
            }

            IOSCTransmitter transmitter = null;
            try
            {
                _vrcCamera = new VRCCamera(cameraComponent);
                _missingPoseTransformWarned = false;

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
                _vrcCamera.SetShowUIInCamera(showUIInCamera);
                _vrcCamera.SetLock(lockCamera);
                _vrcCamera.SetLocalPlayer(localPlayer);
                _vrcCamera.SetRemotePlayer(remotePlayer);
                _vrcCamera.SetEnvironment(environment);
                _vrcCamera.SetGreenScreen(greenScreen);
                _vrcCamera.SetSmoothMovement(smoothMovement);
                _vrcCamera.SetLookAtMe(lookAtMe);
                _vrcCamera.SetAutoLevelRoll(autoLevelRoll);
                _vrcCamera.SetAutoLevelPitch(autoLevelPitch);
                _vrcCamera.SetFlying(flying);
                _vrcCamera.SetTriggerTakesPhotos(triggerTakesPhotos);
                _vrcCamera.SetDollyPathsStayVisible(dollyPathsStayVisible);
                _vrcCamera.SetCameraEars(cameraEars);
                _vrcCamera.SetShowFocus(showFocus);
                _vrcCamera.SetStreaming(streaming);
                _vrcCamera.SetRollWhileFlying(rollWhileFlying);
                _vrcCamera.SetOrientation(orientation);
                _vrcCamera.SetMode(mode);
                ApplyPose();

                transmitter = new OSCTransmitter(destination, port.Value);
                _synchronizer = new VRCCameraSynchronizer(transmitter, _vrcCamera);
            }
            catch (Exception ex)
            {
                transmitter?.Dispose();
                Debug.LogError(
                    $"[{nameof(VRCCameraComponent)}] Failed to initialize: {ex.Message}\n{ex.StackTrace}");
                enabled = false;
            }
        }

        private void OnDisable()
        {
            _synchronizer?.Dispose();
            _synchronizer = null;
            _vrcCamera?.Dispose();
            _vrcCamera = null;
            _missingPoseTransformWarned = false;
        }

        // Action wrappers for Editor buttons or external callers
        public void Action_CloseCamera()
        {
            _synchronizer?.Close();
        }

        public void Action_Capture()
        {
            _synchronizer?.Capture();
        }

        public void Action_CaptureDelayed()
        {
            _synchronizer?.CaptureDelayed();
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
            _vrcCamera.SetShowUIInCamera(showUIInCamera);
            _vrcCamera.SetLock(lockCamera);
            _vrcCamera.SetLocalPlayer(localPlayer);
            _vrcCamera.SetRemotePlayer(remotePlayer);
            _vrcCamera.SetEnvironment(environment);
            _vrcCamera.SetGreenScreen(greenScreen);
            _vrcCamera.SetSmoothMovement(smoothMovement);
            _vrcCamera.SetLookAtMe(lookAtMe);
            _vrcCamera.SetAutoLevelRoll(autoLevelRoll);
            _vrcCamera.SetAutoLevelPitch(autoLevelPitch);
            _vrcCamera.SetFlying(flying);
            _vrcCamera.SetTriggerTakesPhotos(triggerTakesPhotos);
            _vrcCamera.SetDollyPathsStayVisible(dollyPathsStayVisible);
            _vrcCamera.SetCameraEars(cameraEars);
            _vrcCamera.SetShowFocus(showFocus);
            _vrcCamera.SetStreaming(streaming);
            _vrcCamera.SetRollWhileFlying(rollWhileFlying);
            _vrcCamera.SetOrientation(orientation);
            _vrcCamera.SetMode(mode);
            ApplyPose();
        }

        private void ApplyPose()
        {
            if (_vrcCamera == null) return;

            Pose nextPose;
            if (syncPoseFromTransform)
            {
                var src = poseTransform;
                if (src == null)
                {
                    src = transform;
                    if (!_missingPoseTransformWarned)
                    {
                        Debug.LogWarning(
                            $"[{nameof(VRCCameraComponent)}] Follow Object is enabled but no source transform is assigned; falling back to this component's transform.");
                        _missingPoseTransformWarned = true;
                    }
                }

                nextPose = new Pose(src.position, src.rotation);
            }
            else
            {
                nextPose = new Pose(posePosition, Quaternion.Euler(poseEuler));
            }

            // Keep runtime pose aligned with either the follow target or manual fields.
            _vrcCamera?.SetPose(nextPose);
        }

    }
}
