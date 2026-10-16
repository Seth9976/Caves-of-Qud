using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>A column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that contains a hyperlink for each item in the column.</summary>
	// Token: 0x020003AD RID: 941
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HyperLinkColumn : DataGridColumn
	{
		/// <summary>Gets or sets the field from a data source to bind to the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</summary>
		/// <returns>The field from a data source to bind to the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</returns>
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x0006276B File Offset: 0x0006096B
		// (set) Token: 0x0600267A RID: 9850 RVA: 0x00062782 File Offset: 0x00060982
		[DefaultValue("")]
		[WebCategory("Misc")]
		[WebSysDescription("")]
		public virtual string DataNavigateUrlField
		{
			get
			{
				return base.ViewState.GetString("DataNavigateUrlField", string.Empty);
			}
			set
			{
				base.ViewState["DataNavigateUrlField"] = value;
			}
		}

		/// <summary>Gets or sets the display format for the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" /> when the URL is data-bound to a field in a data source.</summary>
		/// <returns>The string that specifies the display format for the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" /> when the URL is data-bound to a field in a data source. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x00062795 File Offset: 0x00060995
		// (set) Token: 0x0600267C RID: 9852 RVA: 0x000627AC File Offset: 0x000609AC
		[Description("The formatting applied to the value bound to the NavigateUrl property.")]
		[DefaultValue("")]
		[WebCategory("Misc")]
		public virtual string DataNavigateUrlFormatString
		{
			get
			{
				return base.ViewState.GetString("DataNavigateUrlFormatString", string.Empty);
			}
			set
			{
				base.ViewState["DataNavigateUrlFormatString"] = value;
			}
		}

		/// <summary>Gets or sets the field from a data source to bind to the text caption of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</summary>
		/// <returns>The field name from a data source to bind to the text caption of the hyperlinks in <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</returns>
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x00047986 File Offset: 0x00045B86
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x0004799D File Offset: 0x00045B9D
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual string DataTextField
		{
			get
			{
				return base.ViewState.GetString("DataTextField", string.Empty);
			}
			set
			{
				base.ViewState["DataTextField"] = value;
			}
		}

		/// <summary>Gets or sets the display format for the text caption of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" /> column.</summary>
		/// <returns>The string that specifies the display format for the text caption of the hyperlinks in the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000479B0 File Offset: 0x00045BB0
		// (set) Token: 0x06002680 RID: 9856 RVA: 0x000627BF File Offset: 0x000609BF
		[Description("The formatting applied to the value bound to the Text property.")]
		[DefaultValue("")]
		[WebCategory("Misc")]
		public virtual string DataTextFormatString
		{
			get
			{
				return base.ViewState.GetString("DataTextFormatString", string.Empty);
			}
			set
			{
				base.ViewState["DataTextFormatString"] = value;
			}
		}

		/// <summary>Gets or sets the URL to link to when a hyperlink in the column is clicked.</summary>
		/// <returns>The URL to link to when a hyperlink in the column is clicked.</returns>
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x000627D2 File Offset: 0x000609D2
		// (set) Token: 0x06002682 RID: 9858 RVA: 0x000627E9 File Offset: 0x000609E9
		[WebSysDescription("")]
		[UrlProperty]
		[DefaultValue("")]
		[WebCategory("Misc")]
		public virtual string NavigateUrl
		{
			get
			{
				return base.ViewState.GetString("NavigateUrl", string.Empty);
			}
			set
			{
				base.ViewState["NavigateUrl"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame to display the Web page content that is linked to when the hyperlink in the column is clicked.</summary>
		/// <returns>The target window or frame to load the Web page linked to when a hyperlink in the column is clicked. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x000627FC File Offset: 0x000609FC
		// (set) Token: 0x06002684 RID: 9860 RVA: 0x00062813 File Offset: 0x00060A13
		[TypeConverter("System.Web.UI.WebControls.TargetConverter")]
		[WebCategory("Misc")]
		[DefaultValue("")]
		[WebSysDescription("")]
		public virtual string Target
		{
			get
			{
				return base.ViewState.GetString("Target", string.Empty);
			}
			set
			{
				base.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the text caption to display for the hyperlinks in the column.</summary>
		/// <returns>The text caption for the hyperlinks in the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x000479E1 File Offset: 0x00045BE1
		// (set) Token: 0x06002686 RID: 9862 RVA: 0x000479F8 File Offset: 0x00045BF8
		[WebCategory("Misc")]
		[Localizable(true)]
		[DefaultValue("")]
		[WebSysDescription("")]
		public virtual string Text
		{
			get
			{
				return base.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		/// <summary>Formats a data-bound URL using the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataNavigateUrlFormatString" /> property.</summary>
		/// <returns>The data-bound URL in the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataNavigateUrlFormatString" /> property.</returns>
		/// <param name="dataUrlValue">The data-bound URL to format. </param>
		// Token: 0x06002687 RID: 9863 RVA: 0x00062828 File Offset: 0x00060A28
		protected virtual string FormatDataNavigateUrlValue(object dataUrlValue)
		{
			string text = this.DataNavigateUrlFormatString;
			if (text == "")
			{
				text = null;
			}
			return DataBinder.FormatResult(dataUrlValue, text);
		}

		/// <summary>Formats a data-bound text caption using the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataTextFormatString" /> property.</summary>
		/// <returns>The data-bound text caption in the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataTextFormatString" /> property.</returns>
		/// <param name="dataTextValue">The data-bound URL to format. </param>
		// Token: 0x06002688 RID: 9864 RVA: 0x00062854 File Offset: 0x00060A54
		protected virtual string FormatDataTextValue(object dataTextValue)
		{
			string text = this.DataTextFormatString;
			if (text == "")
			{
				text = null;
			}
			return DataBinder.FormatResult(dataTextValue, text);
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x00047A35 File Offset: 0x00045C35
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x00062880 File Offset: 0x00060A80
		private void ItemDataBinding(object sender, EventArgs args)
		{
			TableCell tableCell = (TableCell)sender;
			HyperLink hyperLink = (HyperLink)tableCell.Controls[0];
			DataGridItem dataGridItem = (DataGridItem)tableCell.NamingContainer;
			if (this.DataNavigateUrlField != "")
			{
				hyperLink.NavigateUrl = this.FormatDataNavigateUrlValue(DataBinder.Eval(dataGridItem.DataItem, this.DataNavigateUrlField));
			}
			else
			{
				hyperLink.NavigateUrl = this.NavigateUrl;
			}
			if (this.DataTextField != "")
			{
				hyperLink.Text = this.FormatDataTextValue(DataBinder.Eval(dataGridItem.DataItem, this.DataTextField));
			}
			else
			{
				hyperLink.Text = this.Text;
			}
			hyperLink.Target = this.Target;
		}

		/// <summary>Initializes the cell representing this column with the contained hyperlink.</summary>
		/// <param name="cell">The cell to be initialized. </param>
		/// <param name="columnIndex">The index of the column that contains the cell. </param>
		/// <param name="itemType">The type of item that the cell is part of. </param>
		// Token: 0x0600268B RID: 9867 RVA: 0x00062938 File Offset: 0x00060B38
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			switch (itemType)
			{
			case ListItemType.Header:
			case ListItemType.Footer:
			case ListItemType.Separator:
			case ListItemType.Pager:
				return;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.EditItem:
				cell.DataBinding += this.ItemDataBinding;
				cell.Controls.Add(new HyperLink());
				break;
			case ListItemType.SelectedItem:
				break;
			default:
				return;
			}
		}
	}
}
