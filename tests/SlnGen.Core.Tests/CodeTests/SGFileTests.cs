using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnGen.Core.Code;

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
    }
}
