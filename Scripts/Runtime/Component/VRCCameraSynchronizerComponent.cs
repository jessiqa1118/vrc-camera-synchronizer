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
                transmitter = new OSCJackTransmitter(destination, port);
                _synchronizer = new VRCCameraSynchronizer(transmitter, cameraComponent);
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
            _synchronizer?.Sync();
        }
    }
}