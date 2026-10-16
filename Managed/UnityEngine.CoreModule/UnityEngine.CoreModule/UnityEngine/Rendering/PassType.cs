using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200049B RID: 1179
	public enum PassType
	{
		// Token: 0x04000D82 RID: 3458
		Normal,
		// Token: 0x04000D83 RID: 3459
		Vertex,
		// Token: 0x04000D84 RID: 3460
		VertexLM,
		// Token: 0x04000D85 RID: 3461
		[Obsolete("VertexLMRGBM PassType is obsolete. Please use VertexLM PassType together with DecodeLightmap shader function.")]
		VertexLMRGBM,
		// Token: 0x04000D86 RID: 3462
		ForwardBase,
		// Token: 0x04000D87 RID: 3463
		ForwardAdd,
		// Token: 0x04000D88 RID: 3464
		[Obsolete("Deferred Lighting was removed, so LightPrePassBase pass type is never used anymore.")]
		LightPrePassBase,
		// Token: 0x04000D89 RID: 3465
		[Obsolete("Deferred Lighting was removed, so LightPrePassFinal pass type is never used anymore.")]
		LightPrePassFinal,
		// Token: 0x04000D8A RID: 3466
		ShadowCaster,
		// Token: 0x04000D8B RID: 3467
		Deferred = 10,
		// Token: 0x04000D8C RID: 3468
		Meta,
		// Token: 0x04000D8D RID: 3469
		MotionVectors,
		// Token: 0x04000D8E RID: 3470
		ScriptableRenderPipeline,
		// Token: 0x04000D8F RID: 3471
		ScriptableRenderPipelineDefaultUnlit,
		// Token: 0x04000D90 RID: 3472
		GrabPass
	}
}
