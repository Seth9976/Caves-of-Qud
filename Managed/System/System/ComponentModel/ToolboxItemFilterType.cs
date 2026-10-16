using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers used to indicate the type of filter that a <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> uses.</summary>
	// Token: 0x020003F4 RID: 1012
	public enum ToolboxItemFilterType
	{
		/// <summary>Indicates that a toolbox item filter string is allowed, but not required.</summary>
		// Token: 0x0400100F RID: 4111
		Allow,
		/// <summary>Indicates that custom processing is required to determine whether to use a toolbox item filter string. </summary>
		// Token: 0x04001010 RID: 4112
		Custom,
		/// <summary>Indicates that a toolbox item filter string is not allowed. </summary>
		// Token: 0x04001011 RID: 4113
		Prevent,
		/// <summary>Indicates that a toolbox item filter string must be present for a toolbox item to be enabled. </summary>
		// Token: 0x04001012 RID: 4114
		Require
	}
}
