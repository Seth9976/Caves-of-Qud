using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an item in a <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	// Token: 0x0200037E RID: 894
	[ToolboxItem("")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataListItem : WebControl, INamingContainer, IDataItemContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataListItem" /> class.</summary>
		/// <param name="itemIndex">The index of the item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control from the <see cref="P:System.Web.UI.WebControls.DataList.Items" /> collection. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x06002274 RID: 8820 RVA: 0x00056FBF File Offset: 0x000551BF
		public DataListItem(int itemIndex, ListItemType itemType)
		{
			this.index = itemIndex;
			this.type = itemType;
		}

		/// <summary>Gets or sets a data item associated with the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a data item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x00056FD5 File Offset: 0x000551D5
		// (set) Token: 0x06002276 RID: 8822 RVA: 0x00056FDD File Offset: 0x000551DD
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

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object from the <see cref="P:System.Web.UI.WebControls.DataList.Items" /> collection of the control.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object from the <see cref="P:System.Web.UI.WebControls.DataList.Items" /> collection.</returns>
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x00056FE6 File Offset: 0x000551E6
		public virtual int ItemIndex
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the type of the item represented by the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</returns>
		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x00056FEE File Offset: 0x000551EE
		public virtual ListItemType ItemType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object for the <see cref="T:System.Web.UI.WebControls.DataListItem" /> control, which contains the style properties for the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties of the control.</returns>
		// Token: 0x0600227A RID: 8826 RVA: 0x00056FF6 File Offset: 0x000551F6
		protected override Style CreateControlStyle()
		{
			return new TableItemStyle(this.ViewState);
		}

		/// <summary>Determines whether the event for the control is passed up the server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x0600227B RID: 8827 RVA: 0x00057004 File Offset: 0x00055204
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs e2 = e as CommandEventArgs;
			if (e2 != null)
			{
				base.RaiseBubbleEvent(this, new DataListCommandEventArgs(this, source, e2));
				return true;
			}
			return false;
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that contains the output stream for rendering on the client. </param>
		/// <param name="extractRows">true to extract rows; otherwise false. </param>
		/// <param name="tableLayout">true to display as a table; otherwise false. </param>
		// Token: 0x0600227C RID: 8828 RVA: 0x00057030 File Offset: 0x00055230
		public virtual void RenderItem(HtmlTextWriter writer, bool extractRows, bool tableLayout)
		{
			bool flag = !extractRows && !tableLayout;
			if (flag)
			{
				writer.RenderBeginTag(this.TagKey);
			}
			if (this.HasControls())
			{
				if (extractRows)
				{
					bool flag2 = false;
					foreach (object obj in this.Controls)
					{
						Table table = ((Control)obj) as Table;
						if (table != null)
						{
							flag2 = true;
							using (IEnumerator enumerator2 = table.Rows.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									TableRow tableRow = (TableRow)obj2;
									if (base.ControlStyleCreated && !base.ControlStyle.IsEmpty)
									{
										tableRow.ControlStyle.MergeWith(base.ControlStyle);
									}
									tableRow.RenderControl(writer);
								}
								break;
							}
						}
					}
					if (!flag2)
					{
						throw new HttpException("No Table found in DataList template.");
					}
				}
				else
				{
					this.RenderContents(writer);
				}
			}
			if (flag)
			{
				writer.RenderEndTag();
			}
		}

		/// <summary>Sets the current <see cref="P:System.Web.UI.WebControls.DataListItem.ItemType" /> property for the <see cref="T:System.Web.UI.WebControls.DataListItem" /> control.</summary>
		/// <param name="itemType">A <see cref="T:System.Web.UI.WebControls.ListItemType" />  value.</param>
		// Token: 0x0600227D RID: 8829 RVA: 0x00057158 File Offset: 0x00055358
		protected virtual void SetItemType(ListItemType itemType)
		{
			this.type = itemType;
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItem" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the value to use when data-binding operations are performed.</returns>
		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x00056FD5 File Offset: 0x000551D5
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
		/// <returns>An integer representing the index of the data item bound to a control.</returns>
		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x00056FE6 File Offset: 0x000551E6
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
		/// <returns>An integer representing the position of the data item as displayed in a control.</returns>
		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x00056FE6 File Offset: 0x000551E6
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x0400192A RID: 6442
		private int index;

		// Token: 0x0400192B RID: 6443
		private ListItemType type;

		// Token: 0x0400192C RID: 6444
		private object item;
	}
}
