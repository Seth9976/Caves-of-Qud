using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;

namespace UnityEngine.UIElements
{
	// Token: 0x02000062 RID: 98
	public class DefaultMultiColumnTreeViewController<T> : MultiColumnTreeViewController, IDefaultTreeViewController<T>
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001090C File Offset: 0x0000EB0C
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

		// Token: 0x0600039D RID: 925 RVA: 0x00010931 File Offset: 0x0000EB31
		public DefaultMultiColumnTreeViewController(Columns columns, SortColumnDescriptions sortDescriptions, List<SortColumnDescription> sortedColumns)
			: base(columns, sortDescriptions, sortedColumns)
		{
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0001093E File Offset: 0x0000EB3E
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00010948 File Offset: 0x0000EB48
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

		// Token: 0x060003A0 RID: 928 RVA: 0x000109A4 File Offset: 0x0000EBA4
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

		// Token: 0x060003A1 RID: 929 RVA: 0x00010A78 File Offset: 0x0000EC78
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

		// Token: 0x060003A2 RID: 930 RVA: 0x00010BC0 File Offset: 0x0000EDC0
		public virtual TreeViewItemData<T> GetTreeViewItemDataForId(int id)
		{
			return this.treeDataController.GetTreeItemDataForNode(base.GetHierarchyNodeById(id));
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00010BE4 File Offset: 0x0000EDE4
		public virtual TreeViewItemData<T> GetTreeViewItemDataForIndex(int index)
		{
			int idForIndex = this.GetIdForIndex(index);
			return this.treeDataController.GetTreeItemDataForNode(base.GetHierarchyNodeById(idForIndex));
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00010C10 File Offset: 0x0000EE10
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

		// Token: 0x060003A5 RID: 933 RVA: 0x00010D08 File Offset: 0x0000EF08
		public override object GetItemForId(int id)
		{
			return this.treeDataController.GetTreeItemDataForNode(base.GetHierarchyNodeById(id)).data;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00010D3C File Offset: 0x0000EF3C
		public T GetDataForId(int id)
		{
			return this.treeDataController.GetDataForNode(base.GetHierarchyNodeById(id));
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00010D60 File Offset: 0x0000EF60
		public T GetDataForIndex(int index)
		{
			int sourceIndex = base.columnController.GetSourceIndex(index);
			return this.treeDataController.GetDataForNode(base.GetHierarchyNodeByIndex(sourceIndex));
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00010D94 File Offset: 0x0000EF94
		public override object GetItemForIndex(int index)
		{
			return this.treeDataController.GetDataForNode(base.GetHierarchyNodeByIndex(index));
		}

		// Token: 0x040001EF RID: 495
		private TreeDataController<T> m_TreeDataController;
	}
}
