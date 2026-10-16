using System;

namespace Mono.Cecil
{
	// Token: 0x020001FE RID: 510
	[Flags]
	public enum EventAttributes : ushort
	{
		// Token: 0x040002F7 RID: 759
		None = 0,
		// Token: 0x040002F8 RID: 760
		SpecialName = 512,
		// Token: 0x040002F9 RID: 761
		RTSpecialName = 1024
	}
}
