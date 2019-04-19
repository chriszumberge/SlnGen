using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnGen.Core.Code;
using SlnGen.Core.Utils;

namespace SlnGen.Core.Tests.CodeTests
{
    [TestClass]
    public class SGInterfaceTests
    {
        [TestMethod]
        public void TestInterfaceCtor_NameAccessibility_InitsFields()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;

            // Act
            SGInterface @interface = new SGInterface(accessibilityLevel, interfaceName);

            // Assert
            Assert.AreEqual(interfaceName, @interface.InterfaceName);
            Assert.AreEqual(accessibilityLevel, @interface.AccessibilityLevel);
        }

        [TestMethod]
        public void TestInterfaceCtor_Name_InitsFields()
        {
            // Arrange
            string interfaceName = "IInterface";

            // Act
            SGInterface @interface = new SGInterface(interfaceName);

            // Assert
            Assert.AreEqual(interfaceName, @interface.InterfaceName);
            Assert.AreEqual(SGAccessibilityLevel.Private, @interface.AccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullInterfaceName_ThrowsArgumentNullException()
        {
            // Arrange
            string interfaceName = null;
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;

            // Act
            SGInterface @interface = new SGInterface(accessibilityLevel, interfaceName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyInterfaceName_ThrowsArgumentException()
        {
            // Arrange
            string interfaceName = String.Empty;
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;

            // Act
            SGInterface @interface = new SGInterface(accessibilityLevel, interfaceName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullInterfaceNameNoAccessibility_ThrowsArgumentNullException()
        {
            // Arrange
            string interfaceName = null;

            // Act
            SGInterface @interface = new SGInterface(interfaceName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyInterfaceNameNoAccessibility_ThrowsArgumentException()
        {
            // Arrange
            string interfaceName = String.Empty;

            // Act
            SGInterface @interface = new SGInterface(interfaceName);
        }

        [TestMethod]
        public void TestInterfaceNameWithSpaces_ReplacedWithUnderscores()
        {
            // Arrange
            string interfaceName = "ISome Interface Name";

            // Act
            SGInterface @interface = new SGInterface(interfaceName);

            // Assert
            Assert.AreEqual(interfaceName.Replace(" ", "_"), @interface.InterfaceName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestNamespaceNameSetEmpty_ThrowsArgumentException()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(interfaceName);

            // Act
            @interface.InterfaceName = String.Empty;
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNamespaceNameSetNull_ThrowsArgumentNullException()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(interfaceName);

            // Act
            @interface.InterfaceName = null;
        }

        [TestMethod]
        public void TestNamespaceNameWithSpaces_ReplacedWithUnderscores()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(interfaceName);
            string newInterfaceName = "Some Interface Name";

            // Act
            @interface.InterfaceName = newInterfaceName;

            // Assert
            Assert.AreEqual(newInterfaceName.Replace(" ", "_"), @interface.InterfaceName);
        }

        [TestMethod]
        public void TestAccessibilityLevelSet_FluentAPI()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(SGAccessibilityLevel.Private, interfaceName);

            // Act
            @interface = @interface.WithAccessibilityLevel(SGAccessibilityLevel.Public);

            // Assert
            Assert.AreEqual(SGAccessibilityLevel.Public, @interface.AccessibilityLevel);
        }

        [TestMethod]
        public void TestAccessibilityLevelSet_Property()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(SGAccessibilityLevel.Private, interfaceName);

            // Act
            @interface.AccessibilityLevel = SGAccessibilityLevel.Public;

            // Assert
            Assert.AreEqual(SGAccessibilityLevel.Public, @interface.AccessibilityLevel);
        }

        [TestMethod]
        public void TestAccessibilityLevelSet_PropertyInitializer()
        {
            // Arrange
            string interfaceName = "IInterface";

            // Act
            SGInterface @interface = new SGInterface(interfaceName)
            {
                AccessibilityLevel = SGAccessibilityLevel.Public
            };

            // Assert
            Assert.AreEqual(SGAccessibilityLevel.Public, @interface.AccessibilityLevel);
        }

        [TestMethod]
        public void TestAddInterfaceImplementation_AsString_FluentAPI()
        {
            // Arrange
            string interfaceName = "IInterface";
            string implementedInterface1 = "IImp";
            string implementedInterface2 = "IImp2";

            // Act
            SGInterface @interface = new SGInterface(interfaceName).WithImplementations(implementedInterface1, implementedInterface2);

            // Assert
            Assert.IsTrue(@interface.InterfaceImplementations.ContainsAll(implementedInterface1, implementedInterface2));
        }

        [TestMethod]
        public void TestAddInterfaceImplementation_AsSGinterface_FluentAPI()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGInterface if1 = new SGInterface("IImp");
            SGInterface if2 = new SGInterface("IImp2");

            // Act
            SGInterface @interface = new SGInterface(interfaceName).WithImplementations(if1, if2);

            // Assert
            Assert.IsTrue(@interface.InterfaceImplementations.ContainsAll(if1.InterfaceName, if2.InterfaceName));
        }

        [TestMethod]
        public void TestAddInterfaceImplementation_AsString__PropertyInitializer()
        {
            // Arrange
            string interfaceName = "IInterface";
            string implementedInterface1 = "IImp";
            string implementedInterface2 = "IImp2";

            // Act
            SGInterface @interface = new SGInterface(interfaceName)
            {
                InterfaceImplementations =
                {
                    implementedInterface1,
                    implementedInterface2
                }
            };

            // Assert
            Assert.IsTrue(@interface.InterfaceImplementations.ContainsAll(implementedInterface1, implementedInterface2));
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddNullInterfaceImplementation_ThrowsArgumentException()
        {
            // Arrange
            string interfaceName = "IInterface";
            string interfaceImplementation = null;

            // Act
            SGInterface @interface = new SGInterface(interfaceName).WithImplementations(interfaceImplementation);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddEmptyInterfaceImplementation_ThrowsArgumentException()
        {
            // Arrange
            string interfaceName = "IInterface";
            string interfaceImplementation = String.Empty;

            // Act
            SGInterface @interface = new SGInterface(interfaceName).WithImplementations(interfaceImplementation);
        }

        [TestMethod]
        public void TestAddGeneric_FluentAPI()
        {
            // Arrange
            string interfaceName = "IInterface";
            string genericTypeName = "T";
            string genericTypeName2 = "U";

            // Act
            SGInterface @interface = new SGInterface(interfaceName).WithGenericTypeNames(genericTypeName, genericTypeName2);

            // Assert
            Assert.IsTrue(@interface.GenericTypeNames.ContainsAll(genericTypeName, genericTypeName2));
        }
        
        [TestMethod]
        public void TestAddGeneric_PropertyInitializer()
        {
            // Arrange
            string interfaceName = "IInterface";
            string genericTypeName = "T";
            string genericTypeName2 = "U";

            // Act
            SGInterface @interface = new SGInterface(interfaceName)
            {
                GenericTypeNames =
                {
                    genericTypeName,
                    genericTypeName2
                }
            };

            // Assert
            Assert.IsTrue(@interface.GenericTypeNames.ContainsAll(genericTypeName, genericTypeName2));
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddNullGeneric_ThrowsArgumentException()
        {
            // Arrange
            string interfaceName = "IInterface";
            string genericTypeName = null;

            // Act
            SGInterface @interface = new SGInterface(interfaceName).WithGenericTypeNames(genericTypeName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddEmptyGeneric_ThrowsArgumentException()
        {
            // Arrange
            string interfaceName = "IInterface";
            string genericTypeName = String.Empty;

            // Act
            SGInterface @interface = new SGInterface(interfaceName).WithGenericTypeNames(genericTypeName);
        }

        [TestMethod]
        public void TestIsGenericFlag_FalseWhenNotGeneric()
        {
            // Arrange
            string interfaceName = "IInterface";

            // Act
            SGInterface @interface = new SGInterface(interfaceName);

            // Assert
            Assert.IsFalse(@interface.IsGeneric);
        }

        [TestMethod]
        public void TestIsGenericFlag_TrueWhenGeneric()
        {
            // Arrange
            string interfaceName = "IInterface";
            string genericTypeName = "T";

            // Act
            SGInterface @interface = new SGInterface(interfaceName).WithGenericTypeNames(genericTypeName);

            // Assert
            Assert.IsTrue(@interface.IsGeneric);
        }

        [TestMethod]
        public void TestIsGenericFlag_FalseWhenSetNull()
        {
            // Arrange
            string interfaceName = "IInterface";
            string genericTypeName = "T";
            SGInterface @interface = new SGInterface(interfaceName).WithGenericTypeNames(genericTypeName);

            // Act
            @interface.GenericTypeNames = null;

            // Assert
            Assert.IsFalse(@interface.IsGeneric);
        }

        [TestMethod]
        public void TestAddMethodSignatures_FluentAPI()
        {
            // Arrange
            string interfaceName = "IInterface";
            string methodName1 = "Method1";
            string methodName2 = "Method2";
            SGInterface @interface = new SGInterface(interfaceName);
            SGMethodSignature method1 = new SGMethodSignature(methodName1);
            SGMethodSignature method2 = new SGMethodSignature(methodName2);

            // Act
            @interface = @interface.WithMethodSignatures(method1, method2);

            // Assert
            Assert.IsTrue(@interface.MethodSignatures.ContainsAll(method1, method2));
        }

        [TestMethod]
        public void TestAddMethodSignatures_PropertyInitializer()
        {
            // Arrange
            string interfaceName = "IInterface";
            string methodName1 = "Method1";
            string methodName2 = "Method2";
            SGMethodSignature method1 = new SGMethodSignature(methodName1);
            SGMethodSignature method2 = new SGMethodSignature(methodName2);

            // Act
            SGInterface @interface = new SGInterface(interfaceName)
            {
                MethodSignatures =
                {
                    method1,
                    method2
                }
            };

            // Assert
            Assert.IsTrue(@interface.MethodSignatures.ContainsAll(method1, method2));
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddNullMethodSignature_ThrowsArgumentException()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(interfaceName);

            // Act
            @interface = @interface.WithMethodSignatures(null);
        }

        [TestMethod]
        public void TestAddAttributes_FluentAPI()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(interfaceName);
            string attr1Name = "Attr1";
            string attr2Name = "Attr2";
            SGAttribute attr1 = new SGAttribute(attr1Name);
            SGAttribute attr2 = new SGAttribute(attr2Name);

            // Act
            @interface = @interface.WithAttributes(attr1, attr2);

            // Assert
            Assert.IsTrue(@interface.Attributes.ContainsAll(attr1, attr2));
        }

        [TestMethod]
        public void TestAddAttributes_PropertyInitializer()
        {
            // Arrange
            string interfaceName = "IInterface";
            string attr1Name = "Attr1";
            string attr2Name = "Attr2";
            SGAttribute attr1 = new SGAttribute(attr1Name);
            SGAttribute attr2 = new SGAttribute(attr2Name);

            // Act
            SGInterface @interface = new SGInterface(interfaceName)
            {
                Attributes =
                {
                    attr1,
                    attr2
                }
            };

            // Assert
            Assert.IsTrue(@interface.Attributes.ContainsAll(attr1, attr2));
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddNullAttribute_ThrowsArgumentException()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(interfaceName);

            // Act
            @interface = @interface.WithAttributes(null);
        }
    }
}
