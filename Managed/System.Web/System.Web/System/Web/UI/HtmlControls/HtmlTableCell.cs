using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Represents the &lt;td&gt; and &lt;th&gt; HTML elements in an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object.</summary>
	// Token: 0x0200026E RID: 622
	[ConstructorNeedsTag(true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTableCell : HtmlContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class using default values.</summary>
		// Token: 0x06001994 RID: 6548 RVA: 0x000426F9 File Offset: 0x000408F9
		public HtmlTableCell()
			: base("td")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class, using the specified tag name.</summary>
		/// <param name="tagName">The element name of the tag. </param>
		// Token: 0x06001995 RID: 6549 RVA: 0x00042706 File Offset: 0x00040906
		public HtmlTableCell(string tagName)
			: base(tagName)
		{
		}

		/// <summary>Gets or sets the horizontal alignment of the content in the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The horizontal alignment of the content in the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x00042710 File Offset: 0x00040910
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x00040044 File Offset: 0x0003E244
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

		/// <summary>Gets or sets the background color of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The background color of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</returns>
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x00042738 File Offset: 0x00040938
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x00042414 File Offset: 0x00040614
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Gets or sets the border color of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The border color of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</returns>
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x00042760 File Offset: 0x00040960
		// (set) Token: 0x0600199B RID: 6555 RVA: 0x00042490 File Offset: 0x00040690
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
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

		/// <summary>Gets or sets the number of columns occupied by a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The number of columns occupied by the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x00042788 File Offset: 0x00040988
		// (set) Token: 0x0600199D RID: 6557 RVA: 0x000427B1 File Offset: 0x000409B1
		[WebCategory("Layout")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public int ColSpan
		{
			get
			{
				string text = base.Attributes["colspan"];
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
					base.Attributes.Remove("colspan");
					return;
				}
				base.Attributes["colspan"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the height (in pixels) of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The height (in pixels) of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x000427E4 File Offset: 0x000409E4
		// (set) Token: 0x0600199F RID: 6559 RVA: 0x0004280C File Offset: 0x00040A0C
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
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
					base.Attributes.Remove("align");
					return;
				}
				base.Attributes["height"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class automatically continues on the next line when it reaches the end of the cell.</summary>
		/// <returns>true if the text does not automatically wrap in the cell; otherwise, false. The default value is false.</returns>
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00042833 File Offset: 0x00040A33
		// (set) Token: 0x060019A1 RID: 6561 RVA: 0x0004284F File Offset: 0x00040A4F
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
		[WebSysDescription("")]
		public bool NoWrap
		{
			get
			{
				return base.Attributes["nowrap"] == "nowrap";
			}
			set
			{
				if (value)
				{
					base.Attributes["nowrap"] = "nowrap";
					return;
				}
				base.Attributes.Remove("nowrap");
			}
		}

		/// <summary>Gets or sets the number of rows occupied by a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The number of rows occupied by a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x0004287C File Offset: 0x00040A7C
		// (set) Token: 0x060019A3 RID: 6563 RVA: 0x000428A5 File Offset: 0x00040AA5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Layout")]
		[DefaultValue("")]
		[WebSysDescription("")]
		public int RowSpan
		{
			get
			{
				string text = base.Attributes["rowspan"];
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
					base.Attributes.Remove("rowspan");
					return;
				}
				base.Attributes["rowspan"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the vertical alignment for the content of a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The vertical alignment for the content of a cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x000428D8 File Offset: 0x00040AD8
		// (set) Token: 0x060019A5 RID: 6565 RVA: 0x00042900 File Offset: 0x00040B00
		[WebSysDescription("")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
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

		/// <summary>Gets or sets the width (in pixels) of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
		/// <returns>The width (in pixels) of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x00042928 File Offset: 0x00040B28
		// (set) Token: 0x060019A7 RID: 6567 RVA: 0x00042604 File Offset: 0x00040804
		[DefaultValue("")]
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> control's end tag.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x060019A8 RID: 6568 RVA: 0x00042950 File Offset: 0x00040B50
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.WriteEndTag(this.TagName);
			if (writer.Indent == 0)
			{
				writer.WriteLine();
			}
		}
	}
}
