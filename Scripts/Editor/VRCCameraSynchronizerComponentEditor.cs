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
        private SerializedProperty _aperture;
        private SerializedProperty _hue;
        private SerializedProperty _saturation;
        private SerializedProperty _lightness;
        private SerializedProperty _lookAtMeXOffset;
        private SerializedProperty _lookAtMeYOffset;
        private SerializedProperty _focalDistance;
        private SerializedProperty _flySpeed;

        private void OnEnable()
        {
            _destination = serializedObject.FindProperty("destination");
            _port = serializedObject.FindProperty("port");
            _exposure = serializedObject.FindProperty("exposure");
            _aperture = serializedObject.FindProperty("aperture");
            _hue = serializedObject.FindProperty("hue");
            _saturation = serializedObject.FindProperty("saturation");
            _lightness = serializedObject.FindProperty("lightness");
            _lookAtMeXOffset = serializedObject.FindProperty("lookAtMeXOffset");
            _lookAtMeYOffset = serializedObject.FindProperty("lookAtMeYOffset");
            _focalDistance = serializedObject.FindProperty("focalDistance");
            _flySpeed = serializedObject.FindProperty("flySpeed");
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
            
            // GreenScreen Parameters
            EditorGUILayout.LabelField("GreenScreen", EditorStyles.boldLabel);
            
            // Color picker only
            // Map Lightness to V for display
            Color currentColor = Color.HSVToRGB(
                _hue.floatValue / Hue.MaxValue, 
                _saturation.floatValue / Saturation.MaxValue,
                _lightness.floatValue / Lightness.MaxValue
            );
            
            EditorGUI.BeginChangeCheck();
            Color newColor = EditorGUILayout.ColorField("GreenScreen Color", currentColor);
            if (EditorGUI.EndChangeCheck())
            {
                Color.RGBToHSV(newColor, out float h, out float s, out float v);
                _hue.floatValue = h * Hue.MaxValue;
                _saturation.floatValue = s * Saturation.MaxValue;
                _lightness.floatValue = v * Lightness.MaxValue;
            }
            
            EditorGUILayout.Space();
            
            // LookAtMe Parameters
            EditorGUILayout.LabelField("LookAtMe", EditorStyles.boldLabel);
            
            // Use Vector2Field for X/Y offset
            Vector2 lookAtMeOffset = new Vector2(_lookAtMeXOffset.floatValue, _lookAtMeYOffset.floatValue);
            EditorGUI.BeginChangeCheck();
            lookAtMeOffset = EditorGUILayout.Vector2Field("Offset", lookAtMeOffset);
            if (EditorGUI.EndChangeCheck())
            {
                _lookAtMeXOffset.floatValue = Mathf.Clamp(lookAtMeOffset.x, LookAtMeXOffset.MinValue, LookAtMeXOffset.MaxValue);
                _lookAtMeYOffset.floatValue = Mathf.Clamp(lookAtMeOffset.y, LookAtMeYOffset.MinValue, LookAtMeYOffset.MaxValue);
            }
            
            EditorGUILayout.Space();
            
            // FlySpeed
            EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
            EditorGUILayout.Slider(_flySpeed, FlySpeed.MinValue, FlySpeed.MaxValue, "Fly Speed");
            
            EditorGUILayout.Space();
            
            // Camera Sync (Read-only)
            EditorGUILayout.LabelField("Camera Sync (From Camera Component)", EditorStyles.boldLabel);
            
            // Get current camera values
            var component = (VRCCameraSynchronizerComponent)target;
            var camera = component.GetComponent<Camera>();
            
            if (camera != null)
            {
                // Update the properties with current camera values (clamped)
                _aperture.floatValue = Mathf.Clamp(camera.aperture, Aperture.MinValue, Aperture.MaxValue);
                _focalDistance.floatValue = Mathf.Clamp(camera.focusDistance, FocalDistance.MinValue, FocalDistance.MaxValue);
                
                GUI.enabled = false; // Make read-only
                
                // Zoom (focal length) - clamp the display value
                float clampedZoom = Mathf.Clamp(camera.focalLength, Zoom.MinValue, Zoom.MaxValue);
                EditorGUILayout.Slider("Zoom (Focal Length)", clampedZoom, Zoom.MinValue, Zoom.MaxValue);
                
                // Aperture - clamp the display value
                float clampedAperture = Mathf.Clamp(camera.aperture, Aperture.MinValue, Aperture.MaxValue);
                EditorGUILayout.Slider("Aperture", clampedAperture, Aperture.MinValue, Aperture.MaxValue);
                
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