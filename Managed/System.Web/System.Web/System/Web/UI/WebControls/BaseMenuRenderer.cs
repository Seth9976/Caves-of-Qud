using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000333 RID: 819
	internal abstract class BaseMenuRenderer : IMenuRenderer
	{
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001CAA RID: 7338
		public abstract HtmlTextWriterTag Tag { get; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x000455F2 File Offset: 0x000437F2
		// (set) Token: 0x06001CAC RID: 7340 RVA: 0x000455FA File Offset: 0x000437FA
		private protected Menu Owner { protected get; private set; }

		// Token: 0x06001CAD RID: 7341 RVA: 0x00045603 File Offset: 0x00043803
		public BaseMenuRenderer(Menu owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.Owner = owner;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00045628 File Offset: 0x00043828
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			Menu owner = this.Owner;
			Page page = owner.Page;
			SubMenuStyle staticMenuStyleInternal = owner.StaticMenuStyleInternal;
			SubMenuStyleCollection levelSubMenuStylesInternal = owner.LevelSubMenuStylesInternal;
			bool flag = levelSubMenuStylesInternal != null && levelSubMenuStylesInternal.Count > 0;
			Style style = ((flag || staticMenuStyleInternal != null) ? owner.ControlStyle : null);
			if (page != null && page.Header != null)
			{
				if (staticMenuStyleInternal != null)
				{
					this.AddCssClass(style, staticMenuStyleInternal.CssClass);
					this.AddCssClass(style, staticMenuStyleInternal.RegisteredCssClass);
				}
				if (flag)
				{
					this.AddCssClass(style, levelSubMenuStylesInternal[0].CssClass);
					this.AddCssClass(style, levelSubMenuStylesInternal[0].RegisteredCssClass);
					return;
				}
			}
			else
			{
				if (staticMenuStyleInternal != null)
				{
					style.CopyFrom(staticMenuStyleInternal);
				}
				if (flag)
				{
					style.CopyFrom(levelSubMenuStylesInternal[0]);
				}
			}
		}

		// Token: 0x06001CAF RID: 7343
		public abstract void PreRender(Page page, HtmlHead head, ClientScriptManager csm, string cmenu, StringBuilder script);

		// Token: 0x06001CB0 RID: 7344
		public abstract void RenderMenuBeginTag(HtmlTextWriter writer, bool dynamic, int menuLevel);

		// Token: 0x06001CB1 RID: 7345
		public abstract void RenderMenuBody(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, bool notLast);

		// Token: 0x06001CB2 RID: 7346
		public abstract void RenderBeginTag(HtmlTextWriter writer, string skipLinkText);

		// Token: 0x06001CB3 RID: 7347
		public abstract void RenderEndTag(HtmlTextWriter writer);

		// Token: 0x06001CB4 RID: 7348
		public abstract void RenderContents(HtmlTextWriter writer);

		// Token: 0x06001CB5 RID: 7349
		public abstract bool IsDynamicItem(Menu owner, MenuItem item);

		// Token: 0x06001CB6 RID: 7350
		protected abstract void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool vertical, bool notLast, bool isFirst, BaseMenuRenderer.OwnerContext oc);

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000456E8 File Offset: 0x000438E8
		public virtual void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool notLast, bool isFirst)
		{
			BaseMenuRenderer.OwnerContext ownerContext = new BaseMenuRenderer.OwnerContext(this);
			this.RenderMenuItem(writer, item, ownerContext.IsVertical, notLast, isFirst, ownerContext);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000431E9 File Offset: 0x000413E9
		public virtual void RenderMenuEndTag(HtmlTextWriter writer, bool dynamic, int menuLevel)
		{
			writer.RenderEndTag();
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00045710 File Offset: 0x00043910
		public virtual void RenderItemContent(HtmlTextWriter writer, MenuItem item, bool isDynamicItem)
		{
			Menu owner = this.Owner;
			if (!string.IsNullOrEmpty(item.ImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, owner.ResolveClientUrl(item.ImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, item.ToolTip);
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
				writer.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "middle");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
			string text;
			if (isDynamicItem && (text = owner.DynamicItemFormatString).Length > 0)
			{
				writer.Write(string.Format(text, item.Text));
				return;
			}
			if (!isDynamicItem && (text = owner.StaticItemFormatString).Length > 0)
			{
				writer.Write(string.Format(text, item.Text));
				return;
			}
			writer.Write(item.Text);
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x000457D1 File Offset: 0x000439D1
		public void AddCssClass(Style style, string cssClass)
		{
			style.AddCssClass(cssClass);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x000457DA File Offset: 0x000439DA
		public string GetItemClientId(string ownerClientID, MenuItem item, string suffix)
		{
			return ownerClientID + "_" + item.Path + suffix;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x000457F0 File Offset: 0x000439F0
		public virtual void RenderItemHref(Menu owner, HtmlTextWriter writer, MenuItem item)
		{
			if (!item.BranchEnabled)
			{
				writer.AddAttribute("disabled", "true", false);
				return;
			}
			if (!item.Selectable)
			{
				writer.AddAttribute("href", "#", false);
				writer.AddStyleAttribute("cursor", "text");
				return;
			}
			if (item.NavigateUrl != string.Empty)
			{
				string text = ((item.Target != string.Empty) ? item.Target : owner.Target);
				string text2 = owner.ResolveClientUrl(item.NavigateUrl);
				writer.AddAttribute("href", text2);
				if (text != string.Empty)
				{
					writer.AddAttribute("target", text);
					return;
				}
			}
			else
			{
				writer.AddAttribute("href", this.GetClientEvent(owner, item));
			}
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x000458BC File Offset: 0x00043ABC
		public string GetPopOutImage(Menu owner, MenuItem item, bool isDynamicItem)
		{
			if (owner == null)
			{
				owner = this.Owner;
			}
			if (item.PopOutImageUrl != string.Empty)
			{
				return item.PopOutImageUrl;
			}
			bool flag = false;
			if (isDynamicItem)
			{
				if (owner.DynamicPopOutImageUrl != string.Empty)
				{
					return owner.DynamicPopOutImageUrl;
				}
				if (owner.DynamicEnableDefaultPopOutImage)
				{
					flag = true;
				}
			}
			else
			{
				if (owner.StaticPopOutImageUrl != string.Empty)
				{
					return owner.StaticPopOutImageUrl;
				}
				if (owner.StaticEnableDefaultPopOutImage)
				{
					flag = true;
				}
			}
			if (flag)
			{
				return this.GetArrowResourceUrl(owner);
			}
			return null;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00045948 File Offset: 0x00043B48
		public string GetArrowResourceUrl(Menu owner)
		{
			Page page = owner.Page;
			ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : null);
			if (clientScriptManager != null)
			{
				return clientScriptManager.GetWebResourceUrl(typeof(Menu), "arrow_plus.gif");
			}
			return null;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x00045984 File Offset: 0x00043B84
		public void FillMenuStyle(HtmlHead header, bool dynamic, int menuLevel, SubMenuStyle style)
		{
			Menu owner = this.Owner;
			if (header == null)
			{
				Page page = owner.Page;
				header = ((page != null) ? page.Header : null);
			}
			SubMenuStyle staticMenuStyleInternal = owner.StaticMenuStyleInternal;
			SubMenuStyle dynamicMenuStyleInternal = owner.DynamicMenuStyleInternal;
			SubMenuStyleCollection levelSubMenuStylesInternal = owner.LevelSubMenuStylesInternal;
			if (header != null)
			{
				if (!dynamic && staticMenuStyleInternal != null)
				{
					this.AddCssClass(style, staticMenuStyleInternal.CssClass);
					this.AddCssClass(style, staticMenuStyleInternal.RegisteredCssClass);
				}
				if (dynamic && dynamicMenuStyleInternal != null)
				{
					this.AddCssClass(style, dynamicMenuStyleInternal.CssClass);
					this.AddCssClass(style, dynamicMenuStyleInternal.RegisteredCssClass);
				}
				if (levelSubMenuStylesInternal != null && levelSubMenuStylesInternal.Count > menuLevel)
				{
					this.AddCssClass(style, levelSubMenuStylesInternal[menuLevel].CssClass);
					this.AddCssClass(style, levelSubMenuStylesInternal[menuLevel].RegisteredCssClass);
					return;
				}
			}
			else
			{
				if (!dynamic && staticMenuStyleInternal != null)
				{
					style.CopyFrom(staticMenuStyleInternal);
				}
				if (dynamic && dynamicMenuStyleInternal != null)
				{
					style.CopyFrom(dynamicMenuStyleInternal);
				}
				if (levelSubMenuStylesInternal != null && levelSubMenuStylesInternal.Count > menuLevel)
				{
					style.CopyFrom(levelSubMenuStylesInternal[menuLevel]);
				}
			}
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00045A7A File Offset: 0x00043C7A
		public void RegisterStyle(Style baseStyle, Style linkStyle, HtmlHead head)
		{
			this.RegisterStyle(baseStyle, linkStyle, null, head);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00045A86 File Offset: 0x00043C86
		public void RegisterStyle(Style baseStyle, Style linkStyle, string className, HtmlHead head)
		{
			if (head == null)
			{
				return;
			}
			linkStyle.CopyTextStylesFrom(baseStyle);
			linkStyle.BorderStyle = BorderStyle.None;
			this.RegisterStyle(linkStyle, className, head);
			this.RegisterStyle(baseStyle, className, head);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x00045AAF File Offset: 0x00043CAF
		public void RegisterStyle(Style baseStyle, HtmlHead head)
		{
			this.RegisterStyle(baseStyle, null, head);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00045ABA File Offset: 0x00043CBA
		public void RegisterStyle(Style baseStyle, string className, HtmlHead head)
		{
			if (head == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(className))
			{
				className = this.IncrementStyleClassName();
			}
			baseStyle.SetRegisteredCssClass(className);
			head.StyleSheet.CreateStyleRule(baseStyle, this.Owner, "." + className);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00045AF4 File Offset: 0x00043CF4
		public void RenderSeparatorImage(Menu owner, HtmlTextWriter writer, string url, bool standardsCompliant)
		{
			if (string.IsNullOrEmpty(url))
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Src, owner.ResolveClientUrl(url));
			if (standardsCompliant)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "separator");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00045B44 File Offset: 0x00043D44
		public bool IsDynamicItem(MenuItem item)
		{
			return this.IsDynamicItem(this.Owner, item);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00045B54 File Offset: 0x00043D54
		private string GetClientEvent(Menu owner, MenuItem item)
		{
			if (owner == null)
			{
				owner = this.Owner;
			}
			Page page = owner.Page;
			ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : null);
			if (clientScriptManager == null)
			{
				return string.Empty;
			}
			return clientScriptManager.GetPostBackClientHyperlink(owner, item.Path, true);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00045B97 File Offset: 0x00043D97
		private string IncrementStyleClassName()
		{
			this.registeredStylesCounter++;
			return this.Owner.ClientID + "_" + this.registeredStylesCounter.ToString();
		}

		// Token: 0x040017F9 RID: 6137
		private int registeredStylesCounter = -1;

		// Token: 0x02000334 RID: 820
		protected sealed class OwnerContext
		{
			// Token: 0x170008E2 RID: 2274
			// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00045BC7 File Offset: 0x00043DC7
			public string StaticPopOutImageTextFormatString
			{
				get
				{
					if (this.staticPopOutImageTextFormatString == null)
					{
						this.staticPopOutImageTextFormatString = this.container.Owner.StaticPopOutImageTextFormatString;
					}
					return this.staticPopOutImageTextFormatString;
				}
			}

			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x00045BED File Offset: 0x00043DED
			public string DynamicPopOutImageTextFormatString
			{
				get
				{
					if (this.dynamicPopOutImageTextFormatString == null)
					{
						this.dynamicPopOutImageTextFormatString = this.container.Owner.DynamicPopOutImageTextFormatString;
					}
					return this.dynamicPopOutImageTextFormatString;
				}
			}

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x06001CCA RID: 7370 RVA: 0x00045C13 File Offset: 0x00043E13
			public string DynamicTopSeparatorImageUrl
			{
				get
				{
					if (this.dynamicTopSeparatorImageUrl == null)
					{
						this.dynamicTopSeparatorImageUrl = this.container.Owner.DynamicTopSeparatorImageUrl;
					}
					return this.dynamicTopSeparatorImageUrl;
				}
			}

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x06001CCB RID: 7371 RVA: 0x00045C39 File Offset: 0x00043E39
			public string DynamicBottomSeparatorImageUrl
			{
				get
				{
					if (this.dynamicBottomSeparatorImageUrl == null)
					{
						this.dynamicBottomSeparatorImageUrl = this.container.Owner.DynamicBottomSeparatorImageUrl;
					}
					return this.dynamicBottomSeparatorImageUrl;
				}
			}

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x06001CCC RID: 7372 RVA: 0x00045C5F File Offset: 0x00043E5F
			public string StaticTopSeparatorImageUrl
			{
				get
				{
					if (this.staticTopSeparatorImageUrl == null)
					{
						this.staticTopSeparatorImageUrl = this.container.Owner.StaticTopSeparatorImageUrl;
					}
					return this.staticBottomSeparatorImageUrl;
				}
			}

			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x06001CCD RID: 7373 RVA: 0x00045C85 File Offset: 0x00043E85
			public string StaticBottomSeparatorImageUrl
			{
				get
				{
					if (this.staticBottomSeparatorImageUrl == null)
					{
						this.staticBottomSeparatorImageUrl = this.container.Owner.StaticBottomSeparatorImageUrl;
					}
					return this.staticBottomSeparatorImageUrl;
				}
			}

			// Token: 0x170008E8 RID: 2280
			// (get) Token: 0x06001CCE RID: 7374 RVA: 0x00045CAB File Offset: 0x00043EAB
			public List<Style> LevelMenuItemLinkStyles
			{
				get
				{
					if (this.levelMenuItemLinkStyles == null)
					{
						this.levelMenuItemLinkStyles = this.container.Owner.LevelMenuItemLinkStyles;
					}
					return this.levelMenuItemLinkStyles;
				}
			}

			// Token: 0x170008E9 RID: 2281
			// (get) Token: 0x06001CCF RID: 7375 RVA: 0x00045CD1 File Offset: 0x00043ED1
			public List<Style> LevelSelectedLinkStyles
			{
				get
				{
					if (this.levelSelectedLinkStyles == null)
					{
						this.levelSelectedLinkStyles = this.container.Owner.LevelSelectedLinkStyles;
					}
					return this.levelSelectedLinkStyles;
				}
			}

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00045CF7 File Offset: 0x00043EF7
			public Style StaticMenuItemLinkStyle
			{
				get
				{
					if (this.staticMenuItemLinkStyle == null)
					{
						this.staticMenuItemLinkStyle = this.container.Owner.StaticMenuItemLinkStyle;
					}
					return this.staticMenuItemLinkStyle;
				}
			}

			// Token: 0x170008EB RID: 2283
			// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x00045D1D File Offset: 0x00043F1D
			public Style DynamicMenuItemLinkStyle
			{
				get
				{
					if (this.dynamicMenuItemLinkStyle == null)
					{
						this.dynamicMenuItemLinkStyle = this.container.Owner.DynamicMenuItemLinkStyle;
					}
					return this.dynamicMenuItemLinkStyle;
				}
			}

			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00045D43 File Offset: 0x00043F43
			public MenuItemStyle StaticSelectedStyle
			{
				get
				{
					if (this.staticSelectedStyle == null)
					{
						this.staticSelectedStyle = this.container.Owner.StaticSelectedStyle;
					}
					return this.staticSelectedStyle;
				}
			}

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00045D69 File Offset: 0x00043F69
			public MenuItemStyle DynamicSelectedStyle
			{
				get
				{
					if (this.dynamicSelectedStyle == null)
					{
						this.dynamicSelectedStyle = this.container.Owner.DynamicSelectedStyle;
					}
					return this.dynamicSelectedStyle;
				}
			}

			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x00045D8F File Offset: 0x00043F8F
			public Style StaticSelectedLinkStyle
			{
				get
				{
					if (this.staticSelectedLinkStyle == null)
					{
						this.staticSelectedLinkStyle = this.container.Owner.StaticSelectedLinkStyle;
					}
					return this.staticSelectedLinkStyle;
				}
			}

			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x00045DB5 File Offset: 0x00043FB5
			public Style DynamicSelectedLinkStyle
			{
				get
				{
					if (this.dynamicSelectedLinkStyle == null)
					{
						this.dynamicSelectedLinkStyle = this.container.Owner.DynamicSelectedLinkStyle;
					}
					return this.dynamicSelectedLinkStyle;
				}
			}

			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x00045DDB File Offset: 0x00043FDB
			public MenuItemStyleCollection LevelSelectedStyles
			{
				get
				{
					if (this.levelSelectedStyles == null)
					{
						this.levelSelectedStyles = this.container.Owner.LevelSelectedStyles;
					}
					return this.levelSelectedStyles;
				}
			}

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x00045E01 File Offset: 0x00044001
			public ITemplate DynamicItemTemplate
			{
				get
				{
					if (!this.dynamicItemTemplateQueried && this.dynamicItemTemplate == null)
					{
						this.dynamicItemTemplate = this.container.Owner.DynamicItemTemplate;
						this.dynamicItemTemplateQueried = true;
					}
					return this.dynamicItemTemplate;
				}
			}

			// Token: 0x06001CD8 RID: 7384 RVA: 0x00045E38 File Offset: 0x00044038
			public OwnerContext(BaseMenuRenderer container)
			{
				if (container == null)
				{
					throw new ArgumentNullException("container");
				}
				this.container = container;
				Menu owner = container.Owner;
				Page page = owner.Page;
				this.Header = ((page != null) ? page.Header : null);
				this.ClientID = owner.ClientID;
				this.IsVertical = owner.Orientation == Orientation.Vertical;
				this.StaticSubMenuIndent = owner.StaticSubMenuIndent;
				this.SelectedItem = owner.SelectedItem;
				this.ControlLinkStyle = owner.ControlLinkStyle;
				this.StaticDisplayLevels = owner.StaticDisplayLevels;
				this.StaticMenuItemStyle = owner.StaticMenuItemStyleInternal;
				this.DynamicMenuItemStyle = owner.DynamicMenuItemStyleInternal;
				this.LevelMenuItemStyles = owner.LevelMenuItemStyles;
			}

			// Token: 0x040017FB RID: 6139
			private BaseMenuRenderer container;

			// Token: 0x040017FC RID: 6140
			private string staticPopOutImageTextFormatString;

			// Token: 0x040017FD RID: 6141
			private string dynamicPopOutImageTextFormatString;

			// Token: 0x040017FE RID: 6142
			private string dynamicTopSeparatorImageUrl;

			// Token: 0x040017FF RID: 6143
			private string dynamicBottomSeparatorImageUrl;

			// Token: 0x04001800 RID: 6144
			private string staticTopSeparatorImageUrl;

			// Token: 0x04001801 RID: 6145
			private string staticBottomSeparatorImageUrl;

			// Token: 0x04001802 RID: 6146
			private List<Style> levelMenuItemLinkStyles;

			// Token: 0x04001803 RID: 6147
			private List<Style> levelSelectedLinkStyles;

			// Token: 0x04001804 RID: 6148
			private Style staticMenuItemLinkStyle;

			// Token: 0x04001805 RID: 6149
			private Style dynamicMenuItemLinkStyle;

			// Token: 0x04001806 RID: 6150
			private MenuItemStyle staticSelectedStyle;

			// Token: 0x04001807 RID: 6151
			private Style staticSelectedLinkStyle;

			// Token: 0x04001808 RID: 6152
			private MenuItemStyle dynamicSelectedStyle;

			// Token: 0x04001809 RID: 6153
			private Style dynamicSelectedLinkStyle;

			// Token: 0x0400180A RID: 6154
			private MenuItemStyleCollection levelSelectedStyles;

			// Token: 0x0400180B RID: 6155
			private ITemplate dynamicItemTemplate;

			// Token: 0x0400180C RID: 6156
			private bool dynamicItemTemplateQueried;

			// Token: 0x0400180D RID: 6157
			public readonly MenuItemStyle StaticMenuItemStyle;

			// Token: 0x0400180E RID: 6158
			public readonly MenuItemStyle DynamicMenuItemStyle;

			// Token: 0x0400180F RID: 6159
			public readonly MenuItemStyleCollection LevelMenuItemStyles;

			// Token: 0x04001810 RID: 6160
			public readonly Style ControlLinkStyle;

			// Token: 0x04001811 RID: 6161
			public readonly HtmlHead Header;

			// Token: 0x04001812 RID: 6162
			public readonly string ClientID;

			// Token: 0x04001813 RID: 6163
			public readonly int StaticDisplayLevels;

			// Token: 0x04001814 RID: 6164
			public readonly bool IsVertical;

			// Token: 0x04001815 RID: 6165
			public readonly MenuItem SelectedItem;

			// Token: 0x04001816 RID: 6166
			public readonly Unit StaticSubMenuIndent;
		}
	}
}
