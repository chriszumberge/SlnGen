using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnGen.Core.Code;

namespace SlnGen.Core.Tests.CodeTests
{
    [TestClass]
    public class SGClassFieldTests
    {
        [TestMethod]
        public void TestFieldCtor_InitsFields()
        {
            // Arrange
            string fieldName = "field";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            string fieldType = "int";
            bool @static = true;
            bool @const = true;
            bool @readonly = true;

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType, accessibilityLevel, @static, @const, @readonly);

            // Assert
            Assert.AreEqual(fieldName, field.FieldName);
            Assert.AreEqual(accessibilityLevel, field.AccessibilityLevel);
            Assert.AreEqual(fieldType, field.FieldType);
            Assert.AreEqual(@static, field.IsStatic);
            Assert.AreEqual(@const, field.IsConst);
            Assert.AreEqual(@readonly, field.IsReadonly);
        }

        [TestMethod]
        public void TestFieldCtor_Defaults_InitsFields()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Assert
            Assert.AreEqual(fieldName, field.FieldName);
            Assert.AreEqual(fieldType, field.FieldType);
            Assert.AreEqual(SGAccessibilityLevel.Private, field.AccessibilityLevel);
            Assert.IsFalse(field.IsStatic);
            Assert.IsFalse(field.IsConst);
            Assert.IsFalse(field.IsReadonly);
        }

        [TestMethod]
        public void TestFieldCtor_TypedFieldType_InitsFields()
        {
            // Arrange
            string fieldName = "field";
            Type fieldType = typeof(int);

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Assert
            Assert.AreEqual(fieldType.Name, field.FieldType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFieldCtor_NullFieldName_ThrowsArgumentNullException()
        {
            // Arrange
            string fieldName = null;
            string fieldType = "int";

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestFieldCtor_EmptyFieldName_ThrowsArgumentException()
        {
            // Arrange
            string fieldName = String.Empty;
            string fieldType = "int";

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType);
        }

        [TestMethod]
        public void TestFieldCtor_FieldNameWithSpaces_ReplacedWithUnderscores()
        {
            // Arrange
            string fieldName = "Some Field Name";
            string fieldType = "int";

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Assert
            Assert.AreEqual(fieldName.Replace(" ", "_"), field.FieldName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFieldCtor_NullFieldType_ThrowsArgumentNullException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = null;

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFieldCtor_NullTypedFieldType_ThrowsArgumentNullException()
        {
            // Arrange
            string fieldName = "field";
            Type fieldType = null;

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestFieldCtor_EmptyFieldType_ThrowsArgumentException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = String.Empty;

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType);
        }

        [TestMethod]
        public void TestFieldNameSet()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldName = "newField";
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field.FieldName = newFieldName;

            // Assert
            Assert.AreEqual(newFieldName, field.FieldName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFieldNameSetNull_ThrowsArgumentNullException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldName = null;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field.FieldName = newFieldName;
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestFieldNameSetEmpty_ThrowsArgumentException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldName = String.Empty;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field.FieldName = newFieldName;
        }

        [TestMethod]
        public void TestFieldNameSet_WithSpaces_ReplacedWithUnderscores()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldName = "some New Field Name";
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field.FieldName = newFieldName;

            // Assert
            Assert.AreEqual(newFieldName.Replace(" ", "_"), field.FieldName);
        }

        [TestMethod]
        public void TestFieldNameSet_FluentAPI()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldName = "newField";
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithFieldName(newFieldName);

            // Assert
            Assert.AreEqual(newFieldName, field.FieldName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFieldNameSetNull_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldName = null;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithFieldName(newFieldName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestFieldNameSetEmpty_FluentAPI_ThrowsArgumentException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldName = String.Empty;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithFieldName(newFieldName);
        }

        [TestMethod]
        public void TestFieldNameSet_WithSpaces_FluentAPI_ReplacedWithUnderscores()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldName = "some New Field Name";
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithFieldName(newFieldName);

            // Assert
            Assert.AreEqual(newFieldName.Replace(" ", "_"), field.FieldName);
        }

        [TestMethod]
        public void TestTypeValueSet_Property()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldType = "bool";
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field.FieldType = newFieldType;

            // Assert
            Assert.AreEqual(newFieldType, field.FieldType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTypeValueSetNull_Property_ThrowsArgumentNullException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldType = null;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field.FieldType = newFieldType;
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestTypevalueSetEmpty_Property_ThrowsArgumentException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldType = String.Empty;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field.FieldType = newFieldType;
        }

        [TestMethod]
        public void TestTypeValueSet_FluentAPI()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldType = "bool";
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithTypeValue(newFieldType);

            // Assert
            Assert.AreEqual(newFieldType, field.FieldType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTypeValueSetNull_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldType = null;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithTypeValue(newFieldType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestTypevalueSetEmpty_FluentAPI_ThrowsArgumentException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string newFieldType = String.Empty;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithTypeValue(newFieldType);
        }

        [TestMethod]
        public void TestTypeValueSet_WithSystemType_FluentAPI()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            Type newFieldType = typeof(bool);
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithTypeValue(newFieldType);

            // Assert
            Assert.AreEqual(newFieldType.Name, field.FieldType);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTypeValueSetNull_WithSystemType_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            Type newFieldType = null;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithTypeValue(newFieldType);
        }
        [TestMethod]
        public void TestAccessibilityLevelSet_Property()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            SGAccessibilityLevel newAccessibilityLevel = SGAccessibilityLevel.Protected;
            SGClassField field = new SGClassField(fieldName, fieldType, accessibilityLevel);

            // Act
            field.AccessibilityLevel = newAccessibilityLevel;

            // Assert
            Assert.AreEqual(newAccessibilityLevel, field.AccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAccessibilityLevelSetNull_Property_ThrowsArgumentNullException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            SGAccessibilityLevel newAccessibilityLevel = null;
            SGClassField field = new SGClassField(fieldName, fieldType, accessibilityLevel);

            // Act
            field.AccessibilityLevel = newAccessibilityLevel;
        }

        [TestMethod]
        public void TestAccessibilityLevelSet_FluentAPI()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            SGAccessibilityLevel newAccessibilityLevel = SGAccessibilityLevel.Protected;
            SGClassField field = new SGClassField(fieldName, fieldType, accessibilityLevel);

            // Act
            field = field.WithAccessibilityLevel(newAccessibilityLevel);

            // Assert
            Assert.AreEqual(newAccessibilityLevel, field.AccessibilityLevel);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAccessibilityLevelSetNull_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            SGAccessibilityLevel accessibilityLevel = SGAccessibilityLevel.Public;
            SGAccessibilityLevel newAccessibilityLevel = null;
            SGClassField field = new SGClassField(fieldName, fieldType, accessibilityLevel);

            // Act
            field = field.WithAccessibilityLevel(newAccessibilityLevel);
        }

        [TestMethod]
        public void TestSetStaticFlag_PropertyInitializer()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            bool isStatic = true;

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType)
            {
                IsStatic = isStatic
            };

            // Assert
            Assert.IsTrue(field.IsStatic);
        }

        [TestMethod]
        public void TestSetStaticFlag_PropertySetter()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            bool isStatic = true;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field.IsStatic = isStatic;

            // Assert
            Assert.IsTrue(field.IsStatic);
        }

        [TestMethod]
        public void TestSetStaticFlag_FluentAPI()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            bool isStatic = true;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithIsStatic(isStatic);

            // Assert
            Assert.IsTrue(field.IsStatic);
        }

        [TestMethod]
        public void TestSetConstFlag_PropertyInitializer()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            bool isConst = true;

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType)
            {
                IsConst = isConst
            };

            // Assert
            Assert.IsTrue(field.IsConst);
        }

        [TestMethod]
        public void TestSetConstFlag_PropertySetter()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            bool isConst = true;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field.IsConst = isConst;

            // Assert
            Assert.IsTrue(field.IsConst);
        }

        [TestMethod]
        public void TestSetConstFlag_FluentAPI()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            bool isConst = true;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithIsConst(isConst);

            // Assert
            Assert.IsTrue(field.IsConst);
        }

        [TestMethod]
        public void TestSetReadonlyFlag_PropertyInitializer()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            bool isReadonly = true;

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType)
            {
                IsReadonly = isReadonly
            };

            // Assert
            Assert.IsTrue(field.IsReadonly);
        }

        [TestMethod]
        public void TestSetReadonlyFlag_PropertySetter()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            bool isReadonly = true;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field.IsReadonly = isReadonly;

            // Assert
            Assert.IsTrue(field.IsReadonly);
        }

        [TestMethod]
        public void TestSetReadonlyFlag_FluentAPI()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            bool isReadonly = true;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithIsReadonly(isReadonly);

            // Assert
            Assert.IsTrue(field.IsReadonly);
        }

        [TestMethod]
        public void TestSetInitializationValue_PropertyInitializer()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string initializationValue = "5";

            // Act
            SGClassField field = new SGClassField(fieldName, fieldType)
            {
                InitializationValue = initializationValue
            };

            // Assert
            Assert.AreEqual(initializationValue, field.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_PropertySetter()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string initializationValue = "5";
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field.InitializationValue = initializationValue;

            // Assert
            Assert.AreEqual(initializationValue, field.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_FluentAPI()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            object initializationValue = 5;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithInitializationValue(initializationValue);

            // Assert
            Assert.AreEqual(initializationValue.ToString(), field.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_Null()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            object initializationValue = null;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithInitializationValue(initializationValue);

            // Assert
            Assert.AreEqual("null", field.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_String()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string initializationValue = "test string";
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithInitializationValue(initializationValue);

            // Assert
            Assert.AreEqual($"\"{initializationValue}\"", field.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_Int()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            int initializationValue = 5;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithInitializationValue(initializationValue);

            // Assert
            Assert.AreEqual($"5", field.InitializationValue);
        }

        [TestMethod]
        public void TestSetInitializationValue_EmptyString()
        {
            // Arrange
            string fieldName = "field";
            string fieldType = "int";
            string initializationValue = String.Empty;
            SGClassField field = new SGClassField(fieldName, fieldType);

            // Act
            field = field.WithInitializationValue(initializationValue);

            // Assert
            Assert.AreEqual($"\"\"", field.InitializationValue);
        }
    }
}
