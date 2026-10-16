using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class KeysOrValuesCollectionAccessor<TKey, [Nullable(2)] TValue, [Nullable(2)] T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000902D File Offset: 0x0000722D
		protected KeysOrValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary, IEnumerable<T> keysOrValues)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, "dictionary");
			Requires.NotNull<IEnumerable<T>>(keysOrValues, "keysOrValues");
			this._dictionary = dictionary;
			this._keysOrValues = keysOrValues;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00009059 File Offset: 0x00007259
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000905C File Offset: 0x0000725C
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00009069 File Offset: 0x00007269
		protected IImmutableDictionary<TKey, TValue> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009071 File Offset: 0x00007271
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009078 File Offset: 0x00007278
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000363 RID: 867
		public abstract bool Contains(T item);

		// Token: 0x06000364 RID: 868 RVA: 0x00009080 File Offset: 0x00007280
		public void CopyTo(T[] array, int arrayIndex)
		{
			Requires.NotNull<T[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00009108 File Offset: 0x00007308
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000910F File Offset: 0x0000730F
		public IEnumerator<T> GetEnumerator()
		{
			return this._keysOrValues.GetEnumerator();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000911C File Offset: 0x0000731C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00009124 File Offset: 0x00007324
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array.SetValue(t, arrayIndex++);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000369 RID: 873 RVA: 0x000091B4 File Offset: 0x000073B4
		[DebuggerBrowsable(0)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600036A RID: 874 RVA: 0x000091B7 File Offset: 0x000073B7
		[DebuggerBrowsable(0)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400003D RID: 61
		private readonly IImmutableDictionary<TKey, TValue> _dictionary;

		// Token: 0x0400003E RID: 62
		private readonly IEnumerable<T> _keysOrValues;
	}
}
