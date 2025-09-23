using NUnit.Framework;
using UnityEngine;

namespace Astearium.VRChat.Camera.Tests.Utility
{
    internal static class MathAssert
    {
        public static void AreApproximatelyEqual(float expected, float actual)
        {
            if (!Mathf.Approximately(expected, actual))
            {
                Assert.Fail($"Expected {expected} but was {actual} (delta {actual - expected}).");
            }
        }
    }
}
