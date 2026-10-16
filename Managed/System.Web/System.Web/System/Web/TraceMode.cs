using System;

namespace System.Web
{
	/// <summary>Specifies in what order trace messages are emitted into the HTML output of a page.</summary>
	// Token: 0x020000E4 RID: 228
	public enum TraceMode
	{
		/// <summary>Emit trace messages in the order they were processed.</summary>
		// Token: 0x040010F9 RID: 4345
		SortByTime,
		/// <summary>Emit trace messages alphabetically by category.</summary>
		// Token: 0x040010FA RID: 4346
		SortByCategory,
		/// <summary>Specifies the default value of the <see cref="P:System.Web.TraceContext.TraceMode" /> enumeration, which is <see cref="F:System.Web.TraceMode.SortByTime" />.</summary>
		// Token: 0x040010FB RID: 4347
		Default
	}
}
