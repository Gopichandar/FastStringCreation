﻿using BenchmarkDotNet.Attributes;
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
            return builder.ToString(); ;
        }
    }
}