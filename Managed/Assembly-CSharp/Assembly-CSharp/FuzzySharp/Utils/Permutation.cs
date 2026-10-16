using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzySharp.Utils
{
	// Token: 0x020001AD RID: 429
	public static class Permutation
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x00065150 File Offset: 0x00063350
		public static List<List<T>> AllPermutations<T>(this IEnumerable<T> seed)
		{
			List<T> list = new List<T>(seed);
			return Permutation.Permute<T>(list, 0, list.Count - 1).ToList<List<T>>();
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00065178 File Offset: 0x00063378
		public static List<List<T>> PermutationsOfSize<T>(this IEnumerable<T> seed, int size)
		{
			if (seed.Count<T>() < size)
			{
				return new List<List<T>>();
			}
			return seed.PermutationsOfSize<T>(new List<T>(), size).ToList<List<T>>();
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0006519A File Offset: 0x0006339A
		private static IEnumerable<List<T>> PermutationsOfSize<T>(this IEnumerable<T> seed, List<T> set, int size)
		{
			if (size == 0)
			{
				foreach (List<T> list in set.AllPermutations<T>())
				{
					yield return list;
				}
				List<List<T>>.Enumerator enumerator = default(List<List<T>>.Enumerator);
			}
			else
			{
				List<T> seedAsList = seed.ToList<T>();
				int num;
				for (int i = 0; i < seedAsList.Count; i = num + 1)
				{
					List<T> list2 = new List<T>(set) { seedAsList[i] };
					foreach (List<T> list3 in seedAsList.Skip<T>(i + 1).PermutationsOfSize<T>(list2, size - 1))
					{
						yield return list3;
					}
					IEnumerator<List<T>> enumerator2 = null;
					num = i;
				}
				seedAsList = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000651B8 File Offset: 0x000633B8
		private static IEnumerable<List<T>> Permute<T>(List<T> set, int start, int end)
		{
			if (start == end)
			{
				yield return new List<T>(set);
			}
			else
			{
				int num;
				for (int i = start; i <= end; i = num + 1)
				{
					Permutation.Swap<T>(set, start, i);
					foreach (List<T> list in Permutation.Permute<T>(set, start + 1, end))
					{
						yield return list;
					}
					IEnumerator<List<T>> enumerator = null;
					Permutation.Swap<T>(set, start, i);
					num = i;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x000651D8 File Offset: 0x000633D8
		private static void Swap<T>(List<T> set, int a, int b)
		{
			T t = set[a];
			set[a] = set[b];
			set[b] = t;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00065203 File Offset: 0x00063403
		public static IEnumerable<List<T>> Cycles<T>(IEnumerable<T> seed)
		{
			LinkedList<T> set = new LinkedList<T>(seed);
			int num;
			for (int i = 0; i < set.Count; i = num + 1)
			{
				yield return new List<T>(set);
				T t = set.First<T>();
				set.RemoveFirst();
				set.AddLast(t);
				num = i;
			}
			yield break;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00065213 File Offset: 0x00063413
		public static bool IsPermutationOf<T>(this IEnumerable<T> set, IEnumerable<T> other)
		{
			return new HashSet<T>(set).SetEquals(other);
		}
	}
}
