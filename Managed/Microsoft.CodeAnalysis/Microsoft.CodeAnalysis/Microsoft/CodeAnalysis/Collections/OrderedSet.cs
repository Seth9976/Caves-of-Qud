using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x0200049F RID: 1183
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrderedSet<[Nullable(2)] T> : IEnumerable<T>, IEnumerable, IReadOnlySet<T>, IReadOnlyList<T>, IReadOnlyCollection<T>, IOrderedReadOnlySet<T>
	{
		// Token: 0x06002EAE RID: 11950 RVA: 0x000839AB File Offset: 0x00081BAB
		public OrderedSet()
		{
			this._set = new HashSet<T>();
			this._list = new ArrayBuilder<T>();
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000839C9 File Offset: 0x00081BC9
		public OrderedSet(IEnumerable<T> items)
			: this()
		{
			this.AddRange(items);
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000839D8 File Offset: 0x00081BD8
		public void AddRange(IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				this.Add(t);
			}
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x00083A24 File Offset: 0x00081C24
		public bool Add(T item)
		{
			if (this._set.Add(item))
			{
				this._list.Add(item);
				return true;
			}
			return false;
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x00083A43 File Offset: 0x00081C43
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17000D80 RID: 3456
		public T this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x00083A5E File Offset: 0x00081C5E
		public bool Contains(T item)
		{
			return this._set.Contains(item);
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x00083A6C File Offset: 0x00081C6C
		[return: Nullable(new byte[] { 0, 1 })]
		public ArrayBuilder<T>.Enumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x00083A79 File Offset: 0x00081C79
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x00083A86 File Offset: 0x00081C86
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x00083A93 File Offset: 0x00081C93
		public void Clear()
		{
			this._set.Clear();
			this._list.Clear();
		}

		// Token: 0x0400127C RID: 4732
		private readonly HashSet<T> _set;

		// Token: 0x0400127D RID: 4733
		private readonly ArrayBuilder<T> _list;
	}
}
