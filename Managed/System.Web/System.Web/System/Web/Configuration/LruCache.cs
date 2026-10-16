using System;
using System.Collections.Generic;

namespace System.Web.Configuration
{
	// Token: 0x020005B1 RID: 1457
	internal class LruCache<TKey, TValue>
	{
		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x06003E78 RID: 15992 RVA: 0x000A3522 File Offset: 0x000A1722
		// (set) Token: 0x06003E77 RID: 15991 RVA: 0x000A3519 File Offset: 0x000A1719
		internal string EvictionWarning { private get; set; }

		// Token: 0x06003E79 RID: 15993 RVA: 0x000A352A File Offset: 0x000A172A
		public LruCache(int entryLimit)
		{
			this.entry_limit = entryLimit;
			this.dict = new Dictionary<TKey, LinkedListNode<TValue>>();
			this.revdict = new Dictionary<LinkedListNode<TValue>, TKey>();
			this.list = new LinkedList<TValue>();
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x000A355C File Offset: 0x000A175C
		private void Evict()
		{
			LinkedListNode<TValue> last = this.list.Last;
			if (last == null)
			{
				return;
			}
			TKey tkey = this.revdict[last];
			this.dict.Remove(tkey);
			this.revdict.Remove(last);
			this.list.RemoveLast();
			this.DisposeValue(last.Value);
			this.evictions++;
			if (!string.IsNullOrEmpty(this.EvictionWarning) && !this.eviction_warning_shown && this.evictions >= this.entry_limit)
			{
				Console.Error.WriteLine("WARNING: " + this.EvictionWarning);
				this.eviction_warning_shown = true;
			}
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x000A360C File Offset: 0x000A180C
		public void Clear()
		{
			foreach (TValue tvalue in this.list)
			{
				this.DisposeValue(tvalue);
			}
			this.dict.Clear();
			this.revdict.Clear();
			this.list.Clear();
			this.eviction_warning_shown = false;
			this.evictions = 0;
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x000A3690 File Offset: 0x000A1890
		private void DisposeValue(TValue value)
		{
			if (value is IDisposable)
			{
				((IDisposable)((object)value)).Dispose();
			}
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x000A36B0 File Offset: 0x000A18B0
		public bool TryGetValue(TKey key, out TValue value)
		{
			LinkedListNode<TValue> linkedListNode;
			if (this.dict.TryGetValue(key, out linkedListNode))
			{
				this.list.Remove(linkedListNode);
				this.list.AddFirst(linkedListNode);
				value = linkedListNode.Value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x000A36FC File Offset: 0x000A18FC
		public void Add(TKey key, TValue value)
		{
			LinkedListNode<TValue> linkedListNode;
			if (this.dict.TryGetValue(key, out linkedListNode))
			{
				this.list.Remove(linkedListNode);
				this.list.AddFirst(linkedListNode);
				this.DisposeValue(linkedListNode.Value);
				linkedListNode.Value = value;
				return;
			}
			if (this.dict.Count >= this.entry_limit)
			{
				this.Evict();
			}
			linkedListNode = new LinkedListNode<TValue>(value);
			this.list.AddFirst(linkedListNode);
			this.dict[key] = linkedListNode;
			this.revdict[linkedListNode] = key;
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x000A378B File Offset: 0x000A198B
		public override string ToString()
		{
			return "LRUCache dict={0} revdict={1} list={2}";
		}

		// Token: 0x04002233 RID: 8755
		private Dictionary<TKey, LinkedListNode<TValue>> dict;

		// Token: 0x04002234 RID: 8756
		private Dictionary<LinkedListNode<TValue>, TKey> revdict;

		// Token: 0x04002235 RID: 8757
		private LinkedList<TValue> list;

		// Token: 0x04002236 RID: 8758
		private int entry_limit;

		// Token: 0x04002237 RID: 8759
		private bool eviction_warning_shown;

		// Token: 0x04002238 RID: 8760
		private int evictions;
	}
}
