using Lesson_8_1;
using NUnit.Framework;

namespace UnitTest_Lesson_8_1
{
    [TestFixture]
    public class BucketSort_Tests
    {
        private BucketsortService _bucketsortService;
        private int[] _testingArray;
        private int[] _expectedArray;

        [SetUp]
        public void SetUp()
        {
            _bucketsortService = new BucketsortService();
            _testingArray = new[] {int.MinValue, 0, -1, -2, -3, -4, 2, 4, 1, 3, int.MaxValue, 0, int.MinValue};
            _expectedArray = new[] {int.MinValue, int.MinValue, -4, -3, -2, -1, 0, 0, 1, 2, 3, 4, int.MaxValue};
        }

        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        [TestCase(0)]
        [TestCase(5)]
        public void BucketsortService_SortArray_BucketNumber_AreEqual_ReturnTrue(int bucketsNumber)
        {
            var testCopy = new int[_testingArray.Length];
            _testingArray.CopyTo(testCopy, 0);
            _bucketsortService.SortArray(testCopy, bucketsNumber);
            Assert.AreEqual(true, AreEqual(testCopy, _expectedArray));
        }

        private bool AreEqual(int[] first, int[] second)
        {
            if (first.Length != second.Length)
                return false;
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i]!=second[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}