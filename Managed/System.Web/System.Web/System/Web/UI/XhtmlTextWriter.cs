using System;
using System.Collections;
using System.IO;

namespace System.Web.UI
{
	/// <summary>Writes Extensible Hypertext Markup Language (XHTML)-specific characters, including all variations of XHTML modules that derive from XTHML, to the output stream for an ASP.NET server control for mobile devices. Override the <see cref="T:System.Web.UI.XhtmlTextWriter" /> class to provide custom XHTML rendering for ASP.NET pages and server controls.</summary>
	// Token: 0x0200024E RID: 590
	public class XhtmlTextWriter : HtmlTextWriter
	{
		// Token: 0x060017F4 RID: 6132 RVA: 0x0003E5A8 File Offset: 0x0003C7A8
		static XhtmlTextWriter()
		{
			XhtmlTextWriter.SetupHash(XhtmlTextWriter.default_common_attrs, XhtmlTextWriter.DefaultCommonAttributes);
			XhtmlTextWriter.default_suppress_common_attrs = new Hashtable(XhtmlTextWriter.DefaultSuppressCommonAttributes.Length);
			XhtmlTextWriter.SetupHash(XhtmlTextWriter.default_suppress_common_attrs, XhtmlTextWriter.DefaultSuppressCommonAttributes);
			XhtmlTextWriter.SetupElementsSpecificAttributes();
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0003E668 File Offset: 0x0003C868
		private static void SetupHash(Hashtable hash, string[] values)
		{
			foreach (string text in values)
			{
				hash.Add(text, true);
			}
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0003E698 File Offset: 0x0003C898
		private static void SetupElementsSpecificAttributes()
		{
			XhtmlTextWriter.default_element_specific_attrs = new Hashtable();
			string[] array = new string[] { "accesskey", "href", "charset", "hreflang", "rel", "type", "rev", "title", "tabindex" };
			XhtmlTextWriter.SetupElementSpecificAttributes("a", array);
			string[] array2 = new string[] { "href" };
			XhtmlTextWriter.SetupElementSpecificAttributes("base", array2);
			string[] array3 = new string[] { "cite" };
			XhtmlTextWriter.SetupElementSpecificAttributes("blockquote", array3);
			string[] array4 = new string[] { "id", "class", "title" };
			XhtmlTextWriter.SetupElementSpecificAttributes("br", array4);
			string[] array5 = new string[] { "action", "method", "enctype" };
			XhtmlTextWriter.SetupElementSpecificAttributes("form", array5);
			string[] array6 = new string[] { "xml:lang" };
			XhtmlTextWriter.SetupElementSpecificAttributes("head", array6);
			string[] array7 = new string[] { "version", "xml:lang", "xmlns" };
			XhtmlTextWriter.SetupElementSpecificAttributes("html", array7);
			string[] array8 = new string[] { "src", "alt", "width", "longdesc", "height" };
			XhtmlTextWriter.SetupElementSpecificAttributes("img", array8);
			string[] array9 = new string[]
			{
				"size", "accesskey", "title", "name", "type", "disabled", "value", "src", "checked", "maxlength",
				"tabindex"
			};
			XhtmlTextWriter.SetupElementSpecificAttributes("input", array9);
			string[] array10 = new string[] { "accesskey", "for" };
			XhtmlTextWriter.SetupElementSpecificAttributes("label", array10);
			string[] array11 = new string[] { "value" };
			XhtmlTextWriter.SetupElementSpecificAttributes("li", array11);
			string[] array12 = new string[] { "hreflang", "rev", "type", "charset", "rel", "href", "media" };
			XhtmlTextWriter.SetupElementSpecificAttributes("link", array12);
			string[] array13 = new string[] { "content", "name", "xml:lang", "http-equiv", "scheme" };
			XhtmlTextWriter.SetupElementSpecificAttributes("meta", array13);
			string[] array14 = new string[]
			{
				"codebase", "classid", "data", "standby", "name", "type", "height", "archive", "declare", "width",
				"tabindex", "codetype"
			};
			XhtmlTextWriter.SetupElementSpecificAttributes("object", array14);
			string[] array15 = new string[] { "start" };
			XhtmlTextWriter.SetupElementSpecificAttributes("ol", array15);
			string[] array16 = new string[] { "label", "disabled" };
			XhtmlTextWriter.SetupElementSpecificAttributes("optgroup", array16);
			string[] array17 = new string[] { "selected", "value" };
			XhtmlTextWriter.SetupElementSpecificAttributes("option", array17);
			string[] array18 = new string[] { "id", "name", "valuetype", "value", "type" };
			XhtmlTextWriter.SetupElementSpecificAttributes("param", array18);
			string[] array19 = new string[] { "xml:space" };
			XhtmlTextWriter.SetupElementSpecificAttributes("pre", array19);
			string[] array20 = new string[] { "cite" };
			XhtmlTextWriter.SetupElementSpecificAttributes("q", array20);
			string[] array21 = new string[] { "name", "tabindex", "disabled", "multiple", "size" };
			XhtmlTextWriter.SetupElementSpecificAttributes("select", array21);
			string[] array22 = new string[] { "xml:lang", "xml:space", "type", "title", "media" };
			XhtmlTextWriter.SetupElementSpecificAttributes("style", array22);
			string[] array23 = new string[] { "width", "summary" };
			XhtmlTextWriter.SetupElementSpecificAttributes("table", array23);
			string[] array24 = new string[] { "name", "cols", "accesskey", "tabindex", "rows" };
			XhtmlTextWriter.SetupElementSpecificAttributes("textarea", array24);
			string[] array25 = new string[] { "headers", "align", "rowspan", "colspan", "axis", "scope", "abbr", "valign" };
			XhtmlTextWriter.SetupElementSpecificAttributes("td", array25);
			XhtmlTextWriter.SetupElementSpecificAttributes("th", array25);
			string[] array26 = new string[] { "xml:lang" };
			XhtmlTextWriter.SetupElementSpecificAttributes("title", array26);
			string[] array27 = new string[] { "align", "valign" };
			XhtmlTextWriter.SetupElementSpecificAttributes("tr", array27);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0003EC18 File Offset: 0x0003CE18
		private static void SetupElementSpecificAttributes(string elementName, string[] attributesNames)
		{
			Hashtable hashtable = new Hashtable(attributesNames.Length);
			XhtmlTextWriter.SetupHash(hashtable, attributesNames);
			XhtmlTextWriter.default_element_specific_attrs.Add(elementName, hashtable);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.XhtmlTextWriter" /> class that uses the line indentation that is specified in the <see cref="F:System.Web.UI.HtmlTextWriter.DefaultTabString" /> field. Use the <see cref="M:System.Web.UI.XhtmlTextWriter.#ctor(System.IO.TextWriter)" /> constructor if you do not want to change the default line indentation.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> instance that renders the XHTML content. </param>
		// Token: 0x060017F8 RID: 6136 RVA: 0x0003EC41 File Offset: 0x0003CE41
		public XhtmlTextWriter(TextWriter writer)
			: this(writer, "\t")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.XhtmlTextWriter" /> class with the specified line indentation.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> instance that renders the XHTML content. </param>
		/// <param name="tabString">The string used to render a line indentation.</param>
		// Token: 0x060017F9 RID: 6137 RVA: 0x00030BF7 File Offset: 0x0002EDF7
		public XhtmlTextWriter(TextWriter writer, string tabString)
			: base(writer, tabString)
		{
		}

		/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object containing common attributes of the markup tags for the <see cref="T:System.Web.UI.XhtmlTextWriter" /> object.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> object containing common attributes.</returns>
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x0003EC4F File Offset: 0x0003CE4F
		protected Hashtable CommonAttributes
		{
			get
			{
				if (this.common_attrs == null)
				{
					this.common_attrs = (Hashtable)XhtmlTextWriter.default_common_attrs.Clone();
				}
				return this.common_attrs;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object containing element-specific attributes.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> object containing element-specific attributes.</returns>
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0003EC74 File Offset: 0x0003CE74
		protected Hashtable ElementSpecificAttributes
		{
			get
			{
				if (this.element_specific_attrs == null)
				{
					this.element_specific_attrs = (Hashtable)XhtmlTextWriter.default_element_specific_attrs.Clone();
				}
				return this.element_specific_attrs;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object of elements for which <see cref="P:System.Web.UI.XhtmlTextWriter.CommonAttributes" /> attributes are suppressed.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> of elements containing a collection of <see cref="P:System.Web.UI.XhtmlTextWriter.CommonAttributes" /> that are not rendered.</returns>
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0003EC99 File Offset: 0x0003CE99
		protected Hashtable SuppressCommonAttributes
		{
			get
			{
				if (this.suppress_common_attrs == null)
				{
					this.suppress_common_attrs = (Hashtable)XhtmlTextWriter.default_suppress_common_attrs.Clone();
				}
				return this.suppress_common_attrs;
			}
		}

		/// <summary>Adds an attribute to an XHTML element. The collection of element-specific attributes for the <see cref="T:System.Web.UI.XhtmlTextWriter" /> object is referenced by the <see cref="P:System.Web.UI.XhtmlTextWriter.ElementSpecificAttributes" /> property.</summary>
		/// <param name="elementName">The XHTML element to add the attribute to.</param>
		/// <param name="attributeName">The attribute to add.</param>
		// Token: 0x060017FD RID: 6141 RVA: 0x0003ECC0 File Offset: 0x0003CEC0
		public virtual void AddRecognizedAttribute(string elementName, string attributeName)
		{
			Hashtable hashtable = (Hashtable)this.ElementSpecificAttributes[elementName];
			if (hashtable == null)
			{
				Hashtable hashtable2 = new Hashtable();
				hashtable2.Add(attributeName, true);
				this.ElementSpecificAttributes.Add(elementName, hashtable2);
				return;
			}
			hashtable.Add(attributeName, true);
		}

		/// <summary>Checks an XHTML attribute to ensure that it can be rendered in the opening tag of a &lt;form&gt; element.</summary>
		/// <returns>true if the attribute can be applied to a &lt;form&gt; element; otherwise, false.</returns>
		/// <param name="attributeName">The attribute name to check. </param>
		// Token: 0x060017FE RID: 6142 RVA: 0x0003ED10 File Offset: 0x0003CF10
		public override bool IsValidFormAttribute(string attributeName)
		{
			return attributeName == "action" || attributeName == "method" || attributeName == "enctype";
		}

		/// <summary>Removes an attribute from the <see cref="P:System.Web.UI.XhtmlTextWriter.ElementSpecificAttributes" /> collection of an element.</summary>
		/// <param name="elementName">The XHTML element to remove an attribute from.</param>
		/// <param name="attributeName">The attribute to remove from the specified XHTML element.</param>
		// Token: 0x060017FF RID: 6143 RVA: 0x0003ED3C File Offset: 0x0003CF3C
		public virtual void RemoveRecognizedAttribute(string elementName, string attributeName)
		{
			Hashtable hashtable = (Hashtable)this.ElementSpecificAttributes[elementName];
			if (hashtable != null)
			{
				hashtable.Remove(attributeName);
			}
		}

		/// <summary>Specifies the XHTML document type for the text writer to render to the page or control.</summary>
		/// <param name="docType">One of the <see cref="T:System.Web.UI.XhtmlMobileDocType" /> enumeration values. </param>
		// Token: 0x06001800 RID: 6144 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void SetDocType(XhtmlMobileDocType docType)
		{
		}

		/// <summary>Writes a &lt;br/&gt; element to the XHTML output stream.</summary>
		// Token: 0x06001801 RID: 6145 RVA: 0x0003ED68 File Offset: 0x0003CF68
		public override void WriteBreak()
		{
			string tagName = this.GetTagName(HtmlTextWriterTag.Br);
			this.WriteBeginTag(tagName);
			this.Write('/');
			this.Write('>');
		}

		/// <summary>Determines whether the specified XHTML attribute and its value can be rendered to the current markup element.</summary>
		/// <returns>true if the attribute is rendered to the page; otherwise, false.</returns>
		/// <param name="name">The XHTML attribute to render. </param>
		/// <param name="value">The value assigned to the XHTML attribute. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> enumeration value associated with the XHTML attribute. </param>
		// Token: 0x06001802 RID: 6146 RVA: 0x0003ED95 File Offset: 0x0003CF95
		protected override bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
		{
			throw new ArgumentNullException();
		}

		/// <summary>Determines whether the specified XHTML style attribute and its value can be rendered to the current markup element.</summary>
		/// <returns>true if the style attribute is rendered; otherwise, false.</returns>
		/// <param name="name">The XHTML style attribute to render. </param>
		/// <param name="value">The value assigned to the XHTML style attribute. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value associated with the XHTML style attribute. </param>
		// Token: 0x06001803 RID: 6147 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected override bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
		{
			return false;
		}

		// Token: 0x04001611 RID: 5649
		private static Hashtable default_common_attrs = new Hashtable(XhtmlTextWriter.DefaultCommonAttributes.Length);

		// Token: 0x04001612 RID: 5650
		private static Hashtable default_suppress_common_attrs;

		// Token: 0x04001613 RID: 5651
		private static Hashtable default_element_specific_attrs;

		// Token: 0x04001614 RID: 5652
		private Hashtable common_attrs;

		// Token: 0x04001615 RID: 5653
		private Hashtable suppress_common_attrs;

		// Token: 0x04001616 RID: 5654
		private Hashtable element_specific_attrs;

		// Token: 0x04001617 RID: 5655
		private static string[] DefaultCommonAttributes = new string[] { "class", "id", "title", "xml:lang" };

		// Token: 0x04001618 RID: 5656
		private static string[] DefaultSuppressCommonAttributes = new string[] { "base", "meta", "br", "head", "title", "html", "style" };
	}
}
