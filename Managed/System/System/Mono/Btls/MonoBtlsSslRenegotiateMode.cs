using System;

namespace Mono.Btls
{
	// Token: 0x020000F1 RID: 241
	[Flags]
	internal enum MonoBtlsSslRenegotiateMode
	{
		// Token: 0x040003F3 RID: 1011
		NEVER = 0,
		// Token: 0x040003F4 RID: 1012
		ONCE = 1,
		// Token: 0x040003F5 RID: 1013
		FREELY = 2,
		// Token: 0x040003F6 RID: 1014
		IGNORE = 3
	}
}
