using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000258 RID: 600
	internal class StyleSheetBag : IStyleSheet
	{
		// Token: 0x06001876 RID: 6262 RVA: 0x0003FE44 File Offset: 0x0003E044
		public void CreateStyleRule(Style style, IUrlResolutionService urlResolver, string selection)
		{
			StyleSheetBag.StyleEntry styleEntry = new StyleSheetBag.StyleEntry();
			styleEntry.Style = style;
			styleEntry.UrlResolver = urlResolver;
			styleEntry.Selection = selection;
			this.entries.Add(styleEntry);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0003FE7C File Offset: 0x0003E07C
		public void RegisterStyle(Style style, IUrlResolutionService urlResolver)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (((StyleSheetBag.StyleEntry)this.entries[i]).Style == style)
				{
					return;
				}
			}
			string text = "aspnet_" + this.entries.Count.ToString();
			style.SetRegisteredCssClass(text);
			this.CreateStyleRule(style, urlResolver, "." + text);
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0003FEF4 File Offset: 0x0003E0F4
		public void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute("type", "text/css", false);
			writer.RenderBeginTag(HtmlTextWriterTag.Style);
			foreach (object obj in this.entries)
			{
				StyleSheetBag.StyleEntry styleEntry = (StyleSheetBag.StyleEntry)obj;
				CssStyleCollection styleAttributes = styleEntry.Style.GetStyleAttributes(styleEntry.UrlResolver);
				writer.Write(string.Concat(new string[] { "\n", styleEntry.Selection, " {", styleAttributes.Value, "}" }));
			}
			writer.RenderEndTag();
		}

		// Token: 0x04001629 RID: 5673
		private ArrayList entries = new ArrayList();

		// Token: 0x02000259 RID: 601
		internal class StyleEntry
		{
			// Token: 0x0400162A RID: 5674
			public Style Style;

			// Token: 0x0400162B RID: 5675
			public string Selection;

			// Token: 0x0400162C RID: 5676
			public IUrlResolutionService UrlResolver;
		}
	}
}
