using System;

namespace UnityEngine
{
	// Token: 0x02000059 RID: 89
	[Flags]
	public enum ParticleSystemBakeTextureOptions
	{
		// Token: 0x0400017C RID: 380
		BakeRotationAndScale = 1,
		// Token: 0x0400017D RID: 381
		BakePosition = 2,
		// Token: 0x0400017E RID: 382
		PerVertex = 4,
		// Token: 0x0400017F RID: 383
		PerParticle = 8,
		// Token: 0x04000180 RID: 384
		IncludeParticleIndices = 16,
		// Token: 0x04000181 RID: 385
		Default = 4
	}
}
