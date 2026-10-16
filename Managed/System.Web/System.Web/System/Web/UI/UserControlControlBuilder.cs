using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Supports the ASP.NET page parser in building an instance of a user control.</summary>
	// Token: 0x0200023D RID: 573
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class UserControlControlBuilder : ControlBuilder
	{
		/// <summary>Determines whether the control builder needs to get the control's inner text. </summary>
		/// <returns>true if the control builder requires the control's inner text; otherwise, false. </returns>
		// Token: 0x0600179C RID: 6044 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool NeedsTagInnerText()
		{
			return false;
		}

		/// <summary>Provides the <see cref="T:System.Web.UI.UserControlControlBuilder" /> object with the inner text of the control tag.</summary>
		/// <param name="text">The text to be provided.</param>
		// Token: 0x0600179D RID: 6045 RVA: 0x00003946 File Offset: 0x00001B46
		[global::System.MonoTODO("Not implemented, does nothing")]
		public override void SetTagInnerText(string text)
		{
		}

		/// <summary>Builds an instance of the control identified by the <see cref="P:System.Web.UI.ControlBuilder.ControlType" /> property. </summary>
		/// <returns>An instance of a user control identified by <see cref="P:System.Web.UI.ControlBuilder.ControlType" />.</returns>
		// Token: 0x0600179E RID: 6046 RVA: 0x0003AFA4 File Offset: 0x000391A4
		public override object BuildObject()
		{
			return base.BuildObject();
		}
	}
}
