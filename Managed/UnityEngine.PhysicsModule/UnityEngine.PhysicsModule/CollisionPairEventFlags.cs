using System;

namespace UnityEngine
{
	// Token: 0x0200003E RID: 62
	internal enum CollisionPairEventFlags : ushort
	{
		// Token: 0x040000F5 RID: 245
		SolveContacts = 1,
		// Token: 0x040000F6 RID: 246
		ModifyContacts,
		// Token: 0x040000F7 RID: 247
		NotifyTouchFound = 4,
		// Token: 0x040000F8 RID: 248
		NotifyTouchPersists = 8,
		// Token: 0x040000F9 RID: 249
		NotifyTouchLost = 16,
		// Token: 0x040000FA RID: 250
		NotifyTouchCCD = 32,
		// Token: 0x040000FB RID: 251
		NotifyThresholdForceFound = 64,
		// Token: 0x040000FC RID: 252
		NotifyThresholdForcePersists = 128,
		// Token: 0x040000FD RID: 253
		NotifyThresholdForceLost = 256,
		// Token: 0x040000FE RID: 254
		NotifyContactPoint = 512,
		// Token: 0x040000FF RID: 255
		DetectDiscreteContact = 1024,
		// Token: 0x04000100 RID: 256
		DetectCCDContact = 2048,
		// Token: 0x04000101 RID: 257
		PreSolverVelocity = 4096,
		// Token: 0x04000102 RID: 258
		PostSolverVelocity = 8192,
		// Token: 0x04000103 RID: 259
		ContactEventPose = 16384,
		// Token: 0x04000104 RID: 260
		NextFree = 32768,
		// Token: 0x04000105 RID: 261
		ContactDefault = 1025,
		// Token: 0x04000106 RID: 262
		TriggerDefault = 1044
	}
}
