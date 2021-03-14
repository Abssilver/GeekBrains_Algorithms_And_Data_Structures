using Lesson_7_1;
using NUnit.Framework;

namespace UnitTest_Lesson_7_1
{
    [TestFixture]
    public class RouteSearch_Tests
    {
        private RouteSearchService _routeService;
        private int[,] _expectedData;

        [SetUp]
        public void SetUp()
        {
            _routeService = new RouteSearchService();
            _expectedData = GetExpectedData();
        }

        #region CreateData

        private int[,] GetTestData() =>
            new[,]
            {
                { 1, 1, 1, 1, 1, 0, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1},
                { 0, 1, 1, 1, 1, 1, 0, 1, 1, 1},
                { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
            };

        private int[,] GetExpectedData() =>
            new[,]
            {
                { 1, 1, 1,  1,  1,  0,  0,  0,   0,   0},
                { 1, 2, 3,  4,  5,  5,  5,  0,   0,   0},
                { 0, 2, 5,  9, 14, 19,  0,  0,   0,   0},
                { 0, 2, 7, 16,  0, 19, 19, 19,  19,  19},
                { 0, 2, 9, 25, 25, 44, 63, 82, 101, 120}
            };

        #endregion
        
        [Test]
        public void RouteSearchService_FindRoutes_AreEqual_ReturnTrue()
        {
            int[,] actual = GetTestData();
            _routeService.FindRoutes(actual);
            bool areEqual = AreEqual(_expectedData, actual);
            Assert.AreEqual(true, areEqual);
        }

        private bool AreEqual(int[,] expected, int[,] actual)
        {
            if (expected.Length == actual.Length)
            {
                for (int i = 0; i < expected.GetLength(0); i++)
                for (int j = 0; j < expected.GetLength(1); j++) 
                    if (expected[i,j]!=actual[i,j]) 
                        return false;
                return true;
            } 
            return false;
        }
    }
}