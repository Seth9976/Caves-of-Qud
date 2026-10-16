using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= password&gt; element on the server.</summary>
	// Token: 0x02000262 RID: 610
	[SupportsEventValidation]
	[DefaultEvent("ServerChange")]
	[ValidationProperty("Value")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputPassword : HtmlInputText, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputPassword" /> class using default values.</summary>
		// Token: 0x060018F2 RID: 6386 RVA: 0x00041077 File Offset: 0x0003F277
		public HtmlInputPassword()
			: base("password")
		{
		}

		/// <summary>Renders the attributes of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputPassword" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x060018F3 RID: 6387 RVA: 0x00041084 File Offset: 0x0003F284
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.Attributes.Remove("value");
			base.RenderAttributes(writer);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x000410A0 File Offset: 0x0003F2A0
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			if (base.Attributes["value"] != text)
			{
				base.Attributes["value"] = text;
				return true;
			}
			return false;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000410E1 File Offset: 0x0003F2E1
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.OnServerChange(EventArgs.Empty);
		}
	}
}
