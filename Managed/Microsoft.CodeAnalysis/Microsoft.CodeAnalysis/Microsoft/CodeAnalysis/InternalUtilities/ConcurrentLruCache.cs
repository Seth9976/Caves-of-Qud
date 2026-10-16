using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.InternalUtilities
{
	// Token: 0x020002CF RID: 719
	[NullableContext(1)]
	[Nullable(0)]
	internal class ConcurrentLruCache<K, V>
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x00056CC0 File Offset: 0x00054EC0
		public ConcurrentLruCache(int capacity)
		{
			if (capacity <= 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this._capacity = capacity;
			this._cache = new Dictionary<K, ConcurrentLruCache<K, V>.CacheValue>(capacity);
			this._nodeList = new LinkedList<K>();
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00056D00 File Offset: 0x00054F00
		public ConcurrentLruCache([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<K, V>[] array)
			: this(array.Length)
		{
			foreach (KeyValuePair<K, V> keyValuePair in array)
			{
				this.UnsafeAdd(keyValuePair.Key, keyValuePair.Value, true);
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x00056D44 File Offset: 0x00054F44
		[Nullable(new byte[] { 1, 0, 1, 1 })]
		internal IEnumerable<KeyValuePair<K, V>> TestingEnumerable
		{
			[return: Nullable(new byte[] { 1, 0, 1, 1 })]
			get
			{
				object lockObject = this._lockObject;
				IEnumerable<KeyValuePair<K, V>> enumerable;
				lock (lockObject)
				{
					KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._cache.Count];
					int num = 0;
					foreach (K k in this._nodeList)
					{
						array[num++] = new KeyValuePair<K, V>(k, this._cache[k].Value);
					}
					enumerable = array;
				}
				return enumerable;
			}
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00056DF8 File Offset: 0x00054FF8
		public void Add(K key, V value)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				this.UnsafeAdd(key, value, true);
			}
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00056E3C File Offset: 0x0005503C
		private void MoveNodeToTop(LinkedListNode<K> node)
		{
			if (this._nodeList.First != node)
			{
				this._nodeList.Remove(node);
				this._nodeList.AddFirst(node);
			}
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x00056E64 File Offset: 0x00055064
		private void UnsafeEvictLastNode()
		{
			LinkedListNode<K> last = this._nodeList.Last;
			this._nodeList.Remove(last);
			this._cache.Remove(last.Value);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00056E9C File Offset: 0x0005509C
		private void UnsafeAddNodeToTop(K key, V value)
		{
			LinkedListNode<K> linkedListNode = new LinkedListNode<K>(key);
			this._cache.Add(key, new ConcurrentLruCache<K, V>.CacheValue
			{
				Node = linkedListNode,
				Value = value
			});
			this._nodeList.AddFirst(linkedListNode);
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00056EE4 File Offset: 0x000550E4
		private void UnsafeAdd(K key, V value, bool throwExceptionIfKeyExists)
		{
			ConcurrentLruCache<K, V>.CacheValue cacheValue;
			if (this._cache.TryGetValue(key, ref cacheValue))
			{
				if (throwExceptionIfKeyExists)
				{
					throw new ArgumentException("Key already exists", "key");
				}
				if (!cacheValue.Value.Equals(value))
				{
					cacheValue.Value = value;
					this._cache[key] = cacheValue;
					this.MoveNodeToTop(cacheValue.Node);
					return;
				}
			}
			else
			{
				if (this._cache.Count == this._capacity)
				{
					this.UnsafeEvictLastNode();
				}
				this.UnsafeAddNodeToTop(key, value);
			}
		}

		// Token: 0x170007E6 RID: 2022
		public V this[K key]
		{
			get
			{
				V v;
				if (this.TryGetValue(key, out v))
				{
					return v;
				}
				throw new KeyNotFoundException();
			}
			set
			{
				object lockObject = this._lockObject;
				lock (lockObject)
				{
					this.UnsafeAdd(key, value, false);
				}
			}
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00056FD8 File Offset: 0x000551D8
		public bool TryGetValue(K key, [MaybeNullWhen(false)] out V value)
		{
			object lockObject = this._lockObject;
			bool flag2;
			lock (lockObject)
			{
				flag2 = this.UnsafeTryGetValue(key, out value);
			}
			return flag2;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0005701C File Offset: 0x0005521C
		public bool UnsafeTryGetValue(K key, [MaybeNullWhen(false)] out V value)
		{
			ConcurrentLruCache<K, V>.CacheValue cacheValue;
			if (this._cache.TryGetValue(key, ref cacheValue))
			{
				this.MoveNodeToTop(cacheValue.Node);
				value = cacheValue.Value;
				return true;
			}
			value = default(V);
			return false;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0005705C File Offset: 0x0005525C
		public V GetOrAdd(K key, V value)
		{
			object lockObject = this._lockObject;
			V v2;
			lock (lockObject)
			{
				V v;
				if (this.UnsafeTryGetValue(key, out v))
				{
					v2 = v;
				}
				else
				{
					this.UnsafeAdd(key, value, true);
					v2 = value;
				}
			}
			return v2;
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000570B4 File Offset: 0x000552B4
		public V GetOrAdd(K key, Func<V> creator)
		{
			object lockObject = this._lockObject;
			V v2;
			lock (lockObject)
			{
				V v;
				if (this.UnsafeTryGetValue(key, out v))
				{
					v2 = v;
				}
				else
				{
					V v3 = creator.Invoke();
					this.UnsafeAdd(key, v3, true);
					v2 = v3;
				}
			}
			return v2;
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00057114 File Offset: 0x00055314
		public V GetOrAdd<[Nullable(2)] T>(K key, T arg, Func<T, V> creator)
		{
			object lockObject = this._lockObject;
			V v2;
			lock (lockObject)
			{
				V v;
				if (this.UnsafeTryGetValue(key, out v))
				{
					v2 = v;
				}
				else
				{
					V v3 = creator.Invoke(arg);
					this.UnsafeAdd(key, v3, true);
					v2 = v3;
				}
			}
			return v2;
		}

		// Token: 0x04000D86 RID: 3462
		private readonly int _capacity;

		// Token: 0x04000D87 RID: 3463
		[Nullable(new byte[] { 1, 1, 0, 0, 0 })]
		private readonly Dictionary<K, ConcurrentLruCache<K, V>.CacheValue> _cache;

		// Token: 0x04000D88 RID: 3464
		private readonly LinkedList<K> _nodeList;

		// Token: 0x04000D89 RID: 3465
		private readonly object _lockObject = new object();

		// Token: 0x0200065F RID: 1631
		[Nullable(0)]
		private struct CacheValue
		{
			// Token: 0x04001876 RID: 6262
			public V Value;

			// Token: 0x04001877 RID: 6263
			public LinkedListNode<K> Node;
		}
	}
}
