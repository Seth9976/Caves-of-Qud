using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Writes a series of HTML 3.2–specific characters and text to the output stream for an ASP.NET server control. The <see cref="T:System.Web.UI.Html32TextWriter" /> class provides formatting capabilities that ASP.NET server controls use when rendering HTML 3.2 content to clients.</summary>
	// Token: 0x020001D0 RID: 464
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Html32TextWriter : HtmlTextWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Html32TextWriter" /> class that uses the line indentation that is specified in the <see cref="F:System.Web.UI.HtmlTextWriter.DefaultTabString" /> field when the requesting browser requires line indentation.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the HMTL content. </param>
		// Token: 0x060012C0 RID: 4800 RVA: 0x00030BEE File Offset: 0x0002EDEE
		public Html32TextWriter(TextWriter writer)
			: base(writer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Html32TextWriter" /> class that uses the specified line indentation.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the HMTL 3.2 content. </param>
		/// <param name="tabString">A string that represents the number of spaces defined by the <see cref="P:System.Web.UI.HtmlTextWriter.Indent" />. </param>
		// Token: 0x060012C1 RID: 4801 RVA: 0x00030BF7 File Offset: 0x0002EDF7
		public Html32TextWriter(TextWriter writer, string tabString)
			: base(writer, tabString)
		{
		}

		/// <summary>Gets or sets a Boolean value indicating whether to replace a Table element with a Div element to reduce the time that it takes to render a block of HTML.</summary>
		/// <returns>true to replace Table with Div; otherwise, false.</returns>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00030C01 File Offset: 0x0002EE01
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x00030C09 File Offset: 0x0002EE09
		[global::System.MonoTODO("no effect on html generation")]
		public bool ShouldPerformDivTableSubstitution
		{
			get
			{
				return this.div_table_substitution;
			}
			set
			{
				this.div_table_substitution = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether the requesting device supports bold HTML text. Use the <see cref="P:System.Web.UI.Html32TextWriter.SupportsBold" /> property to conditionally render bold text to the <see cref="T:System.Web.UI.Html32TextWriter" /> output stream.</summary>
		/// <returns>true if the requesting device supports bold text; otherwise, false. The default is true.</returns>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00030C12 File Offset: 0x0002EE12
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x00030C1A File Offset: 0x0002EE1A
		[global::System.MonoTODO("no effect on html generation")]
		public bool SupportsBold
		{
			get
			{
				return this.bold;
			}
			set
			{
				this.bold = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether the requesting device supports italic HTML text. Use the <see cref="P:System.Web.UI.Html32TextWriter.SupportsItalic" /> property to conditionally render italicized text to the <see cref="T:System.Web.UI.Html32TextWriter" /> output stream.</summary>
		/// <returns>true if the requesting device supports italic text; otherwise, false. The default is true.</returns>
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00030C23 File Offset: 0x0002EE23
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x00030C2B File Offset: 0x0002EE2B
		[global::System.MonoTODO("no effect on html generation")]
		public bool SupportsItalic
		{
			get
			{
				return this.italic;
			}
			set
			{
				this.italic = value;
			}
		}

		/// <summary>Writes the opening tag of the specified element to the HTML 3.2 output stream.</summary>
		/// <param name="tagKey">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value that indicates which HTML element to write. </param>
		// Token: 0x060012C8 RID: 4808 RVA: 0x00030C34 File Offset: 0x0002EE34
		public override void RenderBeginTag(HtmlTextWriterTag tagKey)
		{
			base.RenderBeginTag(tagKey);
		}

		/// <summary>Writes the end tag of an HTML element to the <see cref="T:System.Web.UI.Html32TextWriter" /> output stream, along with any font information that is associated with the element.</summary>
		// Token: 0x060012C9 RID: 4809 RVA: 0x00030C3D File Offset: 0x0002EE3D
		public override void RenderEndTag()
		{
			base.RenderEndTag();
		}

		/// <summary>Returns the HTML element that is associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value.</summary>
		/// <returns>The HTML element.</returns>
		/// <param name="tagKey">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value to obtain the HTML element for. </param>
		// Token: 0x060012CA RID: 4810 RVA: 0x00030C45 File Offset: 0x0002EE45
		protected override string GetTagName(HtmlTextWriterTag tagKey)
		{
			if (tagKey == HtmlTextWriterTag.Unknown || !Enum.IsDefined(typeof(HtmlTextWriterTag), tagKey))
			{
				return string.Empty;
			}
			return tagKey.ToString().ToLower(Helpers.InvariantCulture);
		}

		/// <summary>Determines whether to write the specified HTML style attribute and its value to the output stream.</summary>
		/// <returns>true if the HTML style attribute and its value will be rendered to the output stream; otherwise, false.</returns>
		/// <param name="name">The HTML style attribute to write to the output stream. </param>
		/// <param name="value">The value associated with the HTML style attribute. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value associated with the HTML style attribute. </param>
		// Token: 0x060012CB RID: 4811 RVA: 0x00030C7E File Offset: 0x0002EE7E
		protected override bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
		{
			return base.OnStyleAttributeRender(name, value, key);
		}

		/// <summary>Determines whether to write the specified HTML element to the output stream.</summary>
		/// <returns>true if the HTML element is written to the output stream; otherwise, false.</returns>
		/// <param name="name">The HTML element to write to the output stream. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value associated with the HTML element. </param>
		// Token: 0x060012CC RID: 4812 RVA: 0x00030C89 File Offset: 0x0002EE89
		protected override bool OnTagRender(string name, HtmlTextWriterTag key)
		{
			return base.OnTagRender(name, key);
		}

		/// <summary>Writes any text or spacing that appears after the content of the HTML element.</summary>
		/// <returns>The spacing or text to write after the content of the HTML element; otherwise, if there is no such information to render, null.</returns>
		// Token: 0x060012CD RID: 4813 RVA: 0x00030C93 File Offset: 0x0002EE93
		protected override string RenderAfterContent()
		{
			return base.RenderAfterContent();
		}

		/// <summary>Writes any spacing or text that occurs after an HTML element's closing tag.</summary>
		/// <returns>The spacing or text to write after the closing tag of the HTML element; otherwise, if there is no such information to render, null.</returns>
		// Token: 0x060012CE RID: 4814 RVA: 0x00030C9B File Offset: 0x0002EE9B
		protected override string RenderAfterTag()
		{
			return base.RenderAfterTag();
		}

		/// <summary>Writes any tab spacing or font information that appears before the content that is contained in an HTML element.</summary>
		/// <returns>The font information or spacing to write before rendering the content of the HTML element; otherwise, if there is no such information to render, null.</returns>
		// Token: 0x060012CF RID: 4815 RVA: 0x00030CA3 File Offset: 0x0002EEA3
		protected override string RenderBeforeContent()
		{
			return base.RenderBeforeContent();
		}

		/// <summary>Writes any text or tab spacing that occurs before the opening tag of an HTML element to the HTML 3.2 output stream.</summary>
		/// <returns>The HTML font and spacing information to render before the tag; otherwise, if there is no such information to render, null.</returns>
		// Token: 0x060012D0 RID: 4816 RVA: 0x00030CAB File Offset: 0x0002EEAB
		protected override string RenderBeforeTag()
		{
			return base.RenderBeforeTag();
		}

		/// <summary>Gets a collection of font information for the HTML to render.</summary>
		/// <returns>The collection of font information.</returns>
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00003BF6 File Offset: 0x00001DF6
		[global::System.MonoTODO("Not implemented, always returns null")]
		protected Stack FontStack
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001435 RID: 5173
		private bool div_table_substitution;

		// Token: 0x04001436 RID: 5174
		private bool bold;

		// Token: 0x04001437 RID: 5175
		private bool italic;
	}
}
