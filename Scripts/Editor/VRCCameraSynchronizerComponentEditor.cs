using UnityEditor;
using UnityEngine;
using Parameters;

namespace VRCCamera.Editor
{
    [CustomEditor(typeof(VRCCameraSynchronizerComponent))]
    public class VRCCameraSynchronizerComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty _destination;
        private SerializedProperty _port;
        private SerializedProperty _exposure;
        private SerializedProperty _hue;
        private SerializedProperty _saturation;
        private SerializedProperty _lightness;
        private SerializedProperty _lookAtMeXOffset;
        private SerializedProperty _lookAtMeYOffset;
        private SerializedProperty _flySpeed;
        private SerializedProperty _turnSpeed;
        private SerializedProperty _smoothingStrength;
        private SerializedProperty _photoRate;
        private SerializedProperty _duration;
        private SerializedProperty _showUIInCamera;

        private void OnEnable()
        {
            _destination = serializedObject.FindProperty("destination");
            _port = serializedObject.FindProperty("port");
            _exposure = serializedObject.FindProperty("exposure");
            _hue = serializedObject.FindProperty("hue");
            _saturation = serializedObject.FindProperty("saturation");
            _lightness = serializedObject.FindProperty("lightness");
            _lookAtMeXOffset = serializedObject.FindProperty("lookAtMeXOffset");
            _lookAtMeYOffset = serializedObject.FindProperty("lookAtMeYOffset");
            _flySpeed = serializedObject.FindProperty("flySpeed");
            _turnSpeed = serializedObject.FindProperty("turnSpeed");
            _smoothingStrength = serializedObject.FindProperty("smoothingStrength");
            _photoRate = serializedObject.FindProperty("photoRate");
            _duration = serializedObject.FindProperty("duration");
            _showUIInCamera = serializedObject.FindProperty("showUIInCamera");
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
            
            // Toggle Parameters
            EditorGUILayout.LabelField("Toggles", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_showUIInCamera, new GUIContent("Show UI In Camera"));
            
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
            
            // Use sliders for X/Y offset
            EditorGUILayout.Slider(_lookAtMeXOffset, LookAtMeXOffset.MinValue, LookAtMeXOffset.MaxValue, "X Offset");
            EditorGUILayout.Slider(_lookAtMeYOffset, LookAtMeYOffset.MinValue, LookAtMeYOffset.MaxValue, "Y Offset");
            
            EditorGUILayout.Space();
            
            // Movement Parameters
            EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
            EditorGUILayout.Slider(_flySpeed, FlySpeed.MinValue, FlySpeed.MaxValue, "Fly Speed");
            EditorGUILayout.Slider(_turnSpeed, TurnSpeed.MinValue, TurnSpeed.MaxValue, "Turn Speed");
            EditorGUILayout.Slider(_smoothingStrength, SmoothingStrength.MinValue, SmoothingStrength.MaxValue, "Smoothing Strength");
            
            EditorGUILayout.Space();
            
            // Photo Settings
            EditorGUILayout.LabelField("Photo", EditorStyles.boldLabel);
            EditorGUILayout.Slider(_photoRate, PhotoRate.MinValue, PhotoRate.MaxValue, "Photo Rate");
            EditorGUILayout.Slider(_duration, Duration.MinValue, Duration.MaxValue, "Duration");
            
            EditorGUILayout.Space();
            
            // Camera Sync (Read-only)
            EditorGUILayout.LabelField("Camera Sync (From Camera Component)", EditorStyles.boldLabel);
            
            // Get current camera values
            var component = (VRCCameraSynchronizerComponent)target;
            var camera = component.GetComponent<Camera>();
            
            if (camera != null)
            {
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