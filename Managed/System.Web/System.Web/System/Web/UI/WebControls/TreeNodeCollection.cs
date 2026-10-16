using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200042A RID: 1066
	public sealed class TreeNodeCollection : ICollection, IEnumerable, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> class using the default values.</summary>
		// Token: 0x060030A2 RID: 12450 RVA: 0x0007E072 File Offset: 0x0007C272
		public TreeNodeCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> class using the specified parent node (or owner).</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object that represents the parent node of the collection. </param>
		// Token: 0x060030A3 RID: 12451 RVA: 0x0007E085 File Offset: 0x0007C285
		public TreeNodeCollection(TreeNode owner)
		{
			this.parent = owner;
			this.tree = owner.Tree;
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x0007E0AB File Offset: 0x0007C2AB
		internal TreeNodeCollection(TreeView tree)
		{
			this.tree = tree;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x0007E0C8 File Offset: 0x0007C2C8
		internal void SetTree(TreeView tree)
		{
			this.tree = tree;
			foreach (object obj in this.items)
			{
				((TreeNode)obj).Tree = tree;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to retrieve. </param>
		// Token: 0x17000F86 RID: 3974
		public TreeNode this[int index]
		{
			get
			{
				return (TreeNode)this.items[index];
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <param name="child">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to append. </param>
		// Token: 0x060030A7 RID: 12455 RVA: 0x0007E13B File Offset: 0x0007C33B
		public void Add(TreeNode child)
		{
			this.Add(child, true);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x0007E148 File Offset: 0x0007C348
		internal void Add(TreeNode child, bool updateParent)
		{
			int num = this.items.Add(child);
			if (this.parent != null)
			{
				this.parent.HadChildrenBeforePopulating = true;
			}
			if (!updateParent)
			{
				return;
			}
			child.Index = num;
			child.SetParent(this.parent);
			child.Tree = this.tree;
			if (this.marked)
			{
				((IStateManager)child).TrackViewState();
				this.SetDirty();
			}
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object in a <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object. </param>
		/// <param name="child">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="child" /> is null.</exception>
		// Token: 0x060030A9 RID: 12457 RVA: 0x0007E1B0 File Offset: 0x0007C3B0
		public void AddAt(int index, TreeNode child)
		{
			this.items.Insert(index, child);
			child.Index = index;
			child.SetParent(this.parent);
			child.Tree = this.tree;
			for (int i = index + 1; i < this.items.Count; i++)
			{
				((TreeNode)this.items[i]).Index = i;
			}
			if (this.marked)
			{
				((IStateManager)child).TrackViewState();
				this.SetDirty();
			}
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x0007E22C File Offset: 0x0007C42C
		internal void SetDirty()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].SetDirty();
			}
			this.dirty = true;
		}

		/// <summary>Empties the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		// Token: 0x060030AB RID: 12459 RVA: 0x0007E260 File Offset: 0x0007C460
		public void Clear()
		{
			if (this.tree != null || this.parent != null)
			{
				foreach (object obj in this.items)
				{
					TreeNode treeNode = (TreeNode)obj;
					treeNode.Tree = null;
					treeNode.SetParent(null);
				}
			}
			this.items.Clear();
			if (this.marked)
			{
				this.dirty = true;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object is in the collection.</summary>
		/// <returns>true if the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object is contained in the collection; otherwise, false.</returns>
		/// <param name="c">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to find. </param>
		// Token: 0x060030AC RID: 12460 RVA: 0x0007E2E8 File Offset: 0x0007C4E8
		public bool Contains(TreeNode c)
		{
			return this.items.Contains(c);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="nodeArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x060030AD RID: 12461 RVA: 0x0007E2F6 File Offset: 0x0007C4F6
		public void CopyTo(TreeNode[] nodeArray, int index)
		{
			this.items.CopyTo(nodeArray, index);
		}

		/// <summary>Returns an enumerator that can be used to iterate through a <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <returns>An enumerator that can be used to iterate through the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
		// Token: 0x060030AE RID: 12462 RVA: 0x0007E305 File Offset: 0x0007C505
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to locate. </param>
		// Token: 0x060030AF RID: 12463 RVA: 0x0007E312 File Offset: 0x0007C512
		public int IndexOf(TreeNode value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060030B0 RID: 12464 RVA: 0x0007E320 File Offset: 0x0007C520
		public void Remove(TreeNode value)
		{
			int num = this.IndexOf(value);
			if (num == -1)
			{
				return;
			}
			this.items.RemoveAt(num);
			if (this.tree != null)
			{
				value.Tree = null;
			}
			if (this.marked)
			{
				this.SetDirty();
			}
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <param name="index">The zero-based index location of the node to remove. </param>
		// Token: 0x060030B1 RID: 12465 RVA: 0x0007E364 File Offset: 0x0007C564
		public void RemoveAt(int index)
		{
			TreeNode treeNode = (TreeNode)this.items[index];
			this.items.RemoveAt(index);
			if (this.tree != null)
			{
				treeNode.Tree = null;
			}
			if (this.marked)
			{
				this.SetDirty();
			}
		}

		/// <summary>Gets the number of items in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <returns>The number of items in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x0007E3AC File Offset: 0x0007C5AC
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> is synchronized (thread safe).</summary>
		/// <returns>false.</returns>
		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x0007E3B9 File Offset: 0x0007C5B9
		public object SyncRoot
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> object that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />. </param>
		/// <param name="index">The position in the target array at which to start receiving the copied content. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not an array of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects.</exception>
		// Token: 0x060030B5 RID: 12469 RVA: 0x0007E2F6 File Offset: 0x0007C4F6
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Loads the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object's previously saved view state.</summary>
		/// <param name="state">A <see cref="T:System.Object" /> that contains the saved view state values. </param>
		// Token: 0x060030B6 RID: 12470 RVA: 0x0007E3C4 File Offset: 0x0007C5C4
		void IStateManager.LoadViewState(object state)
		{
			if (state == null)
			{
				return;
			}
			object[] array = (object[])state;
			this.dirty = (bool)array[0];
			if (this.dirty)
			{
				this.items.Clear();
				for (int i = 1; i < array.Length; i++)
				{
					Pair pair = array[i] as Pair;
					if (pair == null)
					{
						throw new InvalidOperationException("Broken view state (item " + i.ToString() + ")");
					}
					TreeNode treeNode;
					if (pair.First as Type == null)
					{
						treeNode = new TreeNode();
					}
					else
					{
						treeNode = Activator.CreateInstance(pair.First as Type) as TreeNode;
					}
					this.Add(treeNode);
					object second = pair.Second;
					if (second != null)
					{
						((IStateManager)treeNode).LoadViewState(second);
					}
				}
				return;
			}
			for (int j = 1; j < array.Length; j++)
			{
				Pair pair2 = array[j] as Pair;
				if (pair2 == null)
				{
					throw new InvalidOperationException("Broken view state " + j.ToString() + ")");
				}
				int num = (int)pair2.First;
				((IStateManager)((TreeNode)this.items[num])).LoadViewState(pair2.Second);
			}
		}

		/// <summary>Saves the changes to view state to a <see cref="T:System.Object" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x060030B7 RID: 12471 RVA: 0x0007E4EC File Offset: 0x0007C6EC
		object IStateManager.SaveViewState()
		{
			object[] array = null;
			bool flag = false;
			if (this.dirty)
			{
				if (this.items.Count > 0)
				{
					flag = true;
					array = new object[this.items.Count + 1];
					array[0] = true;
					for (int i = 0; i < this.items.Count; i++)
					{
						TreeNode treeNode = this.items[i] as TreeNode;
						object obj = ((IStateManager)treeNode).SaveViewState();
						Type type = treeNode.GetType();
						array[i + 1] = new Pair((type == typeof(TreeNode)) ? null : type, obj);
					}
				}
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				for (int j = 0; j < this.items.Count; j++)
				{
					object obj2 = ((IStateManager)(this.items[j] as TreeNode)).SaveViewState();
					if (obj2 != null)
					{
						flag = true;
						arrayList.Add(new Pair(j, obj2));
					}
				}
				if (flag)
				{
					arrayList.Insert(0, false);
					array = arrayList.ToArray();
				}
			}
			if (flag)
			{
				return array;
			}
			return null;
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> to track changes to its view state.</summary>
		// Token: 0x060030B8 RID: 12472 RVA: 0x0007E608 File Offset: 0x0007C808
		void IStateManager.TrackViewState()
		{
			this.marked = true;
			for (int i = 0; i < this.items.Count; i++)
			{
				((IStateManager)this.items[i]).TrackViewState();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object is saving changes to its view state.</summary>
		/// <returns>true if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x0007E648 File Offset: 0x0007C848
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x04001C12 RID: 7186
		private ArrayList items = new ArrayList();

		// Token: 0x04001C13 RID: 7187
		private TreeView tree;

		// Token: 0x04001C14 RID: 7188
		private TreeNode parent;

		// Token: 0x04001C15 RID: 7189
		private bool marked;

		// Token: 0x04001C16 RID: 7190
		private bool dirty;
	}
}
