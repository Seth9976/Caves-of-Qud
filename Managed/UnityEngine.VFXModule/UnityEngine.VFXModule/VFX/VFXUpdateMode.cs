using System;

namespace UnityEngine.VFX
{
	// Token: 0x0200000A RID: 10
	[Flags]
	internal enum VFXUpdateMode
	{
		// Token: 0x040000E8 RID: 232
		FixedDeltaTime = 0,
		// Token: 0x040000E9 RID: 233
		DeltaTime = 1,
		// Token: 0x040000EA RID: 234
		IgnoreTimeScale = 2,
		// Token: 0x040000EB RID: 235
		ExactFixedTimeStep = 4,
		// Token: 0x040000EC RID: 236
		DeltaTimeAndIgnoreTimeScale = 3,
		// Token: 0x040000ED RID: 237
		FixedDeltaAndExactTime = 4,
		// Token: 0x040000EE RID: 238
		FixedDeltaAndExactTimeAndIgnoreTimeScale = 6
	}
}
