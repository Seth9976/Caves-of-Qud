using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000018 RID: 24
	[Flags]
	public enum ClipCaps
	{
		// Token: 0x04000091 RID: 145
		None = 0,
		// Token: 0x04000092 RID: 146
		Looping = 1,
		// Token: 0x04000093 RID: 147
		Extrapolation = 2,
		// Token: 0x04000094 RID: 148
		ClipIn = 4,
		// Token: 0x04000095 RID: 149
		SpeedMultiplier = 8,
		// Token: 0x04000096 RID: 150
		Blending = 16,
		// Token: 0x04000097 RID: 151
		AutoScale = 40,
		// Token: 0x04000098 RID: 152
		All = -1
	}
}
