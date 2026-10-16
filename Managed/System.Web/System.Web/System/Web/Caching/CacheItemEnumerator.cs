using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.Caching
{
	// Token: 0x0200067A RID: 1658
	internal sealed class CacheItemEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x060046F7 RID: 18167 RVA: 0x000C588A File Offset: 0x000C3A8A
		public CacheItemEnumerator(List<CacheItem> list)
		{
			this.list = list;
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x060046F8 RID: 18168 RVA: 0x000C58A0 File Offset: 0x000C3AA0
		private CacheItem Item
		{
			get
			{
				if (this.pos < 0 || this.pos >= this.list.Count)
				{
					throw new InvalidOperationException();
				}
				return this.list[this.pos];
			}
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x060046F9 RID: 18169 RVA: 0x000C58D8 File Offset: 0x000C3AD8
		public DictionaryEntry Entry
		{
			get
			{
				CacheItem item = this.Item;
				if (item == null)
				{
					return new DictionaryEntry(null, null);
				}
				return new DictionaryEntry(item.Key, item.Value);
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x060046FA RID: 18170 RVA: 0x000C5908 File Offset: 0x000C3B08
		public object Key
		{
			get
			{
				return this.Item.Key;
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x000C5915 File Offset: 0x000C3B15
		public object Value
		{
			get
			{
				return this.Item.Value;
			}
		}

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x060046FC RID: 18172 RVA: 0x000C5922 File Offset: 0x000C3B22
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x000C5930 File Offset: 0x000C3B30
		public bool MoveNext()
		{
			int num = this.pos + 1;
			this.pos = num;
			return num < this.list.Count;
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x000C595B File Offset: 0x000C3B5B
		public void Reset()
		{
			this.pos = -1;
		}

		// Token: 0x04002576 RID: 9590
		private List<CacheItem> list;

		// Token: 0x04002577 RID: 9591
		private int pos = -1;
	}
}
