using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Analog TV")]
public class CC_AnalogTV : CC_Base
{
	// Token: 0x06000051 RID: 81 RVA: 0x0000367C File Offset: 0x0000187C
	private void Update()
	{
		if (this.autoPhase)
		{
			this.phase += Time.deltaTime * 0.25f;
		}
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000036A0 File Offset: 0x000018A0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Phase", this.phase);
		base.material.SetFloat("_NoiseIntensity", this.noiseIntensity);
		base.material.SetFloat("_ScanlinesIntensity", this.scanlinesIntensity);
		base.material.SetFloat("_ScanlinesCount", (float)((int)this.scanlinesCount));
		base.material.SetFloat("_ScanlinesOffset", this.scanlinesOffset);
		base.material.SetFloat("_Distortion", this.distortion);
		base.material.SetFloat("_CubicDistortion", this.cubicDistortion);
		base.material.SetFloat("_Scale", this.scale);
		Graphics.Blit(source, destination, base.material, this.grayscale ? 1 : 0);
	}

	// Token: 0x04000038 RID: 56
	public bool autoPhase = true;

	// Token: 0x04000039 RID: 57
	public float phase = 0.5f;

	// Token: 0x0400003A RID: 58
	public bool grayscale;

	// Token: 0x0400003B RID: 59
	[Range(0f, 1f)]
	public float noiseIntensity = 0.5f;

	// Token: 0x0400003C RID: 60
	[Range(0f, 10f)]
	public float scanlinesIntensity = 2f;

	// Token: 0x0400003D RID: 61
	[Range(0f, 4096f)]
	public float scanlinesCount = 768f;

	// Token: 0x0400003E RID: 62
	public float scanlinesOffset;

	// Token: 0x0400003F RID: 63
	[Range(-2f, 2f)]
	public float distortion = 0.2f;

	// Token: 0x04000040 RID: 64
	[Range(-2f, 2f)]
	public float cubicDistortion = 0.6f;

	// Token: 0x04000041 RID: 65
	[Range(0.01f, 2f)]
	public float scale = 0.8f;
}
