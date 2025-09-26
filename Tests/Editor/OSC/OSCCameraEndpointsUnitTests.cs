using NUnit.Framework;
using Astearium.VRChat.Camera;
using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class OSCCameraEndpointsUnitTests
    {
        [Test]
        public void AllEndpoints_StartWithBasePath()
        {
            // Assert
            Assert.IsTrue(OSCCameraEndpoints.Mode.Value.StartsWith("/usercamera"));
            Assert.IsTrue(OSCCameraEndpoints.Zoom.Value.StartsWith("/usercamera"));
            Assert.IsTrue(OSCCameraEndpoints.Capture.Value.StartsWith("/usercamera"));
        }
        
        [Test]
        public void Mode_HasCorrectPath()
        {
            // Assert
            Assert.AreEqual("/usercamera/Mode", OSCCameraEndpoints.Mode.Value);
        }
        
        [Test]
        public void Pose_HasCorrectPath()
        {
            // Assert
            Assert.AreEqual("/usercamera/Pose", OSCCameraEndpoints.Pose.Value);
        }
        
        [Test]
        public void Actions_HaveCorrectPaths()
        {
            // Assert
            Assert.AreEqual("/usercamera/Close", OSCCameraEndpoints.Close.Value);
            Assert.AreEqual("/usercamera/Capture", OSCCameraEndpoints.Capture.Value);
            Assert.AreEqual("/usercamera/CaptureDelayed", OSCCameraEndpoints.CaptureDelayed.Value);
        }
        
        [Test]
        public void Toggles_HaveCorrectPaths()
        {
            // Assert
            Assert.AreEqual("/usercamera/ShowUIInCamera", OSCCameraEndpoints.ShowUIInCamera.Value);
            Assert.AreEqual("/usercamera/Lock", OSCCameraEndpoints.Lock.Value);
            Assert.AreEqual("/usercamera/LocalPlayer", OSCCameraEndpoints.LocalPlayer.Value);
            Assert.AreEqual("/usercamera/RemotePlayer", OSCCameraEndpoints.RemotePlayer.Value);
            Assert.AreEqual("/usercamera/Environment", OSCCameraEndpoints.Environment.Value);
            Assert.AreEqual("/usercamera/GreenScreen", OSCCameraEndpoints.GreenScreen.Value);
            Assert.AreEqual("/usercamera/SmoothMovement", OSCCameraEndpoints.SmoothMovement.Value);
            Assert.AreEqual("/usercamera/LookAtMe", OSCCameraEndpoints.LookAtMe.Value);
            Assert.AreEqual("/usercamera/AutoLevelRoll", OSCCameraEndpoints.AutoLevelRoll.Value);
            Assert.AreEqual("/usercamera/AutoLevelPitch", OSCCameraEndpoints.AutoLevelPitch.Value);
            Assert.AreEqual("/usercamera/Flying", OSCCameraEndpoints.Flying.Value);
            Assert.AreEqual("/usercamera/TriggerTakesPhotos", OSCCameraEndpoints.TriggerTakesPhotos.Value);
            Assert.AreEqual("/usercamera/DollyPathsStayVisible", OSCCameraEndpoints.DollyPathsStayVisible.Value);
            Assert.AreEqual("/usercamera/CameraEars", OSCCameraEndpoints.CameraEars.Value);
            Assert.AreEqual("/usercamera/ShowFocus", OSCCameraEndpoints.ShowFocus.Value);
            Assert.AreEqual("/usercamera/Streaming", OSCCameraEndpoints.Streaming.Value);
            Assert.AreEqual("/usercamera/RollWhileFlying", OSCCameraEndpoints.RollWhileFlying.Value);
            Assert.AreEqual("/usercamera/OrientationIsLandscape", OSCCameraEndpoints.OrientationIsLandscape.Value);
        }
        
        [Test]
        public void Sliders_HaveCorrectPaths()
        {
            // Assert
            Assert.AreEqual("/usercamera/Zoom", OSCCameraEndpoints.Zoom.Value);
            Assert.AreEqual("/usercamera/Exposure", OSCCameraEndpoints.Exposure.Value);
            Assert.AreEqual("/usercamera/FocalDistance", OSCCameraEndpoints.FocalDistance.Value);
            Assert.AreEqual("/usercamera/Aperture", OSCCameraEndpoints.Aperture.Value);
            Assert.AreEqual("/usercamera/Hue", OSCCameraEndpoints.Hue.Value);
            Assert.AreEqual("/usercamera/Saturation", OSCCameraEndpoints.Saturation.Value);
            Assert.AreEqual("/usercamera/Lightness", OSCCameraEndpoints.Lightness.Value);
            Assert.AreEqual("/usercamera/LookAtMeXOffset", OSCCameraEndpoints.LookAtMeXOffset.Value);
            Assert.AreEqual("/usercamera/LookAtMeYOffset", OSCCameraEndpoints.LookAtMeYOffset.Value);
            Assert.AreEqual("/usercamera/FlySpeed", OSCCameraEndpoints.FlySpeed.Value);
            Assert.AreEqual("/usercamera/TurnSpeed", OSCCameraEndpoints.TurnSpeed.Value);
            Assert.AreEqual("/usercamera/SmoothingStrength", OSCCameraEndpoints.SmoothingStrength.Value);
            Assert.AreEqual("/usercamera/PhotoRate", OSCCameraEndpoints.PhotoRate.Value);
            Assert.AreEqual("/usercamera/Duration", OSCCameraEndpoints.Duration.Value);
        }
        
        [Test]
        public void AllEndpoints_AreValidOSCAddresses()
        {
            // Act & Assert - Creating Address objects validates the paths
            Assert.DoesNotThrow(() =>
            {
                _ = OSCCameraEndpoints.Mode;
                _ = OSCCameraEndpoints.Zoom;
                _ = OSCCameraEndpoints.Capture;
                _ = OSCCameraEndpoints.Lock;
                _ = OSCCameraEndpoints.Exposure;
            });
        }
        
        [Test]
        public void Endpoints_AreReadOnly()
        {
            // Assert
            var type = typeof(OSCCameraEndpoints);
            var fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(Address))
                {
                    Assert.IsTrue(field.IsInitOnly, $"Field {field.Name} should be readonly");
                }
            }
        }
    }
}
