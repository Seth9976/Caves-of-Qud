using System;
using System.Collections.Generic;
using System.Linq;

namespace AiUnity.Common.Extensions
{
	// Token: 0x020002DE RID: 734
	public static class LinqExtensions
	{
		// Token: 0x06001945 RID: 6469 RVA: 0x00080DB7 File Offset: 0x0007EFB7
		public static IEnumerable<T> MyAppend<T>(this IEnumerable<T> list, T item)
		{
			foreach (T t in list)
			{
				yield return t;
			}
			IEnumerator<T> enumerator = null;
			yield return item;
			yield break;
			yield break;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00080DCE File Offset: 0x0007EFCE
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			return enumerable == null || !enumerable.Any<T>();
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00080DDE File Offset: 0x0007EFDE
		public static IEnumerable<T> MyPrepend<T>(this IEnumerable<T> values, T value)
		{
			yield return value;
			foreach (T t in values)
			{
				yield return t;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00080DF5 File Offset: 0x0007EFF5
		public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int count = 1)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			Queue<T> queue = new Queue<T>(count + 1);
			while (enumerator.MoveNext())
			{
				queue.Enqueue(enumerator.Current);
				if (queue.Count > count)
				{
					yield return queue.Dequeue();
				}
			}
			yield break;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00080E0C File Offset: 0x0007F00C
		public static IEnumerable<TSource> SkipUntil<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			using (IEnumerator<TSource> iterator = source.GetEnumerator())
			{
				while (iterator.MoveNext())
				{
					if (predicate(iterator.Current))
					{
						IL_00AB:
						while (iterator.MoveNext())
						{
							TSource tsource = iterator.Current;
							yield return tsource;
						}
						goto JumpOutOfTryFinally-3;
					}
				}
				goto IL_00AB;
			}
			JumpOutOfTryFinally-3:
			IEnumerator<TSource> iterator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00080E23 File Offset: 0x0007F023
		public static IEnumerable<T> Yield<T>(this T item)
		{
			if (item == null)
			{
				yield break;
			}
			yield return item;
			yield break;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00080E34 File Offset: 0x0007F034
		public static void InsertRelative<T>(this IList<T> source, IList<T> reference, params T[] items)
		{
			foreach (T t in items)
			{
				int num = reference.IndexOf(t);
				int num2 = 0;
				while (num2 < source.Count<T>() && (num < 0 || num >= reference.IndexOf(source[num2])))
				{
					num2++;
				}
				source.Insert(num2, t);
			}
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00080E94 File Offset: 0x0007F094
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
		{
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				return defaultValue;
			}
			return tvalue;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00080EB0 File Offset: 0x0007F0B0
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueProvider)
		{
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				return defaultValueProvider();
			}
			return tvalue;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00080ED0 File Offset: 0x0007F0D0
		public static bool ScrambledEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
		{
			if (list2 == null || list1.Count<T>() != list2.Count<T>())
			{
				return false;
			}
			Dictionary<T, int> dictionary = new Dictionary<T, int>();
			foreach (T t in list1)
			{
				if (dictionary.ContainsKey(t))
				{
					Dictionary<T, int> dictionary2 = dictionary;
					T t2 = t;
					int num = dictionary2[t2];
					dictionary2[t2] = num + 1;
				}
				else
				{
					dictionary.Add(t, 1);
				}
			}
			foreach (T t3 in list2)
			{
				if (!dictionary.ContainsKey(t3))
				{
					return false;
				}
				Dictionary<T, int> dictionary3 = dictionary;
				T t2 = t3;
				int num = dictionary3[t2];
				dictionary3[t2] = num - 1;
			}
			return dictionary.Values.All<int>((int c) => c == 0);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00080FDC File Offset: 0x0007F1DC
		public static int IndexOf<T>(this IEnumerable<T> list, T item)
		{
			return list.Select<T, int>(delegate(T x, int index)
			{
				if (!EqualityComparer<T>.Default.Equals(item, x))
				{
					return -1;
				}
				return index;
			}).FirstOrDefault((int x) => x != -1, -1);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0008102D File Offset: 0x0007F22D
		public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T alternate)
		{
			return source.Where<T>(predicate).FirstOrDefault(alternate);
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0008103C File Offset: 0x0007F23C
		public static T FirstOrDefault<T>(this IEnumerable<T> source, T alternate)
		{
			return source.DefaultIfEmpty(alternate).First<T>();
		}
	}
}
