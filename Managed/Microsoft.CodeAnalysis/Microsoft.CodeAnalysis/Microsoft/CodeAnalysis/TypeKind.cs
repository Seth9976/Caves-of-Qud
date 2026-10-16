using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000268 RID: 616
	public enum TypeKind : byte
	{
		// Token: 0x0400098A RID: 2442
		Unknown,
		// Token: 0x0400098B RID: 2443
		Array,
		// Token: 0x0400098C RID: 2444
		Class,
		// Token: 0x0400098D RID: 2445
		Delegate,
		// Token: 0x0400098E RID: 2446
		Dynamic,
		// Token: 0x0400098F RID: 2447
		Enum,
		// Token: 0x04000990 RID: 2448
		Error,
		// Token: 0x04000991 RID: 2449
		Interface,
		// Token: 0x04000992 RID: 2450
		Module,
		// Token: 0x04000993 RID: 2451
		Pointer,
		// Token: 0x04000994 RID: 2452
		Struct,
		// Token: 0x04000995 RID: 2453
		Structure = 10,
		// Token: 0x04000996 RID: 2454
		TypeParameter,
		// Token: 0x04000997 RID: 2455
		Submission,
		// Token: 0x04000998 RID: 2456
		FunctionPointer
	}
}
