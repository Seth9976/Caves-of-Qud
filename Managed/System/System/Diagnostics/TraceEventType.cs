using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Identifies the type of event that has caused the trace.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000230 RID: 560
	public enum TraceEventType
	{
		/// <summary>Fatal error or application crash.</summary>
		// Token: 0x040009FB RID: 2555
		Critical = 1,
		/// <summary>Recoverable error.</summary>
		// Token: 0x040009FC RID: 2556
		Error,
		/// <summary>Noncritical problem.</summary>
		// Token: 0x040009FD RID: 2557
		Warning = 4,
		/// <summary>Informational message.</summary>
		// Token: 0x040009FE RID: 2558
		Information = 8,
		/// <summary>Debugging trace.</summary>
		// Token: 0x040009FF RID: 2559
		Verbose = 16,
		/// <summary>Starting of a logical operation.</summary>
		// Token: 0x04000A00 RID: 2560
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Start = 256,
		/// <summary>Stopping of a logical operation.</summary>
		// Token: 0x04000A01 RID: 2561
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Stop = 512,
		/// <summary>Suspension of a logical operation.</summary>
		// Token: 0x04000A02 RID: 2562
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Suspend = 1024,
		/// <summary>Resumption of a logical operation.</summary>
		// Token: 0x04000A03 RID: 2563
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Resume = 2048,
		/// <summary>Changing of correlation identity.</summary>
		// Token: 0x04000A04 RID: 2564
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Transfer = 4096
	}
}
