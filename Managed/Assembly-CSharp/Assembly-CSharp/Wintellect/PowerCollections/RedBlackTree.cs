using System;
using System.Collections;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x02000306 RID: 774
	[Serializable]
	internal class RedBlackTree<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06001C8C RID: 7308 RVA: 0x0008C4B0 File Offset: 0x0008A6B0
		private RedBlackTree<T>.Node[] GetNodeStack()
		{
			int num;
			if (this.count < 1024)
			{
				num = 21;
			}
			else if (this.count < 65536)
			{
				num = 41;
			}
			else
			{
				num = 65;
			}
			if (this.stack == null || this.stack.Length < num)
			{
				this.stack = new RedBlackTree<T>.Node[num];
			}
			return this.stack;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0008C509 File Offset: 0x0008A709
		internal void StopEnumerations()
		{
			this.changeStamp++;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0008C519 File Offset: 0x0008A719
		private void CheckEnumerationStamp(int startStamp)
		{
			if (startStamp != this.changeStamp)
			{
				throw new InvalidOperationException(Strings.ChangeDuringEnumeration);
			}
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0008C52F File Offset: 0x0008A72F
		public RedBlackTree(IComparer<T> comparer)
		{
			this.comparer = comparer;
			this.count = 0;
			this.root = null;
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x0008C54C File Offset: 0x0008A74C
		public int ElementCount
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0008C554 File Offset: 0x0008A754
		public RedBlackTree<T> Clone()
		{
			RedBlackTree<T> redBlackTree = new RedBlackTree<T>(this.comparer);
			redBlackTree.count = this.count;
			if (this.root != null)
			{
				redBlackTree.root = this.root.Clone();
			}
			return redBlackTree;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0008C594 File Offset: 0x0008A794
		public bool Find(T key, bool findFirst, bool replace, out T item)
		{
			RedBlackTree<T>.Node node = this.root;
			RedBlackTree<T>.Node node2 = null;
			while (node != null)
			{
				int num = this.comparer.Compare(key, node.item);
				if (num < 0)
				{
					node = node.left;
				}
				else if (num > 0)
				{
					node = node.right;
				}
				else
				{
					node2 = node;
					if (findFirst)
					{
						node = node.left;
					}
					else
					{
						node = node.right;
					}
				}
			}
			if (node2 != null)
			{
				item = node2.item;
				if (replace)
				{
					node2.item = key;
				}
				return true;
			}
			item = default(T);
			return false;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0008C618 File Offset: 0x0008A818
		public int FindIndex(T key, bool findFirst)
		{
			T t;
			if (findFirst)
			{
				return this.FirstItemInRange(this.EqualRangeTester(key), out t);
			}
			return this.LastItemInRange(this.EqualRangeTester(key), out t);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0008C648 File Offset: 0x0008A848
		public T GetItemByIndex(int index)
		{
			if (index < 0 || index >= this.count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			RedBlackTree<T>.Node node = this.root;
			for (;;)
			{
				int num;
				if (node.left != null)
				{
					num = node.left.Count;
				}
				else
				{
					num = 0;
				}
				if (num > index)
				{
					node = node.left;
				}
				else
				{
					if (num == index)
					{
						break;
					}
					index -= num + 1;
					node = node.right;
				}
			}
			return node.item;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x0008C6B4 File Offset: 0x0008A8B4
		public bool Insert(T item, DuplicatePolicy dupPolicy, out T previous)
		{
			RedBlackTree<T>.Node node = this.root;
			RedBlackTree<T>.Node node2 = null;
			RedBlackTree<T>.Node node3 = null;
			RedBlackTree<T>.Node node4 = null;
			bool flag = false;
			bool flag2 = false;
			RedBlackTree<T>.Node node5 = null;
			this.StopEnumerations();
			bool flag3 = dupPolicy != DuplicatePolicy.InsertFirst && dupPolicy != DuplicatePolicy.InsertLast;
			RedBlackTree<T>.Node[] array = null;
			int num = 0;
			if (flag3)
			{
				array = this.GetNodeStack();
			}
			bool flag4;
			while (node != null)
			{
				if (node.left != null && node.left.IsRed && node.right != null && node.right.IsRed)
				{
					node = this.InsertSplit(node4, node3, node2, node, out flag4);
					if (flag3 && flag4)
					{
						num -= 2;
						if (num < 0)
						{
							num = 0;
						}
					}
				}
				node4 = node3;
				node3 = node2;
				node2 = node;
				int num2 = this.comparer.Compare(item, node.item);
				if (num2 == 0)
				{
					if (dupPolicy == DuplicatePolicy.DoNothing)
					{
						previous = node.item;
						for (int i = 0; i < num; i++)
						{
							array[i].DecrementCount();
						}
						return false;
					}
					if (dupPolicy == DuplicatePolicy.InsertFirst || dupPolicy == DuplicatePolicy.ReplaceFirst)
					{
						node5 = node;
						num2 = -1;
					}
					else
					{
						node5 = node;
						num2 = 1;
					}
				}
				node.IncrementCount();
				if (flag3)
				{
					array[num++] = node;
				}
				if (num2 < 0)
				{
					node = node.left;
					flag = true;
					flag2 = false;
				}
				else
				{
					node = node.right;
					flag2 = true;
					flag = false;
				}
			}
			if (node5 != null)
			{
				previous = node5.item;
				if (node5 != null && (dupPolicy == DuplicatePolicy.ReplaceFirst || dupPolicy == DuplicatePolicy.ReplaceLast))
				{
					node5.item = item;
					for (int j = 0; j < num; j++)
					{
						array[j].DecrementCount();
					}
					return false;
				}
			}
			else
			{
				previous = default(T);
			}
			node = new RedBlackTree<T>.Node();
			node.item = item;
			node.Count = 1;
			if (flag)
			{
				node2.left = node;
			}
			else if (flag2)
			{
				node2.right = node;
			}
			else
			{
				this.root = node;
			}
			this.InsertSplit(node4, node3, node2, node, out flag4);
			this.count++;
			return node5 == null;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0008C88C File Offset: 0x0008AA8C
		private RedBlackTree<T>.Node InsertSplit(RedBlackTree<T>.Node ggparent, RedBlackTree<T>.Node gparent, RedBlackTree<T>.Node parent, RedBlackTree<T>.Node node, out bool rotated)
		{
			if (node != this.root)
			{
				node.IsRed = true;
			}
			if (node.left != null)
			{
				node.left.IsRed = false;
			}
			if (node.right != null)
			{
				node.right.IsRed = false;
			}
			if (parent != null && parent.IsRed)
			{
				if (gparent.left == parent != (parent.left == node))
				{
					this.Rotate(gparent, parent, node);
					parent = node;
				}
				gparent.IsRed = true;
				this.Rotate(ggparent, gparent, parent);
				parent.IsRed = false;
				rotated = true;
				return parent;
			}
			rotated = false;
			return node;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0008C928 File Offset: 0x0008AB28
		private void Rotate(RedBlackTree<T>.Node node, RedBlackTree<T>.Node child, RedBlackTree<T>.Node gchild)
		{
			if (gchild == child.left)
			{
				child.left = gchild.right;
				gchild.right = child;
			}
			else
			{
				child.right = gchild.left;
				gchild.left = child;
			}
			child.Count = ((child.left != null) ? child.left.Count : 0) + ((child.right != null) ? child.right.Count : 0) + 1;
			gchild.Count = ((gchild.left != null) ? gchild.left.Count : 0) + ((gchild.right != null) ? gchild.right.Count : 0) + 1;
			if (node == null)
			{
				this.root = gchild;
				return;
			}
			if (child == node.left)
			{
				node.left = gchild;
				return;
			}
			node.right = gchild;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x0008C9F3 File Offset: 0x0008ABF3
		public bool Delete(T key, bool deleteFirst, out T item)
		{
			return this.DeleteItemFromRange(this.EqualRangeTester(key), deleteFirst, out item);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0008CA04 File Offset: 0x0008AC04
		public void Clear()
		{
			this.root = null;
			if (this.count > 0)
			{
				this.count = 0;
				Array.Clear(this.stack, 0, this.stack.Length);
			}
			this.changeStamp++;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0008CA3F File Offset: 0x0008AC3F
		public IEnumerator<T> GetEnumerator()
		{
			return this.EnumerateRange(new RedBlackTree<T>.RangeTester(this.EntireRangeTester)).GetEnumerator();
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0008CA58 File Offset: 0x0008AC58
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0008CA60 File Offset: 0x0008AC60
		public RedBlackTree<T>.RangeTester BoundedRangeTester(bool useFirst, T first, bool useLast, T last)
		{
			return delegate(T item)
			{
				if (useFirst && this.comparer.Compare(first, item) > 0)
				{
					return -1;
				}
				if (useLast && this.comparer.Compare(last, item) <= 0)
				{
					return 1;
				}
				return 0;
			};
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x0008CA96 File Offset: 0x0008AC96
		public RedBlackTree<T>.RangeTester DoubleBoundedRangeTester(T first, bool firstInclusive, T last, bool lastInclusive)
		{
			return delegate(T item)
			{
				if (firstInclusive)
				{
					if (this.comparer.Compare(first, item) > 0)
					{
						return -1;
					}
				}
				else if (this.comparer.Compare(first, item) >= 0)
				{
					return -1;
				}
				if (lastInclusive)
				{
					if (this.comparer.Compare(last, item) < 0)
					{
						return 1;
					}
				}
				else if (this.comparer.Compare(last, item) <= 0)
				{
					return 1;
				}
				return 0;
			};
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x0008CACC File Offset: 0x0008ACCC
		public RedBlackTree<T>.RangeTester LowerBoundedRangeTester(T first, bool inclusive)
		{
			return delegate(T item)
			{
				if (inclusive)
				{
					if (this.comparer.Compare(first, item) > 0)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (this.comparer.Compare(first, item) >= 0)
					{
						return -1;
					}
					return 0;
				}
			};
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0008CAF3 File Offset: 0x0008ACF3
		public RedBlackTree<T>.RangeTester UpperBoundedRangeTester(T last, bool inclusive)
		{
			return delegate(T item)
			{
				if (inclusive)
				{
					if (this.comparer.Compare(last, item) < 0)
					{
						return 1;
					}
					return 0;
				}
				else
				{
					if (this.comparer.Compare(last, item) <= 0)
					{
						return 1;
					}
					return 0;
				}
			};
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0008CB1A File Offset: 0x0008AD1A
		public RedBlackTree<T>.RangeTester EqualRangeTester(T equalTo)
		{
			return (T item) => this.comparer.Compare(item, equalTo);
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0008CB3A File Offset: 0x0008AD3A
		public int EntireRangeTester(T item)
		{
			return 0;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0008CB3D File Offset: 0x0008AD3D
		public IEnumerable<T> EnumerateRange(RedBlackTree<T>.RangeTester rangeTester)
		{
			return this.EnumerateRangeInOrder(rangeTester, this.root);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0008CB4C File Offset: 0x0008AD4C
		private IEnumerable<T> EnumerateRangeInOrder(RedBlackTree<T>.RangeTester rangeTester, RedBlackTree<T>.Node node)
		{
			int startStamp = this.changeStamp;
			if (node != null)
			{
				int compare = rangeTester(node.item);
				if (compare >= 0)
				{
					foreach (T t in this.EnumerateRangeInOrder(rangeTester, node.left))
					{
						yield return t;
						this.CheckEnumerationStamp(startStamp);
					}
					IEnumerator<T> enumerator = null;
				}
				if (compare == 0)
				{
					yield return node.item;
					this.CheckEnumerationStamp(startStamp);
				}
				if (compare <= 0)
				{
					foreach (T t2 in this.EnumerateRangeInOrder(rangeTester, node.right))
					{
						yield return t2;
						this.CheckEnumerationStamp(startStamp);
					}
					IEnumerator<T> enumerator = null;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0008CB6A File Offset: 0x0008AD6A
		public IEnumerable<T> EnumerateRangeReversed(RedBlackTree<T>.RangeTester rangeTester)
		{
			return this.EnumerateRangeInReversedOrder(rangeTester, this.root);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0008CB79 File Offset: 0x0008AD79
		private IEnumerable<T> EnumerateRangeInReversedOrder(RedBlackTree<T>.RangeTester rangeTester, RedBlackTree<T>.Node node)
		{
			int startStamp = this.changeStamp;
			if (node != null)
			{
				int compare = rangeTester(node.item);
				if (compare <= 0)
				{
					foreach (T t in this.EnumerateRangeInReversedOrder(rangeTester, node.right))
					{
						yield return t;
						this.CheckEnumerationStamp(startStamp);
					}
					IEnumerator<T> enumerator = null;
				}
				if (compare == 0)
				{
					yield return node.item;
					this.CheckEnumerationStamp(startStamp);
				}
				if (compare >= 0)
				{
					foreach (T t2 in this.EnumerateRangeInReversedOrder(rangeTester, node.left))
					{
						yield return t2;
						this.CheckEnumerationStamp(startStamp);
					}
					IEnumerator<T> enumerator = null;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0008CB98 File Offset: 0x0008AD98
		public bool DeleteItemFromRange(RedBlackTree<T>.RangeTester rangeTester, bool deleteFirst, out T item)
		{
			this.StopEnumerations();
			if (this.root == null)
			{
				item = default(T);
				return false;
			}
			RedBlackTree<T>.Node[] nodeStack = this.GetNodeStack();
			int num = 0;
			RedBlackTree<T>.Node node = this.root;
			RedBlackTree<T>.Node node4;
			RedBlackTree<T>.Node node3;
			RedBlackTree<T>.Node node2 = (node3 = (node4 = null));
			RedBlackTree<T>.Node node5 = null;
			for (;;)
			{
				if ((node.left == null || !node.left.IsRed) && (node.right == null || !node.right.IsRed))
				{
					if (node2 == null)
					{
						node.IsRed = true;
					}
					else if ((node3.left == null || !node3.left.IsRed) && (node3.right == null || !node3.right.IsRed))
					{
						node.IsRed = true;
						node3.IsRed = true;
						node2.IsRed = false;
					}
					else
					{
						if (node2.left == node && (node3.right == null || !node3.right.IsRed))
						{
							RedBlackTree<T>.Node left = node3.left;
							this.Rotate(node2, node3, left);
							node3 = left;
						}
						else if (node2.right == node && (node3.left == null || !node3.left.IsRed))
						{
							RedBlackTree<T>.Node right = node3.right;
							this.Rotate(node2, node3, right);
							node3 = right;
						}
						this.Rotate(node4, node2, node3);
						node.IsRed = true;
						node3.IsRed = true;
						node3.left.IsRed = false;
						node3.right.IsRed = false;
						node3.DecrementCount();
						nodeStack[num - 1] = node3;
						node2.DecrementCount();
						nodeStack[num++] = node2;
					}
				}
				do
				{
					node.DecrementCount();
					nodeStack[num++] = node;
					int num2 = rangeTester(node.item);
					RedBlackTree<T>.Node node6;
					RedBlackTree<T>.Node node7;
					if (num2 == 0)
					{
						node5 = node;
						if (deleteFirst)
						{
							node6 = node.left;
							node7 = node.right;
						}
						else
						{
							node6 = node.right;
							node7 = node.left;
						}
					}
					else if (num2 > 0)
					{
						node6 = node.left;
						node7 = node.right;
					}
					else
					{
						node6 = node.right;
						node7 = node.left;
					}
					if (node6 == null)
					{
						goto IL_0260;
					}
					node4 = node2;
					node2 = node;
					node = node6;
					node3 = node7;
				}
				while (!node2.IsRed && node.IsRed);
				if (!node2.IsRed)
				{
					this.Rotate(node4, node2, node3);
					node3.DecrementCount();
					nodeStack[num - 1] = node3;
					node2.DecrementCount();
					nodeStack[num++] = node2;
					node3.IsRed = false;
					node2.IsRed = true;
					node4 = node3;
					node3 = ((node2.left == node) ? node2.right : node2.left);
				}
			}
			IL_0260:
			if (node5 == null)
			{
				for (int i = 0; i < num; i++)
				{
					nodeStack[i].IncrementCount();
				}
				if (this.root != null)
				{
					this.root.IsRed = false;
				}
				item = default(T);
				return false;
			}
			item = node5.item;
			if (node5 != node)
			{
				node5.item = node.item;
			}
			RedBlackTree<T>.Node node8;
			if (node.left != null)
			{
				node8 = node.left;
				node8.IsRed = false;
			}
			else if (node.right != null)
			{
				node8 = node.right;
				node8.IsRed = false;
			}
			else
			{
				node8 = null;
			}
			if (node2 == null)
			{
				this.root = node8;
			}
			else if (node2.left == node)
			{
				node2.left = node8;
			}
			else
			{
				node2.right = node8;
			}
			if (this.root != null)
			{
				this.root.IsRed = false;
			}
			this.count--;
			return true;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0008CEE4 File Offset: 0x0008B0E4
		public int DeleteRange(RedBlackTree<T>.RangeTester rangeTester)
		{
			int num = 0;
			bool flag;
			do
			{
				T t;
				flag = this.DeleteItemFromRange(rangeTester, true, out t);
				if (flag)
				{
					num++;
				}
			}
			while (flag);
			return num;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0008CF09 File Offset: 0x0008B109
		public int CountRange(RedBlackTree<T>.RangeTester rangeTester)
		{
			return this.CountRangeUnderNode(rangeTester, this.root, false, false);
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0008CF1C File Offset: 0x0008B11C
		private int CountRangeUnderNode(RedBlackTree<T>.RangeTester rangeTester, RedBlackTree<T>.Node node, bool belowRangeTop, bool aboveRangeBottom)
		{
			if (node == null)
			{
				return 0;
			}
			if (belowRangeTop && aboveRangeBottom)
			{
				return node.Count;
			}
			int num = rangeTester(node.item);
			int num2;
			if (num == 0)
			{
				num2 = 1;
				num2 += this.CountRangeUnderNode(rangeTester, node.left, true, aboveRangeBottom);
				num2 += this.CountRangeUnderNode(rangeTester, node.right, belowRangeTop, true);
			}
			else if (num < 0)
			{
				num2 = this.CountRangeUnderNode(rangeTester, node.right, belowRangeTop, aboveRangeBottom);
			}
			else
			{
				num2 = this.CountRangeUnderNode(rangeTester, node.left, belowRangeTop, aboveRangeBottom);
			}
			return num2;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0008CFA0 File Offset: 0x0008B1A0
		public int FirstItemInRange(RedBlackTree<T>.RangeTester rangeTester, out T item)
		{
			RedBlackTree<T>.Node node = this.root;
			RedBlackTree<T>.Node node2 = null;
			int num = 0;
			int num2 = -1;
			while (node != null)
			{
				int num3 = rangeTester(node.item);
				if (num3 == 0)
				{
					node2 = node;
					if (node.left != null)
					{
						num2 = num + node.left.Count;
					}
					else
					{
						num2 = num;
					}
				}
				if (num3 >= 0)
				{
					node = node.left;
				}
				else
				{
					if (node.left != null)
					{
						num += node.left.Count + 1;
					}
					else
					{
						num++;
					}
					node = node.right;
				}
			}
			if (node2 != null)
			{
				item = node2.item;
				return num2;
			}
			item = default(T);
			return -1;
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0008D034 File Offset: 0x0008B234
		public int LastItemInRange(RedBlackTree<T>.RangeTester rangeTester, out T item)
		{
			RedBlackTree<T>.Node node = this.root;
			RedBlackTree<T>.Node node2 = null;
			int num = 0;
			int num2 = -1;
			while (node != null)
			{
				int num3 = rangeTester(node.item);
				if (num3 == 0)
				{
					node2 = node;
					if (node.left != null)
					{
						num2 = num + node.left.Count;
					}
					else
					{
						num2 = num;
					}
				}
				if (num3 <= 0)
				{
					if (node.left != null)
					{
						num += node.left.Count + 1;
					}
					else
					{
						num++;
					}
					node = node.right;
				}
				else
				{
					node = node.left;
				}
			}
			if (node2 != null)
			{
				item = node2.item;
				return num2;
			}
			item = default(T);
			return num2;
		}

		// Token: 0x04000F2C RID: 3884
		private IComparer<T> comparer;

		// Token: 0x04000F2D RID: 3885
		private RedBlackTree<T>.Node root;

		// Token: 0x04000F2E RID: 3886
		private int count;

		// Token: 0x04000F2F RID: 3887
		private int changeStamp;

		// Token: 0x04000F30 RID: 3888
		private RedBlackTree<T>.Node[] stack;

		// Token: 0x02001705 RID: 5893
		[Serializable]
		private class Node
		{
			// Token: 0x17000DE3 RID: 3555
			// (get) Token: 0x0600DB71 RID: 56177 RVA: 0x00506358 File Offset: 0x00504558
			// (set) Token: 0x0600DB72 RID: 56178 RVA: 0x00506369 File Offset: 0x00504569
			public bool IsRed
			{
				get
				{
					return (this.count & 2147483648U) > 0U;
				}
				set
				{
					if (value)
					{
						this.count |= 2147483648U;
						return;
					}
					this.count &= 2147483647U;
				}
			}

			// Token: 0x17000DE4 RID: 3556
			// (get) Token: 0x0600DB73 RID: 56179 RVA: 0x00506393 File Offset: 0x00504593
			// (set) Token: 0x0600DB74 RID: 56180 RVA: 0x005063A1 File Offset: 0x005045A1
			public int Count
			{
				get
				{
					return (int)(this.count & 2147483647U);
				}
				set
				{
					this.count = (this.count & 2147483648U) | (uint)value;
				}
			}

			// Token: 0x0600DB75 RID: 56181 RVA: 0x005063B7 File Offset: 0x005045B7
			public void IncrementCount()
			{
				this.count += 1U;
			}

			// Token: 0x0600DB76 RID: 56182 RVA: 0x005063C7 File Offset: 0x005045C7
			public void DecrementCount()
			{
				this.count -= 1U;
			}

			// Token: 0x0600DB77 RID: 56183 RVA: 0x005063D8 File Offset: 0x005045D8
			public RedBlackTree<T>.Node Clone()
			{
				RedBlackTree<T>.Node node = new RedBlackTree<T>.Node();
				node.item = this.item;
				node.count = this.count;
				if (this.left != null)
				{
					node.left = this.left.Clone();
				}
				if (this.right != null)
				{
					node.right = this.right.Clone();
				}
				return node;
			}

			// Token: 0x04005C80 RID: 23680
			public RedBlackTree<T>.Node left;

			// Token: 0x04005C81 RID: 23681
			public RedBlackTree<T>.Node right;

			// Token: 0x04005C82 RID: 23682
			public T item;

			// Token: 0x04005C83 RID: 23683
			private const uint REDMASK = 2147483648U;

			// Token: 0x04005C84 RID: 23684
			private uint count;
		}

		// Token: 0x02001706 RID: 5894
		// (Invoke) Token: 0x0600DB7A RID: 56186
		public delegate int RangeTester(T item);
	}
}
