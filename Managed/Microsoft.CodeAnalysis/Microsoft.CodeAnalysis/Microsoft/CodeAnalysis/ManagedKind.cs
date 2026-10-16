using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000255 RID: 597
	[Flags]
	internal enum ManagedKind : byte
	{
		// Token: 0x04000922 RID: 2338
		Unknown = 0,
		// Token: 0x04000923 RID: 2339
		Unmanaged = 1,
		// Token: 0x04000924 RID: 2340
		UnmanagedWithGenerics = 2,
		// Token: 0x04000925 RID: 2341
		Managed = 3
	}
}
