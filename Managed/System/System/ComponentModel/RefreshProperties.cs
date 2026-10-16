using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers that indicate the type of a refresh of the Properties window.</summary>
	// Token: 0x02000439 RID: 1081
	public enum RefreshProperties
	{
		/// <summary>No refresh is necessary.</summary>
		// Token: 0x040010C1 RID: 4289
		None,
		/// <summary>The properties should be requeried and the view should be refreshed.</summary>
		// Token: 0x040010C2 RID: 4290
		All,
		/// <summary>The view should be refreshed.</summary>
		// Token: 0x040010C3 RID: 4291
		Repaint
	}
}
