using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine.Bindings;

namespace Unity.Collections
{
	// Token: 0x02000097 RID: 151
	[VisibleToOtherModules]
	internal static class CollectionExtensions
	{
		// Token: 0x06000322 RID: 802 RVA: 0x00006378 File Offset: 0x00004578
		internal static void AddSorted<T>([DisallowNull] this List<T> list, T item, IComparer<T> comparer = null)
		{
			bool flag = list == null;
			if (flag)
			{
				throw new ArgumentNullException("list must not be null.");
			}
			if (comparer == null)
			{
				comparer = Comparer<T>.Default;
			}
			bool flag2 = list.Count == 0;
			if (flag2)
			{
				list.Add(item);
			}
			else
			{
				bool flag3 = comparer.Compare(list[list.Count - 1], item) <= 0;
				if (flag3)
				{
					list.Add(item);
				}
				else
				{
					bool flag4 = comparer.Compare(list[0], item) >= 0;
					if (flag4)
					{
						list.Insert(0, item);
					}
					else
					{
						int num = list.BinarySearch(item, comparer);
						bool flag5 = num < 0;
						if (flag5)
						{
							num = ~num;
						}
						list.Insert(num, item);
					}
				}
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000642C File Offset: 0x0000462C
		public static void Fill<T>([DisallowNull] this List<T> dest, T value, int count)
		{
			bool flag = dest == null;
			if (flag)
			{
				throw new ArgumentNullException("dest");
			}
			dest.Capacity = Math.Max(dest.Capacity, dest.Count + count);
			while (count-- > 0)
			{
				dest.Add(value);
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00006484 File Offset: 0x00004684
		public static T FirstOrDefaultSorted<T>(this IEnumerable<T> collection, IComparer<T> comparer = null)
		{
			bool flag = collection == null;
			if (flag)
			{
				throw new ArgumentNullException("collection must not be null.");
			}
			if (comparer == null)
			{
				comparer = Comparer<T>.Default;
			}
			bool flag2 = false;
			T t = default(T);
			foreach (T t2 in collection)
			{
				bool flag3 = !flag2;
				if (flag3)
				{
					t = t2;
					flag2 = true;
				}
				bool flag4 = comparer.Compare(t2, t) < 0;
				if (flag4)
				{
					t = t2;
				}
			}
			return t;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00006520 File Offset: 0x00004720
		internal static string SerializedView<T>([DisallowNull] this IEnumerable<T> collection, [DisallowNull] Func<T, string> serializeElement)
		{
			bool flag = collection == null;
			if (flag)
			{
				throw new ArgumentNullException("collection must not be null.");
			}
			bool flag2 = serializeElement == null;
			if (flag2)
			{
				throw new ArgumentNullException("Argument serializeElement must not be null.");
			}
			return "[" + string.Join(",", collection.Select<T, string>((T t) => (t == null) ? "null" : serializeElement(t))) + "]";
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00006598 File Offset: 0x00004798
		internal static bool ContainsByEquals<T>([DisallowNull] this IEnumerable<T> collection, T element)
		{
			bool flag = collection == null;
			if (flag)
			{
				throw new ArgumentNullException("collection must not be null.");
			}
			foreach (T t in collection)
			{
				bool flag2 = t.Equals(element);
				if (flag2)
				{
					return true;
				}
			}
			return false;
		}
	}
}
