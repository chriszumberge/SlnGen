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
        public void Test_ToString()
        {
            // Arrange
            string assemblyName = "System.Text";

            // Act
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);

            // Assert
            Assert.AreEqual($"using {assemblyName};", assemblyReference.ToString());
        }
    }
}
