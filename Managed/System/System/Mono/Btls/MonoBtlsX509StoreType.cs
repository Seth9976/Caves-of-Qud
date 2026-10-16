using System;

namespace Mono.Btls
{
	// Token: 0x02000110 RID: 272
	internal enum MonoBtlsX509StoreType
	{
		// Token: 0x0400047B RID: 1147
		Custom,
		// Token: 0x0400047C RID: 1148
		MachineTrustedRoots,
		// Token: 0x0400047D RID: 1149
		MachineIntermediateCA,
		// Token: 0x0400047E RID: 1150
		MachineUntrusted,
		// Token: 0x0400047F RID: 1151
		UserTrustedRoots,
		// Token: 0x04000480 RID: 1152
		UserIntermediateCA,
		// Token: 0x04000481 RID: 1153
		UserUntrusted
	}
}
