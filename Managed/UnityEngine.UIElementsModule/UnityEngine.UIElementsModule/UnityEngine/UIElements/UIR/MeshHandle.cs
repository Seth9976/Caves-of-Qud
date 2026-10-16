using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200061F RID: 1567
	internal class MeshHandle : LinkedPoolItem<MeshHandle>
	{
		// Token: 0x04001507 RID: 5383
		internal Alloc allocVerts;

		// Token: 0x04001508 RID: 5384
		internal Alloc allocIndices;

		// Token: 0x04001509 RID: 5385
		internal uint triangleCount;

		// Token: 0x0400150A RID: 5386
		internal Page allocPage;

		// Token: 0x0400150B RID: 5387
		internal uint allocTime;

		// Token: 0x0400150C RID: 5388
		internal uint updateAllocID;
	}
}
