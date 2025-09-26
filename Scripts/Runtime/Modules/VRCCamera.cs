using System;
using UnityEngine;

namespace Astearium.VRChat.Camera
{
    public class VRCCamera : IDisposable
    {
        public ReactiveProperty<Zoom> Zoom { get; } = new(new Zoom(Astearium.VRChat.Camera.Zoom.DefaultValue));

        public ReactiveProperty<Exposure> Exposure { get; } =
            new(new Exposure(Astearium.VRChat.Camera.Exposure.DefaultValue));

        public ReactiveProperty<FocalDistance> FocalDistance { get; } = new(
            new FocalDistance(Astearium.VRChat.Camera.FocalDistance.DefaultValue));

        public ReactiveProperty<Aperture> Aperture { get; } =
            new(new Aperture(Astearium.VRChat.Camera.Aperture.DefaultValue));

        public ReactiveProperty<Hue> Hue { get; } = new(new Hue(Astearium.VRChat.Camera.Hue.DefaultValue));

        public ReactiveProperty<Saturation> Saturation { get; } =
            new(new Saturation(Astearium.VRChat.Camera.Saturation.DefaultValue));

        public ReactiveProperty<Lightness> Lightness { get; } =
            new(new Lightness(Astearium.VRChat.Camera.Lightness.DefaultValue));

        public ReactiveProperty<LookAtMeOffset> LookAtMeOffset { get; } = new(new LookAtMeOffset(
            new LookAtMeXOffset(LookAtMeXOffset.DefaultValue),
            new LookAtMeYOffset(LookAtMeYOffset.DefaultValue)));

        public ReactiveProperty<FlySpeed> FlySpeed { get; } =
            new(new FlySpeed(Astearium.VRChat.Camera.FlySpeed.DefaultValue));

        public ReactiveProperty<TurnSpeed> TurnSpeed { get; } =
            new(new TurnSpeed(Astearium.VRChat.Camera.TurnSpeed.DefaultValue));

        public ReactiveProperty<SmoothingStrength> SmoothingStrength { get; } = new(
            new SmoothingStrength(Astearium.VRChat.Camera.SmoothingStrength.DefaultValue));

        public ReactiveProperty<PhotoRate> PhotoRate { get; } =
            new(new PhotoRate(Astearium.VRChat.Camera.PhotoRate.DefaultValue));

        public ReactiveProperty<Duration> Duration { get; } =
            new(new Duration(Astearium.VRChat.Camera.Duration.DefaultValue));

        /// <summary>
        /// Current world-space pose (position + rotation) for OSC sync
        /// </summary>
        public ReactiveProperty<Pose> Pose { get; } = new(UnityEngine.Pose.identity);

        public ReactiveProperty<bool> ShowUIInCamera { get; } = new(false);
        public ReactiveProperty<bool> Lock { get; } = new(false);
        public ReactiveProperty<bool> LocalPlayer { get; } = new(true);
        public ReactiveProperty<bool> RemotePlayer { get; } = new(true);
        public ReactiveProperty<bool> Environment { get; } = new(true);
        public ReactiveProperty<bool> GreenScreen { get; } = new(false);
        public ReactiveProperty<bool> SmoothMovement { get; } = new(false);
        public ReactiveProperty<bool> LookAtMe { get; } = new(false);
        public ReactiveProperty<bool> AutoLevelRoll { get; } = new(false);
        public ReactiveProperty<bool> AutoLevelPitch { get; } = new(false);
        public ReactiveProperty<bool> Flying { get; } = new(false);
        public ReactiveProperty<bool> TriggerTakesPhotos { get; } = new(false);
        public ReactiveProperty<bool> DollyPathsStayVisible { get; } = new(false);
        public ReactiveProperty<bool> CameraEars { get; } = new(false);
        public ReactiveProperty<bool> ShowFocus { get; } = new(false);
        public ReactiveProperty<bool> Streaming { get; } = new(false);
        public ReactiveProperty<bool> RollWhileFlying { get; } = new(false);
        public ReactiveProperty<Orientation> Orientation { get; } = new(Astearium.VRChat.Camera.Orientation.Landscape);
        public ReactiveProperty<bool> Items { get; } = new(true);

        /// <summary>
        /// Current camera <see cref="Mode"/>; changes are published over OSC.
        /// </summary>
        public ReactiveProperty<Mode> Mode { get; } = new(Astearium.VRChat.Camera.Mode.Photo);

        /// <summary>
        /// Disposes the VRCCamera and clears all event subscriptions
        /// </summary>
        public void Dispose()
        {
            // Clear all event subscriptions to prevent memory leaks
            Zoom?.ClearSubscriptions();
            Exposure?.ClearSubscriptions();
            FocalDistance?.ClearSubscriptions();
            Aperture?.ClearSubscriptions();
            Hue?.ClearSubscriptions();
            Saturation?.ClearSubscriptions();
            Lightness?.ClearSubscriptions();
            LookAtMeOffset?.ClearSubscriptions();
            FlySpeed?.ClearSubscriptions();
            TurnSpeed?.ClearSubscriptions();
            SmoothingStrength?.ClearSubscriptions();
            PhotoRate?.ClearSubscriptions();
            Duration?.ClearSubscriptions();
            ShowUIInCamera?.ClearSubscriptions();
            Lock?.ClearSubscriptions();
            LocalPlayer?.ClearSubscriptions();
            RemotePlayer?.ClearSubscriptions();
            Environment?.ClearSubscriptions();
            GreenScreen?.ClearSubscriptions();
            SmoothMovement?.ClearSubscriptions();
            LookAtMe?.ClearSubscriptions();
            AutoLevelRoll?.ClearSubscriptions();
            AutoLevelPitch?.ClearSubscriptions();
            Flying?.ClearSubscriptions();
            TriggerTakesPhotos?.ClearSubscriptions();
            DollyPathsStayVisible?.ClearSubscriptions();
            CameraEars?.ClearSubscriptions();
            ShowFocus?.ClearSubscriptions();
            Streaming?.ClearSubscriptions();
            RollWhileFlying?.ClearSubscriptions();
            Orientation?.ClearSubscriptions();
            Mode?.ClearSubscriptions();
            Pose?.ClearSubscriptions();
            Items?.ClearSubscriptions();
        }
    }
}