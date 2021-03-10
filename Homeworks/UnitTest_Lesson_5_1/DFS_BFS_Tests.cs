using System.Collections.Generic;
using Lesson_4_2;
using Lesson_5_1;
using NUnit.Framework;

namespace UnitTest_Lesson_5_1
{
    [TestFixture]
    public class DFS_BFS_Tests
    {
        private ITree _tree;
        private DFS_BFS_Service _dfsBfsService;
        private int[] _arrayDFS;
        private int[] _arrayBFS;

        [SetUp]
        public void Setup()
        {
            _dfsBfsService = new DFS_BFS_Service();
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
            _arrayDFS = new []{ 6, 2, 3, 11, 9, 30, 13, 18};
            _arrayBFS = new[] {6, 2, 11, 3, 9, 30, 13, 18};
        }
        
        [Test]
        public void DepthFirstSearch_AreEqual_ArrayDFSAndListValues_ReturnTrue()
        {
            List<int> actual = new List<int>();
            _dfsBfsService.DepthFirstSearch(_tree.GetRoot(), vertex =>
            {
                actual.Add(vertex.Value);
            });
            Assert.AreEqual(true, AreEqual(array:_arrayDFS, list: actual));
        }
        
        [Test]
        public void BreadthFirstSearch_AreEqual_ArrayDFSAndListValues_ReturnTrue()
        {
            List<int> actual = new List<int>();
            _dfsBfsService.BreadthFirstSearch(_tree.GetRoot(), vertex =>
            {
                actual.Add(vertex.Value);
            });
            Assert.AreEqual(true, AreEqual(array:_arrayBFS, list: actual));
        }

        private bool AreEqual(int[] array, List<int> list)
        {
            if (array.Length == list.Count)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i]!=list[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}