using System;
using System.Collections.Generic;
using System.Linq;

namespace HarmonyLib
{
	// Token: 0x02000192 RID: 402
	public static class CollectionExtensions
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x0001671C File Offset: 0x0001491C
		public static void Do<T>(this IEnumerable<T> sequence, Action<T> action)
		{
			if (sequence == null)
			{
				return;
			}
			foreach (T t in sequence)
			{
				action(t);
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001674A File Offset: 0x0001494A
		public static void DoIf<T>(this IEnumerable<T> sequence, Func<T, bool> condition, Action<T> action)
		{
			sequence.Where<T>(condition).Do<T>(action);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00016759 File Offset: 0x00014959
		public static IEnumerable<T> AddItem<T>(this IEnumerable<T> sequence, T item)
		{
			return (sequence ?? Enumerable.Empty<T>()).Concat<T>(new T[] { item });
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00016778 File Offset: 0x00014978
		public static T[] AddToArray<T>(this T[] sequence, T item)
		{
			return sequence.AddItem(item).ToArray<T>();
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00016788 File Offset: 0x00014988
		public static T[] AddRangeToArray<T>(this T[] sequence, T[] items)
		{
			return (sequence ?? Enumerable.Empty<T>()).Concat<T>(items).ToArray<T>();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000167AC File Offset: 0x000149AC
		internal static Dictionary<K, V> Merge<K, V>(this IEnumerable<KeyValuePair<K, V>> firstDict, params IEnumerable<KeyValuePair<K, V>>[] otherDicts)
		{
			Dictionary<K, V> dictionary = new Dictionary<K, V>();
			foreach (KeyValuePair<K, V> keyValuePair in firstDict)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			for (int i = 0; i < otherDicts.Length; i++)
			{
				foreach (KeyValuePair<K, V> keyValuePair2 in otherDicts[i])
				{
					dictionary[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			return dictionary;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00016864 File Offset: 0x00014A64
		internal static Dictionary<K, V> TransformKeys<K, V>(this Dictionary<K, V> origDict, Func<K, K> transform)
		{
			Dictionary<K, V> dictionary = new Dictionary<K, V>();
			foreach (KeyValuePair<K, V> keyValuePair in origDict)
			{
				dictionary.Add(transform(keyValuePair.Key), keyValuePair.Value);
			}
			return dictionary;
		}
	}
}
