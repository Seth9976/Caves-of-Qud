using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an item (row) in a <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	// Token: 0x02000376 RID: 886
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataGridItem : TableRow, INamingContainer, IDataItemContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> class.</summary>
		/// <param name="itemIndex">The index of the item from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. </param>
		/// <param name="dataSetIndex">The index number of the item, from the bound data source, that appears in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x060021D2 RID: 8658 RVA: 0x0005504F File Offset: 0x0005324F
		public DataGridItem(int itemIndex, int dataSetIndex, ListItemType itemType)
		{
			this.item_index = itemIndex;
			this.dataset_index = dataSetIndex;
			this.item_type = itemType;
		}

		/// <summary>Gets or sets the data item represented by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents a data item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0005506C File Offset: 0x0005326C
		// (set) Token: 0x060021D4 RID: 8660 RVA: 0x00055074 File Offset: 0x00053274
		public virtual object DataItem
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object from the bound data source.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the bound data source.</returns>
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x0005507D File Offset: 0x0005327D
		public virtual int DataSetIndex
		{
			get
			{
				return this.dataset_index;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x00055085 File Offset: 0x00053285
		public virtual int ItemIndex
		{
			get
			{
				return this.item_index;
			}
		}

		/// <summary>Gets the type of the item represented by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</returns>
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0005508D File Offset: 0x0005328D
		public virtual ListItemType ItemType
		{
			get
			{
				return this.item_type;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItem" />.</summary>
		/// <returns>An object that represents a data item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060021D8 RID: 8664 RVA: 0x0005506C File Offset: 0x0005326C
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the bound data source.</returns>
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x00055085 File Offset: 0x00053285
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.item_index;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060021DA RID: 8666 RVA: 0x00055085 File Offset: 0x00053285
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.item_index;
			}
		}

		/// <summary>Used internally by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> class.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060021DB RID: 8667 RVA: 0x00055095 File Offset: 0x00053295
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				base.RaiseBubbleEvent(this, new DataGridCommandEventArgs(this, source, (CommandEventArgs)e));
				return true;
			}
			return base.OnBubbleEvent(source, e);
		}

		/// <summary>Used internally by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> class.</summary>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</param>
		// Token: 0x060021DC RID: 8668 RVA: 0x000550BD File Offset: 0x000532BD
		protected internal virtual void SetItemType(ListItemType itemType)
		{
			this.item_type = itemType;
		}

		// Token: 0x040018FA RID: 6394
		private object item;

		// Token: 0x040018FB RID: 6395
		private int dataset_index;

		// Token: 0x040018FC RID: 6396
		private int item_index;

		// Token: 0x040018FD RID: 6397
		private ListItemType item_type;
	}
}
