using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200002C RID: 44
	[Flags]
	public enum EventAttributes : ushort
	{
		// Token: 0x04000051 RID: 81
		None = 0,
		// Token: 0x04000052 RID: 82
		SpecialName = 512,
		// Token: 0x04000053 RID: 83
		RTSpecialName = 1024
	}
}
