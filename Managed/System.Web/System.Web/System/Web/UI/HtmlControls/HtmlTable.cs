using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access on the server to the HTML &lt;table&gt; element.</summary>
	// Token: 0x0200026C RID: 620
	[ParseChildren(true, "Rows")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTable : HtmlContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> class.</summary>
		// Token: 0x06001978 RID: 6520 RVA: 0x000423B4 File Offset: 0x000405B4
		public HtmlTable()
			: base("table")
		{
		}

		/// <summary>Gets or sets the alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control in relation to other elements on the Web page.</summary>
		/// <returns>The alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control in relation to other elements on the Web page. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x000423C4 File Offset: 0x000405C4
		// (set) Token: 0x0600197A RID: 6522 RVA: 0x00040044 File Offset: 0x0003E244
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
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

		/// <summary>Gets or sets the background color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The background color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x000423EC File Offset: 0x000405EC
		// (set) Token: 0x0600197C RID: 6524 RVA: 0x00042414 File Offset: 0x00040614
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("")]
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

		/// <summary>Gets or sets the width (in pixels) of the border of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The width (in pixels) of the border of an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default is -1, which indicates that the border width is not set.</returns>
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x0004243C File Offset: 0x0004063C
		// (set) Token: 0x0600197E RID: 6526 RVA: 0x00040DEE File Offset: 0x0003EFEE
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public int Border
		{
			get
			{
				string text = base.Attributes["border"];
				if (text != null)
				{
					return Convert.ToInt32(text);
				}
				return -1;
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("border");
					return;
				}
				base.Attributes["border"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the border color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The border color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x00042468 File Offset: 0x00040668
		// (set) Token: 0x06001980 RID: 6528 RVA: 0x00042490 File Offset: 0x00040690
		[WebSysDescription("")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
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

		/// <summary>Gets or sets the amount of space (in pixels) between the contents of a cell and the cell's border in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The amount of space (in pixels) between the contents of a cell and the cell's border in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x000424B8 File Offset: 0x000406B8
		// (set) Token: 0x06001982 RID: 6530 RVA: 0x000424E1 File Offset: 0x000406E1
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public int CellPadding
		{
			get
			{
				string text = base.Attributes["cellpadding"];
				if (text != null)
				{
					return Convert.ToInt32(text);
				}
				return -1;
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("cellpadding");
					return;
				}
				base.Attributes["cellpadding"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the amount of space (in pixels) between adjacent cells in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The amount of space (in pixels) between adjacent cells in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x00042514 File Offset: 0x00040714
		// (set) Token: 0x06001984 RID: 6532 RVA: 0x0004253D File Offset: 0x0004073D
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public int CellSpacing
		{
			get
			{
				string text = base.Attributes["cellspacing"];
				if (text != null)
				{
					return Convert.ToInt32(text);
				}
				return -1;
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("cellspacing");
					return;
				}
				base.Attributes["cellspacing"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the height of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The height of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x00042570 File Offset: 0x00040770
		// (set) Token: 0x06001986 RID: 6534 RVA: 0x00042598 File Offset: 0x00040798
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

		/// <summary>Gets or sets the content between the opening and closing tags of the control, without automatically converting special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
		/// <returns>The content between the opening and closing tags of the control.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to read from or assign a value to this property. </exception>
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x00003A0D File Offset: 0x00001C0D
		// (set) Token: 0x06001988 RID: 6536 RVA: 0x00003A0D File Offset: 0x00001C0D
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

		/// <summary>Gets or sets the content between the opening and closing tags of the control, with automatic conversion of special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
		/// <returns>The content between the opening and closing tags of the control.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to read from or assign a value to this property. </exception>
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x00003A0D File Offset: 0x00001C0D
		// (set) Token: 0x0600198A RID: 6538 RVA: 0x00003A0D File Offset: 0x00001C0D
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

		/// <summary>Gets an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection that contains all the rows in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> that contains all the rows in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x000425BF File Offset: 0x000407BF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual HtmlTableRowCollection Rows
		{
			get
			{
				if (this._rows == null)
				{
					this._rows = new HtmlTableRowCollection(this);
				}
				return this._rows;
			}
		}

		/// <summary>Gets or sets the width of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
		/// <returns>The width of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x000425DC File Offset: 0x000407DC
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x00042604 File Offset: 0x00040804
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public string Width
		{
			get
			{
				string text = base.Attributes["width"];
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
					base.Attributes.Remove("width");
					return;
				}
				base.Attributes["width"] = value;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object for the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control's child server controls.</returns>
		// Token: 0x0600198E RID: 6542 RVA: 0x0004262B File Offset: 0x0004082B
		protected override ControlCollection CreateControlCollection()
		{
			return new HtmlTable.HtmlTableRowControlCollection(this);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control's child controls to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content. </param>
		// Token: 0x0600198F RID: 6543 RVA: 0x00042634 File Offset: 0x00040834
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

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control's end tag.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content. </param>
		// Token: 0x06001990 RID: 6544 RVA: 0x00042676 File Offset: 0x00040876
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.WriteLine();
			writer.WriteEndTag(this.TagName);
			writer.WriteLine();
		}

		// Token: 0x0400163E RID: 5694
		private HtmlTableRowCollection _rows;

		/// <summary>Represents a collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects that are the rows of an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. </summary>
		// Token: 0x0200026D RID: 621
		protected class HtmlTableRowControlCollection : ControlCollection
		{
			// Token: 0x06001991 RID: 6545 RVA: 0x000287B2 File Offset: 0x000269B2
			internal HtmlTableRowControlCollection(HtmlTable owner)
				: base(owner)
			{
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />.</exception>
			// Token: 0x06001992 RID: 6546 RVA: 0x00042690 File Offset: 0x00040890
			public override void Add(Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is HtmlTableRow))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an HtmlTableRow instance."));
				}
				base.Add(child);
			}

			/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection. The new control is added to the array at the specified index location.</summary>
			/// <param name="index">The location in the array at which to add the child control. </param>
			/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
			/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />. </exception>
			// Token: 0x06001993 RID: 6547 RVA: 0x000426C4 File Offset: 0x000408C4
			public override void AddAt(int index, Control child)
			{
				if (child == null)
				{
					throw new NullReferenceException("null");
				}
				if (!(child is HtmlTableRow))
				{
					throw new ArgumentException("child", global::Locale.GetText("Must be an HtmlTableRow instance."));
				}
				base.AddAt(index, child);
			}
		}
	}
}
