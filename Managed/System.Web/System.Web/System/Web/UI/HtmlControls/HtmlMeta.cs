using System;
using System.ComponentModel;
using System.Web.Configuration;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;meta&gt; tag on the server. </summary>
	// Token: 0x02000268 RID: 616
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	public class HtmlMeta : HtmlControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> class. </summary>
		// Token: 0x0600192D RID: 6445 RVA: 0x00041638 File Offset: 0x0003F838
		public HtmlMeta()
			: base("meta")
		{
		}

		/// <summary>Gets or sets the metadata property value defined by the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control.</summary>
		/// <returns>The metadata property value. </returns>
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x00041648 File Offset: 0x0003F848
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x00041670 File Offset: 0x0003F870
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public virtual string Content
		{
			get
			{
				string text = base.Attributes["content"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("content");
					return;
				}
				base.Attributes["content"] = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control property that is included in the HTTP response header.</summary>
		/// <returns>The name of the HTTP response header item. </returns>
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x00041698 File Offset: 0x0003F898
		// (set) Token: 0x06001931 RID: 6449 RVA: 0x000416C0 File Offset: 0x0003F8C0
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string HttpEquiv
		{
			get
			{
				string text = base.Attributes["http-equiv"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("http-equiv");
					return;
				}
				base.Attributes["http-equiv"] = value;
			}
		}

		/// <summary>Gets or sets the metadata property name defined by the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control. </summary>
		/// <returns>The property name. </returns>
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x000416E8 File Offset: 0x0003F8E8
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x0004117C File Offset: 0x0003F37C
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Name
		{
			get
			{
				string text = base.Attributes["name"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("name");
					return;
				}
				base.Attributes["name"] = value;
			}
		}

		/// <summary>Gets or sets a scheme attribute used to interpret the metadata property value defined by the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control.</summary>
		/// <returns>The scheme attribute. </returns>
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x00041710 File Offset: 0x0003F910
		// (set) Token: 0x06001935 RID: 6453 RVA: 0x00041738 File Offset: 0x0003F938
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Scheme
		{
			get
			{
				string text = base.Attributes["scheme"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("scheme");
					return;
				}
				base.Attributes["scheme"] = value;
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control to the client's browser using the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> used to render the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control's content on the client's browser.</param>
		// Token: 0x06001936 RID: 6454 RVA: 0x00041760 File Offset: 0x0003F960
		protected internal override void Render(HtmlTextWriter writer)
		{
			XhtmlConformanceSection xhtmlConformanceSection = WebConfigurationManager.GetSection("system.web/xhtmlConformance") as XhtmlConformanceSection;
			if (xhtmlConformanceSection != null && xhtmlConformanceSection.Mode == XhtmlConformanceMode.Legacy)
			{
				base.Render(writer);
				return;
			}
			writer.WriteBeginTag(this.TagName);
			this.RenderAttributes(writer);
			writer.Write("/>");
		}
	}
}
