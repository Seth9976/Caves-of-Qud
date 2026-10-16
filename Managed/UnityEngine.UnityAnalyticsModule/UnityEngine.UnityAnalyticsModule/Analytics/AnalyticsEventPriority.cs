using System;

namespace UnityEngine.Analytics
{
	// Token: 0x02000011 RID: 17
	[Flags]
	public enum AnalyticsEventPriority
	{
		// Token: 0x0400001F RID: 31
		FlushQueueFlag = 1,
		// Token: 0x04000020 RID: 32
		CacheImmediatelyFlag = 2,
		// Token: 0x04000021 RID: 33
		AllowInStopModeFlag = 4,
		// Token: 0x04000022 RID: 34
		SendImmediateFlag = 8,
		// Token: 0x04000023 RID: 35
		NoCachingFlag = 16,
		// Token: 0x04000024 RID: 36
		NoRetryFlag = 32,
		// Token: 0x04000025 RID: 37
		NormalPriorityEvent = 0,
		// Token: 0x04000026 RID: 38
		NormalPriorityEvent_WithCaching = 2,
		// Token: 0x04000027 RID: 39
		NormalPriorityEvent_NoRetryNoCaching = 48,
		// Token: 0x04000028 RID: 40
		HighPriorityEvent = 1,
		// Token: 0x04000029 RID: 41
		HighPriorityEvent_InStopMode = 5,
		// Token: 0x0400002A RID: 42
		HighestPriorityEvent = 9,
		// Token: 0x0400002B RID: 43
		HighestPriorityEvent_NoRetryNoCaching = 49
	}
}
