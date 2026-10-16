using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000035 RID: 53
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class ConcurrentSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00004998 File Offset: 0x00002B98
		public ConcurrentSet()
		{
			this._dictionary = new ConcurrentDictionary<T, byte>(2, 31);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000049AE File Offset: 0x00002BAE
		public ConcurrentSet(IEqualityComparer<T> equalityComparer)
		{
			this._dictionary = new ConcurrentDictionary<T, byte>(2, 31, equalityComparer);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000049C5 File Offset: 0x00002BC5
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000049D2 File Offset: 0x00002BD2
		public bool IsEmpty
		{
			get
			{
				return this._dictionary.IsEmpty;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000049DF File Offset: 0x00002BDF
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000049E2 File Offset: 0x00002BE2
		public bool Contains(T value)
		{
			return this._dictionary.ContainsKey(value);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000049F0 File Offset: 0x00002BF0
		public bool Add(T value)
		{
			return this._dictionary.TryAdd(value, 0);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004A00 File Offset: 0x00002C00
		public void AddRange([Nullable(new byte[] { 2, 1 })] IEnumerable<T> values)
		{
			if (values != null)
			{
				foreach (T t in values)
				{
					this.Add(t);
				}
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004A4C File Offset: 0x00002C4C
		public bool Remove(T value)
		{
			byte b;
			return this._dictionary.TryRemove(value, ref b);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004A67 File Offset: 0x00002C67
		public void Clear()
		{
			this._dictionary.Clear();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004A74 File Offset: 0x00002C74
		[NullableContext(0)]
		public ConcurrentSet<T>.KeyEnumerator GetEnumerator()
		{
			return new ConcurrentSet<T>.KeyEnumerator(this._dictionary);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004A81 File Offset: 0x00002C81
		private IEnumerator<T> GetEnumeratorImpl()
		{
			foreach (KeyValuePair<T, byte> keyValuePair in this._dictionary)
			{
				yield return keyValuePair.Key;
			}
			IEnumerator<KeyValuePair<T, byte>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004A90 File Offset: 0x00002C90
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumeratorImpl();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004A98 File Offset: 0x00002C98
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorImpl();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004AA0 File Offset: 0x00002CA0
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004AAC File Offset: 0x00002CAC
		public void CopyTo(T[] array, int arrayIndex)
		{
			foreach (T t in this)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x04000036 RID: 54
		private const int DefaultConcurrencyLevel = 2;

		// Token: 0x04000037 RID: 55
		private const int DefaultCapacity = 31;

		// Token: 0x04000038 RID: 56
		private readonly ConcurrentDictionary<T, byte> _dictionary;

		// Token: 0x020004F0 RID: 1264
		[Nullable(0)]
		public struct KeyEnumerator
		{
			// Token: 0x060032AB RID: 12971 RVA: 0x0008FE65 File Offset: 0x0008E065
			internal KeyEnumerator([Nullable(new byte[] { 1, 0, 1 })] IEnumerable<KeyValuePair<T, byte>> data)
			{
				this._kvpEnumerator = data.GetEnumerator();
			}

			// Token: 0x17000EA4 RID: 3748
			// (get) Token: 0x060032AC RID: 12972 RVA: 0x0008FE74 File Offset: 0x0008E074
			public T Current
			{
				get
				{
					KeyValuePair<T, byte> keyValuePair = this._kvpEnumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x060032AD RID: 12973 RVA: 0x0008FE94 File Offset: 0x0008E094
			public bool MoveNext()
			{
				return this._kvpEnumerator.MoveNext();
			}

			// Token: 0x060032AE RID: 12974 RVA: 0x0008FEA1 File Offset: 0x0008E0A1
			public void Reset()
			{
				this._kvpEnumerator.Reset();
			}

			// Token: 0x040013D7 RID: 5079
			[Nullable(new byte[] { 1, 0, 1 })]
			private readonly IEnumerator<KeyValuePair<T, byte>> _kvpEnumerator;
		}
	}
}
