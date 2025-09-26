using System;
using UnityEngine;
using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    [AddComponentMenu("Astearium/VRChat/VRC Camera")]
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class VRCCameraComponent : MonoBehaviour
    {
#if UNITY_EDITOR
        public const string DestinationFieldName = nameof(destination);
        public const string PortFieldName = nameof(port);

        public const string ModeFieldName = nameof(mode);

        public const string PoseFieldName = nameof(pose);

        public const string ShowUIInCameraFieldName = nameof(showUIInCamera);
        public const string LockCameraFieldName = nameof(lockCamera);
        public const string LocalPlayerFieldName = nameof(localPlayer);
        public const string RemotePlayerFieldName = nameof(remotePlayer);
        public const string EnvironmentFieldName = nameof(environment);
        public const string GreenScreenFieldName = nameof(greenScreen);
        public const string SmoothMovementFieldName = nameof(smoothMovement);
        public const string LookAtMeFieldName = nameof(lookAtMe);
        public const string AutoLevelRollFieldName = nameof(autoLevelRoll);
        public const string AutoLevelPitchFieldName = nameof(autoLevelPitch);
        public const string FlyingFieldName = nameof(flying);
        public const string TriggerTakesPhotosFieldName = nameof(triggerTakesPhotos);
        public const string DollyPathsStayVisibleFieldName = nameof(dollyPathsStayVisible);
        public const string CameraEarsFieldName = nameof(cameraEars);
        public const string ShowFocusFieldName = nameof(showFocus);
        public const string StreamingFieldName = nameof(streaming);
        public const string RollWhileFlyingFieldName = nameof(rollWhileFlying);
        public const string OrientationFieldName = nameof(orientation);

        public const string ZoomFieldName = nameof(zoom);
        public const string ExposureFieldName = nameof(exposure);
        public const string FocalDistanceFieldName = nameof(focalDistance);
        public const string ApertureFieldName = nameof(aperture);
        public const string HueFieldName = nameof(hue);
        public const string SaturationFieldName = nameof(saturation);
        public const string LightnessFieldName = nameof(lightness);
        public const string LookAtMeXOffsetFieldName = nameof(lookAtMeXOffset);
        public const string LookAtMeYOffsetFieldName = nameof(lookAtMeYOffset);
        public const string FlySpeedFieldName = nameof(flySpeed);
        public const string TurnSpeedFieldName = nameof(turnSpeed);
        public const string SmoothingStrengthFieldName = nameof(smoothingStrength);
        public const string PhotoRateFieldName = nameof(photoRate);
        public const string DurationFieldName = nameof(duration);

        public const string ItemsFieldName = nameof(items);
        public const string SyncUnityCameraFieldName = nameof(syncUnityCamera);
        public const string UnityCameraFieldName = nameof(unityCamera);
        public const string SyncPoseFromTransformFieldName = nameof(syncPoseFromTransform);
        public const string PoseSourceFieldName = nameof(poseSource);
#endif

        #region Serialized Fields

        // OSC Endpoint
        [SerializeField] private string destination = "127.0.0.1";
        [SerializeField] private PortNumber port = new(9000);

        // Camera Mode
        [SerializeField] private Mode mode;

        // Pose
        [SerializeField] private Pose pose = Pose.identity;

        // Toggles
        [SerializeField] private bool showUIInCamera;
        [SerializeField] private bool lockCamera;
        [SerializeField] private bool localPlayer;
        [SerializeField] private bool remotePlayer;
        [SerializeField] private bool environment;
        [SerializeField] private bool greenScreen;
        [SerializeField] private bool smoothMovement;
        [SerializeField] private bool lookAtMe;
        [SerializeField] private bool autoLevelRoll;
        [SerializeField] private bool autoLevelPitch;
        [SerializeField] private bool flying;
        [SerializeField] private bool triggerTakesPhotos;
        [SerializeField] private bool dollyPathsStayVisible;
        [SerializeField] private bool cameraEars;
        [SerializeField] private bool showFocus;
        [SerializeField] private bool streaming;
        [SerializeField] private bool rollWhileFlying;
        [SerializeField] private Orientation orientation;

        // Sliders
        [SerializeField] private float zoom = Zoom.DefaultValue;
        [SerializeField] private float exposure = Exposure.DefaultValue;
        [SerializeField] private float focalDistance = FocalDistance.DefaultValue;
        [SerializeField] private float aperture = Aperture.DefaultValue;
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

        // Display-only (no OSC endpoint)
        [SerializeField] private bool items = true;

        [SerializeField] private bool syncUnityCamera = false;
        [SerializeField] private UnityEngine.Camera unityCamera = null;

        [SerializeField] private bool syncPoseFromTransform = false;
        [SerializeField] private Transform poseSource = null;

        #endregion

        private UnityEngine.Camera _unityCamera;
        private VRCCamera _vrcCamera;
        private VRCCameraSynchronizer _synchronizer;

        #region API

        // API: Camera Mode
        public Mode Mode
        {
            get => mode;
            set => mode = value;
        }

        // API: Pose
        public Pose Pose
        {
            get => pose;
            set => pose = value;
        }

        // API: Actions
        public void Close() => _synchronizer?.Close();
        public void Capture() => _synchronizer?.Capture();
        public void CaptureDelayed() => _synchronizer?.CaptureDelayed();

        // API: Toggles
        public bool ShowUIInCamera
        {
            get => showUIInCamera;
            set => showUIInCamera = value;
        }

        public bool Lock
        {
            get => lockCamera;
            set => lockCamera = value;
        }

        public bool LocalPlayer
        {
            get => localPlayer;
            set => localPlayer = value;
        }

        public bool RemotePlayer
        {
            get => remotePlayer;
            set => remotePlayer = value;
        }

        public bool Environment
        {
            get => environment;
            set => environment = value;
        }

        public bool GreenScreen
        {
            get => greenScreen;
            set => greenScreen = value;
        }

        public bool SmoothMovement
        {
            get => smoothMovement;
            set => smoothMovement = value;
        }

        public bool LookAtMe
        {
            get => lookAtMe;
            set => lookAtMe = value;
        }

        public bool AutoLevelRoll
        {
            get => autoLevelRoll;
            set => autoLevelRoll = value;
        }

        public bool AutoLevelPitch
        {
            get => autoLevelPitch;
            set => autoLevelPitch = value;
        }

        public bool Flying
        {
            get => flying;
            set => flying = value;
        }

        public bool TriggerTakesPhotos
        {
            get => triggerTakesPhotos;
            set => triggerTakesPhotos = value;
        }

        public bool DollyPathsStayVisible
        {
            get => dollyPathsStayVisible;
            set => dollyPathsStayVisible = value;
        }

        public bool CameraEars
        {
            get => cameraEars;
            set => cameraEars = value;
        }

        public bool ShowFocus
        {
            get => showFocus;
            set => showFocus = value;
        }

        public bool Streaming
        {
            get => streaming;
            set => streaming = value;
        }

        public bool RollWhileFlying
        {
            get => rollWhileFlying;
            set => rollWhileFlying = value;
        }

        public bool OrientationIsLandscape
        {
            get => orientation == Orientation.Landscape;
            set => orientation = value ? Orientation.Landscape : Orientation.Portrait;
        }

        // API: Sliders
        public Zoom Zoom
        {
            get => new(zoom);
            set => zoom = value;
        }

        public Exposure Exposure
        {
            get => new(exposure);
            set => exposure = value;
        }

        public FocalDistance FocalDistance
        {
            get => new(focalDistance);
            set => focalDistance = value;
        }

        public Aperture Aperture
        {
            get => new(aperture);
            set => aperture = value;
        }

        public Hue Hue
        {
            get => new(hue);
            set => hue = value;
        }

        public Saturation Saturation
        {
            get => new(saturation);
            set => saturation = value;
        }

        public Lightness Lightness
        {
            get => new(lightness);
            set => lightness = value;
        }

        public LookAtMeXOffset LookAtMeXOffset
        {
            get => new(lookAtMeXOffset);
            set => lookAtMeXOffset = value;
        }

        public LookAtMeYOffset LookAtMeYOffset
        {
            get => new(lookAtMeYOffset);
            set => lookAtMeYOffset = value;
        }

        public FlySpeed FlySpeed
        {
            get => new(flySpeed);
            set => flySpeed = value;
        }

        public TurnSpeed TurnSpeed
        {
            get => new(turnSpeed);
            set => turnSpeed = value;
        }

        public SmoothingStrength SmoothingStrength
        {
            get => new(smoothingStrength);
            set => smoothingStrength = value;
        }

        public PhotoRate PhotoRate
        {
            get => new(photoRate);
            set => photoRate = value;
        }

        public Duration Duration
        {
            get => new(duration);
            set => duration = value;
        }

        #endregion

        private void OnEnable()
        {
            try
            {
                _vrcCamera = new VRCCamera();

                SyncUnityCamera();
                ApplyCameraSettings();

                SyncPose();
                _vrcCamera.Pose.SetValue(Pose);

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
        }

        private void FixedUpdate()
        {
            SyncUnityCamera();
            SyncPose();

            ApplyCameraSettings();
        }

        private void ApplyCameraSettings()
        {
            _vrcCamera?.Pose.SetValue(Pose);
            _vrcCamera?.Mode.SetValue(Mode);

            _vrcCamera?.Zoom.SetValue(Zoom);
            _vrcCamera?.FocalDistance.SetValue(FocalDistance);
            _vrcCamera?.Aperture.SetValue(Aperture);
            _vrcCamera?.Exposure.SetValue(Exposure);
            _vrcCamera?.Hue.SetValue(Hue);
            _vrcCamera?.Saturation.SetValue(Saturation);
            _vrcCamera?.Lightness.SetValue(Lightness);
            _vrcCamera?.LookAtMeOffset.SetValue(new LookAtMeOffset(LookAtMeXOffset, LookAtMeYOffset));
            _vrcCamera?.FlySpeed.SetValue(FlySpeed);
            _vrcCamera?.TurnSpeed.SetValue(TurnSpeed);
            _vrcCamera?.SmoothingStrength.SetValue(SmoothingStrength);
            _vrcCamera?.PhotoRate.SetValue(PhotoRate);
            _vrcCamera?.Duration.SetValue(Duration);
            _vrcCamera?.ShowUIInCamera.SetValue(ShowUIInCamera);
            _vrcCamera?.Lock.SetValue(Lock);
            _vrcCamera?.LocalPlayer.SetValue(LocalPlayer);
            _vrcCamera?.RemotePlayer.SetValue(RemotePlayer);
            _vrcCamera?.Environment.SetValue(Environment);
            _vrcCamera?.GreenScreen.SetValue(GreenScreen);
            _vrcCamera?.SmoothMovement.SetValue(SmoothMovement);
            _vrcCamera?.LookAtMe.SetValue(LookAtMe);
            _vrcCamera?.AutoLevelPitch.SetValue(AutoLevelPitch);
            _vrcCamera?.AutoLevelRoll.SetValue(AutoLevelRoll);
            _vrcCamera?.Flying.SetValue(Flying);
            _vrcCamera?.TriggerTakesPhotos.SetValue(TriggerTakesPhotos);
            _vrcCamera?.DollyPathsStayVisible.SetValue(DollyPathsStayVisible);
            _vrcCamera?.CameraEars.SetValue(CameraEars);
            _vrcCamera?.ShowFocus.SetValue(ShowFocus);
            _vrcCamera?.Streaming.SetValue(Streaming);
            _vrcCamera?.RollWhileFlying.SetValue(RollWhileFlying);
            _vrcCamera?.Orientation.SetValue(OrientationIsLandscape ? Orientation.Landscape : Orientation.Portrait);
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

            Pose = new Pose(poseSource.position, poseSource.rotation);
        }
    }
}