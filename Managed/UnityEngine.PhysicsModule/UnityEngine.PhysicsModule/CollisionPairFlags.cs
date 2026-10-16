using System;

namespace UnityEngine
{
	// Token: 0x0200003D RID: 61
	internal enum CollisionPairFlags : ushort
	{
		// Token: 0x040000EE RID: 238
		RemovedShape = 1,
		// Token: 0x040000EF RID: 239
		RemovedOtherShape,
		// Token: 0x040000F0 RID: 240
		ActorPairHasFirstTouch = 4,
		// Token: 0x040000F1 RID: 241
		ActorPairLostTouch = 8,
		// Token: 0x040000F2 RID: 242
		InternalHasImpulses = 16,
		// Token: 0x040000F3 RID: 243
		InternalContactsAreFlipped = 32
	}
}
