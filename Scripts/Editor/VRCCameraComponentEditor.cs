using UnityEditor;
using UnityEngine;

namespace Astearium.VRChat.Camera.Editor
{
    [CustomEditor(typeof(VRCCameraComponent))]
    public class VRCCameraSynchronizerComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty _destination;
        private SerializedProperty _port;
        private SerializedProperty _portValue;
        private int _lastValidPort;

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
        private SerializedProperty _mode;
        private SerializedProperty _poseTransform;
        private SerializedProperty _syncPoseFromTransform;
        private SerializedProperty _posePosition;
        private SerializedProperty _poseEuler;
        private SerializedProperty _syncUnityCamera;
        private SerializedProperty _unityCameraOverride;
        private SerializedProperty _zoomValue;
        private SerializedProperty _focalDistanceValue;
        private SerializedProperty _apertureValue;

        private void OnEnable()
        {
            serializedObject.Update();

            _destination = serializedObject.FindProperty("destination");
            _port = serializedObject.FindProperty("port");
            _portValue = _port?.FindPropertyRelative("value");

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
            _mode = serializedObject.FindProperty("mode");
            _poseTransform = serializedObject.FindProperty("poseSource");
            _syncPoseFromTransform = serializedObject.FindProperty("syncPoseFromTransform");
            _posePosition = serializedObject.FindProperty("posePosition");
            _poseEuler = serializedObject.FindProperty("poseEuler");
            _syncUnityCamera = serializedObject.FindProperty("syncUnityCamera");
            _unityCameraOverride = serializedObject.FindProperty("unityCamera");
            _zoomValue = serializedObject.FindProperty("zoom");
            _focalDistanceValue = serializedObject.FindProperty("focalDistance");
            _apertureValue = serializedObject.FindProperty("aperture");

            InitializePortDefaults();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // VRChat Host
            EditorGUILayout.LabelField("VRChat Host", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_destination, new GUIContent("IP Address"));
                DrawPortField();
            }

            EditorGUILayout.Space();

            if (_syncUnityCamera != null)
            {
                EditorGUILayout.LabelField("Unity Link", EditorStyles.boldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUILayout.PropertyField(_syncUnityCamera, new GUIContent("Sync Unity Camera"));
                    EditorGUILayout.PropertyField(_unityCameraOverride, new GUIContent("Source"));
                }

                EditorGUILayout.Space();
            }

            // Pose (manual input or follow object)
            EditorGUILayout.LabelField("Pose", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_syncPoseFromTransform, new GUIContent("Follow Object"));

                EditorGUILayout.PropertyField(_poseTransform, new GUIContent("Source"));

                if (_syncPoseFromTransform.boolValue)
                {
                    var src = _poseTransform.objectReferenceValue as Transform;
                    if (src != null)
                    {
                        _posePosition.vector3Value = src.position;
                        _poseEuler.vector3Value = src.rotation.eulerAngles;
                    }
                }

                using (new EditorGUI.DisabledScope(_syncPoseFromTransform.boolValue))
                {
                    EditorGUILayout.PropertyField(_posePosition, new GUIContent("Position"));
                    EditorGUILayout.PropertyField(_poseEuler, new GUIContent("Rotation (Euler, degrees)"));
                }
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
                    var targetComponent = (VRCCameraComponent)target;
                    targetComponent.Action_CloseCamera();
                }

                if (GUILayout.Button("Take Photo"))
                {
                    serializedObject.ApplyModifiedProperties();
                    var targetComponent = (VRCCameraComponent)target;
                    targetComponent.Action_Capture();
                }

                if (GUILayout.Button("Timed (5s)"))
                {
                    serializedObject.ApplyModifiedProperties();
                    var targetComponent = (VRCCameraComponent)target;
                    targetComponent.Action_CaptureDelayed();
                }
            }

            EditorGUILayout.Space();

            // General (under Actions)
            EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                var modeOrder = new Mode[]
                {
                    Mode.Off,
                    Mode.Photo,
                    Mode.Multilayer,
                    Mode.Stream,
                    Mode.Print,
                    Mode.Emoji,
                    Mode.Drone
                };

                var modeLabels = new[] { "Off", "Local Photo", "Multi Layer", "Stream", "Print", "Emoji", "Drone" };
                var currentModeValue = _mode.intValue;
                var currentIndex = System.Array.IndexOf(modeOrder, (Mode)currentModeValue);
                if (currentIndex < 0)
                {
                    currentIndex = 0;
                }

                EditorGUI.BeginChangeCheck();
                var newIndex = EditorGUILayout.Popup("Camera Mode", currentIndex, modeLabels);
                if (EditorGUI.EndChangeCheck())
                {
                    _mode.intValue = (int)modeOrder[newIndex];
                }

                EditorGUILayout.PropertyField(_orientation, new GUIContent("Orientation"));
                EditorGUILayout.Slider(_exposure, Exposure.MinValue, Exposure.MaxValue, "Exposure");

                var targetComponent = (VRCCameraComponent)target;
                var sourceCamera = (_unityCameraOverride != null ? _unityCameraOverride.objectReferenceValue : null) as UnityEngine.Camera;
                if (sourceCamera == null)
                {
                    sourceCamera = targetComponent != null ? targetComponent.GetComponent<UnityEngine.Camera>() : null;
                }

                var isSyncing = _syncUnityCamera != null && _syncUnityCamera.boolValue;
                if (isSyncing && sourceCamera != null)
                {
                    var syncedZoom = Mathf.Clamp(sourceCamera.focalLength, Zoom.MinValue, Zoom.MaxValue);
                    if (!Mathf.Approximately(_zoomValue.floatValue, syncedZoom))
                    {
                        _zoomValue.floatValue = syncedZoom;
                    }

                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.Slider("Zoom (Focal Length)", syncedZoom, Zoom.MinValue, Zoom.MaxValue);
                    }
                }
                else
                {
                    EditorGUILayout.Slider(_zoomValue, Zoom.MinValue, Zoom.MaxValue, "Zoom (Focal Length)");
                }
            }

            EditorGUILayout.Space();

            // Stream (before Mask)
            EditorGUILayout.LabelField("Stream", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_streaming, new GUIContent("Spout Stream"));

                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(_cameraEars, new GUIContent("Audio From Camera"));

                    var last = GUILayoutUtility.GetLastRect();
                    var labelW = EditorGUIUtility.labelWidth;
                    var fieldW = last.width - labelW;
                    var fieldX = last.x + labelW;
                    var toggleW = EditorGUIUtility.singleLineHeight;
                    var suffixRect = new Rect(fieldX + toggleW, last.y, fieldW - toggleW, last.height);
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
                    EditorGUILayout.Slider(_smoothingStrength, SmoothingStrength.MinValue, SmoothingStrength.MaxValue, "Smoothing Strength");
                }

                EditorGUILayout.PropertyField(_lookAtMe, new GUIContent("Look-At-Me"));
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUILayout.Slider(_lookAtMeXOffset, LookAtMeXOffset.MinValue, LookAtMeXOffset.MaxValue, "Horizontal Offset");
                    EditorGUILayout.Slider(_lookAtMeYOffset, LookAtMeYOffset.MinValue, LookAtMeYOffset.MaxValue, "Vertical Offset");
                }

                EditorGUILayout.PropertyField(_autoLevelPitch, new GUIContent("Auto Level Pitch"));
                EditorGUILayout.PropertyField(_autoLevelRoll, new GUIContent("Auto Level Roll"));
            }

            EditorGUILayout.Space();

            // Focus
            EditorGUILayout.LabelField("Focus", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                var targetComponent = (VRCCameraComponent)target;
                var sourceCamera = (_unityCameraOverride != null ? _unityCameraOverride.objectReferenceValue : null) as UnityEngine.Camera;
                if (sourceCamera == null && targetComponent != null)
                {
                    sourceCamera = targetComponent.GetComponent<UnityEngine.Camera>();
                }

#if UNITY_2021_2_OR_NEWER
                var isSyncing = _syncUnityCamera != null && _syncUnityCamera.boolValue;
                if (isSyncing && sourceCamera != null)
                {
                    var focal = Mathf.Clamp(sourceCamera.focusDistance, FocalDistance.MinValue, FocalDistance.MaxValue);
                    if (!Mathf.Approximately(_focalDistanceValue.floatValue, focal))
                    {
                        _focalDistanceValue.floatValue = focal;
                    }

                    var aperture = Mathf.Clamp(sourceCamera.aperture, Aperture.MinValue, Aperture.MaxValue);
                    if (!Mathf.Approximately(_apertureValue.floatValue, aperture))
                    {
                        _apertureValue.floatValue = aperture;
                    }

                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.Slider("Focal Distance", focal, FocalDistance.MinValue, FocalDistance.MaxValue);
                        EditorGUILayout.Slider("Aperture", aperture, Aperture.MinValue, Aperture.MaxValue);
                    }
                }
                else
                {
                    EditorGUILayout.Slider(_focalDistanceValue, FocalDistance.MinValue, FocalDistance.MaxValue, "Focal Distance");
                    EditorGUILayout.Slider(_apertureValue, Aperture.MinValue, Aperture.MaxValue, "Aperture");
                }
#else
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.LabelField("Focal Distance", "(not supported on this Unity version)");
                    EditorGUILayout.LabelField("Aperture", "(not supported on this Unity version)");
                }
#endif

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
                    var currentMaskColor = Color.HSVToRGB(
                        _hue.floatValue / Hue.MaxValue,
                        _saturation.floatValue / Saturation.MaxValue,
                        _lightness.floatValue / Lightness.MaxValue);

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
                    EditorGUILayout.PropertyField(_items, new GUIContent("Items"));
                    var last = GUILayoutUtility.GetLastRect();
                    var labelW = EditorGUIUtility.labelWidth;
                    var fieldW = last.width - labelW;
                    var fieldX = last.x + labelW;
                    var toggleW = EditorGUIUtility.singleLineHeight;
                    var suffixRect = new Rect(fieldX + toggleW, last.y, fieldW - toggleW, last.height);
                    EditorGUI.LabelField(suffixRect, "(not supported)", EditorStyles.miniLabel);
                }
            }

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
                EditorGUILayout.PropertyField(_triggerTakesPhotos, new GUIContent("Trigger Takes Photos"));
                EditorGUILayout.PropertyField(_dollyPathsStayVisible, new GUIContent("Dolly Paths Stay Visible"));
            }

            EditorGUILayout.Space();
            serializedObject.ApplyModifiedProperties();
        }

        private void InitializePortDefaults()
        {
            if (_portValue == null)
            {
                _lastValidPort = 9000;
                return;
            }

            var currentValue = _portValue.intValue;
            if (currentValue < PortNumber.MinValue || currentValue > PortNumber.MaxValue)
            {
                currentValue = 9000;
                _portValue.intValue = currentValue;
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
                serializedObject.Update();
            }

            _lastValidPort = currentValue;
        }

        private void DrawPortField()
        {
            if (_portValue == null)
            {
                EditorGUILayout.PropertyField(_port);
                return;
            }

            if (_portValue.intValue >= PortNumber.MinValue && _portValue.intValue <= PortNumber.MaxValue)
            {
                _lastValidPort = _portValue.intValue;
            }

            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUILayout.DelayedIntField(new GUIContent("Port"), _portValue.intValue);
            if (EditorGUI.EndChangeCheck())
            {
                if (newValue < PortNumber.MinValue || newValue > PortNumber.MaxValue)
                {
                    _portValue.intValue = _lastValidPort;
                    Debug.LogWarning($"[{nameof(VRCCameraComponent)}] Port must be between {PortNumber.MinValue} and {PortNumber.MaxValue}. Reverting to {_lastValidPort}.");
                }
                else
                {
                    _portValue.intValue = newValue;
                    _lastValidPort = newValue;
                }
            }
        }
    }
}
