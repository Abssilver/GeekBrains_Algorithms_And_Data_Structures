using BenchmarkDotNet.Running;

namespace Lesson_4_1
{
    class Program
    {
        /*
        BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1379 (1909/November2018Update/19H2)
        Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
            .NET Core SDK=5.0.103
        [Host]     : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT
        DefaultJob : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT


        |                       Method |         Mean |      Error |     StdDev |       Median |
        |----------------------------- |-------------:|-----------:|-----------:|-------------:|
        |     Test_Search_ValueInArray |   892.749 us |  4.4470 us |  3.9421 us |   892.596 us |
        |   Test_Search_ValueInHashSet |     2.971 us |  0.0212 us |  0.0198 us |     2.972 us |
        |   Test_Search_NoValueInArray | 1,208.180 us | 23.9491 us | 56.9175 us | 1,234.391 us |
        | Test_Search_NoValueInHashSet |    27.811 us |  0.2871 us |  0.2685 us |    27.689 us |
        */

        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(SearchTests).Assembly).Run(args);
        }
    }
}