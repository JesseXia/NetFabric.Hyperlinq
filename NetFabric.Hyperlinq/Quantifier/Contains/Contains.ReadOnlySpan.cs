using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace NetFabric.Hyperlinq
{
    public static partial class ReadOnlySpanExtensions
    {
        [Pure]
        public static bool Contains<TSource>(this ReadOnlySpan<TSource> source, TSource value, IEqualityComparer<TSource>? comparer = null)
        {
            var length = source.Length;
            if (length == 0) return false;

            if (comparer is null)
            {
                for (var index = 0; index < length; index++)
                {
                    if (EqualityComparer<TSource>.Default.Equals(value, source[index]))
                        return true;
                }
            }
            else
            {
                for (var index = 0; index < length; index++)
                {
                    if (comparer.Equals(value, source[index]))
                        return true;
                }
            }

            return false;
        }
    }
}

