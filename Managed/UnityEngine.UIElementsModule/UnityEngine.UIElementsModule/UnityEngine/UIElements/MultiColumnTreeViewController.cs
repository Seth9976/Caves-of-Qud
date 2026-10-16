using System;
using System.Collections.Generic;
using UnityEngine.UIElements.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006C RID: 108
	public abstract class MultiColumnTreeViewController : BaseTreeViewController
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00011772 File Offset: 0x0000F972
		public MultiColumnController columnController
		{
			get
			{
				return this.m_ColumnController;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0001177A File Offset: 0x0000F97A
		internal MultiColumnCollectionHeader header
		{
			get
			{
				MultiColumnController columnController = this.m_ColumnController;
				return (columnController != null) ? columnController.header : null;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001178E File Offset: 0x0000F98E
		protected MultiColumnTreeViewController(Columns columns, SortColumnDescriptions sortDescriptions, List<SortColumnDescription> sortedColumns)
		{
			this.m_ColumnController = new MultiColumnController(columns, sortDescriptions, sortedColumns);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000117A6 File Offset: 0x0000F9A6
		internal override void PreRefresh()
		{
			base.PreRefresh();
			this.m_ColumnController.SortIfNeeded();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000117BC File Offset: 0x0000F9BC
		internal override void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			ReusableMultiColumnTreeViewItem reusableMultiColumnTreeViewItem = reusableItem as ReusableMultiColumnTreeViewItem;
			bool flag = reusableMultiColumnTreeViewItem != null;
			if (flag)
			{
				reusableMultiColumnTreeViewItem.Init(this.MakeItem(), this.m_ColumnController.header.columns);
				base.PostInitRegistration(reusableMultiColumnTreeViewItem);
			}
			else
			{
				base.InvokeMakeItem(reusableItem);
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001180C File Offset: 0x0000FA0C
		public override object GetItemForIndex(int index)
		{
			int sourceIndex = this.columnController.GetSourceIndex(index);
			return base.GetItemForIndex(sourceIndex);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00011834 File Offset: 0x0000FA34
		public override int GetIndexForId(int id)
		{
			int indexForId = base.GetIndexForId(id);
			return this.columnController.GetSortedIndex(indexForId);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001185C File Offset: 0x0000FA5C
		public override int GetIdForIndex(int index)
		{
			int sourceIndex = this.columnController.GetSourceIndex(index);
			return base.GetIdForIndex(sourceIndex);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00011884 File Offset: 0x0000FA84
		protected override VisualElement MakeItem()
		{
			return this.m_ColumnController.MakeItem();
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000118A1 File Offset: 0x0000FAA1
		protected override void BindItem(VisualElement element, int index)
		{
			this.m_ColumnController.BindItem<object>(element, index, this.GetItemForIndex(index));
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000118B9 File Offset: 0x0000FAB9
		protected override void UnbindItem(VisualElement element, int index)
		{
			this.m_ColumnController.UnbindItem(element, index);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000118CA File Offset: 0x0000FACA
		protected override void DestroyItem(VisualElement element)
		{
			this.m_ColumnController.DestroyItem(element);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000118DA File Offset: 0x0000FADA
		protected override void PrepareView()
		{
			this.m_ColumnController.PrepareView(base.view);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000118EF File Offset: 0x0000FAEF
		public override void Dispose()
		{
			this.m_ColumnController.Dispose();
			this.m_ColumnController = null;
			base.Dispose();
		}

		// Token: 0x040001FA RID: 506
		private MultiColumnController m_ColumnController;
	}
}
