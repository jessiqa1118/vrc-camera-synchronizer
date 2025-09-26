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

        private SerializedProperty _mode;
        private SerializedProperty _pose;

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
        private SerializedProperty _orientationIsLandscape;
        private SerializedProperty _poseTransform;
        private SerializedProperty _syncPoseFromTransform;
        private SerializedProperty _syncUnityCamera;
        private SerializedProperty _unityCameraOverride;
        private SerializedProperty _zoom;
        private SerializedProperty _focalDistance;
        private SerializedProperty _aperture;

        private void OnEnable()
        {
            serializedObject.Update();

            _destination = serializedObject.FindProperty(VRCCameraComponent.DestinationFieldName);
            _port = serializedObject.FindProperty(VRCCameraComponent.PortFieldName);
            _portValue = _port?.FindPropertyRelative("value");

            _mode = serializedObject.FindProperty(VRCCameraComponent.ModeFieldName);
            _pose = serializedObject.FindProperty(VRCCameraComponent.PoseFieldName);

            _exposure = serializedObject.FindProperty(VRCCameraComponent.ExposureFieldName);
            _hue = serializedObject.FindProperty(VRCCameraComponent.HueFieldName);
            _saturation = serializedObject.FindProperty(VRCCameraComponent.SaturationFieldName);
            _lightness = serializedObject.FindProperty(VRCCameraComponent.LightnessFieldName);
            _lookAtMeXOffset = serializedObject.FindProperty(VRCCameraComponent.LookAtMeXOffsetFieldName);
            _lookAtMeYOffset = serializedObject.FindProperty(VRCCameraComponent.LookAtMeYOffsetFieldName);
            _flySpeed = serializedObject.FindProperty(VRCCameraComponent.FlySpeedFieldName);
            _turnSpeed = serializedObject.FindProperty(VRCCameraComponent.TurnSpeedFieldName);
            _smoothingStrength = serializedObject.FindProperty(VRCCameraComponent.SmoothingStrengthFieldName);
            _photoRate = serializedObject.FindProperty(VRCCameraComponent.PhotoRateFieldName);
            _duration = serializedObject.FindProperty(VRCCameraComponent.DurationFieldName);
            _showUIInCamera = serializedObject.FindProperty(VRCCameraComponent.ShowUIInCameraFieldName);
            _lockCamera = serializedObject.FindProperty(VRCCameraComponent.LockCameraFieldName);
            _localPlayer = serializedObject.FindProperty(VRCCameraComponent.LocalPlayerFieldName);
            _remotePlayer = serializedObject.FindProperty(VRCCameraComponent.RemotePlayerFieldName);
            _environment = serializedObject.FindProperty(VRCCameraComponent.EnvironmentFieldName);
            _greenScreen = serializedObject.FindProperty(VRCCameraComponent.GreenScreenFieldName);
            _items = serializedObject.FindProperty(VRCCameraComponent.ItemsFieldName);
            _smoothMovement = serializedObject.FindProperty(VRCCameraComponent.SmoothMovementFieldName);
            _lookAtMe = serializedObject.FindProperty(VRCCameraComponent.LookAtMeFieldName);
            _autoLevelRoll = serializedObject.FindProperty(VRCCameraComponent.AutoLevelRollFieldName);
            _autoLevelPitch = serializedObject.FindProperty(VRCCameraComponent.AutoLevelPitchFieldName);
            _flying = serializedObject.FindProperty(VRCCameraComponent.FlyingFieldName);
            _triggerTakesPhotos = serializedObject.FindProperty(VRCCameraComponent.TriggerTakesPhotosFieldName);
            _dollyPathsStayVisible = serializedObject.FindProperty(VRCCameraComponent.DollyPathsStayVisibleFieldName);
            _cameraEars = serializedObject.FindProperty(VRCCameraComponent.CameraEarsFieldName);
            _showFocus = serializedObject.FindProperty(VRCCameraComponent.ShowFocusFieldName);
            _streaming = serializedObject.FindProperty(VRCCameraComponent.StreamingFieldName);
            _rollWhileFlying = serializedObject.FindProperty(VRCCameraComponent.RollWhileFlyingFieldName);
            _orientationIsLandscape = serializedObject.FindProperty(VRCCameraComponent.OrientationFieldName);
            _poseTransform = serializedObject.FindProperty(VRCCameraComponent.PoseSourceFieldName);
            _syncPoseFromTransform = serializedObject.FindProperty(VRCCameraComponent.SyncPoseFromTransformFieldName);
            _syncUnityCamera = serializedObject.FindProperty(VRCCameraComponent.SyncUnityCameraFieldName);
            _unityCameraOverride = serializedObject.FindProperty(VRCCameraComponent.UnityCameraFieldName);
            _zoom = serializedObject.FindProperty(VRCCameraComponent.ZoomFieldName);
            _focalDistance = serializedObject.FindProperty(VRCCameraComponent.FocalDistanceFieldName);
            _aperture = serializedObject.FindProperty(VRCCameraComponent.ApertureFieldName);

            InitializePortDefaults();

            ApplyEditorDefaults();
        }

        private void ApplyEditorDefaults()
        {
            var changed = false;

            if (_mode is { intValue: (int)Mode.Off })
            {
                _mode.intValue = (int)Mode.Photo;
                changed = true;
            }

            if (_localPlayer is { boolValue: false })
            {
                _localPlayer.boolValue = true;
                changed = true;
            }

            if (_remotePlayer is { boolValue: false })
            {
                _remotePlayer.boolValue = true;
                changed = true;
            }

            if (_environment is { boolValue: false })
            {
                _environment.boolValue = true;
                changed = true;
            }

            if (_items is { boolValue: false })
            {
                _items.boolValue = true;
                changed = true;
            }

            // Default greenscreen color to RGB 00FF00 (Hue 120, Saturation 100, Lightness 100)
            if (_hue != null && Mathf.Approximately(_hue.floatValue, Hue.DefaultValue))
            {
                _hue.floatValue = 120f;
                changed = true;
            }

            if (_saturation != null && Mathf.Approximately(_saturation.floatValue, Saturation.DefaultValue))
            {
                _saturation.floatValue = 100f;
                changed = true;
            }

            if (_lightness != null && Mathf.Approximately(_lightness.floatValue, Lightness.DefaultValue))
            {
                _lightness.floatValue = 100f;
                changed = true;
            }

            if (changed)
            {
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
                serializedObject.Update();
            }
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

                var posePosition = _pose?.FindPropertyRelative("position");
                var poseRotation = _pose?.FindPropertyRelative("rotation");

                if (_syncPoseFromTransform.boolValue)
                {
                    var src = _poseTransform.objectReferenceValue as Transform;
                    if (src)
                    {
                        if (posePosition != null)
                        {
                            posePosition.vector3Value = src.position;
                        }

                        if (poseRotation != null)
                        {
                            poseRotation.quaternionValue = src.rotation;
                        }
                    }
                }

                using (new EditorGUI.DisabledScope(_syncPoseFromTransform.boolValue))
                {
                    if (posePosition != null)
                    {
                        EditorGUILayout.PropertyField(posePosition, new GUIContent("Position"));
                    }

                    if (poseRotation != null)
                    {
                        var currentEuler = poseRotation.quaternionValue.eulerAngles;
                        EditorGUI.BeginChangeCheck();
                        var newEuler = EditorGUILayout.Vector3Field("Rotation (Euler, degrees)", currentEuler);
                        if (EditorGUI.EndChangeCheck())
                        {
                            poseRotation.quaternionValue = Quaternion.Euler(newEuler);
                        }
                    }
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
                    targetComponent.Close();
                }

                if (GUILayout.Button("Take Photo"))
                {
                    serializedObject.ApplyModifiedProperties();
                    var targetComponent = (VRCCameraComponent)target;
                    targetComponent.Capture();
                }

                if (GUILayout.Button("Timed (5s)"))
                {
                    serializedObject.ApplyModifiedProperties();
                    var targetComponent = (VRCCameraComponent)target;
                    targetComponent.CaptureDelayed();
                }
            }

            EditorGUILayout.Space();

            // General (under Actions)
            EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                var modeOrder = new[]
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

                var currentOrientation =
                    _orientationIsLandscape.boolValue ? Orientation.Landscape : Orientation.Portrait;
                EditorGUI.BeginChangeCheck();
                var newOrientation = (Orientation)EditorGUILayout.EnumPopup("Orientation", currentOrientation);
                if (EditorGUI.EndChangeCheck())
                {
                    _orientationIsLandscape.boolValue = newOrientation == Orientation.Landscape;
                }

                EditorGUILayout.Slider(_exposure, Exposure.MinValue, Exposure.MaxValue, "Exposure");

                var sourceCamera = _unityCameraOverride?.objectReferenceValue as UnityEngine.Camera;

                var isSyncing = _syncUnityCamera is { boolValue: true };
                using (new EditorGUI.DisabledScope(isSyncing))
                {
                    if (isSyncing && sourceCamera != null)
                    {
                        var syncedZoom = Mathf.Clamp(sourceCamera.focalLength, Zoom.MinValue, Zoom.MaxValue);
                        if (!Mathf.Approximately(_zoom.floatValue, syncedZoom))
                        {
                            _zoom.floatValue = syncedZoom;
                        }

                        EditorGUILayout.Slider("Zoom (Focal Length)", syncedZoom, Zoom.MinValue, Zoom.MaxValue);
                    }
                    else
                    {
                        EditorGUILayout.Slider(_zoom, Zoom.MinValue, Zoom.MaxValue, "Zoom (Focal Length)");
                    }
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
                EditorGUILayout.PropertyField(_autoLevelRoll, new GUIContent("Auto Level Roll"));
            }

            EditorGUILayout.Space();

            // Focus
            EditorGUILayout.LabelField("Focus", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                var sourceCamera = _unityCameraOverride?.objectReferenceValue as UnityEngine.Camera;

#if UNITY_2021_2_OR_NEWER
                var isSyncing = _syncUnityCamera is { boolValue: true };
                using (new EditorGUI.DisabledScope(isSyncing))
                {
                    if (isSyncing && sourceCamera)
                    {
                        var focal = Mathf.Clamp(sourceCamera.focusDistance, FocalDistance.MinValue,
                            FocalDistance.MaxValue);
                        if (!Mathf.Approximately(_focalDistance.floatValue, focal))
                        {
                            _focalDistance.floatValue = focal;
                        }

                        var aperture = Mathf.Clamp(sourceCamera.aperture, Aperture.MinValue, Aperture.MaxValue);
                        if (!Mathf.Approximately(_aperture.floatValue, aperture))
                        {
                            _aperture.floatValue = aperture;
                        }

                        EditorGUILayout.Slider("Focal Distance", focal, FocalDistance.MinValue, FocalDistance.MaxValue);
                        EditorGUILayout.Slider("Aperture", aperture, Aperture.MinValue, Aperture.MaxValue);
                    }
                    else
                    {
                        EditorGUILayout.Slider(_focalDistance, FocalDistance.MinValue, FocalDistance.MaxValue,
                            "Focal Distance");
                        EditorGUILayout.Slider(_aperture, Aperture.MinValue, Aperture.MaxValue, "Aperture");
                    }
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
                        Color.RGBToHSV(newMaskColor, out var h, out var s, out var v);
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
                EditorGUILayout.PropertyField(_dollyPathsStayVisible, new GUIContent("Show Paths"));
            }

            EditorGUILayout.Space();

            // Others (migrated from Toggles)
            EditorGUILayout.LabelField("Others", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_lockCamera, new GUIContent("Lock"));
                EditorGUILayout.PropertyField(_triggerTakesPhotos, new GUIContent("Trigger Takes Photos"));
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
            if (currentValue is < PortNumber.MinValue or > PortNumber.MaxValue)
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

            if (_portValue.intValue is >= PortNumber.MinValue and <= PortNumber.MaxValue)
            {
                _lastValidPort = _portValue.intValue;
            }

            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUILayout.DelayedIntField(new GUIContent("Port"), _portValue.intValue);
            if (!EditorGUI.EndChangeCheck()) return;

            if (newValue is < PortNumber.MinValue or > PortNumber.MaxValue)
            {
                _portValue.intValue = _lastValidPort;
                Debug.LogWarning(
                    $"[{nameof(VRCCameraComponent)}] Port must be between {PortNumber.MinValue} and {PortNumber.MaxValue}. Reverting to {_lastValidPort}.");
            }
            else
            {
                _portValue.intValue = newValue;
                _lastValidPort = newValue;
            }
        }
    }
}