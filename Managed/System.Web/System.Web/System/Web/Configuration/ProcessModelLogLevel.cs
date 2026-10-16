using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the event types to be logged to the event log.</summary>
	// Token: 0x0200056C RID: 1388
	public enum ProcessModelLogLevel
	{
		/// <summary>Specifies that no events are logged. This field is constant.</summary>
		// Token: 0x0400203B RID: 8251
		None,
		/// <summary>Specifies that all process events are logged. This field is constant.</summary>
		// Token: 0x0400203C RID: 8252
		All,
		/// <summary>Specifies that only unexpected shutdowns, memory-limit shutdowns, and deadlock shutdowns are logged. This field is constant.</summary>
		// Token: 0x0400203D RID: 8253
		Errors
	}
}
