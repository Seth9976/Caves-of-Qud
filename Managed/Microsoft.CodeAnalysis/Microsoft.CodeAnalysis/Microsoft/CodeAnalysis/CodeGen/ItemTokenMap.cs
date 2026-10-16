using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004C7 RID: 1223
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ItemTokenMap<T> where T : class
	{
		// Token: 0x0600313E RID: 12606 RVA: 0x0008D5BC File Offset: 0x0008B7BC
		public uint GetOrAddTokenFor(T item)
		{
			uint num;
			if (this._itemToToken.TryGetValue(item, ref num))
			{
				return num;
			}
			return this.AddItem(item);
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x0008D5E4 File Offset: 0x0008B7E4
		private uint AddItem(T item)
		{
			ArrayBuilder<T> items = this._items;
			uint count;
			lock (items)
			{
				if (this._itemToToken.TryGetValue(item, ref count))
				{
					return count;
				}
				count = (uint)this._items.Count;
				this._items.Add(item);
				this._itemToToken.Add(item, count);
			}
			return count;
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x0008D65C File Offset: 0x0008B85C
		public T GetItem(uint token)
		{
			ArrayBuilder<T> items = this._items;
			T t;
			lock (items)
			{
				t = this._items[(int)token];
			}
			return t;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x0008D6A4 File Offset: 0x0008B8A4
		public IEnumerable<T> GetAllItems()
		{
			ArrayBuilder<T> items = this._items;
			IEnumerable<T> enumerable;
			lock (items)
			{
				enumerable = this._items.ToArray();
			}
			return enumerable;
		}

		// Token: 0x040012FD RID: 4861
		private readonly ConcurrentDictionary<T, uint> _itemToToken = new ConcurrentDictionary<T, uint>(ReferenceEqualityComparer.Instance);

		// Token: 0x040012FE RID: 4862
		private readonly ArrayBuilder<T> _items = new ArrayBuilder<T>();
	}
}
