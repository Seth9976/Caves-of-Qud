using System;
using System.Collections.Generic;

namespace System.Web.Caching
{
	// Token: 0x0200067B RID: 1659
	internal sealed class CacheItemLRU
	{
		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x000C5964 File Offset: 0x000C3B64
		public int Count
		{
			get
			{
				return this.dict.Count;
			}
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x000C5974 File Offset: 0x000C3B74
		public CacheItemLRU(Cache owner, int highWaterMark, int lowWaterMark)
		{
			this.list = new LinkedList<CacheItem>();
			this.dict = new Dictionary<string, LinkedListNode<CacheItem>>(StringComparer.Ordinal);
			this.revdict = new Dictionary<LinkedListNode<CacheItem>, string>();
			this.highWaterMark = highWaterMark;
			this.lowWaterMark = lowWaterMark;
			this.owner = owner;
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x000C59C4 File Offset: 0x000C3BC4
		public bool TryGetValue(string key, out CacheItem value)
		{
			LinkedListNode<CacheItem> linkedListNode;
			if (this.dict.TryGetValue(key, out linkedListNode))
			{
				value = linkedListNode.Value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x000C59F0 File Offset: 0x000C3BF0
		public void EvictIfNecessary()
		{
			if (!this.needsEviction)
			{
				return;
			}
			for (int i = this.dict.Count; i > this.lowWaterMark; i--)
			{
				string text = this.revdict[this.list.Last];
				this.owner.Remove(text, CacheItemRemovedReason.Underused, false, true);
			}
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x000C5A48 File Offset: 0x000C3C48
		public void InvokePrivateCallbacks()
		{
			foreach (KeyValuePair<string, LinkedListNode<CacheItem>> keyValuePair in this.dict)
			{
				CacheItem value = keyValuePair.Value.Value;
				if (value != null && !value.Disabled && value.OnRemoveCallback != null)
				{
					try
					{
						value.OnRemoveCallback(keyValuePair.Key, value.Value, CacheItemRemovedReason.Removed);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x000C5AE0 File Offset: 0x000C3CE0
		public List<CacheItem> SelectItems(CacheItemLRU.SelectItemsQualifier qualifier)
		{
			List<CacheItem> list = new List<CacheItem>();
			foreach (LinkedListNode<CacheItem> linkedListNode in this.dict.Values)
			{
				CacheItem value = linkedListNode.Value;
				if (qualifier(value))
				{
					list.Add(value);
				}
			}
			return list;
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x000C5B50 File Offset: 0x000C3D50
		public List<CacheItem> ToList()
		{
			List<CacheItem> list = new List<CacheItem>();
			if (this.dict.Count == 0)
			{
				return list;
			}
			foreach (LinkedListNode<CacheItem> linkedListNode in this.dict.Values)
			{
				list.Add(linkedListNode.Value);
			}
			return list;
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x000C5BC4 File Offset: 0x000C3DC4
		public void Remove(string key)
		{
			if (key == null)
			{
				return;
			}
			LinkedListNode<CacheItem> linkedListNode;
			if (!this.dict.TryGetValue(key, out linkedListNode))
			{
				return;
			}
			CacheItem value = linkedListNode.Value;
			this.dict.Remove(key);
			if (value == null || value.Priority != CacheItemPriority.NotRemovable)
			{
				this.revdict.Remove(linkedListNode);
				this.list.Remove(linkedListNode);
			}
		}

		// Token: 0x17001602 RID: 5634
		public CacheItem this[string key]
		{
			get
			{
				if (key == null)
				{
					return null;
				}
				LinkedListNode<CacheItem> linkedListNode;
				if (this.dict.TryGetValue(key, out linkedListNode))
				{
					CacheItem value = linkedListNode.Value;
					if (value == null || value.Priority != CacheItemPriority.NotRemovable)
					{
						this.list.Remove(linkedListNode);
						this.list.AddFirst(linkedListNode);
					}
					return value;
				}
				return null;
			}
			set
			{
				LinkedListNode<CacheItem> linkedListNode;
				if (this.dict.TryGetValue(key, out linkedListNode))
				{
					this.list.Remove(linkedListNode);
					if (value == null || value.Priority != CacheItemPriority.NotRemovable)
					{
						this.list.AddFirst(linkedListNode);
					}
					else
					{
						this.revdict.Remove(linkedListNode);
					}
					linkedListNode.Value = value;
					return;
				}
				this.needsEviction = this.dict.Count >= this.highWaterMark;
				linkedListNode = new LinkedListNode<CacheItem>(value);
				if (value == null || value.Priority != CacheItemPriority.NotRemovable)
				{
					this.list.AddFirst(linkedListNode);
					this.revdict[linkedListNode] = key;
				}
				this.dict[key] = linkedListNode;
			}
		}

		// Token: 0x04002578 RID: 9592
		private Dictionary<string, LinkedListNode<CacheItem>> dict;

		// Token: 0x04002579 RID: 9593
		private Dictionary<LinkedListNode<CacheItem>, string> revdict;

		// Token: 0x0400257A RID: 9594
		private LinkedList<CacheItem> list;

		// Token: 0x0400257B RID: 9595
		private Cache owner;

		// Token: 0x0400257C RID: 9596
		private int highWaterMark;

		// Token: 0x0400257D RID: 9597
		private int lowWaterMark;

		// Token: 0x0400257E RID: 9598
		private bool needsEviction;

		// Token: 0x0200067C RID: 1660
		// (Invoke) Token: 0x0600470A RID: 18186
		public delegate bool SelectItemsQualifier(CacheItem item);
	}
}
