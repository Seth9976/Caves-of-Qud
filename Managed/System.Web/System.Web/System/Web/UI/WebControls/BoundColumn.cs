using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>A column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that is bound to a field in a data source. </summary>
	// Token: 0x02000336 RID: 822
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class BoundColumn : DataGridColumn
	{
		/// <summary>Gets or sets the field name from the data source to bind to the <see cref="T:System.Web.UI.WebControls.BoundColumn" />.</summary>
		/// <returns>The name of the field to bind to the <see cref="T:System.Web.UI.WebControls.BoundColumn" />. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x000467CB File Offset: 0x000449CB
		// (set) Token: 0x06001D07 RID: 7431 RVA: 0x000467E2 File Offset: 0x000449E2
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual string DataField
		{
			get
			{
				return base.ViewState.GetString("DataField", string.Empty);
			}
			set
			{
				base.ViewState["DataField"] = value;
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for items in the column.</summary>
		/// <returns>A formatting string that specifies the display format of items in the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x000467F5 File Offset: 0x000449F5
		// (set) Token: 0x06001D09 RID: 7433 RVA: 0x0004680C File Offset: 0x00044A0C
		[WebCategory("Misc")]
		[WebSysDescription("")]
		[DefaultValue("")]
		public virtual string DataFormatString
		{
			get
			{
				return base.ViewState.GetString("DataFormatString", string.Empty);
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the items in the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> can be edited.</summary>
		/// <returns>true if the items in the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> cannot be edited; otherwise, false. The default value is false.</returns>
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0004681F File Offset: 0x00044A1F
		// (set) Token: 0x06001D0B RID: 7435 RVA: 0x00046832 File Offset: 0x00044A32
		[WebCategory("Misc")]
		[WebSysDescription("")]
		[DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				return base.ViewState.GetBool("ReadOnly", false);
			}
			set
			{
				base.ViewState["ReadOnly"] = value;
			}
		}

		/// <summary>Resets the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> to its initial state.</summary>
		// Token: 0x06001D0C RID: 7436 RVA: 0x0004684A File Offset: 0x00044A4A
		public override void Initialize()
		{
			this.data_format_string = this.DataFormatString;
		}

		/// <summary>Resets the specified cell in the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> to its initial state.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> object that represents the cell to reset. </param>
		/// <param name="columnIndex">The column number where the cell is located. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x06001D0D RID: 7437 RVA: 0x00046858 File Offset: 0x00044A58
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			string dataField = this.DataField;
			if (itemType - ListItemType.Item > 2)
			{
				if (itemType != ListItemType.EditItem)
				{
					return;
				}
				if (this.ReadOnly && dataField != null && dataField.Length != 0)
				{
					cell.DataBinding += this.ItemDataBinding;
					return;
				}
				TextBox textBox = new TextBox();
				if (dataField != null && dataField.Length != 0)
				{
					textBox.DataBinding += this.ItemDataBinding;
				}
				cell.Controls.Add(textBox);
			}
			else if (dataField != null && dataField.Length != 0)
			{
				cell.DataBinding += this.ItemDataBinding;
				return;
			}
		}

		/// <summary>Converts the specified value to the format indicated by the <see cref="P:System.Web.UI.WebControls.BoundColumn.DataFormatString" /> property.</summary>
		/// <returns>The specified value converted to the format indicated by the <see cref="P:System.Web.UI.WebControls.BoundColumn.DataFormatString" /> property.</returns>
		/// <param name="dataValue">The value to format. </param>
		// Token: 0x06001D0E RID: 7438 RVA: 0x000468F3 File Offset: 0x00044AF3
		protected virtual string FormatDataValue(object dataValue)
		{
			if (dataValue == null)
			{
				return "";
			}
			if (this.data_format_string == string.Empty)
			{
				return dataValue.ToString();
			}
			return string.Format(this.data_format_string, dataValue);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00046924 File Offset: 0x00044B24
		private string GetValueFromItem(DataGridItem item)
		{
			object obj;
			if (this.DataField != BoundColumn.thisExpr)
			{
				obj = DataBinder.Eval(item.DataItem, this.DataField);
			}
			else
			{
				obj = item.DataItem;
			}
			string text = this.FormatDataValue(obj);
			if (!(text != ""))
			{
				return "&nbsp;";
			}
			return text;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0004697C File Offset: 0x00044B7C
		private void ItemDataBinding(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			string valueFromItem = this.GetValueFromItem((DataGridItem)control.NamingContainer);
			TableCell tableCell = sender as TableCell;
			if (tableCell == null)
			{
				((TextBox)sender).Text = valueFromItem;
				return;
			}
			tableCell.Text = valueFromItem;
		}

		// Token: 0x0400181B RID: 6171
		private string data_format_string;

		/// <summary>Represents the string "!". This field is read-only.</summary>
		// Token: 0x0400181C RID: 6172
		public static readonly string thisExpr = "!";
	}
}
