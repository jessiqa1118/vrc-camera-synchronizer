using UnityEditor;
using UnityEngine;

namespace JessiQa.Editor
{
    [CustomEditor(typeof(VRCCameraSynchronizerComponent))]
    public class VRCCameraSynchronizerComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty _destination;
        private SerializedProperty _port;
        private SerializedProperty _exposure;
        private SerializedProperty _focalDistance;

        private void OnEnable()
        {
            _destination = serializedObject.FindProperty("destination");
            _port = serializedObject.FindProperty("port");
            _exposure = serializedObject.FindProperty("exposure");
            _focalDistance = serializedObject.FindProperty("focalDistance");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // OSC Settings
            EditorGUILayout.PropertyField(_destination);
            EditorGUILayout.PropertyField(_port);
            
            EditorGUILayout.Space();

            // Camera Parameters
            EditorGUILayout.Slider(_exposure, Exposure.MinValue, Exposure.MaxValue, "Exposure");
            
            EditorGUILayout.Space();
            
            // Camera Sync (Read-only)
            EditorGUILayout.LabelField("Camera Sync (From Camera Component)", EditorStyles.boldLabel);
            
            // Get current camera values
            var component = (VRCCameraSynchronizerComponent)target;
            var camera = component.GetComponent<Camera>();
            
            if (camera != null)
            {
                EditorGUILayout.HelpBox(
                    "These values are synced from the Camera component. " +
                    "Modify Camera.focalLength and Camera.focusDistance to change them.",
                    MessageType.Info);
                
                // Update the focal distance property with current camera value (clamped)
                _focalDistance.floatValue = Mathf.Clamp(camera.focusDistance, FocalDistance.MinValue, FocalDistance.MaxValue);
                
                GUI.enabled = false; // Make read-only
                
                // Zoom (focal length) - clamp the display value
                float clampedZoom = Mathf.Clamp(camera.focalLength, Zoom.MinValue, Zoom.MaxValue);
                EditorGUILayout.Slider("Zoom (Focal Length)", clampedZoom, Zoom.MinValue, Zoom.MaxValue);
                
                // Focal Distance - clamp the display value
                float clampedFocalDistance = Mathf.Clamp(camera.focusDistance, FocalDistance.MinValue, FocalDistance.MaxValue);
                EditorGUILayout.Slider("Focal Distance", clampedFocalDistance, FocalDistance.MinValue, FocalDistance.MaxValue);
                
                GUI.enabled = true;
            }
            else
            {
                EditorGUILayout.HelpBox("Camera component not found!", MessageType.Warning);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}