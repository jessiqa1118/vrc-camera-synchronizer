# VRC Camera Synchronizer

A Unity package for synchronizing camera parameters to VRChat via OSC (Open Sound Control).

## Features

- Real-time camera focal length synchronization to VRChat
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
2. Add Component → JessiQa → VRC Camera Synchronizer
3. Configure the OSC settings:
   - **Destination**: IP address of the target (default: 127.0.0.1)
   - **Port**: OSC port number (default: 9000)

### VRChat Configuration

1. Enable OSC in VRChat:
   - Open the Action Menu (R on desktop)
   - Navigate to Options → OSC → Enabled

2. The synchronizer will automatically send camera data to:
   - `/usercamera/Zoom` - Focal length value (20-150 range)

## OSC Endpoints

Based on VRChat's OSC documentation ([VRChat 2025.3.3 Open Beta - OSC Camera Endpoints](https://docs.vrchat.com/docs/vrchat-202533-openbeta#osc-camera-endpoints)), the following endpoint is used:

| Endpoint | Type | Range | Description |
|----------|------|-------|-------------|
| `/usercamera/Zoom` | Float | 20-150 | Camera focal length in mm |


## License

License pending

## Acknowledgments

- Built with [OscJack](https://github.com/keijiro/OscJack) by Keijiro Takahashi
- Designed for use with [VRChat](https://vrchat.com/)