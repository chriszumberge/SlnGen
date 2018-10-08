using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnGen.Core.Code;

namespace SlnGen.Core.Tests.CodeTests
{
    [TestClass]
    public class SGAssemblyReferenceTests
    {
        [TestMethod]
        public void TestAssemblyNameCtor_InitsFields()
        {
            // Arrange
            string assemblyName = "System";

            // Act
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);

            // Assert
            Assert.AreEqual(assemblyName, assemblyReference.AssemblyName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullAssemblyName_ThrowsArgumentNullException()
        {
            // Arrange
            string assemblyName = null;

            // Act
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyAssemblyName_ThrowsArgumentException()
        {
            // Arrange
            string assemblyName = String.Empty;

            // Act
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);
        }

        [TestMethod]
        public void TestAssemblyNameWithSpaces_ReplacedWithEmpty()
        {
            // Arrange
            string assemblyName = "Assembly Reference";

            // Act
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);

            // Assert
            Assert.AreEqual(assemblyName.Replace(" ", String.Empty), assemblyReference.AssemblyName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestAssemblyNameSetEmpty_ThrowsArgumentException()
        {
            // Arrange
            string assemblyName = "Assembly Reference";
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);

            // Act
            assemblyReference.AssemblyName = String.Empty;
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAssemblyNameSetNull_ThrowsArgumentNullException()
        {
            // Arrange
            string assemblyName = "Assembly Reference";
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);

            // Act
            assemblyReference.AssemblyName = null;
        }

        [TestMethod]
        public void TestAssemblyNameWithSpacesSet_ReplacedWithEmpty()
        {
            // Arrange
            string assemblyName = "Assembly Reference";
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);

            // Act
            assemblyReference.AssemblyName = assemblyName;

            // Assert
            Assert.AreEqual(assemblyName.Replace(" ", String.Empty), assemblyReference.AssemblyName);
        }

        [TestMethod]
        public void Test_ToString()
        {
            // Arrange
            string assemblyName = "System.Text";
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);

            // Act
            string output = assemblyReference.ToString();

            // Assert
            Assert.AreEqual($"using {assemblyName};", output);
        }
    }
}
