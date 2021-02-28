using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Lesson_3_1
{
    /*
    BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1379 (1909/November2018Update/19H2)
    Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
    .NET Core SDK=5.0.103
    [Host]     : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT
    DefaultJob : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT

    |                      Method |     Mean |    Error |   StdDev |
    |---------------------------- |---------:|---------:|---------:|
    |         Test_Standart_Class | 21.06 ns | 0.058 ns | 0.054 ns |
    |  Test_Standart_Struct_Float | 21.79 ns | 0.082 ns | 0.077 ns |
    | Test_Standart_Struct_Double | 22.44 ns | 0.114 ns | 0.096 ns |
    |    Test_NoRoot_Struct_Float | 27.60 ns | 0.152 ns | 0.135 ns |
    */

    class DistanceCalculationsPerformanceTests
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(DistanceCalculationsPerformanceTests).Assembly).Run(args);
        }
    }
    
    public class BenchmarkClass
    {
        private readonly PointClass[,] _pointClassTestCases;
        private readonly PointStruct_Float[,] _pointStructFloatsTestCases;
        private readonly PointStruct_Double[,] _pointStructDoublesTestCases;
        
        public BenchmarkClass()
        {
            _pointClassTestCases = GenerateTestCase(5, 5, 5, CreatePointClass);
            _pointStructFloatsTestCases = GenerateTestCase(5, 5, 5, CreatePointFloatStruct);
            _pointStructDoublesTestCases = GenerateTestCase(5, 5, 5, CreatePointDoubleStruct);
        }

        #region GenerateData
        
        private T[,] GenerateTestCase<T>(int min, int max, int count, Func<int, int, T> createTFunc) where T: ITestData
        {
            Random rnd = new Random();
            T[,] array = new T[count,2];
            
            for (int i = 0; i < count; i++)
            {
                int first = rnd.Next(min, max);
                int second = rnd.Next(min, max);
                array[i, 0] = createTFunc.Invoke(first, second);
                array[i, 1] = createTFunc.Invoke(second, first);
            }
            return array;
        }
        static PointClass CreatePointClass(int firstValue, int secondValue) => new()
        {
            X = secondValue == 0 ? 0 : (float) firstValue / secondValue,
            Y = firstValue == 0 ? 0 : (float) secondValue / firstValue,
        };
        static PointStruct_Float CreatePointFloatStruct(int firstValue, int secondValue) => new()
        {
            X = secondValue == 0 ? 0 : (float) firstValue / secondValue,
            Y = firstValue == 0 ? 0 : (float) secondValue / firstValue,
        };
        static PointStruct_Double CreatePointDoubleStruct(int firstValue, int secondValue) => new()
        {
            X = secondValue == 0 ? 0 : (double) firstValue / secondValue,
            Y = firstValue == 0 ? 0 : (double) secondValue / firstValue,
        };

        #endregion

        #region Methods
        
        public static float FastInvSqrt(float z)
        {
            if (z == 0) return 0;
            FloatIntUnion u;
            u.i = 0;
            u.f = z;
            u.i -= 1 << 23; /* Subtract 2^m. */
            u.i >>= 1; /* Divide by 2. */
            u.i += 1 << 29; /* Add ((b + 1) / 2) * 2^m. */
            return u.f;
        }

        public float StandartPointDistanceCalc(PointClass pointOne, PointClass pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }
        
        public float StandartPointDistanceCalc(PointStruct_Float pointOne, PointStruct_Float pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }
        
        public double StandartPointDistanceCalc(PointStruct_Double pointOne, PointStruct_Double pointTwo)
        {
            double x = pointOne.X - pointTwo.X;
            double y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }
        public float NoRootPointDistanceCalc(PointStruct_Float pointOne, PointStruct_Float pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return FastInvSqrt((x * x) + (y * y));
        }
        
        #endregion

        #region Tests

        [Benchmark]
        public void Test_Standart_Class()
        {
            for (int i = 0; i < _pointClassTestCases.GetLength(0); i++)
            {
                StandartPointDistanceCalc(
                    _pointClassTestCases[i, 0],
                    _pointClassTestCases[i, 1]
                );
            }
        }

        [Benchmark]
        public void Test_Standart_Struct_Float()
        {
            for (int i = 0; i < _pointStructFloatsTestCases.GetLength(0); i++)
            {
                StandartPointDistanceCalc(
                    _pointStructFloatsTestCases[i, 0],
                    _pointStructFloatsTestCases[i, 1]
                );
            }
        }
        
        [Benchmark]
        public void Test_Standart_Struct_Double()
        {
            for (int i = 0; i < _pointStructDoublesTestCases.GetLength(0); i++)
            {
                StandartPointDistanceCalc(
                    _pointStructDoublesTestCases[i, 0],
                    _pointStructDoublesTestCases[i, 1]
                );
            }
        }
        
        [Benchmark]
        public void Test_NoRoot_Struct_Float()
        {
            for (int i = 0; i < _pointStructFloatsTestCases.GetLength(0); i++)
            {
                NoRootPointDistanceCalc(
                    _pointStructFloatsTestCases[i, 0],
                    _pointStructFloatsTestCases[i, 1]
                );
            }
        }
        #endregion

    }
    
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct FloatIntUnion
    {
        [FieldOffset(0)]
        public int i;

        [FieldOffset(0)]
        public float f;
    }
    public class PointClass: ITestData
    {
        public float X;
        public float Y;
    }
    public struct PointStruct_Float: ITestData
    {
        public float X;
        public float Y;
    }
    public struct PointStruct_Double: ITestData
    {
        public double X;
        public double Y;
    }

    public interface ITestData
    {
    }
}