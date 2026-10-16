using System;

namespace System.IO
{
	// Token: 0x0200051D RID: 1309
	[Flags]
	internal enum EventFlags : ushort
	{
		// Token: 0x0400169E RID: 5790
		Add = 1,
		// Token: 0x0400169F RID: 5791
		Delete = 2,
		// Token: 0x040016A0 RID: 5792
		Enable = 4,
		// Token: 0x040016A1 RID: 5793
		Disable = 8,
		// Token: 0x040016A2 RID: 5794
		OneShot = 16,
		// Token: 0x040016A3 RID: 5795
		Clear = 32,
		// Token: 0x040016A4 RID: 5796
		Receipt = 64,
		// Token: 0x040016A5 RID: 5797
		Dispatch = 128,
		// Token: 0x040016A6 RID: 5798
		Flag0 = 4096,
		// Token: 0x040016A7 RID: 5799
		Flag1 = 8192,
		// Token: 0x040016A8 RID: 5800
		SystemFlags = 61440,
		// Token: 0x040016A9 RID: 5801
		EOF = 32768,
		// Token: 0x040016AA RID: 5802
		Error = 16384
	}
}
