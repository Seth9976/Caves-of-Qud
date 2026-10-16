using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Represents the &lt;tr&gt; HTML element in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	// Token: 0x02000270 RID: 624
	[ParseChildren(true, "Cells")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTableRow : HtmlContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
		// Token: 0x060019B7 RID: 6583 RVA: 0x00042A02 File Offset: 0x00040C02
		public HtmlTableRow()
			: base("tr")
		{
		}

		/// <summary>Gets or sets the horizontal alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The horizontal alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00042A10 File Offset: 0x00040C10
		// (set) Token: 0x060019B9 RID: 6585 RVA: 0x00040044 File Offset: 0x0003E244
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public string Align
		{
			get
			{
				string text = base.Attributes["align"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("align");
					return;
				}
				base.Attributes["align"] = value;
			}
		}

		/// <summary>Gets or sets the background color of the row represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
		/// <returns>The background color of the row represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />.</returns>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x00042A38 File Offset: 0x00040C38
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x00042414 File Offset: 0x00040614
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public string BgColor
		{
			get
			{
				string text = base.Attributes["bgcolor"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("bgcolor");
					return;
				}
				base.Attributes["bgcolor"] = value;
			}
		}

		/// <summary>Gets or sets the border color of the row represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
		/// <returns>The border color of the row represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />.</returns>
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x00042A60 File Offset: 0x00040C60
		// (set) Token: 0x060019BD RID: 6589 RVA: 0x00042490 File Offset: 0x00040690
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public string BorderColor
		{
			get
			{
				string text = base.Attributes["bordercolor"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("bordercolor");
					return;
				}
				base.Attributes["bordercolor"] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects that represent the cells contained in a row of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> that contains the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x00042A88 File Offset: 0x00040C88
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual HtmlTableCellCollection Cells
		{
			get
			{
				if (this._cells == null)
				{
					this._cells = new HtmlTableCellCollection(this);
				}
				return this._cells;
			}
		}

		/// <summary>Gets or sets the height (in pixels) of the row represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
		/// <returns>The height (in pixels) of the row represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x00042AA4 File Offset: 0x00040CA4
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x00042598 File Offset: 0x00040798
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public string Height
		{
			get
			{
				string text = base.Attributes["height"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("height");
					return;
				}
				base.Attributes["height"] = value;
			}
		}

		/// <summary>Gets or sets the content between the opening and closing tags of the control without automatically converting special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
		/// <returns>The content between the opening and closing tags of the control.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to read from or assign a value to this property. </exception>
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x00003A0D File Offset: 0x00001C0D
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x00003A0D File Offset: 0x00001C0D
		public override string InnerHtml
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the content between the opening and closing tags of the control with automatic conversion of special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
		/// <returns>The content between the opening and closing tags of the control.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to read from or assign a value to this property. </exception>
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x00003A0D File Offset: 0x00001C0D
		// (set) Token: 0x060019C4 RID: 6596 RVA: 0x00003A0D File Offset: 0x00001C0D
		public override string InnerText
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the vertical alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The vertical alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00042ACC File Offset: 0x00040CCC
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x00042900 File Offset: 0x00040B00
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public string VAlign
		{
			get
			{
				string text = base.Attributes["valign"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("valign");
					return;
				}
				base.Attributes["valign"] = value;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00042AF4 File Offset: 0x00040CF4
		private int Count
		{
			get
			{
				if (this._cells != null)
				{
					return this._cells.Count;
				}
				return 0;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object for the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control's child server controls.</returns>
		// Token: 0x060019C8 RID: 6600 RVA: 0x00042B0B File Offset: 0x00040D0B
		protected override ControlCollection CreateControlCollection()
		{
			return new HtmlTableRow.HtmlTableCellControlCollection(this);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control's child controls to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the rendered content.</param>
		// Token: 0x060019C9 RID: 6601 RVA: 0x00042B14 File Offset: 0x00040D14
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			if (this.HasControls())
			{
				int num = writer.Indent;
				writer.Indent = num + 1;
				base.RenderChildren(writer);
				num = writer.Indent;
				writer.Indent = num - 1;
				writer.WriteLine();
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control's end tag.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the rendered content.</param>
		// Token: 0x060019CA RID: 6602 RVA: 0x00042B56 File Offset: 0x00040D56
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			if (this.Count == 0)
			{
				writer.WriteLine();
			}
			writer.WriteEndTag(this.TagName);
			if (writer.Indent == 0)
			{
				writer.WriteLine();
			}
		}

		// Token: 0x04001640 RID: 5696
		private HtmlTableCellCollection _cells;

		/// <summary>Represents a collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects that are the cells of an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control. </summary>
		// Token: 0x02000271 RID: 625
		protected class HtmlTableCellControlCollection : ControlCollection
		{
			// Token: 0x060019CB RID: 6603 RVA: 0x000287B2 File Offset: 0x000269B2
			internal HtmlTableCellControlCollection(HtmlTableRow owner)
				: base(owner)
			{
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</exception>
			// Token: 0x060019CC RID: 6604 RVA: 0x00042B80 File Offset: 0x00040D80
			public override void Add(Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is HtmlTableCell))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an HtmlTableCell instance."));
				}
				base.Add(child);
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection at the specified index location.</summary>
			/// <param name="index">The location in the array at which to add the child control. </param>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
			/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</exception>
			// Token: 0x060019CD RID: 6605 RVA: 0x00042BB4 File Offset: 0x00040DB4
			public override void AddAt(int index, Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is HtmlTableCell))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an HtmlTableCell instance."));
				}
				base.AddAt(index, child);
			}
		}
	}
}
