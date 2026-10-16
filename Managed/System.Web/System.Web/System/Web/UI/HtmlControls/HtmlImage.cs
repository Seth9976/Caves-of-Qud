using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Provides programmatic access for the HTML &lt;img&gt; element on the server.</summary>
	// Token: 0x0200025B RID: 603
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlImage : HtmlControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlImage" /> class.</summary>
		// Token: 0x0600187D RID: 6269 RVA: 0x0004000D File Offset: 0x0003E20D
		public HtmlImage()
			: base("img")
		{
		}

		/// <summary>Gets or sets the alignment of the image relative to other Web page elements.</summary>
		/// <returns>A string that specifies the alignment of the image relative to other Web page elements.</returns>
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0004001C File Offset: 0x0003E21C
		// (set) Token: 0x0600187F RID: 6271 RVA: 0x00040044 File Offset: 0x0003E244
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public string Align
		{
			get
			{
				string text = base.Attributes["align"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
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

		/// <summary>Gets or sets the alternative caption the browser displays if an image is unavailable or currently downloading and not yet finished.</summary>
		/// <returns>A string that contains the alternative caption for the browser to use when the image is unavailable.</returns>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x0004006C File Offset: 0x0003E26C
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x00040094 File Offset: 0x0003E294
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		public string Alt
		{
			get
			{
				string text = base.Attributes["alt"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("alt");
					return;
				}
				base.Attributes["alt"] = value;
			}
		}

		/// <summary>Gets or sets the width of a frame for an image.</summary>
		/// <returns>The width (in pixels) of a frame for an image.</returns>
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x000400BC File Offset: 0x0003E2BC
		// (set) Token: 0x06001883 RID: 6275 RVA: 0x000400EA File Offset: 0x0003E2EA
		[WebCategory("Appearance")]
		[DefaultValue(0)]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Border
		{
			get
			{
				string text = base.Attributes["border"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, Helpers.InvariantCulture);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("border");
					return;
				}
				base.Attributes["border"] = value.ToString();
			}
		}

		/// <summary>Gets or sets the height of the image.</summary>
		/// <returns>The height of the image.</returns>
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x00040118 File Offset: 0x0003E318
		// (set) Token: 0x06001885 RID: 6277 RVA: 0x00040146 File Offset: 0x0003E346
		[DefaultValue(100)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Height
		{
			get
			{
				string text = base.Attributes["height"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, Helpers.InvariantCulture);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("height");
					return;
				}
				base.Attributes["height"] = value.ToString();
			}
		}

		/// <summary>Gets or sets the source of the image file to display.</summary>
		/// <returns>A string that contains the path to an image file to display.</returns>
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x00040174 File Offset: 0x0003E374
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x0004019C File Offset: 0x0003E39C
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Behavior")]
		[UrlProperty]
		[DefaultValue("")]
		public string Src
		{
			get
			{
				string text = base.Attributes["src"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("src");
					return;
				}
				base.Attributes["src"] = value;
			}
		}

		/// <summary>Gets or sets the width of the image.</summary>
		/// <returns>The width of the image.</returns>
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x000401C4 File Offset: 0x0003E3C4
		// (set) Token: 0x06001889 RID: 6281 RVA: 0x000401F2 File Offset: 0x0003E3F2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Layout")]
		[DefaultValue(100)]
		[WebSysDescription("")]
		public int Width
		{
			get
			{
				string text = base.Attributes["width"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, Helpers.InvariantCulture);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("width");
					return;
				}
				base.Attributes["width"] = value.ToString();
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlImage" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlImage.Src" /> property contains a malformed URL.</exception>
		// Token: 0x0600188A RID: 6282 RVA: 0x00040220 File Offset: 0x0003E420
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReference(writer, "src");
			string text = base.Attributes["src"];
			if (text == null || text.Length == 0)
			{
				base.Attributes.Remove("src");
			}
			base.RenderAttributes(writer);
			writer.Write(" /");
		}
	}
}
