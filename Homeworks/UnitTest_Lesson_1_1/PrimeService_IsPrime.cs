using Lesson_1_1;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class PrimeService_IsPrime
    {
        private PrimeNumberCheckService _primeService;

        [SetUp]
        public void SetUp()
        {
            _primeService = new PrimeNumberCheckService();
        }
        [TestCase(uint.MinValue)] // Следует обратить внимание, что простым является число, большее 1 - код нужнается в доработке
        [TestCase((uint)1)] // Следует обратить внимание, что простым является число, большее 1 - код нужнается в доработке
        [TestCase((uint)2)]
        [TestCase((uint)19)]
        public void IsPrime_ReturnTrue(uint value)
        {
            var result = _primeService.PrimeNumberCheck(value);

            Assert.IsTrue(result, $"{value} is prime");
        }
        
        [TestCase((uint)27)]
        [TestCase(uint.MaxValue)]
        public void IsPrime_ReturnFalse(uint value)
        {
            var result = _primeService.PrimeNumberCheck(value);

            Assert.IsFalse(result, $"{value} should not be prime");
        }
    }
}