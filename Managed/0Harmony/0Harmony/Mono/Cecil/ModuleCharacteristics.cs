using System;

namespace Mono.Cecil
{
	// Token: 0x0200024E RID: 590
	[Flags]
	public enum ModuleCharacteristics
	{
		// Token: 0x04000457 RID: 1111
		HighEntropyVA = 32,
		// Token: 0x04000458 RID: 1112
		DynamicBase = 64,
		// Token: 0x04000459 RID: 1113
		NoSEH = 1024,
		// Token: 0x0400045A RID: 1114
		NXCompat = 256,
		// Token: 0x0400045B RID: 1115
		AppContainer = 4096,
		// Token: 0x0400045C RID: 1116
		TerminalServerAware = 32768
	}
}
