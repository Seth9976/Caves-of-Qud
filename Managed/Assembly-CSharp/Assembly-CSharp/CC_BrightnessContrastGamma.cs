using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Brightness, Contrast, Gamma")]
public class CC_BrightnessContrastGamma : CC_Base
{
	// Token: 0x0600005D RID: 93 RVA: 0x00003934 File Offset: 0x00001B34
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.brightness == 0f && this.contrast == 0f && this.gamma == 1f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetVector("_BCG", new Vector4((this.brightness + 100f) * 0.01f, (this.contrast + 100f) * 0.01f, 1f / this.gamma));
		base.material.SetVector("_Coeffs", new Vector4(this.redCoeff, this.greenCoeff, this.blueCoeff, 1f));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000048 RID: 72
	[Range(-100f, 100f)]
	public float brightness;

	// Token: 0x04000049 RID: 73
	[Range(-100f, 100f)]
	public float contrast;

	// Token: 0x0400004A RID: 74
	[Range(0f, 1f)]
	public float redCoeff = 0.5f;

	// Token: 0x0400004B RID: 75
	[Range(0f, 1f)]
	public float greenCoeff = 0.5f;

	// Token: 0x0400004C RID: 76
	[Range(0f, 1f)]
	public float blueCoeff = 0.5f;

	// Token: 0x0400004D RID: 77
	[Range(0.1f, 9.9f)]
	public float gamma = 1f;
}
