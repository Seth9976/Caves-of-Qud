using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	internal static class EnumerableExtensions
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public static bool SequenceEqual<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] this IEnumerable<T> first, [Nullable(new byte[] { 2, 1 })] IEnumerable<T> second, Func<T, T, bool> comparer)
		{
			if (first == second)
			{
				return true;
			}
			if (first == null || second == null)
			{
				return false;
			}
			using (IEnumerator<T> enumerator = first.GetEnumerator())
			{
				using (IEnumerator<T> enumerator2 = second.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator2.MoveNext() || !comparer.Invoke(enumerator.Current, enumerator2.Current))
						{
							return false;
						}
					}
					if (enumerator2.MoveNext())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E54 File Offset: 0x00001054
		[return: Nullable(2)]
		public static T AggregateOrDefault<[Nullable(2)] T>(this IEnumerable<T> source, Func<T, T, T> func)
		{
			T t;
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					t = default(T);
					t = t;
				}
				else
				{
					T t2 = enumerator.Current;
					while (enumerator.MoveNext())
					{
						T t3 = enumerator.Current;
						t2 = func.Invoke(t2, t3);
					}
					t = t2;
				}
			}
			return t;
		}
	}
}
