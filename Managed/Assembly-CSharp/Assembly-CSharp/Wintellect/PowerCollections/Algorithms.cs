using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Genkit;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002F1 RID: 753
	public static class Algorithms
	{
		// Token: 0x06001991 RID: 6545 RVA: 0x0008185C File Offset: 0x0007FA5C
		public static IList<T> Range<T>(IList<T> list, int start, int count)
		{
			if (list == null)
			{
				throw new ArgumentOutOfRangeException("list");
			}
			if (start < 0 || start > list.Count || (start == list.Count && count != 0))
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (count < 0 || count > list.Count || count + start > list.Count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new Algorithms.ListRange<T>(list, start, count);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000818C8 File Offset: 0x0007FAC8
		public static IList<T> Range<T>(T[] array, int start, int count)
		{
			if (array == null)
			{
				throw new ArgumentOutOfRangeException("array");
			}
			if (start < 0 || start > array.Length || (start == array.Length && count != 0))
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (count < 0 || count > array.Length || count + start > array.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new Algorithms.ArrayRange<T>(array, start, count);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00081926 File Offset: 0x0007FB26
		public static ICollection<T> ReadOnly<T>(ICollection<T> collection)
		{
			if (collection == null)
			{
				return null;
			}
			return new Algorithms.ReadOnlyCollection<T>(collection);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00081933 File Offset: 0x0007FB33
		public static IList<T> ReadOnly<T>(IList<T> list)
		{
			if (list == null)
			{
				return null;
			}
			if (list.IsReadOnly)
			{
				return list;
			}
			return new Algorithms.ReadOnlyList<T>(list);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0008194A File Offset: 0x0007FB4A
		public static IDictionary<TKey, TValue> ReadOnly<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				return null;
			}
			if (dictionary.IsReadOnly)
			{
				return dictionary;
			}
			return new Algorithms.ReadOnlyDictionary<TKey, TValue>(dictionary);
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00081961 File Offset: 0x0007FB61
		public static IEnumerable<T> TypedAs<T>(IEnumerable untypedCollection)
		{
			if (untypedCollection == null)
			{
				return null;
			}
			if (untypedCollection is IEnumerable<T>)
			{
				return (IEnumerable<T>)untypedCollection;
			}
			return new Algorithms.TypedEnumerable<T>(untypedCollection);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0008197D File Offset: 0x0007FB7D
		public static ICollection<T> TypedAs<T>(ICollection untypedCollection)
		{
			if (untypedCollection == null)
			{
				return null;
			}
			if (untypedCollection is ICollection<T>)
			{
				return (ICollection<T>)untypedCollection;
			}
			return new Algorithms.TypedCollection<T>(untypedCollection);
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00081999 File Offset: 0x0007FB99
		public static IList<T> TypedAs<T>(IList untypedList)
		{
			if (untypedList == null)
			{
				return null;
			}
			if (untypedList is IList<T>)
			{
				return (IList<T>)untypedList;
			}
			return new Algorithms.TypedList<T>(untypedList);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000819B5 File Offset: 0x0007FBB5
		public static ICollection Untyped<T>(ICollection<T> typedCollection)
		{
			if (typedCollection == null)
			{
				return null;
			}
			if (typedCollection is ICollection)
			{
				return (ICollection)typedCollection;
			}
			return new Algorithms.UntypedCollection<T>(typedCollection);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000819D1 File Offset: 0x0007FBD1
		public static IList Untyped<T>(IList<T> typedList)
		{
			if (typedList == null)
			{
				return null;
			}
			if (typedList is IList)
			{
				return (IList)typedList;
			}
			return new Algorithms.UntypedList<T>(typedList);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000819ED File Offset: 0x0007FBED
		public static IList<T> ReadWriteList<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return new Algorithms.ArrayWrapper<T>(array);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00081A03 File Offset: 0x0007FC03
		public static IEnumerable<T> Replace<T>(IEnumerable<T> collection, T itemFind, T replaceWith)
		{
			return Algorithms.Replace<T>(collection, itemFind, replaceWith, EqualityComparer<T>.Default);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00081A12 File Offset: 0x0007FC12
		public static IEnumerable<T> Replace<T>(IEnumerable<T> collection, T itemFind, T replaceWith, IEqualityComparer<T> equalityComparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			foreach (T t in collection)
			{
				if (equalityComparer.Equals(t, itemFind))
				{
					yield return replaceWith;
				}
				else
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00081A37 File Offset: 0x0007FC37
		public static IEnumerable<T> Replace<T>(IEnumerable<T> collection, Predicate<T> predicate, T replaceWith)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T t in collection)
			{
				if (predicate(t))
				{
					yield return replaceWith;
				}
				else
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00081A55 File Offset: 0x0007FC55
		public static void ReplaceInPlace<T>(IList<T> list, T itemFind, T replaceWith)
		{
			Algorithms.ReplaceInPlace<T>(list, itemFind, replaceWith, EqualityComparer<T>.Default);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00081A64 File Offset: 0x0007FC64
		public static void ReplaceInPlace<T>(IList<T> list, T itemFind, T replaceWith, IEqualityComparer<T> equalityComparer)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			if (list is T[])
			{
				list = new Algorithms.ArrayWrapper<T>((T[])list);
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (equalityComparer.Equals(list[i], itemFind))
				{
					list[i] = replaceWith;
				}
			}
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00081AE8 File Offset: 0x0007FCE8
		public static void ReplaceInPlace<T>(IList<T> list, Predicate<T> predicate, T replaceWith)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (list is T[])
			{
				list = new Algorithms.ArrayWrapper<T>((T[])list);
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (predicate(list[i]))
				{
					list[i] = replaceWith;
				}
			}
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00081B68 File Offset: 0x0007FD68
		public static IEnumerable<T> RemoveDuplicates<T>(IEnumerable<T> collection)
		{
			return Algorithms.RemoveDuplicates<T>(collection, EqualityComparer<T>.Default);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00081B75 File Offset: 0x0007FD75
		public static IEnumerable<T> RemoveDuplicates<T>(IEnumerable<T> collection, IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			return Algorithms.RemoveDuplicates<T>(collection, new BinaryPredicate<T>(equalityComparer.Equals));
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00081B98 File Offset: 0x0007FD98
		public static IEnumerable<T> RemoveDuplicates<T>(IEnumerable<T> collection, BinaryPredicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			T current = default(T);
			bool flag = true;
			foreach (T t in collection)
			{
				if (flag || !predicate(current, t))
				{
					current = t;
					yield return t;
				}
				flag = false;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00081BAF File Offset: 0x0007FDAF
		public static void RemoveDuplicatesInPlace<T>(IList<T> list)
		{
			Algorithms.RemoveDuplicatesInPlace<T>(list, EqualityComparer<T>.Default);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00081BBC File Offset: 0x0007FDBC
		public static void RemoveDuplicatesInPlace<T>(IList<T> list, IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			Algorithms.RemoveDuplicatesInPlace<T>(list, new BinaryPredicate<T>(equalityComparer.Equals));
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00081BE0 File Offset: 0x0007FDE0
		public static void RemoveDuplicatesInPlace<T>(IList<T> list, BinaryPredicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (list is T[])
			{
				list = new Algorithms.ArrayWrapper<T>((T[])list);
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			T t = default(T);
			int i = -1;
			int j = 0;
			int num = list.Count;
			while (j < num)
			{
				T t2 = list[j];
				if (i < 0 || !predicate(t, t2))
				{
					t = t2;
					i++;
					if (i != j)
					{
						list[i] = t;
					}
				}
				j++;
			}
			i++;
			if (i < num)
			{
				if (!(list is Algorithms.ArrayWrapper<T>))
				{
					if (!(list is IList) || !((IList)list).IsFixedSize)
					{
						while (i < num)
						{
							list.RemoveAt(num - 1);
							num--;
						}
						return;
					}
				}
				while (i < num)
				{
					list[i++] = default(T);
				}
				return;
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00081CD5 File Offset: 0x0007FED5
		public static int FirstConsecutiveEqual<T>(IList<T> list, int count)
		{
			return Algorithms.FirstConsecutiveEqual<T>(list, count, EqualityComparer<T>.Default);
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00081CE3 File Offset: 0x0007FEE3
		public static int FirstConsecutiveEqual<T>(IList<T> list, int count, IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			return Algorithms.FirstConsecutiveEqual<T>(list, count, new BinaryPredicate<T>(equalityComparer.Equals));
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00081D08 File Offset: 0x0007FF08
		public static int FirstConsecutiveEqual<T>(IList<T> list, int count, BinaryPredicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (count < 1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (list.Count < count)
			{
				return -1;
			}
			if (count == 1)
			{
				return 0;
			}
			int num = 0;
			int num2 = 0;
			T t = default(T);
			int num3 = 0;
			foreach (T t2 in list)
			{
				if (num2 > 0 && predicate(t, t2))
				{
					num3++;
					if (num3 >= count)
					{
						return num;
					}
				}
				else
				{
					t = t2;
					num = num2;
					num3 = 1;
				}
				num2++;
			}
			return -1;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00081DC4 File Offset: 0x0007FFC4
		public static int FirstConsecutiveWhere<T>(IList<T> list, int count, Predicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (count < 1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int count2 = list.Count;
			if (count > count2)
			{
				return -1;
			}
			int num = 0;
			int num2 = -1;
			int num3 = 0;
			foreach (T t in list)
			{
				if (predicate(t))
				{
					if (num2 < 0)
					{
						num2 = num;
					}
					num3++;
					if (num3 >= count)
					{
						return num2;
					}
				}
				else
				{
					num3 = 0;
					num2 = -1;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00081E74 File Offset: 0x00080074
		public static T FindFirstWhere<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			T t;
			if (Algorithms.TryFindFirstWhere<T>(collection, predicate, out t))
			{
				return t;
			}
			return default(T);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00081E98 File Offset: 0x00080098
		public static bool TryFindFirstWhere<T>(IEnumerable<T> collection, Predicate<T> predicate, out T foundItem)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T t in collection)
			{
				if (predicate(t))
				{
					foundItem = t;
					return true;
				}
			}
			foundItem = default(T);
			return false;
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00081F14 File Offset: 0x00080114
		public static T FindLastWhere<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			T t;
			if (Algorithms.TryFindLastWhere<T>(collection, predicate, out t))
			{
				return t;
			}
			return default(T);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00081F38 File Offset: 0x00080138
		public static bool TryFindLastWhere<T>(IEnumerable<T> collection, Predicate<T> predicate, out T foundItem)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			IList<T> list = collection as IList<T>;
			if (list != null)
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					T t = list[i];
					if (predicate(t))
					{
						foundItem = t;
						return true;
					}
				}
				foundItem = default(T);
				return false;
			}
			bool flag = false;
			foundItem = default(T);
			foreach (T t2 in collection)
			{
				if (predicate(t2))
				{
					foundItem = t2;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00081FFC File Offset: 0x000801FC
		public static IEnumerable<T> FindWhere<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T t in collection)
			{
				if (predicate(t))
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00082014 File Offset: 0x00080214
		public static int FindFirstIndexWhere<T>(IList<T> list, Predicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			int num = 0;
			foreach (T t in list)
			{
				if (predicate(t))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00082088 File Offset: 0x00080288
		public static int FindLastIndexWhere<T>(IList<T> list, Predicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (predicate(list[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000820D6 File Offset: 0x000802D6
		public static IEnumerable<int> FindIndicesWhere<T>(IList<T> list, Predicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			int index = 0;
			foreach (T t in list)
			{
				if (predicate(t))
				{
					yield return index;
				}
				int num = index + 1;
				index = num;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000820ED File Offset: 0x000802ED
		public static int FirstIndexOf<T>(IList<T> list, T item)
		{
			return Algorithms.FirstIndexOf<T>(list, item, EqualityComparer<T>.Default);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000820FC File Offset: 0x000802FC
		public static int FirstIndexOf<T>(IList<T> list, T item, IEqualityComparer<T> equalityComparer)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			int num = 0;
			foreach (T t in list)
			{
				if (equalityComparer.Equals(t, item))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00082170 File Offset: 0x00080370
		public static int LastIndexOf<T>(IList<T> list, T item)
		{
			return Algorithms.LastIndexOf<T>(list, item, EqualityComparer<T>.Default);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00082180 File Offset: 0x00080380
		public static int LastIndexOf<T>(IList<T> list, T item, IEqualityComparer<T> equalityComparer)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (equalityComparer.Equals(list[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000821CF File Offset: 0x000803CF
		public static IEnumerable<int> IndicesOf<T>(IList<T> list, T item)
		{
			return Algorithms.IndicesOf<T>(list, item, EqualityComparer<T>.Default);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000821DD File Offset: 0x000803DD
		public static IEnumerable<int> IndicesOf<T>(IList<T> list, T item, IEqualityComparer<T> equalityComparer)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			int index = 0;
			foreach (T t in list)
			{
				if (equalityComparer.Equals(t, item))
				{
					yield return index;
				}
				int num = index + 1;
				index = num;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000821FB File Offset: 0x000803FB
		public static int FirstIndexOfMany<T>(IList<T> list, IEnumerable<T> itemsToLookFor)
		{
			return Algorithms.FirstIndexOfMany<T>(list, itemsToLookFor, EqualityComparer<T>.Default);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0008220C File Offset: 0x0008040C
		public static int FirstIndexOfMany<T>(IList<T> list, IEnumerable<T> itemsToLookFor, IEqualityComparer<T> equalityComparer)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (itemsToLookFor == null)
			{
				throw new ArgumentNullException("itemsToLookFor");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			Set<T> set = new Set<T>(itemsToLookFor, equalityComparer);
			int num = 0;
			foreach (T t in list)
			{
				if (set.Contains(t))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00082298 File Offset: 0x00080498
		public static int FirstIndexOfMany<T>(IList<T> list, IEnumerable<T> itemsToLookFor, BinaryPredicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (itemsToLookFor == null)
			{
				throw new ArgumentNullException("itemsToLookFor");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			int num = 0;
			foreach (T t in list)
			{
				foreach (T t2 in itemsToLookFor)
				{
					if (predicate(t, t2))
					{
						return num;
					}
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00082350 File Offset: 0x00080550
		public static int LastIndexOfMany<T>(IList<T> list, IEnumerable<T> itemsToLookFor)
		{
			return Algorithms.LastIndexOfMany<T>(list, itemsToLookFor, EqualityComparer<T>.Default);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00082360 File Offset: 0x00080560
		public static int LastIndexOfMany<T>(IList<T> list, IEnumerable<T> itemsToLookFor, IEqualityComparer<T> equalityComparer)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (itemsToLookFor == null)
			{
				throw new ArgumentNullException("itemsToLookFor");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			Set<T> set = new Set<T>(itemsToLookFor, equalityComparer);
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (set.Contains(list[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000823C4 File Offset: 0x000805C4
		public static int LastIndexOfMany<T>(IList<T> list, IEnumerable<T> itemsToLookFor, BinaryPredicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (itemsToLookFor == null)
			{
				throw new ArgumentNullException("itemsToLookFor");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				foreach (T t in itemsToLookFor)
				{
					if (predicate(list[i], t))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0008245C File Offset: 0x0008065C
		public static IEnumerable<int> IndicesOfMany<T>(IList<T> list, IEnumerable<T> itemsToLookFor)
		{
			return Algorithms.IndicesOfMany<T>(list, itemsToLookFor, EqualityComparer<T>.Default);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0008246A File Offset: 0x0008066A
		public static IEnumerable<int> IndicesOfMany<T>(IList<T> list, IEnumerable<T> itemsToLookFor, IEqualityComparer<T> equalityComparer)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (itemsToLookFor == null)
			{
				throw new ArgumentNullException("itemsToLookFor");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			Set<T> setToLookFor = new Set<T>(itemsToLookFor, equalityComparer);
			int index = 0;
			foreach (T t in list)
			{
				if (setToLookFor.Contains(t))
				{
					yield return index;
				}
				int num = index + 1;
				index = num;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00082488 File Offset: 0x00080688
		public static IEnumerable<int> IndicesOfMany<T>(IList<T> list, IEnumerable<T> itemsToLookFor, BinaryPredicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (itemsToLookFor == null)
			{
				throw new ArgumentNullException("itemsToLookFor");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			int index = 0;
			foreach (T x in list)
			{
				foreach (T t in itemsToLookFor)
				{
					if (predicate(x, t))
					{
						yield return index;
					}
				}
				IEnumerator<T> enumerator2 = null;
				int num = index + 1;
				index = num;
				x = default(T);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000824A6 File Offset: 0x000806A6
		public static int SearchForSubsequence<T>(IList<T> list, IEnumerable<T> pattern)
		{
			return Algorithms.SearchForSubsequence<T>(list, pattern, EqualityComparer<T>.Default);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000824B4 File Offset: 0x000806B4
		public static int SearchForSubsequence<T>(IList<T> list, IEnumerable<T> pattern, BinaryPredicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			T[] array = Algorithms.ToArray<T>(pattern);
			int count = list.Count;
			int num = array.Length;
			if (num == 0)
			{
				return 0;
			}
			if (count == 0)
			{
				return -1;
			}
			int i = 0;
			IL_007A:
			while (i <= count - num)
			{
				for (int j = 0; j < num; j++)
				{
					if (!predicate(list[i + j], array[j]))
					{
						i++;
						goto IL_007A;
					}
				}
				return i;
			}
			return -1;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00082542 File Offset: 0x00080742
		public static int SearchForSubsequence<T>(IList<T> list, IEnumerable<T> pattern, IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			return Algorithms.SearchForSubsequence<T>(list, pattern, new BinaryPredicate<T>(equalityComparer.Equals));
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00082566 File Offset: 0x00080766
		public static bool IsSubsetOf<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
		{
			return Algorithms.IsSubsetOf<T>(collection1, collection2, EqualityComparer<T>.Default);
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00082574 File Offset: 0x00080774
		public static bool IsSubsetOf<T>(IEnumerable<T> collection1, IEnumerable<T> collection2, IEqualityComparer<T> equalityComparer)
		{
			if (collection1 == null)
			{
				throw new ArgumentNullException("collection1");
			}
			if (collection2 == null)
			{
				throw new ArgumentNullException("collection2");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentException("equalityComparer");
			}
			Bag<T> bag = new Bag<T>(collection1, equalityComparer);
			return new Bag<T>(collection2, equalityComparer).IsSupersetOf(bag);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x000825C0 File Offset: 0x000807C0
		public static bool IsProperSubsetOf<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
		{
			return Algorithms.IsProperSubsetOf<T>(collection1, collection2, EqualityComparer<T>.Default);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x000825D0 File Offset: 0x000807D0
		public static bool IsProperSubsetOf<T>(IEnumerable<T> collection1, IEnumerable<T> collection2, IEqualityComparer<T> equalityComparer)
		{
			if (collection1 == null)
			{
				throw new ArgumentNullException("collection1");
			}
			if (collection2 == null)
			{
				throw new ArgumentNullException("collection2");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentException("equalityComparer");
			}
			Bag<T> bag = new Bag<T>(collection1, equalityComparer);
			return new Bag<T>(collection2, equalityComparer).IsProperSupersetOf(bag);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0008261C File Offset: 0x0008081C
		public static bool DisjointSets<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
		{
			return Algorithms.DisjointSets<T>(collection1, collection2, EqualityComparer<T>.Default);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0008262C File Offset: 0x0008082C
		public static bool DisjointSets<T>(IEnumerable<T> collection1, IEnumerable<T> collection2, IEqualityComparer<T> equalityComparer)
		{
			if (collection1 == null)
			{
				throw new ArgumentNullException("collection1");
			}
			if (collection2 == null)
			{
				throw new ArgumentNullException("collection2");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentException("equalityComparer");
			}
			Set<T> set = new Set<T>(collection1, equalityComparer);
			foreach (T t in collection2)
			{
				if (set.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000826B0 File Offset: 0x000808B0
		public static bool EqualSets<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
		{
			return Algorithms.EqualSets<T>(collection1, collection2, EqualityComparer<T>.Default);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000826C0 File Offset: 0x000808C0
		public static bool EqualSets<T>(IEnumerable<T> collection1, IEnumerable<T> collection2, IEqualityComparer<T> equalityComparer)
		{
			if (collection1 == null)
			{
				throw new ArgumentNullException("collection1");
			}
			if (collection2 == null)
			{
				throw new ArgumentNullException("collection2");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentException("equalityComparer");
			}
			Bag<T> bag = new Bag<T>(collection1, equalityComparer);
			return new Bag<T>(collection2, equalityComparer).IsEqualTo(bag);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0008270C File Offset: 0x0008090C
		public static IEnumerable<T> SetIntersection<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
		{
			return Algorithms.SetIntersection<T>(collection1, collection2, EqualityComparer<T>.Default);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0008271C File Offset: 0x0008091C
		public static IEnumerable<T> SetIntersection<T>(IEnumerable<T> collection1, IEnumerable<T> collection2, IEqualityComparer<T> equalityComparer)
		{
			if (collection1 == null)
			{
				throw new ArgumentNullException("collection1");
			}
			if (collection2 == null)
			{
				throw new ArgumentNullException("collection2");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentException("equalityComparer");
			}
			Bag<T> bag = new Bag<T>(collection1, equalityComparer);
			Bag<T> bag2 = new Bag<T>(collection2, equalityComparer);
			return Util.CreateEnumerableWrapper<T>(bag.Intersection(bag2));
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0008276D File Offset: 0x0008096D
		public static IEnumerable<T> SetUnion<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
		{
			return Algorithms.SetUnion<T>(collection1, collection2, EqualityComparer<T>.Default);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0008277C File Offset: 0x0008097C
		public static IEnumerable<T> SetUnion<T>(IEnumerable<T> collection1, IEnumerable<T> collection2, IEqualityComparer<T> equalityComparer)
		{
			if (collection1 == null)
			{
				throw new ArgumentNullException("collection1");
			}
			if (collection2 == null)
			{
				throw new ArgumentNullException("collection2");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentException("equalityComparer");
			}
			Bag<T> bag = new Bag<T>(collection1, equalityComparer);
			Bag<T> bag2 = new Bag<T>(collection2, equalityComparer);
			if (bag.Count > bag2.Count)
			{
				bag.UnionWith(bag2);
				return Util.CreateEnumerableWrapper<T>(bag);
			}
			bag2.UnionWith(bag);
			return Util.CreateEnumerableWrapper<T>(bag2);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000827EC File Offset: 0x000809EC
		public static IEnumerable<T> SetDifference<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
		{
			return Algorithms.SetDifference<T>(collection1, collection2, EqualityComparer<T>.Default);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000827FC File Offset: 0x000809FC
		public static IEnumerable<T> SetDifference<T>(IEnumerable<T> collection1, IEnumerable<T> collection2, IEqualityComparer<T> equalityComparer)
		{
			if (collection1 == null)
			{
				throw new ArgumentNullException("collection1");
			}
			if (collection2 == null)
			{
				throw new ArgumentNullException("collection2");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentException("equalityComparer");
			}
			Bag<T> bag = new Bag<T>(collection1, equalityComparer);
			Bag<T> bag2 = new Bag<T>(collection2, equalityComparer);
			bag.DifferenceWith(bag2);
			return Util.CreateEnumerableWrapper<T>(bag);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0008284E File Offset: 0x00080A4E
		public static IEnumerable<T> SetSymmetricDifference<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
		{
			return Algorithms.SetSymmetricDifference<T>(collection1, collection2, EqualityComparer<T>.Default);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0008285C File Offset: 0x00080A5C
		public static IEnumerable<T> SetSymmetricDifference<T>(IEnumerable<T> collection1, IEnumerable<T> collection2, IEqualityComparer<T> equalityComparer)
		{
			if (collection1 == null)
			{
				throw new ArgumentNullException("collection1");
			}
			if (collection2 == null)
			{
				throw new ArgumentNullException("collection2");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentException("equalityComparer");
			}
			Bag<T> bag = new Bag<T>(collection1, equalityComparer);
			Bag<T> bag2 = new Bag<T>(collection2, equalityComparer);
			if (bag.Count > bag2.Count)
			{
				bag.SymmetricDifferenceWith(bag2);
				return Util.CreateEnumerableWrapper<T>(bag);
			}
			bag2.SymmetricDifferenceWith(bag);
			return Util.CreateEnumerableWrapper<T>(bag2);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000828CC File Offset: 0x00080ACC
		public static IEnumerable<Pair<TFirst, TSecond>> CartesianProduct<TFirst, TSecond>(IEnumerable<TFirst> first, IEnumerable<TSecond> second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			foreach (TFirst itemFirst in first)
			{
				foreach (TSecond tsecond in second)
				{
					yield return new Pair<TFirst, TSecond>(itemFirst, tsecond);
				}
				IEnumerator<TSecond> enumerator2 = null;
				itemFirst = default(TFirst);
			}
			IEnumerator<TFirst> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000828E3 File Offset: 0x00080AE3
		public static string ToString<T>(IEnumerable<T> collection)
		{
			return Algorithms.ToString<T>(collection, true, "{", ",", "}");
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000828FC File Offset: 0x00080AFC
		public static string ToString<T>(IEnumerable<T> collection, bool recursive, string start, string separator, string end)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (separator == null)
			{
				throw new ArgumentNullException("separator");
			}
			if (end == null)
			{
				throw new ArgumentNullException("end");
			}
			if (collection == null)
			{
				return "null";
			}
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(start);
			foreach (T t in collection)
			{
				if (!flag)
				{
					stringBuilder.Append(separator);
				}
				if (t == null)
				{
					stringBuilder.Append("null");
				}
				else if (recursive && t is IEnumerable && !(t is string))
				{
					stringBuilder.Append(Algorithms.ToString<object>(Algorithms.TypedAs<object>((IEnumerable)((object)t)), recursive, start, separator, end));
				}
				else
				{
					stringBuilder.Append(t.ToString());
				}
				flag = false;
			}
			stringBuilder.Append(end);
			return stringBuilder.ToString();
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00082A0C File Offset: 0x00080C0C
		public static string ToString<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
		{
			bool flag = true;
			if (dictionary == null)
			{
				return "null";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				if (keyValuePair.Key == null)
				{
					stringBuilder.Append("null");
				}
				else if (keyValuePair.Key is IEnumerable && !(keyValuePair.Key is string))
				{
					stringBuilder.Append(Algorithms.ToString<object>(Algorithms.TypedAs<object>((IEnumerable)((object)keyValuePair.Key)), true, "{", ",", "}"));
				}
				else
				{
					StringBuilder stringBuilder2 = stringBuilder;
					TKey key = keyValuePair.Key;
					stringBuilder2.Append(key.ToString());
				}
				stringBuilder.Append("->");
				if (keyValuePair.Value == null)
				{
					stringBuilder.Append("null");
				}
				else if (keyValuePair.Value is IEnumerable && !(keyValuePair.Value is string))
				{
					stringBuilder.Append(Algorithms.ToString<object>(Algorithms.TypedAs<object>((IEnumerable)((object)keyValuePair.Value)), true, "{", ",", "}"));
				}
				else
				{
					StringBuilder stringBuilder3 = stringBuilder;
					TValue value = keyValuePair.Value;
					stringBuilder3.Append(value.ToString());
				}
				flag = false;
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00082BD4 File Offset: 0x00080DD4
		private static Random GetRandomGenerator()
		{
			return Calc.R;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00082BDB File Offset: 0x00080DDB
		public static T[] RandomShuffle<T>(IEnumerable<T> collection)
		{
			return Algorithms.RandomShuffle<T>(collection, Algorithms.GetRandomGenerator());
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00082BE8 File Offset: 0x00080DE8
		public static T[] RandomShuffle<T>(IEnumerable<T> collection, Random randomGenerator)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (randomGenerator == null)
			{
				throw new ArgumentNullException("randomGenerator");
			}
			T[] array = Algorithms.ToArray<T>(collection);
			for (int i = array.Length - 1; i >= 1; i--)
			{
				int num = randomGenerator.Next(i + 1);
				T t = array[i];
				array[i] = array[num];
				array[num] = t;
			}
			return array;
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00082C51 File Offset: 0x00080E51
		public static void RandomShuffleInPlace<T>(IList<T> list)
		{
			Algorithms.RandomShuffleInPlace<T>(list, Algorithms.GetRandomGenerator());
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00082C60 File Offset: 0x00080E60
		public static void RandomShuffleInPlace<T>(IList<T> list, Random randomGenerator)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (randomGenerator == null)
			{
				throw new ArgumentNullException("randomGenerator");
			}
			if (list is T[])
			{
				list = new Algorithms.ArrayWrapper<T>((T[])list);
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			for (int i = list.Count - 1; i >= 1; i--)
			{
				int num = randomGenerator.Next(i + 1);
				T t = list[i];
				list[i] = list[num];
				list[num] = t;
			}
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00082CF1 File Offset: 0x00080EF1
		public static T[] RandomSubset<T>(IEnumerable<T> collection, int count)
		{
			return Algorithms.RandomSubset<T>(collection, count, Algorithms.GetRandomGenerator());
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00082D00 File Offset: 0x00080F00
		public static T[] RandomSubset<T>(IEnumerable<T> collection, int count, Random randomGenerator)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (randomGenerator == null)
			{
				throw new ArgumentNullException("randomGenerator");
			}
			IList<T> list = collection as IList<T>;
			if (list == null)
			{
				list = new List<T>(collection);
			}
			int count2 = list.Count;
			if (count < 0 || count > count2)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			T[] array = new T[count];
			Dictionary<int, T> dictionary = new Dictionary<int, T>(count);
			for (int i = 0; i < count; i++)
			{
				int num = randomGenerator.Next(count2 - i) + i;
				T t;
				if (!dictionary.TryGetValue(num, out t))
				{
					t = list[num];
				}
				array[i] = t;
				if (i != num)
				{
					if (dictionary.TryGetValue(i, out t))
					{
						dictionary[num] = t;
					}
					else
					{
						dictionary[num] = list[i];
					}
				}
			}
			return array;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00082DD0 File Offset: 0x00080FD0
		public static IEnumerable<T[]> GeneratePermutations<T>(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			T[] array = Algorithms.ToArray<T>(collection);
			if (array.Length == 0)
			{
				yield break;
			}
			int[] state = new int[array.Length - 1];
			int maxLength = state.Length;
			yield return array;
			if (array.Length == 1)
			{
				yield break;
			}
			int i = 0;
			for (;;)
			{
				if (state[i] < i + 1)
				{
					T t;
					if (state[i] > 0)
					{
						t = array[i + 1];
						array[i + 1] = array[state[i] - 1];
						array[state[i] - 1] = t;
					}
					t = array[i + 1];
					array[i + 1] = array[state[i]];
					array[state[i]] = t;
					yield return array;
					state[i]++;
					i = 0;
				}
				else
				{
					T t = array[i + 1];
					array[i + 1] = array[i];
					array[i] = t;
					state[i] = 0;
					int num = i + 1;
					i = num;
					if (i >= maxLength)
					{
						break;
					}
				}
			}
			yield break;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00082DE0 File Offset: 0x00080FE0
		public static IEnumerable<T[]> GenerateSortedPermutations<T>(IEnumerable<T> collection) where T : IComparable<T>
		{
			return Algorithms.GenerateSortedPermutations<T>(collection, Comparer<T>.Default);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00082DED File Offset: 0x00080FED
		public static IEnumerable<T[]> GenerateSortedPermutations<T>(IEnumerable<T> collection, IComparer<T> comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			T[] array = Algorithms.ToArray<T>(collection);
			int length = array.Length;
			if (length == 0)
			{
				yield break;
			}
			Array.Sort<T>(array, comparer);
			yield return array;
			if (length == 1)
			{
				yield break;
			}
			for (;;)
			{
				int num = length - 2;
				while (comparer.Compare(array[num], array[num + 1]) >= 0)
				{
					num--;
					if (num < 0)
					{
						goto Block_5;
					}
				}
				int num2 = length - 1;
				while (comparer.Compare(array[num2], array[num]) <= 0)
				{
					num2--;
				}
				T t = array[num];
				array[num] = array[num2];
				array[num2] = t;
				int i = num + 1;
				int num3 = length - 1;
				while (i < num3)
				{
					t = array[i];
					array[i] = array[num3];
					array[num3] = t;
					i++;
					num3--;
				}
				yield return array;
			}
			Block_5:
			yield break;
			yield break;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00082E04 File Offset: 0x00081004
		public static IEnumerable<T[]> GenerateSortedPermutations<T>(IEnumerable<T> collection, Comparison<T> comparison)
		{
			return Algorithms.GenerateSortedPermutations<T>(collection, Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00082E12 File Offset: 0x00081012
		public static T Maximum<T>(IEnumerable<T> collection) where T : IComparable<T>
		{
			return Algorithms.Maximum<T>(collection, Comparer<T>.Default);
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00082E20 File Offset: 0x00081020
		public static T Maximum<T>(IEnumerable<T> collection, IComparer<T> comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			T t = default(T);
			bool flag = false;
			foreach (T t2 in collection)
			{
				if (!flag || comparer.Compare(t, t2) < 0)
				{
					t = t2;
				}
				flag = true;
			}
			if (!flag)
			{
				throw new InvalidOperationException(Strings.CollectionIsEmpty);
			}
			return t;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00082EA8 File Offset: 0x000810A8
		public static T Maximum<T>(IEnumerable<T> collection, Comparison<T> comparison)
		{
			return Algorithms.Maximum<T>(collection, Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00082EB6 File Offset: 0x000810B6
		public static T Minimum<T>(IEnumerable<T> collection) where T : IComparable<T>
		{
			return Algorithms.Minimum<T>(collection, Comparer<T>.Default);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00082EC4 File Offset: 0x000810C4
		public static T Minimum<T>(IEnumerable<T> collection, IComparer<T> comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			T t = default(T);
			bool flag = false;
			foreach (T t2 in collection)
			{
				if (!flag || comparer.Compare(t, t2) > 0)
				{
					t = t2;
				}
				flag = true;
			}
			if (!flag)
			{
				throw new InvalidOperationException(Strings.CollectionIsEmpty);
			}
			return t;
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00082F4C File Offset: 0x0008114C
		public static T Minimum<T>(IEnumerable<T> collection, Comparison<T> comparison)
		{
			return Algorithms.Minimum<T>(collection, Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00082F5A File Offset: 0x0008115A
		public static int IndexOfMaximum<T>(IList<T> list) where T : IComparable<T>
		{
			return Algorithms.IndexOfMaximum<T>(list, Comparer<T>.Default);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00082F68 File Offset: 0x00081168
		public static int IndexOfMaximum<T>(IList<T> list, IComparer<T> comparer)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			T t = default(T);
			int num = -1;
			int num2 = 0;
			foreach (T t2 in list)
			{
				if (num < 0 || comparer.Compare(t, t2) < 0)
				{
					t = t2;
					num = num2;
				}
				num2++;
			}
			return num;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00082FEC File Offset: 0x000811EC
		public static int IndexOfMaximum<T>(IList<T> list, Comparison<T> comparison)
		{
			return Algorithms.IndexOfMaximum<T>(list, Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00082FFA File Offset: 0x000811FA
		public static int IndexOfMinimum<T>(IList<T> list) where T : IComparable<T>
		{
			return Algorithms.IndexOfMinimum<T>(list, Comparer<T>.Default);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00083008 File Offset: 0x00081208
		public static int IndexOfMinimum<T>(IList<T> list, IComparer<T> comparer)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			T t = default(T);
			int num = -1;
			int num2 = 0;
			foreach (T t2 in list)
			{
				if (num < 0 || comparer.Compare(t, t2) > 0)
				{
					t = t2;
					num = num2;
				}
				num2++;
			}
			return num;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0008308C File Offset: 0x0008128C
		public static int IndexOfMinimum<T>(IList<T> list, Comparison<T> comparison)
		{
			return Algorithms.IndexOfMinimum<T>(list, Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0008309A File Offset: 0x0008129A
		public static T[] Sort<T>(IEnumerable<T> collection) where T : IComparable<T>
		{
			return Algorithms.Sort<T>(collection, Comparer<T>.Default);
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000830A7 File Offset: 0x000812A7
		public static T[] Sort<T>(IEnumerable<T> collection, IComparer<T> comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			T[] array = Algorithms.ToArray<T>(collection);
			Array.Sort<T>(array, comparer);
			return array;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000830D2 File Offset: 0x000812D2
		public static T[] Sort<T>(IEnumerable<T> collection, Comparison<T> comparison)
		{
			return Algorithms.Sort<T>(collection, Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000830E0 File Offset: 0x000812E0
		public static void SortInPlace<T>(IList<T> list) where T : IComparable<T>
		{
			Algorithms.SortInPlace<T>(list, Comparer<T>.Default);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000830F0 File Offset: 0x000812F0
		public static void SortInPlace<T>(IList<T> list, IComparer<T> comparer)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			if (list is T[])
			{
				Array.Sort<T>((T[])list, comparer);
				return;
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			int[] array = new int[32];
			int[] array2 = new int[32];
			int num = 0;
			int num2 = 0;
			int num3 = list.Count - 1;
			for (;;)
			{
				if (num2 == num3 - 1)
				{
					T t = list[num2];
					T t2 = list[num3];
					if (comparer.Compare(t, t2) > 0)
					{
						list[num3] = t;
						list[num2] = t2;
					}
					num2 = num3;
				}
				else if (num2 < num3)
				{
					int num4 = num2 + (num3 - num2) / 2;
					T t3 = list[num2];
					T t4 = list[num4];
					T t5 = list[num3];
					if (comparer.Compare(t3, t4) > 0)
					{
						T t6 = t3;
						t3 = t4;
						t4 = t6;
					}
					if (comparer.Compare(t3, t5) > 0)
					{
						T t7 = t5;
						t5 = t4;
						t4 = t3;
						t3 = t7;
					}
					else if (comparer.Compare(t4, t5) > 0)
					{
						T t8 = t4;
						t4 = t5;
						t5 = t8;
					}
					if (num2 == num3 - 2)
					{
						list[num2] = t3;
						list[num4] = t4;
						list[num3] = t5;
						num2 = num3;
					}
					else
					{
						list[num2] = t3;
						list[num4] = t5;
						T t9 = (list[num3] = t4);
						int num5 = num2;
						int num6 = num3;
						T t10;
						for (;;)
						{
							num5++;
							t10 = list[num5];
							if (comparer.Compare(t10, t9) >= 0)
							{
								T t11;
								do
								{
									num6--;
									t11 = list[num6];
								}
								while (comparer.Compare(t11, t9) > 0);
								if (num6 < num5)
								{
									break;
								}
								list[num5] = t11;
								list[num6] = t10;
							}
						}
						list[num3] = t10;
						list[num5] = t9;
						num5++;
						if (num6 - num2 > num3 - num5)
						{
							array[num] = num2;
							array2[num] = num6;
							num2 = num5;
						}
						else
						{
							array[num] = num5;
							array2[num] = num3;
							num3 = num6;
						}
						num++;
					}
				}
				else
				{
					if (num <= 0)
					{
						break;
					}
					num--;
					num2 = array[num];
					num3 = array2[num];
				}
			}
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00083320 File Offset: 0x00081520
		public static void SortInPlace<T>(IList<T> list, Comparison<T> comparison)
		{
			Algorithms.SortInPlace<T>(list, Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0008332E File Offset: 0x0008152E
		public static T[] StableSort<T>(IEnumerable<T> collection) where T : IComparable<T>
		{
			return Algorithms.StableSort<T>(collection, Comparer<T>.Default);
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0008333B File Offset: 0x0008153B
		public static T[] StableSort<T>(IEnumerable<T> collection, IComparer<T> comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			T[] array = Algorithms.ToArray<T>(collection);
			Algorithms.StableSortInPlace<T>(Algorithms.ReadWriteList<T>(array), comparer);
			return array;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0008336B File Offset: 0x0008156B
		public static T[] StableSort<T>(IEnumerable<T> collection, Comparison<T> comparison)
		{
			return Algorithms.StableSort<T>(collection, Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x00083379 File Offset: 0x00081579
		public static void StableSortInPlace<T>(IList<T> list) where T : IComparable<T>
		{
			Algorithms.StableSortInPlace<T>(list, Comparer<T>.Default);
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00083388 File Offset: 0x00081588
		public static void StableSortInPlace<T>(IList<T> list, IComparer<T> comparer)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			if (list is T[])
			{
				list = new Algorithms.ArrayWrapper<T>((T[])list);
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			int[] array = new int[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i;
			}
			int[] array2 = new int[32];
			int[] array3 = new int[32];
			int num = 0;
			int num2 = 0;
			int num3 = list.Count - 1;
			for (;;)
			{
				if (num2 == num3 - 1)
				{
					T t = list[num2];
					int num4 = array[num2];
					T t2 = list[num3];
					int num5 = array[num3];
					int num6;
					if ((num6 = comparer.Compare(t, t2)) > 0 || (num6 == 0 && num4 > num5))
					{
						list[num3] = t;
						array[num3] = num4;
						list[num2] = t2;
						array[num2] = num5;
					}
					num2 = num3;
				}
				else if (num2 < num3)
				{
					int num7 = num2 + (num3 - num2) / 2;
					T t3 = list[num2];
					T t4 = list[num7];
					T t5 = list[num3];
					int num8 = array[num2];
					int num9 = array[num7];
					int num10 = array[num3];
					int num6;
					if ((num6 = comparer.Compare(t3, t4)) > 0 || (num6 == 0 && num8 > num9))
					{
						T t6 = t3;
						t3 = t4;
						t4 = t6;
						int num11 = num8;
						num8 = num9;
						num9 = num11;
					}
					if ((num6 = comparer.Compare(t3, t5)) > 0 || (num6 == 0 && num8 > num10))
					{
						T t7 = t5;
						t5 = t4;
						t4 = t3;
						t3 = t7;
						int num12 = num10;
						num10 = num9;
						num9 = num8;
						num8 = num12;
					}
					else if ((num6 = comparer.Compare(t4, t5)) > 0 || (num6 == 0 && num9 > num10))
					{
						T t8 = t4;
						t4 = t5;
						t5 = t8;
						int num13 = num9;
						num9 = num10;
						num10 = num13;
					}
					if (num2 == num3 - 2)
					{
						list[num2] = t3;
						list[num7] = t4;
						list[num3] = t5;
						array[num2] = num8;
						array[num7] = num9;
						array[num3] = num10;
						num2 = num3;
					}
					else
					{
						list[num2] = t3;
						array[num2] = num8;
						list[num7] = t5;
						array[num7] = num10;
						T t9 = (list[num3] = t4);
						int num14 = (array[num3] = num9);
						int num15 = num2;
						int num16 = num3;
						T t10;
						int num17;
						for (;;)
						{
							num15++;
							t10 = list[num15];
							num17 = array[num15];
							if ((num6 = comparer.Compare(t10, t9)) >= 0 && (num6 != 0 || num17 >= num14))
							{
								T t11;
								int num18;
								do
								{
									num16--;
									t11 = list[num16];
									num18 = array[num16];
								}
								while ((num6 = comparer.Compare(t11, t9)) > 0 || (num6 == 0 && num18 > num14));
								if (num16 < num15)
								{
									break;
								}
								list[num15] = t11;
								list[num16] = t10;
								array[num15] = num18;
								array[num16] = num17;
							}
						}
						list[num3] = t10;
						array[num3] = num17;
						list[num15] = t9;
						array[num15] = num14;
						num15++;
						if (num16 - num2 > num3 - num15)
						{
							array2[num] = num2;
							array3[num] = num16;
							num2 = num15;
						}
						else
						{
							array2[num] = num15;
							array3[num] = num3;
							num3 = num16;
						}
						num++;
					}
				}
				else
				{
					if (num <= 0)
					{
						break;
					}
					num--;
					num2 = array2[num];
					num3 = array3[num];
				}
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000836D0 File Offset: 0x000818D0
		public static void StableSortInPlace<T>(IList<T> list, Comparison<T> comparison)
		{
			Algorithms.StableSortInPlace<T>(list, Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000836DE File Offset: 0x000818DE
		public static int BinarySearch<T>(IList<T> list, T item, out int index) where T : IComparable<T>
		{
			return Algorithms.BinarySearch<T>(list, item, Comparer<T>.Default, out index);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x000836F0 File Offset: 0x000818F0
		public static int BinarySearch<T>(IList<T> list, T item, IComparer<T> comparer, out int index)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			int num = 0;
			int i = list.Count;
			while (i > num)
			{
				int num2 = num + (i - num) / 2;
				T t = list[num2];
				int num3 = comparer.Compare(t, item);
				if (num3 < 0)
				{
					num = num2 + 1;
				}
				else
				{
					if (num3 <= 0)
					{
						int num4 = num;
						int num5 = i;
						int num6 = num2;
						num = num4;
						i = num6;
						while (i > num)
						{
							num2 = num + (i - num) / 2;
							t = list[num2];
							num3 = comparer.Compare(t, item);
							if (num3 < 0)
							{
								num = num2 + 1;
							}
							else
							{
								i = num2;
							}
						}
						index = num;
						num = num6;
						i = num5;
						while (i > num)
						{
							num2 = num + (i - num) / 2;
							t = list[num2];
							num3 = comparer.Compare(t, item);
							if (num3 <= 0)
							{
								num = num2 + 1;
							}
							else
							{
								i = num2;
							}
						}
						return num - index;
					}
					i = num2;
				}
			}
			index = num;
			return 0;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x000837DA File Offset: 0x000819DA
		public static int BinarySearch<T>(IList<T> list, T item, Comparison<T> comparison, out int index)
		{
			return Algorithms.BinarySearch<T>(list, item, Comparers.ComparerFromComparison<T>(comparison), out index);
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x000837EA File Offset: 0x000819EA
		public static IEnumerable<T> MergeSorted<T>(params IEnumerable<T>[] collections) where T : IComparable<T>
		{
			return Algorithms.MergeSorted<T>(Comparer<T>.Default, collections);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000837F7 File Offset: 0x000819F7
		public static IEnumerable<T> MergeSorted<T>(IComparer<T> comparer, params IEnumerable<T>[] collections)
		{
			if (collections == null)
			{
				throw new ArgumentNullException("collections");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			IEnumerator<T>[] enumerators = new IEnumerator<T>[collections.Length];
			bool[] more = new bool[collections.Length];
			T smallestItem = default(T);
			try
			{
				for (int i = 0; i < collections.Length; i++)
				{
					if (collections[i] != null)
					{
						enumerators[i] = collections[i].GetEnumerator();
						more[i] = enumerators[i].MoveNext();
					}
				}
				for (;;)
				{
					int smallestItemIndex = -1;
					for (int j = 0; j < enumerators.Length; j++)
					{
						if (more[j])
						{
							T t = enumerators[j].Current;
							if (smallestItemIndex < 0 || comparer.Compare(smallestItem, t) > 0)
							{
								smallestItemIndex = j;
								smallestItem = t;
							}
						}
					}
					if (smallestItemIndex == -1)
					{
						break;
					}
					yield return smallestItem;
					more[smallestItemIndex] = enumerators[smallestItemIndex].MoveNext();
				}
				yield break;
			}
			finally
			{
				foreach (IEnumerator<T> enumerator in enumerators)
				{
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0008380E File Offset: 0x00081A0E
		public static IEnumerable<T> MergeSorted<T>(Comparison<T> comparison, params IEnumerable<T>[] collections)
		{
			return Algorithms.MergeSorted<T>(Comparers.ComparerFromComparison<T>(comparison), collections);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0008381C File Offset: 0x00081A1C
		public static int LexicographicalCompare<T>(IEnumerable<T> sequence1, IEnumerable<T> sequence2) where T : IComparable<T>
		{
			return Algorithms.LexicographicalCompare<T>(sequence1, sequence2, Comparer<T>.Default);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0008382A File Offset: 0x00081A2A
		public static int LexicographicalCompare<T>(IEnumerable<T> sequence1, IEnumerable<T> sequence2, Comparison<T> comparison)
		{
			return Algorithms.LexicographicalCompare<T>(sequence1, sequence2, Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0008383C File Offset: 0x00081A3C
		public static int LexicographicalCompare<T>(IEnumerable<T> sequence1, IEnumerable<T> sequence2, IComparer<T> comparer)
		{
			if (sequence1 == null)
			{
				throw new ArgumentNullException("sequence1");
			}
			if (sequence2 == null)
			{
				throw new ArgumentNullException("sequence2");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			int num2;
			using (IEnumerator<T> enumerator = sequence1.GetEnumerator())
			{
				using (IEnumerator<T> enumerator2 = sequence2.GetEnumerator())
				{
					bool flag;
					bool flag2;
					int num;
					do
					{
						flag = enumerator.MoveNext();
						flag2 = enumerator2.MoveNext();
						if (!flag || !flag2)
						{
							goto IL_006A;
						}
						num = comparer.Compare(enumerator.Current, enumerator2.Current);
					}
					while (num == 0);
					return num;
					IL_006A:
					if (flag == flag2)
					{
						num2 = 0;
					}
					else if (flag)
					{
						num2 = 1;
					}
					else
					{
						num2 = -1;
					}
				}
			}
			return num2;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x000838FC File Offset: 0x00081AFC
		public static IComparer<IEnumerable<T>> GetLexicographicalComparer<T>() where T : IComparable<T>
		{
			return Algorithms.GetLexicographicalComparer<T>(Comparer<T>.Default);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00083908 File Offset: 0x00081B08
		public static IComparer<IEnumerable<T>> GetLexicographicalComparer<T>(IComparer<T> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			return new Algorithms.LexicographicalComparerClass<T>(comparer);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0008391E File Offset: 0x00081B1E
		public static IComparer<IEnumerable<T>> GetLexicographicalComparer<T>(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			return new Algorithms.LexicographicalComparerClass<T>(Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00083939 File Offset: 0x00081B39
		public static IComparer<T> GetReverseComparer<T>(IComparer<T> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			return new Algorithms.ReverseComparerClass<T>(comparer);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0008394F File Offset: 0x00081B4F
		public static IEqualityComparer<T> GetIdentityComparer<T>() where T : class
		{
			return new Algorithms.IdentityComparer<T>();
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00083956 File Offset: 0x00081B56
		public static Comparison<T> GetReverseComparison<T>(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			return (T x, T y) => -comparison(x, y);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00083982 File Offset: 0x00081B82
		public static IComparer<T> GetComparerFromComparison<T>(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			return Comparers.ComparerFromComparison<T>(comparison);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00083998 File Offset: 0x00081B98
		public static Comparison<T> GetComparisonFromComparer<T>(IComparer<T> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			return new Comparison<T>(comparer.Compare);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000839B5 File Offset: 0x00081BB5
		public static IEqualityComparer<IEnumerable<T>> GetCollectionEqualityComparer<T>()
		{
			return Algorithms.GetCollectionEqualityComparer<T>(EqualityComparer<T>.Default);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000839C1 File Offset: 0x00081BC1
		public static IEqualityComparer<IEnumerable<T>> GetCollectionEqualityComparer<T>(IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			return new Algorithms.CollectionEqualityComparer<T>(equalityComparer);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000839D7 File Offset: 0x00081BD7
		public static IEqualityComparer<IEnumerable<T>> GetSetEqualityComparer<T>()
		{
			return Algorithms.GetSetEqualityComparer<T>(EqualityComparer<T>.Default);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000839E3 File Offset: 0x00081BE3
		public static IEqualityComparer<IEnumerable<T>> GetSetEqualityComparer<T>(IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			return new Algorithms.SetEqualityComparer<T>(equalityComparer);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x000839FC File Offset: 0x00081BFC
		public static bool Exists<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T t in collection)
			{
				if (predicate(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00083A6C File Offset: 0x00081C6C
		public static bool TrueForAll<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T t in collection)
			{
				if (!predicate(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00083ADC File Offset: 0x00081CDC
		public static int CountWhere<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			int num = 0;
			foreach (T t in collection)
			{
				if (predicate(t))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00083B4C File Offset: 0x00081D4C
		public static ICollection<T> RemoveWhere<T>(ICollection<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (collection is T[])
			{
				collection = new Algorithms.ArrayWrapper<T>((T[])collection);
			}
			if (collection.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "collection");
			}
			IList<T> list = collection as IList<T>;
			if (list != null)
			{
				int i = -1;
				int j = 0;
				int num = list.Count;
				List<T> list2 = new List<T>();
				while (j < num)
				{
					T t = list[j];
					if (predicate(t))
					{
						list2.Add(t);
					}
					else
					{
						i++;
						if (i != j)
						{
							list[i] = t;
						}
					}
					j++;
				}
				i++;
				if (i < num)
				{
					if (list is IList && ((IList)list).IsFixedSize)
					{
						while (i < num)
						{
							list[i++] = default(T);
						}
					}
					else
					{
						while (i < num)
						{
							list.RemoveAt(num - 1);
							num--;
						}
					}
				}
				return list2;
			}
			List<T> list3 = new List<T>();
			foreach (T t2 in collection)
			{
				if (predicate(t2))
				{
					list3.Add(t2);
				}
			}
			foreach (T t3 in list3)
			{
				collection.Remove(t3);
			}
			return list3;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00083CE4 File Offset: 0x00081EE4
		public static IEnumerable<TDest> Convert<TSource, TDest>(IEnumerable<TSource> sourceCollection, Converter<TSource, TDest> converter)
		{
			if (sourceCollection == null)
			{
				throw new ArgumentNullException("sourceCollection");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			foreach (TSource tsource in sourceCollection)
			{
				yield return converter(tsource);
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00083CFC File Offset: 0x00081EFC
		public static Converter<TKey, TValue> GetDictionaryConverter<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
		{
			return Algorithms.GetDictionaryConverter<TKey, TValue>(dictionary, default(TValue));
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00083D18 File Offset: 0x00081F18
		public static Converter<TKey, TValue> GetDictionaryConverter<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TValue defaultValue)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			return delegate(TKey key)
			{
				TValue tvalue;
				if (dictionary.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				return defaultValue;
			};
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00083D4C File Offset: 0x00081F4C
		public static void ForEach<T>(IEnumerable<T> collection, Action<T> action)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			foreach (T t in collection)
			{
				action(t);
			}
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00083DB0 File Offset: 0x00081FB0
		public static int Partition<T>(IList<T> list, Predicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (list is T[])
			{
				list = new Algorithms.ArrayWrapper<T>((T[])list);
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			int num = 0;
			int num2 = list.Count - 1;
			for (;;)
			{
				if (num <= num2)
				{
					if (predicate(list[num]))
					{
						num++;
						continue;
					}
				}
				while (num <= num2 && !predicate(list[num2]))
				{
					num2--;
				}
				if (num > num2)
				{
					break;
				}
				T t = list[num];
				list[num] = list[num2];
				list[num2] = t;
				num++;
				num2--;
			}
			return num;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00083E70 File Offset: 0x00082070
		public static int StablePartition<T>(IList<T> list, Predicate<T> predicate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (list is T[])
			{
				list = new Algorithms.ArrayWrapper<T>((T[])list);
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			int count = list.Count;
			if (count == 0)
			{
				return 0;
			}
			T[] array = new T[count];
			int num = 0;
			int num2 = count - 1;
			foreach (T t in list)
			{
				if (predicate(t))
				{
					array[num++] = t;
				}
				else
				{
					array[num2--] = t;
				}
			}
			int i;
			for (i = 0; i < num; i++)
			{
				list[i] = array[i];
			}
			num2 = count - 1;
			while (i < count)
			{
				list[i++] = array[num2--];
			}
			return num;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00083F84 File Offset: 0x00082184
		public static IEnumerable<T> Concatenate<T>(params IEnumerable<T>[] collections)
		{
			if (collections == null)
			{
				throw new ArgumentNullException("collections");
			}
			foreach (IEnumerable<T> enumerable in collections)
			{
				foreach (T t in enumerable)
				{
					yield return t;
				}
				IEnumerator<T> enumerator = null;
			}
			IEnumerable<T>[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00083F94 File Offset: 0x00082194
		public static bool EqualCollections<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
		{
			return Algorithms.EqualCollections<T>(collection1, collection2, EqualityComparer<T>.Default);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00083FA4 File Offset: 0x000821A4
		public static bool EqualCollections<T>(IEnumerable<T> collection1, IEnumerable<T> collection2, IEqualityComparer<T> equalityComparer)
		{
			if (collection1 == null)
			{
				throw new ArgumentNullException("collection1");
			}
			if (collection2 == null)
			{
				throw new ArgumentNullException("collection2");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			bool flag3;
			using (IEnumerator<T> enumerator = collection1.GetEnumerator())
			{
				using (IEnumerator<T> enumerator2 = collection2.GetEnumerator())
				{
					bool flag;
					bool flag2;
					do
					{
						flag = enumerator.MoveNext();
						flag2 = enumerator2.MoveNext();
						if (!flag || !flag2)
						{
							goto IL_0065;
						}
					}
					while (equalityComparer.Equals(enumerator.Current, enumerator2.Current));
					return false;
					IL_0065:
					flag3 = flag == flag2;
				}
			}
			return flag3;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00084050 File Offset: 0x00082250
		public static bool EqualCollections<T>(IEnumerable<T> collection1, IEnumerable<T> collection2, BinaryPredicate<T> predicate)
		{
			if (collection1 == null)
			{
				throw new ArgumentNullException("collection1");
			}
			if (collection2 == null)
			{
				throw new ArgumentNullException("collection2");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			bool flag3;
			using (IEnumerator<T> enumerator = collection1.GetEnumerator())
			{
				using (IEnumerator<T> enumerator2 = collection2.GetEnumerator())
				{
					bool flag;
					bool flag2;
					do
					{
						flag = enumerator.MoveNext();
						flag2 = enumerator2.MoveNext();
						if (!flag || !flag2)
						{
							goto IL_0065;
						}
					}
					while (predicate(enumerator.Current, enumerator2.Current));
					return false;
					IL_0065:
					flag3 = flag == flag2;
				}
			}
			return flag3;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000840FC File Offset: 0x000822FC
		public static T[] ToArray<T>(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				T[] array = new T[collection2.Count];
				collection2.CopyTo(array, 0);
				return array;
			}
			return new List<T>(collection).ToArray();
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00084144 File Offset: 0x00082344
		public static int Count<T>(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (collection is ICollection<T>)
			{
				return ((ICollection<T>)collection).Count;
			}
			int num = 0;
			foreach (T t in collection)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x000841B0 File Offset: 0x000823B0
		public static int CountEqual<T>(IEnumerable<T> collection, T find)
		{
			return Algorithms.CountEqual<T>(collection, find, EqualityComparer<T>.Default);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x000841C0 File Offset: 0x000823C0
		public static int CountEqual<T>(IEnumerable<T> collection, T find, IEqualityComparer<T> equalityComparer)
		{
			if (collection == null)
			{
				throw new ArgumentException("collection");
			}
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			int num = 0;
			foreach (T t in collection)
			{
				if (equalityComparer.Equals(t, find))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00084230 File Offset: 0x00082430
		public static IEnumerable<T> NCopiesOf<T>(int n, T item)
		{
			if (n < 0)
			{
				throw new ArgumentOutOfRangeException("n", n, Strings.ArgMustNotBeNegative);
			}
			for (;;)
			{
				int num = n;
				n = num - 1;
				if (num <= 0)
				{
					break;
				}
				yield return item;
			}
			yield break;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00084248 File Offset: 0x00082448
		public static void Fill<T>(IList<T> list, T value)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i] = value;
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00084298 File Offset: 0x00082498
		public static void Fill<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = value;
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000842CC File Offset: 0x000824CC
		public static void FillRange<T>(IList<T> list, int start, int count, T value)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			if (count == 0)
			{
				return;
			}
			if (start < 0 || start >= list.Count)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (count < 0 || count > list.Count || start > list.Count - count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			for (int i = start; i < count + start; i++)
			{
				list[i] = value;
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00084354 File Offset: 0x00082554
		public static void FillRange<T>(T[] array, int start, int count, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (count == 0)
			{
				return;
			}
			if (start < 0 || start >= array.Length)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (count < 0 || count > array.Length || start > array.Length - count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			for (int i = start; i < count + start; i++)
			{
				array[i] = value;
			}
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000843BB File Offset: 0x000825BB
		public static void Copy<T>(IEnumerable<T> source, IList<T> dest, int destIndex)
		{
			Algorithms.Copy<T>(source, dest, destIndex, int.MaxValue);
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000843CC File Offset: 0x000825CC
		public static void Copy<T>(IEnumerable<T> source, T[] dest, int destIndex)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (destIndex < 0 || destIndex > dest.Length)
			{
				throw new ArgumentOutOfRangeException("destIndex");
			}
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (destIndex >= dest.Length)
					{
						throw new ArgumentException(Strings.ArrayTooSmall, "array");
					}
					dest[destIndex++] = enumerator.Current;
				}
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00084460 File Offset: 0x00082660
		public static void Copy<T>(IEnumerable<T> source, IList<T> dest, int destIndex, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (dest.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "dest");
			}
			int count2 = dest.Count;
			if (destIndex < 0 || destIndex > count2)
			{
				throw new ArgumentOutOfRangeException("destIndex");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				while (destIndex < count2 && count > 0)
				{
					if (!enumerator.MoveNext())
					{
						break;
					}
					dest[destIndex++] = enumerator.Current;
					count--;
				}
				while (count > 0 && enumerator.MoveNext())
				{
					dest.Insert(count2++, enumerator.Current);
					count--;
				}
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0008453C File Offset: 0x0008273C
		public static void Copy<T>(IEnumerable<T> source, T[] dest, int destIndex, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			int num = dest.Length;
			if (destIndex < 0 || destIndex > num)
			{
				throw new ArgumentOutOfRangeException("destIndex");
			}
			if (count < 0 || destIndex + count > num)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				while (destIndex < num && count > 0 && enumerator.MoveNext())
				{
					dest[destIndex++] = enumerator.Current;
					count--;
				}
			}
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000845E0 File Offset: 0x000827E0
		public static void Copy<T>(IList<T> source, int sourceIndex, IList<T> dest, int destIndex, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (dest.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "dest");
			}
			int count2 = source.Count;
			int count3 = dest.Count;
			if (sourceIndex < 0 || sourceIndex >= count2)
			{
				throw new ArgumentOutOfRangeException("sourceIndex");
			}
			if (destIndex < 0 || destIndex > count3)
			{
				throw new ArgumentOutOfRangeException("destIndex");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > count2 - sourceIndex)
			{
				count = count2 - sourceIndex;
			}
			if (source == dest && sourceIndex > destIndex)
			{
				while (count > 0)
				{
					dest[destIndex++] = source[sourceIndex++];
					count--;
				}
				return;
			}
			int num;
			int num2;
			if (destIndex + count > count3)
			{
				int i = destIndex + count - count3;
				num = sourceIndex + (count - i);
				num2 = count3;
				count -= i;
				while (i > 0)
				{
					dest.Insert(num2++, source[num++]);
					i--;
				}
			}
			num = sourceIndex + count - 1;
			num2 = destIndex + count - 1;
			while (count > 0)
			{
				dest[num2--] = source[num--];
				count--;
			}
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00084714 File Offset: 0x00082914
		public static void Copy<T>(IList<T> source, int sourceIndex, T[] dest, int destIndex, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			int count2 = source.Count;
			int num = dest.Length;
			if (sourceIndex < 0 || sourceIndex >= count2)
			{
				throw new ArgumentOutOfRangeException("sourceIndex");
			}
			if (destIndex < 0 || destIndex > num)
			{
				throw new ArgumentOutOfRangeException("destIndex");
			}
			if (count < 0 || destIndex + count > num)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > count2 - sourceIndex)
			{
				count = count2 - sourceIndex;
			}
			if (source is T[])
			{
				Array.Copy((T[])source, sourceIndex, dest, destIndex, count);
				return;
			}
			int num2 = sourceIndex;
			int num3 = destIndex;
			while (count > 0)
			{
				dest[num3++] = source[num2++];
				count--;
			}
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000847D1 File Offset: 0x000829D1
		public static IEnumerable<T> Reverse<T>(IList<T> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int num;
			for (int i = source.Count - 1; i >= 0; i = num)
			{
				yield return source[i];
				num = i - 1;
			}
			yield break;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000847E4 File Offset: 0x000829E4
		public static void ReverseInPlace<T>(IList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list is T[])
			{
				list = new Algorithms.ArrayWrapper<T>((T[])list);
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			int i = 0;
			int num = list.Count - 1;
			while (i < num)
			{
				T t = list[i];
				list[i] = list[num];
				list[num] = t;
				i++;
				num--;
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00084863 File Offset: 0x00082A63
		public static IEnumerable<T> Rotate<T>(IList<T> source, int amountToRotate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int count = source.Count;
			if (count != 0)
			{
				amountToRotate %= count;
				if (amountToRotate < 0)
				{
					amountToRotate += count;
				}
				int num;
				for (int i = amountToRotate; i < count; i = num)
				{
					yield return source[i];
					num = i + 1;
				}
				for (int i = 0; i < amountToRotate; i = num)
				{
					yield return source[i];
					num = i + 1;
				}
			}
			yield break;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0008487C File Offset: 0x00082A7C
		public static void RotateInPlace<T>(IList<T> list, int amountToRotate)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list is T[])
			{
				list = new Algorithms.ArrayWrapper<T>((T[])list);
			}
			if (list.IsReadOnly)
			{
				throw new ArgumentException(Strings.ListIsReadOnly, "list");
			}
			int count = list.Count;
			if (count != 0)
			{
				amountToRotate %= count;
				if (amountToRotate < 0)
				{
					amountToRotate += count;
				}
				int i = count;
				int num = 0;
				while (i > 0)
				{
					int num2 = num;
					T t = list[num];
					for (;;)
					{
						i--;
						int num3 = num2 + amountToRotate;
						if (num3 >= count)
						{
							num3 -= count;
						}
						if (num3 == num)
						{
							break;
						}
						list[num2] = list[num3];
						num2 = num3;
					}
					list[num2] = t;
					num++;
				}
			}
		}

		// Token: 0x020016A8 RID: 5800
		[Serializable]
		private class ListRange<T> : ListBase<T>, ICollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600D8D8 RID: 55512 RVA: 0x004FED88 File Offset: 0x004FCF88
			public ListRange(IList<T> wrappedList, int start, int count)
			{
				this.wrappedList = wrappedList;
				this.start = start;
				this.count = count;
			}

			// Token: 0x17000D53 RID: 3411
			// (get) Token: 0x0600D8D9 RID: 55513 RVA: 0x004FEDA5 File Offset: 0x004FCFA5
			public override int Count
			{
				get
				{
					return Math.Min(this.count, this.wrappedList.Count - this.start);
				}
			}

			// Token: 0x0600D8DA RID: 55514 RVA: 0x004FEDC4 File Offset: 0x004FCFC4
			public override void Clear()
			{
				if (this.wrappedList.Count - this.start < this.count)
				{
					this.count = this.wrappedList.Count - this.start;
				}
				while (this.count > 0)
				{
					this.wrappedList.RemoveAt(this.start + this.count - 1);
					this.count--;
				}
			}

			// Token: 0x0600D8DB RID: 55515 RVA: 0x004FEE36 File Offset: 0x004FD036
			public override void Insert(int index, T item)
			{
				if (index < 0 || index > this.count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.wrappedList.Insert(this.start + index, item);
				this.count++;
			}

			// Token: 0x0600D8DC RID: 55516 RVA: 0x004FEE72 File Offset: 0x004FD072
			public override void RemoveAt(int index)
			{
				if (index < 0 || index >= this.count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.wrappedList.RemoveAt(this.start + index);
				this.count--;
			}

			// Token: 0x0600D8DD RID: 55517 RVA: 0x004FEEAD File Offset: 0x004FD0AD
			public override bool Remove(T item)
			{
				if (this.wrappedList.IsReadOnly)
				{
					throw new NotSupportedException(string.Format(Strings.CannotModifyCollection, "Range"));
				}
				return base.Remove(item);
			}

			// Token: 0x17000D54 RID: 3412
			public override T this[int index]
			{
				get
				{
					if (index < 0 || index >= this.count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.wrappedList[this.start + index];
				}
				set
				{
					if (index < 0 || index >= this.count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this.wrappedList[this.start + index] = value;
				}
			}

			// Token: 0x17000D55 RID: 3413
			// (get) Token: 0x0600D8E0 RID: 55520 RVA: 0x004FEF33 File Offset: 0x004FD133
			bool ICollection<T>.IsReadOnly
			{
				get
				{
					return this.wrappedList.IsReadOnly;
				}
			}

			// Token: 0x04005B1D RID: 23325
			private IList<T> wrappedList;

			// Token: 0x04005B1E RID: 23326
			private int start;

			// Token: 0x04005B1F RID: 23327
			private int count;
		}

		// Token: 0x020016A9 RID: 5801
		[Serializable]
		private class ArrayRange<T> : ListBase<T>
		{
			// Token: 0x0600D8E1 RID: 55521 RVA: 0x004FEF40 File Offset: 0x004FD140
			public ArrayRange(T[] wrappedArray, int start, int count)
			{
				this.wrappedArray = wrappedArray;
				this.start = start;
				this.count = count;
			}

			// Token: 0x17000D56 RID: 3414
			// (get) Token: 0x0600D8E2 RID: 55522 RVA: 0x004FEF5D File Offset: 0x004FD15D
			public override int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x0600D8E3 RID: 55523 RVA: 0x004FEF68 File Offset: 0x004FD168
			public override void Clear()
			{
				Array.Copy(this.wrappedArray, this.start + this.count, this.wrappedArray, this.start, this.wrappedArray.Length - (this.start + this.count));
				Algorithms.FillRange<T>(this.wrappedArray, this.wrappedArray.Length - this.count, this.count, default(T));
				this.count = 0;
			}

			// Token: 0x0600D8E4 RID: 55524 RVA: 0x004FEFE0 File Offset: 0x004FD1E0
			public override void Insert(int index, T item)
			{
				if (index < 0 || index > this.count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				int num = this.start + index;
				if (num + 1 < this.wrappedArray.Length)
				{
					Array.Copy(this.wrappedArray, num, this.wrappedArray, num + 1, this.wrappedArray.Length - num - 1);
				}
				if (num < this.wrappedArray.Length)
				{
					this.wrappedArray[num] = item;
				}
				if (this.start + this.count < this.wrappedArray.Length)
				{
					this.count++;
				}
			}

			// Token: 0x0600D8E5 RID: 55525 RVA: 0x004FF07C File Offset: 0x004FD27C
			public override void RemoveAt(int index)
			{
				if (index < 0 || index >= this.count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				int num = this.start + index;
				if (num < this.wrappedArray.Length - 1)
				{
					Array.Copy(this.wrappedArray, num + 1, this.wrappedArray, num, this.wrappedArray.Length - num - 1);
				}
				this.wrappedArray[this.wrappedArray.Length - 1] = default(T);
				this.count--;
			}

			// Token: 0x17000D57 RID: 3415
			public override T this[int index]
			{
				get
				{
					if (index < 0 || index >= this.count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.wrappedArray[this.start + index];
				}
				set
				{
					if (index < 0 || index >= this.count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this.wrappedArray[this.start + index] = value;
				}
			}

			// Token: 0x04005B20 RID: 23328
			private T[] wrappedArray;

			// Token: 0x04005B21 RID: 23329
			private int start;

			// Token: 0x04005B22 RID: 23330
			private int count;
		}

		// Token: 0x020016AA RID: 5802
		[Serializable]
		private class ReadOnlyCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600D8E8 RID: 55528 RVA: 0x004FF15F File Offset: 0x004FD35F
			public ReadOnlyCollection(ICollection<T> wrappedCollection)
			{
				this.wrappedCollection = wrappedCollection;
			}

			// Token: 0x0600D8E9 RID: 55529 RVA: 0x004FF16E File Offset: 0x004FD36E
			private void MethodModifiesCollection()
			{
				throw new NotSupportedException(string.Format(Strings.CannotModifyCollection, "read-only collection"));
			}

			// Token: 0x0600D8EA RID: 55530 RVA: 0x004FF184 File Offset: 0x004FD384
			public IEnumerator<T> GetEnumerator()
			{
				return this.wrappedCollection.GetEnumerator();
			}

			// Token: 0x0600D8EB RID: 55531 RVA: 0x004FF191 File Offset: 0x004FD391
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.wrappedCollection.GetEnumerator();
			}

			// Token: 0x0600D8EC RID: 55532 RVA: 0x004FF19E File Offset: 0x004FD39E
			public bool Contains(T item)
			{
				return this.wrappedCollection.Contains(item);
			}

			// Token: 0x0600D8ED RID: 55533 RVA: 0x004FF1AC File Offset: 0x004FD3AC
			public void CopyTo(T[] array, int arrayIndex)
			{
				this.wrappedCollection.CopyTo(array, arrayIndex);
			}

			// Token: 0x17000D58 RID: 3416
			// (get) Token: 0x0600D8EE RID: 55534 RVA: 0x004FF1BB File Offset: 0x004FD3BB
			public int Count
			{
				get
				{
					return this.wrappedCollection.Count;
				}
			}

			// Token: 0x17000D59 RID: 3417
			// (get) Token: 0x0600D8EF RID: 55535 RVA: 0x004FF1C8 File Offset: 0x004FD3C8
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600D8F0 RID: 55536 RVA: 0x004FF1CB File Offset: 0x004FD3CB
			public void Add(T item)
			{
				this.MethodModifiesCollection();
			}

			// Token: 0x0600D8F1 RID: 55537 RVA: 0x004FF1D3 File Offset: 0x004FD3D3
			public void Clear()
			{
				this.MethodModifiesCollection();
			}

			// Token: 0x0600D8F2 RID: 55538 RVA: 0x004FF1DB File Offset: 0x004FD3DB
			public bool Remove(T item)
			{
				this.MethodModifiesCollection();
				return false;
			}

			// Token: 0x04005B23 RID: 23331
			private ICollection<T> wrappedCollection;
		}

		// Token: 0x020016AB RID: 5803
		[Serializable]
		private class ReadOnlyList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600D8F3 RID: 55539 RVA: 0x004FF1E4 File Offset: 0x004FD3E4
			public ReadOnlyList(IList<T> wrappedList)
			{
				this.wrappedList = wrappedList;
			}

			// Token: 0x0600D8F4 RID: 55540 RVA: 0x004FF1F3 File Offset: 0x004FD3F3
			private void MethodModifiesCollection()
			{
				throw new NotSupportedException(string.Format(Strings.CannotModifyCollection, "read-only list"));
			}

			// Token: 0x0600D8F5 RID: 55541 RVA: 0x004FF209 File Offset: 0x004FD409
			public IEnumerator<T> GetEnumerator()
			{
				return this.wrappedList.GetEnumerator();
			}

			// Token: 0x0600D8F6 RID: 55542 RVA: 0x004FF216 File Offset: 0x004FD416
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.wrappedList.GetEnumerator();
			}

			// Token: 0x0600D8F7 RID: 55543 RVA: 0x004FF223 File Offset: 0x004FD423
			public int IndexOf(T item)
			{
				return this.wrappedList.IndexOf(item);
			}

			// Token: 0x0600D8F8 RID: 55544 RVA: 0x004FF231 File Offset: 0x004FD431
			public bool Contains(T item)
			{
				return this.wrappedList.Contains(item);
			}

			// Token: 0x0600D8F9 RID: 55545 RVA: 0x004FF23F File Offset: 0x004FD43F
			public void CopyTo(T[] array, int arrayIndex)
			{
				this.wrappedList.CopyTo(array, arrayIndex);
			}

			// Token: 0x17000D5A RID: 3418
			// (get) Token: 0x0600D8FA RID: 55546 RVA: 0x004FF24E File Offset: 0x004FD44E
			public int Count
			{
				get
				{
					return this.wrappedList.Count;
				}
			}

			// Token: 0x17000D5B RID: 3419
			// (get) Token: 0x0600D8FB RID: 55547 RVA: 0x004FF25B File Offset: 0x004FD45B
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000D5C RID: 3420
			public T this[int index]
			{
				get
				{
					return this.wrappedList[index];
				}
				set
				{
					this.MethodModifiesCollection();
				}
			}

			// Token: 0x0600D8FE RID: 55550 RVA: 0x004FF274 File Offset: 0x004FD474
			public void Add(T item)
			{
				this.MethodModifiesCollection();
			}

			// Token: 0x0600D8FF RID: 55551 RVA: 0x004FF27C File Offset: 0x004FD47C
			public void Clear()
			{
				this.MethodModifiesCollection();
			}

			// Token: 0x0600D900 RID: 55552 RVA: 0x004FF284 File Offset: 0x004FD484
			public void Insert(int index, T item)
			{
				this.MethodModifiesCollection();
			}

			// Token: 0x0600D901 RID: 55553 RVA: 0x004FF28C File Offset: 0x004FD48C
			public void RemoveAt(int index)
			{
				this.MethodModifiesCollection();
			}

			// Token: 0x0600D902 RID: 55554 RVA: 0x004FF294 File Offset: 0x004FD494
			public bool Remove(T item)
			{
				this.MethodModifiesCollection();
				return false;
			}

			// Token: 0x04005B24 RID: 23332
			private IList<T> wrappedList;
		}

		// Token: 0x020016AC RID: 5804
		[Serializable]
		private class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
		{
			// Token: 0x0600D903 RID: 55555 RVA: 0x004FF29D File Offset: 0x004FD49D
			public ReadOnlyDictionary(IDictionary<TKey, TValue> wrappedDictionary)
			{
				this.wrappedDictionary = wrappedDictionary;
			}

			// Token: 0x0600D904 RID: 55556 RVA: 0x004FF2AC File Offset: 0x004FD4AC
			private void MethodModifiesCollection()
			{
				throw new NotSupportedException(string.Format(Strings.CannotModifyCollection, "read-only dictionary"));
			}

			// Token: 0x0600D905 RID: 55557 RVA: 0x004FF2C2 File Offset: 0x004FD4C2
			public void Add(TKey key, TValue value)
			{
				this.MethodModifiesCollection();
			}

			// Token: 0x0600D906 RID: 55558 RVA: 0x004FF2CA File Offset: 0x004FD4CA
			public bool ContainsKey(TKey key)
			{
				return this.wrappedDictionary.ContainsKey(key);
			}

			// Token: 0x17000D5D RID: 3421
			// (get) Token: 0x0600D907 RID: 55559 RVA: 0x004FF2D8 File Offset: 0x004FD4D8
			public ICollection<TKey> Keys
			{
				get
				{
					return Algorithms.ReadOnly<TKey>(this.wrappedDictionary.Keys);
				}
			}

			// Token: 0x17000D5E RID: 3422
			// (get) Token: 0x0600D908 RID: 55560 RVA: 0x004FF2EA File Offset: 0x004FD4EA
			public ICollection<TValue> Values
			{
				get
				{
					return Algorithms.ReadOnly<TValue>(this.wrappedDictionary.Values);
				}
			}

			// Token: 0x0600D909 RID: 55561 RVA: 0x004FF2FC File Offset: 0x004FD4FC
			public bool Remove(TKey key)
			{
				this.MethodModifiesCollection();
				return false;
			}

			// Token: 0x0600D90A RID: 55562 RVA: 0x004FF305 File Offset: 0x004FD505
			public bool TryGetValue(TKey key, out TValue value)
			{
				return this.wrappedDictionary.TryGetValue(key, out value);
			}

			// Token: 0x17000D5F RID: 3423
			public TValue this[TKey key]
			{
				get
				{
					return this.wrappedDictionary[key];
				}
				set
				{
					this.MethodModifiesCollection();
				}
			}

			// Token: 0x0600D90D RID: 55565 RVA: 0x004FF32A File Offset: 0x004FD52A
			public void Add(KeyValuePair<TKey, TValue> item)
			{
				this.MethodModifiesCollection();
			}

			// Token: 0x0600D90E RID: 55566 RVA: 0x004FF332 File Offset: 0x004FD532
			public void Clear()
			{
				this.MethodModifiesCollection();
			}

			// Token: 0x0600D90F RID: 55567 RVA: 0x004FF33A File Offset: 0x004FD53A
			public bool Contains(KeyValuePair<TKey, TValue> item)
			{
				return this.wrappedDictionary.Contains(item);
			}

			// Token: 0x0600D910 RID: 55568 RVA: 0x004FF348 File Offset: 0x004FD548
			public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				this.wrappedDictionary.CopyTo(array, arrayIndex);
			}

			// Token: 0x17000D60 RID: 3424
			// (get) Token: 0x0600D911 RID: 55569 RVA: 0x004FF357 File Offset: 0x004FD557
			public int Count
			{
				get
				{
					return this.wrappedDictionary.Count;
				}
			}

			// Token: 0x17000D61 RID: 3425
			// (get) Token: 0x0600D912 RID: 55570 RVA: 0x004FF364 File Offset: 0x004FD564
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600D913 RID: 55571 RVA: 0x004FF367 File Offset: 0x004FD567
			public bool Remove(KeyValuePair<TKey, TValue> item)
			{
				this.MethodModifiesCollection();
				return false;
			}

			// Token: 0x0600D914 RID: 55572 RVA: 0x004FF370 File Offset: 0x004FD570
			public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
			{
				return this.wrappedDictionary.GetEnumerator();
			}

			// Token: 0x0600D915 RID: 55573 RVA: 0x004FF37D File Offset: 0x004FD57D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.wrappedDictionary.GetEnumerator();
			}

			// Token: 0x04005B25 RID: 23333
			private IDictionary<TKey, TValue> wrappedDictionary;
		}

		// Token: 0x020016AD RID: 5805
		[Serializable]
		private class TypedEnumerator<T> : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600D916 RID: 55574 RVA: 0x004FF38A File Offset: 0x004FD58A
			public TypedEnumerator(IEnumerator wrappedEnumerator)
			{
				this.wrappedEnumerator = wrappedEnumerator;
			}

			// Token: 0x17000D62 RID: 3426
			// (get) Token: 0x0600D917 RID: 55575 RVA: 0x004FF399 File Offset: 0x004FD599
			T IEnumerator<T>.Current
			{
				get
				{
					return (T)((object)this.wrappedEnumerator.Current);
				}
			}

			// Token: 0x0600D918 RID: 55576 RVA: 0x004FF3AB File Offset: 0x004FD5AB
			void IDisposable.Dispose()
			{
				if (this.wrappedEnumerator is IDisposable)
				{
					((IDisposable)this.wrappedEnumerator).Dispose();
				}
			}

			// Token: 0x17000D63 RID: 3427
			// (get) Token: 0x0600D919 RID: 55577 RVA: 0x004FF3CA File Offset: 0x004FD5CA
			object IEnumerator.Current
			{
				get
				{
					return this.wrappedEnumerator.Current;
				}
			}

			// Token: 0x0600D91A RID: 55578 RVA: 0x004FF3D7 File Offset: 0x004FD5D7
			bool IEnumerator.MoveNext()
			{
				return this.wrappedEnumerator.MoveNext();
			}

			// Token: 0x0600D91B RID: 55579 RVA: 0x004FF3E4 File Offset: 0x004FD5E4
			void IEnumerator.Reset()
			{
				this.wrappedEnumerator.Reset();
			}

			// Token: 0x04005B26 RID: 23334
			private IEnumerator wrappedEnumerator;
		}

		// Token: 0x020016AE RID: 5806
		[Serializable]
		private class TypedEnumerable<T> : IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600D91C RID: 55580 RVA: 0x004FF3F1 File Offset: 0x004FD5F1
			public TypedEnumerable(IEnumerable wrappedEnumerable)
			{
				this.wrappedEnumerable = wrappedEnumerable;
			}

			// Token: 0x0600D91D RID: 55581 RVA: 0x004FF400 File Offset: 0x004FD600
			public IEnumerator<T> GetEnumerator()
			{
				return new Algorithms.TypedEnumerator<T>(this.wrappedEnumerable.GetEnumerator());
			}

			// Token: 0x0600D91E RID: 55582 RVA: 0x004FF412 File Offset: 0x004FD612
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.wrappedEnumerable.GetEnumerator();
			}

			// Token: 0x04005B27 RID: 23335
			private IEnumerable wrappedEnumerable;
		}

		// Token: 0x020016AF RID: 5807
		[Serializable]
		private class TypedCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600D91F RID: 55583 RVA: 0x004FF41F File Offset: 0x004FD61F
			public TypedCollection(ICollection wrappedCollection)
			{
				this.wrappedCollection = wrappedCollection;
			}

			// Token: 0x0600D920 RID: 55584 RVA: 0x004FF42E File Offset: 0x004FD62E
			private void MethodModifiesCollection()
			{
				throw new NotSupportedException(string.Format(Strings.CannotModifyCollection, "strongly-typed Collection"));
			}

			// Token: 0x0600D921 RID: 55585 RVA: 0x004FF444 File Offset: 0x004FD644
			public void Add(T item)
			{
				this.MethodModifiesCollection();
			}

			// Token: 0x0600D922 RID: 55586 RVA: 0x004FF44C File Offset: 0x004FD64C
			public void Clear()
			{
				this.MethodModifiesCollection();
			}

			// Token: 0x0600D923 RID: 55587 RVA: 0x004FF454 File Offset: 0x004FD654
			public bool Remove(T item)
			{
				this.MethodModifiesCollection();
				return false;
			}

			// Token: 0x0600D924 RID: 55588 RVA: 0x004FF460 File Offset: 0x004FD660
			public bool Contains(T item)
			{
				IEqualityComparer<T> @default = EqualityComparer<T>.Default;
				foreach (object obj in this.wrappedCollection)
				{
					if (obj is T && @default.Equals(item, (T)((object)obj)))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600D925 RID: 55589 RVA: 0x004FF4D4 File Offset: 0x004FD6D4
			public void CopyTo(T[] array, int arrayIndex)
			{
				this.wrappedCollection.CopyTo(array, arrayIndex);
			}

			// Token: 0x17000D64 RID: 3428
			// (get) Token: 0x0600D926 RID: 55590 RVA: 0x004FF4E3 File Offset: 0x004FD6E3
			public int Count
			{
				get
				{
					return this.wrappedCollection.Count;
				}
			}

			// Token: 0x17000D65 RID: 3429
			// (get) Token: 0x0600D927 RID: 55591 RVA: 0x004FF4F0 File Offset: 0x004FD6F0
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600D928 RID: 55592 RVA: 0x004FF4F3 File Offset: 0x004FD6F3
			public IEnumerator<T> GetEnumerator()
			{
				return new Algorithms.TypedEnumerator<T>(this.wrappedCollection.GetEnumerator());
			}

			// Token: 0x0600D929 RID: 55593 RVA: 0x004FF505 File Offset: 0x004FD705
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.wrappedCollection.GetEnumerator();
			}

			// Token: 0x04005B28 RID: 23336
			private ICollection wrappedCollection;
		}

		// Token: 0x020016B0 RID: 5808
		[Serializable]
		private class TypedList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600D92A RID: 55594 RVA: 0x004FF512 File Offset: 0x004FD712
			public TypedList(IList wrappedList)
			{
				this.wrappedList = wrappedList;
			}

			// Token: 0x0600D92B RID: 55595 RVA: 0x004FF521 File Offset: 0x004FD721
			public IEnumerator<T> GetEnumerator()
			{
				return new Algorithms.TypedEnumerator<T>(this.wrappedList.GetEnumerator());
			}

			// Token: 0x0600D92C RID: 55596 RVA: 0x004FF533 File Offset: 0x004FD733
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.wrappedList.GetEnumerator();
			}

			// Token: 0x0600D92D RID: 55597 RVA: 0x004FF540 File Offset: 0x004FD740
			public int IndexOf(T item)
			{
				return this.wrappedList.IndexOf(item);
			}

			// Token: 0x0600D92E RID: 55598 RVA: 0x004FF553 File Offset: 0x004FD753
			public void Insert(int index, T item)
			{
				this.wrappedList.Insert(index, item);
			}

			// Token: 0x0600D92F RID: 55599 RVA: 0x004FF567 File Offset: 0x004FD767
			public void RemoveAt(int index)
			{
				this.wrappedList.RemoveAt(index);
			}

			// Token: 0x0600D930 RID: 55600 RVA: 0x004FF575 File Offset: 0x004FD775
			public void Add(T item)
			{
				this.wrappedList.Add(item);
			}

			// Token: 0x0600D931 RID: 55601 RVA: 0x004FF589 File Offset: 0x004FD789
			public void Clear()
			{
				this.wrappedList.Clear();
			}

			// Token: 0x0600D932 RID: 55602 RVA: 0x004FF596 File Offset: 0x004FD796
			public bool Contains(T item)
			{
				return this.wrappedList.Contains(item);
			}

			// Token: 0x0600D933 RID: 55603 RVA: 0x004FF5A9 File Offset: 0x004FD7A9
			public void CopyTo(T[] array, int arrayIndex)
			{
				this.wrappedList.CopyTo(array, arrayIndex);
			}

			// Token: 0x17000D66 RID: 3430
			public T this[int index]
			{
				get
				{
					return (T)((object)this.wrappedList[index]);
				}
				set
				{
					this.wrappedList[index] = value;
				}
			}

			// Token: 0x17000D67 RID: 3431
			// (get) Token: 0x0600D936 RID: 55606 RVA: 0x004FF5DF File Offset: 0x004FD7DF
			public int Count
			{
				get
				{
					return this.wrappedList.Count;
				}
			}

			// Token: 0x17000D68 RID: 3432
			// (get) Token: 0x0600D937 RID: 55607 RVA: 0x004FF5EC File Offset: 0x004FD7EC
			public bool IsReadOnly
			{
				get
				{
					return this.wrappedList.IsReadOnly;
				}
			}

			// Token: 0x0600D938 RID: 55608 RVA: 0x004FF5F9 File Offset: 0x004FD7F9
			public bool Remove(T item)
			{
				if (this.wrappedList.Contains(item))
				{
					this.wrappedList.Remove(item);
					return true;
				}
				return false;
			}

			// Token: 0x04005B29 RID: 23337
			private IList wrappedList;
		}

		// Token: 0x020016B1 RID: 5809
		[Serializable]
		private class UntypedCollection<T> : ICollection, IEnumerable
		{
			// Token: 0x0600D939 RID: 55609 RVA: 0x004FF622 File Offset: 0x004FD822
			public UntypedCollection(ICollection<T> wrappedCollection)
			{
				this.wrappedCollection = wrappedCollection;
			}

			// Token: 0x0600D93A RID: 55610 RVA: 0x004FF634 File Offset: 0x004FD834
			public void CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				int num = 0;
				int count = this.wrappedCollection.Count;
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, Strings.ArgMustNotBeNegative);
				}
				if (index >= array.Length || count > array.Length - index)
				{
					throw new ArgumentException("index", Strings.ArrayTooSmall);
				}
				foreach (T t in this.wrappedCollection)
				{
					if (num >= count)
					{
						break;
					}
					array.SetValue(t, index);
					index++;
					num++;
				}
			}

			// Token: 0x17000D69 RID: 3433
			// (get) Token: 0x0600D93B RID: 55611 RVA: 0x004FF6F0 File Offset: 0x004FD8F0
			public int Count
			{
				get
				{
					return this.wrappedCollection.Count;
				}
			}

			// Token: 0x17000D6A RID: 3434
			// (get) Token: 0x0600D93C RID: 55612 RVA: 0x004FF6FD File Offset: 0x004FD8FD
			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000D6B RID: 3435
			// (get) Token: 0x0600D93D RID: 55613 RVA: 0x004FF700 File Offset: 0x004FD900
			public object SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600D93E RID: 55614 RVA: 0x004FF703 File Offset: 0x004FD903
			public IEnumerator GetEnumerator()
			{
				return this.wrappedCollection.GetEnumerator();
			}

			// Token: 0x04005B2A RID: 23338
			private ICollection<T> wrappedCollection;
		}

		// Token: 0x020016B2 RID: 5810
		[Serializable]
		private class UntypedList<T> : IList, ICollection, IEnumerable
		{
			// Token: 0x0600D93F RID: 55615 RVA: 0x004FF710 File Offset: 0x004FD910
			public UntypedList(IList<T> wrappedList)
			{
				this.wrappedList = wrappedList;
			}

			// Token: 0x0600D940 RID: 55616 RVA: 0x004FF720 File Offset: 0x004FD920
			private T ConvertToItemType(string name, object value)
			{
				T t;
				try
				{
					t = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					throw new ArgumentException(string.Format(Strings.WrongType, value, typeof(T)), name);
				}
				return t;
			}

			// Token: 0x0600D941 RID: 55617 RVA: 0x004FF764 File Offset: 0x004FD964
			public int Add(object value)
			{
				this.wrappedList.Add(this.ConvertToItemType("value", value));
				return this.wrappedList.Count - 1;
			}

			// Token: 0x0600D942 RID: 55618 RVA: 0x004FF78A File Offset: 0x004FD98A
			public void Clear()
			{
				this.wrappedList.Clear();
			}

			// Token: 0x0600D943 RID: 55619 RVA: 0x004FF797 File Offset: 0x004FD997
			public bool Contains(object value)
			{
				return value is T && this.wrappedList.Contains((T)((object)value));
			}

			// Token: 0x0600D944 RID: 55620 RVA: 0x004FF7B4 File Offset: 0x004FD9B4
			public int IndexOf(object value)
			{
				if (value is T)
				{
					return this.wrappedList.IndexOf((T)((object)value));
				}
				return -1;
			}

			// Token: 0x0600D945 RID: 55621 RVA: 0x004FF7D1 File Offset: 0x004FD9D1
			public void Insert(int index, object value)
			{
				this.wrappedList.Insert(index, this.ConvertToItemType("value", value));
			}

			// Token: 0x17000D6C RID: 3436
			// (get) Token: 0x0600D946 RID: 55622 RVA: 0x004FF7EB File Offset: 0x004FD9EB
			public bool IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000D6D RID: 3437
			// (get) Token: 0x0600D947 RID: 55623 RVA: 0x004FF7EE File Offset: 0x004FD9EE
			public bool IsReadOnly
			{
				get
				{
					return this.wrappedList.IsReadOnly;
				}
			}

			// Token: 0x0600D948 RID: 55624 RVA: 0x004FF7FB File Offset: 0x004FD9FB
			public void Remove(object value)
			{
				if (value is T)
				{
					this.wrappedList.Remove((T)((object)value));
				}
			}

			// Token: 0x0600D949 RID: 55625 RVA: 0x004FF817 File Offset: 0x004FDA17
			public void RemoveAt(int index)
			{
				this.wrappedList.RemoveAt(index);
			}

			// Token: 0x17000D6E RID: 3438
			public object this[int index]
			{
				get
				{
					return this.wrappedList[index];
				}
				set
				{
					this.wrappedList[index] = this.ConvertToItemType("value", value);
				}
			}

			// Token: 0x0600D94C RID: 55628 RVA: 0x004FF854 File Offset: 0x004FDA54
			public void CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				int num = 0;
				int count = this.wrappedList.Count;
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, Strings.ArgMustNotBeNegative);
				}
				if (index >= array.Length || count > array.Length - index)
				{
					throw new ArgumentException("index", Strings.ArrayTooSmall);
				}
				foreach (T t in this.wrappedList)
				{
					if (num >= count)
					{
						break;
					}
					array.SetValue(t, index);
					index++;
					num++;
				}
			}

			// Token: 0x17000D6F RID: 3439
			// (get) Token: 0x0600D94D RID: 55629 RVA: 0x004FF910 File Offset: 0x004FDB10
			public int Count
			{
				get
				{
					return this.wrappedList.Count;
				}
			}

			// Token: 0x17000D70 RID: 3440
			// (get) Token: 0x0600D94E RID: 55630 RVA: 0x004FF91D File Offset: 0x004FDB1D
			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000D71 RID: 3441
			// (get) Token: 0x0600D94F RID: 55631 RVA: 0x004FF920 File Offset: 0x004FDB20
			public object SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600D950 RID: 55632 RVA: 0x004FF923 File Offset: 0x004FDB23
			public IEnumerator GetEnumerator()
			{
				return this.wrappedList.GetEnumerator();
			}

			// Token: 0x04005B2B RID: 23339
			private IList<T> wrappedList;
		}

		// Token: 0x020016B3 RID: 5811
		[Serializable]
		private class ArrayWrapper<T> : ListBase<T>, IList, ICollection, IEnumerable
		{
			// Token: 0x0600D951 RID: 55633 RVA: 0x004FF930 File Offset: 0x004FDB30
			public ArrayWrapper(T[] wrappedArray)
			{
				this.wrappedArray = wrappedArray;
			}

			// Token: 0x17000D72 RID: 3442
			// (get) Token: 0x0600D952 RID: 55634 RVA: 0x004FF93F File Offset: 0x004FDB3F
			public override int Count
			{
				get
				{
					return this.wrappedArray.Length;
				}
			}

			// Token: 0x0600D953 RID: 55635 RVA: 0x004FF94C File Offset: 0x004FDB4C
			public override void Clear()
			{
				int num = this.wrappedArray.Length;
				for (int i = 0; i < num; i++)
				{
					this.wrappedArray[i] = default(T);
				}
			}

			// Token: 0x0600D954 RID: 55636 RVA: 0x004FF984 File Offset: 0x004FDB84
			public override void Insert(int index, T item)
			{
				if (index < 0 || index > this.wrappedArray.Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index + 1 < this.wrappedArray.Length)
				{
					Array.Copy(this.wrappedArray, index, this.wrappedArray, index + 1, this.wrappedArray.Length - index - 1);
				}
				if (index < this.wrappedArray.Length)
				{
					this.wrappedArray[index] = item;
				}
			}

			// Token: 0x0600D955 RID: 55637 RVA: 0x004FF9F4 File Offset: 0x004FDBF4
			public override void RemoveAt(int index)
			{
				if (index < 0 || index >= this.wrappedArray.Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index < this.wrappedArray.Length - 1)
				{
					Array.Copy(this.wrappedArray, index + 1, this.wrappedArray, index, this.wrappedArray.Length - index - 1);
				}
				this.wrappedArray[this.wrappedArray.Length - 1] = default(T);
			}

			// Token: 0x17000D73 RID: 3443
			public override T this[int index]
			{
				get
				{
					if (index < 0 || index >= this.wrappedArray.Length)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.wrappedArray[index];
				}
				set
				{
					if (index < 0 || index >= this.wrappedArray.Length)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this.wrappedArray[index] = value;
				}
			}

			// Token: 0x0600D958 RID: 55640 RVA: 0x004FFAB8 File Offset: 0x004FDCB8
			public override void CopyTo(T[] array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Length < this.wrappedArray.Length)
				{
					throw new ArgumentException("array is too short", "array");
				}
				if (arrayIndex < 0 || arrayIndex >= array.Length)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				if (array.Length + arrayIndex < this.wrappedArray.Length)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				Array.Copy(this.wrappedArray, 0, array, arrayIndex, this.wrappedArray.Length);
			}

			// Token: 0x0600D959 RID: 55641 RVA: 0x004FFB35 File Offset: 0x004FDD35
			public override IEnumerator<T> GetEnumerator()
			{
				return this.wrappedArray.GetEnumerator();
			}

			// Token: 0x0600D95A RID: 55642 RVA: 0x004FFB42 File Offset: 0x004FDD42
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.wrappedArray.GetEnumerator();
			}

			// Token: 0x17000D74 RID: 3444
			// (get) Token: 0x0600D95B RID: 55643 RVA: 0x004FFB4F File Offset: 0x004FDD4F
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04005B2C RID: 23340
			private T[] wrappedArray;
		}

		// Token: 0x020016B4 RID: 5812
		[Serializable]
		private class LexicographicalComparerClass<T> : IComparer<IEnumerable<T>>
		{
			// Token: 0x0600D95C RID: 55644 RVA: 0x004FFB52 File Offset: 0x004FDD52
			public LexicographicalComparerClass(IComparer<T> itemComparer)
			{
				this.itemComparer = itemComparer;
			}

			// Token: 0x0600D95D RID: 55645 RVA: 0x004FFB61 File Offset: 0x004FDD61
			public int Compare(IEnumerable<T> x, IEnumerable<T> y)
			{
				return Algorithms.LexicographicalCompare<T>(x, y, this.itemComparer);
			}

			// Token: 0x0600D95E RID: 55646 RVA: 0x004FFB70 File Offset: 0x004FDD70
			public override bool Equals(object obj)
			{
				return obj is Algorithms.LexicographicalComparerClass<T> && this.itemComparer.Equals(((Algorithms.LexicographicalComparerClass<T>)obj).itemComparer);
			}

			// Token: 0x0600D95F RID: 55647 RVA: 0x004FFB92 File Offset: 0x004FDD92
			public override int GetHashCode()
			{
				return this.itemComparer.GetHashCode();
			}

			// Token: 0x04005B2D RID: 23341
			private IComparer<T> itemComparer;
		}

		// Token: 0x020016B5 RID: 5813
		[Serializable]
		private class ReverseComparerClass<T> : IComparer<T>
		{
			// Token: 0x0600D960 RID: 55648 RVA: 0x004FFB9F File Offset: 0x004FDD9F
			public ReverseComparerClass(IComparer<T> comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x0600D961 RID: 55649 RVA: 0x004FFBAE File Offset: 0x004FDDAE
			public int Compare(T x, T y)
			{
				return -this.comparer.Compare(x, y);
			}

			// Token: 0x0600D962 RID: 55650 RVA: 0x004FFBBE File Offset: 0x004FDDBE
			public override bool Equals(object obj)
			{
				return obj is Algorithms.ReverseComparerClass<T> && this.comparer.Equals(((Algorithms.ReverseComparerClass<T>)obj).comparer);
			}

			// Token: 0x0600D963 RID: 55651 RVA: 0x004FFBE0 File Offset: 0x004FDDE0
			public override int GetHashCode()
			{
				return this.comparer.GetHashCode();
			}

			// Token: 0x04005B2E RID: 23342
			private IComparer<T> comparer;
		}

		// Token: 0x020016B6 RID: 5814
		[Serializable]
		private class IdentityComparer<T> : IEqualityComparer<T> where T : class
		{
			// Token: 0x0600D964 RID: 55652 RVA: 0x004FFBED File Offset: 0x004FDDED
			public bool Equals(T x, T y)
			{
				return x == y;
			}

			// Token: 0x0600D965 RID: 55653 RVA: 0x004FFBFD File Offset: 0x004FDDFD
			public int GetHashCode(T obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}

			// Token: 0x0600D966 RID: 55654 RVA: 0x004FFC0A File Offset: 0x004FDE0A
			public override bool Equals(object obj)
			{
				return obj != null && obj is Algorithms.IdentityComparer<T>;
			}

			// Token: 0x0600D967 RID: 55655 RVA: 0x004FFC1A File Offset: 0x004FDE1A
			public override int GetHashCode()
			{
				return 1900273135;
			}
		}

		// Token: 0x020016B7 RID: 5815
		[Serializable]
		private class CollectionEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
		{
			// Token: 0x0600D969 RID: 55657 RVA: 0x004FFC29 File Offset: 0x004FDE29
			public CollectionEqualityComparer(IEqualityComparer<T> equalityComparer)
			{
				this.equalityComparer = equalityComparer;
			}

			// Token: 0x0600D96A RID: 55658 RVA: 0x004FFC38 File Offset: 0x004FDE38
			public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
			{
				return Algorithms.EqualCollections<T>(x, y, this.equalityComparer);
			}

			// Token: 0x0600D96B RID: 55659 RVA: 0x004FFC48 File Offset: 0x004FDE48
			public int GetHashCode(IEnumerable<T> obj)
			{
				int num = 927934782;
				foreach (T t in obj)
				{
					int hashCode = Util.GetHashCode<T>(t, this.equalityComparer);
					num += hashCode;
					num = (num << 9) | (num >> 23);
				}
				return num & int.MaxValue;
			}

			// Token: 0x04005B2F RID: 23343
			private IEqualityComparer<T> equalityComparer;
		}

		// Token: 0x020016B8 RID: 5816
		[Serializable]
		private class SetEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
		{
			// Token: 0x0600D96C RID: 55660 RVA: 0x004FFCB0 File Offset: 0x004FDEB0
			public SetEqualityComparer(IEqualityComparer<T> equalityComparer)
			{
				this.equalityComparer = equalityComparer;
			}

			// Token: 0x0600D96D RID: 55661 RVA: 0x004FFCBF File Offset: 0x004FDEBF
			public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
			{
				return Algorithms.EqualSets<T>(x, y, this.equalityComparer);
			}

			// Token: 0x0600D96E RID: 55662 RVA: 0x004FFCD0 File Offset: 0x004FDED0
			public int GetHashCode(IEnumerable<T> obj)
			{
				int num = 1649354556;
				foreach (T t in obj)
				{
					int hashCode = Util.GetHashCode<T>(t, this.equalityComparer);
					num += hashCode;
				}
				return num & int.MaxValue;
			}

			// Token: 0x04005B30 RID: 23344
			private IEqualityComparer<T> equalityComparer;
		}
	}
}
