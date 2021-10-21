using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Text;

namespace FastStringCreation
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchy>();
            /*
            | Method                | Mean      | Error    | StdDev   | Median    | Gen 0  | Allocated |
            | --------------------- | ---------:| --------:| --------:| ---------:| ------:| ---------:|
            | MaskNaive             | 193.44 ns | 3.951 ns | 4.704 ns | 194.75 ns | 0.1273 | 400 B     |
            | MaskUsingStringBuilder| 74.79 ns  | 0.715 ns | 0.669 ns | 74.84 ns  | 0.0587 | 184 B     |
            | MaskNewString         | 38.78 ns  | 0.815 ns | 2.176 ns | 37.92 ns  | 0.0382 | 120 B     |
            | MaskStringCreate      | 18.26 ns  | 0.431 ns | 0.618 ns | 18.03 ns  | 0.0153 | 48 B      |
            */
            Console.ReadKey();
        }
    }

    [MemoryDiagnoser]
    public class Benchy
    {
        private const string ClearValue = "Password123!";
        [Benchmark]
        public string MaskNaive()
        {
            var firstChars = ClearValue.Substring(0, 3);
            var length = ClearValue.Length - 3;

            for (int i = 0; i < length; i++)
            {
                firstChars += '*';
            }
            return firstChars;
        }
        [Benchmark]
        public string MaskUsingStringBuilder()
        {
            var firstChars = ClearValue.Substring(0, 3);
            var length = ClearValue.Length - 3;
            StringBuilder builder = new StringBuilder(firstChars);
            for (int i = 0; i < length; i++)
            {
                builder.Append('*');
            }
            return builder.ToString(); 
        }

        [Benchmark]
        public string MaskNewString()
        {
            var firstChars = ClearValue.Substring(0, 3);
            var length = ClearValue.Length - 3;
            var asterisks = new string('*', length);
            return firstChars + asterisks;
        }

        [Benchmark]
        public string MaskStringCreate()
        {
             return string.Create(ClearValue.Length, ClearValue, (span, value) =>
             {
                 value.AsSpan().CopyTo(span);
                 span[3..].Fill('*');
             });
        }
    }
}
