using System;

namespace System.Diagnostics
{
	/// <summary>Specifies trace data options to be written to the trace output.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000236 RID: 566
	[Flags]
	public enum TraceOptions
	{
		/// <summary>Do not write any elements.</summary>
		// Token: 0x04000A1F RID: 2591
		None = 0,
		/// <summary>Write the logical operation stack, which is represented by the return value of the <see cref="P:System.Diagnostics.CorrelationManager.LogicalOperationStack" /> property.</summary>
		// Token: 0x04000A20 RID: 2592
		LogicalOperationStack = 1,
		/// <summary>Write the date and time. </summary>
		// Token: 0x04000A21 RID: 2593
		DateTime = 2,
		/// <summary>Write the timestamp, which is represented by the return value of the <see cref="M:System.Diagnostics.Stopwatch.GetTimestamp" /> method.</summary>
		// Token: 0x04000A22 RID: 2594
		Timestamp = 4,
		/// <summary>Write the process identity, which is represented by the return value of the <see cref="P:System.Diagnostics.Process.Id" /> property.</summary>
		// Token: 0x04000A23 RID: 2595
		ProcessId = 8,
		/// <summary>Write the thread identity, which is represented by the return value of the <see cref="P:System.Threading.Thread.ManagedThreadId" /> property for the current thread.</summary>
		// Token: 0x04000A24 RID: 2596
		ThreadId = 16,
		/// <summary>Write the call stack, which is represented by the return value of the <see cref="P:System.Environment.StackTrace" /> property.</summary>
		// Token: 0x04000A25 RID: 2597
		Callstack = 32
	}
}
