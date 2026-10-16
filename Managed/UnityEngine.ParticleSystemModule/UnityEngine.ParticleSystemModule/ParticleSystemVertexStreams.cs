using System;

namespace UnityEngine
{
	// Token: 0x0200005A RID: 90
	[Flags]
	[Obsolete("ParticleSystemVertexStreams is deprecated. Please use ParticleSystemVertexStream instead.", false)]
	public enum ParticleSystemVertexStreams
	{
		// Token: 0x04000183 RID: 387
		Position = 1,
		// Token: 0x04000184 RID: 388
		Normal = 2,
		// Token: 0x04000185 RID: 389
		Tangent = 4,
		// Token: 0x04000186 RID: 390
		Color = 8,
		// Token: 0x04000187 RID: 391
		UV = 16,
		// Token: 0x04000188 RID: 392
		UV2BlendAndFrame = 32,
		// Token: 0x04000189 RID: 393
		CenterAndVertexID = 64,
		// Token: 0x0400018A RID: 394
		Size = 128,
		// Token: 0x0400018B RID: 395
		Rotation = 256,
		// Token: 0x0400018C RID: 396
		Velocity = 512,
		// Token: 0x0400018D RID: 397
		Lifetime = 1024,
		// Token: 0x0400018E RID: 398
		Custom1 = 2048,
		// Token: 0x0400018F RID: 399
		Custom2 = 4096,
		// Token: 0x04000190 RID: 400
		Random = 8192,
		// Token: 0x04000191 RID: 401
		None = 0,
		// Token: 0x04000192 RID: 402
		All = 2147483647
	}
}
