using System;

namespace Microsoft.DiaSymReader
{
	// Token: 0x02000102 RID: 258
	[Flags]
	internal enum SymUnmanagedWriterCreationOptions
	{
		// Token: 0x04000281 RID: 641
		Default = 0,
		// Token: 0x04000282 RID: 642
		UseAlternativeLoadPath = 2,
		// Token: 0x04000283 RID: 643
		UseComRegistry = 4,
		// Token: 0x04000284 RID: 644
		Deterministic = 8
	}
}
