using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
	// Token: 0x020003FA RID: 1018
	[ToolboxItem("")]
	public class RepeaterItem : Control, INamingContainer, IDataItemContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> class.</summary>
		/// <param name="itemIndex">The index of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control from the <see cref="P:System.Web.UI.WebControls.Repeater.Items" /> collection of the control. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x06002D34 RID: 11572 RVA: 0x00076067 File Offset: 0x00074267
		public RepeaterItem(int itemIndex, ListItemType itemType)
		{
			this.idx = itemIndex;
			this.type = itemType;
		}

		/// <summary>Assigns any sources of the event and its information to the parent <see cref="T:System.Web.UI.WebControls.Repeater" /> control, if the <see cref="T:System.EventArgs" /> parameter is an instance of <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" />.</summary>
		/// <returns>true if the event assigned to the parent was raised, otherwise false.</returns>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002D35 RID: 11573 RVA: 0x00076080 File Offset: 0x00074280
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs e2 = e as CommandEventArgs;
			if (e2 != null)
			{
				base.RaiseBubbleEvent(this, new RepeaterCommandEventArgs(this, source, e2));
				return true;
			}
			return false;
		}

		/// <summary>Gets or sets a data item associated with the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents a data item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</returns>
		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x000760A9 File Offset: 0x000742A9
		// (set) Token: 0x06002D37 RID: 11575 RVA: 0x000760B1 File Offset: 0x000742B1
		public virtual object DataItem
		{
			get
			{
				return this.data_item;
			}
			set
			{
				this.data_item = value;
			}
		}

		/// <summary>Gets the index of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control from the <see cref="P:System.Web.UI.WebControls.Repeater.Items" /> collection of the control.</summary>
		/// <returns>The index of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control from the <see cref="P:System.Web.UI.WebControls.Repeater.Items" /> collection of the control.</returns>
		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x000760BA File Offset: 0x000742BA
		public virtual int ItemIndex
		{
			get
			{
				return this.idx;
			}
		}

		/// <summary>Gets the type of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</returns>
		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x000760C2 File Offset: 0x000742C2
		public virtual ListItemType ItemType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
		/// <returns>An <see cref="P:System.Web.UI.WebControls.RepeaterItem.ItemIndex" /> property.</returns>
		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x000760CA File Offset: 0x000742CA
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
		/// <returns>An <see cref="P:System.Web.UI.WebControls.RepeaterItem.ItemIndex" /> property.</returns>
		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000760CA File Offset: 0x000742CA
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x04001B72 RID: 7026
		private object data_item;

		// Token: 0x04001B73 RID: 7027
		private int idx;

		// Token: 0x04001B74 RID: 7028
		private ListItemType type;
	}
}
