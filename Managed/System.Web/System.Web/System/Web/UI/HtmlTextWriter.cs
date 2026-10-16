using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	/// <summary>Writes markup characters and text to an ASP.NET server control output stream. This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.</summary>
	// Token: 0x020001D2 RID: 466
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTextWriter : TextWriter
	{
		// Token: 0x060012D4 RID: 4820 RVA: 0x00030CCC File Offset: 0x0002EECC
		static HtmlTextWriter()
		{
			foreach (HtmlTextWriter.HtmlTag htmlTag in HtmlTextWriter.tags)
			{
				HtmlTextWriter._tagTable.Add(htmlTag.name, htmlTag);
			}
			foreach (HtmlTextWriter.HtmlAttribute htmlAttribute in HtmlTextWriter.htmlattrs)
			{
				HtmlTextWriter._attributeTable.Add(htmlAttribute.name, htmlAttribute);
			}
			foreach (HtmlTextWriter.HtmlStyle htmlStyle in HtmlTextWriter.htmlstyles)
			{
				HtmlTextWriter._styleTable.Add(htmlStyle.name, htmlStyle);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> class that uses a default tab string.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> instance that renders the markup content. </param>
		// Token: 0x060012D5 RID: 4821 RVA: 0x00031A0E File Offset: 0x0002FC0E
		public HtmlTextWriter(TextWriter writer)
			: this(writer, "\t")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> class with a specified tab string character.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the markup content. </param>
		/// <param name="tabString">The string to use to render a line indentation. </param>
		// Token: 0x060012D6 RID: 4822 RVA: 0x00031A1C File Offset: 0x0002FC1C
		public HtmlTextWriter(TextWriter writer, string tabString)
		{
			this.b = writer;
			this.tab_string = tabString;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00031A47 File Offset: 0x0002FC47
		internal static string StaticGetStyleName(HtmlTextWriterStyle styleKey)
		{
			if (styleKey < (HtmlTextWriterStyle)HtmlTextWriter.htmlstyles.Length)
			{
				return HtmlTextWriter.htmlstyles[(int)styleKey].name;
			}
			return null;
		}

		/// <summary>Registers markup attributes, whether literals or dynamically generated, from the source file so that they can be properly rendered to the requesting client.</summary>
		/// <param name="name">A string containing the name of the markup attribute to register. </param>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that corresponds with the attribute name. </param>
		// Token: 0x060012D8 RID: 4824 RVA: 0x00003946 File Offset: 0x00001B46
		[global::System.MonoTODO("Does nothing")]
		protected static void RegisterAttribute(string name, HtmlTextWriterAttribute key)
		{
		}

		/// <summary>Registers markup style properties, whether literals or dynamically generated, from the source file so that they can be properly rendered to the requesting client.</summary>
		/// <param name="name">The string passed from the source file specifying the style name. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> that corresponds with the specified style. </param>
		// Token: 0x060012D9 RID: 4825 RVA: 0x00003946 File Offset: 0x00001B46
		[global::System.MonoTODO("Does nothing")]
		protected static void RegisterStyle(string name, HtmlTextWriterStyle key)
		{
		}

		/// <summary>Registers markup tags, whether literals or dynamically generated, from the source file so that they can be properly rendered to the requesting client.</summary>
		/// <param name="name">A string that contains the HTML tag. </param>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies which element to render. </param>
		// Token: 0x060012DA RID: 4826 RVA: 0x00003946 File Offset: 0x00001B46
		[global::System.MonoTODO("Does nothing")]
		protected static void RegisterTag(string name, HtmlTextWriterTag key)
		{
		}

		/// <summary>Adds the markup attribute and the attribute value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method, with optional encoding.</summary>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that represents the markup attribute to add to the output stream. </param>
		/// <param name="value">A string containing the value to assign to the attribute. </param>
		/// <param name="fEncode">true to encode the attribute and its value; otherwise, false. </param>
		// Token: 0x060012DB RID: 4827 RVA: 0x00031A61 File Offset: 0x0002FC61
		public virtual void AddAttribute(HtmlTextWriterAttribute key, string value, bool fEncode)
		{
			if (fEncode)
			{
				value = HttpUtility.HtmlAttributeEncode(value);
			}
			this.AddAttribute(this.GetAttributeName(key), value, key);
		}

		/// <summary>Adds the markup attribute and the attribute value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that represents the markup attribute to add to the output stream. </param>
		/// <param name="value">A string containing the value to assign to the attribute. </param>
		// Token: 0x060012DC RID: 4828 RVA: 0x00031A7D File Offset: 0x0002FC7D
		public virtual void AddAttribute(HtmlTextWriterAttribute key, string value)
		{
			if (key != HtmlTextWriterAttribute.Name && key != HtmlTextWriterAttribute.Id)
			{
				value = HttpUtility.HtmlAttributeEncode(value);
			}
			this.AddAttribute(this.GetAttributeName(key), value, key);
		}

		/// <summary>Adds the specified markup attribute and value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method, with optional encoding.</summary>
		/// <param name="name">A string containing the name of the attribute to add. </param>
		/// <param name="value">A string containing the value to assign to the attribute. </param>
		/// <param name="fEndode">true to encode the attribute and its value; otherwise, false. </param>
		// Token: 0x060012DD RID: 4829 RVA: 0x00031AA0 File Offset: 0x0002FCA0
		public virtual void AddAttribute(string name, string value, bool fEndode)
		{
			if (fEndode)
			{
				value = HttpUtility.HtmlAttributeEncode(value);
			}
			this.AddAttribute(name, value, this.GetAttributeKey(name));
		}

		/// <summary>Adds the specified markup attribute and value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="name">A string containing the name of the attribute to add. </param>
		/// <param name="value">A string containing the value to assign to the attribute. </param>
		// Token: 0x060012DE RID: 4830 RVA: 0x00031ABC File Offset: 0x0002FCBC
		public virtual void AddAttribute(string name, string value)
		{
			HtmlTextWriterAttribute attributeKey = this.GetAttributeKey(name);
			if (attributeKey != HtmlTextWriterAttribute.Name && attributeKey != HtmlTextWriterAttribute.Id)
			{
				value = HttpUtility.HtmlAttributeEncode(value);
			}
			this.AddAttribute(name, value, attributeKey);
		}

		/// <summary>Adds the specified markup attribute and value, along with an <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> enumeration value, to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="name">A string containing the name of the attribute to add. </param>
		/// <param name="value">A string containing the value to assign to the attribute. </param>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that represents the attribute. </param>
		// Token: 0x060012DF RID: 4831 RVA: 0x00031AEC File Offset: 0x0002FCEC
		protected virtual void AddAttribute(string name, string value, HtmlTextWriterAttribute key)
		{
			this.NextAttrStack();
			this.attrs[this.attrs_pos].name = name;
			this.attrs[this.attrs_pos].value = value;
			this.attrs[this.attrs_pos].key = key;
		}

		/// <summary>Adds the specified markup style attribute and the attribute value, along with an <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value, to the opening markup tag created by a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="name">A string that contains the style attribute to be added. </param>
		/// <param name="value">A string that contains the value to assign to the attribute. </param>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> that represents the style attribute to add. </param>
		// Token: 0x060012E0 RID: 4832 RVA: 0x00031B44 File Offset: 0x0002FD44
		protected virtual void AddStyleAttribute(string name, string value, HtmlTextWriterStyle key)
		{
			this.NextStyleStack();
			this.styles[this.styles_pos].name = name;
			value = HttpUtility.HtmlAttributeEncode(value);
			this.styles[this.styles_pos].value = value;
			this.styles[this.styles_pos].key = key;
		}

		/// <summary>Adds the specified markup style attribute and the attribute value to the opening markup tag created by a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="name">A string that contains the style attribute to add. </param>
		/// <param name="value">A string that contains the value to assign to the attribute. </param>
		// Token: 0x060012E1 RID: 4833 RVA: 0x00031BA4 File Offset: 0x0002FDA4
		public virtual void AddStyleAttribute(string name, string value)
		{
			this.AddStyleAttribute(name, value, this.GetStyleKey(name));
		}

		/// <summary>Adds the markup style attribute associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> value and the attribute value to the opening markup tag created by a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> that represents the style attribute to add to the output stream. </param>
		/// <param name="value">A string that contains the value to assign to the attribute. </param>
		// Token: 0x060012E2 RID: 4834 RVA: 0x00031BB5 File Offset: 0x0002FDB5
		public virtual void AddStyleAttribute(HtmlTextWriterStyle key, string value)
		{
			this.AddStyleAttribute(this.GetStyleName(key), value, key);
		}

		/// <summary>Closes the <see cref="T:System.Web.UI.HtmlTextWriter" /> object and releases any system resources associated with it.</summary>
		// Token: 0x060012E3 RID: 4835 RVA: 0x00031BC6 File Offset: 0x0002FDC6
		public override void Close()
		{
			this.b.Close();
		}

		/// <summary>Encodes the value of the specified markup attribute based on the requirements of the <see cref="T:System.Web.HttpRequest" /> object of the current context.</summary>
		/// <returns>A string containing the encoded attribute value.</returns>
		/// <param name="attrKey">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> representing the markup attribute. </param>
		/// <param name="value">A string containing the attribute value to encode. </param>
		// Token: 0x060012E4 RID: 4836 RVA: 0x00031BD3 File Offset: 0x0002FDD3
		protected virtual string EncodeAttributeValue(HtmlTextWriterAttribute attrKey, string value)
		{
			return HttpUtility.HtmlAttributeEncode(value);
		}

		/// <summary>Encodes the value of the specified markup attribute based on the requirements of the <see cref="T:System.Web.HttpRequest" /> object of the current context.</summary>
		/// <returns>A string containing the encoded attribute value, null if <paramref name="value" /> is empty, or the unencoded attribute value if <paramref name="fEncode" /> is false.</returns>
		/// <param name="value">A string containing the attribute value to encode. </param>
		/// <param name="fEncode">true to encode the attribute value; otherwise, false. </param>
		// Token: 0x060012E5 RID: 4837 RVA: 0x00031BDB File Offset: 0x0002FDDB
		protected string EncodeAttributeValue(string value, bool fEncode)
		{
			if (fEncode)
			{
				return HttpUtility.HtmlAttributeEncode(value);
			}
			return value;
		}

		/// <summary>Performs minimal URL encoding by converting spaces in the specified URL to the string "%20".</summary>
		/// <returns>A string containing the encoded URL.</returns>
		/// <param name="url">A string containing the URL to encode. </param>
		// Token: 0x060012E6 RID: 4838 RVA: 0x00031BE8 File Offset: 0x0002FDE8
		protected string EncodeUrl(string url)
		{
			return HttpUtility.UrlPathEncode(url);
		}

		/// <summary>Removes all the markup and style attributes on all properties of the page or Web server control.</summary>
		// Token: 0x060012E7 RID: 4839 RVA: 0x00031BF0 File Offset: 0x0002FDF0
		protected virtual void FilterAttributes()
		{
			HtmlTextWriter.AddedAttr addedAttr = default(HtmlTextWriter.AddedAttr);
			for (int i = 0; i <= this.attrs_pos; i++)
			{
				HtmlTextWriter.AddedAttr addedAttr2 = this.attrs[i];
				if (this.OnAttributeRender(addedAttr2.name, addedAttr2.value, addedAttr2.key))
				{
					if (addedAttr2.key == HtmlTextWriterAttribute.Style)
					{
						addedAttr = addedAttr2;
					}
					else
					{
						this.WriteAttribute(addedAttr2.name, addedAttr2.value, false);
					}
				}
			}
			if (this.styles_pos != -1 || addedAttr.value != null)
			{
				this.Write(' ');
				this.Write("style");
				this.Write("=\"");
				for (int j = 0; j <= this.styles_pos; j++)
				{
					HtmlTextWriter.AddedStyle addedStyle = this.styles[j];
					if (this.OnStyleAttributeRender(addedStyle.name, addedStyle.value, addedStyle.key))
					{
						if (addedStyle.key == HtmlTextWriterStyle.BackgroundImage)
						{
							addedStyle.value = "url(" + HttpUtility.UrlPathEncode(addedStyle.value) + ")";
						}
						this.WriteStyleAttribute(addedStyle.name, addedStyle.value, false);
					}
				}
				this.Write(addedAttr.value);
				this.Write('"');
			}
			this.styles_pos = (this.attrs_pos = -1);
		}

		/// <summary>Clears all buffers for the current <see cref="T:System.Web.UI.HtmlTextWriter" /> object and causes any buffered data to be written to the output stream.</summary>
		// Token: 0x060012E8 RID: 4840 RVA: 0x00031D33 File Offset: 0x0002FF33
		public override void Flush()
		{
			this.b.Flush();
		}

		/// <summary>Obtains the corresponding <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> enumeration value for the specified attribute.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> enumeration value for the specified attribute; otherwise, an invalid <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> value if the attribute is not a member of the enumeration.</returns>
		/// <param name="attrName">A string that contains the attribute for which to obtain the <see cref="T:System.Web.UI.HtmlTextWriterAttribute" />. </param>
		// Token: 0x060012E9 RID: 4841 RVA: 0x00031D40 File Offset: 0x0002FF40
		protected HtmlTextWriterAttribute GetAttributeKey(string attrName)
		{
			object obj = HtmlTextWriter._attributeTable[attrName];
			if (obj == null)
			{
				return (HtmlTextWriterAttribute)(-1);
			}
			return ((HtmlTextWriter.HtmlAttribute)obj).key;
		}

		/// <summary>Obtains the name of the markup attribute associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> value.</summary>
		/// <returns>A string containing the name of the markup attribute.</returns>
		/// <param name="attrKey">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> to obtain the markup attribute name for. </param>
		// Token: 0x060012EA RID: 4842 RVA: 0x00031D69 File Offset: 0x0002FF69
		protected string GetAttributeName(HtmlTextWriterAttribute attrKey)
		{
			if (attrKey < (HtmlTextWriterAttribute)HtmlTextWriter.htmlattrs.Length)
			{
				return HtmlTextWriter.htmlattrs[(int)attrKey].name;
			}
			return null;
		}

		/// <summary>Obtains the <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value for the specified style.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value corresponding to <paramref name="styleName" />.</returns>
		/// <param name="styleName">The style attribute for which to obtain the <see cref="T:System.Web.UI.HtmlTextWriterStyle" />. </param>
		// Token: 0x060012EB RID: 4843 RVA: 0x00031D84 File Offset: 0x0002FF84
		protected HtmlTextWriterStyle GetStyleKey(string styleName)
		{
			object obj = HtmlTextWriter._styleTable[styleName];
			if (obj == null)
			{
				return (HtmlTextWriterStyle)(-1);
			}
			return ((HtmlTextWriter.HtmlStyle)obj).key;
		}

		/// <summary>Obtains the markup style attribute name associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value.</summary>
		/// <returns>The style attribute name associated with the <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value specified in <paramref name="styleKey" />.</returns>
		/// <param name="styleKey">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> to obtain the style attribute name for. </param>
		// Token: 0x060012EC RID: 4844 RVA: 0x00031DAD File Offset: 0x0002FFAD
		protected string GetStyleName(HtmlTextWriterStyle styleKey)
		{
			return HtmlTextWriter.StaticGetStyleName(styleKey);
		}

		/// <summary>Obtains the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value associated with the specified markup element.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value; otherwise, if <paramref name="tagName" /> is not associated with a specific <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value, <see cref="F:System.Web.UI.HtmlTextWriterTag.Unknown" />.</returns>
		/// <param name="tagName">The markup element for which to obtain the <see cref="T:System.Web.UI.HtmlTextWriterTag" />. </param>
		// Token: 0x060012ED RID: 4845 RVA: 0x00031DB8 File Offset: 0x0002FFB8
		protected virtual HtmlTextWriterTag GetTagKey(string tagName)
		{
			object obj = HtmlTextWriter._tagTable[tagName];
			if (obj == null)
			{
				return HtmlTextWriterTag.Unknown;
			}
			return ((HtmlTextWriter.HtmlTag)obj).key;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00031DE1 File Offset: 0x0002FFE1
		internal static string StaticGetTagName(HtmlTextWriterTag tagKey)
		{
			if (tagKey < (HtmlTextWriterTag)HtmlTextWriter.tags.Length)
			{
				return HtmlTextWriter.tags[(int)tagKey].name;
			}
			return null;
		}

		/// <summary>Obtains the markup element associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value.</summary>
		/// <returns>A string representing the markup element.</returns>
		/// <param name="tagKey">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> to obtain the markup element for. </param>
		// Token: 0x060012EF RID: 4847 RVA: 0x00031DFB File Offset: 0x0002FFFB
		protected virtual string GetTagName(HtmlTextWriterTag tagKey)
		{
			if (tagKey < (HtmlTextWriterTag)HtmlTextWriter.tags.Length)
			{
				return HtmlTextWriter.tags[(int)tagKey].name;
			}
			return null;
		}

		/// <summary>Determines whether the specified markup attribute and its value are rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <returns>true if the attribute is rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, false.</returns>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with the markup attribute. </param>
		// Token: 0x060012F0 RID: 4848 RVA: 0x00031E18 File Offset: 0x00030018
		protected bool IsAttributeDefined(HtmlTextWriterAttribute key)
		{
			string text;
			return this.IsAttributeDefined(key, out text);
		}

		/// <summary>Determines whether the specified markup attribute and its value are rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <returns>true if the attribute is rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, false.</returns>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with the markup attribute. </param>
		/// <param name="value">The value assigned to the attribute. </param>
		// Token: 0x060012F1 RID: 4849 RVA: 0x00031E30 File Offset: 0x00030030
		protected bool IsAttributeDefined(HtmlTextWriterAttribute key, out string value)
		{
			for (int i = 0; i <= this.attrs_pos; i++)
			{
				if (this.attrs[i].key == key)
				{
					value = this.attrs[i].value;
					return true;
				}
			}
			value = null;
			return false;
		}

		/// <summary>Determines whether the specified markup style attribute is rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <returns>true if the attribute will be rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, false.</returns>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with the attribute. </param>
		// Token: 0x060012F2 RID: 4850 RVA: 0x00031E7C File Offset: 0x0003007C
		protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key)
		{
			string text;
			return this.IsStyleAttributeDefined(key, out text);
		}

		/// <summary>Determines whether the specified markup style attribute and its value are rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
		/// <returns>true if the attribute and its value will be rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, false.</returns>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with the attribute. </param>
		/// <param name="value">The value assigned to the style attribute. </param>
		// Token: 0x060012F3 RID: 4851 RVA: 0x00031E94 File Offset: 0x00030094
		protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key, out string value)
		{
			for (int i = 0; i <= this.styles_pos; i++)
			{
				if (this.styles[i].key == key)
				{
					value = this.styles[i].value;
					return true;
				}
			}
			value = null;
			return false;
		}

		/// <summary>Determines whether the specified markup attribute and its value can be rendered to the current markup element.</summary>
		/// <returns>Always true.</returns>
		/// <param name="name">A string containing the name of the attribute to render. </param>
		/// <param name="value">A string containing the value that is assigned to the attribute. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with the markup attribute. </param>
		// Token: 0x060012F4 RID: 4852 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected virtual bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
		{
			return true;
		}

		/// <summary>Determines whether the specified markup style attribute and its value can be rendered to the current markup element.</summary>
		/// <returns>Always true.</returns>
		/// <param name="name">A string containing the name of the style attribute to render. </param>
		/// <param name="value">A string containing the value that is assigned to the style attribute. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with the style attribute. </param>
		// Token: 0x060012F5 RID: 4853 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected virtual bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
		{
			return true;
		}

		/// <summary>Determines whether the specified markup element will be rendered to the requesting page.</summary>
		/// <returns>Always true.</returns>
		/// <param name="name">A string containing the name of the element to render. </param>
		/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> associated with the element. </param>
		// Token: 0x060012F6 RID: 4854 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected virtual bool OnTagRender(string name, HtmlTextWriterTag key)
		{
			return true;
		}

		/// <summary>Writes a series of tab strings that represent the indentation level for a line of markup characters.</summary>
		// Token: 0x060012F7 RID: 4855 RVA: 0x00031EE0 File Offset: 0x000300E0
		protected virtual void OutputTabs()
		{
			if (!this.newline)
			{
				return;
			}
			this.newline = false;
			for (int i = 0; i < this.Indent; i++)
			{
				this.b.Write(this.tab_string);
			}
		}

		/// <summary>Removes the most recently saved markup element from the list of rendered elements.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the most recently rendered markup element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The list of rendered elements is empty. </exception>
		// Token: 0x060012F8 RID: 4856 RVA: 0x00031F1F File Offset: 0x0003011F
		protected string PopEndTag()
		{
			if (this.tagstack_pos == -1)
			{
				throw new InvalidOperationException();
			}
			string tagName = this.TagName;
			this.tagstack_pos--;
			return tagName;
		}

		/// <summary>Saves the specified markup element for later use when generating the end tag for a markup element.</summary>
		/// <param name="endTag">The closing tag of the markup element. </param>
		// Token: 0x060012F9 RID: 4857 RVA: 0x00031F44 File Offset: 0x00030144
		protected void PushEndTag(string endTag)
		{
			this.NextTagStack();
			this.TagName = endTag;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00031F53 File Offset: 0x00030153
		private void PushEndTag(HtmlTextWriterTag t)
		{
			this.NextTagStack();
			this.TagKey = t;
		}

		/// <summary>Writes any text or spacing that occurs after the content and before the closing tag of the markup element to the markup output stream.</summary>
		/// <returns>A string that contains the spacing or text to write after the content of the element. </returns>
		// Token: 0x060012FB RID: 4859 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected virtual string RenderAfterContent()
		{
			return null;
		}

		/// <summary>Writes any spacing or text that occurs after the closing tag for a markup element.</summary>
		/// <returns>The spacing or text to write after the closing tag of the element. </returns>
		// Token: 0x060012FC RID: 4860 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected virtual string RenderAfterTag()
		{
			return null;
		}

		/// <summary>Writes any text or spacing before the content and after the opening tag of a markup element.</summary>
		/// <returns>The text or spacing to write prior to the content of the element. If not overridden, <see cref="M:System.Web.UI.HtmlTextWriter.RenderBeforeContent" /> returns null.</returns>
		// Token: 0x060012FD RID: 4861 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected virtual string RenderBeforeContent()
		{
			return null;
		}

		/// <summary>Writes any text or spacing that occurs before the opening tag of a markup element.</summary>
		/// <returns>The text or spacing to write before the markup element opening tag. If not overridden, null.</returns>
		// Token: 0x060012FE RID: 4862 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected virtual string RenderBeforeTag()
		{
			return null;
		}

		/// <summary>Writes the opening tag of the specified markup element to the output stream.</summary>
		/// <param name="tagName">A string containing the name of the markup element for which to render the opening tag.</param>
		// Token: 0x060012FF RID: 4863 RVA: 0x00031F64 File Offset: 0x00030164
		public virtual void RenderBeginTag(string tagName)
		{
			bool flag = !this.OnTagRender(tagName, this.GetTagKey(tagName));
			this.PushEndTag(tagName);
			this.TagIgnore = flag;
			this.DoBeginTag();
		}

		/// <summary>Writes the opening tag of the markup element associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value to the output stream.</summary>
		/// <param name="tagKey">One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> values that defines the opening tag of the markup element to render. </param>
		// Token: 0x06001300 RID: 4864 RVA: 0x00031F98 File Offset: 0x00030198
		public virtual void RenderBeginTag(HtmlTextWriterTag tagKey)
		{
			bool flag = !this.OnTagRender(this.GetTagName(tagKey), tagKey);
			this.PushEndTag(tagKey);
			this.DoBeginTag();
			this.TagIgnore = flag;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00031FCB File Offset: 0x000301CB
		private void WriteIfNotNull(string s)
		{
			if (s != null)
			{
				this.Write(s);
			}
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00031FD8 File Offset: 0x000301D8
		private void DoBeginTag()
		{
			this.WriteIfNotNull(this.RenderBeforeTag());
			if (!this.TagIgnore)
			{
				this.WriteBeginTag(this.TagName);
				this.FilterAttributes();
				HtmlTextWriterTag htmlTextWriterTag = ((this.TagKey < (HtmlTextWriterTag)HtmlTextWriter.tags.Length) ? this.TagKey : HtmlTextWriterTag.Unknown);
				switch (HtmlTextWriter.tags[(int)htmlTextWriterTag].tag_type)
				{
				case HtmlTextWriter.TagType.Block:
				{
					this.Write('>');
					this.WriteLine();
					int num = this.Indent;
					this.Indent = num + 1;
					break;
				}
				case HtmlTextWriter.TagType.Inline:
					this.Write('>');
					break;
				case HtmlTextWriter.TagType.SelfClosing:
					this.Write(" />");
					break;
				}
			}
			this.WriteIfNotNull(this.RenderBeforeContent());
		}

		/// <summary>Writes the end tag of a markup element to the output stream.</summary>
		// Token: 0x06001303 RID: 4867 RVA: 0x0003208C File Offset: 0x0003028C
		public virtual void RenderEndTag()
		{
			this.WriteIfNotNull(this.RenderAfterContent());
			if (!this.TagIgnore)
			{
				HtmlTextWriterTag htmlTextWriterTag = ((this.TagKey < (HtmlTextWriterTag)HtmlTextWriter.tags.Length) ? this.TagKey : HtmlTextWriterTag.Unknown);
				switch (HtmlTextWriter.tags[(int)htmlTextWriterTag].tag_type)
				{
				case HtmlTextWriter.TagType.Block:
				{
					int num = this.Indent;
					this.Indent = num - 1;
					this.WriteLineNoTabs(string.Empty);
					this.WriteEndTag(this.TagName);
					break;
				}
				case HtmlTextWriter.TagType.Inline:
					this.WriteEndTag(this.TagName);
					break;
				}
			}
			this.WriteIfNotNull(this.RenderAfterTag());
			this.PopEndTag();
		}

		/// <summary>Writes the specified markup attribute and value to the output stream, and, if specified, writes the value encoded.</summary>
		/// <param name="name">The markup attribute to write to the output stream. </param>
		/// <param name="value">The value assigned to the attribute. </param>
		/// <param name="fEncode">true to encode the attribute and its assigned value; otherwise, false. </param>
		// Token: 0x06001304 RID: 4868 RVA: 0x0003212F File Offset: 0x0003032F
		public virtual void WriteAttribute(string name, string value, bool fEncode)
		{
			this.Write(' ');
			this.Write(name);
			if (value != null)
			{
				this.Write("=\"");
				value = this.EncodeAttributeValue(value, fEncode);
				this.Write(value);
				this.Write('"');
			}
		}

		/// <summary>Writes any tab spacing and the opening tag of the specified markup element to the output stream.</summary>
		/// <param name="tagName">The markup element of which to write the opening tag. </param>
		// Token: 0x06001305 RID: 4869 RVA: 0x00032167 File Offset: 0x00030367
		public virtual void WriteBeginTag(string tagName)
		{
			this.Write('<');
			this.Write(tagName);
		}

		/// <summary>Writes any tab spacing and the closing tag of the specified markup element.</summary>
		/// <param name="tagName">The element to write the closing tag for. </param>
		// Token: 0x06001306 RID: 4870 RVA: 0x00032178 File Offset: 0x00030378
		public virtual void WriteEndTag(string tagName)
		{
			this.Write("</");
			this.Write(tagName);
			this.Write('>');
		}

		/// <summary>Writes any tab spacing and the opening tag of the specified markup element to the output stream.</summary>
		/// <param name="tagName">The element to write to the output stream. </param>
		// Token: 0x06001307 RID: 4871 RVA: 0x00032194 File Offset: 0x00030394
		public virtual void WriteFullBeginTag(string tagName)
		{
			this.Write('<');
			this.Write(tagName);
			this.Write('>');
		}

		/// <summary>Writes the specified style attribute to the output stream.</summary>
		/// <param name="name">The style attribute to write to the output stream. </param>
		/// <param name="value">The value assigned to the style attribute. </param>
		// Token: 0x06001308 RID: 4872 RVA: 0x000321AD File Offset: 0x000303AD
		public virtual void WriteStyleAttribute(string name, string value)
		{
			this.WriteStyleAttribute(name, value, false);
		}

		/// <summary>Writes the specified style attribute and value to the output stream, and encodes the value, if specified.</summary>
		/// <param name="name">The style attribute to write to the output stream. </param>
		/// <param name="value">The value assigned to the style attribute. </param>
		/// <param name="fEncode">true to encode the style attribute and its assigned value; otherwise, false. </param>
		// Token: 0x06001309 RID: 4873 RVA: 0x000321B8 File Offset: 0x000303B8
		public virtual void WriteStyleAttribute(string name, string value, bool fEncode)
		{
			this.Write(name);
			this.Write(':');
			this.Write(this.EncodeAttributeValue(value, fEncode));
			this.Write(';');
		}

		/// <summary>Writes the text representation of a subarray of Unicode characters to the output stream, along with any pending tab spacing.</summary>
		/// <param name="buffer">The array of characters from which to write text to the output stream. </param>
		/// <param name="index">The index location in the array where writing begins. </param>
		/// <param name="count">The number of characters to write to the output stream. </param>
		// Token: 0x0600130A RID: 4874 RVA: 0x000321DF File Offset: 0x000303DF
		public override void Write(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this.b.Write(buffer, index, count);
		}

		/// <summary>Writes the text representation of a double-precision floating-point number to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The double-precision floating-point number to write to the output stream. </param>
		// Token: 0x0600130B RID: 4875 RVA: 0x000321F5 File Offset: 0x000303F5
		public override void Write(double value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the text representation of a Unicode character to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The Unicode character to write to the output stream. </param>
		// Token: 0x0600130C RID: 4876 RVA: 0x00032209 File Offset: 0x00030409
		public override void Write(char value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the text representation of an array of Unicode characters to the output stream, along with any pending tab spacing.</summary>
		/// <param name="buffer">The array of Unicode characters to write to the output stream. </param>
		// Token: 0x0600130D RID: 4877 RVA: 0x0003221D File Offset: 0x0003041D
		public override void Write(char[] buffer)
		{
			this.OutputTabs();
			this.b.Write(buffer);
		}

		/// <summary>Writes the text representation of a 32-byte signed integer to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The 32-byte signed integer to write to the output stream. </param>
		// Token: 0x0600130E RID: 4878 RVA: 0x00032231 File Offset: 0x00030431
		public override void Write(int value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes a tab string and a formatted string to the output stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object)" /> method, along with any pending tab spacing.</summary>
		/// <param name="format">A string that contains zero or more format items. </param>
		/// <param name="arg0">An object to format.</param>
		// Token: 0x0600130F RID: 4879 RVA: 0x00032245 File Offset: 0x00030445
		public override void Write(string format, object arg0)
		{
			this.OutputTabs();
			this.b.Write(format, arg0);
		}

		/// <summary>Writes a formatted string that contains the text representation of two objects to the output stream, along with any pending tab spacing. This method uses the same semantics as the <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> method.</summary>
		/// <param name="format">A string that contains zero or more format items. </param>
		/// <param name="arg0">An object to format. </param>
		/// <param name="arg1">An object to format.</param>
		// Token: 0x06001310 RID: 4880 RVA: 0x0003225A File Offset: 0x0003045A
		public override void Write(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this.b.Write(format, arg0, arg1);
		}

		/// <summary>Writes a formatted string that contains the text representation of an object array to the output stream, along with any pending tab spacing. This method uses the same semantics as the <see cref="M:System.String.Format(System.String,System.Object[])" /> method.</summary>
		/// <param name="format">A string that contains zero or more format items. </param>
		/// <param name="arg">An object array to format. </param>
		// Token: 0x06001311 RID: 4881 RVA: 0x00032270 File Offset: 0x00030470
		public override void Write(string format, params object[] arg)
		{
			this.OutputTabs();
			this.b.Write(format, arg);
		}

		/// <summary>Writes the specified string to the output stream, along with any pending tab spacing.</summary>
		/// <param name="s">The string to write to the output stream. </param>
		// Token: 0x06001312 RID: 4882 RVA: 0x00032285 File Offset: 0x00030485
		public override void Write(string s)
		{
			this.OutputTabs();
			this.b.Write(s);
		}

		/// <summary>Writes the text representation of a 64-byte signed integer to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The 64-byte signed integer to write to the output stream. </param>
		// Token: 0x06001313 RID: 4883 RVA: 0x00032299 File Offset: 0x00030499
		public override void Write(long value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the text representation of an object to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The object to write to the output stream. </param>
		// Token: 0x06001314 RID: 4884 RVA: 0x000322AD File Offset: 0x000304AD
		public override void Write(object value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the text representation of a single-precision floating-point number to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The single-precision floating-point number to write to the output stream. </param>
		// Token: 0x06001315 RID: 4885 RVA: 0x000322C1 File Offset: 0x000304C1
		public override void Write(float value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the text representation of a Boolean value to the output stream, along with any pending tab spacing.</summary>
		/// <param name="value">The <see cref="T:System.Boolean" /> to write to the output stream. </param>
		// Token: 0x06001316 RID: 4886 RVA: 0x000322D5 File Offset: 0x000304D5
		public override void Write(bool value)
		{
			this.OutputTabs();
			this.b.Write(value);
		}

		/// <summary>Writes the specified markup attribute and value to the output stream.</summary>
		/// <param name="name">The attribute to write to the output stream. </param>
		/// <param name="value">The value assigned to the attribute. </param>
		// Token: 0x06001317 RID: 4887 RVA: 0x000322E9 File Offset: 0x000304E9
		public virtual void WriteAttribute(string name, string value)
		{
			this.WriteAttribute(name, value, false);
		}

		/// <summary>Writes any pending tab spacing and a Unicode character, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The character to write to the output stream. </param>
		// Token: 0x06001318 RID: 4888 RVA: 0x000322F4 File Offset: 0x000304F4
		public override void WriteLine(char value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a 64-byte signed integer, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The 64-byte signed integer to write to the output stream. </param>
		// Token: 0x06001319 RID: 4889 RVA: 0x0003230F File Offset: 0x0003050F
		public override void WriteLine(long value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of an object, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The object to write to the output stream. </param>
		// Token: 0x0600131A RID: 4890 RVA: 0x0003232A File Offset: 0x0003052A
		public override void WriteLine(object value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a double-precision floating-point number, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The double-precision floating-point number to write to the output stream. </param>
		// Token: 0x0600131B RID: 4891 RVA: 0x00032345 File Offset: 0x00030545
		public override void WriteLine(double value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and a subarray of Unicode characters, followed by a line terminator string, to the output stream.</summary>
		/// <param name="buffer">The character array from which to write text to the output stream. </param>
		/// <param name="index">The location in the character array where writing begins. </param>
		/// <param name="count">The number of characters in the array to write to the output stream. </param>
		// Token: 0x0600131C RID: 4892 RVA: 0x00032360 File Offset: 0x00030560
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this.b.WriteLine(buffer, index, count);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and an array of Unicode characters, followed by a line terminator string, to the output stream.</summary>
		/// <param name="buffer">The character array to write to the output stream. </param>
		// Token: 0x0600131D RID: 4893 RVA: 0x0003237D File Offset: 0x0003057D
		public override void WriteLine(char[] buffer)
		{
			this.OutputTabs();
			this.b.WriteLine(buffer);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a Boolean value, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The Boolean to write to the output stream. </param>
		// Token: 0x0600131E RID: 4894 RVA: 0x00032398 File Offset: 0x00030598
		public override void WriteLine(bool value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes a line terminator string to the output stream.</summary>
		// Token: 0x0600131F RID: 4895 RVA: 0x000323B3 File Offset: 0x000305B3
		public override void WriteLine()
		{
			this.OutputTabs();
			this.b.WriteLine();
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a 32-byte signed integer, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The 32-byte signed integer to write to the output stream. </param>
		// Token: 0x06001320 RID: 4896 RVA: 0x000323CD File Offset: 0x000305CD
		public override void WriteLine(int value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and a formatted string that contains the text representation of two objects, followed by a line terminator string, to the output stream.</summary>
		/// <param name="format">A string containing zero or more format items.</param>
		/// <param name="arg0">An object to format.</param>
		/// <param name="arg1">An object to format.</param>
		// Token: 0x06001321 RID: 4897 RVA: 0x000323E8 File Offset: 0x000305E8
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this.b.WriteLine(format, arg0, arg1);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and a formatted string containing the text representation of an object, followed by a line terminator string, to the output stream. </summary>
		/// <param name="format">A string containing zero or more format items. </param>
		/// <param name="arg0">An object to format.</param>
		// Token: 0x06001322 RID: 4898 RVA: 0x00032405 File Offset: 0x00030605
		public override void WriteLine(string format, object arg0)
		{
			this.OutputTabs();
			this.b.WriteLine(format, arg0);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and a formatted string that contains the text representation of an object array, followed by a line terminator string, to the output stream.</summary>
		/// <param name="format">A string containing zero or more format items.</param>
		/// <param name="arg">An object array to format. </param>
		// Token: 0x06001323 RID: 4899 RVA: 0x00032421 File Offset: 0x00030621
		public override void WriteLine(string format, params object[] arg)
		{
			this.OutputTabs();
			this.b.WriteLine(format, arg);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a 4-byte unsigned integer, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The 4-byte unsigned integer to write to the output stream. </param>
		// Token: 0x06001324 RID: 4900 RVA: 0x0003243D File Offset: 0x0003063D
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and a text string, followed by a line terminator string, to the output stream.</summary>
		/// <param name="s">The string to write to the output stream. </param>
		// Token: 0x06001325 RID: 4901 RVA: 0x00032458 File Offset: 0x00030658
		public override void WriteLine(string s)
		{
			this.OutputTabs();
			this.b.WriteLine(s);
			this.newline = true;
		}

		/// <summary>Writes any pending tab spacing and the text representation of a single-precision floating-point number, followed by a line terminator string, to the output stream.</summary>
		/// <param name="value">The single-precision floating point number to write to the output stream. </param>
		// Token: 0x06001326 RID: 4902 RVA: 0x00032473 File Offset: 0x00030673
		public override void WriteLine(float value)
		{
			this.OutputTabs();
			this.b.WriteLine(value);
			this.newline = true;
		}

		/// <summary>Writes a string, followed by a line terminator string, to the output stream. This method ignores any specified tab spacing.</summary>
		/// <param name="s">The string to write to the output stream. </param>
		// Token: 0x06001327 RID: 4903 RVA: 0x0003248E File Offset: 0x0003068E
		public void WriteLineNoTabs(string s)
		{
			this.b.WriteLine(s);
			this.newline = true;
		}

		/// <summary>Gets the encoding that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object uses to write content to the page.</summary>
		/// <returns>The <see cref="T:System.Text.Encoding" /> in which the markup is written to the page.</returns>
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x000324A3 File Offset: 0x000306A3
		public override Encoding Encoding
		{
			get
			{
				return this.b.Encoding;
			}
		}

		/// <summary>Gets or sets the number of tab positions to indent the beginning of each line of markup.</summary>
		/// <returns>The number of tab positions to indent each line.</returns>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x000324B0 File Offset: 0x000306B0
		// (set) Token: 0x0600132A RID: 4906 RVA: 0x000324B8 File Offset: 0x000306B8
		public int Indent
		{
			get
			{
				return this.indent;
			}
			set
			{
				this.indent = value;
			}
		}

		/// <summary>Gets or sets the text writer that writes the inner content of the markup element.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that writes the inner markup content.</returns>
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x000324C1 File Offset: 0x000306C1
		// (set) Token: 0x0600132C RID: 4908 RVA: 0x000324C9 File Offset: 0x000306C9
		public TextWriter InnerWriter
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		/// <summary>Gets or sets the line terminator string used by the <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <returns>The line terminator string used by the current <see cref="T:System.Web.UI.HtmlTextWriter" />.</returns>
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x000324D2 File Offset: 0x000306D2
		// (set) Token: 0x0600132E RID: 4910 RVA: 0x000324DF File Offset: 0x000306DF
		public override string NewLine
		{
			get
			{
				return this.b.NewLine;
			}
			set
			{
				this.b.NewLine = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the specified markup element.</summary>
		/// <returns>The markup element that is having its opening tag rendered.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property value cannot be set. </exception>
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x000324ED File Offset: 0x000306ED
		// (set) Token: 0x06001330 RID: 4912 RVA: 0x00032514 File Offset: 0x00030714
		protected HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.tagstack_pos == -1)
				{
					throw new InvalidOperationException();
				}
				return this.tagstack[this.tagstack_pos].key;
			}
			set
			{
				this.tagstack[this.tagstack_pos].key = value;
				this.tagstack[this.tagstack_pos].name = this.GetTagName(value);
			}
		}

		/// <summary>Gets or sets the tag name of the markup element being rendered.</summary>
		/// <returns>The tag name of the markup element being rendered.</returns>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0003254A File Offset: 0x0003074A
		// (set) Token: 0x06001332 RID: 4914 RVA: 0x00032574 File Offset: 0x00030774
		protected string TagName
		{
			get
			{
				if (this.tagstack_pos == -1)
				{
					throw new InvalidOperationException();
				}
				return this.tagstack[this.tagstack_pos].name;
			}
			set
			{
				this.tagstack[this.tagstack_pos].name = value;
				this.tagstack[this.tagstack_pos].key = this.GetTagKey(value);
				if (this.tagstack[this.tagstack_pos].key != HtmlTextWriterTag.Unknown)
				{
					this.tagstack[this.tagstack_pos].name = this.GetTagName(this.tagstack[this.tagstack_pos].key);
				}
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x000325FF File Offset: 0x000307FF
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x00032626 File Offset: 0x00030826
		private bool TagIgnore
		{
			get
			{
				if (this.tagstack_pos == -1)
				{
					throw new InvalidOperationException();
				}
				return this.tagstack[this.tagstack_pos].ignore;
			}
			set
			{
				if (this.tagstack_pos == -1)
				{
					throw new InvalidOperationException();
				}
				this.tagstack[this.tagstack_pos].ignore = value;
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0003264E File Offset: 0x0003084E
		internal HttpWriter GetHttpWriter()
		{
			return this.b as HttpWriter;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0003265C File Offset: 0x0003085C
		private void NextStyleStack()
		{
			if (this.styles == null)
			{
				this.styles = new HtmlTextWriter.AddedStyle[16];
			}
			int num = this.styles_pos + 1;
			this.styles_pos = num;
			if (num < this.styles.Length)
			{
				return;
			}
			HtmlTextWriter.AddedStyle[] array = new HtmlTextWriter.AddedStyle[this.styles.Length * 2];
			Array.Copy(this.styles, array, this.styles.Length);
			this.styles = array;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000326C8 File Offset: 0x000308C8
		private void NextAttrStack()
		{
			if (this.attrs == null)
			{
				this.attrs = new HtmlTextWriter.AddedAttr[16];
			}
			int num = this.attrs_pos + 1;
			this.attrs_pos = num;
			if (num < this.attrs.Length)
			{
				return;
			}
			HtmlTextWriter.AddedAttr[] array = new HtmlTextWriter.AddedAttr[this.attrs.Length * 2];
			Array.Copy(this.attrs, array, this.attrs.Length);
			this.attrs = array;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00032734 File Offset: 0x00030934
		private void NextTagStack()
		{
			if (this.tagstack == null)
			{
				this.tagstack = new HtmlTextWriter.AddedTag[16];
			}
			int num = this.tagstack_pos + 1;
			this.tagstack_pos = num;
			if (num < this.tagstack.Length)
			{
				return;
			}
			HtmlTextWriter.AddedTag[] array = new HtmlTextWriter.AddedTag[this.tagstack.Length * 2];
			Array.Copy(this.tagstack, array, this.tagstack.Length);
			this.tagstack = array;
		}

		/// <summary>Checks an attribute to ensure that it can be rendered in the opening tag of a &lt;form&gt; markup element. </summary>
		/// <returns>Always true.</returns>
		/// <param name="attribute">A string that contains the name of the attribute to check. </param>
		// Token: 0x06001339 RID: 4921 RVA: 0x00008B4A File Offset: 0x00006D4A
		public virtual bool IsValidFormAttribute(string attribute)
		{
			return true;
		}

		/// <summary>Writes a &lt;br /&gt; markup element to the output stream. </summary>
		// Token: 0x0600133A RID: 4922 RVA: 0x000327A0 File Offset: 0x000309A0
		public virtual void WriteBreak()
		{
			string tagName = this.GetTagName(HtmlTextWriterTag.Br);
			this.WriteBeginTag(tagName);
			this.Write(" />");
		}

		/// <summary>Encodes the specified text for the requesting device, and then writes it to the output stream. </summary>
		/// <param name="text">The text string to encode and write to the output stream. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="text" /> is null.</exception>
		// Token: 0x0600133B RID: 4923 RVA: 0x000327C8 File Offset: 0x000309C8
		public virtual void WriteEncodedText(string text)
		{
			this.Write(HttpUtility.HtmlEncode(text));
		}

		/// <summary>Encodes the specified URL, and then writes it to the output stream. The URL might include parameters.</summary>
		/// <param name="url">The URL string to encode and write to the output stream. </param>
		// Token: 0x0600133C RID: 4924 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoNotSupported("")]
		public virtual void WriteEncodedUrl(string url)
		{
			throw new NotImplementedException();
		}

		/// <summary>Encodes the specified URL parameter for the requesting device, and then writes it to the output stream.</summary>
		/// <param name="urlText">The URL parameter string to encode and write to the output stream. </param>
		// Token: 0x0600133D RID: 4925 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoNotSupported("")]
		public virtual void WriteEncodedUrlParameter(string urlText)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the specified string, encoding it according to URL requirements.</summary>
		/// <param name="text">The string to encode and write to the output stream. </param>
		/// <param name="argument">true to encode the string as a part of the parameter section of the URL; false to encode the string as part of the path section of the URL. </param>
		// Token: 0x0600133E RID: 4926 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoNotSupported("")]
		protected void WriteUrlEncodedString(string text, bool argument)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the opening tag of a &lt;span&gt; element that contains attributes that implement the layout and character formatting of the specified style. </summary>
		/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to begin applying to the block of markup. </param>
		// Token: 0x0600133F RID: 4927 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoNotSupported("")]
		public virtual void EnterStyle(Style style)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the opening tag of a markup element that contains attributes that implement the layout and character formatting of the specified style. </summary>
		/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to begin applying to the block of markup.</param>
		/// <param name="tag">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies the opening tag of the markup element that will contain the style object specified in <paramref name="style" />. </param>
		// Token: 0x06001340 RID: 4928 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoNotSupported("")]
		public virtual void EnterStyle(Style style, HtmlTextWriterTag tag)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the closing tag of a &lt;span&gt; element to end the specified layout and character formatting. </summary>
		/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to close. </param>
		// Token: 0x06001341 RID: 4929 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoNotSupported("")]
		public virtual void ExitStyle(Style style)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the closing tag of the specified markup element to end the specified layout and character formatting. </summary>
		/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to stop applying to the output text.</param>
		/// <param name="tag">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies the closing tag of the markup element that contained the attributes that applied the specified style. This must match the key passed in the corresponding <see cref="M:System.Web.UI.HtmlTextWriter.EnterStyle" /> call. </param>
		// Token: 0x06001342 RID: 4930 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoNotSupported("")]
		public virtual void ExitStyle(Style style, HtmlTextWriterTag tag)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies an <see cref="T:System.Web.UI.HtmlTextWriter" /> object, or an object of a derived class, that a control is about to be rendered. </summary>
		// Token: 0x06001343 RID: 4931 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void BeginRender()
		{
		}

		/// <summary>Notifies an <see cref="T:System.Web.UI.HtmlTextWriter" /> object, or an object of a derived class, that a control has finished rendering. You can use this method to close any markup elements opened in the <see cref="M:System.Web.UI.HtmlTextWriter.BeginRender" /> method.</summary>
		// Token: 0x06001344 RID: 4932 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void EndRender()
		{
		}

		// Token: 0x04001439 RID: 5177
		private static readonly Hashtable _tagTable = new Hashtable(HtmlTextWriter.tags.Length, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400143A RID: 5178
		private static readonly Hashtable _attributeTable = new Hashtable(HtmlTextWriter.htmlattrs.Length, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400143B RID: 5179
		private static readonly Hashtable _styleTable = new Hashtable(HtmlTextWriter.htmlstyles.Length, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400143C RID: 5180
		private int indent;

		// Token: 0x0400143D RID: 5181
		private TextWriter b;

		// Token: 0x0400143E RID: 5182
		private string tab_string;

		// Token: 0x0400143F RID: 5183
		private bool newline;

		// Token: 0x04001440 RID: 5184
		private HtmlTextWriter.AddedStyle[] styles;

		// Token: 0x04001441 RID: 5185
		private HtmlTextWriter.AddedAttr[] attrs;

		// Token: 0x04001442 RID: 5186
		private HtmlTextWriter.AddedTag[] tagstack;

		// Token: 0x04001443 RID: 5187
		private int styles_pos = -1;

		// Token: 0x04001444 RID: 5188
		private int attrs_pos = -1;

		// Token: 0x04001445 RID: 5189
		private int tagstack_pos = -1;

		/// <summary>Represents a single tab character.</summary>
		// Token: 0x04001446 RID: 5190
		public const string DefaultTabString = "\t";

		/// <summary>Represents the quotation mark (") character.</summary>
		// Token: 0x04001447 RID: 5191
		public const char DoubleQuoteChar = '"';

		/// <summary>Represents the left angle bracket and slash mark (&lt;/) of the closing tag of a markup element.</summary>
		// Token: 0x04001448 RID: 5192
		public const string EndTagLeftChars = "</";

		/// <summary>Represents the equal sign (=).</summary>
		// Token: 0x04001449 RID: 5193
		public const char EqualsChar = '=';

		/// <summary>Represents an equal sign (=) and a double quotation mark (") together in a string (="). </summary>
		// Token: 0x0400144A RID: 5194
		public const string EqualsDoubleQuoteString = "=\"";

		/// <summary>Represents a space and the self-closing slash mark (/) of a markup tag.</summary>
		// Token: 0x0400144B RID: 5195
		public const string SelfClosingChars = " /";

		/// <summary>Represents the closing slash mark and right angle bracket (/&gt;) of a self-closing markup element.</summary>
		// Token: 0x0400144C RID: 5196
		public const string SelfClosingTagEnd = " />";

		/// <summary>Represents the semicolon (;).</summary>
		// Token: 0x0400144D RID: 5197
		public const char SemicolonChar = ';';

		/// <summary>Represents an apostrophe (').</summary>
		// Token: 0x0400144E RID: 5198
		public const char SingleQuoteChar = '\'';

		/// <summary>Represents the slash mark (/).</summary>
		// Token: 0x0400144F RID: 5199
		public const char SlashChar = '/';

		/// <summary>Represents a space ( ) character.</summary>
		// Token: 0x04001450 RID: 5200
		public const char SpaceChar = ' ';

		/// <summary>Represents the style equals (:) character used to set style attributes equal to values.</summary>
		// Token: 0x04001451 RID: 5201
		public const char StyleEqualsChar = ':';

		/// <summary>Represents the opening angle bracket (&lt;) of a markup tag.</summary>
		// Token: 0x04001452 RID: 5202
		public const char TagLeftChar = '<';

		/// <summary>Represents the closing angle bracket (&gt;) of a markup tag.</summary>
		// Token: 0x04001453 RID: 5203
		public const char TagRightChar = '>';

		// Token: 0x04001454 RID: 5204
		private static HtmlTextWriter.HtmlTag[] tags = new HtmlTextWriter.HtmlTag[]
		{
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Unknown, string.Empty, HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.A, "a", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Acronym, "acronym", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Address, "address", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Area, "area", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.B, "b", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Base, "base", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Basefont, "basefont", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Bdo, "bdo", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Bgsound, "bgsound", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Big, "big", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Blockquote, "blockquote", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Body, "body", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Br, "br", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Button, "button", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Caption, "caption", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Center, "center", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Cite, "cite", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Code, "code", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Col, "col", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Colgroup, "colgroup", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dd, "dd", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Del, "del", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dfn, "dfn", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dir, "dir", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Div, "div", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dl, "dl", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dt, "dt", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Em, "em", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Embed, "embed", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Fieldset, "fieldset", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Font, "font", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Form, "form", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Frame, "frame", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Frameset, "frameset", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H1, "h1", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H2, "h2", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H3, "h3", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H4, "h4", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H5, "h5", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H6, "h6", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Head, "head", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Hr, "hr", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Html, "html", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.I, "i", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Iframe, "iframe", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Img, "img", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Input, "input", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ins, "ins", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Isindex, "isindex", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Kbd, "kbd", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Label, "label", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Legend, "legend", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Li, "li", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Link, "link", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Map, "map", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Marquee, "marquee", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Menu, "menu", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Meta, "meta", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Nobr, "nobr", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Noframes, "noframes", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Noscript, "noscript", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Object, "object", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ol, "ol", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Option, "option", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.P, "p", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Param, "param", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Pre, "pre", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Q, "q", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Rt, "rt", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ruby, "ruby", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.S, "s", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Samp, "samp", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Script, "script", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Select, "select", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Small, "small", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Span, "span", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Strike, "strike", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Strong, "strong", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Style, "style", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Sub, "sub", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Sup, "sup", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Table, "table", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tbody, "tbody", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Td, "td", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Textarea, "textarea", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tfoot, "tfoot", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Th, "th", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Thead, "thead", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Title, "title", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tr, "tr", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tt, "tt", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.U, "u", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ul, "ul", HtmlTextWriter.TagType.Block),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Var, "var", HtmlTextWriter.TagType.Inline),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Wbr, "wbr", HtmlTextWriter.TagType.SelfClosing),
			new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Xml, "xml", HtmlTextWriter.TagType.Block)
		};

		// Token: 0x04001455 RID: 5205
		private static HtmlTextWriter.HtmlAttribute[] htmlattrs = new HtmlTextWriter.HtmlAttribute[]
		{
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Accesskey, "accesskey"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Align, "align"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Alt, "alt"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Background, "background"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Bgcolor, "bgcolor"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Border, "border"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Bordercolor, "bordercolor"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Cellpadding, "cellpadding"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Cellspacing, "cellspacing"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Checked, "checked"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Class, "class"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Cols, "cols"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Colspan, "colspan"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Disabled, "disabled"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.For, "for"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Height, "height"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Href, "href"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Id, "id"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Maxlength, "maxlength"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Multiple, "multiple"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Name, "name"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Nowrap, "nowrap"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Onchange, "onchange"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Onclick, "onclick"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rows, "rows"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rowspan, "rowspan"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rules, "rules"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Selected, "selected"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Size, "size"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Src, "src"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Style, "style"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Tabindex, "tabindex"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Target, "target"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Title, "title"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Type, "type"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Valign, "valign"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Value, "value"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Width, "width"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Wrap, "wrap"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Abbr, "abbr"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.AutoComplete, "autocomplete"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Axis, "axis"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Content, "content"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Coords, "coords"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.DesignerRegion, "_designerregion"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Dir, "dir"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Headers, "headers"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Longdesc, "longdesc"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rel, "rel"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Scope, "scope"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Shape, "shape"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Usemap, "usemap"),
			new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.VCardName, "vcard_name")
		};

		// Token: 0x04001456 RID: 5206
		private static HtmlTextWriter.HtmlStyle[] htmlstyles = new HtmlTextWriter.HtmlStyle[]
		{
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BackgroundColor, "background-color"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BackgroundImage, "background-image"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderCollapse, "border-collapse"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderColor, "border-color"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderStyle, "border-style"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderWidth, "border-width"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Color, "color"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontFamily, "font-family"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontSize, "font-size"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontStyle, "font-style"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontWeight, "font-weight"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Height, "height"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.TextDecoration, "text-decoration"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Width, "width"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.ListStyleImage, "list-style-image"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.ListStyleType, "list-style-type"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Cursor, "cursor"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Direction, "direction"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Display, "display"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Filter, "filter"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontVariant, "font-variant"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Left, "left"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Margin, "margin"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginBottom, "margin-bottom"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginLeft, "margin-left"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginRight, "margin-right"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginTop, "margin-top"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Overflow, "overflow"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.OverflowX, "overflow-x"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.OverflowY, "overflow-y"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Padding, "padding"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingBottom, "padding-bottom"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingLeft, "padding-left"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingRight, "padding-right"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingTop, "padding-top"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Position, "position"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.TextAlign, "text-align"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.VerticalAlign, "vertical-align"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.TextOverflow, "text-overflow"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Top, "top"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Visibility, "visibility"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.WhiteSpace, "white-space"),
			new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.ZIndex, "z-index")
		};

		// Token: 0x020001D3 RID: 467
		private struct AddedTag
		{
			// Token: 0x04001457 RID: 5207
			public string name;

			// Token: 0x04001458 RID: 5208
			public HtmlTextWriterTag key;

			// Token: 0x04001459 RID: 5209
			public bool ignore;
		}

		// Token: 0x020001D4 RID: 468
		private struct AddedStyle
		{
			// Token: 0x0400145A RID: 5210
			public string name;

			// Token: 0x0400145B RID: 5211
			public HtmlTextWriterStyle key;

			// Token: 0x0400145C RID: 5212
			public string value;
		}

		// Token: 0x020001D5 RID: 469
		private struct AddedAttr
		{
			// Token: 0x0400145D RID: 5213
			public string name;

			// Token: 0x0400145E RID: 5214
			public HtmlTextWriterAttribute key;

			// Token: 0x0400145F RID: 5215
			public string value;
		}

		// Token: 0x020001D6 RID: 470
		private enum TagType
		{
			// Token: 0x04001461 RID: 5217
			Block,
			// Token: 0x04001462 RID: 5218
			Inline,
			// Token: 0x04001463 RID: 5219
			SelfClosing
		}

		// Token: 0x020001D7 RID: 471
		private sealed class HtmlTag
		{
			// Token: 0x06001345 RID: 4933 RVA: 0x000327D6 File Offset: 0x000309D6
			public HtmlTag(HtmlTextWriterTag k, string n, HtmlTextWriter.TagType tt)
			{
				this.key = k;
				this.name = n;
				this.tag_type = tt;
			}

			// Token: 0x04001464 RID: 5220
			public readonly HtmlTextWriterTag key;

			// Token: 0x04001465 RID: 5221
			public readonly string name;

			// Token: 0x04001466 RID: 5222
			public readonly HtmlTextWriter.TagType tag_type;
		}

		// Token: 0x020001D8 RID: 472
		private sealed class HtmlStyle
		{
			// Token: 0x06001346 RID: 4934 RVA: 0x000327F3 File Offset: 0x000309F3
			public HtmlStyle(HtmlTextWriterStyle k, string n)
			{
				this.key = k;
				this.name = n;
			}

			// Token: 0x04001467 RID: 5223
			public readonly HtmlTextWriterStyle key;

			// Token: 0x04001468 RID: 5224
			public readonly string name;
		}

		// Token: 0x020001D9 RID: 473
		private sealed class HtmlAttribute
		{
			// Token: 0x06001347 RID: 4935 RVA: 0x00032809 File Offset: 0x00030A09
			public HtmlAttribute(HtmlTextWriterAttribute k, string n)
			{
				this.key = k;
				this.name = n;
			}

			// Token: 0x04001469 RID: 5225
			public readonly HtmlTextWriterAttribute key;

			// Token: 0x0400146A RID: 5226
			public readonly string name;
		}
	}
}
