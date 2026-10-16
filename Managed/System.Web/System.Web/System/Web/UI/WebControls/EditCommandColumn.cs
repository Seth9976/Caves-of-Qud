using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>A special column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that contains the Edit buttons for editing data items in each row.</summary>
	// Token: 0x0200038D RID: 909
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class EditCommandColumn : DataGridColumn
	{
		/// <summary>Gets or sets the button type for the column.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. The default value is LinkButton.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified button type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. </exception>
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0005A810 File Offset: 0x00058A10
		// (set) Token: 0x060023A7 RID: 9127 RVA: 0x0005A839 File Offset: 0x00058A39
		[DefaultValue(ButtonColumnType.LinkButton)]
		public virtual ButtonColumnType ButtonType
		{
			get
			{
				object obj = base.ViewState["ButtonType"];
				if (obj != null)
				{
					return (ButtonColumnType)obj;
				}
				return ButtonColumnType.LinkButton;
			}
			set
			{
				base.ViewState["ButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the text to display for the Cancel command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</summary>
		/// <returns>The caption to display for the Cancel command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</returns>
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x0005A851 File Offset: 0x00058A51
		// (set) Token: 0x060023A9 RID: 9129 RVA: 0x0005A868 File Offset: 0x00058A68
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string CancelText
		{
			get
			{
				return base.ViewState.GetString("CancelText", string.Empty);
			}
			set
			{
				base.ViewState["CancelText"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when an Update button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> object is clicked.</summary>
		/// <returns>true if validation is performed when an Update button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> is clicked; otherwise, false. The default is true.</returns>
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x0005A87B File Offset: 0x00058A7B
		// (set) Token: 0x060023AB RID: 9131 RVA: 0x0004796E File Offset: 0x00045B6E
		[DefaultValue(true)]
		public virtual bool CausesValidation
		{
			get
			{
				return base.ViewState.GetBool("CausesValidation", true);
			}
			set
			{
				base.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets the group of validation controls for which the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> object causes validation when it posts back to the server.</summary>
		/// <returns>The group of validation controls for which the Update button in an <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x00047A0B File Offset: 0x00045C0B
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x00047A22 File Offset: 0x00045C22
		[DefaultValue("")]
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

		/// <summary>Gets or sets the text to display for the Edit button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</summary>
		/// <returns>The caption to display for the Edit button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</returns>
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x0005A88E File Offset: 0x00058A8E
		// (set) Token: 0x060023AF RID: 9135 RVA: 0x0005A8A5 File Offset: 0x00058AA5
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string EditText
		{
			get
			{
				return base.ViewState.GetString("EditText", string.Empty);
			}
			set
			{
				base.ViewState["EditText"] = value;
			}
		}

		/// <summary>Gets or sets the text to display for the Update command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</summary>
		/// <returns>The caption to display for the Update command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</returns>
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x0005A8B8 File Offset: 0x00058AB8
		// (set) Token: 0x060023B1 RID: 9137 RVA: 0x0005A8CF File Offset: 0x00058ACF
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string UpdateText
		{
			get
			{
				return base.ViewState.GetString("UpdateText", string.Empty);
			}
			set
			{
				base.ViewState["UpdateText"] = value;
			}
		}

		/// <summary>Initializes a cell within the column.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that contains information about the cell to initialize. </param>
		/// <param name="columnIndex">The column number where the cell is located. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x060023B2 RID: 9138 RVA: 0x0005A8E4 File Offset: 0x00058AE4
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
			case ListItemType.SelectedItem:
				cell.Controls.Add(this.CreateButton(this.ButtonType, this.EditText, "Edit", false));
				return;
			case ListItemType.EditItem:
				cell.Controls.Add(this.CreateButton(this.ButtonType, this.UpdateText, "Update", this.CausesValidation));
				cell.Controls.Add(new LiteralControl("&nbsp;"));
				cell.Controls.Add(this.CreateButton(this.ButtonType, this.CancelText, "Cancel", false));
				return;
			default:
				return;
			}
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x0005A9A8 File Offset: 0x00058BA8
		private Control CreateButton(ButtonColumnType type, string text, string command, bool valid)
		{
			if (type == ButtonColumnType.LinkButton)
			{
				LinkButton linkButton = new DataGridColumn.ForeColorLinkButton();
				linkButton.Text = text;
				linkButton.CommandName = command;
				linkButton.CausesValidation = valid;
				if (valid)
				{
					linkButton.ValidationGroup = this.ValidationGroup;
				}
				return linkButton;
			}
			Button button = new Button();
			button.Text = text;
			button.CommandName = command;
			button.CausesValidation = valid;
			if (valid)
			{
				button.ValidationGroup = this.ValidationGroup;
			}
			return button;
		}
	}
}
