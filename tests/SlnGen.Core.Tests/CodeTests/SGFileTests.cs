using System;
using SlnGen.Core.Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SlnGen.Core.Utils;
using System.Collections.Generic;

namespace SlnGen.Core.Tests.CodeTests
{
    [TestClass]
    public class SGFileTests
    {
        [TestMethod]
        public void TestFileNameAndExtensionCtor_InitsFields()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = "cs";

            // Act
            SGFile file = new SGFile(fileName, fileExtension);

            // Assert
            Assert.AreEqual(fileName, file.FileName);
            Assert.AreEqual(fileExtension, file.FileExtension);
        }

        [TestMethod]
        public void TestFileNameContainsExtraPeriod_InitsFields()
        {
            // Arrange
            string fileName = "TestFile.Test";
            string fileExtension = "cs";

            // Act
            SGFile file = new SGFile(fileName, fileExtension);

            // Assert
            Assert.AreEqual(fileName, file.FileName);
            Assert.AreEqual(fileExtension, file.FileExtension);
        }

        [TestMethod]
        public void TestFileNameContainsMultiplePeriods_InitsFields()
        {
            // Arrange
            string fileName = "TestFile.Test.MoreTests";
            string fileExtension = "cs";

            // Act
            SGFile file = new SGFile(fileName, fileExtension);

            // Assert
            Assert.AreEqual(fileName, file.FileName);
            Assert.AreEqual(fileExtension, file.FileExtension);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestFileNameEndsInPeriod_ThrowsException()
        {
            // Arrange
            string fileName = "TestFile.";
            string fileExtension = "cs";

            // Act
            SGFile file = new SGFile(fileName, fileExtension);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestFileNameStartsWithPeriod_ThrowsException()
        {
            // Arrange
            string fileName = ".TestFile";
            string fileExtension = "cs";

            // Act
            SGFile file = new SGFile(fileName, fileExtension);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestFileExtensionContainsPeriods_ThrowsArgumentException()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = ".cs";

            // Act
            SGFile file = new SGFile(fileName, fileExtension);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullFileName_ThrowsArgumentNullException()
        {
            // Arrange
            string fileName = null;
            string fileExtension = "cs";

            // Act
            SGFile file = new SGFile(fileName, fileExtension);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyFileName_ThrowsArgumentException()
        {
            // Arrange
            string fileName = String.Empty;
            string fileExtension = "cs";

            // Act
            SGFile file = new SGFile(fileName, fileExtension);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullFileExtension_ThrowsArgumentNullException()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = null;

            // Act
            SGFile file = new SGFile(fileName, fileExtension);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyFileExtension_ThrowsArgumentException()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = String.Empty;

            // Act
            SGFile file = new SGFile(fileName, fileExtension);
        }


        [TestMethod]
        public void TestStingleStringCtor_InitsFields()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = "cs";
            string fullFileName = $"{fileName}.{fileExtension}";

            // Act
            SGFile file = new SGFile(fullFileName);

            // Assert
            Assert.AreEqual(fileName, file.FileName);
            Assert.AreEqual(fileExtension, file.FileExtension);
        }

        [TestMethod]
        public void TestSingleStringContainsMultiplePeriods_InitsFields()
        {
            // Arrange
            string fileName = "TestFile.MoreTests.Test";
            string fileExtension = "cs";
            string fullFileName = $"{fileName}.{fileExtension}";

            // Act
            SGFile file = new SGFile(fullFileName);

            // Assert
            Assert.AreEqual(fileName, file.FileName);
            Assert.AreEqual(fileExtension, file.FileExtension);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullName_ThrowsArgumentNullException()
        {
            // Arrange
            string fullFileName = null;

            // Act
            SGFile file = new SGFile(fullFileName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyName_ThrowsArgumentException()
        {
            // Arrange
            string fullFileName = String.Empty;

            // Act
            SGFile file = new SGFile(fullFileName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestNameStartsWithPeriod_ThrowsArgumentException()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = "cs";
            string fullFileName = $".{fileName}.{fileExtension}";

            // Act
            SGFile file = new SGFile(fullFileName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestNameEndsWithPeriod_ThrowsArgumentException()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = "cs";
            string fullFileName = $"{fileName}.{fileExtension}.";

            // Act
            SGFile file = new SGFile(fullFileName);
        }

        [TestMethod]
        // Assert
        [ExpectedException(typeof(ArgumentException))]
        public void TestNameWithNoPeriod_ThrowsArgumentException()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = "cs";
            string fullFileName = $"{fileName}{fileExtension}";

            // Act
            SGFile file = new SGFile(fullFileName);
        }

        [TestMethod]
        public void TestAddingAssemblyReference_FluentAPI()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = "cs";
            string fullFileName = $"{fileName}.{fileExtension}";
            string assemblyName = "System";
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);

            // Act
            SGFile file = new SGFile(fullFileName).WithAssemblies(assemblyReference);

            // Assert
            Assert.IsTrue(file.AssemblyReferences.Contains(assemblyReference));
        }

        [TestMethod]
        public void TestAddingMultipleAssemblyReferences_FluentAPI()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = "cs";
            string fullFileName = $"{fileName}.{fileExtension}";
            string assemblyName = "System";
            string assemblyName2 = "System.Text";
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);
            SGAssemblyReference assemblyReference2 = new SGAssemblyReference(assemblyName2);

            // Act
            SGFile file = new SGFile(fullFileName).WithAssemblies(assemblyReference, assemblyReference2);

            // Assert
            Assert.IsTrue(file.AssemblyReferences.ContainsAll(new List<SGAssemblyReference>
            {
                assemblyReference,
                assemblyReference2
            }));
        }

        [TestMethod]
        public void TestAddingAssemblyReferences_PropertyInitializer()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = "cs";
            string fullFileName = $"{fileName}.{fileExtension}";
            string assemblyName = "System";
            string assemblyName2 = "System.Text";
            SGAssemblyReference assemblyReference = new SGAssemblyReference(assemblyName);
            SGAssemblyReference assemblyReference2 = new SGAssemblyReference(assemblyName2);

            // Act
            SGFile file = new SGFile(fullFileName)
            {
                AssemblyReferences =
                {
                    assemblyReference,
                    assemblyReference2
                }
            };

            // Assert
            Assert.IsTrue(file.AssemblyReferences.ContainsAll(new List<SGAssemblyReference>
            {
                assemblyReference,
                assemblyReference2
            }));
        }

        [TestMethod]
        public void TestAddingNamespace_FluentAPI()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = "cs";
            string fullFileName = $"{fileName}.{fileExtension}";
            string namespaceName = "SlnGen.Core";
            SGNamespace @namespace = new SGNamespace(namespaceName);

            // Act
            SGFile file = new SGFile(fullFileName).WithNamespaces(@namespace);

            // Assert
            Assert.IsTrue(file.Namespaces.Contains(@namespace));
        }

        [TestMethod]
        public void TestAddingMultipleNamespaces_FluentAPI()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = "cs";
            string fullFileName = $"{fileName}.{fileExtension}";
            string namespaceName = "SlnGen.Core";
            string namespaceName2 = "SlnGen.Core.Code";
            SGNamespace namespace1 = new SGNamespace(namespaceName);
            SGNamespace namespace2 = new SGNamespace(namespaceName2);

            // Act
            SGFile file = new SGFile(fullFileName).WithNamespaces(namespace1, namespace2);

            // Assert
            Assert.IsTrue(file.Namespaces.ContainsAll(new List<SGNamespace>
            {
                namespace1,
                namespace2
            }));
        }

        [TestMethod]
        public void TestAddingNamespaces_PropertyInitializer()
        {
            // Arrange
            string fileName = "TestFile";
            string fileExtension = "cs";
            string fullFileName = $"{fileName}.{fileExtension}";
            string namespaceName = "SlnGen.Core";
            string namespaceName2 = "SlnGen.Core.Code";
            SGNamespace namespace1 = new SGNamespace(namespaceName);
            SGNamespace namespace2 = new SGNamespace(namespaceName2);

            // Act
            SGFile file = new SGFile(fullFileName)
            {
                Namespaces =
                {
                    namespace1,
                    namespace2
                }
            };

            // Assert
            Assert.IsTrue(file.Namespaces.ContainsAll(new List<SGNamespace>
            {
                namespace1,
                namespace2
            }));
        }
    }
}
