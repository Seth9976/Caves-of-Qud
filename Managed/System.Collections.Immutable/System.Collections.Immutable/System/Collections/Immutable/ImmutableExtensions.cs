using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000030 RID: 48
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ImmutableExtensions
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x0000623C File Offset: 0x0000443C
		[NullableContext(2)]
		internal static bool IsValueType<T>()
		{
			if (default(T) != null)
			{
				return true;
			}
			Type typeFromHandle = typeof(T);
			return typeFromHandle.IsConstructedGenericType && typeFromHandle.GetGenericTypeDefinition() == typeof(Nullable);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006288 File Offset: 0x00004488
		internal static IOrderedCollection<T> AsOrderedCollection<[Nullable(2)] T>(this IEnumerable<T> sequence)
		{
			Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
			IOrderedCollection<T> orderedCollection = sequence as IOrderedCollection<T>;
			if (orderedCollection != null)
			{
				return orderedCollection;
			}
			IList<T> list = sequence as IList<T>;
			if (list != null)
			{
				return new ImmutableExtensions.ListOfTWrapper<T>(list);
			}
			return new ImmutableExtensions.FallbackWrapper<T>(sequence);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000062C3 File Offset: 0x000044C3
		internal static void ClearFastWhenEmpty<[Nullable(2)] T>(this Stack<T> stack)
		{
			if (stack.Count > 0)
			{
				stack.Clear();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000062D4 File Offset: 0x000044D4
		[return: Nullable(new byte[] { 0, 1, 0 })]
		internal static DisposableEnumeratorAdapter<T, TEnumerator> GetEnumerableDisposable<[Nullable(2)] T, [Nullable(0)] TEnumerator>(this IEnumerable<T> enumerable) where TEnumerator : struct, IStrongEnumerator<T>, IEnumerator<T>
		{
			Requires.NotNull<IEnumerable<T>>(enumerable, "enumerable");
			IStrongEnumerable<T, TEnumerator> strongEnumerable = enumerable as IStrongEnumerable<T, TEnumerator>;
			if (strongEnumerable != null)
			{
				return new DisposableEnumeratorAdapter<T, TEnumerator>(strongEnumerable.GetEnumerator());
			}
			return new DisposableEnumeratorAdapter<T, TEnumerator>(enumerable.GetEnumerator());
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000630D File Offset: 0x0000450D
		internal static bool TryGetCount<[Nullable(2)] T>(this IEnumerable<T> sequence, out int count)
		{
			return sequence.TryGetCount<T>(out count);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006318 File Offset: 0x00004518
		internal static bool TryGetCount<[Nullable(2)] T>(this IEnumerable sequence, out int count)
		{
			ICollection collection = sequence as ICollection;
			if (collection != null)
			{
				count = collection.Count;
				return true;
			}
			ICollection<T> collection2 = sequence as ICollection<T>;
			if (collection2 != null)
			{
				count = collection2.Count;
				return true;
			}
			IReadOnlyCollection<T> readOnlyCollection = sequence as IReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				count = readOnlyCollection.Count;
				return true;
			}
			count = 0;
			return false;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006368 File Offset: 0x00004568
		internal static int GetCount<[Nullable(2)] T>(ref IEnumerable<T> sequence)
		{
			int count;
			if (!sequence.TryGetCount<T>(out count))
			{
				List<T> list = Enumerable.ToList<T>(sequence);
				count = list.Count;
				sequence = list;
			}
			return count;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006394 File Offset: 0x00004594
		internal static bool TryCopyTo<[Nullable(2)] T>(this IEnumerable<T> sequence, T[] array, int arrayIndex)
		{
			IList<T> list = sequence as IList<T>;
			if (list != null)
			{
				List<T> list2 = sequence as List<T>;
				if (list2 != null)
				{
					list2.CopyTo(array, arrayIndex);
					return true;
				}
				if (sequence.GetType() == typeof(T[]))
				{
					T[] array2 = (T[])sequence;
					Array.Copy(array2, 0, array, arrayIndex, array2.Length);
					return true;
				}
				if (sequence is ImmutableArray<T>)
				{
					ImmutableArray<T> immutableArray = (ImmutableArray<T>)sequence;
					Array.Copy(immutableArray.array, 0, array, arrayIndex, immutableArray.Length);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006414 File Offset: 0x00004614
		internal static T[] ToArray<[Nullable(2)] T>(this IEnumerable<T> sequence, int count)
		{
			Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
			Requires.Range(count >= 0, "count", null);
			if (count == 0)
			{
				return ImmutableArray<T>.Empty.array;
			}
			T[] array = new T[count];
			if (!sequence.TryCopyTo<T>(array, 0))
			{
				int num = 0;
				foreach (T t in sequence)
				{
					Requires.Argument(num < count);
					array[num++] = t;
				}
				Requires.Argument(num == count);
			}
			return array;
		}

		// Token: 0x0200006A RID: 106
		private class ListOfTWrapper<T> : IOrderedCollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x060004B7 RID: 1207 RVA: 0x0000C370 File Offset: 0x0000A570
			internal ListOfTWrapper(IList<T> collection)
			{
				Requires.NotNull<IList<T>>(collection, "collection");
				this._collection = collection;
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000C38A File Offset: 0x0000A58A
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x170000E3 RID: 227
			public T this[int index]
			{
				get
				{
					return this._collection[index];
				}
			}

			// Token: 0x060004BA RID: 1210 RVA: 0x0000C3A5 File Offset: 0x0000A5A5
			public IEnumerator<T> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			// Token: 0x060004BB RID: 1211 RVA: 0x0000C3B2 File Offset: 0x0000A5B2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040000AC RID: 172
			private readonly IList<T> _collection;
		}

		// Token: 0x0200006B RID: 107
		private class FallbackWrapper<T> : IOrderedCollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x060004BC RID: 1212 RVA: 0x0000C3BA File Offset: 0x0000A5BA
			internal FallbackWrapper(IEnumerable<T> sequence)
			{
				Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
				this._sequence = sequence;
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
			public int Count
			{
				get
				{
					if (this._collection == null)
					{
						int num;
						if (this._sequence.TryGetCount<T>(out num))
						{
							return num;
						}
						this._collection = Enumerable.ToArray<T>(this._sequence);
					}
					return this._collection.Count;
				}
			}

			// Token: 0x170000E5 RID: 229
			public T this[int index]
			{
				get
				{
					if (this._collection == null)
					{
						this._collection = Enumerable.ToArray<T>(this._sequence);
					}
					return this._collection[index];
				}
			}

			// Token: 0x060004BF RID: 1215 RVA: 0x0000C43D File Offset: 0x0000A63D
			public IEnumerator<T> GetEnumerator()
			{
				return this._sequence.GetEnumerator();
			}

			// Token: 0x060004C0 RID: 1216 RVA: 0x0000C44A File Offset: 0x0000A64A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040000AD RID: 173
			private readonly IEnumerable<T> _sequence;

			// Token: 0x040000AE RID: 174
			private IList<T> _collection;
		}
	}
}
