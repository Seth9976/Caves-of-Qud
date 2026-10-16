using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the lifetime of a performance counter instance.</summary>
	// Token: 0x02000277 RID: 631
	public enum PerformanceCounterInstanceLifetime
	{
		/// <summary>Remove the performance counter instance when no counters are using the process category.</summary>
		// Token: 0x04000B43 RID: 2883
		Global,
		/// <summary>Remove the performance counter instance when the process is closed.</summary>
		// Token: 0x04000B44 RID: 2884
		Process
	}
}
