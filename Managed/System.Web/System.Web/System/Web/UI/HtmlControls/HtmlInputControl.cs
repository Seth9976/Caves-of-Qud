using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Serves as the abstract base class that defines the methods, properties, and events common to all HTML input controls, such as the &lt;input type=text&gt;, &lt;input type=submit&gt;, and &lt;input type= file&gt; elements.</summary>
	// Token: 0x0200025E RID: 606
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HtmlInputControl : HtmlControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> class.</summary>
		/// <param name="type">The type of input control. </param>
		// Token: 0x060018AE RID: 6318 RVA: 0x00040926 File Offset: 0x0003EB26
		protected HtmlInputControl(string type)
			: base("input")
		{
			if (type == null)
			{
				type = string.Empty;
			}
			base.Attributes["type"] = type;
		}

		/// <summary>Gets or sets the unique identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> control.</summary>
		/// <returns>A string that represents the value of the <see cref="P:System.Web.UI.Control.UniqueID" /> property.</returns>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0003F6F3 File Offset: 0x0003D8F3
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x00003946 File Offset: 0x00001B46
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual string Name
		{
			get
			{
				return this.UniqueID;
			}
			set
			{
			}
		}

		/// <summary>Gets the type of an <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" />.</summary>
		/// <returns>A string that contains the type of an <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" />.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0004094E File Offset: 0x0003EB4E
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Type
		{
			get
			{
				return base.Attributes["type"];
			}
		}

		/// <summary>Gets or sets the value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> control.</summary>
		/// <returns>The value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" />.</returns>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x00040960 File Offset: 0x0003EB60
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x00040988 File Offset: 0x0003EB88
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string Value
		{
			get
			{
				string text = base.Attributes["value"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("value");
					return;
				}
				base.Attributes["value"] = value;
			}
		}

		/// <summary>Renders the attributes of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render to the client.</param>
		// Token: 0x060018B4 RID: 6324 RVA: 0x000409AF File Offset: 0x0003EBAF
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (base.Attributes["name"] == null)
			{
				writer.WriteAttribute("name", this.Name);
			}
			base.RenderAttributes(writer);
			writer.Write(" /");
		}
	}
}
