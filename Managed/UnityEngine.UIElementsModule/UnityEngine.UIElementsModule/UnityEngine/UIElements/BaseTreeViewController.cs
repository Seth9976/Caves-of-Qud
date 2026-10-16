using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Hierarchy;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005C RID: 92
	public abstract class BaseTreeViewController : CollectionViewController
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000E863 File Offset: 0x0000CA63
		protected BaseTreeView baseTreeView
		{
			get
			{
				return base.view as BaseTreeView;
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600031F RID: 799 RVA: 0x0000E870 File Offset: 0x0000CA70
		// (remove) Token: 0x06000320 RID: 800 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<TreeViewExpansionChangedArgs> itemExpandedChanged;

		// Token: 0x06000321 RID: 801 RVA: 0x0000E8DD File Offset: 0x0000CADD
		protected BaseTreeViewController()
		{
			this.hierarchy = new Hierarchy();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000E900 File Offset: 0x0000CB00
		~BaseTreeViewController()
		{
			this.DisposeHierarchy();
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000E930 File Offset: 0x0000CB30
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000E938 File Offset: 0x0000CB38
		private protected Hierarchy hierarchy
		{
			get
			{
				return this.m_Hierarchy;
			}
			set
			{
				bool flag = this.hierarchy == value;
				if (!flag)
				{
					this.DisposeHierarchy();
					bool flag2 = value == null;
					if (!flag2)
					{
						this.m_Hierarchy = value;
						this.m_HierarchyFlattened = new HierarchyFlattened(this.m_Hierarchy);
						this.m_HierarchyViewModel = new HierarchyViewModel(this.m_HierarchyFlattened, HierarchyNodeFlags.None);
						this.m_TreeViewDataProperty = this.m_Hierarchy.GetOrCreatePropertyUnmanaged<int>("TreeViewDataProperty", HierarchyPropertyStorageType.Dense);
					}
				}
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		internal void DisposeHierarchy()
		{
			bool flag = this.m_HierarchyViewModel != null;
			if (flag)
			{
				bool isCreated = this.m_HierarchyViewModel.IsCreated;
				if (isCreated)
				{
					this.m_HierarchyViewModel.Dispose();
				}
				this.m_HierarchyViewModel = null;
			}
			bool flag2 = this.m_HierarchyFlattened != null;
			if (flag2)
			{
				bool isCreated2 = this.m_HierarchyFlattened.IsCreated;
				if (isCreated2)
				{
					this.m_HierarchyFlattened.Dispose();
				}
				this.m_HierarchyFlattened = null;
			}
			bool flag3 = this.m_Hierarchy != null;
			if (flag3)
			{
				bool isCreated3 = this.m_Hierarchy.IsCreated;
				if (isCreated3)
				{
					this.m_Hierarchy.Dispose();
				}
				this.m_Hierarchy = null;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000EA51 File Offset: 0x0000CC51
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000EA59 File Offset: 0x0000CC59
		public override IList itemsSource
		{
			get
			{
				return base.itemsSource;
			}
			set
			{
				throw new InvalidOperationException("Can't set itemsSource directly. Override this controller to manage tree data.");
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00002152 File Offset: 0x00000352
		[Obsolete("RebuildTree is no longer supported and will be removed.", false)]
		public void RebuildTree()
		{
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000EA65 File Offset: 0x0000CC65
		public IEnumerable<int> GetRootItemIds()
		{
			foreach (ref HierarchyNode ptr in this.m_Hierarchy.EnumerateChildren(this.m_Hierarchy.Root))
			{
				HierarchyNode node = ptr;
				yield return this.m_TreeViewDataProperty.GetValue(in node);
				node = default(HierarchyNode);
			}
			HierarchyNodeChildren.Enumerator enumerator = default(HierarchyNodeChildren.Enumerator);
			yield break;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000EA75 File Offset: 0x0000CC75
		public virtual IEnumerable<int> GetAllItemIds(IEnumerable<int> rootIds = null)
		{
			bool flag = rootIds == null;
			if (flag)
			{
				foreach (ref HierarchyFlattenedNode ptr in this.m_HierarchyFlattened)
				{
					HierarchyFlattenedNode flattenedNode = ptr;
					HierarchyNode node = flattenedNode.Node;
					bool flag2 = (in node) == this.m_Hierarchy.Root || !this.m_Hierarchy.Exists(in node);
					if (!flag2)
					{
						yield return this.m_TreeViewDataProperty.GetValue(in node);
						node = default(HierarchyNode);
						flattenedNode = default(HierarchyFlattenedNode);
					}
				}
				HierarchyFlattened.Enumerator enumerator = default(HierarchyFlattened.Enumerator);
				yield break;
			}
			foreach (int id in rootIds)
			{
				HierarchyNode parentNode = this.m_IdToNodeDictionary[id];
				bool flag3 = !this.m_Hierarchy.Exists(in parentNode);
				if (!flag3)
				{
					HierarchyFlattenedNodeChildren flattenedNodeChildren = this.m_HierarchyFlattened.EnumerateChildren(in parentNode);
					foreach (ref HierarchyNode ptr2 in flattenedNodeChildren)
					{
						HierarchyNode node2 = ptr2;
						yield return this.m_TreeViewDataProperty.GetValue(in node2);
						node2 = default(HierarchyNode);
					}
					HierarchyFlattenedNodeChildren.Enumerator enumerator3 = default(HierarchyFlattenedNodeChildren.Enumerator);
					yield return id;
					parentNode = default(HierarchyNode);
					flattenedNodeChildren = default(HierarchyFlattenedNodeChildren);
				}
			}
			IEnumerator<int> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000EA8C File Offset: 0x0000CC8C
		public virtual int GetParentId(int id)
		{
			HierarchyNode hierarchyNodeById = this.GetHierarchyNodeById(id);
			bool flag = (in hierarchyNodeById) == HierarchyNode.Null || !this.m_Hierarchy.Exists(in hierarchyNodeById);
			int num;
			if (flag)
			{
				num = BaseTreeView.invalidId;
			}
			else
			{
				HierarchyNode parent = this.m_Hierarchy.GetParent(in hierarchyNodeById);
				bool flag2 = (in parent) == this.m_Hierarchy.Root;
				if (flag2)
				{
					num = BaseTreeView.invalidId;
				}
				else
				{
					num = this.m_TreeViewDataProperty.GetValue(in parent);
				}
			}
			return num;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000EB0E File Offset: 0x0000CD0E
		public virtual IEnumerable<int> GetChildrenIds(int id)
		{
			HierarchyNode nodeById = this.GetHierarchyNodeById(id);
			bool flag = (in nodeById) == HierarchyNode.Null || !this.m_Hierarchy.Exists(in nodeById);
			if (flag)
			{
				yield break;
			}
			foreach (ref HierarchyNode ptr in this.m_Hierarchy.EnumerateChildren(in nodeById))
			{
				HierarchyNode node = ptr;
				yield return this.m_TreeViewDataProperty.GetValue(in node);
				node = default(HierarchyNode);
			}
			HierarchyNodeChildren.Enumerator enumerator = default(HierarchyNodeChildren.Enumerator);
			yield break;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public unsafe virtual void Move(int id, int newParentId, int childIndex = -1, bool rebuildTree = true)
		{
			bool flag = id == newParentId;
			if (!flag)
			{
				bool flag2 = this.IsChildOf(newParentId, id);
				if (!flag2)
				{
					HierarchyNode hierarchyNode;
					bool flag3 = !this.m_IdToNodeDictionary.TryGetValue(id, out hierarchyNode);
					if (!flag3)
					{
						HierarchyNode hierarchyNode2 = ((newParentId == BaseTreeView.invalidId) ? (*this.m_Hierarchy.Root) : this.GetHierarchyNodeById(newParentId));
						HierarchyNode parent = this.m_Hierarchy.GetParent(in hierarchyNode);
						bool flag4 = (in parent) == (in hierarchyNode2);
						if (flag4)
						{
							int childIndexForId = this.GetChildIndexForId(id);
							bool flag5 = childIndexForId < childIndex;
							if (flag5)
							{
								childIndex--;
							}
						}
						else
						{
							this.m_Hierarchy.SetParent(in hierarchyNode, in hierarchyNode2);
						}
						this.UpdateSortOrder(in hierarchyNode2, in hierarchyNode, childIndex);
						if (rebuildTree)
						{
							this.RaiseItemParentChanged(id, newParentId);
						}
					}
				}
			}
		}

		// Token: 0x0600032E RID: 814
		public abstract bool TryRemoveItem(int id, bool rebuildTree = true);

		// Token: 0x0600032F RID: 815 RVA: 0x0000EC00 File Offset: 0x0000CE00
		internal override void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			ReusableTreeViewItem reusableTreeViewItem = reusableItem as ReusableTreeViewItem;
			bool flag = reusableTreeViewItem != null;
			if (flag)
			{
				reusableTreeViewItem.Init(this.MakeItem());
				this.PostInitRegistration(reusableTreeViewItem);
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000EC34 File Offset: 0x0000CE34
		internal override void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			ReusableTreeViewItem reusableTreeViewItem = reusableItem as ReusableTreeViewItem;
			bool flag = reusableTreeViewItem != null;
			if (flag)
			{
				reusableTreeViewItem.customIndentWidth = this.baseTreeView.customIdent;
				reusableTreeViewItem.Indent(this.GetIndentationDepthByIndex(index));
				reusableTreeViewItem.SetExpandedWithoutNotify(this.IsExpandedByIndex(index));
				reusableTreeViewItem.SetToggleVisibility(this.HasChildrenByIndex(index));
			}
			base.InvokeBindItem(reusableItem, index);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000EC98 File Offset: 0x0000CE98
		internal override void InvokeDestroyItem(ReusableCollectionItem reusableItem)
		{
			ReusableTreeViewItem reusableTreeViewItem = reusableItem as ReusableTreeViewItem;
			bool flag = reusableTreeViewItem != null;
			if (flag)
			{
				reusableTreeViewItem.onPointerUp -= this.OnItemPointerUp;
				reusableTreeViewItem.onToggleValueChanged -= this.OnToggleValueChanged;
			}
			base.InvokeDestroyItem(reusableItem);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000ECE5 File Offset: 0x0000CEE5
		internal void PostInitRegistration(ReusableTreeViewItem treeItem)
		{
			treeItem.onPointerUp += this.OnItemPointerUp;
			treeItem.onToggleValueChanged += this.OnToggleValueChanged;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000ED10 File Offset: 0x0000CF10
		private void OnItemPointerUp(PointerUpEvent evt)
		{
			bool flag = (evt.modifiers & EventModifiers.Alt) == EventModifiers.None;
			if (!flag)
			{
				VisualElement visualElement = evt.currentTarget as VisualElement;
				Toggle toggle = visualElement.Q<Toggle>(BaseTreeView.itemToggleUssClassName, null);
				int index = ((ReusableTreeViewItem)toggle.userData).index;
				bool flag2 = !this.HasChildrenByIndex(index);
				if (!flag2)
				{
					bool flag3 = this.IsExpandedByIndex(index);
					bool flag4 = this.IsViewDataKeyEnabled();
					if (flag4)
					{
						int idForIndex = this.GetIdForIndex(index);
						HashSet<int> hashSet = new HashSet<int>(this.baseTreeView.expandedItemIds);
						bool flag5 = flag3;
						if (flag5)
						{
							hashSet.Remove(idForIndex);
						}
						else
						{
							hashSet.Add(idForIndex);
						}
						IEnumerable<int> childrenIdsByIndex = this.GetChildrenIdsByIndex(index);
						foreach (int num in this.GetAllItemIds(childrenIdsByIndex))
						{
							bool flag6 = this.HasChildren(num);
							if (flag6)
							{
								bool flag7 = flag3;
								if (flag7)
								{
									hashSet.Remove(num);
								}
								else
								{
									hashSet.Add(num);
								}
							}
						}
						this.baseTreeView.expandedItemIds = new List<int>(hashSet);
					}
					bool flag8 = flag3;
					if (flag8)
					{
						HierarchyViewModel hierarchyViewModel = this.m_HierarchyViewModel;
						HierarchyNode hierarchyNode = this.GetHierarchyNodeByIndex(index);
						hierarchyViewModel.ClearFlags(in hierarchyNode, HierarchyNodeFlags.Expanded, true);
					}
					else
					{
						HierarchyViewModel hierarchyViewModel2 = this.m_HierarchyViewModel;
						HierarchyNode hierarchyNode = this.GetHierarchyNodeByIndex(index);
						hierarchyViewModel2.SetFlags(in hierarchyNode, HierarchyNodeFlags.Expanded, true);
					}
					this.UpdateHierarchy();
					this.baseTreeView.RefreshItems();
					this.RaiseItemExpandedChanged(this.GetIdForIndex(index), !flag3, true);
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		private void RaiseItemExpandedChanged(int id, bool isExpanded, bool isAppliedToAllChildren)
		{
			Action<TreeViewExpansionChangedArgs> action = this.itemExpandedChanged;
			if (action != null)
			{
				action(new TreeViewExpansionChangedArgs
				{
					id = id,
					isExpanded = isExpanded,
					isAppliedToAllChildren = isAppliedToAllChildren
				});
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		private void OnToggleValueChanged(ChangeEvent<bool> evt)
		{
			Toggle toggle = evt.target as Toggle;
			int index = ((ReusableTreeViewItem)toggle.userData).index;
			bool flag = this.IsExpandedByIndex(index);
			bool flag2 = flag;
			if (flag2)
			{
				this.CollapseItemByIndex(index, false, true);
			}
			else
			{
				this.ExpandItemByIndex(index, false, true);
			}
			this.baseTreeView.scrollView.contentContainer.Focus();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000EF54 File Offset: 0x0000D154
		public virtual int GetTreeItemsCount()
		{
			return this.m_Hierarchy.Count;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000EF74 File Offset: 0x0000D174
		public override int GetIndexForId(int id)
		{
			HierarchyNode hierarchyNode;
			return this.m_IdToNodeDictionary.TryGetValue(id, out hierarchyNode) ? this.m_HierarchyViewModel.IndexOf(in hierarchyNode) : BaseTreeView.invalidId;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		public override int GetIdForIndex(int index)
		{
			int count = this.m_HierarchyViewModel.Count;
			bool flag = index == count && count > 0;
			int num;
			if (flag)
			{
				IHierarchyProperty<int> treeViewDataProperty = this.m_TreeViewDataProperty;
				HierarchyViewModel hierarchyViewModel = this.m_HierarchyViewModel;
				num = treeViewDataProperty.GetValue(hierarchyViewModel[hierarchyViewModel.Count - 1]);
			}
			else
			{
				num = ((!this.IsIndexValid(index)) ? BaseTreeView.invalidId : this.m_TreeViewDataProperty.GetValue(this.m_HierarchyViewModel[index]));
			}
			return num;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F024 File Offset: 0x0000D224
		public virtual bool HasChildren(int id)
		{
			HierarchyNode hierarchyNode;
			bool flag = this.m_IdToNodeDictionary.TryGetValue(id, out hierarchyNode) && this.m_Hierarchy.Exists(in hierarchyNode);
			return flag && this.m_Hierarchy.GetChildrenCount(in hierarchyNode) > 0;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F070 File Offset: 0x0000D270
		public bool Exists(int id)
		{
			return this.m_IdToNodeDictionary.ContainsKey(id);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000F090 File Offset: 0x0000D290
		public bool HasChildrenByIndex(int index)
		{
			bool flag = !this.IsIndexValid(index);
			return !flag && this.HasChildren(this.GetIdForIndex(index));
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000F0C1 File Offset: 0x0000D2C1
		public IEnumerable<int> GetChildrenIdsByIndex(int index)
		{
			bool flag = !this.IsIndexValid(index);
			if (flag)
			{
				yield break;
			}
			foreach (ref HierarchyNode ptr in this.m_Hierarchy.EnumerateChildren(this.m_HierarchyViewModel[index]))
			{
				HierarchyNode node = ptr;
				yield return this.m_TreeViewDataProperty.GetValue(in node);
				node = default(HierarchyNode);
			}
			HierarchyNodeChildren.Enumerator enumerator = default(HierarchyNodeChildren.Enumerator);
			yield break;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
		public int GetChildIndexForId(int id)
		{
			HierarchyNode hierarchyNode;
			bool flag = this.m_IdToNodeDictionary.TryGetValue(id, out hierarchyNode);
			int num;
			if (flag)
			{
				HierarchyNode parent = this.m_Hierarchy.GetParent(in hierarchyNode);
				bool flag2 = (in parent) == HierarchyNode.Null;
				if (flag2)
				{
					num = BaseTreeView.invalidId;
				}
				else
				{
					HierarchyNodeChildren hierarchyNodeChildren = this.m_Hierarchy.EnumerateChildren(in parent);
					int num2 = 0;
					foreach (ref HierarchyNode ptr in hierarchyNodeChildren)
					{
						HierarchyNode hierarchyNode2 = ptr;
						bool flag3 = (in hierarchyNode2) == (in hierarchyNode);
						if (flag3)
						{
							break;
						}
						num2++;
					}
					num = num2;
				}
			}
			else
			{
				num = BaseTreeView.invalidId;
			}
			return num;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000F180 File Offset: 0x0000D380
		public int GetIndentationDepth(int id)
		{
			int num = 0;
			int num2 = this.GetParentId(id);
			while (num2 != BaseTreeView.invalidId)
			{
				num2 = this.GetParentId(num2);
				num++;
			}
			return num;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000F1BC File Offset: 0x0000D3BC
		public int GetIndentationDepthByIndex(int index)
		{
			int idForIndex = this.GetIdForIndex(index);
			return this.GetIndentationDepth(idForIndex);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
		public virtual bool CanChangeExpandedState(int id)
		{
			return true;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
		public bool IsExpanded(int id)
		{
			if (this.m_IdToNodeDictionary.ContainsKey(id))
			{
				Hierarchy hierarchy = this.m_Hierarchy;
				HierarchyNode hierarchyNode = this.m_IdToNodeDictionary[id];
				if (hierarchy.Exists(in hierarchyNode))
				{
					HierarchyViewModel hierarchyViewModel = this.m_HierarchyViewModel;
					HierarchyNode hierarchyNode2 = this.m_IdToNodeDictionary[id];
					return hierarchyViewModel.HasAllFlags(in hierarchyNode2, HierarchyNodeFlags.Expanded);
				}
			}
			return false;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000F250 File Offset: 0x0000D450
		public bool IsExpandedByIndex(int index)
		{
			bool flag = !this.IsIndexValid(index);
			return !flag && this.IsExpanded(this.GetIdForIndex(index));
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000F284 File Offset: 0x0000D484
		public void ExpandItemByIndex(int index, bool expandAllChildren, bool refresh = true)
		{
			using (BaseTreeViewController.K_ExpandItemByIndex.Auto())
			{
				bool flag = !this.HasChildrenByIndex(index);
				if (!flag)
				{
					HierarchyNode hierarchyNodeById = this.GetHierarchyNodeById(this.GetIdForIndex(index));
					this.ExpandItemByNode(in hierarchyNodeById, expandAllChildren, refresh);
				}
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		public void ExpandItem(int id, bool expandAllChildren, bool refresh = true)
		{
			bool flag = !this.HasChildren(id) || !this.CanChangeExpandedState(id);
			if (!flag)
			{
				HierarchyNode hierarchyNode;
				bool flag2 = this.m_IdToNodeDictionary.TryGetValue(id, out hierarchyNode);
				if (flag2)
				{
					this.ExpandItemByNode(in hierarchyNode, expandAllChildren, refresh);
				}
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F334 File Offset: 0x0000D534
		public void CollapseItemByIndex(int index, bool collapseAllChildren, bool refresh = true)
		{
			bool flag = !this.HasChildrenByIndex(index);
			if (!flag)
			{
				HierarchyNode hierarchyNodeById = this.GetHierarchyNodeById(this.GetIdForIndex(index));
				this.CollapseItemByNode(in hierarchyNodeById, collapseAllChildren, refresh);
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F36C File Offset: 0x0000D56C
		public void CollapseItem(int id, bool collapseAllChildren, bool refresh = true)
		{
			bool flag = !this.HasChildren(id) || !this.CanChangeExpandedState(id);
			if (!flag)
			{
				HierarchyNode hierarchyNode;
				bool flag2 = this.m_IdToNodeDictionary.TryGetValue(id, out hierarchyNode);
				if (flag2)
				{
					this.CollapseItemByNode(in hierarchyNode, collapseAllChildren, refresh);
				}
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		public void ExpandAll()
		{
			this.m_HierarchyViewModel.SetFlags(HierarchyNodeFlags.Expanded);
			this.UpdateHierarchy();
			bool flag = this.IsViewDataKeyEnabled();
			if (flag)
			{
				this.baseTreeView.expandedItemIds.Clear();
				foreach (ref HierarchyNode ptr in this.m_HierarchyViewModel.EnumerateNodesWithAllFlags(HierarchyNodeFlags.Expanded))
				{
					HierarchyNode hierarchyNode = ptr;
					this.baseTreeView.expandedItemIds.Add(this.m_TreeViewDataProperty.GetValue(in hierarchyNode));
				}
				this.baseTreeView.SaveViewData();
			}
			this.baseTreeView.RefreshItems();
			this.RaiseItemExpandedChanged(-1, true, true);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000F464 File Offset: 0x0000D664
		public void CollapseAll()
		{
			this.m_HierarchyViewModel.ClearFlags(HierarchyNodeFlags.Expanded);
			this.UpdateHierarchy();
			bool flag = this.IsViewDataKeyEnabled();
			if (flag)
			{
				this.baseTreeView.expandedItemIds.Clear();
				this.baseTreeView.SaveViewData();
			}
			this.baseTreeView.RefreshItems();
			this.RaiseItemExpandedChanged(-1, false, true);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		private void ExpandItemByNode(in HierarchyNode node, bool expandAllChildren, bool refresh)
		{
			int value = this.m_TreeViewDataProperty.GetValue(in node);
			bool flag = !this.CanChangeExpandedState(value);
			if (!flag)
			{
				this.m_HierarchyViewModel.SetFlags(in node, HierarchyNodeFlags.Expanded, expandAllChildren);
				this.m_HierarchyHasPendingChanged = true;
				bool flag2 = this.IsViewDataKeyEnabled();
				if (flag2)
				{
					HashSet<int> hashSet = new HashSet<int>(this.baseTreeView.expandedItemIds) { value };
					if (expandAllChildren)
					{
						this.UpdateHierarchy();
						IEnumerable<int> childrenIds = this.GetChildrenIds(value);
						foreach (int num in this.GetAllItemIds(childrenIds))
						{
							hashSet.Add(num);
						}
					}
					this.baseTreeView.expandedItemIds.Clear();
					this.baseTreeView.expandedItemIds.AddRange(hashSet);
					this.baseTreeView.SaveViewData();
				}
				if (refresh)
				{
					this.baseTreeView.RefreshItems();
				}
				this.RaiseItemExpandedChanged(value, true, expandAllChildren);
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		private void CollapseItemByNode(in HierarchyNode node, bool collapseAllChildren, bool refresh)
		{
			int value = this.m_TreeViewDataProperty.GetValue(in node);
			bool flag = !this.CanChangeExpandedState(value);
			if (!flag)
			{
				bool flag2 = this.IsViewDataKeyEnabled();
				if (flag2)
				{
					if (collapseAllChildren)
					{
						IEnumerable<int> childrenIds = this.GetChildrenIds(value);
						foreach (int num in this.GetAllItemIds(childrenIds))
						{
							this.baseTreeView.expandedItemIds.Remove(num);
						}
					}
					this.baseTreeView.expandedItemIds.Remove(value);
					this.baseTreeView.SaveViewData();
				}
				this.m_HierarchyViewModel.ClearFlags(in node, HierarchyNodeFlags.Expanded, collapseAllChildren);
				this.m_HierarchyHasPendingChanged = true;
				if (refresh)
				{
					this.baseTreeView.RefreshItems();
				}
				this.RaiseItemExpandedChanged(value, false, collapseAllChildren);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void GetExpandedItemIds(List<int> list)
		{
			bool flag = list.Count > 0;
			if (flag)
			{
				list.Clear();
			}
			bool flag2 = this.IsViewDataKeyEnabled();
			if (flag2)
			{
				list.AddRange(this.baseTreeView.expandedItemIds);
			}
			foreach (ref HierarchyNode ptr in this.m_HierarchyViewModel.EnumerateNodesWithAllFlags(HierarchyNodeFlags.Expanded))
			{
				HierarchyNode hierarchyNode = ptr;
				list.Add(this.m_TreeViewDataProperty.GetValue(in hierarchyNode));
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000F760 File Offset: 0x0000D960
		internal bool IsViewDataKeyEnabled()
		{
			return this.baseTreeView.enableViewDataPersistence && !string.IsNullOrEmpty(this.baseTreeView.viewDataKey);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000F798 File Offset: 0x0000D998
		internal void ExpandAncestorNodes(in HierarchyNode node)
		{
			HierarchyNode hierarchyNode = this.m_Hierarchy.GetParent(in node);
			for (;;)
			{
				bool flag;
				if ((in hierarchyNode) != this.m_Hierarchy.Root)
				{
					HierarchyNode hierarchyNodeById;
					hierarchyNode = (hierarchyNodeById = this.GetHierarchyNodeById(this.m_TreeViewDataProperty.GetValue(in hierarchyNode)));
					flag = (in hierarchyNodeById) != this.m_Hierarchy.Root;
				}
				else
				{
					flag = false;
				}
				if (!flag)
				{
					break;
				}
				int value = this.m_TreeViewDataProperty.GetValue(in hierarchyNode);
				bool flag2 = !this.m_HierarchyViewModel.HasAllFlags(in hierarchyNode, HierarchyNodeFlags.Expanded) && this.CanChangeExpandedState(value);
				if (flag2)
				{
					bool flag3 = this.IsViewDataKeyEnabled();
					if (flag3)
					{
						this.baseTreeView.expandedItemIds.Add(value);
					}
					this.m_HierarchyViewModel.SetFlags(in hierarchyNode, HierarchyNodeFlags.Expanded, false);
					this.m_HierarchyViewModel.Update();
				}
				hierarchyNode = this.m_Hierarchy.GetParent(in hierarchyNode);
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000F874 File Offset: 0x0000DA74
		internal override void PreRefresh()
		{
			bool flag = !this.m_HierarchyHasPendingChanged;
			if (!flag)
			{
				this.UpdateHierarchy();
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000F898 File Offset: 0x0000DA98
		private bool IsIndexValid(int index)
		{
			return index >= 0 && index < this.m_HierarchyViewModel.Count;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		private bool IsChildOf(int childId, int id)
		{
			bool flag = childId == BaseTreeView.invalidId || id == BaseTreeView.invalidId;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				HierarchyNode hierarchyNode = this.GetHierarchyNodeById(childId);
				HierarchyNode hierarchyNodeById = this.GetHierarchyNodeById(id);
				bool flag3 = (in hierarchyNodeById) == (in hierarchyNode);
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					for (;;)
					{
						HierarchyNode parent;
						HierarchyNode hierarchyNode2 = (parent = this.m_Hierarchy.GetParent(in hierarchyNode));
						if (!((in parent) != this.m_Hierarchy.Root))
						{
							goto Block_5;
						}
						bool flag4 = (in hierarchyNodeById) == (in hierarchyNode2);
						if (flag4)
						{
							break;
						}
						hierarchyNode = hierarchyNode2;
					}
					return true;
					Block_5:
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000F958 File Offset: 0x0000DB58
		internal void RaiseItemParentChanged(int id, int newParentId)
		{
			base.RaiseItemIndexChanged(id, newParentId);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000F964 File Offset: 0x0000DB64
		internal unsafe HierarchyNode CreateNode(in HierarchyNode parent)
		{
			Hierarchy hierarchy = this.m_Hierarchy;
			HierarchyNode hierarchyNode = (((in parent) == HierarchyNode.Null) ? (*this.m_Hierarchy.Root) : parent);
			return hierarchy.Add(in hierarchyNode);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000F9AC File Offset: 0x0000DBAC
		internal void UpdateIdToNodeDictionary(int id, in HierarchyNode node, bool isAdd = true)
		{
			this.m_HierarchyHasPendingChanged = true;
			if (isAdd)
			{
				this.m_TreeViewDataProperty.SetValue(in node, id);
				this.m_IdToNodeDictionary[id] = node;
			}
			else
			{
				this.m_IdToNodeDictionary.Remove(id);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		internal unsafe void RemoveAllChildrenItemsFromCollections(in HierarchyNode node, Action<HierarchyNode, int> removeCallback)
		{
			bool flag = (in node) == HierarchyNode.Null;
			if (!flag)
			{
				int num = this.m_HierarchyFlattened.IndexOf(in node);
				bool flag2 = num == -1;
				if (!flag2)
				{
					int num2 = num + 1;
					int childrenCountRecursive = this.m_HierarchyFlattened.GetChildrenCountRecursive(in node);
					for (int i = num2; i < num2 + childrenCountRecursive; i++)
					{
						HierarchyFlattenedNode hierarchyFlattenedNode = *this.m_HierarchyFlattened[i];
						HierarchyNode node2 = hierarchyFlattenedNode.Node;
						IHierarchyProperty<int> treeViewDataProperty = this.m_TreeViewDataProperty;
						HierarchyNode node3 = hierarchyFlattenedNode.Node;
						removeCallback(node2, treeViewDataProperty.GetValue(in node3));
					}
				}
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000FA93 File Offset: 0x0000DC93
		internal void ClearIdToNodeDictionary()
		{
			this.m_IdToNodeDictionary.Clear();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		internal unsafe void UpdateSortOrder(in HierarchyNode newParent, in HierarchyNode insertedNode, int insertedIndex)
		{
			Span<HierarchyNode> span = this.m_Hierarchy.GetChildren(in newParent);
			bool flag = insertedIndex == -1;
			if (flag)
			{
				insertedIndex = span.Length;
			}
			int num = 0;
			int num2 = 0;
			while (num2 < insertedIndex && num2 < span.Length)
			{
				bool flag2 = (in insertedNode) == span[num2];
				if (!flag2)
				{
					this.m_Hierarchy.SetSortIndex(span[num2], num++);
				}
				num2++;
			}
			this.m_Hierarchy.SetSortIndex(in insertedNode, insertedIndex);
			bool flag3 = insertedIndex == num;
			if (flag3)
			{
				num++;
			}
			for (int i = insertedIndex; i < span.Length; i++)
			{
				bool flag4 = (in insertedNode) == span[i];
				if (!flag4)
				{
					this.m_Hierarchy.SetSortIndex(span[i], num++);
				}
			}
			this.m_Hierarchy.SortChildren(in newParent, false);
			this.UpdateHierarchy();
			Span<HierarchyNode> span2 = this.m_Hierarchy.GetChildren(in newParent);
			Span<HierarchyNode> span3 = span2;
			for (int j = 0; j < span3.Length; j++)
			{
				HierarchyNode hierarchyNode = *span3[j];
				this.m_Hierarchy.SetSortIndex(in hierarchyNode, 0);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		internal void OnViewDataReadyUpdateNodes()
		{
			foreach (int num in this.baseTreeView.expandedItemIds)
			{
				HierarchyNode hierarchyNode;
				bool flag = !this.m_IdToNodeDictionary.TryGetValue(num, out hierarchyNode);
				if (!flag)
				{
					this.m_HierarchyViewModel.SetFlags(in hierarchyNode, HierarchyNodeFlags.Expanded, false);
				}
			}
			this.UpdateHierarchy();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000FC80 File Offset: 0x0000DE80
		internal void UpdateHierarchy()
		{
			bool updateNeeded = this.m_Hierarchy.UpdateNeeded;
			if (updateNeeded)
			{
				this.m_Hierarchy.Update();
			}
			bool updateNeeded2 = this.m_HierarchyFlattened.UpdateNeeded;
			if (updateNeeded2)
			{
				this.m_HierarchyFlattened.Update();
			}
			bool updateNeeded3 = this.m_HierarchyViewModel.UpdateNeeded;
			if (updateNeeded3)
			{
				this.m_HierarchyViewModel.Update();
			}
			this.m_HierarchyHasPendingChanged = false;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000FCEC File Offset: 0x0000DEEC
		internal unsafe HierarchyNode GetHierarchyNodeById(int id)
		{
			HierarchyNode hierarchyNode;
			return (this.m_IdToNodeDictionary.TryGetValue(id, out hierarchyNode) && this.m_Hierarchy.Exists(in hierarchyNode)) ? hierarchyNode : (*HierarchyNode.Null);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		internal unsafe HierarchyNode GetHierarchyNodeByIndex(int index)
		{
			bool flag = !this.IsIndexValid(index);
			HierarchyNode hierarchyNode;
			if (flag)
			{
				hierarchyNode = *HierarchyNode.Null;
			}
			else
			{
				hierarchyNode = *this.m_HierarchyViewModel[index];
			}
			return hierarchyNode;
		}

		// Token: 0x040001B9 RID: 441
		private protected Hierarchy m_Hierarchy;

		// Token: 0x040001BA RID: 442
		private protected HierarchyFlattened m_HierarchyFlattened;

		// Token: 0x040001BB RID: 443
		private protected HierarchyViewModel m_HierarchyViewModel;

		// Token: 0x040001BC RID: 444
		private protected Dictionary<int, HierarchyNode> m_IdToNodeDictionary = new Dictionary<int, HierarchyNode>();

		// Token: 0x040001BD RID: 445
		private const string k_HierarchyPropertyName = "TreeViewDataProperty";

		// Token: 0x040001BE RID: 446
		private IHierarchyProperty<int> m_TreeViewDataProperty;

		// Token: 0x040001BF RID: 447
		private bool m_HierarchyHasPendingChanged;

		// Token: 0x040001C1 RID: 449
		private static readonly ProfilerMarker K_ExpandItemByIndex = new ProfilerMarker(ProfilerCategory.Scripts, "BaseTreeViewController.ExpandItemByIndex");
	}
}
