using System;

namespace Mono.Cecil
{
	// Token: 0x0200024D RID: 589
	[Flags]
	public enum ModuleAttributes
	{
		// Token: 0x04000451 RID: 1105
		ILOnly = 1,
		// Token: 0x04000452 RID: 1106
		Required32Bit = 2,
		// Token: 0x04000453 RID: 1107
		ILLibrary = 4,
		// Token: 0x04000454 RID: 1108
		StrongNameSigned = 8,
		// Token: 0x04000455 RID: 1109
		Preferred32Bit = 131072
	}
}
