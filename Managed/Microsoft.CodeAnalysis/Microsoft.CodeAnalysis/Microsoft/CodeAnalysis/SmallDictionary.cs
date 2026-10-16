using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000120 RID: 288
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SmallDictionary<K, [Nullable(2)] V> : IEnumerable<KeyValuePair<K, V>>, IEnumerable
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x0001CEE5 File Offset: 0x0001B0E5
		public SmallDictionary()
			: this(EqualityComparer<K>.Default)
		{
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001CEF2 File Offset: 0x0001B0F2
		public SmallDictionary(IEqualityComparer<K> comparer)
		{
			this.Comparer = comparer;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001CF04 File Offset: 0x0001B104
		public SmallDictionary(SmallDictionary<K, V> other, IEqualityComparer<K> comparer)
			: this(comparer)
		{
			foreach (KeyValuePair<K, V> keyValuePair in other)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0001CF46 File Offset: 0x0001B146
		private bool CompareKeys(K k1, K k2)
		{
			return this.Comparer.Equals(k1, k2);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001CF55 File Offset: 0x0001B155
		private int GetHashCode(K k)
		{
			return this.Comparer.GetHashCode(k);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001CF63 File Offset: 0x0001B163
		public bool TryGetValue(K key, [MaybeNullWhen(false)] out V value)
		{
			if (this._root != null)
			{
				return this.TryGetValue(this.GetHashCode(key), key, out value);
			}
			value = default(V);
			return false;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0001CF85 File Offset: 0x0001B185
		public void Add(K key, V value)
		{
			this.Insert(this.GetHashCode(key), key, value, true);
		}

		// Token: 0x170001FE RID: 510
		public V this[K key]
		{
			get
			{
				V v;
				if (!this.TryGetValue(key, out v))
				{
					throw new KeyNotFoundException(string.Format("Could not find key {0}", key));
				}
				return v;
			}
			set
			{
				this.Insert(this.GetHashCode(key), key, value, false);
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		public bool ContainsKey(K key)
		{
			V v;
			return this.TryGetValue(key, out v);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001CFF2 File Offset: 0x0001B1F2
		[Conditional("DEBUG")]
		internal void AssertBalanced()
		{
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		private bool TryGetValue(int hashCode, K key, [MaybeNullWhen(false)] out V value)
		{
			SmallDictionary<K, V>.AvlNode avlNode = this._root;
			do
			{
				if (avlNode.HashCode > hashCode)
				{
					avlNode = avlNode.Left;
				}
				else
				{
					if (avlNode.HashCode >= hashCode)
					{
						goto IL_0035;
					}
					avlNode = avlNode.Right;
				}
			}
			while (avlNode != null);
			value = default(V);
			return false;
			IL_0035:
			if (this.CompareKeys(avlNode.Key, key))
			{
				value = avlNode.Value;
				return true;
			}
			return this.GetFromList(avlNode.Next, key, out value);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001D061 File Offset: 0x0001B261
		private bool GetFromList([Nullable(new byte[] { 2, 0, 0 })] SmallDictionary<K, V>.Node next, K key, [MaybeNullWhen(false)] out V value)
		{
			while (next != null)
			{
				if (this.CompareKeys(key, next.Key))
				{
					value = next.Value;
					return true;
				}
				next = next.Next;
			}
			value = default(V);
			return false;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001D098 File Offset: 0x0001B298
		private void Insert(int hashCode, K key, V value, bool add)
		{
			SmallDictionary<K, V>.AvlNode avlNode = this._root;
			if (avlNode == null)
			{
				this._root = new SmallDictionary<K, V>.AvlNode(hashCode, key, value);
				return;
			}
			SmallDictionary<K, V>.AvlNode avlNode2 = null;
			SmallDictionary<K, V>.AvlNode avlNode3 = avlNode;
			SmallDictionary<K, V>.AvlNode avlNode4 = null;
			for (;;)
			{
				int hashCode2 = avlNode.HashCode;
				if (avlNode.Balance != 0)
				{
					avlNode4 = avlNode2;
					avlNode3 = avlNode;
				}
				if (hashCode2 > hashCode)
				{
					if (avlNode.Left == null)
					{
						break;
					}
					avlNode2 = avlNode;
					avlNode = avlNode.Left;
				}
				else
				{
					if (hashCode2 >= hashCode)
					{
						goto IL_0087;
					}
					if (avlNode.Right == null)
					{
						goto Block_6;
					}
					avlNode2 = avlNode;
					avlNode = avlNode.Right;
				}
			}
			SmallDictionary<K, V>.AvlNode avlNode5 = avlNode;
			avlNode = new SmallDictionary<K, V>.AvlNode(hashCode, key, value);
			avlNode5.Left = avlNode;
			goto IL_0094;
			Block_6:
			SmallDictionary<K, V>.AvlNode avlNode6 = avlNode;
			avlNode = new SmallDictionary<K, V>.AvlNode(hashCode, key, value);
			avlNode6.Right = avlNode;
			goto IL_0094;
			IL_0087:
			this.HandleInsert(avlNode, avlNode2, key, value, add);
			return;
			IL_0094:
			SmallDictionary<K, V>.AvlNode avlNode7 = avlNode3;
			do
			{
				if (avlNode7.HashCode < hashCode)
				{
					SmallDictionary<K, V>.AvlNode avlNode8 = avlNode7;
					avlNode8.Balance -= 1;
					avlNode7 = avlNode7.Right;
				}
				else
				{
					SmallDictionary<K, V>.AvlNode avlNode9 = avlNode7;
					avlNode9.Balance += 1;
					avlNode7 = avlNode7.Left;
				}
			}
			while (avlNode7 != avlNode);
			sbyte balance = avlNode3.Balance;
			SmallDictionary<K, V>.AvlNode avlNode10;
			if (balance == -2)
			{
				avlNode10 = ((avlNode3.Right.Balance < 0) ? SmallDictionary<K, V>.LeftSimple(avlNode3) : SmallDictionary<K, V>.LeftComplex(avlNode3));
			}
			else
			{
				if (balance != 2)
				{
					return;
				}
				avlNode10 = ((avlNode3.Left.Balance > 0) ? SmallDictionary<K, V>.RightSimple(avlNode3) : SmallDictionary<K, V>.RightComplex(avlNode3));
			}
			if (avlNode4 == null)
			{
				this._root = avlNode10;
				return;
			}
			if (avlNode3 == avlNode4.Left)
			{
				avlNode4.Left = avlNode10;
				return;
			}
			avlNode4.Right = avlNode10;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001D1FC File Offset: 0x0001B3FC
		[return: Nullable(new byte[] { 1, 0, 0 })]
		private static SmallDictionary<K, V>.AvlNode LeftSimple([Nullable(new byte[] { 1, 0, 0 })] SmallDictionary<K, V>.AvlNode unbalanced)
		{
			SmallDictionary<K, V>.AvlNode right = unbalanced.Right;
			unbalanced.Right = right.Left;
			right.Left = unbalanced;
			unbalanced.Balance = 0;
			right.Balance = 0;
			return right;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001D234 File Offset: 0x0001B434
		[return: Nullable(new byte[] { 1, 0, 0 })]
		private static SmallDictionary<K, V>.AvlNode RightSimple([Nullable(new byte[] { 1, 0, 0 })] SmallDictionary<K, V>.AvlNode unbalanced)
		{
			SmallDictionary<K, V>.AvlNode left = unbalanced.Left;
			unbalanced.Left = left.Right;
			left.Right = unbalanced;
			unbalanced.Balance = 0;
			left.Balance = 0;
			return left;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001D26C File Offset: 0x0001B46C
		[return: Nullable(new byte[] { 1, 0, 0 })]
		private static SmallDictionary<K, V>.AvlNode LeftComplex([Nullable(new byte[] { 1, 0, 0 })] SmallDictionary<K, V>.AvlNode unbalanced)
		{
			SmallDictionary<K, V>.AvlNode right = unbalanced.Right;
			SmallDictionary<K, V>.AvlNode left = right.Left;
			right.Left = left.Right;
			left.Right = right;
			unbalanced.Right = left.Left;
			left.Left = unbalanced;
			sbyte balance = left.Balance;
			left.Balance = 0;
			if (balance < 0)
			{
				right.Balance = 0;
				unbalanced.Balance = 1;
			}
			else
			{
				right.Balance = -balance;
				unbalanced.Balance = 0;
			}
			return left;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001D2E0 File Offset: 0x0001B4E0
		[return: Nullable(new byte[] { 1, 0, 0 })]
		private static SmallDictionary<K, V>.AvlNode RightComplex([Nullable(new byte[] { 1, 0, 0 })] SmallDictionary<K, V>.AvlNode unbalanced)
		{
			SmallDictionary<K, V>.AvlNode left = unbalanced.Left;
			SmallDictionary<K, V>.AvlNode right = left.Right;
			left.Right = right.Left;
			right.Left = left;
			unbalanced.Left = right.Right;
			right.Right = unbalanced;
			sbyte balance = right.Balance;
			right.Balance = 0;
			if (balance < 0)
			{
				left.Balance = 1;
				unbalanced.Balance = 0;
			}
			else
			{
				left.Balance = 0;
				unbalanced.Balance = -balance;
			}
			return right;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001D354 File Offset: 0x0001B554
		private void HandleInsert([Nullable(new byte[] { 1, 0, 0 })] SmallDictionary<K, V>.AvlNode node, [Nullable(new byte[] { 2, 0, 0 })] SmallDictionary<K, V>.AvlNode parent, K key, V value, bool add)
		{
			SmallDictionary<K, V>.Node node2 = node;
			while (!this.CompareKeys(node2.Key, key))
			{
				node2 = node2.Next;
				if (node2 == null)
				{
					this.AddNode(node, parent, key, value);
					return;
				}
			}
			if (add)
			{
				throw new InvalidOperationException();
			}
			node2.Value = value;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001D39C File Offset: 0x0001B59C
		private void AddNode([Nullable(new byte[] { 1, 0, 0 })] SmallDictionary<K, V>.AvlNode node, [Nullable(new byte[] { 2, 0, 0 })] SmallDictionary<K, V>.AvlNode parent, K key, V value)
		{
			SmallDictionary<K, V>.AvlNodeHead avlNodeHead = node as SmallDictionary<K, V>.AvlNodeHead;
			if (avlNodeHead != null)
			{
				SmallDictionary<K, V>.NodeLinked nodeLinked = new SmallDictionary<K, V>.NodeLinked(key, value, avlNodeHead.next);
				avlNodeHead.next = nodeLinked;
				return;
			}
			SmallDictionary<K, V>.AvlNodeHead avlNodeHead2 = new SmallDictionary<K, V>.AvlNodeHead(node.HashCode, key, value, node);
			avlNodeHead2.Balance = node.Balance;
			avlNodeHead2.Left = node.Left;
			avlNodeHead2.Right = node.Right;
			if (parent == null)
			{
				this._root = avlNodeHead2;
				return;
			}
			if (node == parent.Left)
			{
				parent.Left = avlNodeHead2;
				return;
			}
			parent.Right = avlNodeHead2;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0001D421 File Offset: 0x0001B621
		[Nullable(0)]
		public SmallDictionary<K, V>.KeyCollection Keys
		{
			[NullableContext(0)]
			get
			{
				return new SmallDictionary<K, V>.KeyCollection(this);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0001D429 File Offset: 0x0001B629
		[Nullable(0)]
		public SmallDictionary<K, V>.ValueCollection Values
		{
			[NullableContext(0)]
			get
			{
				return new SmallDictionary<K, V>.ValueCollection(this);
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001D431 File Offset: 0x0001B631
		[NullableContext(0)]
		public SmallDictionary<K, V>.Enumerator GetEnumerator()
		{
			return new SmallDictionary<K, V>.Enumerator(this);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001D439 File Offset: 0x0001B639
		[return: Nullable(new byte[] { 1, 0, 1, 1 })]
		IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator()
		{
			return new SmallDictionary<K, V>.EnumerableImpl(this.GetEnumerator());
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001D446 File Offset: 0x0001B646
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001D450 File Offset: 0x0001B650
		private int HeightApprox()
		{
			int num = 0;
			for (SmallDictionary<K, V>.AvlNode avlNode = this._root; avlNode != null; avlNode = avlNode.Left)
			{
				num++;
			}
			return num + num / 2;
		}

		// Token: 0x040002CA RID: 714
		[Nullable(new byte[] { 2, 0, 0 })]
		private SmallDictionary<K, V>.AvlNode _root;

		// Token: 0x040002CB RID: 715
		public readonly IEqualityComparer<K> Comparer;

		// Token: 0x040002CC RID: 716
		public static readonly SmallDictionary<K, V> Empty = new SmallDictionary<K, V>(null);

		// Token: 0x0200054C RID: 1356
		[Nullable(0)]
		private abstract class Node
		{
			// Token: 0x060033DE RID: 13278 RVA: 0x00092B64 File Offset: 0x00090D64
			protected Node(K key, V value)
			{
				this.Key = key;
				this.Value = value;
			}

			// Token: 0x17000ECC RID: 3788
			// (get) Token: 0x060033DF RID: 13279 RVA: 0x00092B7A File Offset: 0x00090D7A
			[Nullable(new byte[] { 2, 0, 0 })]
			public virtual SmallDictionary<K, V>.Node Next
			{
				[return: Nullable(new byte[] { 2, 0, 0 })]
				get
				{
					return null;
				}
			}

			// Token: 0x04001516 RID: 5398
			public readonly K Key;

			// Token: 0x04001517 RID: 5399
			public V Value;
		}

		// Token: 0x0200054D RID: 1357
		[NullableContext(0)]
		private sealed class NodeLinked : SmallDictionary<K, V>.Node
		{
			// Token: 0x060033E0 RID: 13280 RVA: 0x00092B7D File Offset: 0x00090D7D
			[NullableContext(1)]
			public NodeLinked(K key, V value, [Nullable(new byte[] { 1, 0, 0 })] SmallDictionary<K, V>.Node next)
				: base(key, value)
			{
				this.Next = next;
			}

			// Token: 0x17000ECD RID: 3789
			// (get) Token: 0x060033E1 RID: 13281 RVA: 0x00092B8E File Offset: 0x00090D8E
			[Nullable(new byte[] { 1, 0, 0 })]
			public override SmallDictionary<K, V>.Node Next
			{
				[return: Nullable(new byte[] { 1, 0, 0 })]
				get;
			}
		}

		// Token: 0x0200054E RID: 1358
		[NullableContext(0)]
		private sealed class AvlNodeHead : SmallDictionary<K, V>.AvlNode
		{
			// Token: 0x060033E2 RID: 13282 RVA: 0x00092B96 File Offset: 0x00090D96
			[NullableContext(1)]
			public AvlNodeHead(int hashCode, K key, V value, [Nullable(new byte[] { 1, 0, 0 })] SmallDictionary<K, V>.Node next)
				: base(hashCode, key, value)
			{
				this.next = next;
			}

			// Token: 0x17000ECE RID: 3790
			// (get) Token: 0x060033E3 RID: 13283 RVA: 0x00092BA9 File Offset: 0x00090DA9
			[Nullable(new byte[] { 1, 0, 0 })]
			public override SmallDictionary<K, V>.Node Next
			{
				[return: Nullable(new byte[] { 1, 0, 0 })]
				get
				{
					return this.next;
				}
			}

			// Token: 0x04001519 RID: 5401
			[Nullable(new byte[] { 1, 0, 0 })]
			public SmallDictionary<K, V>.Node next;
		}

		// Token: 0x0200054F RID: 1359
		[NullableContext(0)]
		private abstract class HashedNode : SmallDictionary<K, V>.Node
		{
			// Token: 0x060033E4 RID: 13284 RVA: 0x00092BB1 File Offset: 0x00090DB1
			[NullableContext(1)]
			protected HashedNode(int hashCode, K key, V value)
				: base(key, value)
			{
				this.HashCode = hashCode;
			}

			// Token: 0x0400151A RID: 5402
			public readonly int HashCode;

			// Token: 0x0400151B RID: 5403
			public sbyte Balance;
		}

		// Token: 0x02000550 RID: 1360
		[NullableContext(0)]
		private class AvlNode : SmallDictionary<K, V>.HashedNode
		{
			// Token: 0x060033E5 RID: 13285 RVA: 0x00092BC2 File Offset: 0x00090DC2
			[NullableContext(1)]
			public AvlNode(int hashCode, K key, V value)
				: base(hashCode, key, value)
			{
			}

			// Token: 0x0400151C RID: 5404
			[Nullable(new byte[] { 2, 0, 0 })]
			public SmallDictionary<K, V>.AvlNode Left;

			// Token: 0x0400151D RID: 5405
			[Nullable(new byte[] { 2, 0, 0 })]
			public SmallDictionary<K, V>.AvlNode Right;
		}

		// Token: 0x02000551 RID: 1361
		[Nullable(0)]
		internal struct KeyCollection : IEnumerable<K>, IEnumerable
		{
			// Token: 0x060033E6 RID: 13286 RVA: 0x00092BCD File Offset: 0x00090DCD
			public KeyCollection(SmallDictionary<K, V> dict)
			{
				this._dict = dict;
			}

			// Token: 0x060033E7 RID: 13287 RVA: 0x00092BD6 File Offset: 0x00090DD6
			[NullableContext(0)]
			public SmallDictionary<K, V>.KeyCollection.Enumerator GetEnumerator()
			{
				return new SmallDictionary<K, V>.KeyCollection.Enumerator(this._dict);
			}

			// Token: 0x060033E8 RID: 13288 RVA: 0x00092BE3 File Offset: 0x00090DE3
			IEnumerator<K> IEnumerable<K>.GetEnumerator()
			{
				return new SmallDictionary<K, V>.KeyCollection.EnumerableImpl(this.GetEnumerator());
			}

			// Token: 0x060033E9 RID: 13289 RVA: 0x00092BF0 File Offset: 0x00090DF0
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400151E RID: 5406
			private readonly SmallDictionary<K, V> _dict;

			// Token: 0x020007AD RID: 1965
			[Nullable(0)]
			public struct Enumerator
			{
				// Token: 0x06003F89 RID: 16265 RVA: 0x000B1BC4 File Offset: 0x000AFDC4
				public Enumerator(SmallDictionary<K, V> dict)
				{
					this = default(SmallDictionary<K, V>.KeyCollection.Enumerator);
					SmallDictionary<K, V>.AvlNode root = dict._root;
					if (root != null)
					{
						if (root.Left == root.Right)
						{
							this._next = root;
							return;
						}
						this._stack = new Stack<SmallDictionary<K, V>.AvlNode>(dict.HeightApprox());
						this._stack.Push(root);
					}
				}

				// Token: 0x1700117D RID: 4477
				// (get) Token: 0x06003F8A RID: 16266 RVA: 0x000B1C15 File Offset: 0x000AFE15
				public K Current
				{
					get
					{
						return this._current.Key;
					}
				}

				// Token: 0x06003F8B RID: 16267 RVA: 0x000B1C24 File Offset: 0x000AFE24
				public bool MoveNext()
				{
					if (this._next != null)
					{
						this._current = this._next;
						this._next = this._next.Next;
						return true;
					}
					if (this._stack == null || this._stack.Count == 0)
					{
						return false;
					}
					SmallDictionary<K, V>.AvlNode avlNode = this._stack.Pop();
					this._current = avlNode;
					this._next = avlNode.Next;
					this.PushIfNotNull(avlNode.Left);
					this.PushIfNotNull(avlNode.Right);
					return true;
				}

				// Token: 0x06003F8C RID: 16268 RVA: 0x000B1CA7 File Offset: 0x000AFEA7
				private void PushIfNotNull([Nullable(new byte[] { 2, 0, 0 })] SmallDictionary<K, V>.AvlNode child)
				{
					if (child != null)
					{
						this._stack.Push(child);
					}
				}

				// Token: 0x04001DAA RID: 7594
				[Nullable(new byte[] { 2, 1, 0, 0 })]
				private readonly Stack<SmallDictionary<K, V>.AvlNode> _stack;

				// Token: 0x04001DAB RID: 7595
				[Nullable(new byte[] { 2, 0, 0 })]
				private SmallDictionary<K, V>.Node _next;

				// Token: 0x04001DAC RID: 7596
				[Nullable(new byte[] { 2, 0, 0 })]
				private SmallDictionary<K, V>.Node _current;
			}

			// Token: 0x020007AE RID: 1966
			[NullableContext(0)]
			public class EnumerableImpl : IEnumerator<K>, IEnumerator, IDisposable
			{
				// Token: 0x06003F8D RID: 16269 RVA: 0x000B1CB8 File Offset: 0x000AFEB8
				public EnumerableImpl(SmallDictionary<K, V>.KeyCollection.Enumerator e)
				{
					this._e = e;
				}

				// Token: 0x1700117E RID: 4478
				// (get) Token: 0x06003F8E RID: 16270 RVA: 0x000B1CC7 File Offset: 0x000AFEC7
				[Nullable(1)]
				K IEnumerator<K>.Current
				{
					[NullableContext(1)]
					get
					{
						return this._e.Current;
					}
				}

				// Token: 0x06003F8F RID: 16271 RVA: 0x000B1CD4 File Offset: 0x000AFED4
				void IDisposable.Dispose()
				{
				}

				// Token: 0x1700117F RID: 4479
				// (get) Token: 0x06003F90 RID: 16272 RVA: 0x000B1CD6 File Offset: 0x000AFED6
				[Nullable(1)]
				object IEnumerator.Current
				{
					[NullableContext(1)]
					get
					{
						return this._e.Current;
					}
				}

				// Token: 0x06003F91 RID: 16273 RVA: 0x000B1CE8 File Offset: 0x000AFEE8
				bool IEnumerator.MoveNext()
				{
					return this._e.MoveNext();
				}

				// Token: 0x06003F92 RID: 16274 RVA: 0x000B1CF5 File Offset: 0x000AFEF5
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x04001DAD RID: 7597
				private SmallDictionary<K, V>.KeyCollection.Enumerator _e;
			}
		}

		// Token: 0x02000552 RID: 1362
		[Nullable(0)]
		internal struct ValueCollection : IEnumerable<V>, IEnumerable
		{
			// Token: 0x060033EA RID: 13290 RVA: 0x00092BF7 File Offset: 0x00090DF7
			public ValueCollection(SmallDictionary<K, V> dict)
			{
				this._dict = dict;
			}

			// Token: 0x060033EB RID: 13291 RVA: 0x00092C00 File Offset: 0x00090E00
			[NullableContext(0)]
			public SmallDictionary<K, V>.ValueCollection.Enumerator GetEnumerator()
			{
				return new SmallDictionary<K, V>.ValueCollection.Enumerator(this._dict);
			}

			// Token: 0x060033EC RID: 13292 RVA: 0x00092C0D File Offset: 0x00090E0D
			IEnumerator<V> IEnumerable<V>.GetEnumerator()
			{
				return new SmallDictionary<K, V>.ValueCollection.EnumerableImpl(this.GetEnumerator());
			}

			// Token: 0x060033ED RID: 13293 RVA: 0x00092C1A File Offset: 0x00090E1A
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400151F RID: 5407
			private readonly SmallDictionary<K, V> _dict;

			// Token: 0x020007AF RID: 1967
			[Nullable(0)]
			public struct Enumerator
			{
				// Token: 0x06003F93 RID: 16275 RVA: 0x000B1CFC File Offset: 0x000AFEFC
				public Enumerator(SmallDictionary<K, V> dict)
				{
					this = default(SmallDictionary<K, V>.ValueCollection.Enumerator);
					SmallDictionary<K, V>.AvlNode root = dict._root;
					if (root == null)
					{
						return;
					}
					if (root.Left == root.Right)
					{
						this._next = root;
						return;
					}
					this._stack = new Stack<SmallDictionary<K, V>.AvlNode>(dict.HeightApprox());
					this._stack.Push(root);
				}

				// Token: 0x17001180 RID: 4480
				// (get) Token: 0x06003F94 RID: 16276 RVA: 0x000B1D4E File Offset: 0x000AFF4E
				public V Current
				{
					get
					{
						return this._current.Value;
					}
				}

				// Token: 0x06003F95 RID: 16277 RVA: 0x000B1D5C File Offset: 0x000AFF5C
				public bool MoveNext()
				{
					if (this._next != null)
					{
						this._current = this._next;
						this._next = this._next.Next;
						return true;
					}
					if (this._stack == null || this._stack.Count == 0)
					{
						return false;
					}
					SmallDictionary<K, V>.AvlNode avlNode = this._stack.Pop();
					this._current = avlNode;
					this._next = avlNode.Next;
					this.PushIfNotNull(avlNode.Left);
					this.PushIfNotNull(avlNode.Right);
					return true;
				}

				// Token: 0x06003F96 RID: 16278 RVA: 0x000B1DDF File Offset: 0x000AFFDF
				private void PushIfNotNull([Nullable(new byte[] { 2, 0, 0 })] SmallDictionary<K, V>.AvlNode child)
				{
					if (child != null)
					{
						this._stack.Push(child);
					}
				}

				// Token: 0x04001DAE RID: 7598
				[Nullable(new byte[] { 2, 1, 0, 0 })]
				private readonly Stack<SmallDictionary<K, V>.AvlNode> _stack;

				// Token: 0x04001DAF RID: 7599
				[Nullable(new byte[] { 2, 0, 0 })]
				private SmallDictionary<K, V>.Node _next;

				// Token: 0x04001DB0 RID: 7600
				[Nullable(new byte[] { 2, 0, 0 })]
				private SmallDictionary<K, V>.Node _current;
			}

			// Token: 0x020007B0 RID: 1968
			[NullableContext(0)]
			public class EnumerableImpl : IEnumerator<V>, IEnumerator, IDisposable
			{
				// Token: 0x06003F97 RID: 16279 RVA: 0x000B1DF0 File Offset: 0x000AFFF0
				public EnumerableImpl(SmallDictionary<K, V>.ValueCollection.Enumerator e)
				{
					this._e = e;
				}

				// Token: 0x17001181 RID: 4481
				// (get) Token: 0x06003F98 RID: 16280 RVA: 0x000B1DFF File Offset: 0x000AFFFF
				[Nullable(1)]
				V IEnumerator<V>.Current
				{
					[NullableContext(1)]
					get
					{
						return this._e.Current;
					}
				}

				// Token: 0x06003F99 RID: 16281 RVA: 0x000B1E0C File Offset: 0x000B000C
				void IDisposable.Dispose()
				{
				}

				// Token: 0x17001182 RID: 4482
				// (get) Token: 0x06003F9A RID: 16282 RVA: 0x000B1E0E File Offset: 0x000B000E
				[Nullable(2)]
				object IEnumerator.Current
				{
					[NullableContext(2)]
					get
					{
						return this._e.Current;
					}
				}

				// Token: 0x06003F9B RID: 16283 RVA: 0x000B1E20 File Offset: 0x000B0020
				bool IEnumerator.MoveNext()
				{
					return this._e.MoveNext();
				}

				// Token: 0x06003F9C RID: 16284 RVA: 0x000B1E2D File Offset: 0x000B002D
				void IEnumerator.Reset()
				{
					throw new NotImplementedException();
				}

				// Token: 0x04001DB1 RID: 7601
				private SmallDictionary<K, V>.ValueCollection.Enumerator _e;
			}
		}

		// Token: 0x02000553 RID: 1363
		[NullableContext(0)]
		public struct Enumerator
		{
			// Token: 0x060033EE RID: 13294 RVA: 0x00092C24 File Offset: 0x00090E24
			[NullableContext(1)]
			public Enumerator(SmallDictionary<K, V> dict)
			{
				this = default(SmallDictionary<K, V>.Enumerator);
				SmallDictionary<K, V>.AvlNode root = dict._root;
				if (root == null)
				{
					return;
				}
				if (root.Left == root.Right)
				{
					this._next = root;
					return;
				}
				this._stack = new Stack<SmallDictionary<K, V>.AvlNode>(dict.HeightApprox());
				this._stack.Push(root);
			}

			// Token: 0x17000ECF RID: 3791
			// (get) Token: 0x060033EF RID: 13295 RVA: 0x00092C76 File Offset: 0x00090E76
			[Nullable(new byte[] { 0, 1, 1 })]
			public KeyValuePair<K, V> Current
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					return new KeyValuePair<K, V>(this._current.Key, this._current.Value);
				}
			}

			// Token: 0x060033F0 RID: 13296 RVA: 0x00092C94 File Offset: 0x00090E94
			public bool MoveNext()
			{
				if (this._next != null)
				{
					this._current = this._next;
					this._next = this._next.Next;
					return true;
				}
				if (this._stack == null || this._stack.Count == 0)
				{
					return false;
				}
				SmallDictionary<K, V>.AvlNode avlNode = this._stack.Pop();
				this._current = avlNode;
				this._next = avlNode.Next;
				this.PushIfNotNull(avlNode.Left);
				this.PushIfNotNull(avlNode.Right);
				return true;
			}

			// Token: 0x060033F1 RID: 13297 RVA: 0x00092D17 File Offset: 0x00090F17
			private void PushIfNotNull([Nullable(new byte[] { 2, 0, 0 })] SmallDictionary<K, V>.AvlNode child)
			{
				if (child != null)
				{
					this._stack.Push(child);
				}
			}

			// Token: 0x04001520 RID: 5408
			[Nullable(new byte[] { 2, 1, 0, 0 })]
			private readonly Stack<SmallDictionary<K, V>.AvlNode> _stack;

			// Token: 0x04001521 RID: 5409
			[Nullable(new byte[] { 2, 0, 0 })]
			private SmallDictionary<K, V>.Node _next;

			// Token: 0x04001522 RID: 5410
			[Nullable(new byte[] { 2, 0, 0 })]
			private SmallDictionary<K, V>.Node _current;
		}

		// Token: 0x02000554 RID: 1364
		[NullableContext(0)]
		public class EnumerableImpl : IEnumerator<KeyValuePair<K, V>>, IEnumerator, IDisposable
		{
			// Token: 0x060033F2 RID: 13298 RVA: 0x00092D28 File Offset: 0x00090F28
			public EnumerableImpl(SmallDictionary<K, V>.Enumerator e)
			{
				this._e = e;
			}

			// Token: 0x17000ED0 RID: 3792
			// (get) Token: 0x060033F3 RID: 13299 RVA: 0x00092D37 File Offset: 0x00090F37
			[Nullable(new byte[] { 0, 1, 1 })]
			KeyValuePair<K, V> IEnumerator<KeyValuePair<K, V>>.Current
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					return this._e.Current;
				}
			}

			// Token: 0x060033F4 RID: 13300 RVA: 0x00092D44 File Offset: 0x00090F44
			void IDisposable.Dispose()
			{
			}

			// Token: 0x17000ED1 RID: 3793
			// (get) Token: 0x060033F5 RID: 13301 RVA: 0x00092D46 File Offset: 0x00090F46
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					return this._e.Current;
				}
			}

			// Token: 0x060033F6 RID: 13302 RVA: 0x00092D58 File Offset: 0x00090F58
			bool IEnumerator.MoveNext()
			{
				return this._e.MoveNext();
			}

			// Token: 0x060033F7 RID: 13303 RVA: 0x00092D65 File Offset: 0x00090F65
			void IEnumerator.Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x04001523 RID: 5411
			private SmallDictionary<K, V>.Enumerator _e;
		}
	}
}
