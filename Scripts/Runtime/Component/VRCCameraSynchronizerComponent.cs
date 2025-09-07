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

        private VRCCameraSynchronizer _synchronizer;
        private VRCCamera _vrcCamera;

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
                transmitter = new OSCJackTransmitter(destination, port);
                _synchronizer = new VRCCameraSynchronizer(transmitter, _vrcCamera);
                
                // Send all initial values on play mode start
                if (Application.isPlaying)
                {
                    SendInitialValues();
                }
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
        }

        private void FixedUpdate()
        {
            // NOTE: Use FixedUpdate to reduce the frequency of updates
            if (_vrcCamera == null) return;

            // Update camera-tracked values (Zoom, FocalDistance, Aperture)
            _vrcCamera.UpdateFromCamera();

            // Update other parameters via setter methods
            _vrcCamera.SetExposure(exposure);
            _vrcCamera.SetHue(hue);
            _vrcCamera.SetSaturation(saturation);
            _vrcCamera.SetLightness(lightness);
            _vrcCamera.SetLookAtMeOffset(lookAtMeXOffset, lookAtMeYOffset);
            _vrcCamera.SetFlySpeed(flySpeed);
            _vrcCamera.SetTurnSpeed(turnSpeed);
            _vrcCamera.SetSmoothingStrength(smoothingStrength);
            _vrcCamera.SetPhotoRate(photoRate);
            _vrcCamera.SetDuration(duration);
        }
        
        private void SendInitialValues()
        {
            if (_vrcCamera == null || _synchronizer == null) return;
            
            // Update camera-tracked values first
            _vrcCamera.UpdateFromCamera();
            
            // Set all parameter values
            _vrcCamera.SetExposure(exposure);
            _vrcCamera.SetHue(hue);
            _vrcCamera.SetSaturation(saturation);
            _vrcCamera.SetLightness(lightness);
            _vrcCamera.SetLookAtMeOffset(lookAtMeXOffset, lookAtMeYOffset);
            _vrcCamera.SetFlySpeed(flySpeed);
            _vrcCamera.SetTurnSpeed(turnSpeed);
            _vrcCamera.SetSmoothingStrength(smoothingStrength);
            _vrcCamera.SetPhotoRate(photoRate);
            _vrcCamera.SetDuration(duration);
            
            // Force send all values using Sync
            _synchronizer.Sync();
        }
    }
}