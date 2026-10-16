using System;

namespace System.Web.UI
{
	/// <summary>Specifies the HTML attributes that an <see cref="T:System.Web.UI.HtmlTextWriter" /> or <see cref="T:System.Web.UI.Html32TextWriter" /> object writes to the opening tag of an HTML element when a Web request is processed.</summary>
	// Token: 0x0200015E RID: 350
	public enum HtmlTextWriterAttribute
	{
		/// <summary>Specifies that the HTML accesskey attribute be written to the tag. </summary>
		// Token: 0x04001244 RID: 4676
		Accesskey,
		/// <summary>Specifies that the HTML align attribute be written to the tag. </summary>
		// Token: 0x04001245 RID: 4677
		Align,
		/// <summary>Specifies that the HTML alt attribute be written to the tag.</summary>
		// Token: 0x04001246 RID: 4678
		Alt,
		/// <summary>Specifies that the HTML background attribute be written to the tag.</summary>
		// Token: 0x04001247 RID: 4679
		Background,
		/// <summary>Specifies that the HTML bgcolor attribute be written to the tag.</summary>
		// Token: 0x04001248 RID: 4680
		Bgcolor,
		/// <summary>Specifies that the HTML border attribute be written to the tag.</summary>
		// Token: 0x04001249 RID: 4681
		Border,
		/// <summary>Specifies that the HTML bordercolor attribute be written to the tag.</summary>
		// Token: 0x0400124A RID: 4682
		Bordercolor,
		/// <summary>Specifies that the HTML cellpadding attribute be written to the tag.</summary>
		// Token: 0x0400124B RID: 4683
		Cellpadding,
		/// <summary>Specifies that the HTML cellspacing attribute be written to the tag.</summary>
		// Token: 0x0400124C RID: 4684
		Cellspacing,
		/// <summary>Specifies that the HTML checked attribute be written to the tag.</summary>
		// Token: 0x0400124D RID: 4685
		Checked,
		/// <summary>Specifies that the HTML class attribute be written to the tag.</summary>
		// Token: 0x0400124E RID: 4686
		Class,
		/// <summary>Specifies that the HTML cols attribute be written to the tag.</summary>
		// Token: 0x0400124F RID: 4687
		Cols,
		/// <summary>Specifies that the HTML colspan attribute be written to the tag.</summary>
		// Token: 0x04001250 RID: 4688
		Colspan,
		/// <summary>Specifies that the HTML disabled attribute be written to the tag.</summary>
		// Token: 0x04001251 RID: 4689
		Disabled,
		/// <summary>Specifies that the HTML for attribute be written to the tag.</summary>
		// Token: 0x04001252 RID: 4690
		For,
		/// <summary>Specifies that the HTML height attribute be written to the tag.</summary>
		// Token: 0x04001253 RID: 4691
		Height,
		/// <summary>Specifies that the HTML href attribute be written to the tag.</summary>
		// Token: 0x04001254 RID: 4692
		Href,
		/// <summary>Specifies that the HTML id attribute be written to the tag.</summary>
		// Token: 0x04001255 RID: 4693
		Id,
		/// <summary>Specifies that the HTML maxlength attribute be written to the tag.</summary>
		// Token: 0x04001256 RID: 4694
		Maxlength,
		/// <summary>Specifies that the HTML multiple attribute be written to the tag.</summary>
		// Token: 0x04001257 RID: 4695
		Multiple,
		/// <summary>Specifies that the HTML name attribute be written to the tag.</summary>
		// Token: 0x04001258 RID: 4696
		Name,
		/// <summary>Specifies that the HTML nowrap attribute be written to the tag.</summary>
		// Token: 0x04001259 RID: 4697
		Nowrap,
		/// <summary>Specifies that the HTML onchange attribute be written to the tag.</summary>
		// Token: 0x0400125A RID: 4698
		Onchange,
		/// <summary>Specifies that the HTML onclick attribute be written to the tag.</summary>
		// Token: 0x0400125B RID: 4699
		Onclick,
		/// <summary>Specifies that the HTML readonly attribute be written to the tag.</summary>
		// Token: 0x0400125C RID: 4700
		ReadOnly,
		/// <summary>Specifies that the HTML rows attribute be written to the tag.</summary>
		// Token: 0x0400125D RID: 4701
		Rows,
		/// <summary>Specifies that the HTML rowspan attribute be written to the tag.</summary>
		// Token: 0x0400125E RID: 4702
		Rowspan,
		/// <summary>Specifies that the HTML rules attribute be written to the tag.</summary>
		// Token: 0x0400125F RID: 4703
		Rules,
		/// <summary>Specifies that the HTML selected attribute be written to the tag.</summary>
		// Token: 0x04001260 RID: 4704
		Selected,
		/// <summary>Specifies that the HTML size attribute be written to the tag.</summary>
		// Token: 0x04001261 RID: 4705
		Size,
		/// <summary>Specifies that the HTML src attribute be written to the tag.</summary>
		// Token: 0x04001262 RID: 4706
		Src,
		/// <summary>Specifies that the HTML style attribute be written to the tag.</summary>
		// Token: 0x04001263 RID: 4707
		Style,
		/// <summary>Specifies that the HTML tabindex attribute be written to the tag.</summary>
		// Token: 0x04001264 RID: 4708
		Tabindex,
		/// <summary>Specifies that the HTML target attribute be written to the tag.</summary>
		// Token: 0x04001265 RID: 4709
		Target,
		/// <summary>Specifies that the HTML title attribute be written to the tag.</summary>
		// Token: 0x04001266 RID: 4710
		Title,
		/// <summary>Specifies that the HTML type attribute be written to the tag.</summary>
		// Token: 0x04001267 RID: 4711
		Type,
		/// <summary>Specifies that the HTML valign attribute be written to the tag.</summary>
		// Token: 0x04001268 RID: 4712
		Valign,
		/// <summary>Specifies that the HTML value attribute be written to the tag.</summary>
		// Token: 0x04001269 RID: 4713
		Value,
		/// <summary>Specifies that the HTML width attribute be written to the tag.</summary>
		// Token: 0x0400126A RID: 4714
		Width,
		/// <summary>Specifies that the HTML wrap attribute be written to the tag.</summary>
		// Token: 0x0400126B RID: 4715
		Wrap,
		/// <summary>Specifies that the HTML abbr attribute be written to the tag.</summary>
		// Token: 0x0400126C RID: 4716
		Abbr,
		/// <summary>Specifies that the HTML autocomplete attribute be written to the tag.</summary>
		// Token: 0x0400126D RID: 4717
		AutoComplete,
		/// <summary>Specifies that the HTML axis attribute be written to the tag.</summary>
		// Token: 0x0400126E RID: 4718
		Axis,
		/// <summary>Specifies that the HTML content attribute be written to the tag.</summary>
		// Token: 0x0400126F RID: 4719
		Content,
		/// <summary>Specifies that the HTML coords attribute be written to the tag.</summary>
		// Token: 0x04001270 RID: 4720
		Coords,
		/// <summary>Specifies that the HTML designerregion attribute be written to the tag.</summary>
		// Token: 0x04001271 RID: 4721
		DesignerRegion,
		/// <summary>Specifies that the HTML dir attribute be written to the tag.</summary>
		// Token: 0x04001272 RID: 4722
		Dir,
		/// <summary>Specifies that the HTML headers attribute be written to the tag.</summary>
		// Token: 0x04001273 RID: 4723
		Headers,
		/// <summary>Specifies that the HTML longdesc attribute be written to the tag.</summary>
		// Token: 0x04001274 RID: 4724
		Longdesc,
		/// <summary>Specifies that the HTML rel attribute be written to the tag.</summary>
		// Token: 0x04001275 RID: 4725
		Rel,
		/// <summary>Specifies that the HTML scope attribute be written to the tag.</summary>
		// Token: 0x04001276 RID: 4726
		Scope,
		/// <summary>Specifies that the HTML shape attribute be written to the tag.</summary>
		// Token: 0x04001277 RID: 4727
		Shape,
		/// <summary>Specifies that the HTML usemap attribute be written to the tag.</summary>
		// Token: 0x04001278 RID: 4728
		Usemap,
		/// <summary>Specifies that the HTML vcardname attribute be written to the tag.</summary>
		// Token: 0x04001279 RID: 4729
		VCardName
	}
}
