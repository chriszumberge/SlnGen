using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnGen.Core.Code;

namespace SlnGen.Core.Tests.CodeTests
{
    [TestClass]
    public class SGMethodSignatureTests
    {
        [TestMethod]
        public void TestMethodCtor_InitsFields()
        {
            // Arrange
            string methodName = "Method";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            string returnType = "int";
            bool @static = true;
            bool @async = true;
            bool @override = true;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName, accessibilityLevel, @static, @async, @override, returnType);

            // Assert
            Assert.AreEqual(methodName, method.MethodName);
            Assert.AreEqual(accessibilityLevel, method.AccessibilityLevel);
            Assert.AreEqual(returnType, method.ReturnType);
            Assert.AreEqual(@static, method.IsStatic);
            Assert.AreEqual(async, method.IsAsync);
            Assert.AreEqual(@override, method.IsOverride);
        }

        [TestMethod]
        public void TestMethodCtor_Defaults_InitsFields()
        {
            // Arrange
            string methodName = "Method";

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Assert
            Assert.AreEqual(methodName, method.MethodName);
            Assert.AreEqual(SGAccessibilityLevel.Private, method.AccessibilityLevel);
            Assert.AreEqual("void", method.ReturnType);
            Assert.AreEqual(false, method.IsStatic);
            Assert.AreEqual(false, method.IsAsync);
            Assert.AreEqual(false, method.IsOverride);
        }

        [TestMethod]
        public void TestMethodCtor_TypedReturnType_InitsFields()
        {
            // Arrange
            string methodName = "Method";
            Type returnType = typeof(int);

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);

            // Assert
            Assert.AreEqual(returnType.Name, method.ReturnType);
        }

        [TestMethod]
        public void TestMethodCtor_All_with_TypedReturnType_InitsFields()
        {
            // Arrange
            string methodName = "Method";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            Type returnType = typeof(int);
            bool @static = true;
            bool @async = true;
            bool @override = true;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName, accessibilityLevel, @static, @async, @override, returnType);

            // Assert
            Assert.AreEqual(methodName, method.MethodName);
            Assert.AreEqual(accessibilityLevel, method.AccessibilityLevel);
            Assert.AreEqual(returnType.Name, method.ReturnType);
            Assert.AreEqual(@static, method.IsStatic);
            Assert.AreEqual(async, method.IsAsync);
            Assert.AreEqual(@override, method.IsOverride);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethodCtor_NullMethodName_ThrowsArgumentNullException()
        {
            // Arrange
            string methodName = null;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethodCtor_EmptyMethodName_ThrowsArgumentException()
        {
            // Arrange
            string methodName = String.Empty;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName);
        }

        [TestMethod]
        public void TestMethodCtor_NameWithSpaces_ReplacedWithUnderscores()
        {
            // Arrange
            string methodName = "Some Method Name";

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Assert
            Assert.AreEqual(methodName.Replace(" ", "_"), method.MethodName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethodCtor_NullStringReturnType_ThrowsArgumentNullException()
        {
            // Arrange
            string methodName = "Method";
            string returnType = null;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethodCtor_EmptyStringReturnType_ThrowsArgumentException()
        {
            // Arrange
            string methodName = "Method";
            string returnType = String.Empty;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethodCtor_NullTypeReturnType_ThrowsArgumentNullException()
        {
            // Arrange
            string methodName = "Method";
            Type returnType = null;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);
        }

        [TestMethod]
        public void TestMethodNameSet()
        {
            // Arrange
            string methodName = "Method";
            string newMethodName = "NewMethod";
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method.MethodName = newMethodName;

            // Assert
            Assert.AreEqual(newMethodName, method.MethodName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethodNameSetNull_ThrowsArgumentNullException()
        {
            // Arrange
            string methodName = "Method";
            string newMethodName = null;
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method.MethodName = newMethodName;
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethodNameSetEmpty_ThrowsArgumentException()
        {
            // Arrange
            string methodName = "Method";
            string newMethodName = String.Empty;
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method.MethodName = newMethodName;
        }

        [TestMethod]
        public void TestMethodNameSet_WithSpaces_ReplacedWithUnderscores()
        {
            // Arrange
            string methodName = "Method";
            string newMethodName = "New Method Name";
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method.MethodName = newMethodName;

            // Assert
            Assert.AreEqual(newMethodName.Replace(" ", "_"), method.MethodName);
        }

        [TestMethod]
        public void TestAccessibilityLevelSet_Property()
        {
            // Arrange
            string methodName = "Method";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            SGAccessibilityLevel newAccessibilityLevel = SGAccessibilityLevel.Protected;
            SGMethodSignature method = new SGMethodSignature(methodName, accessibilityLevel);

            // Act
            method.AccessibilityLevel = newAccessibilityLevel;

            // Assert
            Assert.AreEqual(newAccessibilityLevel, method.AccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAccessibilityLevelSetNull_Property_ThrowsArgumentNullException()
        {
            // Arrange
            string methodName = "Method";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            SGAccessibilityLevel newAccessibilityLevel = null;
            SGMethodSignature method = new SGMethodSignature(methodName, accessibilityLevel);

            // Act
            method.AccessibilityLevel = newAccessibilityLevel;
        }

        [TestMethod]
        public void TestAccessibilityLevelSet_FluentAPI()
        {
            // Arrange
            string methodName = "Method";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;

            // Act
            methodName = new SGMethodSignature(methodName).WithAccessibilityLevel(accessibilityLevel);

            // Assert
            Assert.AreEqual(newAccessibilityLevel, method.AccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAccessibilityLevelSetNull_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string methodName = "Method";
            SGAccessibilityLevel accessibilityLevel = null;

            // Act
            methodName = new SGMethodSignature(methodName).WithAccessibilityLevel(accessibilityLevel);
        }
    }
}
