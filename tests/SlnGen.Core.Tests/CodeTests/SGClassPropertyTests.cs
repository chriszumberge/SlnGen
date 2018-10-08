using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnGen.Core.Code;

namespace SlnGen.Core.Tests.CodeTests
{
    [TestClass]
    public class SGClassPropertyTests
    {
        [TestMethod]
        public void TestPropertyCtor_InitsFields()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            bool @static = true;
            SGAccessibilityLevel getterAccessibilityLevel = SGAccessibilityLevel.Protected;
            SGAccessibilityLevel setterAccessibilityLevel = SGAccessibilityLevel.Internal;

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType, accessibilityLevel, @static, getterAccessibilityLevel, setterAccessibilityLevel);

            // Assert
            Assert.AreEqual(propertyName, property.PropertyName);
            Assert.AreEqual(propertyType, property.PropertyType);
            Assert.AreEqual(accessibilityLevel, property.AccessibilityLevel);
            Assert.AreEqual(@static, property.IsStatic);
            Assert.AreEqual(getterAccessibilityLevel, property.GetterAccessibilityLevel);
            Assert.AreEqual(setterAccessibilityLevel, property.SetterAccessibilityLevel);
        }

        [TestMethod]
        public void TestPropertyCtor_Defaults_InitsFields()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Assert
            Assert.AreEqual(propertyName, property.PropertyName);
            Assert.AreEqual(propertyType, property.PropertyType);
            Assert.AreEqual(SGAccessibilityLevel.Private, property.AccessibilityLevel);
            Assert.IsFalse(property.IsStatic);
            Assert.AreEqual(SGAccessibilityLevel.None, property.GetterAccessibilityLevel);
            Assert.AreEqual(SGAccessibilityLevel.None, property.SetterAccessibilityLevel);
        }

        [TestMethod]
        public void TestPropertyCtor_TypedPropertyType_InitsFields()
        {
            // Arrange
            string propertyName = "property";
            Type propertyType = typeof(int);

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Assert
            Assert.AreEqual(propertyType.Name, property.PropertyType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPropertyCtor_NullPropertyName_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = null;
            string propertyType = "int";

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestPropertyCtor_EmptyPropertyName_ThrowsArgumentException()
        {
            // Arrange
            string propertyName = String.Empty;
            string propertyType = "int";

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);
        }

        [TestMethod]
        public void TestPropertyCtor_PropertyNameWithSpaces_ReplacedWithUnderscores()
        {
            // Arrange
            string propertyName = "Some Property Name";
            string propertyType = "int";

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Assert
            Assert.AreEqual(propertyName.Replace(" ", "_"), property.PropertyName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPropertyCtor_NullPropertyType_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = null;

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPropertyCtor_NullTypedPropertyType_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            Type propertyType = null;

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestPropertyCtor_EmptyPropertyType_ThrowsArgumentException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = String.Empty;

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);
        }

        [TestMethod]
        public void TestPropertyNameSet()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyName = "newProperty";
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.PropertyName = newPropertyName;

            // Assert
            Assert.AreEqual(newPropertyName, property.PropertyName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPropertyNameSetNull_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyName = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.PropertyName = newPropertyName;
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestPropertyNameSetEmpty_ThrowsArgumentException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyName = String.Empty;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.PropertyName = newPropertyName;
        }

        [TestMethod]
        public void TestPropertyNameSet_WithSpaces_ReplacedWithUnderscores()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyName = "Some Property Name";
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.PropertyName = newPropertyName;

            // Assert
            Assert.AreEqual(newPropertyName.Replace(" ", "_"), property.PropertyName);
        }

        [TestMethod]
        public void TestPropertyNameSet_FluentAPI()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyName = "newProperty";
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithPropertyName(newPropertyName);

            // Assert
            Assert.AreEqual(newPropertyName, property.PropertyName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPropertyNameSetNull_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyName = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithPropertyName(newPropertyName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestPropertyNameSetEmpty_FluentAPI_ThrowsArgumentException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyName = String.Empty;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithPropertyName(newPropertyName);
        }

        [TestMethod]
        public void TestPropertyNameSet_WithSpaces_FluentAPI_ReplacedWithUnderscores()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyName = "Some Property Name";
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithPropertyName(newPropertyName);

            // Assert
            Assert.AreEqual(newPropertyName.Replace(" ", "_"), property.PropertyName);
        }

        [TestMethod]
        public void TestPropertyTypeSet_Property()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyType = "bool";
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.PropertyType = newPropertyType;

            // Assert
            Assert.AreEqual(newPropertyType, property.PropertyType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPropertyTypeSetNull_Property_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyType = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.PropertyType = newPropertyType;
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestPropertyTypeSetEmpty_Property_ThrowsArgumentException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyType = String.Empty;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.PropertyType = newPropertyType;
        }

        [TestMethod]
        public void TestPropertyTypeSet_FluentAPI()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyType = "bool";
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithPropertyType(newPropertyType);

            // Assert
            Assert.AreEqual(newPropertyType, property.PropertyType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPropertyTypeSetNull_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyType = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithPropertyType(newPropertyType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestPropertyTypeSetEmpty_FluentAPI_ThrowsArgumentException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string newPropertyType = String.Empty;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithPropertyType(newPropertyType);
        }

        [TestMethod]
        public void TestPropertyTypeSet_WithSystemType_FluentAPI()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            Type newPropertyType = typeof(bool);
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithPropertyType(newPropertyType);

            // Assert
            Assert.AreEqual(newPropertyType.Name, property.PropertyType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPropertyTypeSetNull_WithSystemType_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            Type newPropertyType = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithPropertyType(newPropertyType);
        }

        [TestMethod]
        public void TestAccessibilityLevelSet_Property()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            SGAccessibilityLevel newAccessibilityLevel = SGAccessibilityLevel.Protected;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.AccessibilityLevel = newAccessibilityLevel;

            // Assert
            Assert.AreEqual(newAccessibilityLevel, property.AccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAccessibilityLevelSetNull_Property_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            SGAccessibilityLevel newAccessibilityLevel = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.AccessibilityLevel = newAccessibilityLevel;
        }

        [TestMethod]
        public void TestAccessibilityLevelSet_PropertyInitializer()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType)
            {
                AccessibilityLevel = accessibilityLevel
            };

            // Assert
            Assert.AreEqual(accessibilityLevel, property.AccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAccessibilityLevelSetNull_PropertyInitializer_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel accessibilityLevel = null;

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType)
            {
                AccessibilityLevel = accessibilityLevel
            };
        }

        [TestMethod]
        public void TestAccessibilityLevelSet_FluentAPI()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            SGAccessibilityLevel newAccessibilityLevel = SGAccessibilityLevel.Protected;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithAccessibilityLevel(newAccessibilityLevel);

            // Assert
            Assert.AreEqual(newAccessibilityLevel, property.AccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAccessibilityLevelSetNull_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            SGAccessibilityLevel newAccessibilityLevel = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithAccessibilityLevel(newAccessibilityLevel);
        }

        [TestMethod]
        public void TestSetStaticFlag_PropertyInitializer()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            bool isStatic = true;

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType)
            {
                IsStatic = isStatic
            };

            // Assert
            Assert.IsTrue(isStatic);
        }

        [TestMethod]
        public void TestSetStaticFlag_PropertySetter()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            bool isStatic = true;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.IsStatic = isStatic;

            // Assert
            Assert.IsTrue(isStatic);
        }

        [TestMethod]
        public void TestSetStaticFlag_FluentAPI()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            bool isStatic = true;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithIsStatic(isStatic);

            // Assert
            Assert.IsTrue(isStatic);
        }

        [TestMethod]
        public void TestSetGetterAccessibilityLevel_PropertyInitializer()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel getterAccessLevel = SGAccessibilityLevel.Public;

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType)
            {
                GetterAccessibilityLevel = getterAccessLevel
            };

            // Assert
            Assert.AreEqual(getterAccessLevel, property.GetterAccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetGetterAccessibilityLevelNull_PropertyInitializer_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel getterAccessLevel = null;

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType)
            {
                GetterAccessibilityLevel = getterAccessLevel
            };
        }

        [TestMethod]
        public void TestSetGetterAccessibilityLevel_Property()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel getterAccessLevel = SGAccessibilityLevel.Public;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.GetterAccessibilityLevel = getterAccessLevel;

            // Assert
            Assert.AreEqual(getterAccessLevel, property.GetterAccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetGetterAccessibilityLevelNull_Property_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel getterAccessLevel = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.GetterAccessibilityLevel = getterAccessLevel;
        }

        [TestMethod]
        public void TestSetGetterAccessibilityLevel_FluentAPI()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel getterAccessLevel = SGAccessibilityLevel.Public;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithGetterAccessibilityLevel(getterAccessLevel);

            // Assert
            Assert.AreEqual(getterAccessLevel, property.GetterAccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetGetterAccessibilityLevelNull_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel getterAccessLevel = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithGetterAccessibilityLevel(getterAccessLevel);
        }

        [TestMethod]
        public void TestSetSetterAccessibilityLevel_PropertyInitializer()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel setterAccessLevel = SGAccessibilityLevel.Public;

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType)
            {
                SetterAccessibilityLevel = setterAccessLevel
            };

            // Assert
            Assert.AreEqual(setterAccessLevel, property.SetterAccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetSetterAccessibilityLevelNull_PropertyInitializer_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel setterAccessLevel = null;

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType)
            {
                SetterAccessibilityLevel = setterAccessLevel
            };
        }

        [TestMethod]
        public void TestSetSetterAccessibilityLevel_Property()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel setterAccessLevel = SGAccessibilityLevel.Public;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.SetterAccessibilityLevel = setterAccessLevel;

            // Assert
            Assert.AreEqual(setterAccessLevel, property.SetterAccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetSetterAccessibilityLevelNull_Property_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel setterAccessLevel = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.SetterAccessibilityLevel = setterAccessLevel;
        }

        [TestMethod]
        public void TestSetSetterAccessibilityLevel_FluentAPI()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel setterAccessLevel = SGAccessibilityLevel.Public;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithSetterAccessibilityLevel(setterAccessLevel);

            // Assert
            Assert.AreEqual(setterAccessLevel, property.SetterAccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetSetterAccessibilityLevelNull_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            SGAccessibilityLevel setterAccessLevel = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithSetterAccessibilityLevel(setterAccessLevel);
        }

        [TestMethod]
        public void TestSetInitializationValue_PropertyInitializer()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string initializationValue = "5";

            // Act
            SGClassProperty property = new SGClassProperty(propertyName, propertyType)
            {
                InitializationValue = initializationValue
            };

            // Assert
            Assert.AreEqual(initializationValue, property.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_PropertySetter()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string initializationValue = "5";
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property.InitializationValue = initializationValue;

            // Assert
            Assert.AreEqual(initializationValue, property.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_FluentAPI()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            object initializationValue = 5;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithInitializationValue(initializationValue);

            // Assert
            Assert.AreEqual(initializationValue.ToString(), property.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_Null()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            object initializationValue = null;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithInitializationValue(initializationValue);

            // Assert
            Assert.AreEqual("null", property.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_String()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            object initializationValue = "test string";
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithInitializationValue(initializationValue);

            // Assert
            Assert.AreEqual($"\"{initializationValue}\"", property.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_Int()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            int initializationValue = 5;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithInitializationValue(initializationValue);

            // Assert
            Assert.AreEqual("5", property.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_EmptyString()
        {
            // Arrange
            string propertyName = "property";
            string propertyType = "int";
            string initializationValue = String.Empty;
            SGClassProperty property = new SGClassProperty(propertyName, propertyType);

            // Act
            property = property.WithInitializationValue(initializationValue);

            // Assert
            Assert.AreEqual($"\"\"", property.InitializationValue);
        }
    }
}
