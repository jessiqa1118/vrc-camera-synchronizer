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
        private SerializedProperty _lockCamera;
        private SerializedProperty _localPlayer;
        private SerializedProperty _remotePlayer;
        private SerializedProperty _environment;
        private SerializedProperty _greenScreen;
        private SerializedProperty _items;
        private SerializedProperty _smoothMovement;
        private SerializedProperty _lookAtMe;
        private SerializedProperty _autoLevelRoll;
        private SerializedProperty _autoLevelPitch;
        private SerializedProperty _flying;
        private SerializedProperty _triggerTakesPhotos;
        private SerializedProperty _dollyPathsStayVisible;
        private SerializedProperty _cameraEars;
        private SerializedProperty _showFocus;
        private SerializedProperty _streaming;
        private SerializedProperty _rollWhileFlying;
        private SerializedProperty _orientation;

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
            _lockCamera = serializedObject.FindProperty("lockCamera");
            _localPlayer = serializedObject.FindProperty("localPlayer");
            _remotePlayer = serializedObject.FindProperty("remotePlayer");
            _environment = serializedObject.FindProperty("environment");
            _greenScreen = serializedObject.FindProperty("greenScreen");
            _items = serializedObject.FindProperty("items");
            _smoothMovement = serializedObject.FindProperty("smoothMovement");
            _lookAtMe = serializedObject.FindProperty("lookAtMe");
            _autoLevelRoll = serializedObject.FindProperty("autoLevelRoll");
            _autoLevelPitch = serializedObject.FindProperty("autoLevelPitch");
            _flying = serializedObject.FindProperty("flying");
            _triggerTakesPhotos = serializedObject.FindProperty("triggerTakesPhotos");
            _dollyPathsStayVisible = serializedObject.FindProperty("dollyPathsStayVisible");
            _cameraEars = serializedObject.FindProperty("cameraEars");
            _showFocus = serializedObject.FindProperty("showFocus");
            _streaming = serializedObject.FindProperty("streaming");
            _rollWhileFlying = serializedObject.FindProperty("rollWhileFlying");
            _orientation = serializedObject.FindProperty("orientation");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // VRChat Host
            EditorGUILayout.LabelField("VRChat Host", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_destination, new GUIContent("IP Address"));
                EditorGUILayout.PropertyField(_port);
            }

            EditorGUILayout.Space();

            // Actions (Play Mode only)
            EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                if (GUILayout.Button("Close Camera"))
                {
                    serializedObject.ApplyModifiedProperties();
                    var targetComponent = (VRCCameraSynchronizerComponent)target;
                    targetComponent.Action_CloseCamera();
                }

                if (GUILayout.Button("Take Photo"))
                {
                    serializedObject.ApplyModifiedProperties();
                    var targetComponent = (VRCCameraSynchronizerComponent)target;
                    targetComponent.Action_Capture();
                }

                if (GUILayout.Button("Timed (5s)"))
                {
                    serializedObject.ApplyModifiedProperties();
                    var targetComponent = (VRCCameraSynchronizerComponent)target;
                    targetComponent.Action_CaptureDelayed();
                }
            }

            EditorGUILayout.Space();

            // General (under Actions)
            EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_orientation, new GUIContent("Orientation"));
                EditorGUILayout.Slider(_exposure, Exposure.MinValue, Exposure.MaxValue, "Exposure");
                // Display camera-driven parameters as read-only
                var pComponent = (VRCCameraSynchronizerComponent)target;
                var pCamera = pComponent.GetComponent<Camera>();
                if (pCamera != null)
                {
                    EditorGUI.BeginDisabledGroup(true);
                    var pZoom = Mathf.Clamp(pCamera.focalLength, Zoom.MinValue, Zoom.MaxValue);
                    EditorGUILayout.Slider("Zoom (Focal Length)", pZoom, Zoom.MinValue, Zoom.MaxValue);
                    EditorGUI.EndDisabledGroup();
                }
            }

            EditorGUILayout.Space();

            // Toggles section removed (moved to Others at bottom)
            EditorGUILayout.Space();

            // Stream (before Mask)
            EditorGUILayout.LabelField("Stream", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_streaming, new GUIContent("Spout Stream"));
                using (new EditorGUI.DisabledScope(true))
                {
                    // Draw as a normal PropertyField to match alignment with Spout Stream
                    EditorGUILayout.PropertyField(_cameraEars, new GUIContent("Audio From Camera"));
                    // Compute field area (right of the label) and place suffix right after the checkbox
                    var last = GUILayoutUtility.GetLastRect();
                    var labelW = EditorGUIUtility.labelWidth;
                    var fieldW = last.width - labelW;
                    var fieldX = last.x + labelW;
                    var toggleW = EditorGUIUtility.singleLineHeight; // checkbox approx square
                    var gap = 0f; // no gap between checkbox and suffix
                    var suffixRect = new Rect(fieldX + toggleW + gap, last.y, fieldW - toggleW - gap, last.height);
                    EditorGUI.LabelField(suffixRect, "(not supported)", EditorStyles.miniLabel);
                }
            }

            EditorGUILayout.Space();

            // Flying (before Behaviour)
            EditorGUILayout.LabelField("Flying", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_flying, new GUIContent("Enabled"));
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUILayout.Slider(_flySpeed, FlySpeed.MinValue, FlySpeed.MaxValue, "Fly Speed");
                    EditorGUILayout.Slider(_turnSpeed, TurnSpeed.MinValue, TurnSpeed.MaxValue, "Turn Speed");
                    EditorGUILayout.PropertyField(_rollWhileFlying, new GUIContent("Roll While Flying"));
                }
            }

            EditorGUILayout.Space();

            // Behaviour (before Mask)
            EditorGUILayout.LabelField("Behaviour", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_smoothMovement, new GUIContent("Smoothed"));
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUILayout.Slider(_smoothingStrength, SmoothingStrength.MinValue, SmoothingStrength.MaxValue,
                        "Smoothing Strength");
                }

                EditorGUILayout.PropertyField(_lookAtMe, new GUIContent("Look-At-Me"));
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUILayout.Slider(_lookAtMeXOffset, LookAtMeXOffset.MinValue, LookAtMeXOffset.MaxValue,
                        "Horizontal Offset");
                    EditorGUILayout.Slider(_lookAtMeYOffset, LookAtMeYOffset.MinValue, LookAtMeYOffset.MaxValue,
                        "Vertical Offset");
                }

                EditorGUILayout.PropertyField(_autoLevelPitch, new GUIContent("Auto Level Pitch"));
            }

            EditorGUILayout.Space();
            // Focus
            EditorGUILayout.LabelField("Focus", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                var pComponent = (VRCCameraSynchronizerComponent)target;
                var pCamera = pComponent.GetComponent<Camera>();
                if (pCamera)
                {
#if UNITY_2021_2_OR_NEWER
                    EditorGUI.BeginDisabledGroup(true);
                    var fFocal = Mathf.Clamp(pCamera.focusDistance, FocalDistance.MinValue, FocalDistance.MaxValue);
                    EditorGUILayout.Slider("Focal Distance", fFocal, FocalDistance.MinValue, FocalDistance.MaxValue);
                    var fAperture = Mathf.Clamp(pCamera.aperture, Aperture.MinValue, Aperture.MaxValue);
                    EditorGUILayout.Slider("Aperture", fAperture, Aperture.MinValue, Aperture.MaxValue);
                    EditorGUI.EndDisabledGroup();
#else
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.LabelField("Focal Distance", "(not supported on this Unity version)");
                        EditorGUILayout.LabelField("Aperture", "(not supported on this Unity version)");
                    }
#endif
                }

                EditorGUILayout.PropertyField(_showFocus, new GUIContent("Show"));
            }

            EditorGUILayout.Space();

            // Mask Parameters
            EditorGUILayout.LabelField("Mask", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_localPlayer, new GUIContent("Local User"));
                EditorGUILayout.PropertyField(_remotePlayer, new GUIContent("Remote User"));
                EditorGUILayout.PropertyField(_environment, new GUIContent("World"));
                EditorGUILayout.PropertyField(_greenScreen, new GUIContent("Green Screen"));
                using (new EditorGUI.IndentLevelScope())
                {
                    // Color picker for Green Screen (Hue/Saturation/Lightness)
                    var currentMaskColor = Color.HSVToRGB(
                        _hue.floatValue / Hue.MaxValue,
                        _saturation.floatValue / Saturation.MaxValue,
                        _lightness.floatValue / Lightness.MaxValue
                    );
                    EditorGUI.BeginChangeCheck();
                    var newMaskColor = EditorGUILayout.ColorField("Color", currentMaskColor);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Color.RGBToHSV(newMaskColor, out float h, out float s, out float v);
                        _hue.floatValue = h * Hue.MaxValue;
                        _saturation.floatValue = s * Saturation.MaxValue;
                        _lightness.floatValue = v * Lightness.MaxValue;
                    }
                }

                EditorGUILayout.PropertyField(_showUIInCamera, new GUIContent("UI"));
                using (new EditorGUI.DisabledScope(true))
                {
                    // Draw like a normal PropertyField to match alignment, then append suffix
                    EditorGUILayout.PropertyField(_items, new GUIContent("Items"));
                    var last = GUILayoutUtility.GetLastRect();
                    var labelW = EditorGUIUtility.labelWidth;
                    var fieldW = last.width - labelW;
                    var fieldX = last.x + labelW;
                    var toggleW = EditorGUIUtility.singleLineHeight;
                    var gap = 0f;
                    var suffixRect = new Rect(fieldX + toggleW + gap, last.y, fieldW - toggleW - gap, last.height);
                    EditorGUI.LabelField(suffixRect, "(not supported)", EditorStyles.miniLabel);
                }
            }

            EditorGUILayout.Space();

            EditorGUILayout.Space();

            // Dolly (after Mask)
            EditorGUILayout.LabelField("Dolly", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.Slider(_duration, Duration.MinValue, Duration.MaxValue, "Duration");
                EditorGUILayout.Slider(_photoRate, PhotoRate.MinValue, PhotoRate.MaxValue, "Photo Rate");
            }

            EditorGUILayout.Space();

            // Others (migrated from Toggles)
            EditorGUILayout.LabelField("Others", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_lockCamera, new GUIContent("Lock"));
                EditorGUILayout.PropertyField(_autoLevelRoll, new GUIContent("Auto Level Roll"));
                EditorGUILayout.PropertyField(_triggerTakesPhotos, new GUIContent("Trigger Takes Photos"));
                EditorGUILayout.PropertyField(_dollyPathsStayVisible, new GUIContent("Dolly Paths Stay Visible"));
            }

            EditorGUILayout.Space();

            // Movement Parameters (merged into Flying/Behaviour)

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }
    }
}