using System;
using System.Collections;
using System.IO;

namespace System.Web.UI
{
	/// <summary>Writes a series of cHTML-specific characters and text to the output stream of an ASP.NET server control. The <see cref="T:System.Web.UI.ChtmlTextWriter" /> class provides formatting capabilities that ASP.NET server controls use when rendering cHTML content to clients.</summary>
	// Token: 0x020001A5 RID: 421
	public class ChtmlTextWriter : Html32TextWriter
	{
		// Token: 0x0600101A RID: 4122 RVA: 0x00029F9C File Offset: 0x0002819C
		static ChtmlTextWriter()
		{
			ChtmlTextWriter.SetupGlobalSuppressedAttrs(ChtmlTextWriter.global_suppressed_attributes);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0002A02B File Offset: 0x0002822B
		private static void SetupGlobalSuppressedAttrs(string[] attrs)
		{
			ChtmlTextWriter.global_suppressed_attrs = new Hashtable();
			ChtmlTextWriter.PopulateHash(ChtmlTextWriter.global_suppressed_attrs, ChtmlTextWriter.global_suppressed_attributes);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0002A048 File Offset: 0x00028248
		private static void PopulateHash(Hashtable hash, string[] keys)
		{
			foreach (string text in keys)
			{
				hash.Add(text, true);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> class that uses the <see cref="F:System.Web.UI.HtmlTextWriter.DefaultTabString" /> constant to indent lines.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the markup content. </param>
		// Token: 0x0600101D RID: 4125 RVA: 0x0002A076 File Offset: 0x00028276
		public ChtmlTextWriter(TextWriter writer)
			: this(writer, "\t")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> class with the specified line indentation.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the markup content. </param>
		/// <param name="tabString">The number of spaces defined in the <see cref="P:System.Web.UI.HtmlTextWriter.Indent" />. </param>
		// Token: 0x0600101E RID: 4126 RVA: 0x0002A084 File Offset: 0x00028284
		public ChtmlTextWriter(TextWriter writer, string tabString)
			: base(writer, tabString)
		{
			foreach (string text in ChtmlTextWriter.recognized_attributes)
			{
				this.recognized_attrs.Add(text, new Hashtable());
			}
			this.SetupSuppressedAttrs();
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0002A0F8 File Offset: 0x000282F8
		private void SetupSuppressedAttrs()
		{
			string[] array = new string[] { "accesskey", "cellspacing", "cellpadding", "gridlines", "rules" };
			string[] array2 = new string[] { "cellspacing", "cellpadding", "gridlines", "rules" };
			ChtmlTextWriter.Init("div", array, this.suppressed_attrs);
			ChtmlTextWriter.Init("span", array2, this.suppressed_attrs);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0002A180 File Offset: 0x00028380
		private static void Init(string key, string[] attrs, Hashtable container)
		{
			Hashtable hashtable = new Hashtable(attrs.Length);
			ChtmlTextWriter.PopulateHash(hashtable, attrs);
			container.Add(key, hashtable);
		}

		/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object of globally suppressed attributes that cannot be rendered on cHTML elements. </summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> of globally suppressed cHTML attributes.</returns>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0002A1A5 File Offset: 0x000283A5
		protected Hashtable GlobalSuppressedAttributes
		{
			get
			{
				return ChtmlTextWriter.global_suppressed_attrs;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object of recognized attributes that could be rendered on cHTML elements.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> of recognized cHTML attributes.</returns>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0002A1AC File Offset: 0x000283AC
		protected Hashtable RecognizedAttributes
		{
			get
			{
				return this.recognized_attrs;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object of user-specified suppressed attributes that are not rendered on cHTML elements.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> of suppressed cHTML attributes.</returns>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0002A1B4 File Offset: 0x000283B4
		protected Hashtable SuppressedAttributes
		{
			get
			{
				return this.suppressed_attrs;
			}
		}

		/// <summary>Adds an attribute to a cHTML element of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> object.</summary>
		/// <param name="elementName">The cHTML element to add the attribute to.</param>
		/// <param name="attributeName">The attribute to add to <paramref name="elementName" />.</param>
		// Token: 0x06001024 RID: 4132 RVA: 0x0002A1BC File Offset: 0x000283BC
		public virtual void AddRecognizedAttribute(string elementName, string attributeName)
		{
			Hashtable hashtable = (Hashtable)this.recognized_attrs[elementName];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				hashtable.Add(attributeName, true);
				this.recognized_attrs.Add(elementName, hashtable);
				return;
			}
			hashtable.Add(attributeName, true);
		}

		/// <summary>Removes an attribute of a cHTML element of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> object.</summary>
		/// <param name="elementName">The cHTML element to remove an attribute from.</param>
		/// <param name="attributeName">The attribute to remove from <paramref name="elementName" />.</param>
		// Token: 0x06001025 RID: 4133 RVA: 0x0002A20C File Offset: 0x0002840C
		public virtual void RemoveRecognizedAttribute(string elementName, string attributeName)
		{
			Hashtable hashtable = (Hashtable)this.recognized_attrs[elementName];
			if (hashtable != null)
			{
				hashtable.Remove(attributeName);
			}
		}

		/// <summary>Writes a br element to the cHTML output stream.</summary>
		// Token: 0x06001026 RID: 4134 RVA: 0x0002A238 File Offset: 0x00028438
		public override void WriteBreak()
		{
			string tagName = this.GetTagName(HtmlTextWriterTag.Br);
			this.WriteBeginTag(tagName);
			this.Write('>');
		}

		/// <summary>Encodes the specified text for the requesting device, and then writes it to the output stream. </summary>
		/// <param name="text">The text string to encode and write to the output stream. </param>
		// Token: 0x06001027 RID: 4135 RVA: 0x0002A25D File Offset: 0x0002845D
		public override void WriteEncodedText(string text)
		{
			base.WriteEncodedText(text);
		}

		/// <summary>Determines whether the specified cHTML attribute and its value are rendered to the requesting page. You can override the <see cref="M:System.Web.UI.ChtmlTextWriter.OnAttributeRender(System.String,System.String,System.Web.UI.HtmlTextWriterAttribute)" /> method in classes that derive from the <see cref="T:System.Web.UI.ChtmlTextWriter" /> class to filter out attributes that you do not want to render on devices that support cHTML.</summary>
		/// <returns>true to write the attribute and its value to the <see cref="T:System.Web.UI.ChtmlTextWriter" /> output stream; otherwise, false.</returns>
		/// <param name="name">The cHTML attribute to render. </param>
		/// <param name="value">The value assigned to <paramref name="name" />. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with <paramref name="name" />. </param>
		// Token: 0x06001028 RID: 4136 RVA: 0x0002A266 File Offset: 0x00028466
		protected override bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
		{
			return (bool)this.attr_render[null];
		}

		/// <summary>Determines whether the specified cHTML markup style attribute and its value can be rendered to the current markup element.</summary>
		/// <returns>true if the style can be rendered; otherwise, false.</returns>
		/// <param name="name">A string containing the name of the style attribute to render. </param>
		/// <param name="value">A string containing the value that is assigned to <paramref name="name" />. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with <paramref name="name" />.</param>
		// Token: 0x06001029 RID: 4137 RVA: 0x0002A279 File Offset: 0x00028479
		protected override bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
		{
			return key == HtmlTextWriterStyle.Display;
		}

		/// <summary>Determines whether the specified cHTML markup element is rendered to the requesting page. </summary>
		/// <returns>true if the specified cHTML markup element can be rendered; otherwise, false.</returns>
		/// <param name="name">A string containing the name of the cHTML element to render.</param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> associated with <paramref name="name" />.</param>
		// Token: 0x0600102A RID: 4138 RVA: 0x0002A280 File Offset: 0x00028480
		protected override bool OnTagRender(string name, HtmlTextWriterTag key)
		{
			return key != HtmlTextWriterTag.Span;
		}

		// Token: 0x04001363 RID: 4963
		private static Hashtable global_suppressed_attrs;

		// Token: 0x04001364 RID: 4964
		private static string[] global_suppressed_attributes = new string[] { "onclick", "ondblclick", "onmousedown", "onmouseup", "onmouseover", "onmousemove", "onmouseout", "onkeypress", "onkeydown", "onkeyup" };

		// Token: 0x04001365 RID: 4965
		private static string[] recognized_attributes = new string[] { "div", "span" };

		// Token: 0x04001366 RID: 4966
		private Hashtable recognized_attrs = new Hashtable(ChtmlTextWriter.recognized_attributes.Length);

		// Token: 0x04001367 RID: 4967
		private Hashtable suppressed_attrs = new Hashtable(ChtmlTextWriter.recognized_attributes.Length);

		// Token: 0x04001368 RID: 4968
		private Hashtable attr_render = new Hashtable();
	}
}
