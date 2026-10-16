using System;

namespace System.Web.SessionState
{
	/// <summary>Identifies whether a session item from a data store is for a session that requires initialization.</summary>
	// Token: 0x0200049B RID: 1179
	public enum SessionStateActions
	{
		/// <summary>No initialization actions need to be performed by the calling code.</summary>
		// Token: 0x04001D6D RID: 7533
		None,
		/// <summary>The session item from the data store is for a session that requires initialization.</summary>
		// Token: 0x04001D6E RID: 7534
		InitializeItem
	}
}
