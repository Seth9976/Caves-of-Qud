using System;
using System.Collections.Generic;
using System.Threading;

namespace XRL.Collections
{
	// Token: 0x020005CD RID: 1485
	public class LRUCache<KeyType, ValueType>
	{
		// Token: 0x06003F4F RID: 16207 RVA: 0x00137F90 File Offset: 0x00136190
		public LRUCache(int capacity, LRUCache<KeyType, ValueType>.IGenerator generator, IEqualityComparer<KeyType> equalityComparer = null)
		{
			this.Capacity = capacity;
			this.Generator = generator;
			this.Comparer = equalityComparer ?? EqualityComparer<KeyType>.Default;
			bool flag = true;
			LRUCache<KeyType, ValueType>.CacheEntry cacheEntry = null;
			this.rwlock.EnterWriteLock();
			try
			{
				this.Cache = new Dictionary<KeyType, LRUCache<KeyType, ValueType>.CacheEntry>(capacity, equalityComparer);
				uint num = 0U;
				while ((ulong)num < (ulong)((long)capacity))
				{
					LRUCache<KeyType, ValueType>.CacheEntry cacheEntry2 = new LRUCache<KeyType, ValueType>.CacheEntry();
					if (flag)
					{
						this.Head = cacheEntry2;
						flag = false;
					}
					if (cacheEntry != null)
					{
						cacheEntry.Prev = cacheEntry2;
						cacheEntry2.Next = cacheEntry;
					}
					cacheEntry = cacheEntry2;
					num += 1U;
				}
				this.Tail = cacheEntry;
			}
			finally
			{
				this.rwlock.ExitWriteLock();
			}
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x00138040 File Offset: 0x00136240
		~LRUCache()
		{
			this.rwlock.Dispose();
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x00138074 File Offset: 0x00136274
		public ValueType Get(KeyType Key)
		{
			this.rwlock.EnterUpgradeableReadLock();
			ValueType valueType;
			try
			{
				LRUCache<KeyType, ValueType>.CacheEntry tail;
				if (this.Comparer.Equals(Key, this.Head.Key))
				{
					valueType = this.Head.Value;
				}
				else if (this.Cache.TryGetValue(Key, out tail))
				{
					this.MoveToHead(tail);
					valueType = tail.Value;
				}
				else
				{
					this.rwlock.EnterWriteLock();
					try
					{
						tail = this.Tail;
						if (tail.Key != null)
						{
							this.Cache.Remove(tail.Key);
						}
						tail.Key = Key;
						tail.Value = this.Generator.Generate(Key);
						this.Cache.Add(Key, tail);
						this.MoveToHead(tail);
						valueType = tail.Value;
					}
					finally
					{
						this.rwlock.ExitWriteLock();
					}
				}
			}
			finally
			{
				this.rwlock.ExitUpgradeableReadLock();
			}
			return valueType;
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x00138174 File Offset: 0x00136374
		protected void MoveToHead(LRUCache<KeyType, ValueType>.CacheEntry entry)
		{
			if (entry == this.Head)
			{
				return;
			}
			bool isWriteLockHeld = this.rwlock.IsWriteLockHeld;
			if (!isWriteLockHeld)
			{
				this.rwlock.EnterWriteLock();
			}
			try
			{
				if (entry.Next != null)
				{
					entry.Next.Prev = entry.Prev;
				}
				if (entry.Prev == null)
				{
					this.Tail = entry.Next;
				}
				else
				{
					entry.Prev.Next = entry.Next;
				}
				entry.Next = null;
				entry.Prev = this.Head;
				this.Head.Next = entry;
				this.Head = entry;
			}
			finally
			{
				if (!isWriteLockHeld)
				{
					this.rwlock.ExitWriteLock();
				}
			}
		}

		// Token: 0x04001F3C RID: 7996
		protected Dictionary<KeyType, LRUCache<KeyType, ValueType>.CacheEntry> Cache;

		// Token: 0x04001F3D RID: 7997
		protected LRUCache<KeyType, ValueType>.CacheEntry Head;

		// Token: 0x04001F3E RID: 7998
		protected LRUCache<KeyType, ValueType>.CacheEntry Tail;

		// Token: 0x04001F3F RID: 7999
		protected int Capacity;

		// Token: 0x04001F40 RID: 8000
		protected LRUCache<KeyType, ValueType>.IGenerator Generator;

		// Token: 0x04001F41 RID: 8001
		protected IEqualityComparer<KeyType> Comparer;

		// Token: 0x04001F42 RID: 8002
		protected readonly ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();

		// Token: 0x020019E0 RID: 6624
		protected class CacheEntry
		{
			// Token: 0x04006784 RID: 26500
			public KeyType Key;

			// Token: 0x04006785 RID: 26501
			public ValueType Value;

			// Token: 0x04006786 RID: 26502
			public LRUCache<KeyType, ValueType>.CacheEntry Next;

			// Token: 0x04006787 RID: 26503
			public LRUCache<KeyType, ValueType>.CacheEntry Prev;
		}

		// Token: 0x020019E1 RID: 6625
		public interface IGenerator
		{
			// Token: 0x0600E589 RID: 58761
			ValueType Generate(KeyType Key);
		}
	}
}
