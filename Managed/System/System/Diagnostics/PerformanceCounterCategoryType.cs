using System;

namespace System.Diagnostics
{
	/// <summary>Indicates whether the performance counter category can have multiple instances.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000275 RID: 629
	public enum PerformanceCounterCategoryType
	{
		/// <summary>The performance counter category can have only a single instance.</summary>
		// Token: 0x04000B3F RID: 2879
		SingleInstance,
		/// <summary>The performance counter category can have multiple instances.</summary>
		// Token: 0x04000B40 RID: 2880
		MultiInstance,
		/// <summary>The instance functionality for the performance counter category is unknown. </summary>
		// Token: 0x04000B41 RID: 2881
		Unknown = -1
	}
}
