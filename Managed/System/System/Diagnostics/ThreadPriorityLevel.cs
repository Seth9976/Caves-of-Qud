using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the priority level of a thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000284 RID: 644
	public enum ThreadPriorityLevel
	{
		/// <summary>Specifies one step above the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B8A RID: 2954
		AboveNormal = 1,
		/// <summary>Specifies one step below the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B8B RID: 2955
		BelowNormal = -1,
		/// <summary>Specifies highest priority. This is two steps above the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B8C RID: 2956
		Highest = 2,
		/// <summary>Specifies idle priority. This is the lowest possible priority value of all threads, independent of the value of the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B8D RID: 2957
		Idle = -15,
		/// <summary>Specifies lowest priority. This is two steps below the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B8E RID: 2958
		Lowest = -2,
		/// <summary>Specifies normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B8F RID: 2959
		Normal = 0,
		/// <summary>Specifies time-critical priority. This is the highest priority of all threads, independent of the value of the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B90 RID: 2960
		TimeCritical = 15
	}
}
