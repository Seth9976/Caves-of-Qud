using System;

namespace Mono.Cecil
{
	// Token: 0x02000259 RID: 601
	[Flags]
	public enum PropertyAttributes : ushort
	{
		// Token: 0x040004B1 RID: 1201
		None = 0,
		// Token: 0x040004B2 RID: 1202
		SpecialName = 512,
		// Token: 0x040004B3 RID: 1203
		RTSpecialName = 1024,
		// Token: 0x040004B4 RID: 1204
		HasDefault = 4096,
		// Token: 0x040004B5 RID: 1205
		Unused = 59903
	}
}
