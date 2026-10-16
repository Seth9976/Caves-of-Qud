using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000A1 RID: 161
	[Flags]
	public enum PropertyAttributes : ushort
	{
		// Token: 0x04000233 RID: 563
		None = 0,
		// Token: 0x04000234 RID: 564
		SpecialName = 512,
		// Token: 0x04000235 RID: 565
		RTSpecialName = 1024,
		// Token: 0x04000236 RID: 566
		HasDefault = 4096,
		// Token: 0x04000237 RID: 567
		Unused = 59903
	}
}
