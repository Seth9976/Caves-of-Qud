using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200008B RID: 139
	[Flags]
	public enum ModuleCharacteristics
	{
		// Token: 0x040001A9 RID: 425
		HighEntropyVA = 32,
		// Token: 0x040001AA RID: 426
		DynamicBase = 64,
		// Token: 0x040001AB RID: 427
		NoSEH = 1024,
		// Token: 0x040001AC RID: 428
		NXCompat = 256,
		// Token: 0x040001AD RID: 429
		AppContainer = 4096,
		// Token: 0x040001AE RID: 430
		TerminalServerAware = 32768
	}
}
