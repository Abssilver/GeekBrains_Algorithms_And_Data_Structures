using System;
using Lesson_2_2;
using NUnit.Framework;

namespace UnitTest_Lesson_2_2
{
    [TestFixture]
    public class BinarySearch_Tests
    {
        private BinarySearchService _binarySearchService;
        private int[] _array; 
        
        [SetUp]
        public void Setup()
        {
            _binarySearchService = new BinarySearchService();
            _array = new []{ 4, 6, 10, 15, 99, 102, 148, 150};
        }

        [TestCase(10, 2)]
        [TestCase(150, 7)]
        [TestCase(0, -1)]
        public void BinarySearch_ResultIndexEqualsTrue(int valueToSearch, int expected)
        {
            int actual = _binarySearchService.BinarySearch(_array, valueToSearch);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void BinarySearch_ReturnExceptionIntoEmptyArrayInput()
        {
            int[] emptyArray = new int[0];
            int valueToSearch = 5;
            Assert.Throws<ArgumentException>(() => _binarySearchService.BinarySearch(emptyArray, valueToSearch));
        }
        [Test]
        public void BinarySearch_ReturnExceptionIntoNullArrayInput()
        {
            int valueToSearch = 5;
            Assert.Throws<ArgumentException>(() => _binarySearchService.BinarySearch(null, valueToSearch));
        }
    }
}