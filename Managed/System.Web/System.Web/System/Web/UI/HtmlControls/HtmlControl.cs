using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Defines the methods, properties, and events common to all HTML server controls in the ASP.NET page framework.</summary>
	// Token: 0x02000252 RID: 594
	[Designer("System.Web.UI.Design.HtmlIntrinsicControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HtmlControl : Control, IAttributeAccessor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> class using default values.</summary>
		// Token: 0x06001833 RID: 6195 RVA: 0x0003F423 File Offset: 0x0003D623
		protected HtmlControl()
			: this("span")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> class using the specified tag.</summary>
		/// <param name="tag">A string that specifies the tag name of the control. </param>
		// Token: 0x06001834 RID: 6196 RVA: 0x0003F430 File Offset: 0x0003D630
		protected HtmlControl(string tag)
		{
			this._tagName = tag;
		}

		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> child server controls.</returns>
		// Token: 0x06001835 RID: 6197 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0003F43F File Offset: 0x0003D63F
		internal static string AttributeToString(int n)
		{
			if (n != -1)
			{
				return n.ToString(NumberFormatInfo.InvariantInfo);
			}
			return null;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0003F453 File Offset: 0x0003D653
		internal static string AttributeToString(string s)
		{
			if (s != null && s.Length != 0)
			{
				return s;
			}
			return null;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0003F464 File Offset: 0x0003D664
		internal void PreProcessRelativeReference(HtmlTextWriter writer, string attribName)
		{
			string text = this.Attributes[attribName];
			if (text != null && text.Length != 0)
			{
				try
				{
					text = base.ResolveClientUrl(text);
				}
				catch (Exception)
				{
					throw new HttpException(attribName + " property had malformed url");
				}
				writer.WriteAttribute(attribName, text);
				this.Attributes.Remove(attribName);
			}
		}

		/// <summary>Gets the value of the named attribute on the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> control.</summary>
		/// <returns>The value of this attribute on the element, as a <see cref="T:System.String" /> value. If the specified attribute does not exist on this element, returns an empty string ("").</returns>
		/// <param name="name">The name of the attribute. This argument is case-insensitive.</param>
		// Token: 0x06001839 RID: 6201 RVA: 0x0003F4CC File Offset: 0x0003D6CC
		protected virtual string GetAttribute(string name)
		{
			return this.Attributes[name];
		}

		/// <summary>Sets the value of the named attribute on the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> control.</summary>
		/// <param name="name">The name of the attribute to set.</param>
		/// <param name="value">The value to set the attribute to.</param>
		// Token: 0x0600183A RID: 6202 RVA: 0x0003F4DA File Offset: 0x0003D6DA
		protected virtual void SetAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IAttributeAccessor.GetAttribute(System.String)" />. </summary>
		/// <returns>The value of this attribute on the element, as a <see cref="T:System.String" /> value. If the specified attribute does not exist on this element, returns an empty string ("").</returns>
		/// <param name="name">The attribute name.</param>
		// Token: 0x0600183B RID: 6203 RVA: 0x0003F4CC File Offset: 0x0003D6CC
		string IAttributeAccessor.GetAttribute(string name)
		{
			return this.Attributes[name];
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IAttributeAccessor.SetAttribute(System.String,System.String)" />. </summary>
		/// <param name="name">The name of the attribute to set.</param>
		/// <param name="value">The value to set the attribute to.</param>
		// Token: 0x0600183C RID: 6204 RVA: 0x0003F4DA File Offset: 0x0003D6DA
		void IAttributeAccessor.SetAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		/// <summary>Renders the opening HTML tag of the control into the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x0600183D RID: 6205 RVA: 0x0003F4E9 File Offset: 0x0003D6E9
		protected virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.WriteBeginTag(this.TagName);
			this.RenderAttributes(writer);
			writer.Write('>');
		}

		/// <summary>Writes content to render on a client to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x0600183E RID: 6206 RVA: 0x0003F506 File Offset: 0x0003D706
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> control's attributes into the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x0600183F RID: 6207 RVA: 0x0003F50F File Offset: 0x0003D70F
		protected virtual void RenderAttributes(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.WriteAttribute("id", this.ClientID);
			}
			this.Attributes.Render(writer);
		}

		/// <summary>Gets a collection of all attribute name and value pairs expressed on a server control tag within the ASP.NET page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.AttributeCollection" /> object that contains all attribute name and value pairs expressed on a server control tag within the Web page.</returns>
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x0003F536 File Offset: 0x0003D736
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new AttributeCollection(this.ViewState);
				}
				return this._attributes;
			}
		}

		/// <summary>Gets or sets a value indicating whether the HTML server control is disabled.</summary>
		/// <returns>true if the control is disabled; otherwise, false. The default value is false.</returns>
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x0003F557 File Offset: 0x0003D757
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x0003F56C File Offset: 0x0003D76C
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		public bool Disabled
		{
			get
			{
				return this.Attributes["disabled"] != null;
			}
			set
			{
				if (!value)
				{
					this.Attributes.Remove("disabled");
					return;
				}
				this.Attributes["disabled"] = "disabled";
			}
		}

		/// <summary>Gets a collection of all cascading style sheet (CSS) properties applied to a specified HTML server control in the ASP.NET file.</summary>
		/// <returns>A <see cref="T:System.Web.UI.CssStyleCollection" /> object that contains the style properties for the HTML server control.</returns>
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0003F597 File Offset: 0x0003D797
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CssStyleCollection Style
		{
			get
			{
				return this.Attributes.CssStyle;
			}
		}

		/// <summary>Gets the element name of a tag that contains a runat=server attribute and value pair.</summary>
		/// <returns>The element name of the specified tag.</returns>
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0003F5A4 File Offset: 0x0003D7A4
		[WebSysDescription("")]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string TagName
		{
			get
			{
				return this._tagName;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> view state is case-sensitive.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected override bool ViewStateIgnoresCase
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400161B RID: 5659
		internal string _tagName;

		// Token: 0x0400161C RID: 5660
		private AttributeCollection _attributes;
	}
}
