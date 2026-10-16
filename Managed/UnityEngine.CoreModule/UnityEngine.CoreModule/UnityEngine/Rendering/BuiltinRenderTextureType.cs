using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200049A RID: 1178
	public enum BuiltinRenderTextureType
	{
		// Token: 0x04000D6A RID: 3434
		PropertyName = -4,
		// Token: 0x04000D6B RID: 3435
		BufferPtr,
		// Token: 0x04000D6C RID: 3436
		RenderTexture,
		// Token: 0x04000D6D RID: 3437
		BindableTexture,
		// Token: 0x04000D6E RID: 3438
		None,
		// Token: 0x04000D6F RID: 3439
		CurrentActive,
		// Token: 0x04000D70 RID: 3440
		CameraTarget,
		// Token: 0x04000D71 RID: 3441
		Depth,
		// Token: 0x04000D72 RID: 3442
		DepthNormals,
		// Token: 0x04000D73 RID: 3443
		ResolvedDepth,
		// Token: 0x04000D74 RID: 3444
		[Obsolete("Deferred Lighting has been removed, so PrepassNormalsSpec built-in render texture type is never used now.", false)]
		PrepassNormalsSpec = 7,
		// Token: 0x04000D75 RID: 3445
		[Obsolete("Deferred Lighting has been removed, so PrepassLight built-in render texture type is never used now.", false)]
		PrepassLight,
		// Token: 0x04000D76 RID: 3446
		[Obsolete("Deferred Lighting has been removed, so PrepassLightSpec built-in render texture type is never used now.", false)]
		PrepassLightSpec,
		// Token: 0x04000D77 RID: 3447
		GBuffer0,
		// Token: 0x04000D78 RID: 3448
		GBuffer1,
		// Token: 0x04000D79 RID: 3449
		GBuffer2,
		// Token: 0x04000D7A RID: 3450
		GBuffer3,
		// Token: 0x04000D7B RID: 3451
		Reflections,
		// Token: 0x04000D7C RID: 3452
		MotionVectors,
		// Token: 0x04000D7D RID: 3453
		GBuffer4,
		// Token: 0x04000D7E RID: 3454
		GBuffer5,
		// Token: 0x04000D7F RID: 3455
		GBuffer6,
		// Token: 0x04000D80 RID: 3456
		GBuffer7
	}
}
