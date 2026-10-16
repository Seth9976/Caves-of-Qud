using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>A column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that contains a user-defined button.</summary>
	// Token: 0x0200033C RID: 828
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ButtonColumn : DataGridColumn
	{
		/// <summary>Gets or sets the type of button to display in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. The default is LinkButton.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified column type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. </exception>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x00047906 File Offset: 0x00045B06
		// (set) Token: 0x06001D7A RID: 7546 RVA: 0x00047919 File Offset: 0x00045B19
		[WebCategory("Misc")]
		[DefaultValue(ButtonColumnType.LinkButton)]
		[WebSysDescription("The type of button contained within the column.")]
		public virtual ButtonColumnType ButtonType
		{
			get
			{
				return (ButtonColumnType)base.ViewState.GetInt("LinkButton", 0);
			}
			set
			{
				base.ViewState["LinkButton"] = value;
			}
		}

		/// <summary>Gets or sets a string that represents the command to perform when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object is clicked.</summary>
		/// <returns>A string that represents the command to perform when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> is clicked. The default is an empty string ("").</returns>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x00047931 File Offset: 0x00045B31
		// (set) Token: 0x06001D7C RID: 7548 RVA: 0x00047948 File Offset: 0x00045B48
		[DefaultValue("")]
		[WebSysDescription("The command associated with the button.")]
		[WebCategory("Misc")]
		public virtual string CommandName
		{
			get
			{
				return base.ViewState.GetString("CommandName", string.Empty);
			}
			set
			{
				base.ViewState["CommandName"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object is clicked.</summary>
		/// <returns>true if validation is performed when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> is clicked; otherwise, false. The default is false.</returns>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x0004795B File Offset: 0x00045B5B
		// (set) Token: 0x06001D7E RID: 7550 RVA: 0x0004796E File Offset: 0x00045B6E
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool CausesValidation
		{
			get
			{
				return base.ViewState.GetBool("CausesValidation", false);
			}
			set
			{
				base.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets the field name from a data source to bind to the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object.</summary>
		/// <returns>The field name to bind to the <see cref="T:System.Web.UI.WebControls.ButtonColumn" />. The default is an empty string ("").</returns>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x00047986 File Offset: 0x00045B86
		// (set) Token: 0x06001D80 RID: 7552 RVA: 0x0004799D File Offset: 0x00045B9D
		[WebSysDescription("The field bound to the text property of the button.")]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the string that specifies the display format for the caption in each button.</summary>
		/// <returns>The string that specifies the display format for the caption in each button. The default is an empty string ("").</returns>
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x000479B0 File Offset: 0x00045BB0
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x000479C7 File Offset: 0x00045BC7
		[WebSysDescription("The formatting applied to the value bound to the Text property.")]
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
				this.format = null;
			}
		}

		/// <summary>Gets or sets the caption that is displayed in the buttons of the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object.</summary>
		/// <returns>The caption displayed in the buttons of the <see cref="T:System.Web.UI.WebControls.ButtonColumn" />. The default is an empty string ("").</returns>
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x000479E1 File Offset: 0x00045BE1
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x000479F8 File Offset: 0x00045BF8
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("The text used for the button.")]
		[WebCategory("Misc")]
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

		/// <summary>Gets or sets the group of validation controls for which the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object causes validation when it posts back to the server.</summary>
		/// <returns>The group of validation controls for which the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object causes validation when it posts back to the server. The default is an empty string ("").</returns>
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x00047A0B File Offset: 0x00045C0B
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x00047A22 File Offset: 0x00045C22
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual string ValidationGroup
		{
			get
			{
				return base.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				base.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Resets the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object to its initial state.</summary>
		// Token: 0x06001D87 RID: 7559 RVA: 0x00047A35 File Offset: 0x00045C35
		public override void Initialize()
		{
			base.Initialize();
		}

		/// <summary>Resets a cell in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object to its initial state.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents the cell to reset. </param>
		/// <param name="columnIndex">The column number where the cell is located. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x06001D88 RID: 7560 RVA: 0x00047A40 File Offset: 0x00045C40
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			if (itemType != ListItemType.Header && itemType != ListItemType.Footer)
			{
				ButtonColumnType buttonType = this.ButtonType;
				if (buttonType == ButtonColumnType.LinkButton)
				{
					LinkButton linkButton = new DataGridColumn.ForeColorLinkButton();
					linkButton.Text = this.Text;
					linkButton.CommandName = this.CommandName;
					if (!string.IsNullOrEmpty(this.DataTextField))
					{
						linkButton.DataBinding += this.DoDataBind;
					}
					cell.Controls.Add(linkButton);
					return;
				}
				if (buttonType != ButtonColumnType.PushButton)
				{
					return;
				}
				Button button = new Button();
				button.Text = this.Text;
				button.CommandName = this.CommandName;
				if (!string.IsNullOrEmpty(this.DataTextField))
				{
					button.DataBinding += this.DoDataBind;
				}
				cell.Controls.Add(button);
			}
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x00047B08 File Offset: 0x00045D08
		private string GetValueFromItem(DataGridItem item)
		{
			object obj = null;
			if (this.text_field == null)
			{
				this.text_field = this.DataTextField;
			}
			if (!string.IsNullOrEmpty(this.text_field))
			{
				obj = DataBinder.Eval(item.DataItem, this.text_field);
			}
			return this.FormatDataTextValue(obj);
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x00047B54 File Offset: 0x00045D54
		private void DoDataBind(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			string valueFromItem = this.GetValueFromItem((DataGridItem)control.NamingContainer);
			LinkButton linkButton = sender as LinkButton;
			if (linkButton == null)
			{
				((Button)sender).Text = valueFromItem;
				return;
			}
			linkButton.Text = valueFromItem;
		}

		/// <summary>Converts the specified value to the format that is indicated by the <see cref="P:System.Web.UI.WebControls.ButtonColumn.DataTextFormatString" /> property.</summary>
		/// <returns>The <paramref name="dataTextValue" /> converted to the format indicated by the <see cref="P:System.Web.UI.WebControls.ButtonColumn.DataTextFormatString" />.</returns>
		/// <param name="dataTextValue">The value to format. </param>
		// Token: 0x06001D8B RID: 7563 RVA: 0x00047B98 File Offset: 0x00045D98
		protected virtual string FormatDataTextValue(object dataTextValue)
		{
			if (dataTextValue == null)
			{
				return string.Empty;
			}
			if (this.format == null)
			{
				this.format = this.DataTextFormatString;
			}
			if (string.IsNullOrEmpty(this.format))
			{
				return dataTextValue.ToString();
			}
			return string.Format(this.format, dataTextValue);
		}

		// Token: 0x04001831 RID: 6193
		private string text_field;

		// Token: 0x04001832 RID: 6194
		private string format;
	}
}
