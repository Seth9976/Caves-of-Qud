using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a table on a Web page.</summary>
	// Token: 0x02000413 RID: 1043
	[DefaultProperty("Rows")]
	[Designer("System.Web.UI.Design.WebControls.TableDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ParseChildren(true, "Rows")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Table : WebControl, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Table" /> class.</summary>
		// Token: 0x06002F04 RID: 12036 RVA: 0x0007A4C0 File Offset: 0x000786C0
		public Table()
			: base(HtmlTextWriterTag.Table)
		{
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06002F05 RID: 12037 RVA: 0x0007A4CA File Offset: 0x000786CA
		// (set) Token: 0x06002F06 RID: 12038 RVA: 0x0007A4D2 File Offset: 0x000786D2
		internal bool GenerateTableSections
		{
			get
			{
				return this.generateTableSections;
			}
			set
			{
				this.generateTableSections = value;
			}
		}

		/// <summary>Gets or sets the URL of the background image to display behind the <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>The URL of the background image for the <see cref="T:System.Web.UI.WebControls.Table" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x0007A4DB File Offset: 0x000786DB
		// (set) Token: 0x06002F08 RID: 12040 RVA: 0x0007A4F6 File Offset: 0x000786F6
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return string.Empty;
				}
				return this.TableStyle.BackImageUrl;
			}
			set
			{
				this.TableStyle.BackImageUrl = value;
			}
		}

		/// <summary>Gets or sets the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This property is provided to make the control more accessible to users of Assistive Technology devices.</summary>
		/// <returns>A string that represents the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.Table" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06002F09 RID: 12041 RVA: 0x0007A504 File Offset: 0x00078704
		// (set) Token: 0x06002F0A RID: 12042 RVA: 0x00044FA6 File Offset: 0x000431A6
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Accessibility")]
		[Localizable(true)]
		public virtual string Caption
		{
			get
			{
				object obj = this.ViewState["Caption"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("Caption");
					return;
				}
				this.ViewState["Caption"] = value;
			}
		}

		/// <summary>Gets or sets the horizontal or vertical position of the HTML caption element in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This property is provided to make the control more accessible to users of Assistive Technology devices.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. The default value is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. </exception>
		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x0007A534 File Offset: 0x00078734
		// (set) Token: 0x06002F0C RID: 12044 RVA: 0x00044FE0 File Offset: 0x000431E0
		[DefaultValue(TableCaptionAlign.NotSet)]
		[WebCategory("Accessibility")]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				object obj = this.ViewState["CaptionAlign"];
				if (obj != null)
				{
					return (TableCaptionAlign)obj;
				}
				return TableCaptionAlign.NotSet;
			}
			set
			{
				if (value < TableCaptionAlign.NotSet || value > TableCaptionAlign.Right)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("Invalid TableCaptionAlign value."));
				}
				this.ViewState["CaptionAlign"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border. </summary>
		/// <returns>The amount of space, in pixels, between the contents of a cell and the cell's border. The default value is -1, which indicates that the property has not been set.</returns>
		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x0007A55D File Offset: 0x0007875D
		// (set) Token: 0x06002F0E RID: 12046 RVA: 0x0007A574 File Offset: 0x00078774
		[WebCategory("Appearance")]
		[DefaultValue(-1)]
		[WebSysDescription("")]
		public virtual int CellPadding
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return this.TableStyle.CellPadding;
			}
			set
			{
				this.TableStyle.CellPadding = value;
			}
		}

		/// <summary>Gets or sets the amount of space between cells. </summary>
		/// <returns>The amount of space, in pixels, between cells. The default value is -1, which indicates that the property has not been set.</returns>
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x0007A582 File Offset: 0x00078782
		// (set) Token: 0x06002F10 RID: 12048 RVA: 0x0007A599 File Offset: 0x00078799
		[WebSysDescription("")]
		[DefaultValue(-1)]
		[WebCategory("Appearance")]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return this.TableStyle.CellSpacing;
			}
			set
			{
				this.TableStyle.CellSpacing = value;
			}
		}

		/// <summary>Gets or sets the grid line style to display in the <see cref="T:System.Web.UI.WebControls.Table" /> control. </summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> enumeration values. The default value is None.</returns>
		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x0007A5A7 File Offset: 0x000787A7
		// (set) Token: 0x06002F12 RID: 12050 RVA: 0x0007A5BE File Offset: 0x000787BE
		[DefaultValue(GridLines.None)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.None;
				}
				return this.TableStyle.GridLines;
			}
			set
			{
				this.TableStyle.GridLines = value;
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the <see cref="T:System.Web.UI.WebControls.Table" /> control on the page.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. The default value is NotSet.</returns>
		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x0007A5CC File Offset: 0x000787CC
		// (set) Token: 0x06002F14 RID: 12052 RVA: 0x0007A5E3 File Offset: 0x000787E3
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DefaultValue(HorizontalAlign.NotSet)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return this.TableStyle.HorizontalAlign;
			}
			set
			{
				this.TableStyle.HorizontalAlign = value;
			}
		}

		/// <summary>Gets the collection of rows in the <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> that contains the <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in the <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06002F15 RID: 12053 RVA: 0x0007A5F1 File Offset: 0x000787F1
		[WebSysDescription("")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual TableRowCollection Rows
		{
			get
			{
				if (this.rows == null)
				{
					this.rows = new TableRowCollection(this);
				}
				return this.rows;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x0007A60D File Offset: 0x0007880D
		private TableStyle TableStyle
		{
			get
			{
				return base.ControlStyle as TableStyle;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06002F17 RID: 12055 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" />.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x06002F18 RID: 12056 RVA: 0x00064ACD File Offset: 0x00062CCD
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object to hold the <see cref="T:System.Web.UI.WebControls.TableRow" /> controls of the current <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the <see cref="T:System.Web.UI.WebControls.TableRow" /> controls of the current <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
		// Token: 0x06002F19 RID: 12057 RVA: 0x0007A61A File Offset: 0x0007881A
		protected override ControlCollection CreateControlCollection()
		{
			return new Table.RowControlCollection(this);
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that contains the properties that define the appearance of the <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
		// Token: 0x06002F1A RID: 12058 RVA: 0x0004C992 File Offset: 0x0004AB92
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}

		/// <summary>Renders the rows in the table control to the specified writer.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		/// <exception cref="T:System.Web.HttpException">The table sections are not in order.</exception>
		// Token: 0x06002F1B RID: 12059 RVA: 0x0007A624 File Offset: 0x00078824
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			TableRowSection tableRowSection = TableRowSection.TableHeader;
			bool flag = false;
			if (this.Rows.Count > 0)
			{
				foreach (object obj in this.Rows)
				{
					TableRow tableRow = (TableRow)obj;
					if (this.generateTableSections)
					{
						TableRowSection tableSection = tableRow.TableSection;
						if (tableSection < tableRowSection)
						{
							throw new HttpException("The table " + this.ID + " must contain row sections in order of header, body, then footer.");
						}
						if (tableRowSection != tableSection)
						{
							if (flag)
							{
								writer.RenderEndTag();
								flag = false;
							}
							tableRowSection = tableSection;
						}
						if (!flag)
						{
							switch (tableSection)
							{
							case TableRowSection.TableHeader:
								writer.RenderBeginTag(HtmlTextWriterTag.Thead);
								break;
							case TableRowSection.TableBody:
								writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
								break;
							case TableRowSection.TableFooter:
								writer.RenderBeginTag(HtmlTextWriterTag.Tfoot);
								break;
							}
							flag = true;
						}
					}
					if (tableRow != null)
					{
						tableRow.RenderControl(writer);
					}
				}
				if (flag)
				{
					writer.RenderEndTag();
				}
			}
		}

		/// <summary>Renders the HTML opening tag of the <see cref="T:System.Web.UI.WebControls.Table" /> control to the specified writer. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002F1C RID: 12060 RVA: 0x0007A720 File Offset: 0x00078920
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			string caption = this.Caption;
			if (caption.Length > 0)
			{
				TableCaptionAlign captionAlign = this.CaptionAlign;
				if (captionAlign != TableCaptionAlign.NotSet)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Align, captionAlign.ToString());
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Caption);
				writer.Write(caption);
				writer.RenderEndTag();
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06002F1D RID: 12061 RVA: 0x0007A777 File Offset: 0x00078977
		void IPostBackEventHandler.RaisePostBackEvent(string argument)
		{
			this.RaisePostBackEvent(argument);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.Table" /> control when a form is posted back to the server.</summary>
		/// <param name="argument">A <see cref="T:System.String" /> that represents the argument for the event. </param>
		// Token: 0x06002F1E RID: 12062 RVA: 0x0007A780 File Offset: 0x00078980
		protected virtual void RaisePostBackEvent(string argument)
		{
			base.ValidateEvent(this.UniqueID, argument);
		}

		// Token: 0x04001BDF RID: 7135
		private TableRowCollection rows;

		// Token: 0x04001BE0 RID: 7136
		private bool generateTableSections;

		/// <summary>Represents the collection of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
		// Token: 0x02000414 RID: 1044
		protected class RowControlCollection : ControlCollection
		{
			// Token: 0x06002F1F RID: 12063 RVA: 0x000287B2 File Offset: 0x000269B2
			internal RowControlCollection(Table owner)
				: base(owner)
			{
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" /> collection.</summary>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" />. </param>
			/// <exception cref="T:System.ArgumentException">The object specified by <paramref name="child" /> is not a <see cref="T:System.Web.UI.WebControls.TableRow" />. </exception>
			// Token: 0x06002F20 RID: 12064 RVA: 0x0007A78F File Offset: 0x0007898F
			public override void Add(Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is TableRow))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an TableRow instance."));
				}
				base.Add(child);
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" /> collection. The new control is added to the array at the specified index location.</summary>
			/// <param name="index">The location in the array at which to add the child control. </param>
			/// <param name="child">The Control object to add to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" />. </param>
			/// <exception cref="T:System.Web.HttpException">The control does not allow child controls. </exception>
			/// <exception cref="T:System.ArgumentException">The child value is null. -or- The object is not a <see cref="T:System.Web.UI.WebControls.TableRow" />.</exception>
			// Token: 0x06002F21 RID: 12065 RVA: 0x0007A7C3 File Offset: 0x000789C3
			public override void AddAt(int index, Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is TableRow))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an TableRow instance."));
				}
				base.AddAt(index, child);
			}
		}
	}
}
