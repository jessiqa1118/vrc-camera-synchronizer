using System;
using UnityEngine;
using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    [AddComponentMenu("Astearium/VRChat/VRC Camera")]
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class VRCCameraComponent : MonoBehaviour
    {
        [SerializeField] private string destination;
        [SerializeField] private PortNumber port = new(9000);

        // API: Camera Mode
        [field: SerializeField] public Mode Mode { get; set; }

        // API: Pose
        [field: SerializeField] public Pose Pose { get; set; }

        // API: Actions
        public void Close() => _synchronizer?.Close();
        public void Capture() => _synchronizer?.Capture();
        public void CaptureDelayed() => _synchronizer?.CaptureDelayed();

        // API: Toggles
        [field: SerializeField] public bool ShowUIInCamera { get; set; }
        [field: SerializeField] public bool Lock { get; set; }
        [field: SerializeField] public bool LocalPlayer { get; set; }
        [field: SerializeField] public bool RemotePlayer { get; set; }
        [field: SerializeField] public bool Environment { get; set; }
        [field: SerializeField] public bool GreenScreen { get; set; }
        [field: SerializeField] public bool SmoothMovement { get; set; }
        [field: SerializeField] public bool LookAtMe { get; set; }
        [field: SerializeField] public bool AutoLevelRoll { get; set; }
        [field: SerializeField] public bool AutoLevelPitch { get; set; }
        [field: SerializeField] public bool Flying { get; set; }
        [field: SerializeField] public bool TriggerTakesPhotos { get; set; }
        [field: SerializeField] public bool DollyPathsStayVisible { get; set; }
        [field: SerializeField] public bool CameraEars { get; set; }
        [field: SerializeField] public bool ShowFocus { get; set; }
        [field: SerializeField] public bool Streaming { get; set; }
        [field: SerializeField] public bool RollWhileFlying { get; set; }
        [field: SerializeField] public bool OrientationIsLandscape { get; set; }

        // API: Sliders
        [SerializeField] private float zoom = Zoom.DefaultValue;

        public Zoom Zoom
        {
            get => new(zoom);
            set => zoom = value;
        }

        [SerializeField] private float exposure = Exposure.DefaultValue;

        public Exposure Exposure
        {
            get => new(exposure);
            set => exposure = value;
        }

        [SerializeField] private float focalDistance = FocalDistance.DefaultValue;

        public FocalDistance FocalDistance
        {
            get => new(focalDistance);
            set => focalDistance = value;
        }

        [SerializeField] private float aperture = Aperture.DefaultValue;

        public Aperture Aperture
        {
            get => new(aperture);
            set => aperture = value;
        }

        [SerializeField] private float hue = Hue.DefaultValue;

        public Hue Hue
        {
            get => new(hue);
            set => hue = value;
        }

        [SerializeField] private float saturation = Saturation.DefaultValue;

        public Saturation Saturation
        {
            get => new(saturation);
            set => saturation = value;
        }

        [SerializeField] private float lightness = Lightness.DefaultValue;

        public Lightness Lightness
        {
            get => new(lightness);
            set => lightness = value;
        }

        [SerializeField] private float lookAtMeXOffset = LookAtMeXOffset.DefaultValue;

        public LookAtMeXOffset LookAtMeXOffset
        {
            get => new(lookAtMeXOffset);
            set => lookAtMeXOffset = value;
        }

        [SerializeField] private float lookAtMeYOffset = LookAtMeYOffset.DefaultValue;

        public LookAtMeYOffset LookAtMeYOffset
        {
            get => new(lookAtMeYOffset);
            set => lookAtMeYOffset = value;
        }

        [SerializeField] private float flySpeed = FlySpeed.DefaultValue;

        public FlySpeed FlySpeed
        {
            get => new(flySpeed);
            set => flySpeed = value;
        }

        [SerializeField] private float turnSpeed = TurnSpeed.DefaultValue;

        public TurnSpeed TurnSpeed
        {
            get => new(turnSpeed);
            set => turnSpeed = value;
        }

        [SerializeField] private float smoothingStrength = SmoothingStrength.DefaultValue;

        public SmoothingStrength SmoothingStrength
        {
            get => new(smoothingStrength);
            set => smoothingStrength = value;
        }

        [SerializeField] private float photoRate = PhotoRate.DefaultValue;

        public PhotoRate PhotoRate
        {
            get => new(photoRate);
            set => photoRate = value;
        }

        [SerializeField] private float duration = Duration.DefaultValue;

        public Duration Duration
        {
            get => new(duration);
            set => duration = value;
        }

        // Display-only (no OSC endpoint)
        [SerializeField] private bool items = true;

        [SerializeField] private bool syncUnityCamera = false;
        [SerializeField] private UnityEngine.Camera unityCamera = null;

        [SerializeField] private bool syncPoseFromTransform = false;
        [SerializeField] private Transform poseSource = null;
        [SerializeField] private Vector3 posePosition = Vector3.zero;
        [SerializeField] private Vector3 poseEuler = Vector3.zero;

        [SerializeField] private float zoomValue;

        private UnityEngine.Camera _unityCamera;
        private VRCCamera _vrcCamera;
        private VRCCameraSynchronizer _synchronizer;

        private void OnEnable()
        {
            try
            {
                _vrcCamera = new VRCCamera();

                // Set all initial values before creating synchronizer to avoid duplicate messages
                _vrcCamera.SetExposure(Exposure);
                _vrcCamera.SetHue(Hue);
                _vrcCamera.SetSaturation(Saturation);
                _vrcCamera.SetLightness(Lightness);
                _vrcCamera.SetLookAtMeOffset(new LookAtMeOffset(LookAtMeXOffset, LookAtMeYOffset));
                _vrcCamera.SetFlySpeed(FlySpeed);
                _vrcCamera.SetTurnSpeed(TurnSpeed);
                _vrcCamera.SetSmoothingStrength(SmoothingStrength);
                _vrcCamera.SetPhotoRate(PhotoRate);
                _vrcCamera.SetDuration(Duration);
                _vrcCamera.SetShowUIInCamera(ShowUIInCamera);
                _vrcCamera.SetLock(Lock);
                _vrcCamera.SetLocalPlayer(LocalPlayer);
                _vrcCamera.SetRemotePlayer(RemotePlayer);
                _vrcCamera.SetEnvironment(Environment);
                _vrcCamera.SetGreenScreen(GreenScreen);
                _vrcCamera.SetSmoothMovement(SmoothMovement);
                _vrcCamera.SetLookAtMe(LookAtMe);
                _vrcCamera.SetAutoLevelPitch(AutoLevelPitch);
                _vrcCamera.SetAutoLevelRoll(AutoLevelRoll);
                _vrcCamera.SetFlying(Flying);
                _vrcCamera.SetTriggerTakesPhotos(TriggerTakesPhotos);
                _vrcCamera.SetDollyPathsStayVisible(DollyPathsStayVisible);
                _vrcCamera.SetCameraEars(CameraEars);
                _vrcCamera.SetShowFocus(ShowFocus);
                _vrcCamera.SetStreaming(Streaming);
                _vrcCamera.SetRollWhileFlying(RollWhileFlying);
                _vrcCamera.SetOrientation(OrientationIsLandscape ? Orientation.Landscape : Orientation.Portrait);
                _vrcCamera.SetMode(Mode);

                SyncUnityCamera();
                _vrcCamera.SetZoom(Zoom);
                _vrcCamera.SetFocalDistance(FocalDistance);
                _vrcCamera.SetAperture(Aperture);

                SyncPose();
                _vrcCamera?.SetPose(new Pose(posePosition, Quaternion.Euler(poseEuler)));

                var transmitter = new OSCTransmitter(destination, port.Value);
                _synchronizer = new VRCCameraSynchronizer(transmitter, _vrcCamera);
            }
            catch (Exception ex)
            {
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
            _unityCamera = null;
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
            SyncUnityCamera();

            _vrcCamera.SetZoom(Zoom);
            _vrcCamera.SetFocalDistance(FocalDistance);
            _vrcCamera.SetAperture(Aperture);
            _vrcCamera.SetExposure(Exposure);
            _vrcCamera.SetHue(Hue);
            _vrcCamera.SetSaturation(Saturation);
            _vrcCamera.SetLightness(Lightness);
            _vrcCamera.SetLookAtMeOffset(new LookAtMeOffset(LookAtMeXOffset, LookAtMeYOffset));
            _vrcCamera.SetFlySpeed(FlySpeed);
            _vrcCamera.SetTurnSpeed(TurnSpeed);
            _vrcCamera.SetSmoothingStrength(SmoothingStrength);
            _vrcCamera.SetPhotoRate(PhotoRate);
            _vrcCamera.SetDuration(Duration);
            _vrcCamera.SetShowUIInCamera(ShowUIInCamera);
            _vrcCamera.SetLock(Lock);
            _vrcCamera.SetLocalPlayer(LocalPlayer);
            _vrcCamera.SetRemotePlayer(RemotePlayer);
            _vrcCamera.SetEnvironment(Environment);
            _vrcCamera.SetGreenScreen(GreenScreen);
            _vrcCamera.SetSmoothMovement(SmoothMovement);
            _vrcCamera.SetLookAtMe(LookAtMe);
            _vrcCamera.SetAutoLevelPitch(AutoLevelPitch);
            _vrcCamera.SetAutoLevelRoll(AutoLevelRoll);
            _vrcCamera.SetFlying(Flying);
            _vrcCamera.SetTriggerTakesPhotos(TriggerTakesPhotos);
            _vrcCamera.SetDollyPathsStayVisible(DollyPathsStayVisible);
            _vrcCamera.SetCameraEars(CameraEars);
            _vrcCamera.SetShowFocus(ShowFocus);
            _vrcCamera.SetStreaming(Streaming);
            _vrcCamera.SetRollWhileFlying(RollWhileFlying);
            _vrcCamera.SetOrientation(OrientationIsLandscape ? Orientation.Landscape : Orientation.Portrait);
            _vrcCamera.SetMode(Mode);

            SyncPose();
            _vrcCamera?.SetPose(new Pose(posePosition, Quaternion.Euler(poseEuler)));
        }

        private void SyncUnityCamera()
        {
            if (!syncUnityCamera || !_unityCamera) return;
            Zoom = new Zoom(_unityCamera.focalLength);
            FocalDistance = new FocalDistance(_unityCamera.focusDistance);
            Aperture = new Aperture(_unityCamera.aperture);
        }

        private void SyncPose()
        {
            if (!syncPoseFromTransform || !poseSource) return;
            posePosition = poseSource.transform.position;
            poseEuler = poseSource.transform.rotation.eulerAngles;
        }
    }
}
