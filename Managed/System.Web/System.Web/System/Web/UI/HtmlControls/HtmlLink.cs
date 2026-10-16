using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML link element on the server.</summary>
	// Token: 0x02000267 RID: 615
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	public class HtmlLink : HtmlControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> class.</summary>
		// Token: 0x06001928 RID: 6440 RVA: 0x00041591 File Offset: 0x0003F791
		public HtmlLink()
			: base("link")
		{
		}

		/// <summary>Gets or sets the URL target of the link specified in the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> control. </summary>
		/// <returns>The URL target of the link.</returns>
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x000415A0 File Offset: 0x0003F7A0
		// (set) Token: 0x0600192A RID: 6442 RVA: 0x000415C8 File Offset: 0x0003F7C8
		[UrlProperty]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Href
		{
			get
			{
				string text = base.Attributes["href"];
				if (text == null)
				{
					return "";
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("href");
					return;
				}
				base.Attributes["href"] = value;
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x0600192B RID: 6443 RVA: 0x000415EF File Offset: 0x0003F7EF
		protected internal override void Render(HtmlTextWriter writer)
		{
			writer.WriteBeginTag(this.TagName);
			this.RenderAttributes(writer);
			writer.Write(" />");
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x0600192C RID: 6444 RVA: 0x0004160F File Offset: 0x0003F80F
		[global::System.MonoTODO("why override?")]
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (this.Href.Length > 0)
			{
				this.Href = base.ResolveClientUrl(this.Href);
			}
			base.RenderAttributes(writer);
		}
	}
}
