using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000170 RID: 368
	[Flags]
	internal enum AcmStreamOpenFlags
	{
		// Token: 0x04000824 RID: 2084
		Query = 1,
		// Token: 0x04000825 RID: 2085
		Async = 2,
		// Token: 0x04000826 RID: 2086
		NonRealTime = 4,
		// Token: 0x04000827 RID: 2087
		CallbackTypeMask = 458752,
		// Token: 0x04000828 RID: 2088
		CallbackNull = 0,
		// Token: 0x04000829 RID: 2089
		CallbackWindow = 65536,
		// Token: 0x0400082A RID: 2090
		CallbackTask = 131072,
		// Token: 0x0400082B RID: 2091
		CallbackFunction = 196608,
		// Token: 0x0400082C RID: 2092
		CallbackThread = 131072,
		// Token: 0x0400082D RID: 2093
		CallbackEvent = 327680
	}
}
