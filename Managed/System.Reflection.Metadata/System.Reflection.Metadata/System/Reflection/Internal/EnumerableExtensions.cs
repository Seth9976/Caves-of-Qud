using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000171 RID: 369
	[NullableContext(1)]
	[Nullable(0)]
	internal static class EnumerableExtensions
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x00022B5C File Offset: 0x00020D5C
		[NullableContext(2)]
		public static T FirstOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> collection, [Nullable(1)] Func<T, bool> predicate)
		{
			foreach (T t in collection)
			{
				if (predicate.Invoke(t))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00022B98 File Offset: 0x00020D98
		public static IEnumerable<TResult> Select<[Nullable(2)] TSource, [Nullable(2)] TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			foreach (TSource tsource in source)
			{
				yield return selector.Invoke(tsource);
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00022BAF File Offset: 0x00020DAF
		public static T Last<[Nullable(2)] T>(this ImmutableArray<T>.Builder source)
		{
			return source[source.Count - 1];
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00022BBF File Offset: 0x00020DBF
		public static IEnumerable<T> OrderBy<[Nullable(2)] T>(this List<T> source, Comparison<T> comparison)
		{
			int[] array = new int[source.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i;
			}
			Array.Sort<int>(array, delegate(int left, int right)
			{
				if (left == right)
				{
					return 0;
				}
				int num2 = comparison.Invoke(source[left], source[right]);
				if (num2 == 0)
				{
					return left - right;
				}
				return num2;
			});
			foreach (int num in array)
			{
				yield return source[num];
			}
			int[] array2 = null;
			yield break;
		}
	}
}
