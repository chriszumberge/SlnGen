using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnGen.Core.Code;

namespace SlnGen.Core.Tests.CodeTests
{
    [TestClass]
    public class SGArgumentTests
    {
        [TestMethod]
        public void TestArgumentCtor_InitsFields()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "ArgType";

            // Act
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Assert
            Assert.AreEqual(argumentName, arg.ArgumentName);
            Assert.AreEqual(argTypeName, arg.ArgumentTypeName);
        }

        [TestMethod]
        public void TestArgumentCtor_TypedReturnType_InitsFields()
        {
            // Arrange
            string argumentName = "Argument";
            Type argType = typeof(int);

            // Act
            SGArgument arg = new SGArgument(argType, argumentName);

            // Assert
            Assert.AreEqual(argumentName, arg.ArgumentName);
            Assert.AreEqual(argType.Name, arg.ArgumentTypeName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentCtor_NullArgumentName_ThrowsArgumentNullException()
        {
            // Arrange
            string argumentName = null;
            string argTypeName = "ArgType";

            // Act
            SGArgument arg = new SGArgument(argTypeName, argumentName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentCtor_NullArgumentName_TypedReturnType_ThrowsArgumentNullException()
        {
            // Arrange
            string argumentName = null;
            Type argType = typeof(int);

            // Act
            SGArgument arg = new SGArgument(argType, argumentName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentCtor_EmptyArgumentName_ThrowsArgumentException()
        {
            // Arrange
            string argumentName = String.Empty;
            string argTypeName = "ArgType";

            // Act
            SGArgument arg = new SGArgument(argTypeName, argumentName);
        }


        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentCtor_EmptyArgumentName_TypedReturnType_ThrowsArgumentException()
        {
            // Arrange
            string argumentName = String.Empty;
            Type argType = typeof(int);

            // Act
            SGArgument arg = new SGArgument(argType, argumentName);
        }

        [TestMethod]
        public void TestArgumentCtor_NameWithSpaces_ReplacedWithUnderscores()
        {
            // Arrange
            string argumentName = "Some Argument Name";
            Type argType = typeof(int);

            // Act
            SGArgument arg = new SGArgument(argType, argumentName);

            // Assert
            Assert.AreEqual(argumentName.Replace(" ", "_"), arg.ArgumentName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentCtor_NullArgumentType_ThrowsArgumentNullException()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = null;

            // Act
            SGArgument arg = new SGArgument(argTypeName, argumentName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentCtor_EmptyArgumentType_ThrowsArgumentException()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = String.Empty;

            // Act
            SGArgument arg = new SGArgument(argTypeName, argumentName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentCtor_NullArgumentType_TypedReutrnType_ThrowsArgumentNullException()
        {
            // Arrange
            string argumentName = "Argument";
            Type argType = null;

            // Act
            SGArgument arg = new SGArgument(argType, argumentName);
        }

        [TestMethod]
        public void TestArgumentCtor_DefaultValue_InitsFields()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            string defaultValue = "3";

            // Act
            SGArgument arg = new SGArgument(argTypeName, argumentName, defaultValue);

            // Assert
            Assert.AreEqual(argumentName, arg.ArgumentName);
            Assert.AreEqual(argTypeName, arg.ArgumentTypeName);
            Assert.AreEqual(defaultValue, arg.ArgumentDefaultValue);
        }

        [TestMethod]
        public void TestArgumentCtor_DefaultValue_TypedReturnType_InitsFields()
        {
            // Arrange
            string argumentName = "Argument";
            Type argType = typeof(int);
            string defaultValue = "3";

            // Act
            SGArgument arg = new SGArgument(argType, argumentName, defaultValue);

            // Assert
            Assert.AreEqual(argumentName, arg.ArgumentName);
            Assert.AreEqual(argType.Name, arg.ArgumentTypeName);
            Assert.AreEqual(defaultValue, arg.ArgumentDefaultValue);
        }

        [TestMethod]
        public void TestArgumentNameSet()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "ArgType";
            string newArgName = "NewArgumentName";
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg.ArgumentName = newArgName;

            // Assert
            Assert.AreEqual(newArgName, arg.ArgumentName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentNameSetNull_ThrowsArgumentNullException()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "ArgType";
            string newArgName = null;
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg.ArgumentName = newArgName;
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentNameSetEmpty_ThrowsArgumentException()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "ArgType";
            string newArgName = String.Empty;
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg.ArgumentName = newArgName;
        }

        [TestMethod]
        public void TestArgumentNameSet_WithSpaces_ReplacedWithUnderscores()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "ArgType";
            string newArgName = "New Argument Name";
            SGArgument arg = new SGArgument(argTypeName, argumentName);
            
            // Act
            arg.ArgumentName = newArgName;

            // Assert
            Assert.AreEqual(newArgName.Replace(" ", "_"), arg.ArgumentName);
        }

        [TestMethod]
        public void TestArgumentNameSet_FluentAPI()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "ArgType";
            string newArgName = "NewArgumentName";
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg = arg.WithArgumentName(newArgName);

            // Assert
            Assert.AreEqual(newArgName, arg.ArgumentName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentNameSetNull_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "ArgType";
            string newArgName = null;
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg = arg.WithArgumentName(newArgName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentNameSetEmpty_FluentAPI_ThrowsArgumentException()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "ArgType";
            string newArgName = String.Empty;
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg = arg.WithArgumentName(newArgName);
        }

        [TestMethod]
        public void TestArgumentNameSetWithSpaces_FluentAPI_ReplacedWithUnderscores()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "ArgType";
            string newArgName = "New Argument Name";
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg = arg.WithArgumentName(newArgName);

            // Assert
            Assert.AreEqual(newArgName.Replace(" ", "_"), arg.ArgumentName);
        }

        [TestMethod]
        public void TestArgumentTypeSet_Property()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            string newArgTypeName = "bool";
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg.ArgumentTypeName = newArgTypeName;

            // Assert
            Assert.AreEqual(newArgTypeName, arg.ArgumentTypeName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentTypeSetNull_Property_ThrowsArgumentNullException()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            string newArgTypeName = null;
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg.ArgumentTypeName = newArgTypeName;
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentTypeSetEmpty_Property_ThrowsArgumentException()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            string newArgTypeName = String.Empty;
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg.ArgumentTypeName = newArgTypeName;
        }

        [TestMethod]
        public void TestArgumentTypeSet_FluentAPI()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            string newArgTypeName = "bool";
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg = arg.WithArgumentTypeName(newArgTypeName);

            // Assert
            Assert.AreEqual(newArgTypeName, arg.ArgumentTypeName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentTypeSetNull_FluentAPI_ThrowsArgumentNullException()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            string newArgTypeName = null;
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg = arg.WithArgumentTypeName(newArgTypeName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentTypeSetEmpty_FluentAPI_ThrowsArgumentException()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            string newArgTypeName = String.Empty;
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg = arg.WithArgumentTypeName(newArgTypeName);
        }

        [TestMethod]
        public void TestArgumentTypeSet_FluentAPI_SystemType()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            Type newArgType = typeof(bool);
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg = arg.WithArgumentTypeName(newArgType);

            // Assert
            Assert.AreEqual(newArgType.Name, arg.ArgumentTypeName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentTypeSetNull_FluentAPI_SystemType()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            Type newArgType = null;
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg = arg.WithArgumentTypeName(newArgType);
        }

        [TestMethod]
        public void TestArgumentDefaultValueSet_Property()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            string defaultValue = "3";
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg.ArgumentDefaultValue = defaultValue;

            // Assert
            Assert.AreEqual(defaultValue, arg.ArgumentDefaultValue);
        }

        [TestMethod]
        public void TestArgumentDefaultValueSet_PropertyInitializer()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            string defaultValue = "3";

            // Act
            SGArgument arg = new SGArgument(argTypeName, argumentName)
            {
                ArgumentDefaultValue = defaultValue
            };

            // Assert
            Assert.AreEqual(defaultValue, arg.ArgumentDefaultValue);
        }

        [TestMethod]
        public void TestArgumentDefaultValueSet_FluentAPI()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            string defaultValue = "3";
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            arg = arg.WithDefaultValue(defaultValue);

            // Assert
            Assert.AreEqual(defaultValue, arg.ArgumentDefaultValue);
        }

        [TestMethod]
        public void Test_ToString()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";            
            SGArgument arg = new SGArgument(argTypeName, argumentName);

            // Act
            string output = arg.ToString();

            // Assert
            Assert.AreEqual($"{argTypeName} {argumentName}", output);
        }

        [TestMethod]
        public void Test_Defaultvalue_ToString()
        {
            // Arrange
            string argumentName = "Argument";
            string argTypeName = "int";
            string defaultValue = "3";
            SGArgument arg = new SGArgument(argTypeName, argumentName, defaultValue);

            // Act
            string output = arg.ToString();

            // Assert
            Assert.AreEqual($"{argTypeName} {argumentName} = {defaultValue}", output);
        }
    }
}
