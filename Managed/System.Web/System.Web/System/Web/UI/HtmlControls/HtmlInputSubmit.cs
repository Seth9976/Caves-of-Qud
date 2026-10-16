using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= submit&gt; element on the server.</summary>
	// Token: 0x02000265 RID: 613
	[SupportsEventValidation]
	[DefaultEvent("ServerClick")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputSubmit : HtmlInputButton, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputSubmit" /> class using default values.</summary>
		// Token: 0x06001910 RID: 6416 RVA: 0x00041383 File Offset: 0x0003F583
		public HtmlInputSubmit()
			: base("submit")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputSubmit" /> class using the specified type.</summary>
		/// <param name="type">The input button type. </param>
		// Token: 0x06001911 RID: 6417 RVA: 0x00041348 File Offset: 0x0003F548
		public HtmlInputSubmit(string type)
			: base(type)
		{
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00041390 File Offset: 0x0003F590
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			base.RaisePostBackEvent(eventArgument);
		}
	}
}
