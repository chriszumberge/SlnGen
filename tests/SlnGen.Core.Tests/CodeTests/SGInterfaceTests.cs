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
        [ExpectedException(typeof(Exception))]
        public void TestNamespaceNameSetEmpty_ThrowsException()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(interfaceName);

            // Act
            @interface.InterfaceName = String.Empty;
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(Exception))]
        public void TestNamespaceNameSetNull_ThrowsException()
        {
            // Arrange
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(interfaceName);

            // Act
            @interface.InterfaceName = null;
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
        [ExpectedException(typeof(Exception))]
        public void TestAddNullInterfaceImplementation_ThrowsException()
        {
            // Arrange
            string interfaceName = "IInterface";
            string interfaceImplementation = null;

            // Act
            SGInterface @interface = new SGInterface(interfaceName).WithImplementations(interfaceImplementation);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(Exception))]
        public void TestAddEmptyInterfaceImplementation_ThrowsException()
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
        [ExpectedException(typeof(Exception))]
        public void TestAddNullGeneric_ThrowsException()
        {
            // Arrange
            string interfaceName = "IInterface";
            string genericTypeName = null;

            // Act
            SGInterface @interface = new SGInterface(interfaceName).WithGenericTypeNames(genericTypeName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(Exception))]
        public void TestAddEmptyGeneric_ThrowsException()
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
    }
}
