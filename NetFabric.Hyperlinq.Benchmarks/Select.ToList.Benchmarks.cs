using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace NetFabric.Hyperlinq.Benchmarks
{
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.GitHub]
    public class SelectToListBenchmarks : BenchmarksBase
    {
        [BenchmarkCategory("Range")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_Range() 
            => System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(linqRange, item => item));

        [BenchmarkCategory("Queue")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_Queue() 
            => System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(queue, item => item));

        [BenchmarkCategory("Array")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_Array() 
            => System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(array, item => item));

        [BenchmarkCategory("List")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_List() 
            => System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(list, item => item));

        [BenchmarkCategory("Enumerable_Reference")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_Enumerable_Reference()
            => System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(enumerableReference, item => item));

        [BenchmarkCategory("Enumerable_Value")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_Enumerable_Value()
            => System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(enumerableValue, item => item));

        [BenchmarkCategory("Range")]
        [Benchmark]
        public List<int> Hyperlinq_Range() 
            => hyperlinqRange.Select(item => item).ToList();

        [BenchmarkCategory("Queue")]
        [Benchmark]
        public List<int> Hyperlinq_Queue() 
            => queue.Select(item => item).ToList();

        [BenchmarkCategory("Array")]
        [Benchmark]
        public List<int> Hyperlinq_Array() 
            => array.Select(item => item).ToList();

        [BenchmarkCategory("List")]
        [Benchmark]
        public List<int> Hyperlinq_List() 
            => list.Select(item => item).ToList();

        [BenchmarkCategory("Enumerable_Reference")]
        [Benchmark]
        public List<int> Hyperlinq_Enumerable_Reference()
            => enumerableReference.AsValueEnumerable().Select(item => item).ToList();

        [BenchmarkCategory("Enumerable_Value")]
        [Benchmark]
        public List<int> Hyperlinq_Enumerable_Value()        
            => enumerableValue.AsValueEnumerable<TestEnumerable.Enumerable, TestEnumerable.Enumerable.Enumerator, int>().Select(item => item).ToList();
    }
}
