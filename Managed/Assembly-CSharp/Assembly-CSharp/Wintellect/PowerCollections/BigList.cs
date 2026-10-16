using System;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002F3 RID: 755
	[Serializable]
	public class BigList<T> : ListBase<T>, ICloneable
	{
		// Token: 0x06001A5D RID: 6749 RVA: 0x0008545D File Offset: 0x0008365D
		private void StopEnumerations()
		{
			this.changeStamp++;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0008546D File Offset: 0x0008366D
		private void CheckEnumerationStamp(int startStamp)
		{
			if (startStamp != this.changeStamp)
			{
				throw new InvalidOperationException(Strings.ChangeDuringEnumeration);
			}
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00085483 File Offset: 0x00083683
		public BigList()
		{
			this.root = null;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00085492 File Offset: 0x00083692
		public BigList(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.root = this.NodeFromEnumerable(collection);
			this.CheckBalance();
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000854BB File Offset: 0x000836BB
		public BigList(IEnumerable<T> collection, int copies)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.root = this.NCopiesOfNode(copies, this.NodeFromEnumerable(collection));
			this.CheckBalance();
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000854EB File Offset: 0x000836EB
		public BigList(BigList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.root == null)
			{
				this.root = null;
				return;
			}
			list.root.MarkShared();
			this.root = list.root;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00085528 File Offset: 0x00083728
		public BigList(BigList<T> list, int copies)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.root == null)
			{
				this.root = null;
				return;
			}
			list.root.MarkShared();
			this.root = this.NCopiesOfNode(copies, list.root);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00085577 File Offset: 0x00083777
		private BigList(BigList<T>.Node node)
		{
			this.root = node;
			this.CheckBalance();
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0008558C File Offset: 0x0008378C
		public sealed override int Count
		{
			get
			{
				if (this.root == null)
				{
					return 0;
				}
				return this.root.Count;
			}
		}

		// Token: 0x17000389 RID: 905
		public sealed override T this[int index]
		{
			get
			{
				if (this.root == null || index < 0 || index >= this.root.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				BigList<T>.Node node = this.root;
				for (BigList<T>.ConcatNode concatNode = node as BigList<T>.ConcatNode; concatNode != null; concatNode = node as BigList<T>.ConcatNode)
				{
					int count = concatNode.left.Count;
					if (index < count)
					{
						node = concatNode.left;
					}
					else
					{
						node = concatNode.right;
						index -= count;
					}
				}
				return ((BigList<T>.LeafNode)node).items[index];
			}
			set
			{
				if (this.root == null || index < 0 || index >= this.root.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.StopEnumerations();
				if (this.root.Shared)
				{
					this.root = this.root.SetAt(index, value);
				}
				BigList<T>.Node node = this.root;
				for (BigList<T>.ConcatNode concatNode = node as BigList<T>.ConcatNode; concatNode != null; concatNode = node as BigList<T>.ConcatNode)
				{
					int count = concatNode.left.Count;
					if (index < count)
					{
						node = concatNode.left;
						if (node.Shared)
						{
							concatNode.left = node.SetAt(index, value);
							return;
						}
					}
					else
					{
						node = concatNode.right;
						index -= count;
						if (node.Shared)
						{
							concatNode.right = node.SetAt(index, value);
							return;
						}
					}
				}
				((BigList<T>.LeafNode)node).items[index] = value;
			}
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000856FD File Offset: 0x000838FD
		public sealed override void Clear()
		{
			this.StopEnumerations();
			this.root = null;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0008570C File Offset: 0x0008390C
		public sealed override void Insert(int index, T item)
		{
			this.StopEnumerations();
			if (this.Count + 1 > 2147483646)
			{
				throw new InvalidOperationException(Strings.CollectionTooLarge);
			}
			if (index <= 0 || index >= this.Count)
			{
				if (index == 0)
				{
					this.AddToFront(item);
					return;
				}
				if (index == this.Count)
				{
					this.Add(item);
					return;
				}
				throw new ArgumentOutOfRangeException("index");
			}
			else
			{
				if (this.root == null)
				{
					this.root = new BigList<T>.LeafNode(item);
					return;
				}
				BigList<T>.Node node = this.root.InsertInPlace(index, item);
				if (node != this.root)
				{
					this.root = node;
					this.CheckBalance();
				}
				return;
			}
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x000857A8 File Offset: 0x000839A8
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			this.StopEnumerations();
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (index <= 0 || index >= this.Count)
			{
				if (index == 0)
				{
					this.AddRangeToFront(collection);
					return;
				}
				if (index == this.Count)
				{
					this.AddRange(collection);
					return;
				}
				throw new ArgumentOutOfRangeException("index");
			}
			else
			{
				BigList<T>.Node node = this.NodeFromEnumerable(collection);
				if (node == null)
				{
					return;
				}
				if (this.root == null)
				{
					this.root = node;
					return;
				}
				if (this.Count + node.Count > 2147483646)
				{
					throw new InvalidOperationException(Strings.CollectionTooLarge);
				}
				BigList<T>.Node node2 = this.root.InsertInPlace(index, node, true);
				if (node2 != this.root)
				{
					this.root = node2;
					this.CheckBalance();
				}
				return;
			}
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00085860 File Offset: 0x00083A60
		public void InsertRange(int index, BigList<T> list)
		{
			this.StopEnumerations();
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (this.Count + list.Count > 2147483646)
			{
				throw new InvalidOperationException(Strings.CollectionTooLarge);
			}
			if (index <= 0 || index >= this.Count)
			{
				if (index == 0)
				{
					this.AddRangeToFront(list);
					return;
				}
				if (index == this.Count)
				{
					this.AddRange(list);
					return;
				}
				throw new ArgumentOutOfRangeException("index");
			}
			else
			{
				if (list.Count == 0)
				{
					return;
				}
				if (this.root == null)
				{
					list.root.MarkShared();
					this.root = list.root;
					return;
				}
				if (list.root == this.root)
				{
					this.root.MarkShared();
				}
				BigList<T>.Node node = this.root.InsertInPlace(index, list.root, false);
				if (node != this.root)
				{
					this.root = node;
					this.CheckBalance();
				}
				return;
			}
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00085940 File Offset: 0x00083B40
		public sealed override void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0008594C File Offset: 0x00083B4C
		public void RemoveRange(int index, int count)
		{
			if (count == 0)
			{
				return;
			}
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || count > this.Count - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.StopEnumerations();
			BigList<T>.Node node = this.root.RemoveRangeInPlace(index, index + count - 1);
			if (node != this.root)
			{
				this.root = node;
				this.CheckBalance();
			}
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000859C0 File Offset: 0x00083BC0
		public sealed override void Add(T item)
		{
			if (this.Count + 1 > 2147483646)
			{
				throw new InvalidOperationException(Strings.CollectionTooLarge);
			}
			this.StopEnumerations();
			if (this.root == null)
			{
				this.root = new BigList<T>.LeafNode(item);
				return;
			}
			BigList<T>.Node node = this.root.AppendInPlace(item);
			if (node != this.root)
			{
				this.root = node;
				this.CheckBalance();
			}
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00085A28 File Offset: 0x00083C28
		public void AddToFront(T item)
		{
			if (this.Count + 1 > 2147483646)
			{
				throw new InvalidOperationException(Strings.CollectionTooLarge);
			}
			this.StopEnumerations();
			if (this.root == null)
			{
				this.root = new BigList<T>.LeafNode(item);
				return;
			}
			BigList<T>.Node node = this.root.PrependInPlace(item);
			if (node != this.root)
			{
				this.root = node;
				this.CheckBalance();
			}
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00085A90 File Offset: 0x00083C90
		public void AddRange(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.StopEnumerations();
			BigList<T>.Node node = this.NodeFromEnumerable(collection);
			if (node == null)
			{
				return;
			}
			if (this.root == null)
			{
				this.root = node;
				this.CheckBalance();
				return;
			}
			if (this.Count + node.count > 2147483646)
			{
				throw new InvalidOperationException(Strings.CollectionTooLarge);
			}
			BigList<T>.Node node2 = this.root.AppendInPlace(node, true);
			if (node2 != this.root)
			{
				this.root = node2;
				this.CheckBalance();
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00085B18 File Offset: 0x00083D18
		public void AddRangeToFront(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.StopEnumerations();
			BigList<T>.Node node = this.NodeFromEnumerable(collection);
			if (node == null)
			{
				return;
			}
			if (this.root == null)
			{
				this.root = node;
				this.CheckBalance();
				return;
			}
			if (this.Count + node.Count > 2147483646)
			{
				throw new InvalidOperationException(Strings.CollectionTooLarge);
			}
			BigList<T>.Node node2 = this.root.PrependInPlace(node, true);
			if (node2 != this.root)
			{
				this.root = node2;
				this.CheckBalance();
			}
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00085B9E File Offset: 0x00083D9E
		public BigList<T> Clone()
		{
			if (this.root == null)
			{
				return new BigList<T>();
			}
			this.root.MarkShared();
			return new BigList<T>(this.root);
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00085BC4 File Offset: 0x00083DC4
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00085BCC File Offset: 0x00083DCC
		public BigList<T> CloneContents()
		{
			if (this.root == null)
			{
				return new BigList<T>();
			}
			bool flag;
			if (!Util.IsCloneableType(typeof(T), out flag))
			{
				throw new InvalidOperationException(string.Format(Strings.TypeNotCloneable, typeof(T).FullName));
			}
			if (flag)
			{
				return this.Clone();
			}
			return new BigList<T>(Algorithms.Convert<T, T>(this, delegate(T item)
			{
				if (item == null)
				{
					return default(T);
				}
				return (T)((object)((ICloneable)((object)item)).Clone());
			}));
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00085C50 File Offset: 0x00083E50
		public void AddRange(BigList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (this.Count + list.Count > 2147483646)
			{
				throw new InvalidOperationException(Strings.CollectionTooLarge);
			}
			if (list.Count == 0)
			{
				return;
			}
			this.StopEnumerations();
			if (this.root == null)
			{
				list.root.MarkShared();
				this.root = list.root;
				return;
			}
			BigList<T>.Node node = this.root.AppendInPlace(list.root, false);
			if (node != this.root)
			{
				this.root = node;
				this.CheckBalance();
			}
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00085CE4 File Offset: 0x00083EE4
		public void AddRangeToFront(BigList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (this.Count + list.Count > 2147483646)
			{
				throw new InvalidOperationException(Strings.CollectionTooLarge);
			}
			if (list.Count == 0)
			{
				return;
			}
			this.StopEnumerations();
			if (this.root == null)
			{
				list.root.MarkShared();
				this.root = list.root;
				return;
			}
			BigList<T>.Node node = this.root.PrependInPlace(list.root, false);
			if (node != this.root)
			{
				this.root = node;
				this.CheckBalance();
			}
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00085D78 File Offset: 0x00083F78
		public static BigList<T>operator +(BigList<T> first, BigList<T> second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			if (first.Count + second.Count > 2147483646)
			{
				throw new InvalidOperationException(Strings.CollectionTooLarge);
			}
			if (first.Count == 0)
			{
				return second.Clone();
			}
			if (second.Count == 0)
			{
				return first.Clone();
			}
			BigList<T> bigList = new BigList<T>(first.root.Append(second.root, false));
			bigList.CheckBalance();
			return bigList;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00085DFC File Offset: 0x00083FFC
		public BigList<T> GetRange(int index, int count)
		{
			if (count == 0)
			{
				return new BigList<T>();
			}
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || count > this.Count - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new BigList<T>(this.root.Subrange(index, index + count - 1));
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00085E5C File Offset: 0x0008405C
		public sealed override IList<T> Range(int index, int count)
		{
			if (index < 0 || index > this.Count || (index == this.Count && count != 0))
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || count > this.Count || count + index > this.Count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new BigList<T>.BigListRange(this, index, count);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00085EB8 File Offset: 0x000840B8
		private IEnumerator<T> GetEnumerator(int start, int maxItems)
		{
			int startStamp = this.changeStamp;
			if (this.root != null && maxItems > 0)
			{
				BigList<T>.ConcatNode[] stack = new BigList<T>.ConcatNode[this.root.Depth];
				bool[] leftStack = new bool[this.root.Depth];
				int stackPtr = 0;
				int startIndex = 0;
				BigList<T>.Node node = this.root;
				if (start != 0)
				{
					if (start < 0 || start >= this.root.Count)
					{
						throw new ArgumentOutOfRangeException("start");
					}
					BigList<T>.ConcatNode concatNode = node as BigList<T>.ConcatNode;
					startIndex = start;
					while (concatNode != null)
					{
						stack[stackPtr] = concatNode;
						int count = concatNode.left.Count;
						if (startIndex < count)
						{
							leftStack[stackPtr] = true;
							node = concatNode.left;
						}
						else
						{
							leftStack[stackPtr] = false;
							node = concatNode.right;
							startIndex -= count;
						}
						int num = stackPtr + 1;
						stackPtr = num;
						concatNode = node as BigList<T>.ConcatNode;
					}
				}
				for (;;)
				{
					IL_017F:
					BigList<T>.ConcatNode concatNode;
					if ((concatNode = node as BigList<T>.ConcatNode) == null)
					{
						BigList<T>.LeafNode currentLeaf = (BigList<T>.LeafNode)node;
						int limit = currentLeaf.Count;
						if (limit > startIndex + maxItems)
						{
							limit = startIndex + maxItems;
						}
						int num;
						for (int i = startIndex; i < limit; i = num)
						{
							yield return currentLeaf.items[i];
							this.CheckEnumerationStamp(startStamp);
							num = i + 1;
						}
						maxItems -= limit - startIndex;
						if (maxItems <= 0)
						{
							break;
						}
						startIndex = 0;
						while (stackPtr != 0)
						{
							BigList<T>.ConcatNode[] array = stack;
							num = stackPtr - 1;
							stackPtr = num;
							BigList<T>.ConcatNode concatNode2 = array[num];
							if (leftStack[stackPtr])
							{
								leftStack[stackPtr] = false;
								num = stackPtr + 1;
								stackPtr = num;
								node = concatNode2.right;
								goto IL_017F;
							}
						}
						goto Block_11;
					}
					else
					{
						stack[stackPtr] = concatNode;
						leftStack[stackPtr] = true;
						int num = stackPtr + 1;
						stackPtr = num;
						node = concatNode.left;
					}
				}
				yield break;
				Block_11:
				yield break;
			}
			yield break;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00085ED5 File Offset: 0x000840D5
		public sealed override IEnumerator<T> GetEnumerator()
		{
			return this.GetEnumerator(0, int.MaxValue);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00085EE4 File Offset: 0x000840E4
		private BigList<T>.Node NodeFromEnumerable(IEnumerable<T> collection)
		{
			BigList<T>.Node node = null;
			IEnumerator<T> enumerator = collection.GetEnumerator();
			BigList<T>.LeafNode leafNode;
			while ((leafNode = this.LeafFromEnumerator(enumerator)) != null)
			{
				if (node == null)
				{
					node = leafNode;
				}
				else
				{
					if (node.count + leafNode.count > 2147483646)
					{
						throw new InvalidOperationException(Strings.CollectionTooLarge);
					}
					node = node.AppendInPlace(leafNode, true);
				}
			}
			return node;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00085F38 File Offset: 0x00084138
		private BigList<T>.LeafNode LeafFromEnumerator(IEnumerator<T> enumerator)
		{
			int num = 0;
			T[] array = null;
			while (num < 120 && enumerator.MoveNext())
			{
				if (num == 0)
				{
					array = new T[120];
				}
				array[num++] = enumerator.Current;
			}
			if (array != null)
			{
				return new BigList<T>.LeafNode(num, array);
			}
			return null;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00085F80 File Offset: 0x00084180
		private BigList<T>.Node NCopiesOfNode(int copies, BigList<T>.Node node)
		{
			if (copies < 0)
			{
				throw new ArgumentOutOfRangeException("copies", Strings.ArgMustNotBeNegative);
			}
			if (copies == 0 || node == null)
			{
				return null;
			}
			if (copies == 1)
			{
				return node;
			}
			if ((long)copies * (long)node.count > 2147483646L)
			{
				throw new InvalidOperationException(Strings.CollectionTooLarge);
			}
			int num = 1;
			BigList<T>.Node node2 = node;
			BigList<T>.Node node3 = null;
			while (copies > 0)
			{
				node2.MarkShared();
				if ((copies & num) != 0)
				{
					copies -= num;
					if (node3 == null)
					{
						node3 = node2;
					}
					else
					{
						node3 = node3.Append(node2, false);
					}
				}
				num *= 2;
				node2 = node2.Append(node2, false);
			}
			return node3;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00086008 File Offset: 0x00084208
		private void CheckBalance()
		{
			if (this.root != null && this.root.Depth > 6 && (this.root.Depth - 6 > 44 || this.Count < BigList<T>.FIBONACCI[this.root.Depth - 6]))
			{
				this.Rebalance();
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00086060 File Offset: 0x00084260
		internal void Rebalance()
		{
			if (this.root == null)
			{
				return;
			}
			if (this.root.Depth <= 1 || (this.root.Depth - 2 <= 44 && this.Count >= BigList<T>.FIBONACCI[this.root.Depth - 2]))
			{
				return;
			}
			int num = 0;
			while (num <= 44 && this.root.Count >= BigList<T>.FIBONACCI[num])
			{
				num++;
			}
			BigList<T>.Node[] array = new BigList<T>.Node[num];
			this.AddNodeToRebalanceArray(array, this.root, false);
			BigList<T>.Node node = null;
			for (int i = 0; i < num; i++)
			{
				BigList<T>.Node node2 = array[i];
				if (node2 != null)
				{
					if (node == null)
					{
						node = node2;
					}
					else
					{
						node = node.PrependInPlace(node2, !node2.Shared);
					}
				}
			}
			this.root = node;
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00086120 File Offset: 0x00084320
		private void AddNodeToRebalanceArray(BigList<T>.Node[] rebalanceArray, BigList<T>.Node node, bool shared)
		{
			if (node.Shared)
			{
				shared = true;
			}
			if (node.IsBalanced())
			{
				if (shared)
				{
					node.MarkShared();
				}
				this.AddBalancedNodeToRebalanceArray(rebalanceArray, node);
				return;
			}
			BigList<T>.ConcatNode concatNode = (BigList<T>.ConcatNode)node;
			this.AddNodeToRebalanceArray(rebalanceArray, concatNode.left, shared);
			this.AddNodeToRebalanceArray(rebalanceArray, concatNode.right, shared);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00086178 File Offset: 0x00084378
		private void AddBalancedNodeToRebalanceArray(BigList<T>.Node[] rebalanceArray, BigList<T>.Node balancedNode)
		{
			BigList<T>.Node node = null;
			int i = balancedNode.Count;
			int num = 0;
			while (i >= BigList<T>.FIBONACCI[num + 1])
			{
				BigList<T>.Node node2 = rebalanceArray[num];
				if (node2 != null)
				{
					rebalanceArray[num] = null;
					if (node == null)
					{
						node = node2;
					}
					else
					{
						node = node.PrependInPlace(node2, !node2.Shared);
					}
				}
				num++;
			}
			if (node != null)
			{
				balancedNode = balancedNode.PrependInPlace(node, !node.Shared);
			}
			for (;;)
			{
				BigList<T>.Node node3 = rebalanceArray[num];
				if (node3 != null)
				{
					rebalanceArray[num] = null;
					balancedNode = balancedNode.PrependInPlace(node3, !node3.Shared);
				}
				if (balancedNode.Count < BigList<T>.FIBONACCI[num + 1])
				{
					break;
				}
				num++;
			}
			rebalanceArray[num] = balancedNode;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00086216 File Offset: 0x00084416
		public new BigList<TDest> ConvertAll<TDest>(Converter<T, TDest> converter)
		{
			return new BigList<TDest>(Algorithms.Convert<T, TDest>(this, converter));
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00086224 File Offset: 0x00084424
		public void Reverse()
		{
			Algorithms.ReverseInPlace<T>(this);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0008622C File Offset: 0x0008442C
		public void Reverse(int start, int count)
		{
			Algorithms.ReverseInPlace<T>(this.Range(start, count));
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0008623B File Offset: 0x0008443B
		public void Sort()
		{
			this.Sort(Comparers.DefaultComparer<T>());
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00086248 File Offset: 0x00084448
		public void Sort(IComparer<T> comparer)
		{
			Algorithms.SortInPlace<T>(this, comparer);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00086251 File Offset: 0x00084451
		public void Sort(Comparison<T> comparison)
		{
			this.Sort(Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0008625F File Offset: 0x0008445F
		public int BinarySearch(T item)
		{
			return this.BinarySearch(item, Comparers.DefaultComparer<T>());
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00086270 File Offset: 0x00084470
		public int BinarySearch(T item, IComparer<T> comparer)
		{
			int num;
			if (Algorithms.BinarySearch<T>(this, item, comparer, out num) == 0)
			{
				return ~num;
			}
			return num;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0008628D File Offset: 0x0008448D
		public int BinarySearch(T item, Comparison<T> comparison)
		{
			return this.BinarySearch(item, Comparers.ComparerFromComparison<T>(comparison));
		}

		// Token: 0x04000EF3 RID: 3827
		private const uint MAXITEMS = 2147483646U;

		// Token: 0x04000EF4 RID: 3828
		private const int MAXLEAF = 120;

		// Token: 0x04000EF5 RID: 3829
		private const int BALANCEFACTOR = 6;

		// Token: 0x04000EF6 RID: 3830
		private static readonly int[] FIBONACCI = new int[]
		{
			1, 2, 3, 5, 8, 13, 21, 34, 55, 89,
			144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946,
			17711, 28657, 46368, 75025, 121393, 196418, 317811, 514229, 832040, 1346269,
			2178309, 3524578, 5702887, 9227465, 14930352, 24157817, 39088169, 63245986, 102334155, 165580141,
			267914296, 433494437, 701408733, 1134903170, 1836311903, int.MaxValue
		};

		// Token: 0x04000EF7 RID: 3831
		private const int MAXFIB = 44;

		// Token: 0x04000EF8 RID: 3832
		private BigList<T>.Node root;

		// Token: 0x04000EF9 RID: 3833
		private int changeStamp;

		// Token: 0x020016CE RID: 5838
		[Serializable]
		private abstract class Node
		{
			// Token: 0x17000D9B RID: 3483
			// (get) Token: 0x0600DA19 RID: 55833 RVA: 0x00502208 File Offset: 0x00500408
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x17000D9C RID: 3484
			// (get) Token: 0x0600DA1A RID: 55834 RVA: 0x00502210 File Offset: 0x00500410
			public bool Shared
			{
				get
				{
					return this.shared;
				}
			}

			// Token: 0x0600DA1B RID: 55835 RVA: 0x0050221A File Offset: 0x0050041A
			public void MarkShared()
			{
				this.shared = true;
			}

			// Token: 0x17000D9D RID: 3485
			// (get) Token: 0x0600DA1C RID: 55836
			public abstract int Depth { get; }

			// Token: 0x0600DA1D RID: 55837
			public abstract T GetAt(int index);

			// Token: 0x0600DA1E RID: 55838
			public abstract BigList<T>.Node Subrange(int first, int last);

			// Token: 0x0600DA1F RID: 55839
			public abstract BigList<T>.Node SetAt(int index, T item);

			// Token: 0x0600DA20 RID: 55840
			public abstract BigList<T>.Node SetAtInPlace(int index, T item);

			// Token: 0x0600DA21 RID: 55841
			public abstract BigList<T>.Node Append(BigList<T>.Node node, bool nodeIsUnused);

			// Token: 0x0600DA22 RID: 55842
			public abstract BigList<T>.Node AppendInPlace(BigList<T>.Node node, bool nodeIsUnused);

			// Token: 0x0600DA23 RID: 55843
			public abstract BigList<T>.Node AppendInPlace(T item);

			// Token: 0x0600DA24 RID: 55844
			public abstract BigList<T>.Node RemoveRange(int first, int last);

			// Token: 0x0600DA25 RID: 55845
			public abstract BigList<T>.Node RemoveRangeInPlace(int first, int last);

			// Token: 0x0600DA26 RID: 55846
			public abstract BigList<T>.Node Insert(int index, BigList<T>.Node node, bool nodeIsUnused);

			// Token: 0x0600DA27 RID: 55847
			public abstract BigList<T>.Node InsertInPlace(int index, T item);

			// Token: 0x0600DA28 RID: 55848
			public abstract BigList<T>.Node InsertInPlace(int index, BigList<T>.Node node, bool nodeIsUnused);

			// Token: 0x0600DA29 RID: 55849 RVA: 0x00502225 File Offset: 0x00500425
			public BigList<T>.Node Prepend(BigList<T>.Node node, bool nodeIsUnused)
			{
				if (nodeIsUnused)
				{
					return node.AppendInPlace(this, false);
				}
				return node.Append(this, false);
			}

			// Token: 0x0600DA2A RID: 55850 RVA: 0x0050223B File Offset: 0x0050043B
			public BigList<T>.Node PrependInPlace(BigList<T>.Node node, bool nodeIsUnused)
			{
				if (nodeIsUnused)
				{
					return node.AppendInPlace(this, !this.shared);
				}
				return node.Append(this, !this.shared);
			}

			// Token: 0x0600DA2B RID: 55851
			public abstract BigList<T>.Node PrependInPlace(T item);

			// Token: 0x0600DA2C RID: 55852 RVA: 0x00502265 File Offset: 0x00500465
			public bool IsBalanced()
			{
				return this.Depth <= 44 && this.Count >= BigList<T>.FIBONACCI[this.Depth];
			}

			// Token: 0x0600DA2D RID: 55853 RVA: 0x0050228A File Offset: 0x0050048A
			public bool IsAlmostBalanced()
			{
				return this.Depth == 0 || (this.Depth - 1 <= 44 && this.Count >= BigList<T>.FIBONACCI[this.Depth - 1]);
			}

			// Token: 0x04005BE0 RID: 23520
			public int count;

			// Token: 0x04005BE1 RID: 23521
			protected volatile bool shared;
		}

		// Token: 0x020016CF RID: 5839
		[Serializable]
		private sealed class LeafNode : BigList<T>.Node
		{
			// Token: 0x0600DA2F RID: 55855 RVA: 0x005022C5 File Offset: 0x005004C5
			public LeafNode(T item)
			{
				this.count = 1;
				this.items = new T[120];
				this.items[0] = item;
			}

			// Token: 0x0600DA30 RID: 55856 RVA: 0x005022EE File Offset: 0x005004EE
			public LeafNode(int count, T[] newItems)
			{
				this.count = count;
				this.items = newItems;
			}

			// Token: 0x17000D9E RID: 3486
			// (get) Token: 0x0600DA31 RID: 55857 RVA: 0x00502304 File Offset: 0x00500504
			public override int Depth
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0600DA32 RID: 55858 RVA: 0x00502307 File Offset: 0x00500507
			public override T GetAt(int index)
			{
				return this.items[index];
			}

			// Token: 0x0600DA33 RID: 55859 RVA: 0x00502315 File Offset: 0x00500515
			public override BigList<T>.Node SetAtInPlace(int index, T item)
			{
				if (this.shared)
				{
					return this.SetAt(index, item);
				}
				this.items[index] = item;
				return this;
			}

			// Token: 0x0600DA34 RID: 55860 RVA: 0x00502338 File Offset: 0x00500538
			public override BigList<T>.Node SetAt(int index, T item)
			{
				T[] array = (T[])this.items.Clone();
				array[index] = item;
				return new BigList<T>.LeafNode(this.count, array);
			}

			// Token: 0x0600DA35 RID: 55861 RVA: 0x0050236C File Offset: 0x0050056C
			private bool MergeLeafInPlace(BigList<T>.Node other)
			{
				BigList<T>.LeafNode leafNode = other as BigList<T>.LeafNode;
				int num;
				if (leafNode != null && (num = leafNode.Count + this.count) <= 120)
				{
					if (num > this.items.Length)
					{
						T[] array = new T[120];
						Array.Copy(this.items, 0, array, 0, this.count);
						this.items = array;
					}
					Array.Copy(leafNode.items, 0, this.items, this.count, leafNode.count);
					this.count = num;
					return true;
				}
				return false;
			}

			// Token: 0x0600DA36 RID: 55862 RVA: 0x005023EC File Offset: 0x005005EC
			private BigList<T>.Node MergeLeaf(BigList<T>.Node other)
			{
				BigList<T>.LeafNode leafNode = other as BigList<T>.LeafNode;
				int num;
				if (leafNode != null && (num = leafNode.Count + this.count) <= 120)
				{
					T[] array = new T[120];
					Array.Copy(this.items, 0, array, 0, this.count);
					Array.Copy(leafNode.items, 0, array, this.count, leafNode.count);
					return new BigList<T>.LeafNode(num, array);
				}
				return null;
			}

			// Token: 0x0600DA37 RID: 55863 RVA: 0x00502454 File Offset: 0x00500654
			public override BigList<T>.Node PrependInPlace(T item)
			{
				if (this.shared)
				{
					return base.Prepend(new BigList<T>.LeafNode(item), true);
				}
				if (this.count < 120)
				{
					if (this.count == this.items.Length)
					{
						T[] array = new T[120];
						Array.Copy(this.items, 0, array, 1, this.count);
						this.items = array;
					}
					else
					{
						Array.Copy(this.items, 0, this.items, 1, this.count);
					}
					this.items[0] = item;
					this.count++;
					return this;
				}
				return new BigList<T>.ConcatNode(new BigList<T>.LeafNode(item), this);
			}

			// Token: 0x0600DA38 RID: 55864 RVA: 0x005024FC File Offset: 0x005006FC
			public override BigList<T>.Node AppendInPlace(T item)
			{
				if (this.shared)
				{
					return this.Append(new BigList<T>.LeafNode(item), true);
				}
				if (this.count < 120)
				{
					if (this.count == this.items.Length)
					{
						T[] array = new T[120];
						Array.Copy(this.items, 0, array, 0, this.count);
						this.items = array;
					}
					this.items[this.count] = item;
					this.count++;
					return this;
				}
				return new BigList<T>.ConcatNode(this, new BigList<T>.LeafNode(item));
			}

			// Token: 0x0600DA39 RID: 55865 RVA: 0x0050258C File Offset: 0x0050078C
			public override BigList<T>.Node AppendInPlace(BigList<T>.Node node, bool nodeIsUnused)
			{
				if (this.shared)
				{
					return this.Append(node, nodeIsUnused);
				}
				if (this.MergeLeafInPlace(node))
				{
					return this;
				}
				BigList<T>.ConcatNode concatNode = node as BigList<T>.ConcatNode;
				if (concatNode != null && this.MergeLeafInPlace(concatNode.left))
				{
					if (!nodeIsUnused)
					{
						concatNode.right.MarkShared();
					}
					return new BigList<T>.ConcatNode(this, concatNode.right);
				}
				if (!nodeIsUnused)
				{
					node.MarkShared();
				}
				return new BigList<T>.ConcatNode(this, node);
			}

			// Token: 0x0600DA3A RID: 55866 RVA: 0x005025FC File Offset: 0x005007FC
			public override BigList<T>.Node Append(BigList<T>.Node node, bool nodeIsUnused)
			{
				BigList<T>.Node node2;
				if ((node2 = this.MergeLeaf(node)) != null)
				{
					return node2;
				}
				BigList<T>.ConcatNode concatNode = node as BigList<T>.ConcatNode;
				if (concatNode != null && (node2 = this.MergeLeaf(concatNode.left)) != null)
				{
					if (!nodeIsUnused)
					{
						concatNode.right.MarkShared();
					}
					return new BigList<T>.ConcatNode(node2, concatNode.right);
				}
				if (!nodeIsUnused)
				{
					node.MarkShared();
				}
				base.MarkShared();
				return new BigList<T>.ConcatNode(this, node);
			}

			// Token: 0x0600DA3B RID: 55867 RVA: 0x00502664 File Offset: 0x00500864
			public override BigList<T>.Node InsertInPlace(int index, T item)
			{
				if (this.shared)
				{
					return this.Insert(index, new BigList<T>.LeafNode(item), true);
				}
				if (this.count < 120)
				{
					if (this.count == this.items.Length)
					{
						T[] array = new T[120];
						if (index > 0)
						{
							Array.Copy(this.items, 0, array, 0, index);
						}
						if (this.count > index)
						{
							Array.Copy(this.items, index, array, index + 1, this.count - index);
						}
						this.items = array;
					}
					else if (this.count > index)
					{
						Array.Copy(this.items, index, this.items, index + 1, this.count - index);
					}
					this.items[index] = item;
					this.count++;
					return this;
				}
				if (index == this.count)
				{
					return new BigList<T>.ConcatNode(this, new BigList<T>.LeafNode(item));
				}
				if (index == 0)
				{
					return new BigList<T>.ConcatNode(new BigList<T>.LeafNode(item), this);
				}
				T[] array2 = new T[120];
				Array.Copy(this.items, 0, array2, 0, index);
				array2[index] = item;
				BigList<T>.Node node = new BigList<T>.LeafNode(index + 1, array2);
				T[] array3 = new T[this.count - index];
				Array.Copy(this.items, index, array3, 0, this.count - index);
				BigList<T>.Node node2 = new BigList<T>.LeafNode(this.count - index, array3);
				return new BigList<T>.ConcatNode(node, node2);
			}

			// Token: 0x0600DA3C RID: 55868 RVA: 0x005027B8 File Offset: 0x005009B8
			public override BigList<T>.Node InsertInPlace(int index, BigList<T>.Node node, bool nodeIsUnused)
			{
				if (this.shared)
				{
					return this.Insert(index, node, nodeIsUnused);
				}
				BigList<T>.LeafNode leafNode = node as BigList<T>.LeafNode;
				int num;
				if (leafNode != null && (num = leafNode.Count + this.count) <= 120)
				{
					if (num > this.items.Length)
					{
						T[] array = new T[120];
						Array.Copy(this.items, 0, array, 0, index);
						Array.Copy(leafNode.items, 0, array, index, leafNode.Count);
						Array.Copy(this.items, index, array, index + leafNode.Count, this.count - index);
						this.items = array;
					}
					else
					{
						Array.Copy(this.items, index, this.items, index + leafNode.Count, this.count - index);
						Array.Copy(leafNode.items, 0, this.items, index, leafNode.count);
					}
					this.count = num;
					return this;
				}
				if (index == 0)
				{
					return base.PrependInPlace(node, nodeIsUnused);
				}
				if (index == this.count)
				{
					return this.AppendInPlace(node, nodeIsUnused);
				}
				T[] array2 = new T[index];
				Array.Copy(this.items, 0, array2, 0, index);
				BigList<T>.Node node2 = new BigList<T>.LeafNode(index, array2);
				T[] array3 = new T[this.count - index];
				Array.Copy(this.items, index, array3, 0, this.count - index);
				BigList<T>.Node node3 = new BigList<T>.LeafNode(this.count - index, array3);
				return node2.AppendInPlace(node, nodeIsUnused).AppendInPlace(node3, true);
			}

			// Token: 0x0600DA3D RID: 55869 RVA: 0x00502920 File Offset: 0x00500B20
			public override BigList<T>.Node Insert(int index, BigList<T>.Node node, bool nodeIsUnused)
			{
				BigList<T>.LeafNode leafNode = node as BigList<T>.LeafNode;
				int num;
				if (leafNode != null && (num = leafNode.Count + this.count) <= 120)
				{
					T[] array = new T[120];
					Array.Copy(this.items, 0, array, 0, index);
					Array.Copy(leafNode.items, 0, array, index, leafNode.Count);
					Array.Copy(this.items, index, array, index + leafNode.Count, this.count - index);
					return new BigList<T>.LeafNode(num, array);
				}
				if (index == 0)
				{
					return base.Prepend(node, nodeIsUnused);
				}
				if (index == this.count)
				{
					return this.Append(node, nodeIsUnused);
				}
				T[] array2 = new T[index];
				Array.Copy(this.items, 0, array2, 0, index);
				BigList<T>.Node node2 = new BigList<T>.LeafNode(index, array2);
				T[] array3 = new T[this.count - index];
				Array.Copy(this.items, index, array3, 0, this.count - index);
				BigList<T>.Node node3 = new BigList<T>.LeafNode(this.count - index, array3);
				return node2.AppendInPlace(node, nodeIsUnused).AppendInPlace(node3, true);
			}

			// Token: 0x0600DA3E RID: 55870 RVA: 0x00502A1C File Offset: 0x00500C1C
			public override BigList<T>.Node RemoveRangeInPlace(int first, int last)
			{
				if (this.shared)
				{
					return this.RemoveRange(first, last);
				}
				if (first <= 0 && last >= this.count - 1)
				{
					return null;
				}
				if (first < 0)
				{
					first = 0;
				}
				if (last >= this.count)
				{
					last = this.count - 1;
				}
				int num = first + (this.count - last - 1);
				if (this.count > last + 1)
				{
					Array.Copy(this.items, last + 1, this.items, first, this.count - last - 1);
				}
				for (int i = num; i < this.count; i++)
				{
					this.items[i] = default(T);
				}
				this.count = num;
				return this;
			}

			// Token: 0x0600DA3F RID: 55871 RVA: 0x00502ACC File Offset: 0x00500CCC
			public override BigList<T>.Node RemoveRange(int first, int last)
			{
				if (first <= 0 && last >= this.count - 1)
				{
					return null;
				}
				if (first < 0)
				{
					first = 0;
				}
				if (last >= this.count)
				{
					last = this.count - 1;
				}
				int num = first + (this.count - last - 1);
				T[] array = new T[num];
				if (first > 0)
				{
					Array.Copy(this.items, 0, array, 0, first);
				}
				if (this.count > last + 1)
				{
					Array.Copy(this.items, last + 1, array, first, this.count - last - 1);
				}
				return new BigList<T>.LeafNode(num, array);
			}

			// Token: 0x0600DA40 RID: 55872 RVA: 0x00502B58 File Offset: 0x00500D58
			public override BigList<T>.Node Subrange(int first, int last)
			{
				if (first <= 0 && last >= this.count - 1)
				{
					base.MarkShared();
					return this;
				}
				if (first < 0)
				{
					first = 0;
				}
				if (last >= this.count)
				{
					last = this.count - 1;
				}
				int num = last - first + 1;
				T[] array = new T[num];
				Array.Copy(this.items, first, array, 0, num);
				return new BigList<T>.LeafNode(num, array);
			}

			// Token: 0x04005BE2 RID: 23522
			public T[] items;
		}

		// Token: 0x020016D0 RID: 5840
		[Serializable]
		private sealed class ConcatNode : BigList<T>.Node
		{
			// Token: 0x17000D9F RID: 3487
			// (get) Token: 0x0600DA41 RID: 55873 RVA: 0x00502BB9 File Offset: 0x00500DB9
			public override int Depth
			{
				get
				{
					return (int)this.depth;
				}
			}

			// Token: 0x0600DA42 RID: 55874 RVA: 0x00502BC4 File Offset: 0x00500DC4
			public ConcatNode(BigList<T>.Node left, BigList<T>.Node right)
			{
				this.left = left;
				this.right = right;
				this.count = left.Count + right.Count;
				if (left.Depth > right.Depth)
				{
					this.depth = (short)(left.Depth + 1);
					return;
				}
				this.depth = (short)(right.Depth + 1);
			}

			// Token: 0x0600DA43 RID: 55875 RVA: 0x00502C28 File Offset: 0x00500E28
			private BigList<T>.Node NewNode(BigList<T>.Node newLeft, BigList<T>.Node newRight)
			{
				if (this.left == newLeft)
				{
					if (this.right == newRight)
					{
						base.MarkShared();
						return this;
					}
					this.left.MarkShared();
				}
				else if (this.right == newRight)
				{
					this.right.MarkShared();
				}
				if (newLeft == null)
				{
					return newRight;
				}
				if (newRight == null)
				{
					return newLeft;
				}
				return new BigList<T>.ConcatNode(newLeft, newRight);
			}

			// Token: 0x0600DA44 RID: 55876 RVA: 0x00502C84 File Offset: 0x00500E84
			private BigList<T>.Node NewNodeInPlace(BigList<T>.Node newLeft, BigList<T>.Node newRight)
			{
				if (newLeft == null)
				{
					return newRight;
				}
				if (newRight == null)
				{
					return newLeft;
				}
				this.left = newLeft;
				this.right = newRight;
				this.count = this.left.Count + this.right.Count;
				if (this.left.Depth > this.right.Depth)
				{
					this.depth = (short)(this.left.Depth + 1);
				}
				else
				{
					this.depth = (short)(this.right.Depth + 1);
				}
				return this;
			}

			// Token: 0x0600DA45 RID: 55877 RVA: 0x00502D0C File Offset: 0x00500F0C
			public override T GetAt(int index)
			{
				int count = this.left.Count;
				if (index < count)
				{
					return this.left.GetAt(index);
				}
				return this.right.GetAt(index - count);
			}

			// Token: 0x0600DA46 RID: 55878 RVA: 0x00502D44 File Offset: 0x00500F44
			public override BigList<T>.Node SetAtInPlace(int index, T item)
			{
				if (this.shared)
				{
					return this.SetAt(index, item);
				}
				int count = this.left.Count;
				if (index < count)
				{
					BigList<T>.Node node = this.left.SetAtInPlace(index, item);
					if (node != this.left)
					{
						return this.NewNodeInPlace(node, this.right);
					}
					return this;
				}
				else
				{
					BigList<T>.Node node2 = this.right.SetAtInPlace(index - count, item);
					if (node2 != this.right)
					{
						return this.NewNodeInPlace(this.left, node2);
					}
					return this;
				}
			}

			// Token: 0x0600DA47 RID: 55879 RVA: 0x00502DC4 File Offset: 0x00500FC4
			public override BigList<T>.Node SetAt(int index, T item)
			{
				int count = this.left.Count;
				if (index < count)
				{
					return this.NewNode(this.left.SetAt(index, item), this.right);
				}
				return this.NewNode(this.left, this.right.SetAt(index - count, item));
			}

			// Token: 0x0600DA48 RID: 55880 RVA: 0x00502E18 File Offset: 0x00501018
			public override BigList<T>.Node PrependInPlace(T item)
			{
				if (this.shared)
				{
					return base.Prepend(new BigList<T>.LeafNode(item), true);
				}
				BigList<T>.LeafNode leafNode;
				if (this.left.Count < 120 && !this.left.Shared && (leafNode = this.left as BigList<T>.LeafNode) != null)
				{
					int count = leafNode.Count;
					if (count == leafNode.items.Length)
					{
						T[] array = new T[120];
						Array.Copy(leafNode.items, 0, array, 1, count);
						leafNode.items = array;
					}
					else
					{
						Array.Copy(leafNode.items, 0, leafNode.items, 1, count);
					}
					leafNode.items[0] = item;
					leafNode.count++;
					this.count++;
					return this;
				}
				return new BigList<T>.ConcatNode(new BigList<T>.LeafNode(item), this);
			}

			// Token: 0x0600DA49 RID: 55881 RVA: 0x00502EEC File Offset: 0x005010EC
			public override BigList<T>.Node AppendInPlace(T item)
			{
				if (this.shared)
				{
					return this.Append(new BigList<T>.LeafNode(item), true);
				}
				BigList<T>.LeafNode leafNode;
				if (this.right.Count < 120 && !this.right.Shared && (leafNode = this.right as BigList<T>.LeafNode) != null)
				{
					int count = leafNode.Count;
					if (count == leafNode.items.Length)
					{
						T[] array = new T[120];
						Array.Copy(leafNode.items, 0, array, 0, count);
						leafNode.items = array;
					}
					leafNode.items[count] = item;
					leafNode.count++;
					this.count++;
					return this;
				}
				return new BigList<T>.ConcatNode(this, new BigList<T>.LeafNode(item));
			}

			// Token: 0x0600DA4A RID: 55882 RVA: 0x00502FA4 File Offset: 0x005011A4
			public override BigList<T>.Node AppendInPlace(BigList<T>.Node node, bool nodeIsUnused)
			{
				if (this.shared)
				{
					return this.Append(node, nodeIsUnused);
				}
				if (this.right.Count + node.Count <= 120 && this.right is BigList<T>.LeafNode && node is BigList<T>.LeafNode)
				{
					return this.NewNodeInPlace(this.left, this.right.AppendInPlace(node, nodeIsUnused));
				}
				if (!nodeIsUnused)
				{
					node.MarkShared();
				}
				return new BigList<T>.ConcatNode(this, node);
			}

			// Token: 0x0600DA4B RID: 55883 RVA: 0x0050301C File Offset: 0x0050121C
			public override BigList<T>.Node Append(BigList<T>.Node node, bool nodeIsUnused)
			{
				if (this.right.Count + node.Count <= 120 && this.right is BigList<T>.LeafNode && node is BigList<T>.LeafNode)
				{
					return this.NewNode(this.left, this.right.Append(node, nodeIsUnused));
				}
				base.MarkShared();
				if (!nodeIsUnused)
				{
					node.MarkShared();
				}
				return new BigList<T>.ConcatNode(this, node);
			}

			// Token: 0x0600DA4C RID: 55884 RVA: 0x00503084 File Offset: 0x00501284
			public override BigList<T>.Node InsertInPlace(int index, T item)
			{
				if (this.shared)
				{
					return this.Insert(index, new BigList<T>.LeafNode(item), true);
				}
				int count = this.left.Count;
				if (index <= count)
				{
					return this.NewNodeInPlace(this.left.InsertInPlace(index, item), this.right);
				}
				return this.NewNodeInPlace(this.left, this.right.InsertInPlace(index - count, item));
			}

			// Token: 0x0600DA4D RID: 55885 RVA: 0x005030F0 File Offset: 0x005012F0
			public override BigList<T>.Node InsertInPlace(int index, BigList<T>.Node node, bool nodeIsUnused)
			{
				if (this.shared)
				{
					return this.Insert(index, node, nodeIsUnused);
				}
				int count = this.left.Count;
				if (index < count)
				{
					return this.NewNodeInPlace(this.left.InsertInPlace(index, node, nodeIsUnused), this.right);
				}
				return this.NewNodeInPlace(this.left, this.right.InsertInPlace(index - count, node, nodeIsUnused));
			}

			// Token: 0x0600DA4E RID: 55886 RVA: 0x00503158 File Offset: 0x00501358
			public override BigList<T>.Node Insert(int index, BigList<T>.Node node, bool nodeIsUnused)
			{
				int count = this.left.Count;
				if (index < count)
				{
					return this.NewNode(this.left.Insert(index, node, nodeIsUnused), this.right);
				}
				return this.NewNode(this.left, this.right.Insert(index - count, node, nodeIsUnused));
			}

			// Token: 0x0600DA4F RID: 55887 RVA: 0x005031AC File Offset: 0x005013AC
			public override BigList<T>.Node RemoveRangeInPlace(int first, int last)
			{
				if (this.shared)
				{
					return this.RemoveRange(first, last);
				}
				if (first <= 0 && last >= this.count - 1)
				{
					return null;
				}
				int count = this.left.Count;
				BigList<T>.Node node = this.left;
				BigList<T>.Node node2 = this.right;
				if (first < count)
				{
					node = this.left.RemoveRangeInPlace(first, last);
				}
				if (last >= count)
				{
					node2 = this.right.RemoveRangeInPlace(first - count, last - count);
				}
				return this.NewNodeInPlace(node, node2);
			}

			// Token: 0x0600DA50 RID: 55888 RVA: 0x00503228 File Offset: 0x00501428
			public override BigList<T>.Node RemoveRange(int first, int last)
			{
				if (first <= 0 && last >= this.count - 1)
				{
					return null;
				}
				int count = this.left.Count;
				BigList<T>.Node node = this.left;
				BigList<T>.Node node2 = this.right;
				if (first < count)
				{
					node = this.left.RemoveRange(first, last);
				}
				if (last >= count)
				{
					node2 = this.right.RemoveRange(first - count, last - count);
				}
				return this.NewNode(node, node2);
			}

			// Token: 0x0600DA51 RID: 55889 RVA: 0x00503290 File Offset: 0x00501490
			public override BigList<T>.Node Subrange(int first, int last)
			{
				if (first <= 0 && last >= this.count - 1)
				{
					base.MarkShared();
					return this;
				}
				int count = this.left.Count;
				BigList<T>.Node node = null;
				BigList<T>.Node node2 = null;
				if (first < count)
				{
					node = this.left.Subrange(first, last);
				}
				if (last >= count)
				{
					node2 = this.right.Subrange(first - count, last - count);
				}
				if (node == null)
				{
					return node2;
				}
				if (node2 == null)
				{
					return node;
				}
				return new BigList<T>.ConcatNode(node, node2);
			}

			// Token: 0x04005BE3 RID: 23523
			public BigList<T>.Node left;

			// Token: 0x04005BE4 RID: 23524
			public BigList<T>.Node right;

			// Token: 0x04005BE5 RID: 23525
			private short depth;
		}

		// Token: 0x020016D1 RID: 5841
		[Serializable]
		private class BigListRange : ListBase<T>
		{
			// Token: 0x0600DA52 RID: 55890 RVA: 0x005032FD File Offset: 0x005014FD
			public BigListRange(BigList<T> wrappedList, int start, int count)
			{
				this.wrappedList = wrappedList;
				this.start = start;
				this.count = count;
			}

			// Token: 0x17000DA0 RID: 3488
			// (get) Token: 0x0600DA53 RID: 55891 RVA: 0x0050331A File Offset: 0x0050151A
			public override int Count
			{
				get
				{
					return Math.Min(this.count, this.wrappedList.Count - this.start);
				}
			}

			// Token: 0x0600DA54 RID: 55892 RVA: 0x0050333C File Offset: 0x0050153C
			public override void Clear()
			{
				if (this.wrappedList.Count - this.start < this.count)
				{
					this.count = this.wrappedList.Count - this.start;
				}
				while (this.count > 0)
				{
					this.wrappedList.RemoveAt(this.start + this.count - 1);
					this.count--;
				}
			}

			// Token: 0x0600DA55 RID: 55893 RVA: 0x005033AE File Offset: 0x005015AE
			public override void Insert(int index, T item)
			{
				if (index < 0 || index > this.count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.wrappedList.Insert(this.start + index, item);
				this.count++;
			}

			// Token: 0x0600DA56 RID: 55894 RVA: 0x005033EA File Offset: 0x005015EA
			public override void RemoveAt(int index)
			{
				if (index < 0 || index >= this.count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.wrappedList.RemoveAt(this.start + index);
				this.count--;
			}

			// Token: 0x17000DA1 RID: 3489
			public override T this[int index]
			{
				get
				{
					if (index < 0 || index >= this.count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.wrappedList[this.start + index];
				}
				set
				{
					if (index < 0 || index >= this.count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this.wrappedList[this.start + index] = value;
				}
			}

			// Token: 0x0600DA59 RID: 55897 RVA: 0x00503480 File Offset: 0x00501680
			public override IEnumerator<T> GetEnumerator()
			{
				return this.wrappedList.GetEnumerator(this.start, this.count);
			}

			// Token: 0x04005BE6 RID: 23526
			private BigList<T> wrappedList;

			// Token: 0x04005BE7 RID: 23527
			private int start;

			// Token: 0x04005BE8 RID: 23528
			private int count;
		}
	}
}
