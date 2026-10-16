using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a row in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	// Token: 0x0200041B RID: 1051
	[ParseChildren(true, "Cells")]
	[ToolboxItem("")]
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Cells")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableRow : WebControl
	{
		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06002F68 RID: 12136 RVA: 0x0007B23A File Offset: 0x0007943A
		// (set) Token: 0x06002F69 RID: 12137 RVA: 0x0007B242 File Offset: 0x00079442
		internal TableRowCollection Container { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableRow" /> class.</summary>
		// Token: 0x06002F6A RID: 12138 RVA: 0x0007B24B File Offset: 0x0007944B
		public TableRow()
			: base(HtmlTextWriterTag.Tr)
		{
			base.AutoID = false;
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06002F6B RID: 12139 RVA: 0x0007B25C File Offset: 0x0007945C
		internal bool TableRowSectionSet
		{
			get
			{
				return this.tableRowSectionSet;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects that represent the cells of a row in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> object that represents a collection of cells from a row of a <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06002F6C RID: 12140 RVA: 0x0007B264 File Offset: 0x00079464
		[WebSysDescription("")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[MergableProperty(false)]
		[WebCategory("Layout")]
		public virtual TableCellCollection Cells
		{
			get
			{
				if (this.cells == null)
				{
					this.cells = new TableCellCollection(this);
				}
				return this.cells;
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the contents in the row.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default value is NotSet.</returns>
		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06002F6D RID: 12141 RVA: 0x0007B280 File Offset: 0x00079480
		// (set) Token: 0x06002F6E RID: 12142 RVA: 0x0007B297 File Offset: 0x00079497
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return this.TableItemStyle.HorizontalAlign;
			}
			set
			{
				this.TableItemStyle.HorizontalAlign = value;
			}
		}

		/// <summary>Gets or sets the vertical alignment of the contents in the row.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.VerticalAlign" /> values. The default value is NotSet.</returns>
		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06002F6F RID: 12143 RVA: 0x0007B2A5 File Offset: 0x000794A5
		// (set) Token: 0x06002F70 RID: 12144 RVA: 0x0007B2BC File Offset: 0x000794BC
		[DefaultValue(VerticalAlign.NotSet)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual VerticalAlign VerticalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return VerticalAlign.NotSet;
				}
				return this.TableItemStyle.VerticalAlign;
			}
			set
			{
				this.TableItemStyle.VerticalAlign = value;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x0007A9ED File Offset: 0x00078BED
		private TableItemStyle TableItemStyle
		{
			get
			{
				return base.ControlStyle as TableItemStyle;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object for the <see cref="T:System.Web.UI.WebControls.TableRow" /> control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object that contains the <see cref="T:System.Web.UI.WebControls.TableRow" /> control's child server controls.</returns>
		// Token: 0x06002F73 RID: 12147 RVA: 0x0007B2CA File Offset: 0x000794CA
		protected override ControlCollection CreateControlCollection()
		{
			return new TableRow.CellControlCollection(this);
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object for the <see cref="T:System.Web.UI.WebControls.TableRow" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that specifies the style properties for the <see cref="T:System.Web.UI.WebControls.TableRow" /> control.The <see cref="M:System.Web.UI.WebControls.TableRow.CreateControlCollection" /> method is primarily of interest to control developers extending the functionality of the <see cref="T:System.Web.UI.WebControls.TableRow" /> control.</returns>
		// Token: 0x06002F74 RID: 12148 RVA: 0x00056FF6 File Offset: 0x000551F6
		protected override Style CreateControlStyle()
		{
			return new TableItemStyle(this.ViewState);
		}

		/// <summary>Gets or sets the location for a <see cref="T:System.Web.UI.WebControls.TableRow" /> object in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableRowSection" /> value. The default is <see cref="F:System.Web.UI.WebControls.TableRowSection.TableBody" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="T:System.Web.UI.WebControls.TableRowSection" /> is not valid.</exception>
		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06002F75 RID: 12149 RVA: 0x0007B2D4 File Offset: 0x000794D4
		// (set) Token: 0x06002F76 RID: 12150 RVA: 0x0007B300 File Offset: 0x00079500
		[DefaultValue(TableRowSection.TableBody)]
		public virtual TableRowSection TableSection
		{
			get
			{
				object obj = this.ViewState["TableSection"];
				if (obj != null)
				{
					return (TableRowSection)obj;
				}
				return TableRowSection.TableBody;
			}
			set
			{
				if (value < TableRowSection.TableHeader || value > TableRowSection.TableFooter)
				{
					throw new ArgumentOutOfRangeException("TableSection");
				}
				this.ViewState["TableSection"] = (int)value;
				this.tableRowSectionSet = true;
				TableRowCollection container = this.Container;
				if (container != null)
				{
					container.RowTableSectionSet();
				}
			}
		}

		// Token: 0x04001BE6 RID: 7142
		private TableCellCollection cells;

		// Token: 0x04001BE7 RID: 7143
		private bool tableRowSectionSet;

		/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects that are the cells of a <see cref="T:System.Web.UI.WebControls.TableRow" /> control. </summary>
		// Token: 0x0200041C RID: 1052
		protected class CellControlCollection : ControlCollection
		{
			// Token: 0x06002F77 RID: 12151 RVA: 0x000287B2 File Offset: 0x000269B2
			internal CellControlCollection(TableRow owner)
				: base(owner)
			{
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" /> collection.</summary>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" />. </param>
			/// <exception cref="T:System.ArgumentException">The added <see cref="T:System.Web.UI.Control" /> must be of the type <see cref="T:System.Web.UI.WebControls.TableCell" />. </exception>
			// Token: 0x06002F78 RID: 12152 RVA: 0x0007B34D File Offset: 0x0007954D
			public override void Add(Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is TableCell))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an TableCell instance."));
				}
				base.Add(child);
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" /> collection. The new control is added to the array at the specified index location.</summary>
			/// <param name="index">The location in the array to add the child control. </param>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" />. </param>
			/// <exception cref="T:System.ArgumentException">The added <see cref="T:System.Web.UI.Control" /> must be of the type <see cref="T:System.Web.UI.WebControls.TableCell" />.</exception>
			// Token: 0x06002F79 RID: 12153 RVA: 0x0007B381 File Offset: 0x00079581
			public override void AddAt(int index, Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is TableCell))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an TableCell instance."));
				}
				base.AddAt(index, child);
			}
		}
	}
}
