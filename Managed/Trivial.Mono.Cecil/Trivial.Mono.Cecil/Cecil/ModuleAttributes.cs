using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200008A RID: 138
	[Flags]
	public enum ModuleAttributes
	{
		// Token: 0x040001A3 RID: 419
		ILOnly = 1,
		// Token: 0x040001A4 RID: 420
		Required32Bit = 2,
		// Token: 0x040001A5 RID: 421
		ILLibrary = 4,
		// Token: 0x040001A6 RID: 422
		StrongNameSigned = 8,
		// Token: 0x040001A7 RID: 423
		Preferred32Bit = 131072
	}
}
