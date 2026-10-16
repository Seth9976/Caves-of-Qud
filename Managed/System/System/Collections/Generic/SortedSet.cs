using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of objects that is maintained in sorted order.</summary>
	/// <typeparam name="T">The type of elements in the set.</typeparam>
	// Token: 0x020004E4 RID: 1252
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class SortedSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class. </summary>
		// Token: 0x060028BE RID: 10430 RVA: 0x0008BDF2 File Offset: 0x00089FF2
		public SortedSet()
		{
			this.comparer = Comparer<T>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that uses a specified comparer.</summary>
		/// <param name="comparer">The default comparer to use for comparing objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is null.</exception>
		// Token: 0x060028BF RID: 10431 RVA: 0x0008BE05 File Offset: 0x0008A005
		public SortedSet(IComparer<T> comparer)
		{
			this.comparer = comparer ?? Comparer<T>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains elements copied from a specified enumerable collection.</summary>
		/// <param name="collection">The enumerable collection to be copied. </param>
		// Token: 0x060028C0 RID: 10432 RVA: 0x0008BE1D File Offset: 0x0008A01D
		public SortedSet(IEnumerable<T> collection)
			: this(collection, Comparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains elements copied from a specified enumerable collection and that uses a specified comparer.</summary>
		/// <param name="collection">The enumerable collection to be copied. </param>
		/// <param name="comparer">The default comparer to use for comparing objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x060028C1 RID: 10433 RVA: 0x0008BE2C File Offset: 0x0008A02C
		public SortedSet(IEnumerable<T> collection, IComparer<T> comparer)
			: this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			SortedSet<T> sortedSet = collection as SortedSet<T>;
			if (sortedSet != null && !(sortedSet is SortedSet<T>.TreeSubSet) && this.HasEqualComparer(sortedSet))
			{
				if (sortedSet.Count > 0)
				{
					this.count = sortedSet.count;
					this.root = sortedSet.root.DeepClone(this.count);
				}
				return;
			}
			int num;
			T[] array = EnumerableHelpers.ToArray<T>(collection, out num);
			if (num > 0)
			{
				comparer = this.comparer;
				Array.Sort<T>(array, 0, num, comparer);
				int num2 = 1;
				for (int i = 1; i < num; i++)
				{
					if (comparer.Compare(array[i], array[i - 1]) != 0)
					{
						array[num2++] = array[i];
					}
				}
				num = num2;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains serialized data.</summary>
		/// <param name="info">The object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">The structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		// Token: 0x060028C2 RID: 10434 RVA: 0x0008BF0D File Offset: 0x0008A10D
		protected SortedSet(SerializationInfo info, StreamingContext context)
		{
			this.siInfo = info;
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x0008BF1C File Offset: 0x0008A11C
		private void AddAllElements(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				if (!this.Contains(t))
				{
					this.Add(t);
				}
			}
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x0008BF70 File Offset: 0x0008A170
		private void RemoveAllElements(IEnumerable<T> collection)
		{
			T min = this.Min;
			T max = this.Max;
			foreach (T t in collection)
			{
				if (this.comparer.Compare(t, min) >= 0 && this.comparer.Compare(t, max) <= 0 && this.Contains(t))
				{
					this.Remove(t);
				}
			}
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x0008BFF0 File Offset: 0x0008A1F0
		private bool ContainsAllElements(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x0008C044 File Offset: 0x0008A244
		internal virtual bool InOrderTreeWalk(TreeWalkPredicate<T> action)
		{
			if (this.root == null)
			{
				return true;
			}
			Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(this.Count + 1));
			for (SortedSet<T>.Node node = this.root; node != null; node = node.Left)
			{
				stack.Push(node);
			}
			while (stack.Count != 0)
			{
				SortedSet<T>.Node node = stack.Pop();
				if (!action(node))
				{
					return false;
				}
				for (SortedSet<T>.Node node2 = node.Right; node2 != null; node2 = node2.Left)
				{
					stack.Push(node2);
				}
			}
			return true;
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x0008C0C4 File Offset: 0x0008A2C4
		internal virtual bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
		{
			if (this.root == null)
			{
				return true;
			}
			Queue<SortedSet<T>.Node> queue = new Queue<SortedSet<T>.Node>();
			queue.Enqueue(this.root);
			while (queue.Count != 0)
			{
				SortedSet<T>.Node node = queue.Dequeue();
				if (!action(node))
				{
					return false;
				}
				if (node.Left != null)
				{
					queue.Enqueue(node.Left);
				}
				if (node.Right != null)
				{
					queue.Enqueue(node.Right);
				}
			}
			return true;
		}

		/// <summary>Gets the number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</returns>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060028C8 RID: 10440 RVA: 0x0008C132 File Offset: 0x0008A332
		public int Count
		{
			get
			{
				this.VersionCheck();
				return this.count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> object that is used to determine equality for the values in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The comparer that is used to determine equality for the values in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</returns>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060028C9 RID: 10441 RVA: 0x0008C140 File Offset: 0x0008A340
		public IComparer<T> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Collections.ICollection" /> is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized; otherwise, false.</returns>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x0008C148 File Offset: 0x0008A348
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void VersionCheck()
		{
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x0000390E File Offset: 0x00001B0E
		internal virtual bool IsWithinRange(T item)
		{
			return true;
		}

		/// <summary>Adds an element to the set and returns a value that indicates if it was successfully added.</summary>
		/// <returns>true if <paramref name="item" /> is added to the set; otherwise, false. </returns>
		/// <param name="item">The element to add to the set.</param>
		// Token: 0x060028CF RID: 10447 RVA: 0x0008C16A File Offset: 0x0008A36A
		public bool Add(T item)
		{
			return this.AddIfNotPresent(item);
		}

		/// <summary>Adds an item to an <see cref="T:System.Collections.Generic.ICollection`1" /> object.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</exception>
		// Token: 0x060028D0 RID: 10448 RVA: 0x0008C173 File Offset: 0x0008A373
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x0008C180 File Offset: 0x0008A380
		internal virtual bool AddIfNotPresent(T item)
		{
			if (this.root == null)
			{
				this.root = new SortedSet<T>.Node(item, NodeColor.Black);
				this.count = 1;
				this.version++;
				return true;
			}
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			this.version++;
			int num = 0;
			while (node != null)
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					this.root.ColorBlack();
					return false;
				}
				if (node.Is4Node)
				{
					node.Split4Node();
					if (SortedSet<T>.Node.IsNonNullRed(node2))
					{
						this.InsertionBalance(node, ref node2, node3, node4);
					}
				}
				node4 = node3;
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			SortedSet<T>.Node node5 = new SortedSet<T>.Node(item, NodeColor.Red);
			if (num > 0)
			{
				node2.Right = node5;
			}
			else
			{
				node2.Left = node5;
			}
			if (node2.IsRed)
			{
				this.InsertionBalance(node5, ref node2, node3, node4);
			}
			this.root.ColorBlack();
			this.count++;
			return true;
		}

		/// <summary>Removes a specified item from the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>true if the element is found and successfully removed; otherwise, false. </returns>
		/// <param name="item">The element to remove.</param>
		// Token: 0x060028D2 RID: 10450 RVA: 0x0008C28A File Offset: 0x0008A48A
		public bool Remove(T item)
		{
			return this.DoRemove(item);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x0008C294 File Offset: 0x0008A494
		internal virtual bool DoRemove(T item)
		{
			if (this.root == null)
			{
				return false;
			}
			this.version++;
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			SortedSet<T>.Node node5 = null;
			bool flag = false;
			while (node != null)
			{
				if (node.Is2Node)
				{
					if (node2 == null)
					{
						node.ColorRed();
					}
					else
					{
						SortedSet<T>.Node node6 = node2.GetSibling(node);
						if (node6.IsRed)
						{
							if (node2.Right == node6)
							{
								node2.RotateLeft();
							}
							else
							{
								node2.RotateRight();
							}
							node2.ColorRed();
							node6.ColorBlack();
							this.ReplaceChildOrRoot(node3, node2, node6);
							node3 = node6;
							if (node2 == node4)
							{
								node5 = node6;
							}
							node6 = node2.GetSibling(node);
						}
						if (node6.Is2Node)
						{
							node2.Merge2Nodes();
						}
						else
						{
							SortedSet<T>.Node node7 = node2.Rotate(node2.GetRotation(node, node6));
							node7.Color = node2.Color;
							node2.ColorBlack();
							node.ColorRed();
							this.ReplaceChildOrRoot(node3, node2, node7);
							if (node2 == node4)
							{
								node5 = node7;
							}
						}
					}
				}
				int num = (flag ? (-1) : this.comparer.Compare(item, node.Item));
				if (num == 0)
				{
					flag = true;
					node4 = node;
					node5 = node2;
				}
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			if (node4 != null)
			{
				this.ReplaceNode(node4, node5, node2, node3);
				this.count--;
			}
			SortedSet<T>.Node node8 = this.root;
			if (node8 != null)
			{
				node8.ColorBlack();
			}
			return flag;
		}

		/// <summary>Removes all elements from the set.</summary>
		// Token: 0x060028D4 RID: 10452 RVA: 0x0008C400 File Offset: 0x0008A600
		public virtual void Clear()
		{
			this.root = null;
			this.count = 0;
			this.version++;
		}

		/// <summary>Determines whether the set contains a specific element.</summary>
		/// <returns>true if the set contains <paramref name="item" />; otherwise, false.</returns>
		/// <param name="item">The element to locate in the set.</param>
		// Token: 0x060028D5 RID: 10453 RVA: 0x0008C41E File Offset: 0x0008A61E
		public virtual bool Contains(T item)
		{
			return this.FindNode(item) != null;
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the beginning of the target array.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedSet`1" /> exceeds the number of elements that the destination array can contain. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x060028D6 RID: 10454 RVA: 0x0008C42A File Offset: 0x0008A62A
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this.Count);
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x060028D7 RID: 10455 RVA: 0x0008C43A File Offset: 0x0008A63A
		public void CopyTo(T[] array, int index)
		{
			this.CopyTo(array, index, this.Count);
		}

		/// <summary>Copies a specified number of elements from <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="count" /> is less than zero.</exception>
		// Token: 0x060028D8 RID: 10456 RVA: 0x0008C44C File Offset: 0x0008A64C
		public void CopyTo(T[] array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (count > array.Length - index)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			count += index;
			this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
			{
				if (index >= count)
				{
					return false;
				}
				T[] array2 = array;
				int index2 = index;
				index = index2 + 1;
				array2[index2] = node.Item;
				return true;
			});
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x060028D9 RID: 10457 RVA: 0x0008C50C File Offset: 0x0008A70C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.", "array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			object[] objects = array as object[];
			if (objects == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			try
			{
				this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
				{
					object[] objects2 = objects;
					int index2 = index;
					index = index2 + 1;
					objects2[index2] = node.Item;
					return true;
				});
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>An enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" /> in sorted order.</returns>
		// Token: 0x060028DA RID: 10458 RVA: 0x0008C620 File Offset: 0x0008A820
		public SortedSet<T>.Enumerator GetEnumerator()
		{
			return new SortedSet<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060028DB RID: 10459 RVA: 0x0008C628 File Offset: 0x0008A828
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060028DC RID: 10460 RVA: 0x0008C628 File Offset: 0x0008A828
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x0008C638 File Offset: 0x0008A838
		private void InsertionBalance(SortedSet<T>.Node current, ref SortedSet<T>.Node parent, SortedSet<T>.Node grandParent, SortedSet<T>.Node greatGrandParent)
		{
			bool flag = grandParent.Right == parent;
			bool flag2 = parent.Right == current;
			SortedSet<T>.Node node;
			if (flag == flag2)
			{
				node = (flag2 ? grandParent.RotateLeft() : grandParent.RotateRight());
			}
			else
			{
				node = (flag2 ? grandParent.RotateLeftRight() : grandParent.RotateRightLeft());
				parent = greatGrandParent;
			}
			grandParent.ColorRed();
			node.ColorBlack();
			this.ReplaceChildOrRoot(greatGrandParent, grandParent, node);
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x0008C69D File Offset: 0x0008A89D
		private void ReplaceChildOrRoot(SortedSet<T>.Node parent, SortedSet<T>.Node child, SortedSet<T>.Node newChild)
		{
			if (parent != null)
			{
				parent.ReplaceChild(child, newChild);
				return;
			}
			this.root = newChild;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x0008C6B4 File Offset: 0x0008A8B4
		private void ReplaceNode(SortedSet<T>.Node match, SortedSet<T>.Node parentOfMatch, SortedSet<T>.Node successor, SortedSet<T>.Node parentOfSuccessor)
		{
			if (successor == match)
			{
				successor = match.Left;
			}
			else
			{
				SortedSet<T>.Node right = successor.Right;
				if (right != null)
				{
					right.ColorBlack();
				}
				if (parentOfSuccessor != match)
				{
					parentOfSuccessor.Left = successor.Right;
					successor.Right = match.Right;
				}
				successor.Left = match.Left;
			}
			if (successor != null)
			{
				successor.Color = match.Color;
			}
			this.ReplaceChildOrRoot(parentOfMatch, match, successor);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x0008C724 File Offset: 0x0008A924
		internal virtual SortedSet<T>.Node FindNode(T item)
		{
			int num;
			for (SortedSet<T>.Node node = this.root; node != null; node = ((num < 0) ? node.Left : node.Right))
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					return node;
				}
			}
			return null;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x0008C76C File Offset: 0x0008A96C
		internal virtual int InternalIndexOf(T item)
		{
			SortedSet<T>.Node node = this.root;
			int num = 0;
			while (node != null)
			{
				int num2 = this.comparer.Compare(item, node.Item);
				if (num2 == 0)
				{
					return num;
				}
				node = ((num2 < 0) ? node.Left : node.Right);
				num = ((num2 < 0) ? (2 * num + 1) : (2 * num + 2));
			}
			return -1;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x0008C7C4 File Offset: 0x0008A9C4
		internal SortedSet<T>.Node FindRange(T from, T to)
		{
			return this.FindRange(from, to, true, true);
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x0008C7D0 File Offset: 0x0008A9D0
		internal SortedSet<T>.Node FindRange(T from, T to, bool lowerBoundActive, bool upperBoundActive)
		{
			SortedSet<T>.Node node = this.root;
			while (node != null)
			{
				if (lowerBoundActive && this.comparer.Compare(from, node.Item) > 0)
				{
					node = node.Right;
				}
				else
				{
					if (!upperBoundActive || this.comparer.Compare(to, node.Item) >= 0)
					{
						return node;
					}
					node = node.Left;
				}
			}
			return null;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0008C82F File Offset: 0x0008AA2F
		internal void UpdateVersion()
		{
			this.version++;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object that can be used to create a collection that contains individual sets.</summary>
		/// <returns>A comparer for creating a collection of sets.</returns>
		// Token: 0x060028E5 RID: 10469 RVA: 0x0008C83F File Offset: 0x0008AA3F
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer()
		{
			return SortedSet<T>.CreateSetComparer(null);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object, according to a specified comparer, that can be used to create a collection that contains individual sets.</summary>
		/// <returns>A comparer for creating a collection of sets.</returns>
		/// <param name="memberEqualityComparer">The comparer to use for creating the returned comparer.</param>
		// Token: 0x060028E6 RID: 10470 RVA: 0x0008C847 File Offset: 0x0008AA47
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer(IEqualityComparer<T> memberEqualityComparer)
		{
			return new SortedSetEqualityComparer<T>(memberEqualityComparer);
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0008C850 File Offset: 0x0008AA50
		internal static bool SortedSetEquals(SortedSet<T> set1, SortedSet<T> set2, IComparer<T> comparer)
		{
			if (set1 == null)
			{
				return set2 == null;
			}
			if (set2 == null)
			{
				return false;
			}
			if (set1.HasEqualComparer(set2))
			{
				return set1.Count == set2.Count && set1.SetEquals(set2);
			}
			bool flag = false;
			foreach (T t in set1)
			{
				flag = false;
				foreach (T t2 in set2)
				{
					if (comparer.Compare(t, t2) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0008C91C File Offset: 0x0008AB1C
		private bool HasEqualComparer(SortedSet<T> other)
		{
			return this.Comparer == other.Comparer || this.Comparer.Equals(other.Comparer);
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains all elements that are present in either the current object or the specified collection. </summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060028E9 RID: 10473 RVA: 0x0008C940 File Offset: 0x0008AB40
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && this.count == 0)
			{
				SortedSet<T> sortedSet2 = new SortedSet<T>(sortedSet, this.comparer);
				this.root = sortedSet2.root;
				this.count = sortedSet2.count;
				this.version++;
				return;
			}
			if (sortedSet != null && treeSubSet == null && this.HasEqualComparer(sortedSet) && sortedSet.Count > this.Count / 2)
			{
				T[] array = new T[sortedSet.Count + this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						array[num++] = enumerator.Current;
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						array[num++] = enumerator2.Current;
						flag2 = !enumerator2.MoveNext();
					}
				}
				if (!flag || !flag2)
				{
					SortedSet<T>.Enumerator enumerator3 = (flag ? enumerator2 : enumerator);
					do
					{
						array[num++] = enumerator3.Current;
					}
					while (enumerator3.MoveNext());
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.AddAllElements(other);
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0008CB2C File Offset: 0x0008AD2C
		private static SortedSet<T>.Node ConstructRootFromSortedArray(T[] arr, int startIndex, int endIndex, SortedSet<T>.Node redNode)
		{
			int num = endIndex - startIndex + 1;
			SortedSet<T>.Node node;
			switch (num)
			{
			case 0:
				return null;
			case 1:
				node = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				if (redNode != null)
				{
					node.Left = redNode;
				}
				break;
			case 2:
				node = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				node.Right = new SortedSet<T>.Node(arr[endIndex], NodeColor.Black);
				node.Right.ColorRed();
				if (redNode != null)
				{
					node.Left = redNode;
				}
				break;
			case 3:
				node = new SortedSet<T>.Node(arr[startIndex + 1], NodeColor.Black);
				node.Left = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				node.Right = new SortedSet<T>.Node(arr[endIndex], NodeColor.Black);
				if (redNode != null)
				{
					node.Left.Left = redNode;
				}
				break;
			default:
			{
				int num2 = (startIndex + endIndex) / 2;
				node = new SortedSet<T>.Node(arr[num2], NodeColor.Black);
				node.Left = SortedSet<T>.ConstructRootFromSortedArray(arr, startIndex, num2 - 1, redNode);
				node.Right = ((num % 2 == 0) ? SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 2, endIndex, new SortedSet<T>.Node(arr[num2 + 1], NodeColor.Red)) : SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 1, endIndex, null));
				break;
			}
			}
			return node;
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains only elements that are also in a specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060028EB RID: 10475 RVA: 0x0008CC58 File Offset: 0x0008AE58
		public virtual void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return;
			}
			if (other == this)
			{
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && this.HasEqualComparer(sortedSet))
			{
				T[] array = new T[this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				T max = this.Max;
				T min = this.Min;
				while (!flag && !flag2 && this.Comparer.Compare(enumerator2.Current, max) <= 0)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						flag2 = !enumerator2.MoveNext();
					}
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.IntersectWithEnumerable(other);
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x0008CDB8 File Offset: 0x0008AFB8
		internal virtual void IntersectWithEnumerable(IEnumerable<T> other)
		{
			List<T> list = new List<T>(this.Count);
			foreach (T t in other)
			{
				if (this.Contains(t))
				{
					list.Add(t);
				}
			}
			this.Clear();
			foreach (T t2 in list)
			{
				this.Add(t2);
			}
		}

		/// <summary>Removes all elements that are in a specified collection from the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <param name="other">The collection of items to remove from the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060028ED RID: 10477 RVA: 0x0008CE5C File Offset: 0x0008B05C
		public void ExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.count == 0)
			{
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				if (this.comparer.Compare(sortedSet.Max, this.Min) < 0 || this.comparer.Compare(sortedSet.Min, this.Max) > 0)
				{
					return;
				}
				T min = this.Min;
				T max = this.Max;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						if (this.comparer.Compare(t, min) >= 0)
						{
							if (this.comparer.Compare(t, max) > 0)
							{
								break;
							}
							this.Remove(t);
						}
					}
					return;
				}
			}
			this.RemoveAllElements(other);
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains only elements that are present either in the current object or in the specified collection, but not both.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060028EE RID: 10478 RVA: 0x0008CF54 File Offset: 0x0008B154
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				this.UnionWith(other);
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				this.SymmetricExceptWithSameComparer(sortedSet);
				return;
			}
			int num;
			T[] array = EnumerableHelpers.ToArray<T>(other, out num);
			Array.Sort<T>(array, 0, num, this.Comparer);
			this.SymmetricExceptWithSameComparer(array, num);
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0008CFC4 File Offset: 0x0008B1C4
		private void SymmetricExceptWithSameComparer(SortedSet<T> other)
		{
			foreach (T t in other)
			{
				if (!this.Contains(t))
				{
					this.Add(t);
				}
				else
				{
					this.Remove(t);
				}
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x0008D028 File Offset: 0x0008B228
		private void SymmetricExceptWithSameComparer(T[] other, int count)
		{
			if (count == 0)
			{
				return;
			}
			T t = other[0];
			for (int i = 0; i < count; i++)
			{
				while (i < count && i != 0 && this.comparer.Compare(other[i], t) == 0)
				{
					i++;
				}
				if (i >= count)
				{
					break;
				}
				T t2 = other[i];
				if (!this.Contains(t2))
				{
					this.Add(t2);
				}
				else
				{
					this.Remove(t2);
				}
				t = t2;
			}
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a subset of the specified collection.</summary>
		/// <returns>true if the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060028F1 RID: 10481 RVA: 0x0008D098 File Offset: 0x0008B298
		[SecuritySafeCritical]
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				return this.Count <= sortedSet.Count && this.IsSubsetOfSortedSetWithSameComparer(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount >= 0;
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x0008D110 File Offset: 0x0008B310
		private bool IsSubsetOfSortedSetWithSameComparer(SortedSet<T> asSorted)
		{
			SortedSet<T> viewBetween = asSorted.GetViewBetween(this.Min, this.Max);
			foreach (T t in this)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper subset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060028F3 RID: 10483 RVA: 0x0008D17C File Offset: 0x0008B37C
		[SecuritySafeCritical]
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && this.Count == 0)
			{
				return (other as ICollection).Count > 0;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				return this.Count < sortedSet.Count && this.IsSubsetOfSortedSetWithSameComparer(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount > 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a superset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060028F4 RID: 10484 RVA: 0x0008D204 File Offset: 0x0008B404
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !this.HasEqualComparer(sortedSet))
			{
				return this.ContainsAllElements(other);
			}
			if (this.Count < sortedSet.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T t in sortedSet)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper superset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060028F5 RID: 10485 RVA: 0x0008D2C0 File Offset: 0x0008B4C0
		[SecuritySafeCritical]
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !this.HasEqualComparer(sortedSet))
			{
				SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
				return elementCount.UniqueCount < this.Count && elementCount.UnfoundCount == 0;
			}
			if (sortedSet.Count >= this.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T t in sortedSet)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object and the specified collection contain the same elements.</summary>
		/// <returns>true if the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is equal to <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060028F6 RID: 10486 RVA: 0x0008D3A4 File Offset: 0x0008B5A4
		[SecuritySafeCritical]
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					if (this.Comparer.Compare(enumerator.Current, enumerator2.Current) != 0)
					{
						return false;
					}
					flag = !enumerator.MoveNext();
					flag2 = !enumerator2.MoveNext();
				}
				return flag && flag2;
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount == 0;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object and a specified collection share common elements.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object and <paramref name="other" /> share at least one common element; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060028F7 RID: 10487 RVA: 0x0008D460 File Offset: 0x0008B660
		public bool Overlaps(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection<T> && (other as ICollection<T>).Count == 0)
			{
				return false;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet) && (this.comparer.Compare(this.Min, sortedSet.Max) > 0 || this.comparer.Compare(this.Max, sortedSet.Min) < 0))
			{
				return false;
			}
			foreach (T t in other)
			{
				if (this.Contains(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0008D52C File Offset: 0x0008B72C
		private unsafe SortedSet<T>.ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			SortedSet<T>.ElementCount elementCount;
			if (this.Count == 0)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						num++;
					}
				}
				elementCount.UniqueCount = 0;
				elementCount.UnfoundCount = num;
				return elementCount;
			}
			int num2 = BitHelper.ToIntArrayLength(this.Count);
			BitHelper bitHelper;
			int num3;
			int num4;
			checked
			{
				if (num2 <= 100)
				{
					bitHelper = new BitHelper(stackalloc int[unchecked((UIntPtr)num2) * 4], num2);
				}
				else
				{
					bitHelper = new BitHelper(new int[num2], num2);
				}
				num3 = 0;
				num4 = 0;
			}
			foreach (T t2 in other)
			{
				int num5 = this.InternalIndexOf(t2);
				if (num5 >= 0)
				{
					if (!bitHelper.IsMarked(num5))
					{
						bitHelper.MarkBit(num5);
						num4++;
					}
				}
				else
				{
					num3++;
					if (returnIfUnfound)
					{
						break;
					}
				}
			}
			elementCount.UniqueCount = num4;
			elementCount.UnfoundCount = num3;
			return elementCount;
		}

		/// <summary>Removes all elements that match the conditions defined by the specified predicate from a <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The number of elements that were removed from the <see cref="T:System.Collections.Generic.SortedSet`1" /> collection.. </returns>
		/// <param name="match">The delegate that defines the conditions of the elements to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x060028F9 RID: 10489 RVA: 0x0008D644 File Offset: 0x0008B844
		public int RemoveWhere(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<T> matches = new List<T>(this.Count);
			this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
			{
				if (match(n.Item))
				{
					matches.Add(n.Item);
				}
				return true;
			});
			int num = 0;
			for (int i = matches.Count - 1; i >= 0; i--)
			{
				if (this.Remove(matches[i]))
				{
					num++;
				}
			}
			return num;
		}

		/// <summary>Gets the minimum value in the <see cref="T:System.Collections.Generic.SortedSet`1" />, as defined by the comparer.</summary>
		/// <returns>The minimum value in the set.</returns>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060028FA RID: 10490 RVA: 0x0008D6C8 File Offset: 0x0008B8C8
		public T Min
		{
			get
			{
				return this.MinInternal;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x0008D6D0 File Offset: 0x0008B8D0
		internal virtual T MinInternal
		{
			get
			{
				if (this.root == null)
				{
					return default(T);
				}
				SortedSet<T>.Node left = this.root;
				while (left.Left != null)
				{
					left = left.Left;
				}
				return left.Item;
			}
		}

		/// <summary>Gets the maximum value in the <see cref="T:System.Collections.Generic.SortedSet`1" />, as defined by the comparer.</summary>
		/// <returns>The maximum value in the set.</returns>
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x0008D70D File Offset: 0x0008B90D
		public T Max
		{
			get
			{
				return this.MaxInternal;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060028FD RID: 10493 RVA: 0x0008D718 File Offset: 0x0008B918
		internal virtual T MaxInternal
		{
			get
			{
				if (this.root == null)
				{
					return default(T);
				}
				SortedSet<T>.Node right = this.root;
				while (right.Right != null)
				{
					right = right.Right;
				}
				return right.Item;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.Generic.IEnumerable`1" /> that iterates over the <see cref="T:System.Collections.Generic.SortedSet`1" /> in reverse order.</summary>
		/// <returns>An enumerator that iterates over the <see cref="T:System.Collections.Generic.SortedSet`1" /> in reverse order.</returns>
		// Token: 0x060028FE RID: 10494 RVA: 0x0008D755 File Offset: 0x0008B955
		public IEnumerable<T> Reverse()
		{
			SortedSet<T>.Enumerator e = new SortedSet<T>.Enumerator(this, true);
			while (e.MoveNext())
			{
				T t = e.Current;
				yield return t;
			}
			yield break;
		}

		/// <summary>Returns a view of a subset in a <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>A subset view that contains only the values in the specified range.</returns>
		/// <param name="lowerValue">The lowest desired value in the view.</param>
		/// <param name="upperValue">The highest desired value in the view. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="lowerValue" /> is more than <paramref name="upperValue" /> according to the comparer.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A tried operation on the view was outside the range specified by <paramref name="lowerValue" /> and <paramref name="upperValue" />.</exception>
		// Token: 0x060028FF RID: 10495 RVA: 0x0008D765 File Offset: 0x0008B965
		public virtual SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
		{
			if (this.Comparer.Compare(lowerValue, upperValue) > 0)
			{
				throw new ArgumentException("Must be less than or equal to upperValue.", "lowerValue");
			}
			return new SortedSet<T>.TreeSubSet(this, lowerValue, upperValue, true, true);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and returns the data that you need to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06002900 RID: 10496 RVA: 0x0008D791 File Offset: 0x0008B991
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data that you must have to serialize a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06002901 RID: 10497 RVA: 0x0008D79C File Offset: 0x0008B99C
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Count", this.count);
			info.AddValue("Comparer", this.comparer, typeof(IComparer<T>));
			info.AddValue("Version", this.version);
			if (this.root != null)
			{
				T[] array = new T[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("Items", array, typeof(T[]));
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.IDeserializationCallback" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
		// Token: 0x06002902 RID: 10498 RVA: 0x0008D826 File Offset: 0x0008BA26
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is invalid.</exception>
		// Token: 0x06002903 RID: 10499 RVA: 0x0008D830 File Offset: 0x0008BA30
		protected virtual void OnDeserialization(object sender)
		{
			if (this.comparer != null)
			{
				return;
			}
			if (this.siInfo == null)
			{
				throw new SerializationException("OnDeserialization method was called while the object was not being deserialized.");
			}
			this.comparer = (IComparer<T>)this.siInfo.GetValue("Comparer", typeof(IComparer<T>));
			int @int = this.siInfo.GetInt32("Count");
			if (@int != 0)
			{
				T[] array = (T[])this.siInfo.GetValue("Items", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException("The values for this dictionary are missing.");
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.Add(array[i]);
				}
			}
			this.version = this.siInfo.GetInt32("Version");
			if (this.count != @int)
			{
				throw new SerializationException("The serialized Count information doesn't match the number of items.");
			}
			this.siInfo = null;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x0008D910 File Offset: 0x0008BB10
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			SortedSet<T>.Node node = this.FindNode(equalValue);
			if (node != null)
			{
				actualValue = node.Item;
				return true;
			}
			actualValue = default(T);
			return false;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x0008D940 File Offset: 0x0008BB40
		private static int Log2(int value)
		{
			int num = 0;
			while (value > 0)
			{
				num++;
				value >>= 1;
			}
			return num;
		}

		// Token: 0x040015C7 RID: 5575
		private SortedSet<T>.Node root;

		// Token: 0x040015C8 RID: 5576
		private IComparer<T> comparer;

		// Token: 0x040015C9 RID: 5577
		private int count;

		// Token: 0x040015CA RID: 5578
		private int version;

		// Token: 0x040015CB RID: 5579
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040015CC RID: 5580
		private SerializationInfo siInfo;

		// Token: 0x040015CD RID: 5581
		private const string ComparerName = "Comparer";

		// Token: 0x040015CE RID: 5582
		private const string CountName = "Count";

		// Token: 0x040015CF RID: 5583
		private const string ItemsName = "Items";

		// Token: 0x040015D0 RID: 5584
		private const string VersionName = "Version";

		// Token: 0x040015D1 RID: 5585
		private const string TreeName = "Tree";

		// Token: 0x040015D2 RID: 5586
		private const string NodeValueName = "Item";

		// Token: 0x040015D3 RID: 5587
		private const string EnumStartName = "EnumStarted";

		// Token: 0x040015D4 RID: 5588
		private const string ReverseName = "Reverse";

		// Token: 0x040015D5 RID: 5589
		private const string EnumVersionName = "EnumVersion";

		// Token: 0x040015D6 RID: 5590
		private const string MinName = "Min";

		// Token: 0x040015D7 RID: 5591
		private const string MaxName = "Max";

		// Token: 0x040015D8 RID: 5592
		private const string LowerBoundActiveName = "lBoundActive";

		// Token: 0x040015D9 RID: 5593
		private const string UpperBoundActiveName = "uBoundActive";

		// Token: 0x040015DA RID: 5594
		internal const int StackAllocThreshold = 100;

		// Token: 0x020004E5 RID: 1253
		[Serializable]
		internal sealed class TreeSubSet : SortedSet<T>, ISerializable, IDeserializationCallback
		{
			// Token: 0x06002906 RID: 10502 RVA: 0x0008D960 File Offset: 0x0008BB60
			public TreeSubSet(SortedSet<T> Underlying, T Min, T Max, bool lowerBoundActive, bool upperBoundActive)
				: base(Underlying.Comparer)
			{
				this._underlying = Underlying;
				this._min = Min;
				this._max = Max;
				this._lBoundActive = lowerBoundActive;
				this._uBoundActive = upperBoundActive;
				this.root = this._underlying.FindRange(this._min, this._max, this._lBoundActive, this._uBoundActive);
				this.count = 0;
				this.version = -1;
				this.VersionCheckImpl();
			}

			// Token: 0x06002907 RID: 10503 RVA: 0x0008D9DB File Offset: 0x0008BBDB
			internal override bool AddIfNotPresent(T item)
			{
				if (!this.IsWithinRange(item))
				{
					throw new ArgumentOutOfRangeException("item");
				}
				bool flag = this._underlying.AddIfNotPresent(item);
				this.VersionCheck();
				return flag;
			}

			// Token: 0x06002908 RID: 10504 RVA: 0x0008DA03 File Offset: 0x0008BC03
			public override bool Contains(T item)
			{
				this.VersionCheck();
				return base.Contains(item);
			}

			// Token: 0x06002909 RID: 10505 RVA: 0x0008DA12 File Offset: 0x0008BC12
			internal override bool DoRemove(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return false;
				}
				bool flag = this._underlying.Remove(item);
				this.VersionCheck();
				return flag;
			}

			// Token: 0x0600290A RID: 10506 RVA: 0x0008DA34 File Offset: 0x0008BC34
			public override void Clear()
			{
				if (this.count == 0)
				{
					return;
				}
				List<T> toRemove = new List<T>();
				this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
				{
					toRemove.Add(n.Item);
					return true;
				});
				while (toRemove.Count != 0)
				{
					this._underlying.Remove(toRemove[toRemove.Count - 1]);
					toRemove.RemoveAt(toRemove.Count - 1);
				}
				this.root = null;
				this.count = 0;
				this.version = this._underlying.version;
			}

			// Token: 0x0600290B RID: 10507 RVA: 0x0008DAD8 File Offset: 0x0008BCD8
			internal override bool IsWithinRange(T item)
			{
				return (this._lBoundActive ? base.Comparer.Compare(this._min, item) : (-1)) <= 0 && (this._uBoundActive ? base.Comparer.Compare(this._max, item) : 1) >= 0;
			}

			// Token: 0x17000874 RID: 2164
			// (get) Token: 0x0600290C RID: 10508 RVA: 0x0008DB2C File Offset: 0x0008BD2C
			internal override T MinInternal
			{
				get
				{
					SortedSet<T>.Node node = this.root;
					T t = default(T);
					while (node != null)
					{
						int num = (this._lBoundActive ? base.Comparer.Compare(this._min, node.Item) : (-1));
						if (num == 1)
						{
							node = node.Right;
						}
						else
						{
							t = node.Item;
							if (num == 0)
							{
								break;
							}
							node = node.Left;
						}
					}
					return t;
				}
			}

			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x0600290D RID: 10509 RVA: 0x0008DB90 File Offset: 0x0008BD90
			internal override T MaxInternal
			{
				get
				{
					SortedSet<T>.Node node = this.root;
					T t = default(T);
					while (node != null)
					{
						int num = (this._uBoundActive ? base.Comparer.Compare(this._max, node.Item) : 1);
						if (num == -1)
						{
							node = node.Left;
						}
						else
						{
							t = node.Item;
							if (num == 0)
							{
								break;
							}
							node = node.Right;
						}
					}
					return t;
				}
			}

			// Token: 0x0600290E RID: 10510 RVA: 0x0008DBF4 File Offset: 0x0008BDF4
			internal override bool InOrderTreeWalk(TreeWalkPredicate<T> action)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(this.count + 1));
				SortedSet<T>.Node node = this.root;
				while (node != null)
				{
					if (this.IsWithinRange(node.Item))
					{
						stack.Push(node);
						node = node.Left;
					}
					else if (this._lBoundActive && base.Comparer.Compare(this._min, node.Item) > 0)
					{
						node = node.Right;
					}
					else
					{
						node = node.Left;
					}
				}
				while (stack.Count != 0)
				{
					node = stack.Pop();
					if (!action(node))
					{
						return false;
					}
					SortedSet<T>.Node node2 = node.Right;
					while (node2 != null)
					{
						if (this.IsWithinRange(node2.Item))
						{
							stack.Push(node2);
							node2 = node2.Left;
						}
						else if (this._lBoundActive && base.Comparer.Compare(this._min, node2.Item) > 0)
						{
							node2 = node2.Right;
						}
						else
						{
							node2 = node2.Left;
						}
					}
				}
				return true;
			}

			// Token: 0x0600290F RID: 10511 RVA: 0x0008DCFC File Offset: 0x0008BEFC
			internal override bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				Queue<SortedSet<T>.Node> queue = new Queue<SortedSet<T>.Node>();
				queue.Enqueue(this.root);
				while (queue.Count != 0)
				{
					SortedSet<T>.Node node = queue.Dequeue();
					if (this.IsWithinRange(node.Item) && !action(node))
					{
						return false;
					}
					if (node.Left != null && (!this._lBoundActive || base.Comparer.Compare(this._min, node.Item) < 0))
					{
						queue.Enqueue(node.Left);
					}
					if (node.Right != null && (!this._uBoundActive || base.Comparer.Compare(this._max, node.Item) > 0))
					{
						queue.Enqueue(node.Right);
					}
				}
				return true;
			}

			// Token: 0x06002910 RID: 10512 RVA: 0x0008DDC8 File Offset: 0x0008BFC8
			internal override SortedSet<T>.Node FindNode(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return null;
				}
				this.VersionCheck();
				return base.FindNode(item);
			}

			// Token: 0x06002911 RID: 10513 RVA: 0x0008DDE4 File Offset: 0x0008BFE4
			internal override int InternalIndexOf(T item)
			{
				int num = -1;
				foreach (T t in this)
				{
					num++;
					if (base.Comparer.Compare(item, t) == 0)
					{
						return num;
					}
				}
				return -1;
			}

			// Token: 0x06002912 RID: 10514 RVA: 0x0008DE48 File Offset: 0x0008C048
			internal override void VersionCheck()
			{
				this.VersionCheckImpl();
			}

			// Token: 0x06002913 RID: 10515 RVA: 0x0008DE50 File Offset: 0x0008C050
			private void VersionCheckImpl()
			{
				if (this.version != this._underlying.version)
				{
					this.root = this._underlying.FindRange(this._min, this._max, this._lBoundActive, this._uBoundActive);
					this.version = this._underlying.version;
					this.count = 0;
					this.InOrderTreeWalk(delegate(SortedSet<T>.Node n)
					{
						this.count++;
						return true;
					});
				}
			}

			// Token: 0x06002914 RID: 10516 RVA: 0x0008DEC4 File Offset: 0x0008C0C4
			public override SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
			{
				if (this._lBoundActive && base.Comparer.Compare(this._min, lowerValue) > 0)
				{
					throw new ArgumentOutOfRangeException("lowerValue");
				}
				if (this._uBoundActive && base.Comparer.Compare(this._max, upperValue) < 0)
				{
					throw new ArgumentOutOfRangeException("upperValue");
				}
				return (SortedSet<T>.TreeSubSet)this._underlying.GetViewBetween(lowerValue, upperValue);
			}

			// Token: 0x06002915 RID: 10517 RVA: 0x0008D791 File Offset: 0x0008B991
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				this.GetObjectData(info, context);
			}

			// Token: 0x06002916 RID: 10518 RVA: 0x00011F38 File Offset: 0x00010138
			protected override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06002917 RID: 10519 RVA: 0x00011F38 File Offset: 0x00010138
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06002918 RID: 10520 RVA: 0x00011F38 File Offset: 0x00010138
			protected override void OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x040015DB RID: 5595
			private SortedSet<T> _underlying;

			// Token: 0x040015DC RID: 5596
			private T _min;

			// Token: 0x040015DD RID: 5597
			private T _max;

			// Token: 0x040015DE RID: 5598
			private bool _lBoundActive;

			// Token: 0x040015DF RID: 5599
			private bool _uBoundActive;
		}

		// Token: 0x020004E7 RID: 1255
		[Serializable]
		internal sealed class Node
		{
			// Token: 0x0600291C RID: 10524 RVA: 0x0008DF58 File Offset: 0x0008C158
			public Node(T item, NodeColor color)
			{
				this.Item = item;
				this.Color = color;
			}

			// Token: 0x0600291D RID: 10525 RVA: 0x0008DF6E File Offset: 0x0008C16E
			public static bool IsNonNullBlack(SortedSet<T>.Node node)
			{
				return node != null && node.IsBlack;
			}

			// Token: 0x0600291E RID: 10526 RVA: 0x0008DF7B File Offset: 0x0008C17B
			public static bool IsNonNullRed(SortedSet<T>.Node node)
			{
				return node != null && node.IsRed;
			}

			// Token: 0x0600291F RID: 10527 RVA: 0x0008DF88 File Offset: 0x0008C188
			public static bool IsNullOrBlack(SortedSet<T>.Node node)
			{
				return node == null || node.IsBlack;
			}

			// Token: 0x17000876 RID: 2166
			// (get) Token: 0x06002920 RID: 10528 RVA: 0x0008DF95 File Offset: 0x0008C195
			// (set) Token: 0x06002921 RID: 10529 RVA: 0x0008DF9D File Offset: 0x0008C19D
			public T Item { get; set; }

			// Token: 0x17000877 RID: 2167
			// (get) Token: 0x06002922 RID: 10530 RVA: 0x0008DFA6 File Offset: 0x0008C1A6
			// (set) Token: 0x06002923 RID: 10531 RVA: 0x0008DFAE File Offset: 0x0008C1AE
			public SortedSet<T>.Node Left { get; set; }

			// Token: 0x17000878 RID: 2168
			// (get) Token: 0x06002924 RID: 10532 RVA: 0x0008DFB7 File Offset: 0x0008C1B7
			// (set) Token: 0x06002925 RID: 10533 RVA: 0x0008DFBF File Offset: 0x0008C1BF
			public SortedSet<T>.Node Right { get; set; }

			// Token: 0x17000879 RID: 2169
			// (get) Token: 0x06002926 RID: 10534 RVA: 0x0008DFC8 File Offset: 0x0008C1C8
			// (set) Token: 0x06002927 RID: 10535 RVA: 0x0008DFD0 File Offset: 0x0008C1D0
			public NodeColor Color { get; set; }

			// Token: 0x1700087A RID: 2170
			// (get) Token: 0x06002928 RID: 10536 RVA: 0x0008DFD9 File Offset: 0x0008C1D9
			public bool IsBlack
			{
				get
				{
					return this.Color == NodeColor.Black;
				}
			}

			// Token: 0x1700087B RID: 2171
			// (get) Token: 0x06002929 RID: 10537 RVA: 0x0008DFE4 File Offset: 0x0008C1E4
			public bool IsRed
			{
				get
				{
					return this.Color == NodeColor.Red;
				}
			}

			// Token: 0x1700087C RID: 2172
			// (get) Token: 0x0600292A RID: 10538 RVA: 0x0008DFEF File Offset: 0x0008C1EF
			public bool Is2Node
			{
				get
				{
					return this.IsBlack && SortedSet<T>.Node.IsNullOrBlack(this.Left) && SortedSet<T>.Node.IsNullOrBlack(this.Right);
				}
			}

			// Token: 0x1700087D RID: 2173
			// (get) Token: 0x0600292B RID: 10539 RVA: 0x0008E013 File Offset: 0x0008C213
			public bool Is4Node
			{
				get
				{
					return SortedSet<T>.Node.IsNonNullRed(this.Left) && SortedSet<T>.Node.IsNonNullRed(this.Right);
				}
			}

			// Token: 0x0600292C RID: 10540 RVA: 0x0008E02F File Offset: 0x0008C22F
			public void ColorBlack()
			{
				this.Color = NodeColor.Black;
			}

			// Token: 0x0600292D RID: 10541 RVA: 0x0008E038 File Offset: 0x0008C238
			public void ColorRed()
			{
				this.Color = NodeColor.Red;
			}

			// Token: 0x0600292E RID: 10542 RVA: 0x0008E044 File Offset: 0x0008C244
			public SortedSet<T>.Node DeepClone(int count)
			{
				Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(count) + 2);
				Stack<SortedSet<T>.Node> stack2 = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(count) + 2);
				SortedSet<T>.Node node = this.ShallowClone();
				SortedSet<T>.Node node2 = this;
				SortedSet<T>.Node node3 = node;
				while (node2 != null)
				{
					stack.Push(node2);
					stack2.Push(node3);
					SortedSet<T>.Node node4 = node3;
					SortedSet<T>.Node left = node2.Left;
					node4.Left = ((left != null) ? left.ShallowClone() : null);
					node2 = node2.Left;
					node3 = node3.Left;
				}
				while (stack.Count != 0)
				{
					node2 = stack.Pop();
					node3 = stack2.Pop();
					SortedSet<T>.Node node5 = node2.Right;
					SortedSet<T>.Node node6 = ((node5 != null) ? node5.ShallowClone() : null);
					node3.Right = node6;
					while (node5 != null)
					{
						stack.Push(node5);
						stack2.Push(node6);
						SortedSet<T>.Node node7 = node6;
						SortedSet<T>.Node left2 = node5.Left;
						node7.Left = ((left2 != null) ? left2.ShallowClone() : null);
						node5 = node5.Left;
						node6 = node6.Left;
					}
				}
				return node;
			}

			// Token: 0x0600292F RID: 10543 RVA: 0x0008E138 File Offset: 0x0008C338
			public TreeRotation GetRotation(SortedSet<T>.Node current, SortedSet<T>.Node sibling)
			{
				bool flag = this.Left == current;
				if (!SortedSet<T>.Node.IsNonNullRed(sibling.Left))
				{
					if (!flag)
					{
						return TreeRotation.LeftRight;
					}
					return TreeRotation.Left;
				}
				else
				{
					if (!flag)
					{
						return TreeRotation.Right;
					}
					return TreeRotation.RightLeft;
				}
			}

			// Token: 0x06002930 RID: 10544 RVA: 0x0008E169 File Offset: 0x0008C369
			public SortedSet<T>.Node GetSibling(SortedSet<T>.Node node)
			{
				if (node != this.Left)
				{
					return this.Left;
				}
				return this.Right;
			}

			// Token: 0x06002931 RID: 10545 RVA: 0x0008E181 File Offset: 0x0008C381
			public SortedSet<T>.Node ShallowClone()
			{
				return new SortedSet<T>.Node(this.Item, this.Color);
			}

			// Token: 0x06002932 RID: 10546 RVA: 0x0008E194 File Offset: 0x0008C394
			public void Split4Node()
			{
				this.ColorRed();
				this.Left.ColorBlack();
				this.Right.ColorBlack();
			}

			// Token: 0x06002933 RID: 10547 RVA: 0x0008E1B4 File Offset: 0x0008C3B4
			public SortedSet<T>.Node Rotate(TreeRotation rotation)
			{
				switch (rotation)
				{
				case TreeRotation.Left:
					this.Right.Right.ColorBlack();
					return this.RotateLeft();
				case TreeRotation.LeftRight:
					return this.RotateLeftRight();
				case TreeRotation.Right:
					this.Left.Left.ColorBlack();
					return this.RotateRight();
				case TreeRotation.RightLeft:
					return this.RotateRightLeft();
				default:
					return null;
				}
			}

			// Token: 0x06002934 RID: 10548 RVA: 0x0008E218 File Offset: 0x0008C418
			public SortedSet<T>.Node RotateLeft()
			{
				SortedSet<T>.Node right = this.Right;
				this.Right = right.Left;
				right.Left = this;
				return right;
			}

			// Token: 0x06002935 RID: 10549 RVA: 0x0008E240 File Offset: 0x0008C440
			public SortedSet<T>.Node RotateLeftRight()
			{
				SortedSet<T>.Node left = this.Left;
				SortedSet<T>.Node right = left.Right;
				this.Left = right.Right;
				right.Right = this;
				left.Right = right.Left;
				right.Left = left;
				return right;
			}

			// Token: 0x06002936 RID: 10550 RVA: 0x0008E284 File Offset: 0x0008C484
			public SortedSet<T>.Node RotateRight()
			{
				SortedSet<T>.Node left = this.Left;
				this.Left = left.Right;
				left.Right = this;
				return left;
			}

			// Token: 0x06002937 RID: 10551 RVA: 0x0008E2AC File Offset: 0x0008C4AC
			public SortedSet<T>.Node RotateRightLeft()
			{
				SortedSet<T>.Node right = this.Right;
				SortedSet<T>.Node left = right.Left;
				this.Right = left.Left;
				left.Left = this;
				right.Left = left.Right;
				left.Right = right;
				return left;
			}

			// Token: 0x06002938 RID: 10552 RVA: 0x0008E2EE File Offset: 0x0008C4EE
			public void Merge2Nodes()
			{
				this.ColorBlack();
				this.Left.ColorRed();
				this.Right.ColorRed();
			}

			// Token: 0x06002939 RID: 10553 RVA: 0x0008E30C File Offset: 0x0008C50C
			public void ReplaceChild(SortedSet<T>.Node child, SortedSet<T>.Node newChild)
			{
				if (this.Left == child)
				{
					this.Left = newChild;
					return;
				}
				this.Right = newChild;
			}
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		// Token: 0x020004E8 RID: 1256
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
		{
			// Token: 0x0600293A RID: 10554 RVA: 0x0008E326 File Offset: 0x0008C526
			internal Enumerator(SortedSet<T> set)
			{
				this = new SortedSet<T>.Enumerator(set, false);
			}

			// Token: 0x0600293B RID: 10555 RVA: 0x0008E330 File Offset: 0x0008C530
			internal Enumerator(SortedSet<T> set, bool reverse)
			{
				this._tree = set;
				set.VersionCheck();
				this._version = set.version;
				this._stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(set.Count + 1));
				this._current = null;
				this._reverse = reverse;
				this.Initialize();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
			/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="info" /> is null.</exception>
			// Token: 0x0600293C RID: 10556 RVA: 0x00011F38 File Offset: 0x00010138
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
			/// <param name="sender">The source of the deserialization event.</param>
			/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
			// Token: 0x0600293D RID: 10557 RVA: 0x00011F38 File Offset: 0x00010138
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x0600293E RID: 10558 RVA: 0x0008E384 File Offset: 0x0008C584
			private void Initialize()
			{
				this._current = null;
				SortedSet<T>.Node node = this._tree.root;
				while (node != null)
				{
					SortedSet<T>.Node node2 = (this._reverse ? node.Right : node.Left);
					SortedSet<T>.Node node3 = (this._reverse ? node.Left : node.Right);
					if (this._tree.IsWithinRange(node.Item))
					{
						this._stack.Push(node);
						node = node2;
					}
					else if (node2 == null || !this._tree.IsWithinRange(node2.Item))
					{
						node = node3;
					}
					else
					{
						node = node2;
					}
				}
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedSet`1" /> collection.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x0600293F RID: 10559 RVA: 0x0008E41C File Offset: 0x0008C61C
			public bool MoveNext()
			{
				this._tree.VersionCheck();
				if (this._version != this._tree.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._stack.Count == 0)
				{
					this._current = null;
					return false;
				}
				this._current = this._stack.Pop();
				SortedSet<T>.Node node = (this._reverse ? this._current.Left : this._current.Right);
				while (node != null)
				{
					SortedSet<T>.Node node2 = (this._reverse ? node.Right : node.Left);
					SortedSet<T>.Node node3 = (this._reverse ? node.Left : node.Right);
					if (this._tree.IsWithinRange(node.Item))
					{
						this._stack.Push(node);
						node = node2;
					}
					else if (node3 == null || !this._tree.IsWithinRange(node3.Item))
					{
						node = node2;
					}
					else
					{
						node = node3;
					}
				}
				return true;
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedSet`1.Enumerator" />. </summary>
			// Token: 0x06002940 RID: 10560 RVA: 0x00003917 File Offset: 0x00001B17
			public void Dispose()
			{
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700087E RID: 2174
			// (get) Token: 0x06002941 RID: 10561 RVA: 0x0008E514 File Offset: 0x0008C714
			public T Current
			{
				get
				{
					if (this._current != null)
					{
						return this._current.Item;
					}
					return default(T);
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x1700087F RID: 2175
			// (get) Token: 0x06002942 RID: 10562 RVA: 0x0008E53E File Offset: 0x0008C73E
			object IEnumerator.Current
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._current.Item;
				}
			}

			// Token: 0x17000880 RID: 2176
			// (get) Token: 0x06002943 RID: 10563 RVA: 0x0008E563 File Offset: 0x0008C763
			internal bool NotStartedOrEnded
			{
				get
				{
					return this._current == null;
				}
			}

			// Token: 0x06002944 RID: 10564 RVA: 0x0008E56E File Offset: 0x0008C76E
			internal void Reset()
			{
				if (this._version != this._tree.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._stack.Clear();
				this.Initialize();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06002945 RID: 10565 RVA: 0x0008E59F File Offset: 0x0008C79F
			void IEnumerator.Reset()
			{
				this.Reset();
			}

			// Token: 0x040015E5 RID: 5605
			private static readonly SortedSet<T>.Node s_dummyNode = new SortedSet<T>.Node(default(T), NodeColor.Red);

			// Token: 0x040015E6 RID: 5606
			private SortedSet<T> _tree;

			// Token: 0x040015E7 RID: 5607
			private int _version;

			// Token: 0x040015E8 RID: 5608
			private Stack<SortedSet<T>.Node> _stack;

			// Token: 0x040015E9 RID: 5609
			private SortedSet<T>.Node _current;

			// Token: 0x040015EA RID: 5610
			private bool _reverse;
		}

		// Token: 0x020004E9 RID: 1257
		internal struct ElementCount
		{
			// Token: 0x040015EB RID: 5611
			internal int UniqueCount;

			// Token: 0x040015EC RID: 5612
			internal int UnfoundCount;
		}
	}
}
