using System;

namespace MonoMod.Utils
{
	// Token: 0x0200044E RID: 1102
	[Flags]
	internal enum Platform
	{
		// Token: 0x04001028 RID: 4136
		OS = 1,
		// Token: 0x04001029 RID: 4137
		Bits64 = 2,
		// Token: 0x0400102A RID: 4138
		NT = 4,
		// Token: 0x0400102B RID: 4139
		Unix = 8,
		// Token: 0x0400102C RID: 4140
		ARM = 65536,
		// Token: 0x0400102D RID: 4141
		Wine = 131072,
		// Token: 0x0400102E RID: 4142
		Unknown = 17,
		// Token: 0x0400102F RID: 4143
		Windows = 37,
		// Token: 0x04001030 RID: 4144
		MacOS = 73,
		// Token: 0x04001031 RID: 4145
		Linux = 137,
		// Token: 0x04001032 RID: 4146
		Android = 393,
		// Token: 0x04001033 RID: 4147
		iOS = 585
	}
}
