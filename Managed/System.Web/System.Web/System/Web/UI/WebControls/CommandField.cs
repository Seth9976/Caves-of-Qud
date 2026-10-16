using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a special field that displays command buttons to perform selecting, editing, inserting, or deleting operations in a data-bound control.</summary>
	// Token: 0x0200034E RID: 846
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CommandField : ButtonFieldBase
	{
		/// <summary>Gets or sets the URL to an image to display for the Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for the Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x0004CF04 File Offset: 0x0004B104
		// (set) Token: 0x06001F36 RID: 7990 RVA: 0x0004CF1B File Offset: 0x0004B11B
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		public virtual string CancelImageUrl
		{
			get
			{
				return base.ViewState.GetString("CancelImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["CancelImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for the Cancel button displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for the Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Cancel".</returns>
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x0004CF34 File Offset: 0x0004B134
		// (set) Token: 0x06001F38 RID: 7992 RVA: 0x0004CF4B File Offset: 0x0004B14B
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string CancelText
		{
			get
			{
				return base.ViewState.GetString("CancelText", "Cancel");
			}
			set
			{
				base.ViewState["CancelText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the user clicks a button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to perform validation when the user clicks a button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field; otherwise, false. The default is true.</returns>
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x0004CF64 File Offset: 0x0004B164
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x00047F01 File Offset: 0x00046101
		[DefaultValue(true)]
		public override bool CausesValidation
		{
			get
			{
				return base.ViewState.GetBool("CausesValidation", true);
			}
			set
			{
				base.ViewState["CausesValidation"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x0004CF77 File Offset: 0x0004B177
		// (set) Token: 0x06001F3C RID: 7996 RVA: 0x0004CF8E File Offset: 0x0004B18E
		[UrlProperty]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string DeleteImageUrl
		{
			get
			{
				return base.ViewState.GetString("DeleteImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["DeleteImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Delete".</returns>
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x0004CFA7 File Offset: 0x0004B1A7
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x0004CFBE File Offset: 0x0004B1BE
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string DeleteText
		{
			get
			{
				return base.ViewState.GetString("DeleteText", "Delete");
			}
			set
			{
				base.ViewState["DeleteText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x0004CFD7 File Offset: 0x0004B1D7
		// (set) Token: 0x06001F40 RID: 8000 RVA: 0x0004CFEE File Offset: 0x0004B1EE
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("")]
		public virtual string EditImageUrl
		{
			get
			{
				return base.ViewState.GetString("EditImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["EditImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Edit".</returns>
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x0004D007 File Offset: 0x0004B207
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x0004D01E File Offset: 0x0004B21E
		[WebSysDescription("")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		public virtual string EditText
		{
			get
			{
				return base.ViewState.GetString("EditText", "Edit");
			}
			set
			{
				base.ViewState["EditText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x0004D037 File Offset: 0x0004B237
		// (set) Token: 0x06001F44 RID: 8004 RVA: 0x0004D04E File Offset: 0x0004B24E
		[WebCategory("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[WebSysDescription("")]
		[UrlProperty]
		public virtual string InsertImageUrl
		{
			get
			{
				return base.ViewState.GetString("InsertImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["InsertImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Insert".</returns>
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0004D067 File Offset: 0x0004B267
		// (set) Token: 0x06001F46 RID: 8006 RVA: 0x0004D07E File Offset: 0x0004B27E
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[Localizable(true)]
		public virtual string InsertText
		{
			get
			{
				return base.ViewState.GetString("InsertText", "Insert");
			}
			set
			{
				base.ViewState["InsertText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x0004D097 File Offset: 0x0004B297
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x0004D0AE File Offset: 0x0004B2AE
		[UrlProperty]
		[DefaultValue("")]
		[WebSysDescription("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Appearance")]
		public virtual string NewImageUrl
		{
			get
			{
				return base.ViewState.GetString("NewImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["NewImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field. The default is "New".</returns>
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x0004D0C7 File Offset: 0x0004B2C7
		// (set) Token: 0x06001F4A RID: 8010 RVA: 0x0004D0DE File Offset: 0x0004B2DE
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string NewText
		{
			get
			{
				return base.ViewState.GetString("NewText", "New");
			}
			set
			{
				base.ViewState["NewText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL to an image to display for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0004D0F7 File Offset: 0x0004B2F7
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x0004D10E File Offset: 0x0004B30E
		[WebSysDescription("")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[WebCategory("Appearance")]
		public virtual string SelectImageUrl
		{
			get
			{
				return base.ViewState.GetString("SelectImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["SelectImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Select".</returns>
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0004D127 File Offset: 0x0004B327
		// (set) Token: 0x06001F4E RID: 8014 RVA: 0x0004D13E File Offset: 0x0004B33E
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string SelectText
		{
			get
			{
				return base.ViewState.GetString("SelectText", "Select");
			}
			set
			{
				base.ViewState["SelectText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether a Cancel button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to display a Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, false. The default is true.</returns>
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0004D157 File Offset: 0x0004B357
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x0004D16A File Offset: 0x0004B36A
		[WebSysDescription("")]
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		public virtual bool ShowCancelButton
		{
			get
			{
				return base.ViewState.GetBool("ShowCancelButton", true);
			}
			set
			{
				base.ViewState["ShowCancelButton"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether a Delete button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to display a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, false. The default is false.</returns>
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0004D188 File Offset: 0x0004B388
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x0004D19B File Offset: 0x0004B39B
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool ShowDeleteButton
		{
			get
			{
				return base.ViewState.GetBool("ShowDeleteButton", false);
			}
			set
			{
				base.ViewState["ShowDeleteButton"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether an Edit button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to display an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, false. The default is false.</returns>
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0004D1B9 File Offset: 0x0004B3B9
		// (set) Token: 0x06001F54 RID: 8020 RVA: 0x0004D1CC File Offset: 0x0004B3CC
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public virtual bool ShowEditButton
		{
			get
			{
				return base.ViewState.GetBool("ShowEditButton", false);
			}
			set
			{
				base.ViewState["ShowEditButton"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether a Select button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to display a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, false. The default is false.</returns>
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x0004D1EA File Offset: 0x0004B3EA
		// (set) Token: 0x06001F56 RID: 8022 RVA: 0x0004D1FD File Offset: 0x0004B3FD
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool ShowSelectButton
		{
			get
			{
				return base.ViewState.GetBool("ShowSelectButton", false);
			}
			set
			{
				base.ViewState["ShowSelectButton"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether a New button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>true to display a New button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, false. The default is false.</returns>
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x0004D21B File Offset: 0x0004B41B
		// (set) Token: 0x06001F58 RID: 8024 RVA: 0x0004D22E File Offset: 0x0004B42E
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool ShowInsertButton
		{
			get
			{
				return base.ViewState.GetBool("ShowInsertButton", false);
			}
			set
			{
				base.ViewState["ShowInsertButton"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The URL for an image to display for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x0004D24C File Offset: 0x0004B44C
		// (set) Token: 0x06001F5A RID: 8026 RVA: 0x0004D263 File Offset: 0x0004B463
		[WebSysDescription("")]
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Appearance")]
		public virtual string UpdateImageUrl
		{
			get
			{
				return base.ViewState.GetString("UpdateImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["UpdateImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
		/// <returns>The caption for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Update".</returns>
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x0004D27C File Offset: 0x0004B47C
		// (set) Token: 0x06001F5C RID: 8028 RVA: 0x0004D293 File Offset: 0x0004B493
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string UpdateText
		{
			get
			{
				return base.ViewState.GetString("UpdateText", "Update");
			}
			set
			{
				base.ViewState["UpdateText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object to the specified row state.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize.</param>
		/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="rowIndex">The zero-based index of the row that contains the cell.</param>
		// Token: 0x06001F5D RID: 8029 RVA: 0x0004D2AC File Offset: 0x0004B4AC
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			string text = rowIndex.ToString();
			if (cellType == DataControlCellType.DataCell)
			{
				if ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal)
				{
					if (this.ShowEditButton)
					{
						cell.Controls.Add(this.CreateButton(this.UpdateText, this.UpdateImageUrl, "Update", text));
						if (this.ShowCancelButton)
						{
							this.AddSeparator(cell);
							cell.Controls.Add(this.CreateButton(this.CancelText, this.CancelImageUrl, "Cancel", text));
							return;
						}
					}
				}
				else if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal)
				{
					if (this.ShowInsertButton)
					{
						cell.Controls.Add(this.CreateButton(this.InsertText, this.InsertImageUrl, "Insert", text));
						if (this.ShowCancelButton)
						{
							this.AddSeparator(cell);
							cell.Controls.Add(this.CreateButton(this.CancelText, this.CancelImageUrl, "Cancel", text));
							return;
						}
					}
				}
				else
				{
					if (this.ShowEditButton)
					{
						this.AddSeparator(cell);
						cell.Controls.Add(this.CreateButton(this.EditText, this.EditImageUrl, "Edit", text));
					}
					if (this.ShowDeleteButton)
					{
						this.AddSeparator(cell);
						cell.Controls.Add(this.CreateButton(this.DeleteText, this.DeleteImageUrl, "Delete", text));
					}
					if (this.ShowInsertButton)
					{
						this.AddSeparator(cell);
						cell.Controls.Add(this.CreateButton(this.NewText, this.NewImageUrl, "New", text));
					}
					if (this.ShowSelectButton)
					{
						this.AddSeparator(cell);
						cell.Controls.Add(this.CreateButton(this.SelectText, this.SelectImageUrl, "Select", text));
						return;
					}
				}
			}
			else
			{
				base.InitializeCell(cell, cellType, rowState, rowIndex);
			}
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0004D470 File Offset: 0x0004B670
		private Control CreateButton(string text, string image, string command, string arg)
		{
			IDataControlButton dataControlButton = DataControlButton.CreateButton(this.ButtonType, base.Control, text, image, command, arg, false);
			if (this.CausesValidation && (command == "Update" || command == "Insert"))
			{
				dataControlButton.Container = null;
				dataControlButton.CausesValidation = true;
				dataControlButton.ValidationGroup = this.ValidationGroup;
			}
			return (Control)dataControlButton;
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0004D4D8 File Offset: 0x0004B6D8
		private void AddSeparator(DataControlFieldCell cell)
		{
			if (cell.Controls.Count > 0)
			{
				Literal literal = new Literal();
				literal.Text = "&nbsp;";
				cell.Controls.Add(literal);
			}
		}

		/// <summary>Creates an empty <see cref="T:System.Web.UI.WebControls.CommandField" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Web.UI.WebControls.CommandField" />.</returns>
		// Token: 0x06001F60 RID: 8032 RVA: 0x0004D510 File Offset: 0x0004B710
		protected override DataControlField CreateField()
		{
			return new CommandField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.CommandField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.CommandField" /> to.</param>
		// Token: 0x06001F61 RID: 8033 RVA: 0x0004D518 File Offset: 0x0004B718
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			CommandField commandField = (CommandField)newField;
			commandField.CancelImageUrl = this.CancelImageUrl;
			commandField.CancelText = this.CancelText;
			commandField.DeleteImageUrl = this.DeleteImageUrl;
			commandField.DeleteText = this.DeleteText;
			commandField.EditImageUrl = this.EditImageUrl;
			commandField.EditText = this.EditText;
			commandField.InsertImageUrl = this.InsertImageUrl;
			commandField.InsertText = this.InsertText;
			commandField.NewImageUrl = this.NewImageUrl;
			commandField.NewText = this.NewText;
			commandField.SelectImageUrl = this.SelectImageUrl;
			commandField.SelectText = this.SelectText;
			commandField.ShowCancelButton = this.ShowCancelButton;
			commandField.ShowDeleteButton = this.ShowDeleteButton;
			commandField.ShowEditButton = this.ShowEditButton;
			commandField.ShowSelectButton = this.ShowSelectButton;
			commandField.ShowInsertButton = this.ShowInsertButton;
			commandField.UpdateImageUrl = this.UpdateImageUrl;
			commandField.UpdateText = this.UpdateText;
		}

		/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.CommandField" /> object support callbacks.</summary>
		/// <exception cref="T:System.NotSupportedException">The Select button is displayed in the <see cref="T:System.Web.UI.WebControls.CommandField" /> object. The <see cref="T:System.Web.UI.WebControls.CommandField" />  class does support callbacks when the Select button is displayed.</exception>
		// Token: 0x06001F62 RID: 8034 RVA: 0x0004D618 File Offset: 0x0004B818
		public override void ValidateSupportsCallback()
		{
			if (this.ShowSelectButton)
			{
				throw new NotSupportedException(string.Concat(new string[]
				{
					"Callbacks are not supported on CommandField when the select button is enabled because other controls on your page that are dependent on the selected value of '",
					base.Control.ID,
					"' for their rendering will not update in a callback.  Turn callbacks off on '",
					base.Control.ID,
					"'."
				}));
			}
		}
	}
}
