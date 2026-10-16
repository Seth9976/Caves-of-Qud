using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Defines the methods, properties, and events for all HTML server control elements not represented by a specific .NET Framework class.</summary>
	// Token: 0x02000256 RID: 598
	[ConstructorNeedsTag(true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlGenericControl : HtmlContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlGenericControl" /> class with default values.</summary>
		// Token: 0x06001864 RID: 6244 RVA: 0x0003FB58 File Offset: 0x0003DD58
		public HtmlGenericControl()
			: this("span")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlGenericControl" /> class with the specified tag.</summary>
		/// <param name="tag">The name of the element for which this instance of the class is created. </param>
		// Token: 0x06001865 RID: 6245 RVA: 0x0003FB65 File Offset: 0x0003DD65
		public HtmlGenericControl(string tag)
		{
			if (tag == null)
			{
				tag = "";
			}
			this._tagName = tag;
		}

		/// <summary>Gets or sets the name of the HTML element represented by the <see cref="T:System.Web.UI.HtmlControls.HtmlGenericControl" /> control.</summary>
		/// <returns>The tag name of an element.</returns>
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0003F5A4 File Offset: 0x0003D7A4
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x0003FB7E File Offset: 0x0003DD7E
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public new string TagName
		{
			get
			{
				return this._tagName;
			}
			set
			{
				this._tagName = value;
			}
		}
	}
}
