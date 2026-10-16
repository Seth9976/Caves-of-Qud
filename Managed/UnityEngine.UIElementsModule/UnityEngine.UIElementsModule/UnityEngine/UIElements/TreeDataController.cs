using System;
using System.Collections.Generic;
using Unity.Hierarchy;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006D RID: 109
	internal sealed class TreeDataController<T>
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x0001190C File Offset: 0x0000FB0C
		public void AddItem(in TreeViewItemData<T> item, HierarchyNode node)
		{
			this.m_NodeToItemDataDictionary.TryAdd(node, item);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00011922 File Offset: 0x0000FB22
		public void RemoveItem(HierarchyNode node)
		{
			this.m_NodeToItemDataDictionary.Remove(node);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00011934 File Offset: 0x0000FB34
		public TreeViewItemData<T> GetTreeItemDataForNode(HierarchyNode node)
		{
			TreeViewItemData<T> treeViewItemData;
			bool flag = this.m_NodeToItemDataDictionary.TryGetValue(node, out treeViewItemData);
			TreeViewItemData<T> treeViewItemData2;
			if (flag)
			{
				treeViewItemData2 = treeViewItemData;
			}
			else
			{
				treeViewItemData2 = default(TreeViewItemData<T>);
			}
			return treeViewItemData2;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00011968 File Offset: 0x0000FB68
		public T GetDataForNode(HierarchyNode node)
		{
			TreeViewItemData<T> treeViewItemData;
			bool flag = this.m_NodeToItemDataDictionary.TryGetValue(node, out treeViewItemData);
			T t;
			if (flag)
			{
				t = treeViewItemData.data;
			}
			else
			{
				t = default(T);
			}
			return t;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000119A0 File Offset: 0x0000FBA0
		internal unsafe void ConvertTreeViewItemDataToHierarchy(IEnumerable<TreeViewItemData<T>> list, Func<HierarchyNode, HierarchyNode> createNode, Action<int, HierarchyNode> updateDictionary)
		{
			bool flag = list == null;
			if (!flag)
			{
				this.m_ItemStack.Clear();
				this.m_NodeStack.Clear();
				IEnumerator<TreeViewItemData<T>> enumerator = list.GetEnumerator();
				HierarchyNode hierarchyNode = *HierarchyNode.Null;
				for (;;)
				{
					bool flag2 = enumerator.MoveNext();
					bool flag3 = !flag2;
					if (flag3)
					{
						bool flag4 = this.m_ItemStack.Count > 0;
						if (!flag4)
						{
							break;
						}
						hierarchyNode = this.m_NodeStack.Pop();
						enumerator = this.m_ItemStack.Pop();
					}
					else
					{
						TreeViewItemData<T> treeViewItemData = enumerator.Current;
						HierarchyNode hierarchyNode2 = createNode(hierarchyNode);
						this.UpdateNodeToDataDictionary(hierarchyNode2, treeViewItemData);
						updateDictionary(treeViewItemData.id, hierarchyNode2);
						bool flag5 = treeViewItemData.children != null && ((IList<TreeViewItemData<T>>)treeViewItemData.children).Count > 0;
						if (flag5)
						{
							this.m_NodeStack.Push(hierarchyNode);
							hierarchyNode = hierarchyNode2;
							this.m_ItemStack.Push(enumerator);
							enumerator = treeViewItemData.children.GetEnumerator();
						}
					}
				}
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00011ABB File Offset: 0x0000FCBB
		internal void UpdateNodeToDataDictionary(HierarchyNode node, TreeViewItemData<T> item)
		{
			this.m_NodeToItemDataDictionary.TryAdd(node, item);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00011ACC File Offset: 0x0000FCCC
		internal void ClearNodeToDataDictionary()
		{
			this.m_NodeToItemDataDictionary.Clear();
		}

		// Token: 0x040001FB RID: 507
		private Dictionary<HierarchyNode, TreeViewItemData<T>> m_NodeToItemDataDictionary = new Dictionary<HierarchyNode, TreeViewItemData<T>>();

		// Token: 0x040001FC RID: 508
		private Stack<IEnumerator<TreeViewItemData<T>>> m_ItemStack = new Stack<IEnumerator<TreeViewItemData<T>>>();

		// Token: 0x040001FD RID: 509
		private Stack<HierarchyNode> m_NodeStack = new Stack<HierarchyNode>();
	}
}
