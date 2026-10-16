using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>The <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> class is used by the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control to functionally represent a <see cref="T:System.Web.SiteMapNode" />.</summary>
	// Token: 0x02000406 RID: 1030
	[ToolboxItem(false)]
	public class SiteMapNodeItem : WebControl, IDataItemContainer, INamingContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> class, using the specified index and <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" />.</summary>
		/// <param name="itemIndex">The index in the <see cref="P:System.Web.UI.Control.Controls" /> collection that the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control uses to track the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> object. </param>
		/// <param name="itemType">The functional type of <see cref="T:System.Web.SiteMapNode" /> that this <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> represents. </param>
		// Token: 0x06002DA1 RID: 11681 RVA: 0x00076B01 File Offset: 0x00074D01
		public SiteMapNodeItem(int itemIndex, SiteMapNodeItemType itemType)
		{
			this.itemIndex = itemIndex;
			this.SetItemType(itemType);
		}

		/// <summary>Sets the current <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /><see cref="P:System.Web.UI.WebControls.SiteMapNodeItem.ItemType" /> property.</summary>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" /> values. </param>
		// Token: 0x06002DA2 RID: 11682 RVA: 0x00076B17 File Offset: 0x00074D17
		protected internal virtual void SetItemType(SiteMapNodeItemType itemType)
		{
			this.itemType = itemType;
		}

		/// <summary>Retrieves the index that the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control uses to track and manage the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> in its internal collections.</summary>
		/// <returns>An integer that represents the location of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's internal collections.</returns>
		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x00076B20 File Offset: 0x00074D20
		public virtual int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		/// <summary>Retrieves the functional type of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" />.</summary>
		/// <returns>A member of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" /> enumeration that indicates the functional role of the node item in the navigation path hierarchy.</returns>
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x00076B28 File Offset: 0x00074D28
		public virtual SiteMapNodeItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.SiteMapNode" /> object that the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> represents.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> object that the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control uses to display a site navigation user interface.</returns>
		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x00076B30 File Offset: 0x00074D30
		// (set) Token: 0x06002DA6 RID: 11686 RVA: 0x00076B38 File Offset: 0x00074D38
		public virtual SiteMapNode SiteMapNode
		{
			get
			{
				return this.node;
			}
			set
			{
				this.node = value;
			}
		}

		/// <summary>Gets an object that is used in simplified data-binding operations.</summary>
		/// <returns>An object that represents the value to use when data-binding operations are performed.</returns>
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x00076B30 File Offset: 0x00074D30
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the index of the data item that is bound to the control.</summary>
		/// <returns>An integer that represents the location of the data item in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's internal collections.</returns>
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x00076B20 File Offset: 0x00074D20
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		/// <summary>Gets the position of the data item as displayed in the control.</summary>
		/// <returns>An integer that represents the location of the data item in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's internal collections.</returns>
		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x00076B20 File Offset: 0x00074D20
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x04001B80 RID: 7040
		private int itemIndex;

		// Token: 0x04001B81 RID: 7041
		private SiteMapNodeItemType itemType;

		// Token: 0x04001B82 RID: 7042
		private SiteMapNode node;
	}
}
