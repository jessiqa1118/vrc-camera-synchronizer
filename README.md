# VRC Camera Synchronizer

A Unity package for synchronizing VRChat camera parameters over OSC (Open Sound Control).

## Features

- Real-time synchronization of camera sliders, toggles, pose, and mode to VRChat OSC endpoints
- Reactive property system (value changes propagate automatically through `ReactiveProperty<T>`)
- Centralized OSC message factory to keep message creation consistent and testable
- Inspector conveniences: sensible VRChat defaults (`127.0.0.1`, Local Photo, mask toggles enabled, greenscreen 00FF00) and automatic UI locking while Unity Camera sync is enabled
- Comprehensive unit and integration tests aligned with the current runtime API
- Type-safe OSC message handling backed by a flexible OSC message factory

## Requirements

- Unity 2022.3.22f1 or later
- VRChat with OSC enabled

## Installation

### Option 1: Clone into `Assets`

1. In your Unity project root, open a terminal or command prompt.
2. Navigate to the `Assets` directory (e.g., `cd <ProjectRoot>/Assets`).
3. Clone this repository: `git clone https://github.com/jessiqa1118/vrc-camera-synchronizer.git`.
4. Verify the folder is located at `Assets/VRCCameraSynchronizer`.

### Option 2: Import the `.unitypackage`

1. Download the latest `VRCCameraSynchronizer.unitypackage` from the releases page or the provided distribution link.
2. In Unity, select `Assets > Import Package > Custom Package...` and choose the downloaded file.
3. Import all assets when prompted.

## Usage

1. Add the **VRC Camera** component to the camera you want to synchronize.
2. Configure the OSC settings as described below.

### Basic Setup

1. Add Component -> Astearium -> VRChat -> VRC Camera
2. Configure the OSC settings:
   - **Destination**: IP address of the VRChat application host (default: `127.0.0.1`)
   - **Port**: OSC port number (default: `9000`)
   - (Optional) Enable **Sync Unity Camera** and assign a **Source** camera. When enabled, the Zoom/Focal Distance/Aperture sliders become read-only and mirror the assigned camera.
3. Enter Play Mode in the Unity Editor to start broadcasting OSC messages.
4. Adjust the VRC Camera parameters in the Inspector; changes synchronize to VRChat immediately.

### VRChat Configuration

1. Enable OSC in VRChat:
   - Open the Action Menu (R on desktop)
   - Navigate to Options -> OSC -> Enabled
2. The synchronizer will automatically send camera parameters via OSC

## OSC Endpoints

Based on VRChat's OSC documentation ([VRChat 2025.3.3](https://docs.vrchat.com/docs/vrchat-202533)), the synchronizer emits the following endpoints:

The component exposes API properties whose names match the trailing parameter segment of each OSC address, allowing external scripts to control the camera programmatically via the `VRC Camera` component.

### Slider Parameters

| Endpoint | Default | Range | Description |
|----------|---------|-------|-------------|
| `/usercamera/Zoom` | 45 | 20 – 150 | Camera focal length (mm) |
| `/usercamera/Exposure` | 0 | -10 – 4 | Exposure compensation |
| `/usercamera/FocalDistance` | 1.5 | 0 – 10 | Focus distance (m) |
| `/usercamera/Aperture` | 15 | 1.4 – 32 | Camera aperture (f-stop) |
| `/usercamera/Hue` | 120 | 0 – 360 | Greenscreen hue |
| `/usercamera/Saturation` | 100 | 0 – 100 | Greenscreen saturation |
| `/usercamera/Lightness` | 60 | 0 – 100 | Greenscreen lightness |
| `/usercamera/LookAtMeXOffset` | 0 | -25 – 25 | Look-At-Me horizontal offset |
| `/usercamera/LookAtMeYOffset` | 0 | -25 – 25 | Look-At-Me vertical offset |
| `/usercamera/FlySpeed` | 3 | 0.1 – 15 | Fly speed |
| `/usercamera/TurnSpeed` | 1 | 0.1 – 5 | Turn speed |
| `/usercamera/SmoothingStrength` | 5 | 0.1 – 10 | Movement smoothing strength |
| `/usercamera/PhotoRate` | 1 | 0.1 – 2 | Photo capture rate (shots/sec) |
| `/usercamera/Duration` | 2 | 0.1 – 60 | Photo duration (sec) |

### Toggles & Miscellaneous

| Endpoint | Type | Description |
|----------|------|-------------|
| `/usercamera/ShowUI` | Bool | Show UI in camera |
| `/usercamera/Lock` | Bool | Lock camera |
| `/usercamera/LocalPlayer` | Bool | Include local player |
| `/usercamera/RemotePlayers` | Bool | Include remote players |
| `/usercamera/Environment` | Bool | Include environment |
| `/usercamera/GreenScreen` | Bool | Enable greenscreen |
| `/usercamera/SmoothMovement` | Bool | Smooth movement toggle |
| `/usercamera/LookAtMe` | Bool | Look-At-Me toggle |
| `/usercamera/AutoLevelRoll` | Bool | Auto-level roll |
| `/usercamera/AutoLevelPitch` | Bool | Auto-level pitch |
| `/usercamera/Flying` | Bool | Flying mode |
| `/usercamera/TriggerTakesPhoto` | Bool | Controller trigger captures |
| `/usercamera/DollyPathsStayVisible` | Bool | Keep dolly paths visible |
| `/usercamera/CameraEars` | Bool | Enable camera ears |
| `/usercamera/ShowFocus` | Bool | Show focus peaking |
| `/usercamera/Streaming` | Bool | Enable streaming |
| `/usercamera/RollWhileFlying` | Bool | Allow roll while flying |
| `/usercamera/OrientationIsLandscape` | Bool | Landscape orientation flag |
| `/usercamera/Mode` | Int | Camera mode (enum index) |
| `/usercamera/Pose` | Float[6] | Position (XYZ) + rotation (XYZ Euler) |

## License

License pending

## Acknowledgments

- Designed for use with [VRChat](https://vrchat.com/)
