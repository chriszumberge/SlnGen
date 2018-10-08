using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnGen.Core.Code;

namespace SlnGen.Core.Tests.CodeTests
{
    [TestClass]
    public class SGAccessibilityLevelTests
    {
        [TestMethod]
        public void PublicToStringIsCorrect()
        {
            Assert.AreEqual("public", SGAccessibilityLevel.Public.ToString());
        }

        [TestMethod]
        public void PrivateToStringIsCorrect()
        {
            Assert.AreEqual("private", SGAccessibilityLevel.Private.ToString());
        }

        [TestMethod]
        public void ProtectedToStringIsCorrect()
        {
            Assert.AreEqual("protected", SGAccessibilityLevel.Protected.ToString());
        }

        [TestMethod]
        public void InternalToStringIsCorrect()
        {
            Assert.AreEqual("internal", SGAccessibilityLevel.Internal.ToString());
        }

        [TestMethod]
        public void PublicCastIntIsCorrect()
        {
            Assert.AreEqual(1, (int)SGAccessibilityLevel.Public);
        }

        [TestMethod]
        public void PrivateCastIntIsCorrect()
        {
            Assert.AreEqual(2, (int)SGAccessibilityLevel.Private);
        }

        [TestMethod]
        public void ProtectedCastIntIsCorrect()
        {
            Assert.AreEqual(3, (int)SGAccessibilityLevel.Protected);
        }

        [TestMethod]
        public void InternalCastIntIsCorrect()
        {
            Assert.AreEqual(4, (int)SGAccessibilityLevel.Internal);
        }
    }
}
