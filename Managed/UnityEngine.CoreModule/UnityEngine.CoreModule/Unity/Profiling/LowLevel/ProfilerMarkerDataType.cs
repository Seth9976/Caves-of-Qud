using System;

namespace Unity.Profiling.LowLevel
{
	// Token: 0x02000075 RID: 117
	public enum ProfilerMarkerDataType : byte
	{
		// Token: 0x040001AD RID: 429
		InstanceId = 1,
		// Token: 0x040001AE RID: 430
		Int32,
		// Token: 0x040001AF RID: 431
		UInt32,
		// Token: 0x040001B0 RID: 432
		Int64,
		// Token: 0x040001B1 RID: 433
		UInt64,
		// Token: 0x040001B2 RID: 434
		Float,
		// Token: 0x040001B3 RID: 435
		Double,
		// Token: 0x040001B4 RID: 436
		String16 = 9,
		// Token: 0x040001B5 RID: 437
		Blob8 = 11,
		// Token: 0x040001B6 RID: 438
		GfxResourceId
	}
}
