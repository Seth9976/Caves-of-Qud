using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x020003D8 RID: 984
	public class TreeView : ItemsControl<TreeViewItemDataBindingArgs>
	{
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06002499 RID: 9369 RVA: 0x000B8468 File Offset: 0x000B6668
		// (remove) Token: 0x0600249A RID: 9370 RVA: 0x000B84A0 File Offset: 0x000B66A0
		public event EventHandler<ItemExpandingArgs> ItemExpanding;

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x000B84D5 File Offset: 0x000B66D5
		protected override bool CanScroll
		{
			get
			{
				return base.CanScroll || this.CanReparent;
			}
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000B84E7 File Offset: 0x000B66E7
		protected override void OnEnableOverride()
		{
			base.OnEnableOverride();
			TreeViewItem.ParentChanged += this.OnTreeViewItemParentChanged;
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000B8500 File Offset: 0x000B6700
		protected override void OnDisableOverride()
		{
			base.OnDisableOverride();
			TreeViewItem.ParentChanged -= this.OnTreeViewItemParentChanged;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000B8519 File Offset: 0x000B6719
		public TreeViewItem GetTreeViewItem(int siblingIndex)
		{
			return (TreeViewItem)base.GetItemContainer(siblingIndex);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000B8527 File Offset: 0x000B6727
		public TreeViewItem GetTreeViewItem(object obj)
		{
			return (TreeViewItem)base.GetItemContainer(obj);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000B8538 File Offset: 0x000B6738
		public void AddChild(object parent, object item)
		{
			if (parent == null)
			{
				base.Add(item);
				return;
			}
			TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(parent);
			if (treeViewItem == null)
			{
				return;
			}
			int num = -1;
			if (treeViewItem.IsExpanded)
			{
				if (treeViewItem.HasChildren)
				{
					TreeViewItem treeViewItem2 = treeViewItem.LastDescendant();
					num = base.IndexOf(treeViewItem2.Item) + 1;
				}
				else
				{
					num = base.IndexOf(treeViewItem.Item) + 1;
				}
			}
			else
			{
				treeViewItem.CanExpand = true;
			}
			if (num > -1)
			{
				((TreeViewItem)this.Insert(num, item)).Parent = treeViewItem;
			}
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000B85C1 File Offset: 0x000B67C1
		public override void Remove(object item)
		{
			throw new NotSupportedException("This method is not supported for TreeView use RemoveChild instead");
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000B85D0 File Offset: 0x000B67D0
		public void RemoveChild(object parent, object item, bool isLastChild)
		{
			if (parent == null)
			{
				base.Remove(item);
				return;
			}
			if (base.GetItemContainer(item) != null)
			{
				base.Remove(item);
				return;
			}
			if (isLastChild)
			{
				TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(parent);
				if (treeViewItem)
				{
					treeViewItem.CanExpand = false;
				}
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000B8620 File Offset: 0x000B6820
		public void ChangeParent(object parent, object item)
		{
			if (base.IsDropInProgress)
			{
				return;
			}
			ItemContainer itemContainer = base.GetItemContainer(item);
			if (itemContainer == null)
			{
				return;
			}
			ItemContainer itemContainer2 = base.GetItemContainer(parent);
			ItemContainer[] array = new ItemContainer[] { itemContainer };
			if (this.CanDrop(array, itemContainer2))
			{
				this.Drop(array, itemContainer2, ItemDropAction.SetLastChild);
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000B8670 File Offset: 0x000B6870
		public bool IsExpanded(object item)
		{
			TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(item);
			return !(treeViewItem == null) && treeViewItem.IsExpanded;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000B869C File Offset: 0x000B689C
		public void Expand(TreeViewItem item)
		{
			if (this.m_expandSilently)
			{
				return;
			}
			if (this.ItemExpanding != null)
			{
				ItemExpandingArgs itemExpandingArgs = new ItemExpandingArgs(item.Item);
				this.ItemExpanding(this, itemExpandingArgs);
				IEnumerable children = itemExpandingArgs.Children;
				int num = item.transform.GetSiblingIndex();
				int num2 = base.IndexOf(item.Item);
				item.CanExpand = children != null;
				if (item.CanExpand)
				{
					foreach (object obj in children)
					{
						num++;
						num2++;
						base.InsertItem(num2, obj);
						TreeViewItem treeViewItem = (TreeViewItem)base.InstantiateItemContainer(num);
						treeViewItem.Item = obj;
						treeViewItem.Parent = item;
						this.DataBindItem(obj, treeViewItem);
					}
					base.UpdateSelectedItemIndex();
				}
			}
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000B878C File Offset: 0x000B698C
		public void Collapse(TreeViewItem item)
		{
			int siblingIndex = item.transform.GetSiblingIndex();
			int num = base.IndexOf(item.Item);
			this.Collapse(item, siblingIndex + 1, num + 1);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000B87C0 File Offset: 0x000B69C0
		private void Unselect(List<object> selectedItems, TreeViewItem item, ref int containerIndex, ref int itemIndex)
		{
			for (;;)
			{
				TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(containerIndex);
				if (treeViewItem == null || treeViewItem.Parent != item)
				{
					break;
				}
				containerIndex++;
				itemIndex++;
				selectedItems.Remove(treeViewItem.Item);
				this.Unselect(selectedItems, treeViewItem, ref containerIndex, ref itemIndex);
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000B881C File Offset: 0x000B6A1C
		private void Collapse(TreeViewItem item, int containerIndex, int itemIndex)
		{
			for (;;)
			{
				TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(containerIndex);
				if (treeViewItem == null || treeViewItem.Parent != item)
				{
					break;
				}
				this.Collapse(treeViewItem, containerIndex + 1, itemIndex + 1);
				base.RemoveItemAt(itemIndex);
				base.DestroyItemContainer(containerIndex);
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000B886C File Offset: 0x000B6A6C
		protected override ItemContainer InstantiateItemContainerOverride(GameObject container)
		{
			TreeViewItem treeViewItem = container.GetComponent<TreeViewItem>();
			if (treeViewItem == null)
			{
				treeViewItem = container.AddComponent<TreeViewItem>();
				treeViewItem.gameObject.name = "TreeViewItem";
			}
			return treeViewItem;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000B88A4 File Offset: 0x000B6AA4
		protected override void DestroyItem(object item)
		{
			TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(item);
			if (treeViewItem != null)
			{
				this.Collapse(treeViewItem);
				base.DestroyItem(item);
				if (treeViewItem.Parent != null && !treeViewItem.Parent.HasChildren)
				{
					treeViewItem.Parent.CanExpand = false;
				}
			}
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000B88FC File Offset: 0x000B6AFC
		public override void DataBindItem(object item, ItemContainer itemContainer)
		{
			TreeViewItemDataBindingArgs treeViewItemDataBindingArgs = new TreeViewItemDataBindingArgs();
			treeViewItemDataBindingArgs.Item = item;
			treeViewItemDataBindingArgs.ItemPresenter = ((itemContainer.ItemPresenter == null) ? base.gameObject : itemContainer.ItemPresenter);
			treeViewItemDataBindingArgs.EditorPresenter = ((itemContainer.EditorPresenter == null) ? base.gameObject : itemContainer.EditorPresenter);
			base.RaiseItemDataBinding(treeViewItemDataBindingArgs);
			TreeViewItem treeViewItem = (TreeViewItem)itemContainer;
			treeViewItem.CanExpand = treeViewItemDataBindingArgs.HasChildren;
			treeViewItem.CanEdit = treeViewItemDataBindingArgs.CanEdit;
			treeViewItem.CanDrag = treeViewItemDataBindingArgs.CanDrag;
			treeViewItem.CanDrop = treeViewItemDataBindingArgs.CanDrop;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000B8998 File Offset: 0x000B6B98
		protected override bool CanDrop(ItemContainer[] dragItems, ItemContainer dropTarget)
		{
			if (!base.CanDrop(dragItems, dropTarget))
			{
				return false;
			}
			TreeViewItem treeViewItem = (TreeViewItem)dropTarget;
			if (treeViewItem == null)
			{
				return true;
			}
			foreach (TreeViewItem treeViewItem2 in dragItems)
			{
				if (treeViewItem.IsDescendantOf(treeViewItem2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000B89E8 File Offset: 0x000B6BE8
		private void OnTreeViewItemParentChanged(object sender, ParentChangedEventArgs e)
		{
			TreeViewItem treeViewItem = (TreeViewItem)sender;
			if (!base.CanHandleEvent(treeViewItem))
			{
				return;
			}
			TreeViewItem oldParent = e.OldParent;
			if (base.DropMarker.Action != ItemDropAction.SetLastChild && base.DropMarker.Action != ItemDropAction.None)
			{
				if (oldParent != null && !oldParent.HasChildren)
				{
					oldParent.CanExpand = false;
				}
				return;
			}
			TreeViewItem newParent = e.NewParent;
			if (newParent != null)
			{
				if (newParent.CanExpand)
				{
					newParent.IsExpanded = true;
				}
				else
				{
					newParent.CanExpand = true;
					try
					{
						this.m_expandSilently = true;
						newParent.IsExpanded = true;
					}
					finally
					{
						this.m_expandSilently = false;
					}
				}
			}
			TreeViewItem treeViewItem2 = treeViewItem.FirstChild();
			TreeViewItem treeViewItem3;
			if (newParent != null)
			{
				treeViewItem3 = newParent.LastChild();
				if (treeViewItem3 == null)
				{
					treeViewItem3 = newParent;
				}
			}
			else
			{
				treeViewItem3 = (TreeViewItem)base.LastItemContainer();
			}
			if (treeViewItem3 != treeViewItem)
			{
				TreeViewItem treeViewItem4 = treeViewItem3.LastDescendant();
				if (treeViewItem4 != null)
				{
					treeViewItem3 = treeViewItem4;
				}
				if (!treeViewItem3.IsDescendantOf(treeViewItem))
				{
					base.SetNextSibling(treeViewItem3, treeViewItem);
				}
			}
			if (treeViewItem2 != null)
			{
				this.MoveSubtree(treeViewItem, treeViewItem2);
			}
			if (oldParent != null && !oldParent.HasChildren)
			{
				oldParent.CanExpand = false;
			}
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000B8B28 File Offset: 0x000B6D28
		private void MoveSubtree(TreeViewItem parent, TreeViewItem child)
		{
			int siblingIndex = parent.transform.GetSiblingIndex();
			int num = child.transform.GetSiblingIndex();
			bool flag = false;
			if (siblingIndex < num)
			{
				flag = true;
			}
			TreeViewItem treeViewItem = parent;
			while (child != null && child.IsDescendantOf(parent) && !(treeViewItem == child))
			{
				base.SetNextSibling(treeViewItem, child);
				treeViewItem = child;
				if (flag)
				{
					num++;
				}
				child = (TreeViewItem)base.GetItemContainer(num);
			}
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000B8B94 File Offset: 0x000B6D94
		protected override void Drop(ItemContainer[] dragItems, ItemContainer dropTarget, ItemDropAction action)
		{
			TreeViewItem treeViewItem = (TreeViewItem)dropTarget;
			if (action == ItemDropAction.SetLastChild)
			{
				foreach (TreeViewItem treeViewItem2 in dragItems)
				{
					if (treeViewItem != treeViewItem2)
					{
						treeViewItem2.Parent = treeViewItem;
					}
				}
			}
			else if (action == ItemDropAction.SetPrevSibling)
			{
				for (int j = 0; j < dragItems.Length; j++)
				{
					this.SetPrevSibling(treeViewItem, dragItems[j]);
				}
			}
			else if (action == ItemDropAction.SetNextSibling)
			{
				for (int k = dragItems.Length - 1; k >= 0; k--)
				{
					this.SetNextSibling(treeViewItem, dragItems[k]);
				}
			}
			base.UpdateSelectedItemIndex();
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000B8C1C File Offset: 0x000B6E1C
		protected override void SetNextSibling(ItemContainer sibling, ItemContainer nextSibling)
		{
			TreeViewItem treeViewItem = (TreeViewItem)sibling;
			TreeViewItem treeViewItem2 = treeViewItem.LastDescendant();
			if (treeViewItem2 == null)
			{
				treeViewItem2 = treeViewItem;
			}
			TreeViewItem treeViewItem3 = (TreeViewItem)nextSibling;
			TreeViewItem treeViewItem4 = treeViewItem3.FirstChild();
			base.SetNextSibling(treeViewItem2, nextSibling);
			if (treeViewItem4 != null)
			{
				this.MoveSubtree(treeViewItem3, treeViewItem4);
			}
			treeViewItem3.Parent = treeViewItem.Parent;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000B8C78 File Offset: 0x000B6E78
		protected override void SetPrevSibling(ItemContainer sibling, ItemContainer prevSibling)
		{
			TreeViewItem treeViewItem = (TreeViewItem)sibling;
			TreeViewItem treeViewItem2 = (TreeViewItem)prevSibling;
			TreeViewItem treeViewItem3 = treeViewItem2.FirstChild();
			base.SetPrevSibling(sibling, prevSibling);
			if (treeViewItem3 != null)
			{
				this.MoveSubtree(treeViewItem2, treeViewItem3);
			}
			treeViewItem2.Parent = treeViewItem.Parent;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000B8CC0 File Offset: 0x000B6EC0
		public void FixScrollRect()
		{
			Canvas.ForceUpdateCanvases();
			RectTransform component = this.Panel.GetComponent<RectTransform>();
			component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, component.rect.height - 0.01f);
		}

		// Token: 0x0400125C RID: 4700
		public int Indent = 20;

		// Token: 0x0400125D RID: 4701
		public bool CanReparent = true;

		// Token: 0x0400125E RID: 4702
		public bool AutoExpand;

		// Token: 0x0400125F RID: 4703
		private bool m_expandSilently;
	}
}
