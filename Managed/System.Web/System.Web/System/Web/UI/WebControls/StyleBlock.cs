using System;
using System.Collections.Generic;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200040E RID: 1038
	internal sealed class StyleBlock : Control
	{
		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x00079EF6 File Offset: 0x000780F6
		private List<NamedCssStyleCollection> CssStyles
		{
			get
			{
				if (this.cssStyles == null)
				{
					this.cssStyles = new List<NamedCssStyleCollection>();
					this.cssStyleIndex = new Dictionary<string, NamedCssStyleCollection>(StringComparer.Ordinal);
				}
				return this.cssStyles;
			}
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x00079F21 File Offset: 0x00078121
		public StyleBlock(string stylePrefix)
		{
			if (string.IsNullOrEmpty(stylePrefix))
			{
				throw new ArgumentNullException("stylePrefix");
			}
			this.stylePrefix = stylePrefix;
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x00079F43 File Offset: 0x00078143
		public NamedCssStyleCollection RegisterStyle(string name = null)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			return this.GetStyle(name);
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x00079F56 File Offset: 0x00078156
		public NamedCssStyleCollection RegisterStyle(Style style, string name = null)
		{
			if (style == null)
			{
				throw new ArgumentNullException("style");
			}
			if (name == null)
			{
				name = string.Empty;
			}
			NamedCssStyleCollection style2 = this.GetStyle(name);
			style2.CopyFrom(style.GetStyleAttributes(null));
			return style2;
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x00079F85 File Offset: 0x00078185
		public NamedCssStyleCollection RegisterStyle(HtmlTextWriterStyle key, string value, string styleName = null)
		{
			if (styleName == null)
			{
				styleName = string.Empty;
			}
			NamedCssStyleCollection style = this.GetStyle(styleName);
			style.Add(key, value);
			return style;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x00079FA4 File Offset: 0x000781A4
		private NamedCssStyleCollection GetStyle(string name)
		{
			List<NamedCssStyleCollection> list = this.CssStyles;
			NamedCssStyleCollection namedCssStyleCollection;
			if (!this.cssStyleIndex.TryGetValue(name, out namedCssStyleCollection))
			{
				namedCssStyleCollection = new NamedCssStyleCollection(name);
				this.cssStyleIndex.Add(name, namedCssStyleCollection);
				list.Add(namedCssStyleCollection);
			}
			if (namedCssStyleCollection == null)
			{
				throw new InvalidOperationException(string.Format("Internal error. Stylesheet for style {0} is null.", name));
			}
			return namedCssStyleCollection;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x00079FF8 File Offset: 0x000781F8
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.cssStyles == null || this.cssStyles.Count == 0)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
			writer.RenderBeginTag(HtmlTextWriterTag.Style);
			writer.WriteLine("/* <![CDATA[ */");
			foreach (NamedCssStyleCollection namedCssStyleCollection in this.cssStyles)
			{
				string value = namedCssStyleCollection.Collection.Value;
				if (!string.IsNullOrEmpty(value))
				{
					string text = namedCssStyleCollection.Name;
					if (text != string.Empty)
					{
						text += " ";
					}
					writer.WriteLine("#{0} {1}{{ {2} }}", this.stylePrefix, text, value);
				}
			}
			writer.WriteLine("/* ]]> */");
			writer.RenderEndTag();
		}

		// Token: 0x04001BD9 RID: 7129
		private List<NamedCssStyleCollection> cssStyles;

		// Token: 0x04001BDA RID: 7130
		private Dictionary<string, NamedCssStyleCollection> cssStyleIndex;

		// Token: 0x04001BDB RID: 7131
		private string stylePrefix;
	}
}
