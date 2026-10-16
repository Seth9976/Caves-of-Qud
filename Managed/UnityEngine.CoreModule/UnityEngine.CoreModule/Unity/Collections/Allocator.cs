using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x0200009F RID: 159
	[UsedByNativeCode]
	public enum Allocator
	{
		// Token: 0x0400025B RID: 603
		Invalid,
		// Token: 0x0400025C RID: 604
		None,
		// Token: 0x0400025D RID: 605
		Temp,
		// Token: 0x0400025E RID: 606
		TempJob,
		// Token: 0x0400025F RID: 607
		Persistent,
		// Token: 0x04000260 RID: 608
		AudioKernel,
		// Token: 0x04000261 RID: 609
		Domain,
		// Token: 0x04000262 RID: 610
		FirstUserIndex = 64
	}
}
