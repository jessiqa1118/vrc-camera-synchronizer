using System;
using UnityEngine;

namespace JessiQa
{
    [AddComponentMenu("JessiQa/VRC Camera Synchronizer")]
    [RequireComponent(typeof(Camera))]
    public class VRCCameraSynchronizerComponent : MonoBehaviour
    {
        [SerializeField] private string destination = "127.0.0.1";
        [SerializeField] private int port = 9000;
        [SerializeField] private float exposure = Exposure.DefaultValue;
        [SerializeField] private float aperture = Aperture.DefaultValue;
        [SerializeField] private float hue = Hue.DefaultValue;
        [SerializeField] private float saturation = Saturation.DefaultValue;
        [SerializeField] private float lightness = Lightness.DefaultValue;
        [SerializeField] private float focalDistance = FocalDistance.DefaultValue;

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
            if (_vrcCamera != null)
            {
                _vrcCamera.Exposure = new Exposure(exposure);
                _vrcCamera.Hue = new Hue(hue);
                _vrcCamera.Saturation = new Saturation(saturation);
                _vrcCamera.Lightness = new Lightness(lightness);
                // Aperture and FocalDistance are now automatically synced from Camera component
                
                // Update display values for Inspector
                var camera = GetComponent<Camera>();
                if (camera != null)
                {
                    aperture = camera.aperture;
                    focalDistance = camera.focusDistance;
                }
            }

            _synchronizer?.Sync();
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            // In Editor, update display values when component changes
            var camera = GetComponent<Camera>();
            if (camera != null)
            {
                aperture = camera.aperture;
                focalDistance = camera.focusDistance;
            }
        }
#endif
    }
}