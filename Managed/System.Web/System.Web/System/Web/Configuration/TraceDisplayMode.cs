using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the order in which trace messages are displayed.</summary>
	// Token: 0x02000576 RID: 1398
	public enum TraceDisplayMode
	{
		/// <summary>Emit trace messages in the order they were processed.</summary>
		// Token: 0x04002068 RID: 8296
		SortByTime = 1,
		/// <summary>Emit trace messages alphabetically by category.</summary>
		// Token: 0x04002069 RID: 8297
		SortByCategory
	}
}
