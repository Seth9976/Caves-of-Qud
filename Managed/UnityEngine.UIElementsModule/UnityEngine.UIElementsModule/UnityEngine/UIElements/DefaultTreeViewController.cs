using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;

namespace UnityEngine.UIElements
{
	// Token: 0x02000065 RID: 101
	public class DefaultTreeViewController<T> : TreeViewController, IDefaultTreeViewController<T>
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00010E40 File Offset: 0x0000F040
		private TreeDataController<T> treeDataController
		{
			get
			{
				TreeDataController<T> treeDataController;
				if ((treeDataController = this.m_TreeDataController) == null)
				{
					treeDataController = (this.m_TreeDataController = new TreeDataController<T>());
				}
				return treeDataController;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0001093E File Offset: 0x0000EB3E
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x00010E68 File Offset: 0x0000F068
		public override IList itemsSource
		{
			get
			{
				return base.itemsSource;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.SetRootItems(null);
				}
				else
				{
					IList<TreeViewItemData<T>> list = value as IList<TreeViewItemData<T>>;
					bool flag2 = list != null;
					if (flag2)
					{
						this.SetRootItems(list);
					}
					else
					{
						Debug.LogError(string.Format("Type does not match this tree view controller's data type ({0}).", typeof(T)));
					}
				}
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00010EC4 File Offset: 0x0000F0C4
		public void SetRootItems(IList<TreeViewItemData<T>> items)
		{
			bool flag = items == base.itemsSource;
			if (!flag)
			{
				bool isCreated = this.m_Hierarchy.IsCreated;
				if (isCreated)
				{
					base.ClearIdToNodeDictionary();
					this.treeDataController.ClearNodeToDataDictionary();
					base.hierarchy = new Hierarchy();
				}
				bool flag2 = items != null;
				if (flag2)
				{
					this.treeDataController.ConvertTreeViewItemDataToHierarchy(items, (HierarchyNode node) => base.CreateNode(in node), delegate(int id, HierarchyNode node)
					{
						base.UpdateIdToNodeDictionary(id, in node, true);
					});
					base.UpdateHierarchy();
					bool autoExpand = base.baseTreeView.autoExpand;
					if (autoExpand)
					{
						this.m_HierarchyViewModel.SetFlags(HierarchyNodeFlags.Expanded);
						base.UpdateHierarchy();
					}
					bool flag3 = base.IsViewDataKeyEnabled();
					if (flag3)
					{
						base.OnViewDataReadyUpdateNodes();
					}
				}
				base.SetHierarchyViewModelWithoutNotify(this.m_HierarchyViewModel);
				base.RaiseItemsSourceChanged();
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00010F98 File Offset: 0x0000F198
		public virtual void AddItem(in TreeViewItemData<T> item, int parentId, int childIndex, bool rebuildTree = true)
		{
			bool flag = parentId == BaseTreeView.invalidId;
			HierarchyNode hierarchyNode;
			if (flag)
			{
				hierarchyNode = base.CreateNode(HierarchyNode.Null);
			}
			else
			{
				HierarchyNode hierarchyNodeById = base.GetHierarchyNodeById(parentId);
				hierarchyNode = base.CreateNode(in hierarchyNodeById);
				TreeViewItemData<T> treeItemDataForNode = this.treeDataController.GetTreeItemDataForNode(hierarchyNodeById);
				bool flag2 = treeItemDataForNode.data != null;
				if (flag2)
				{
					treeItemDataForNode.InsertChild(item, childIndex);
				}
			}
			this.treeDataController.AddItem(in item, hierarchyNode);
			base.UpdateIdToNodeDictionary(item.id, in hierarchyNode, true);
			bool flag3 = item.children.GetCount() > 0;
			if (flag3)
			{
				HierarchyNode parentNode = base.GetHierarchyNodeById(item.id);
				this.treeDataController.ConvertTreeViewItemDataToHierarchy(item.children, delegate(HierarchyNode itemNode)
				{
					BaseTreeViewController <>4__this = this;
					HierarchyNode hierarchyNode2 = (((in itemNode) == HierarchyNode.Null) ? parentNode : itemNode);
					return <>4__this.CreateNode(in hierarchyNode2);
				}, delegate(int id, HierarchyNode newNode)
				{
					base.UpdateIdToNodeDictionary(id, in newNode, true);
				});
			}
			bool autoExpand = base.baseTreeView.autoExpand;
			if (autoExpand)
			{
				base.ExpandAncestorNodes(in hierarchyNode);
			}
			bool flag4 = childIndex != -1;
			if (flag4)
			{
				HierarchyNode parent = this.m_Hierarchy.GetParent(in hierarchyNode);
				base.UpdateSortOrder(in parent, in hierarchyNode, childIndex);
			}
			if (rebuildTree)
			{
				base.baseTreeView.RefreshItems();
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000110E0 File Offset: 0x0000F2E0
		public virtual TreeViewItemData<T> GetTreeViewItemDataForId(int id)
		{
			return this.treeDataController.GetTreeItemDataForNode(base.GetHierarchyNodeById(id));
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00011104 File Offset: 0x0000F304
		public virtual TreeViewItemData<T> GetTreeViewItemDataForIndex(int index)
		{
			int idForIndex = this.GetIdForIndex(index);
			return this.treeDataController.GetTreeItemDataForNode(base.GetHierarchyNodeById(idForIndex));
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00011130 File Offset: 0x0000F330
		public override bool TryRemoveItem(int id, bool rebuildTree = true)
		{
			HierarchyNode node = base.GetHierarchyNodeById(id);
			bool flag = (in node) != HierarchyNode.Null;
			bool flag4;
			if (flag)
			{
				int parentId = this.GetParentId(id);
				bool flag2 = parentId != BaseTreeView.invalidId;
				if (flag2)
				{
					TreeViewItemData<T> treeItemDataForNode = this.treeDataController.GetTreeItemDataForNode(base.GetHierarchyNodeById(parentId));
					bool flag3 = treeItemDataForNode.data != null;
					if (flag3)
					{
						treeItemDataForNode.RemoveChild(id);
					}
				}
				base.RemoveAllChildrenItemsFromCollections(in node, delegate(HierarchyNode hierarchyNode, int itemId)
				{
					this.treeDataController.RemoveItem(hierarchyNode);
					this.UpdateIdToNodeDictionary(itemId, in node, false);
				});
				this.treeDataController.RemoveItem(node);
				base.UpdateIdToNodeDictionary(id, in node, false);
				this.m_Hierarchy.Remove(in node);
				if (rebuildTree)
				{
					base.baseTreeView.RefreshItems();
				}
				flag4 = true;
			}
			else
			{
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00011228 File Offset: 0x0000F428
		public override object GetItemForId(int id)
		{
			return this.treeDataController.GetTreeItemDataForNode(base.GetHierarchyNodeById(id)).data;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001125C File Offset: 0x0000F45C
		public virtual T GetDataForId(int id)
		{
			return this.treeDataController.GetDataForNode(base.GetHierarchyNodeById(id));
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00011280 File Offset: 0x0000F480
		public virtual T GetDataForIndex(int index)
		{
			return this.treeDataController.GetDataForNode(base.GetHierarchyNodeByIndex(index));
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000112A4 File Offset: 0x0000F4A4
		public override object GetItemForIndex(int index)
		{
			return this.treeDataController.GetDataForNode(base.GetHierarchyNodeByIndex(index));
		}

		// Token: 0x040001F4 RID: 500
		private TreeDataController<T> m_TreeDataController;
	}
}
