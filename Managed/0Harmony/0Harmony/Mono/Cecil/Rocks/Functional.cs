using System;
using System.Collections.Generic;

namespace Mono.Cecil.Rocks
{
	// Token: 0x02000416 RID: 1046
	internal static class Functional
	{
		// Token: 0x0600160B RID: 5643 RVA: 0x00046EBC File Offset: 0x000450BC
		public static Func<A, R> Y<A, R>(Func<Func<A, R>, Func<A, R>> f)
		{
			Func<A, R> g = null;
			g = f((A a) => g(a));
			return g;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00046EF4 File Offset: 0x000450F4
		public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource element)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Functional.PrependIterator<TSource>(source, element);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00046F0B File Offset: 0x0004510B
		private static IEnumerable<TSource> PrependIterator<TSource>(IEnumerable<TSource> source, TSource element)
		{
			yield return element;
			foreach (TSource tsource in source)
			{
				yield return tsource;
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}
	}
}
