using System.IO;
using Lesson_8_2;
using NUnit.Framework;

namespace UnitTest_Lesson_8_2
{
    [TestFixture]
    public class ExternalSort_Tests
    {
        private ExternalSortService _externalSortService;
        private int[] _testingArray;
        private string _filePathTest;
        private string _filePathExpected;
        private int[] _expectedArray;

        [SetUp]
        public void SetUp()
        {
            _externalSortService = new ExternalSortService();
            _testingArray = new[] {int.MinValue, 0, -1, -2, -3, -4, 2, 4, 1, 3, int.MaxValue, 0, int.MinValue};
            _filePathTest = "test.txt";
            _filePathExpected = "expected.txt";
            SaveDataAsFile(_testingArray, _filePathTest);
            _expectedArray = new[] {int.MinValue, int.MinValue, -4, -3, -2, -1, 0, 0, 1, 2, 3, 4, int.MaxValue};
            SaveDataAsFile(_expectedArray, _filePathExpected);
        }

        private void SaveDataAsFile(int[] array, string filePath)
        {
            using (StreamWriter writer =
                new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write)))
                for (int i = 0; i < array.Length; i++)
                    writer.WriteLine(array[i]);
        }
        
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        [TestCase(0)]
        [TestCase(5)]
        public void ExternalSortService_ExternalSort_BucketNumber_AreEqual_ReturnTrue(int bucketSize)
        {
            string actualFilePath;
            _externalSortService.ExternalSort(_filePathTest, bucketSize, out actualFilePath);
            Assert.AreEqual(true, AreEqual(_filePathExpected, actualFilePath));
        }

        private bool AreEqual(string expectedFilePath, string actualFilePath)
        {
            if (!File.Exists(actualFilePath))
                return false;

            using (StreamReader reader =
                new StreamReader(File.Open($"{expectedFilePath}", FileMode.Open, FileAccess.Read)))
            {
                using (StreamReader anotherReader =
                    new StreamReader(File.Open($"{actualFilePath}", FileMode.Open, FileAccess.Read)))
                {
                    if (reader.BaseStream.Length != anotherReader.BaseStream.Length)
                        return false;
                    
                    string line;
                    string anotherLine;
                    {
                        while ((line = reader.ReadLine()) is not null && 
                               (anotherLine = anotherReader.ReadLine()) is not null)
                        {
                            if (line != anotherLine)
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
            }
        }
    }
}