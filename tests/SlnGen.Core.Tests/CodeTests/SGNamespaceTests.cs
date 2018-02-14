using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnGen.Core.Code;
using System.Collections.Generic;
using SlnGen.Core.Utils;

namespace SlnGen.Core.Tests.CodeTests
{
    [TestClass]
    public class SGNamespaceTests
    {
        [TestMethod]
        public void TestNamespaceNameCtor_InitsFields()
        {
            // Arrange
            string namespaceName = "SlnGen";

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName);

            // Assert
            Assert.AreEqual(namespaceName, @namespace.NamespaceName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullNamespaceName_ThrowsArgumentNullException()
        {
            // Arrange
            string namespaceName = null;

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyNamespaceName_ThrowsArgumentException()
        {
            // Arrange
            string namespaceName = String.Empty;

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName);
        }

        [TestMethod]
        public void TestNamespaceNameWithSpaces_ReplacedWithUnderscores()
        {
            // Arrange
            string namespaceName = "SlnGen Core Code";

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName);

            // Assert
            Assert.AreEqual(namespaceName.Replace(" ", "_"), @namespace.NamespaceName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestNamespaceNameSetEmtpy_ThrowsArgumentException()
        {
            // Arrange
            string namespaceName = "SlnGen";
            SGNamespace @namespace = new SGNamespace(namespaceName);

            // Act
            @namespace.NamespaceName = String.Empty;
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNamespaceNameSetNull_ThrowsArgumentNullException()
        {
            // Arrange
            string namespaceName = "SlnGen";
            SGNamespace @namespace = new SGNamespace(namespaceName);

            // Act
            @namespace.NamespaceName = null;
        }

        [TestMethod]
        public void TestAddingInterface_FluentAPI()
        {
            // Arrange
            string namespaceName = "SlnGen";
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(interfaceName);

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName).WithInterfaces(@interface);

            // Assert
            Assert.IsTrue(@namespace.Interfaces.Contains(@interface));
        }

        [TestMethod]
        public void TestAddingMultipleInterfaces_FluentAPI()
        {
            // Arrange
            string namespaceName = "SlnGen";
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(interfaceName);
            SGInterface interface2 = new SGInterface(interfaceName);

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName).WithInterfaces(@interface, interface2);

            // Assert
            Assert.IsTrue(@namespace.Interfaces.ContainsAll(new List<SGInterface>
            {
                @interface,
                interface2
            }));
        }

        [TestMethod]
        public void TestAddingInterfaces_PropertyInitializer()
        {
            // Arrange
            string namespaceName = "SlnGen";
            string interfaceName = "IInterface";
            SGInterface @interface = new SGInterface(interfaceName);
            SGInterface interface2 = new SGInterface(interfaceName);

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName)
            {
                Interfaces =
                {
                    @interface,
                    interface2
                }
            };

            // Assert
            Assert.IsTrue(@namespace.Interfaces.ContainsAll(new List<SGInterface>
            {
                @interface,
                interface2
            }));
        }

        [TestMethod]
        public void TestAddingClass_FluentAPI()
        {
            // Arrange
            string namespaceName = "SlnGen";
            SGClass @class = new SGClass();

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName).WithClasses(@class);

            // Assert
            Assert.IsTrue(@namespace.Classes.Contains(@class));
        }

        [TestMethod]
        public void TestAddingMultipleClasses_FluentAPI()
        {
            // Arrange
            string namespaceName = "SlnGen";
            SGClass @class = new SGClass();
            SGClass class2 = new SGClass();

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName).WithClasses(@class, class2);

            // Assert
            Assert.IsTrue(@namespace.Classes.ContainsAll(@class, class2));
        }

        [TestMethod]
        public void TestAddingClasses_PropertyInitializer()
        {
            // Arrange
            string namespaceName = "SlnGen";
            SGClass @class = new SGClass();
            SGClass class2 = new SGClass();

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName)
            {
                Classes =
                {
                    @class,
                    class2
                }
            };

            // Assert
            Assert.IsTrue(@namespace.Classes.ContainsAll(@class, class2));
        }

        [TestMethod]
        public void TestAddingEnum_FluentAPI()
        {
            // Arrange
            string namespaceName = "SlnGen";
            SGEnum @enum = new SGEnum();

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName).WithEnums(@enum);

            // Assert
            Assert.IsTrue(@namespace.Enums.Contains(@enum));
        }

        [TestMethod]
        public void TestAddingMultipleEnums_FluentAPI()
        {
            // Arrange
            string namespaceName = "SlnGen";
            SGEnum @enum = new SGEnum();
            SGEnum enum2 = new SGEnum();

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName).WithEnums(@enum, enum2);

            // Assert
            Assert.IsTrue(@namespace.Enums.ContainsAll(@enum, enum2));
        }

        [TestMethod]
        public void TestAddingEnums_PropertyInitializer()
        {
            // Arrange
            string namespaceName = "SlnGen";
            SGEnum @enum = new SGEnum();
            SGEnum enum2 = new SGEnum();

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName)
            {
                Enums =
                {
                    @enum,
                    enum2
                }
            };

            // Assert
            Assert.IsTrue(@namespace.Enums.ContainsAll(@enum, enum2));
        }

        [TestMethod]
        public void TestAddingStruct_FluentAPI()
        {
            // Arrange
            string namespaceName = "SlnGen";
            SGStruct @struct = new SGStruct();

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName).WithStructs(@struct);

            // Assert
            Assert.IsTrue(@namespace.Structs.Contains(@struct));
        }

        [TestMethod]
        public void TestAddingMultipleStructs_FluentAPI()
        {
            // Arrange
            string namespaceName = "SlnGen";
            SGStruct @struct = new SGStruct();
            SGStruct struct2 = new SGStruct();

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName).WithStructs(@struct, struct2);

            // Assert
            Assert.IsTrue(@namespace.Structs.ContainsAll(@struct, struct2));
        }

        [TestMethod]
        public void TestAddingStructs_PropertyInitializer()
        {
            // Arrange
            string namespaceName = "SlnGen";
            SGStruct @struct = new SGStruct();
            SGStruct struct2 = new SGStruct();

            // Act
            SGNamespace @namespace = new SGNamespace(namespaceName)
            {
                Structs =
                {
                    @struct,
                    struct2
                }
            };

            // Assert
            Assert.IsTrue(@namespace.Structs.ContainsAll(@struct, struct2));
        }
    }
}
