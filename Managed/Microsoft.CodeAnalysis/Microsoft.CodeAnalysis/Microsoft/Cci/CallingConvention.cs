using System;

namespace Microsoft.Cci
{
	// Token: 0x02000096 RID: 150
	[Flags]
	internal enum CallingConvention
	{
		// Token: 0x040001A0 RID: 416
		CDecl = 1,
		// Token: 0x040001A1 RID: 417
		Default = 0,
		// Token: 0x040001A2 RID: 418
		ExtraArguments = 5,
		// Token: 0x040001A3 RID: 419
		FastCall = 4,
		// Token: 0x040001A4 RID: 420
		Standard = 2,
		// Token: 0x040001A5 RID: 421
		ThisCall = 3,
		// Token: 0x040001A6 RID: 422
		Unmanaged = 9,
		// Token: 0x040001A7 RID: 423
		Generic = 16,
		// Token: 0x040001A8 RID: 424
		HasThis = 32,
		// Token: 0x040001A9 RID: 425
		ExplicitThis = 64
	}
}
