using System;

namespace System.Diagnostics
{
	/// <summary>Specifies what messages to output for the <see cref="T:System.Diagnostics.Debug" />, <see cref="T:System.Diagnostics.Trace" /> and <see cref="T:System.Diagnostics.TraceSwitch" /> classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000233 RID: 563
	public enum TraceLevel
	{
		/// <summary>Output no tracing and debugging messages.</summary>
		// Token: 0x04000A10 RID: 2576
		Off,
		/// <summary>Output error-handling messages.</summary>
		// Token: 0x04000A11 RID: 2577
		Error,
		/// <summary>Output warnings and error-handling messages.</summary>
		// Token: 0x04000A12 RID: 2578
		Warning,
		/// <summary>Output informational messages, warnings, and error-handling messages.</summary>
		// Token: 0x04000A13 RID: 2579
		Info,
		/// <summary>Output all debugging and tracing messages.</summary>
		// Token: 0x04000A14 RID: 2580
		Verbose
	}
}
