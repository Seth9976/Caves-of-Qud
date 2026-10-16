using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Specifies the levels of trace messages filtered by the source switch and event type filter.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000225 RID: 549
	[Flags]
	public enum SourceLevels
	{
		/// <summary>Does not allow any events through.</summary>
		// Token: 0x040009CF RID: 2511
		Off = 0,
		/// <summary>Allows only <see cref="F:System.Diagnostics.TraceEventType.Critical" /> events through.</summary>
		// Token: 0x040009D0 RID: 2512
		Critical = 1,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" /> and <see cref="F:System.Diagnostics.TraceEventType.Error" /> events through.</summary>
		// Token: 0x040009D1 RID: 2513
		Error = 3,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" />, <see cref="F:System.Diagnostics.TraceEventType.Error" />, and <see cref="F:System.Diagnostics.TraceEventType.Warning" /> events through.</summary>
		// Token: 0x040009D2 RID: 2514
		Warning = 7,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" />, <see cref="F:System.Diagnostics.TraceEventType.Error" />, <see cref="F:System.Diagnostics.TraceEventType.Warning" />, and <see cref="F:System.Diagnostics.TraceEventType.Information" /> events through.</summary>
		// Token: 0x040009D3 RID: 2515
		Information = 15,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" />, <see cref="F:System.Diagnostics.TraceEventType.Error" />, <see cref="F:System.Diagnostics.TraceEventType.Warning" />, <see cref="F:System.Diagnostics.TraceEventType.Information" />, and <see cref="F:System.Diagnostics.TraceEventType.Verbose" /> events through.</summary>
		// Token: 0x040009D4 RID: 2516
		Verbose = 31,
		/// <summary>Allows the <see cref="F:System.Diagnostics.TraceEventType.Stop" />, <see cref="F:System.Diagnostics.TraceEventType.Start" />, <see cref="F:System.Diagnostics.TraceEventType.Suspend" />, <see cref="F:System.Diagnostics.TraceEventType.Transfer" />, and <see cref="F:System.Diagnostics.TraceEventType.Resume" /> events through.</summary>
		// Token: 0x040009D5 RID: 2517
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ActivityTracing = 65280,
		/// <summary>Allows all events through.</summary>
		// Token: 0x040009D6 RID: 2518
		All = -1
	}
}
