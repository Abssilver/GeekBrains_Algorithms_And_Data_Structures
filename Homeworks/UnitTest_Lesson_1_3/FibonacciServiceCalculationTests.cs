using System;
using Lesson_1_3;
using NUnit.Framework;

namespace UnitTest_Lesson_1_3
{
    [TestFixture]
    public class FibonacciService_CalculationTests
    {
        private FibonacciCalculationService _calculationService;

        [SetUp]
        public void SetUp()
        {
            _calculationService = new FibonacciCalculationService();
        }
        
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(7, 13)]
        public void AreLoopCaclulationsCorrect(int input, int expected)
        {
            var actual = _calculationService.GetFibonacciNumberLoop(input);
            Assert.AreEqual(expected, actual, "Calculations are correct");
        }
        
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(7, 13)]
        public void AreRecursionCaclulationsCorrect(int input, int expected)
        {
            var actual = _calculationService.GetFibonacciNumberRecursion(input);
            Assert.AreEqual(expected, actual, "Calculations are correct");
        }
        
        [TestCase(int.MinValue)]
        public void IsLoopThrowsAnException(int input)
        {
            Assert.Throws<ArgumentException>(() => _calculationService.GetFibonacciNumberLoop(input));
        }
        [TestCase(int.MinValue)]
        public void IsRecursionThrowsAnException(int input)
        {
            Assert.Throws<ArgumentException>(() => _calculationService.GetFibonacciNumberRecursion(input));
        }
    }
}