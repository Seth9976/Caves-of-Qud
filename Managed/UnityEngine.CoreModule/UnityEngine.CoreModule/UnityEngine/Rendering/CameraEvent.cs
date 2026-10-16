using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000496 RID: 1174
	public enum CameraEvent
	{
		// Token: 0x04000D37 RID: 3383
		BeforeDepthTexture,
		// Token: 0x04000D38 RID: 3384
		AfterDepthTexture,
		// Token: 0x04000D39 RID: 3385
		BeforeDepthNormalsTexture,
		// Token: 0x04000D3A RID: 3386
		AfterDepthNormalsTexture,
		// Token: 0x04000D3B RID: 3387
		BeforeGBuffer,
		// Token: 0x04000D3C RID: 3388
		AfterGBuffer,
		// Token: 0x04000D3D RID: 3389
		BeforeLighting,
		// Token: 0x04000D3E RID: 3390
		AfterLighting,
		// Token: 0x04000D3F RID: 3391
		BeforeFinalPass,
		// Token: 0x04000D40 RID: 3392
		AfterFinalPass,
		// Token: 0x04000D41 RID: 3393
		BeforeForwardOpaque,
		// Token: 0x04000D42 RID: 3394
		AfterForwardOpaque,
		// Token: 0x04000D43 RID: 3395
		BeforeImageEffectsOpaque,
		// Token: 0x04000D44 RID: 3396
		AfterImageEffectsOpaque,
		// Token: 0x04000D45 RID: 3397
		BeforeSkybox,
		// Token: 0x04000D46 RID: 3398
		AfterSkybox,
		// Token: 0x04000D47 RID: 3399
		BeforeForwardAlpha,
		// Token: 0x04000D48 RID: 3400
		AfterForwardAlpha,
		// Token: 0x04000D49 RID: 3401
		BeforeImageEffects,
		// Token: 0x04000D4A RID: 3402
		AfterImageEffects,
		// Token: 0x04000D4B RID: 3403
		AfterEverything,
		// Token: 0x04000D4C RID: 3404
		BeforeReflections,
		// Token: 0x04000D4D RID: 3405
		AfterReflections,
		// Token: 0x04000D4E RID: 3406
		BeforeHaloAndLensFlares,
		// Token: 0x04000D4F RID: 3407
		AfterHaloAndLensFlares
	}
}
