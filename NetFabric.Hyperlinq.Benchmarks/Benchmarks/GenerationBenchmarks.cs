using System;
using System.Collections;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using StructLinq;

namespace NetFabric.Hyperlinq.Benchmarks
{
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    public class GenerationOperationsBenchmarks : CountBenchmarksBase
    {
        [BenchmarkCategory("Range")]
        [Benchmark(Baseline = true)]
        public int Linq_Range() 
        {
            var sum = 0;
            foreach(var item in System.Linq.Enumerable.Range(0, Count))
                sum += item;
            return sum;
        }

        [BenchmarkCategory("Repeat")]
        [Benchmark]
        public int Linq_Repeat()
        {
            var sum = 0;
            using (var enumerator = System.Linq.EnumerableEx.Repeat(1).GetEnumerator())
            {
                for (var counter = Count; counter != 0; counter--)
                {
                    _ = enumerator.MoveNext();
                    sum += enumerator.Current;
                }
            }
            return sum;
        }

        [BenchmarkCategory("Repeat")]
        [Benchmark(Baseline = true)]
        public int Linq_Repeat_Count() 
        {
            var sum = 0;
            foreach(var item in System.Linq.Enumerable.Repeat(1, Count))
                sum += item;
            return sum;
        }

        [BenchmarkCategory("Return")]
        [Benchmark(Baseline = true)]
        public int Linq_Return()
        {
            var sum = 0;
            foreach (var item in System.Linq.EnumerableEx.Return(1))
                sum += item;
            return sum;
        }

        [BenchmarkCategory("Create")]
        [Benchmark(Baseline = true)]
        public int Linq_Create() 
        {
            var sum = 0;
            foreach(var item in System.Linq.EnumerableEx.Create(() => new Enumerator(1, Count)))
                sum += item;
            return sum;
        }

        // ---------------------------------------------------------------------

        [BenchmarkCategory("Range")]
        [Benchmark]
        public int StructLinq()
        {
            var sum = 0;
            foreach (var item in StructEnumerable.Range(0, Count))
                sum += item;
            return sum;
        }

        // ---------------------------------------------------------------------

        [BenchmarkCategory("Range")]
        [Benchmark]
        public int Hyperlinq_Range() 
        {
            var sum = 0;
            foreach (var item in ValueEnumerable.Range(0, Count))
                sum += item;
            return sum;
        }    

        [BenchmarkCategory("Repeat")]
        [Benchmark]
        public int Hyperlinq_Repeat() 
        {
            var sum = 0;
            foreach(var item in ValueEnumerable.Repeat(1, Count))
                sum += item;
            return sum;
        }

#pragma warning disable HLQ010 // Consider using a 'for' loop instead.
        [BenchmarkCategory("Return")]
        [Benchmark]
        public int Hyperlinq_Return()
        {
            var sum = 0;
            foreach (var item in ValueEnumerable.Return(1))
                sum += item;
            return sum;
        }
#pragma warning restore HLQ010 // Consider using a 'for' loop instead.

        [BenchmarkCategory("Create")]
        [Benchmark]
        public int Hyperlinq_Create()
        {
            var sum = 0;
            foreach (var item in ValueEnumerable.Create<Enumerator, int>(() => new Enumerator(1, Count)))
                sum += item;
            return sum;
        }

        struct Enumerator : IEnumerator<int>
        {
            int counter;

            public Enumerator(int value, int count)
            {
                Current = value;
                counter = count;
            }

            public int Current { get; private set; }
            readonly object IEnumerator.Current 
                => Current;

            public bool MoveNext() => counter-- > 0;

            public readonly void Reset() => throw new NotSupportedException();

            public readonly void Dispose() { }
        }

    }
}