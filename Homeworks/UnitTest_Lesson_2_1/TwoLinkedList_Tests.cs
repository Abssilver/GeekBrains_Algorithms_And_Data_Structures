using System;
using Lesson_2_1;
using NUnit.Framework;

namespace UnitTest_Lesson_2_1
{
    [TestFixture]
    public class TwoLinkedList_Tests
    {
        private TwoLinkedList _twoLinkedList;

        [SetUp]
        public void SetUp()
        {
            _twoLinkedList = new TwoLinkedList();
            // {2 , 8, 10, -5}
            _twoLinkedList.AddNode(2);
            _twoLinkedList.AddNode(8);
            _twoLinkedList.AddNode(10);
            _twoLinkedList.AddNode(-5);
        }

        [TestCase(4)]
        public void CountTest_ReturnTrue(int expected)
        {
            int actual = _twoLinkedList.GetCount();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(10)]
        public void FindNodeTest_ReturnNotNull(int value)
        {
            Node actual = _twoLinkedList.FindNode(value);
            Assert.NotNull(actual);
        }
        
        [TestCase(0)]
        public void FindNodeTest_ReturnNull(int value)
        {
            Node actual = _twoLinkedList.FindNode(value);
            Assert.Null(actual);
        }

        [TestCase(22, 4)]
        public void RemoveNode_ReturnCountTrue(int value, int expected)
        {
            _twoLinkedList.AddNode(value);
            Node test = _twoLinkedList.FindNode(value);
            _twoLinkedList.RemoveNode(test);
            int actual = _twoLinkedList.GetCount();
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase(-1)]
        [TestCase(5)]
        public void RemoveNode_ReturnException(int index)
        {
            Assert.Throws<ArgumentException>(() => _twoLinkedList.RemoveNode(index));
        }
        
        [TestCase(4, 4)]
        public void RemoveNodeByIndex_ReturnCountTrue(int index, int expected)
        {
            _twoLinkedList.AddNode(33);
            _twoLinkedList.RemoveNode(index);
            int actual = _twoLinkedList.GetCount();
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase(5, 5)]
        public void AddNode_ReturnCountTrue(int value, int expected)
        {
            _twoLinkedList.AddNode(value);
            int actual = _twoLinkedList.GetCount();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        public void AddNodeAfterTen_ReturnAreEqualTrue(int value)
        {
            Node ten = _twoLinkedList.FindNode(10);
            _twoLinkedList.AddNodeAfter(ten, value);
            Node expected = ten.NextNode;
            Node actual = _twoLinkedList.FindNode(1);
            Assert.AreEqual(expected, actual);
        }
    }
}