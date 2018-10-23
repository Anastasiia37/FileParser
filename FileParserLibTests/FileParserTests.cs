using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FileParserLib.Tests
{
    [TestClass()]
    public class FileParserTests
    {
        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void StringCounter_InitializeTest_InputNotExistingFile_ExpectedFileNotFoundException()
        {
            // Arrange
            string path = "FileNotExist.txt";
            string stringForCount = "123";

            // Act
            FileParser fileParser = StringCounter.Initialize(path, stringForCount);

            // Assert
            Assert.Fail();
        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void StringReplacer_InitializeTest_InputNotExistingFile_ExpectedFileNotFoundException()
        {
            // Arrange
            string path = "FileNotExist.txt";
            string stringForCount = "123";
            string stringForReplace = "123";

            // Act
            FileParser fileParser = StringReplacer.Initialize(path, stringForCount, stringForReplace);

            // Assert
            Assert.Fail();
        }


        [DataTestMethod()]
        [DataRow("123", 3)]
        [DataRow("12", 4)]
        [DataRow("1234", 0)]
        public void StringCounter_ParseTest_InputStringForSearch_CorrectExecution(string stringForSearch, int expectedCount)
        {
            // Arrange
            string path = @"Resources\File.txt";
            FileParser fileParser = StringCounter.Initialize(path, stringForSearch);

            // Act
            int actualCount = fileParser.Parse();

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [DataTestMethod()]
        [DataRow("123", "56789", 3)]
        [DataRow("56789", "123", 3)]
        [DataRow("1234", "22", 0)]
        public void StringReplacer_ParseTest_InputStringForSearchAndStringForReplacement_CorrectExecution(
            string stringForSearch, string stringForReplacement, int expectedReplacement)
        {
            // Arrange
            string path = @"Resources\File.txt";
            FileParser fileParser = StringReplacer.Initialize(path, stringForSearch, stringForReplacement);

            // Act
            int actualReplacement = fileParser.Parse();

            // Assert
            Assert.AreEqual(expectedReplacement, actualReplacement);
        }
    }
}