using Lesson_4_2;
using NUnit.Framework;

namespace UnitTest_Lesson_4_2
{
    [TestFixture]
    public class Tree_Tests
    {
        private ITree _tree;

        [SetUp]
        public void SetUp()
        {
            _tree = new Tree();
            _tree.AddItem(6);
            _tree.AddItem(2);
            _tree.AddItem(3);
            _tree.AddItem(11);
            _tree.AddItem(30);
            _tree.AddItem(9);
            _tree.AddItem(13);
            _tree.AddItem(18);
            /*
                 __(6)___
                /        \
            (2)          (11)
               \        /    \
                (3)  (9)      (30)
                             /
                        (13)
                            \
                             (18)
            */
        }

        [TestCase(6)]
        public void Tree_GetRoot_Six_ReturnTrue(int expected)
        {
            int actual = _tree.GetRoot().Value;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(2)]
        [TestCase(6)]
        [TestCase(18)]
        public void Tree_GetVertexByValue_CompareValues_ReturnNotNUll(int expected)
        {
            Vertex actual = _tree.GetVertexByValue(expected);
            Assert.NotNull(actual);
        }
        
        [TestCase(0)]
        public void Tree_GetVertexByValue_CompareValues_ReturnNUll(int value)
        {
            Vertex actual = _tree.GetVertexByValue(value);
            Assert.Null(actual);
        }
        
        [TestCase(13, 7)]
        public void Tree_RemoveItem_Thirteen_TreeHelper_GetLength_ValuesAreEqual_True(int value, int expected)
        {
            _tree.RemoveItem(value);
            int actual = TreeHelper.GetTreeInLine(_tree).Length;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(4)]
        public void Tree_AddItem_Four_GetVertexByValue_ReturnNotNull(int value)
        {
            _tree.AddItem(value);
            Vertex actual = _tree.GetVertexByValue(value);
            Assert.NotNull(actual);
        }
    }
}