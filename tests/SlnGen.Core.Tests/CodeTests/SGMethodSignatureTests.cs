using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnGen.Core.Code;
using SlnGen.Core.Utils;

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

        //[TestMethod]
        //public void TestMethodCtor_TypedReturnType_InitsFields()
        //{
        //    // Arrange
        //    string methodName = "Method";
        //    Type returnType = typeof(int);

        //    // Act
        //    SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);

        //    // Assert
        //    Assert.AreEqual(returnType.Name, method.ReturnType);
        //}

        //[TestMethod]
        //public void TestMethodCtor_All_with_TypedReturnType_InitsFields()
        //{
        //    // Arrange
        //    string methodName = "Method";
        //    SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
        //    Type returnType = typeof(int);
        //    bool @static = true;
        //    bool @async = true;
        //    bool @override = true;

        //    // Act
        //    SGMethodSignature method = new SGMethodSignature(methodName, accessibilityLevel, @static, @async, @override, returnType);

        //    // Assert
        //    Assert.AreEqual(methodName, method.MethodName);
        //    Assert.AreEqual(accessibilityLevel, method.AccessibilityLevel);
        //    Assert.AreEqual(returnType.Name, method.ReturnType);
        //    Assert.AreEqual(@static, method.IsStatic);
        //    Assert.AreEqual(async, method.IsAsync);
        //    Assert.AreEqual(@override, method.IsOverride);
        //}

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
        public void TestMethodCtor_NullStringReturnType_SetsToDefault()
        {
            // Arrange
            string methodName = "Method";
            string returnType = null;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);

            // Assert
            Assert.AreEqual("void", method.ReturnType);
        }

        [TestMethod]
        public void TestMethodCtor_EmptyStringReturnType_SetsToDefault()
        {
            // Arrange
            string methodName = "Method";
            string returnType = String.Empty;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);

            // Assert
            Assert.AreEqual("void", method.ReturnType);
        }

        //[TestMethod]
        //public void TestMethodCtor_NullTypeReturnType_SetsToDefault()
        //{
        //    // Arrange
        //    string methodName = "Method";
        //    Type returnType = null;

        //    // Act
        //    SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);

        //    // Assert
        //    Assert.AreEqual("void", method.ReturnType);
        //}

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
            SGMethodSignature method = new SGMethodSignature(methodName).WithAccessibilityLevel(accessibilityLevel);

            // Assert
            Assert.AreEqual(accessibilityLevel, method.AccessibilityLevel);
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
            SGMethodSignature method = new SGMethodSignature(methodName).WithAccessibilityLevel(accessibilityLevel);
        }

        [TestMethod]
        public void TestReturnTypeSet_Property()
        {
            // Arrange
            string methodName = "Method";
            string returnType = "void";
            string newReturnType = "int";
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);

            // Act
            method.ReturnType = newReturnType;

            // Assert
            Assert.AreEqual(newReturnType, method.ReturnType);
        }

        [TestMethod]
        public void TestReturnTypeSetNull_Property_DefaultsToVoid()
        {
            // Arrange
            string methodName = "Method";
            string returnType = "int";
            string newReturnType = null;
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);

            // Act
            method.ReturnType = newReturnType;

            // Assert
            Assert.AreEqual("void", method.ReturnType);
        }

        [TestMethod]
        public void TestReturnTypeSetEmpty_Property_DefaultsToVoid()
        {
            // Arrange
            string methodName = "Method";
            string returnType = "int";
            string newReturnType = String.Empty;
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);

            // Act
            method.ReturnType = newReturnType;

            // Assert
            Assert.AreEqual("void", method.ReturnType);
        }

        [TestMethod]
        public void TestReturnTypeSet_FluentAPI()
        {
            // Arrange
            string methodName = "Method";
            string returnType = "void";
            string newReturnType = "int";
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);

            // Act
            method = method.WithReturnType(newReturnType);

            // Assert
            Assert.AreEqual(newReturnType, method.ReturnType);
        }

        [TestMethod]
        public void TestReturnTypeSetNull_FluentAPI_DefaultsToVoid()
        {
            // Arrange
            string methodName = "Method";
            string returnType = "int";
            string newReturnType = null;
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);

            // Act
            method = method.WithReturnType(newReturnType);

            // Assert
            Assert.AreEqual("void", method.ReturnType);
        }

        [TestMethod]
        public void TestReturnTypeSetEmpty_FluentAPI_DefaultsToVoid()
        {
            // Arrange
            string methodName = "Method";
            string returnType = "int";
            string newReturnType = String.Empty;
            SGMethodSignature method = new SGMethodSignature(methodName, returnType: returnType);

            // Act
            method = method.WithReturnType(newReturnType);

            // Assert
            Assert.AreEqual("void", method.ReturnType);
        }

        [TestMethod]
        public void TestReturnTypeSet_FluentAPI_SystemType()
        {
            // Arrange
            string methodName = "Method";
            Type newReturnType = typeof(int);
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method = method.WithReturnType(newReturnType);

            // Assert
            Assert.AreEqual(newReturnType.Name, method.ReturnType);
        }

        [TestMethod]
        public void TestSetStaticFlag_PropertyInitializer()
        {
            // Arrange
            string methodName = "Method";

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName)
            {
                IsStatic = true
            };

            // Assert
            Assert.IsTrue(method.IsStatic);
        }

        [TestMethod]
        public void TestSetStaticFlag_PropertySetter()
        {
            // Arrange
            string methodName = "Method";
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method.IsStatic = true;

            // Assert
            Assert.IsTrue(method.IsStatic);
        }

        [TestMethod]
        public void TestSetStaticFlag_FluentAPI()
        {
            // Arrange
            string methodName = "Method";
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method = method.WithIsStatic(true);

            // Assert
            Assert.IsTrue(method.IsStatic);
        }

        [TestMethod]
        public void TestSetAsyncFlag_PropertyInitializer()
        {
            // Arrange
            string methodName = "Method";

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName)
            {
                IsAsync = true
            };

            // Assert
            Assert.IsTrue(method.IsAsync);
        }

        [TestMethod]
        public void TestSetAsyncFlag_PropertySetter()
        {
            // Arrange
            string methodName = "Method";
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method.IsAsync = true;

            // Assert
            Assert.IsTrue(method.IsAsync);
        }

        [TestMethod]
        public void TestSetAsyncFlag_FluentAPI()
        {
            // Arrange
            string methodName = "Method";
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method = method.WithIsAsync(true);

            // Assert
            Assert.IsTrue(method.IsAsync);
        }

        [TestMethod]
        public void TestSetOverrideFlag_PropertyInitializer()
        {
            // Arrange
            string methodName = "Method";

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName)
            {
                IsOverride = true
            };

            // Assert
            Assert.IsTrue(method.IsOverride);
        }

        [TestMethod]
        public void TestSetOverrideFlag_PropertySetter()
        {
            // Arrange
            string methodName = "Method";
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method.IsOverride = true;

            // Assert
            Assert.IsTrue(method.IsOverride);
        }

        [TestMethod]
        public void TestSetOverrideFlag_FluentAPI()
        {
            // Arrange
            string methodName = "Method";
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method = method.WithIsOverride(true);

            // Assert
            Assert.IsTrue(method.IsOverride);
        }

        [TestMethod]
        public void TestAddGeneric_FluentAPI()
        {
            // Arrange
            string methodName = "Method";
            string genericTypeName1 = "T";
            string genericTypeName2 = "U";

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName).WithGenericTypeNames(genericTypeName1, genericTypeName2);

            // Assert
            Assert.IsTrue(method.GenericTypeNames.ContainsAll(genericTypeName1, genericTypeName2));
        }

        [TestMethod]
        public void TestAddGeneric_PropertyInitializer()
        {
            // Arrange
            string methodName = "Method";
            string genericTypeName1 = "T";
            string genericTypeName2 = "U";

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName)
            {
                GenericTypeNames =
                {
                    genericTypeName1,
                    genericTypeName2
                }
            };

            // Assert
            Assert.IsTrue(method.GenericTypeNames.ContainsAll(genericTypeName1, genericTypeName2));
        }

        [TestMethod]
        public void TestSetGenericNull_EmptyList()
        {
            // Arrange
            string methodName = "Method";
            string genericTypeName1 = "T";
            string genericTypeName2 = "U";
            SGMethodSignature method = new SGMethodSignature(methodName)
            {
                GenericTypeNames =
                {
                    genericTypeName1,
                    genericTypeName2
                }
            };


            // Act
            method.GenericTypeNames = null;

            // Assert
            Assert.IsNotNull(method.GenericTypeNames);
            Assert.AreEqual(0, method.GenericTypeNames.Count);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddNullGeneric_ThrowsArgumentException()
        {
            // Arrange
            string methodName = "Method";
            string genericTypeName = null;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName).WithGenericTypeNames(genericTypeName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddEmptyGeneric_ThrowsArgumentException()
        {
            // Arrange
            string methodName = "Method";
            string genericTypeName = String.Empty;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName).WithGenericTypeNames(genericTypeName);
        }

        [TestMethod]
        public void TestIsGenericFlag_FalseWhenNotGeneric()
        {
            // Arrange
            string methodName = "Method";

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Assert
            Assert.IsFalse(method.IsGeneric);
        }

        [TestMethod]
        public void TestIsGenericFlag_TrueWhenGeneric()
        {
            // Arrange
            string methodName = "Method";
            string genericTypeName = "T";

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName).WithGenericTypeNames(genericTypeName);

            // Assert
            Assert.IsTrue(method.IsGeneric);
        }

        [TestMethod]
        public void TestIsGenericFlag_FalseWhenSetNull()
        {
            // Arrange
            string methodName = "Method";
            string genericTypeName = "T";
            SGMethodSignature method = new SGMethodSignature(methodName).WithGenericTypeNames(genericTypeName);

            // Act
            method.GenericTypeNames = null;

            // Assert
            Assert.IsFalse(method.IsGeneric);
        }

        [TestMethod]
        public void TestAddArguments_FluentAPI()
        {
            // Arrange
            string methodName = "Method";
            SGArgument arg1 = new SGArgument("arg1", "int");
            SGArgument arg2 = new SGArgument("arg2", "bool");
            SGMethodSignature method = new SGMethodSignature(methodName);

            // Act
            method = method.WithArguments(arg1, arg2);

            // Assert
            Assert.IsTrue(method.Arguments.ContainsAll(arg1, arg2));
        }

        [TestMethod]
        public void TestAddArguments_PropertyInitializer()
        {
            // Arrange
            string methodName = "Method";
            SGArgument arg1 = new SGArgument("arg1", "int");
            SGArgument arg2 = new SGArgument("arg2", "bool");

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName)
            {
                Arguments =
                {
                    arg1,
                    arg2
                }
            };

            // Assert
            Assert.IsTrue(method.Arguments.ContainsAll(arg1, arg2));
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddNullArgument_ThrowsArgumentException()
        {
            // Arrange
            string methodName = "Method";
            SGArgument arg = null;

            // Act
            SGMethodSignature method = new SGMethodSignature(methodName).WithArguments(arg);
        }

        [TestMethod]
        public void TestSetArgumentsNull_EmptyList()
        {
            // Arrange
            string methodName = "Method";
            SGArgument arg1 = new SGArgument("arg1", "int");
            SGArgument arg2 = new SGArgument("arg2", "bool");
            SGMethodSignature method = new SGMethodSignature(methodName)
            {
                Arguments = { arg1, arg2 }
            };


            // Act
            method.Arguments = null;

            // Assert
            Assert.IsNotNull(method.Arguments);
            Assert.AreEqual(0, method.Arguments.Count);
        }
    }
}
