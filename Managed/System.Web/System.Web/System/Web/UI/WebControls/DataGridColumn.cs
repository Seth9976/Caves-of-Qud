using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class for the different column types of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	// Token: 0x02000373 RID: 883
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class DataGridColumn : IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class.</summary>
		// Token: 0x0600219D RID: 8605 RVA: 0x00054924 File Offset: 0x00052B24
		protected DataGridColumn()
		{
			this.viewstate = new StateBag();
		}

		/// <summary>Gets the style properties for the footer section of the column.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the footer section of the column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x00054937 File Offset: 0x00052B37
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TableItemStyle FooterStyle
		{
			get
			{
				if (this.footer_style == null)
				{
					this.footer_style = new TableItemStyle();
					if (this.tracking_viewstate)
					{
						this.footer_style.TrackViewState();
					}
				}
				return this.footer_style;
			}
		}

		/// <summary>Gets or sets the text displayed in the footer section of the column.</summary>
		/// <returns>The text displayed in the footer section of the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x00054965 File Offset: 0x00052B65
		// (set) Token: 0x060021A0 RID: 8608 RVA: 0x0005497C File Offset: 0x00052B7C
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DefaultValue("")]
		public virtual string FooterText
		{
			get
			{
				return this.viewstate.GetString("FooterText", string.Empty);
			}
			set
			{
				this.viewstate["FooterText"] = value;
			}
		}

		/// <summary>Gets or sets the location of an image to display in the header section of the column.</summary>
		/// <returns>The location of an image to display in the header section of the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x0005498F File Offset: 0x00052B8F
		// (set) Token: 0x060021A2 RID: 8610 RVA: 0x000549A6 File Offset: 0x00052BA6
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[UrlProperty]
		public virtual string HeaderImageUrl
		{
			get
			{
				return this.viewstate.GetString("HeaderImageUrl", string.Empty);
			}
			set
			{
				this.viewstate["HeaderImageUrl"] = value;
			}
		}

		/// <summary>Gets the style properties for the header section of the column.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the header section of the column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x000549B9 File Offset: 0x00052BB9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (this.header_style == null)
				{
					this.header_style = new TableItemStyle();
					if (this.tracking_viewstate)
					{
						this.header_style.TrackViewState();
					}
				}
				return this.header_style;
			}
		}

		/// <summary>Gets or sets the text displayed in the header section of the column.</summary>
		/// <returns>The text displayed in the header section of the column. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x000549E7 File Offset: 0x00052BE7
		// (set) Token: 0x060021A5 RID: 8613 RVA: 0x000549FE File Offset: 0x00052BFE
		[WebCategory("Misc")]
		[DefaultValue("")]
		[WebSysDescription("")]
		public virtual string HeaderText
		{
			get
			{
				return this.viewstate.GetString("HeaderText", string.Empty);
			}
			set
			{
				this.viewstate["HeaderText"] = value;
			}
		}

		/// <summary>Gets the style properties for the item cells of the column.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the item cells of the column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x00054A11 File Offset: 0x00052C11
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual TableItemStyle ItemStyle
		{
			get
			{
				if (this.item_style == null)
				{
					this.item_style = new TableItemStyle();
					if (this.tracking_viewstate)
					{
						this.item_style.TrackViewState();
					}
				}
				return this.item_style;
			}
		}

		/// <summary>Gets or sets the name of the field or expression to pass to the <see cref="M:System.Web.UI.WebControls.DataGrid.OnSortCommand(System.Web.UI.WebControls.DataGridSortCommandEventArgs)" /> method when a column is selected for sorting.</summary>
		/// <returns>The name of the field to pass to <see cref="M:System.Web.UI.WebControls.DataGrid.OnSortCommand(System.Web.UI.WebControls.DataGridSortCommandEventArgs)" /> when a column is selected for sorting. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x00054A3F File Offset: 0x00052C3F
		// (set) Token: 0x060021A8 RID: 8616 RVA: 0x00054A56 File Offset: 0x00052C56
		[DefaultValue("")]
		[WebCategory("Misc")]
		[WebSysDescription("")]
		public virtual string SortExpression
		{
			get
			{
				return this.viewstate.GetString("SortExpression", string.Empty);
			}
			set
			{
				this.viewstate["SortExpression"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the column is visible in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>true if the column is visible in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control; otherwise, false. The default value is true.</returns>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x00054A69 File Offset: 0x00052C69
		// (set) Token: 0x060021AA RID: 8618 RVA: 0x00054A7C File Offset: 0x00052C7C
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public bool Visible
		{
			get
			{
				return this.viewstate.GetBool("Visible", true);
			}
			set
			{
				this.viewstate["Visible"] = value;
			}
		}

		/// <summary>Provides the base implementation to reset a column derived from the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class to its initial state.</summary>
		// Token: 0x060021AB RID: 8619 RVA: 0x00054A94 File Offset: 0x00052C94
		public virtual void Initialize()
		{
			if (this.owner != null && this.owner.Site != null)
			{
				this.design = this.owner.Site.DesignMode;
			}
		}

		/// <summary>Provides the base implementation to reset the specified cell from a column derived from the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class to its initial state.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents the cell to reset. </param>
		/// <param name="columnIndex">The column number where the cell is located. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x060021AC RID: 8620 RVA: 0x00054AC4 File Offset: 0x00052CC4
		public virtual void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			if (itemType != ListItemType.Header)
			{
				if (itemType != ListItemType.Footer)
				{
					return;
				}
				string footerText = this.FooterText;
				if (footerText.Length > 0)
				{
					cell.Text = footerText;
					return;
				}
				cell.Text = "&nbsp;";
				return;
			}
			else
			{
				bool flag = false;
				string sortExpression = this.SortExpression;
				if (this.owner != null && sortExpression.Length > 0)
				{
					flag = this.owner.AllowSorting;
				}
				string headerImageUrl = this.HeaderImageUrl;
				if (headerImageUrl.Length > 0)
				{
					if (flag)
					{
						ImageButton imageButton = new ImageButton();
						imageButton.ImageUrl = headerImageUrl;
						imageButton.CommandName = "Sort";
						imageButton.CommandArgument = sortExpression;
						cell.Controls.Add(imageButton);
						return;
					}
					Image image = new Image();
					image.ImageUrl = headerImageUrl;
					cell.Controls.Add(image);
					return;
				}
				else
				{
					if (flag)
					{
						LinkButton linkButton = new DataGridColumn.ForeColorLinkButton();
						linkButton.Text = this.HeaderText;
						linkButton.CommandName = "Sort";
						linkButton.CommandArgument = sortExpression;
						cell.Controls.Add(linkButton);
						return;
					}
					string headerText = this.HeaderText;
					if (headerText.Length > 0)
					{
						cell.Text = headerText;
						return;
					}
					cell.Text = "&nbsp;";
					return;
				}
			}
		}

		/// <summary>Returns the string representation of the column.</summary>
		/// <returns>Returns <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x060021AD RID: 8621 RVA: 0x0000EE63 File Offset: 0x0000D063
		public override string ToString()
		{
			return string.Empty;
		}

		/// <summary>Gets a value that indicates whether the column is in design mode.</summary>
		/// <returns>true if the column is in design mode; otherwise, false.</returns>
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x00054BE4 File Offset: 0x00052DE4
		protected bool DesignMode
		{
			get
			{
				return this.design;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that the column is a member of.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that the column is a member of.</returns>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x00054BEC File Offset: 0x00052DEC
		protected DataGrid Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x00054BF4 File Offset: 0x00052DF4
		internal TableItemStyle GetStyle(ListItemType type)
		{
			if (type == ListItemType.Header)
			{
				return this.header_style;
			}
			if (type == ListItemType.Footer)
			{
				return this.footer_style;
			}
			return this.item_style;
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x00054C11 File Offset: 0x00052E11
		internal void Set_Owner(DataGrid value)
		{
			this.owner = value;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.StateBag" /> object that allows a column derived from the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class to store its properties.</summary>
		/// <returns>The <see cref="T:System.Web.UI.StateBag" /> for the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />.</returns>
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x00054C1A File Offset: 0x00052E1A
		protected StateBag ViewState
		{
			get
			{
				return this.viewstate;
			}
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.Design.WebControls.DataGridDesigner.OnColumnsChanged" /> method.</summary>
		// Token: 0x060021B3 RID: 8627 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void OnColumnChanged()
		{
		}

		/// <summary>Loads previously saved state.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object to restore.</param>
		// Token: 0x060021B4 RID: 8628 RVA: 0x00054C22 File Offset: 0x00052E22
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>Returns an object containing state changes.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x060021B5 RID: 8629 RVA: 0x00054C2B File Offset: 0x00052E2B
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Starts tracking state changes.</summary>
		// Token: 0x060021B6 RID: 8630 RVA: 0x00054C33 File Offset: 0x00052E33
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Gets a value that indicates whether the column is tracking view state changes.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x00054C3B File Offset: 0x00052E3B
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>Loads the state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />. </param>
		// Token: 0x060021B8 RID: 8632 RVA: 0x00054C44 File Offset: 0x00052E44
		protected virtual void LoadViewState(object savedState)
		{
			object[] array = savedState as object[];
			if (array == null)
			{
				return;
			}
			if (array[0] != null)
			{
				this.viewstate.LoadViewState(array[0]);
			}
			if (array[1] != null)
			{
				this.FooterStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				this.HeaderStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				this.ItemStyle.LoadViewState(array[3]);
			}
		}

		/// <summary>Saves the current state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />.</returns>
		// Token: 0x060021B9 RID: 8633 RVA: 0x00054CA8 File Offset: 0x00052EA8
		protected virtual object SaveViewState()
		{
			object[] array = new object[4];
			array[0] = this.viewstate.SaveViewState();
			if (this.footer_style != null)
			{
				array[1] = this.footer_style.SaveViewState();
			}
			if (this.header_style != null)
			{
				array[2] = this.header_style.SaveViewState();
			}
			if (this.item_style != null)
			{
				array[3] = this.item_style.SaveViewState();
			}
			return array;
		}

		/// <summary>Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x060021BA RID: 8634 RVA: 0x00054D10 File Offset: 0x00052F10
		protected virtual void TrackViewState()
		{
			this.tracking_viewstate = true;
			this.viewstate.TrackViewState();
			if (this.footer_style != null)
			{
				this.footer_style.TrackViewState();
			}
			if (this.header_style != null)
			{
				this.header_style.TrackViewState();
			}
			if (this.item_style != null)
			{
				this.item_style.TrackViewState();
			}
		}

		/// <summary>Gets a value that determines whether the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object is marked to save its state.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> is marked; otherwise, false.</returns>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x00054D68 File Offset: 0x00052F68
		protected bool IsTrackingViewState
		{
			get
			{
				return this.tracking_viewstate;
			}
		}

		// Token: 0x040018F0 RID: 6384
		private DataGrid owner;

		// Token: 0x040018F1 RID: 6385
		private StateBag viewstate;

		// Token: 0x040018F2 RID: 6386
		private bool tracking_viewstate;

		// Token: 0x040018F3 RID: 6387
		private bool design;

		// Token: 0x040018F4 RID: 6388
		private TableItemStyle footer_style;

		// Token: 0x040018F5 RID: 6389
		private TableItemStyle header_style;

		// Token: 0x040018F6 RID: 6390
		private TableItemStyle item_style;

		// Token: 0x02000374 RID: 884
		internal class ForeColorLinkButton : LinkButton
		{
			// Token: 0x060021BC RID: 8636 RVA: 0x00054D70 File Offset: 0x00052F70
			private Color GetForeColor(WebControl control)
			{
				if (control == null)
				{
					return Color.Empty;
				}
				if (control is Table)
				{
					return control.ControlStyle.ForeColor;
				}
				Color foreColor = control.ControlStyle.ForeColor;
				if (foreColor != Color.Empty)
				{
					return foreColor;
				}
				return this.GetForeColor((WebControl)control.Parent);
			}

			// Token: 0x060021BD RID: 8637 RVA: 0x00054DC8 File Offset: 0x00052FC8
			protected internal override void Render(HtmlTextWriter writer)
			{
				Color foreColor = this.GetForeColor(this);
				if (foreColor != Color.Empty)
				{
					this.ForeColor = foreColor;
				}
				base.Render(writer);
			}
		}
	}
}
