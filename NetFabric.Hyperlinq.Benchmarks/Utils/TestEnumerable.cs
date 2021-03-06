using System;
using System.Collections;
using System.Collections.Generic;

namespace NetFabric.Hyperlinq.Benchmarks
{
    public static class TestEnumerable
    {
        public static Enumerable ValueType(int[] array) 
            => new Enumerable(array);

        public static IEnumerable<int> ReferenceType(int[] array)
            => new ReferenceEnumerable(array);

        public class Enumerable : IEnumerable<int>
        {
            readonly int[] array;

            public Enumerable(int[] array) 
                => this.array = array;

            public Enumerator GetEnumerator() 
                => new Enumerator(array);
            IEnumerator<int> IEnumerable<int>.GetEnumerator() 
                => new Enumerator(array);
            IEnumerator IEnumerable.GetEnumerator() 
                => new Enumerator(array);

            public struct Enumerator : IEnumerator<int>
            {
                readonly int[] array;
                int index;

                public Enumerator(int[] array)
                {
                    this.array = array;
                    index = -1;
                }

                public int Current => array[index];
                object IEnumerator.Current => array[index];

                public bool MoveNext() => ++index < array.Length;

                public void Reset() => throw new NotSupportedException();

                public void Dispose() { }
            }
        }

        class ReferenceEnumerable : IEnumerable<int>
        {
            readonly int[] array;

            public ReferenceEnumerable(int[] array)
                => this.array = array;

            public IEnumerator<int> GetEnumerator()
                => new Enumerator(array);
            IEnumerator IEnumerable.GetEnumerator()
                => new Enumerator(array);

            class Enumerator : IEnumerator<int>
            {
                readonly int[] array;
                int index =  -1;

                public Enumerator(int[] array) => this.array = array;

                public int Current => array[index];
                object IEnumerator.Current => array[index];

                public bool MoveNext() => ++index < array.Length;

                public void Reset() => throw new NotSupportedException();

                public void Dispose() { }
            }
        }
    }
}