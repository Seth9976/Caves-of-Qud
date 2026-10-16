using System;
using System.Collections.Generic;
using UnityEngine.UIElements.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006B RID: 107
	public class MultiColumnListViewController : BaseListViewController
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0001147D File Offset: 0x0000F67D
		public MultiColumnController columnController
		{
			get
			{
				return this.m_ColumnController;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00011485 File Offset: 0x0000F685
		internal MultiColumnCollectionHeader header
		{
			get
			{
				MultiColumnController columnController = this.m_ColumnController;
				return (columnController != null) ? columnController.header : null;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00011499 File Offset: 0x0000F699
		public MultiColumnListViewController(Columns columns, SortColumnDescriptions sortDescriptions, List<SortColumnDescription> sortedColumns)
		{
			this.m_ColumnController = new MultiColumnController(columns, sortDescriptions, sortedColumns);
			base.itemsSourceSizeChanged += this.SortIfNeeded;
			base.itemsSourceChanged += this.SortIfNeeded;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000114D7 File Offset: 0x0000F6D7
		internal override void PreRefresh()
		{
			base.PreRefresh();
			this.m_ColumnController.SortIfNeeded();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000114F0 File Offset: 0x0000F6F0
		private void SortIfNeeded()
		{
			this.m_ColumnController.UpdateDragger();
			bool flag = this.m_ColumnController.sortingMode == ColumnSortingMode.Default;
			if (flag)
			{
				base.view.RefreshItems();
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001152C File Offset: 0x0000F72C
		internal override void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			ReusableMultiColumnListViewItem reusableMultiColumnListViewItem = reusableItem as ReusableMultiColumnListViewItem;
			bool flag = reusableMultiColumnListViewItem != null;
			if (flag)
			{
				reusableMultiColumnListViewItem.Init(this.MakeItem(), this.m_ColumnController.header.columns, base.baseListView.reorderMode == ListViewReorderMode.Animated);
				base.PostInitRegistration(reusableMultiColumnListViewItem);
			}
			else
			{
				base.InvokeMakeItem(reusableItem);
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001158C File Offset: 0x0000F78C
		internal override void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			base.InvokeBindItem(reusableItem, index);
			ReusableListViewItem reusableListViewItem = reusableItem as ReusableListViewItem;
			bool flag = reusableListViewItem != null;
			if (flag)
			{
				bool flag2 = this.m_ColumnController.header.sortingEnabled && this.m_ColumnController.header.sortedColumnReadonly.Count > 0;
				reusableListViewItem.SetDragHandleEnabled(!flag2);
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000115F0 File Offset: 0x0000F7F0
		public override object GetItemForIndex(int index)
		{
			int sourceIndex = this.columnController.GetSourceIndex(index);
			return base.GetItemForIndex(sourceIndex);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00011618 File Offset: 0x0000F818
		public override int GetIndexForId(int id)
		{
			int indexForId = base.GetIndexForId(id);
			return this.columnController.GetSortedIndex(indexForId);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00011640 File Offset: 0x0000F840
		public override int GetIdForIndex(int index)
		{
			int sourceIndex = this.columnController.GetSourceIndex(index);
			return base.GetIdForIndex(sourceIndex);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00011668 File Offset: 0x0000F868
		protected override VisualElement MakeItem()
		{
			return this.m_ColumnController.MakeItem();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00011688 File Offset: 0x0000F888
		protected override void BindItem(VisualElement element, int index)
		{
			int sourceIndex = this.columnController.GetSourceIndex(index);
			this.m_ColumnController.BindItem<object>(element, sourceIndex, this.GetItemForIndex(index));
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000116B8 File Offset: 0x0000F8B8
		protected override void UnbindItem(VisualElement element, int index)
		{
			this.m_ColumnController.UnbindItem(element, index);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000116C9 File Offset: 0x0000F8C9
		protected override void DestroyItem(VisualElement element)
		{
			this.m_ColumnController.DestroyItem(element);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000116D9 File Offset: 0x0000F8D9
		protected override void PrepareView()
		{
			this.m_ColumnController.PrepareView(base.view);
			base.baseListView.reorderModeChanged += this.UpdateReorderClassList;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00011706 File Offset: 0x0000F906
		public override void Dispose()
		{
			base.baseListView.reorderModeChanged -= this.UpdateReorderClassList;
			this.m_ColumnController.Dispose();
			this.m_ColumnController = null;
			base.Dispose();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001173B File Offset: 0x0000F93B
		private void UpdateReorderClassList()
		{
			this.m_ColumnController.header.EnableInClassList(MultiColumnCollectionHeader.reorderableUssClassName, base.baseListView.reorderable && base.baseListView.reorderMode == ListViewReorderMode.Animated);
		}

		// Token: 0x040001F9 RID: 505
		private MultiColumnController m_ColumnController;
	}
}
