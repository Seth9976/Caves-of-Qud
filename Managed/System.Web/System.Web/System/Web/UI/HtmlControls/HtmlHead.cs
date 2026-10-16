using System;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Provides programmatic access to the HTML head element in server code.</summary>
	// Token: 0x02000257 RID: 599
	[ControlBuilder(typeof(HtmlHeadBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HtmlHead : HtmlGenericControl, IParserAccessor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> class.</summary>
		// Token: 0x06001868 RID: 6248 RVA: 0x0003FB87 File Offset: 0x0003DD87
		public HtmlHead()
			: base("head")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> class by using the specified tag.</summary>
		/// <param name="tag">A string that specifies the tag name of the control.</param>
		// Token: 0x06001869 RID: 6249 RVA: 0x0003FB94 File Offset: 0x0003DD94
		public HtmlHead(string tag)
			: base(tag)
		{
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0003FB9D File Offset: 0x0003DD9D
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page page = this.Page;
			if (page == null)
			{
				throw new HttpException("The <head runat=\"server\"> control requires a page.");
			}
			if (page.Header != null)
			{
				throw new HttpException("You can only have one <head runat=\"server\"> control on a page.");
			}
			page.SetHeader(this);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0003FBD4 File Offset: 0x0003DDD4
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
			if (this.title == null)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Title);
				if (!string.IsNullOrEmpty(this.titleText))
				{
					writer.Write(this.titleText);
				}
				writer.RenderEndTag();
			}
			if (this.descriptionMeta == null && this.descriptionText != null)
			{
				writer.AddAttribute("name", "description");
				writer.AddAttribute("content", HttpUtility.HtmlAttributeEncode(this.descriptionText));
				writer.RenderBeginTag(HtmlTextWriterTag.Meta);
				writer.RenderEndTag();
			}
			if (this.keywordsMeta == null && this.keywordsText != null)
			{
				writer.AddAttribute("name", "keywords");
				writer.AddAttribute("content", HttpUtility.HtmlAttributeEncode(this.keywordsText));
				writer.RenderBeginTag(HtmlTextWriterTag.Meta);
				writer.RenderEndTag();
			}
			if (this.styleSheet != null)
			{
				this.styleSheet.Render(writer);
			}
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0003FCB4 File Offset: 0x0003DEB4
		protected internal override void AddedControl(Control control, int index)
		{
			HtmlTitle htmlTitle = control as HtmlTitle;
			if (htmlTitle != null)
			{
				if (this.title != null)
				{
					throw new HttpException("You can only have one <title> element within the <head> element.");
				}
				this.title = htmlTitle;
			}
			HtmlMeta htmlMeta = control as HtmlMeta;
			if (htmlMeta != null)
			{
				if (string.Compare("keywords", htmlMeta.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.keywordsMeta = htmlMeta;
				}
				else if (string.Compare("description", htmlMeta.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.descriptionMeta = htmlMeta;
				}
			}
			base.AddedControl(control, index);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0003FD2D File Offset: 0x0003DF2D
		protected internal override void RemovedControl(Control control)
		{
			if (this.title == control)
			{
				this.title = null;
			}
			if (this.keywordsMeta == control)
			{
				this.keywordsMeta = null;
			}
			else if (this.descriptionMeta == control)
			{
				this.descriptionMeta = null;
			}
			base.RemovedControl(control);
		}

		/// <summary>Gets the content of the "description" meta element.</summary>
		/// <returns>The content of the "description" meta element.</returns>
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0003FD68 File Offset: 0x0003DF68
		// (set) Token: 0x0600186F RID: 6255 RVA: 0x0003FD84 File Offset: 0x0003DF84
		public string Description
		{
			get
			{
				if (this.descriptionMeta != null)
				{
					return this.descriptionMeta.Content;
				}
				return this.descriptionText;
			}
			set
			{
				if (this.descriptionMeta != null)
				{
					this.descriptionMeta.Content = value;
					return;
				}
				this.descriptionText = value;
			}
		}

		/// <summary>Gets the content of the "keywords" meta element.</summary>
		/// <returns>The content of the "keywords" meta element.</returns>
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0003FDA2 File Offset: 0x0003DFA2
		// (set) Token: 0x06001871 RID: 6257 RVA: 0x0003FDBE File Offset: 0x0003DFBE
		public string Keywords
		{
			get
			{
				if (this.keywordsMeta != null)
				{
					return this.keywordsMeta.Content;
				}
				return this.keywordsText;
			}
			set
			{
				if (this.keywordsMeta != null)
				{
					this.keywordsMeta.Content = value;
					return;
				}
				this.keywordsText = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Web.UI.IStyleSheet" /> instance that represents the style rules in the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control.</summary>
		/// <returns>An object that represents the style rules in the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control.</returns>
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0003FDDC File Offset: 0x0003DFDC
		public IStyleSheet StyleSheet
		{
			get
			{
				if (this.styleSheet == null)
				{
					this.styleSheet = new StyleSheetBag();
				}
				return this.styleSheet;
			}
		}

		/// <summary>Gets the page title.</summary>
		/// <returns>The page title.</returns>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x0003FDF7 File Offset: 0x0003DFF7
		// (set) Token: 0x06001874 RID: 6260 RVA: 0x0003FE13 File Offset: 0x0003E013
		public string Title
		{
			get
			{
				if (this.title != null)
				{
					return this.title.Text;
				}
				return this.titleText;
			}
			set
			{
				if (this.title != null)
				{
					this.title.Text = value;
					return;
				}
				this.titleText = value;
			}
		}

		// Token: 0x04001622 RID: 5666
		private string descriptionText;

		// Token: 0x04001623 RID: 5667
		private string keywordsText;

		// Token: 0x04001624 RID: 5668
		private HtmlMeta descriptionMeta;

		// Token: 0x04001625 RID: 5669
		private HtmlMeta keywordsMeta;

		// Token: 0x04001626 RID: 5670
		private string titleText;

		// Token: 0x04001627 RID: 5671
		private HtmlTitle title;

		// Token: 0x04001628 RID: 5672
		private StyleSheetBag styleSheet;
	}
}
