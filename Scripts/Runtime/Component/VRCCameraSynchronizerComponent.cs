using System;
using UnityEngine;

namespace JessiQa
{
    [AddComponentMenu("JessiQa/VRC Camera Synchronizer")]
    [RequireComponent(typeof(Camera))]
    public class VRCCameraSynchronizerComponent : MonoBehaviour
    {
        [Header("OSC Settings")] [SerializeField]
        private string destination = "127.0.0.1";

        [SerializeField] private int port = 9000;

        [Header("Camera Parameters")]
        [SerializeField]
        [Range(-10f, 4f)]
        [Tooltip("Camera exposure value (-10 to 4, default: 0)")]
        private float exposure = Exposure.DefaultValue;

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
            }

            _synchronizer?.Sync();
        }
    }
}