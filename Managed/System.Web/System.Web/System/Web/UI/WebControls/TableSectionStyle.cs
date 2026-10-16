using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the style for a section of a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	// Token: 0x0200041E RID: 1054
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableSectionStyle : Style
	{
		/// <summary>Gets or sets a value indicating whether the table section is displayed.</summary>
		/// <returns>true if the table section is displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x0007B624 File Offset: 0x00079824
		// (set) Token: 0x06002F95 RID: 12181 RVA: 0x0007B64D File Offset: 0x0007984D
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool Visible
		{
			get
			{
				object obj = base.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}
	}
}
