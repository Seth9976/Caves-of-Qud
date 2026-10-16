using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000036 RID: 54
	[Flags]
	[Map]
	[CLSCompliant(false)]
	public enum SealType
	{
		// Token: 0x040001D1 RID: 465
		F_SEAL_SEAL = 1,
		// Token: 0x040001D2 RID: 466
		F_SEAL_SHRINK = 2,
		// Token: 0x040001D3 RID: 467
		F_SEAL_GROW = 4,
		// Token: 0x040001D4 RID: 468
		F_SEAL_WRITE = 8,
		// Token: 0x040001D5 RID: 469
		F_SEAL_FUTURE_WRITE = 16
	}
}
