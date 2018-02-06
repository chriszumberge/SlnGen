using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnGen.Core.Code;

namespace SlnGen.Core.Tests.CodeTests
{
    [TestClass]
    public class AccessibilityLevelTests
    {
        [TestMethod]
        public void PublicToStringIsCorrect()
        {
            Assert.AreEqual("public", AccessibilityLevel.Public.ToString());
        }

        [TestMethod]
        public void PrivateToStringIsCorrect()
        {
            Assert.AreEqual("private", AccessibilityLevel.Private.ToString());
        }

        [TestMethod]
        public void ProtectedToStringIsCorrect()
        {
            Assert.AreEqual("protected", AccessibilityLevel.Protected.ToString());
        }

        [TestMethod]
        public void InternalToStringIsCorrect()
        {
            Assert.AreEqual("internal", AccessibilityLevel.Internal.ToString());
        }

        [TestMethod]
        public void PublicCastIntIsCorrect()
        {
            Assert.AreEqual(1, (int)AccessibilityLevel.Public);
        }

        [TestMethod]
        public void PrivateCastIntIsCorrect()
        {
            Assert.AreEqual(2, (int)AccessibilityLevel.Private);
        }

        [TestMethod]
        public void ProtectedCastIntIsCorrect()
        {
            Assert.AreEqual(3, (int)AccessibilityLevel.Protected);
        }

        [TestMethod]
        public void InternalCastIntIsCorrect()
        {
            Assert.AreEqual(4, (int)AccessibilityLevel.Internal);
        }
    }
}
