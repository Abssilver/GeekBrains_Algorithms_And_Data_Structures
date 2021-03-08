using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Lesson_4_1
{
    public class SearchTests
    {
        private readonly string [] _array;
        private readonly HashSet<string> _hashSet;
        private readonly Random _generator;
        private readonly int _testCases;

        public SearchTests()
        {
            _array = new string[10000];
            _hashSet = new HashSet<string>();
            _generator = new Random();
            _testCases = 50;
            GenerateTestStringData(ref _array, _hashSet);
        }

        #region GenerateData
        private void GenerateTestStringData(ref string[] array, HashSet<string> hashSet)
        {
            if (array == null)
                array = new string[10000];
            
            if (hashSet == null)
                hashSet = new HashSet<string>();
            else hashSet.Clear();

            for (int i = 0; i < array.Length; i++)
            {
                string newWord = GenerateString(_generator.Next(30, 40));
                
                while (hashSet.Contains(newWord))
                {
                    newWord = GenerateString(_generator.Next(30, 40));
                }

                hashSet.Add(newWord);
                array[i] = newWord;
            }
        }

        private string GenerateString(int length)
        {
            int firstLetter = 'a';
            int lastLetter = 'z';
            char[] word = new char[length];
            for (int j = 0; j < length; j++)
            {
                word[j] = (char)_generator.Next(firstLetter, lastLetter + 1);
            }
            return new string(word);
        }
        
        #endregion

        #region Methods

        private bool SearchInArray(string value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        private bool SearchInHashSet(string value) => _hashSet.Contains(value);

        #endregion

        #region Tests

        [Benchmark]
        public void Test_Search_ValueInArray()
        {
            for (int i = 0; i < _testCases; i++)
            {
                int randomIndex = _generator.Next(0, _array.Length);
                SearchInArray(_array[randomIndex]);
            }
        }
        
        [Benchmark]
        public void Test_Search_ValueInHashSet()
        {
            for (int i = 0; i < _testCases; i++)
            {
                int randomIndex = _generator.Next(0, _array.Length);
                SearchInHashSet(_array[randomIndex]);
            }
        }

        [Benchmark]
        public void Test_Search_NoValueInArray()
        {
            for (int i = 0; i < _testCases; i++)
            {
                string valueIsNotPresented = GenerateString(45);
                SearchInArray(valueIsNotPresented);
            }
        }
        
        [Benchmark]
        public void Test_Search_NoValueInHashSet()
        {
            for (int i = 0; i < _testCases; i++)
            {
                string valueIsNotPresented = GenerateString(45);
                SearchInHashSet(valueIsNotPresented);
            }
        }
        
        #endregion
    }
}