using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a container that holds the contents of a templated menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	// Token: 0x020003D3 RID: 979
	public sealed class MenuItemTemplateContainer : Control, IDataItemContainer, INamingContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemTemplateContainer" /> class using the specified menu item index and menu item.</summary>
		/// <param name="itemIndex">The index of the menu item.</param>
		/// <param name="dataItem">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> object associated with the container.</param>
		// Token: 0x06002A1E RID: 10782 RVA: 0x0006C660 File Offset: 0x0006A860
		public MenuItemTemplateContainer(int itemIndex, MenuItem dataItem)
		{
			this.index = itemIndex;
			this.dataItem = dataItem;
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x0006C678 File Offset: 0x0006A878
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs e2 = e as CommandEventArgs;
			if (e2 == null)
			{
				return false;
			}
			MenuEventArgs e3 = new MenuEventArgs((MenuItem)this.DataItem, source, e2);
			base.RaiseBubbleEvent(this, e3);
			return true;
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x0006C6AD File Offset: 0x0006A8AD
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		/// <summary>Gets or sets the menu item associated with the container.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the menu item associated with the container.</returns>
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x0006C6B6 File Offset: 0x0006A8B6
		// (set) Token: 0x06002A22 RID: 10786 RVA: 0x0006C6BE File Offset: 0x0006A8BE
		public object DataItem
		{
			get
			{
				return this.dataItem;
			}
			set
			{
				this.dataItem = value;
			}
		}

		/// <summary>Gets the index of the menu item associated with the container.</summary>
		/// <returns>The index of the menu item associated with the container.</returns>
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x0006C6C7 File Offset: 0x0006A8C7
		public int ItemIndex
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the index value of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object associated with the container.</summary>
		/// <returns>The index value of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object associated with the container.</returns>
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06002A24 RID: 10788 RVA: 0x0006C6C7 File Offset: 0x0006A8C7
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the index value of the menu item for the container.</summary>
		/// <returns>The index value of the menu item for the container.</returns>
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06002A25 RID: 10789 RVA: 0x0006C6C7 File Offset: 0x0006A8C7
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x04001AD2 RID: 6866
		private object dataItem;

		// Token: 0x04001AD3 RID: 6867
		private int index;
	}
}
