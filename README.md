# VRC Camera Synchronizer

A Unity package for synchronizing camera parameters to VRChat via OSC (Open Sound Control).

## Features

- Real-time synchronization of 14 camera parameters to VRChat via OSC
- Reactive property system for efficient change detection
- Event-driven architecture (sends OSC messages only when values change)
- Clean architecture with value objects and dependency injection support
- Comprehensive test coverage
- Type-safe OSC message handling

## Requirements

- Unity 2022.3.22f1 or later
- [OscJack](https://github.com/keijiro/OscJack) (OSC implementation for Unity)
- VRChat with OSC enabled

## Installation

1. Import OscJack into your Unity project
2. Copy the `VRCCameraSynchronizer` folder to your `Assets` directory
3. Add the VRC Camera Synchronizer component to your camera

## Usage

### Basic Setup

1. Select the camera GameObject you want to synchronize
2. Add Component → VRCCamera → VRC Camera Synchronizer
3. Configure the OSC settings:
   - **Destination**: IP address of the target (default: 127.0.0.1)
   - **Port**: OSC port number (default: 9000)

### VRChat Configuration

1. Enable OSC in VRChat:
   - Open the Action Menu (R on desktop)
   - Navigate to Options → OSC → Enabled

2. The synchronizer will automatically send camera and avatar parameters via OSC

## OSC Endpoints

Based on VRChat's OSC documentation ([VRChat 2025.3.3 Open Beta - OSC Camera Endpoints](https://docs.vrchat.com/docs/vrchat-202533-openbeta#osc-camera-endpoints)), the following endpoints are used:

### Camera Parameters
| Endpoint | Type | Range | Description |
|----------|------|-------|-------------|
| `/usercamera/Zoom` | Float | 20-150 | Camera focal length in mm |
| `/usercamera/Exposure` | Float | -10 to 4 | Exposure compensation |
| `/usercamera/FocalDistance` | Float | 0-10 | Focus distance in meters |
| `/usercamera/Aperture` | Float | 5.6-22 | Camera aperture (f-stop) |
| `/usercamera/Hue` | Float | 0-1 | Green screen hue |
| `/usercamera/Saturation` | Float | 0-1 | Green screen saturation |
| `/usercamera/Lightness` | Float | 0-1 | Green screen lightness |
| `/usercamera/LookAtMeXOffset` | Float | -1 to 1 | LookAtMe horizontal offset |
| `/usercamera/LookAtMeYOffset` | Float | -1 to 1 | LookAtMe vertical offset |
| `/usercamera/FlySpeed` | Float | 0.1-10 | Movement fly speed |
| `/usercamera/TurnSpeed` | Float | 0.1-5 | Movement turn speed |
| `/usercamera/SmoothingStrength` | Float | 0.1-10 | Movement smoothing strength |
| `/usercamera/PhotoRate` | Float | 0.1-2 | Photo capture rate |
| `/usercamera/Duration` | Float | 0.1-60 | Photo duration in seconds |


## License

License pending

## Acknowledgments

- Built with [OscJack](https://github.com/keijiro/OscJack) by Keijiro Takahashi
- Designed for use with [VRChat](https://vrchat.com/)