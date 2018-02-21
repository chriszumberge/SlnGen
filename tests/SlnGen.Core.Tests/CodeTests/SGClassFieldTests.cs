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
    }
}
